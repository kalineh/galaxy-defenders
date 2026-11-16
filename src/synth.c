#include "synth.h"
#include <math.h>
#include <stddef.h>
#include <string.h>

typedef struct {
    uint32_t frame, duration;
    unsigned char patch, note, velocity;
    signed char pan;
} SynthEvent;

typedef struct { const char *name, *source; uint32_t frames, count; const SynthEvent *events; unsigned delay; float wet, feedback; } SynthTrack;

enum { FM_BASS, FM_LEAD, PULSE, BELL, PAD, KICK, SNARE, HAT, TOM, SAW, TRIANGLE, LAYERED_PULSE, PATCH_COUNT };
typedef struct {
    int kind;
    float attack, decay, sustain, release, gain;
    float ratio, index, brightness, vibrato;
} Patch;

#include "synth_score.inc"

#define SINE_SIZE 2048
static float sine_table[SINE_SIZE + 1];
static bool sine_ready;

static float sine(float phase) {
    phase -= floorf(phase);
    float index = phase * SINE_SIZE;
    int i = (int)index;
    return sine_table[i] + (sine_table[i + 1] - sine_table[i]) * (index - i);
}

static float blep(float phase, float delta) {
    if (phase < delta) { float x = phase / delta; return x + x - x * x - 1; }
    if (phase > 1 - delta) { float x = (phase - 1) / delta; return x * x + x + x + 1; }
    return 0;
}

const char *synth_track_name(int track) { return track >= 1 && track <= SYNTH_TRACK_COUNT ? synth_tracks[track-1].name : NULL; }
uint32_t synth_track_frames(int track) { return track >= 1 && track <= SYNTH_TRACK_COUNT ? synth_tracks[track-1].frames : 0; }
int synth_track_find(const char *source) {
    for (int i = 0; i < SYNTH_TRACK_COUNT; ++i) if (!strcmp(source, synth_tracks[i].source)) return i+1;
    return 0;
}
bool synth_select(Synth *synth, int track) {
    if (!synth_track_name(track)) return false;
    synth_init(synth); synth->track = track; return true;
}

void synth_init(Synth *synth) {
    if (!sine_ready) {
        for (int i = 0; i <= SINE_SIZE; ++i) sine_table[i] = sinf(6.28318530718f * i / SINE_SIZE);
        sine_ready = true;
    }
    memset(synth, 0, sizeof(*synth));
    synth->track = 1;
}

static float envelope(const Patch *p, float time) {
    if (time < p->attack) return time / p->attack;
    time -= p->attack;
    if (time < p->decay) return 1 - (1 - p->sustain) * time / p->decay;
    return p->sustain;
}

static void note_on(Synth *synth, const SynthEvent *e, unsigned index) {
    SynthVoice *voice = NULL;
    for (int i = 0; i < SYNTH_VOICES; ++i) if (!synth->voices[i].active) { voice = &synth->voices[i]; break; }
    if (!voice) {
        voice = &synth->voices[0];
        for (int i = 1; i < SYNTH_VOICES; ++i) if (synth->voices[i].age > voice->age) voice = &synth->voices[i];
        ++synth->stolen_voices;
    }
    const Patch *p = &patches[synth->track - 1][e->patch];
    *voice = (SynthVoice){.active=true, .patch=e->patch, .gate=e->duration,
        .release=(uint32_t)(p->release * SYNTH_RATE), .noise=0x9e3779b9u ^ (index * 1664525u + 1),
        .frequency=440 * powf(2, ((int)e->note - 69) / 12.0f),
        .amplitude=p->gain * e->velocity / 127.0f,
        .left=sqrtf((1 - e->pan / 100.0f) * .5f), .right=sqrtf((1 + e->pan / 100.0f) * .5f),
        .release_level=envelope(p, e->duration / (float)SYNTH_RATE)};
}

static float voice_sample(SynthVoice *v, const Patch *p) {
    if (v->age >= v->gate + v->release) { v->active = false; return 0; }
    float time = v->age / (float)SYNTH_RATE;
    float env = v->age < v->gate ? envelope(p, time) : v->release_level * (1 - (v->age - v->gate) / (float)v->release);
    float freq = v->frequency * (1 + (time > .12f ? p->vibrato * sine(time * 5.4f) : 0));
    if (p->kind == KICK) freq = 48 + 140 / (1 + time * time * 18000);
    if (p->kind == TOM) freq *= 1 + .6f / (1 + time * 80);
    float out = 0;
    /* Two substeps tame FM sidebands; pulse additionally uses a band-limited edge. */
    for (int sub = 0; sub < 2; ++sub) {
        float delta = freq / (SYNTH_RATE * 2);
        float phase = v->phase[0];
        float bright = p->index / (1 + time * (p->kind == BELL ? 4 : 8));
        float sample;
        if (p->kind == SAW) {
            sample = 2 * phase - 1 - blep(phase, delta);
        } else if (p->kind == TRIANGLE) {
            sample = 4 * fabsf(phase - .5f) - 1;
        } else if (p->kind == PULSE || p->kind == LAYERED_PULSE) {
            float width = .25f + .06f * sine(time * 1.7f);
            float shifted = phase - width; if (shifted < 0) shifted += 1;
            sample = (phase < width ? 2.0f : 0.0f) - 2 * width + blep(phase, delta) - blep(shifted, delta);
            if (p->kind == LAYERED_PULSE) {
                /* Quiet detuned square adds motion; sine adds body without extra voices. */
                float second = v->phase[3], second_delta = delta * 1.002f;
                float edge = second - .5f; if (edge < 0) edge += 1;
                float square = (second < .5f ? 1.0f : -1.0f)
                    + blep(second, second_delta) - blep(edge, second_delta);
                sample = .65f * sample + .2f * square + .15f * sine(v->phase[1]);
            }
        } else if (p->kind == KICK || p->kind == TOM) {
            sample = sine(phase) + .12f * sine(v->phase[1]) / (1 + time * 500);
        } else if (p->kind == SNARE || p->kind == HAT) {
            v->noise ^= v->noise << 13; v->noise ^= v->noise >> 17; v->noise ^= v->noise << 5;
            float noise = (v->noise & 65535) / 32768.0f - 1;
            if (p->kind == SNARE) sample = .72f * noise + .28f * sine(phase);
            else sample = noise * .65f + .35f * sine(v->phase[2]) * sine(v->phase[3]);
        } else if (p->kind == PAD) {
            sample = .5f * sine(phase + .15f * sine(v->phase[1])) + .5f * sine(v->phase[2] + .12f * sine(v->phase[3]));
        } else {
            float mod = sine(v->phase[1] + .22f * sine(v->phase[2]) / (1 + time * 20));
            sample = .8f * sine(phase + bright * mod / 6.2831853f) + .2f * sine(v->phase[3] + .15f * mod);
        }
        v->phase[0] += delta;
        v->phase[1] += delta * ((p->kind >= KICK && p->kind <= TOM) ? 2.8f : p->ratio);
        v->phase[2] += delta * (p->kind == PAD ? 1.003f : 3.01f);
        v->phase[3] += delta * (p->kind == PAD ? 2.005f : p->kind == HAT ? 4.17f : 1.002f);
        for (int i = 0; i < 4; ++i) v->phase[i] -= floorf(v->phase[i]);
        v->filter += p->brightness * (sample - v->filter);
        out += v->filter * .5f;
    }
    ++v->age;
    return out * env * v->amplitude;
}

void synth_render(Synth *synth, float *stereo, int frames) {
    const SynthTrack *track = &synth_tracks[synth->track - 1];
    for (int i = 0; i < frames; ++i) {
        if (synth->position >= track->frames) synth_select(synth, synth->track);
        while (synth->next_event < track->count && track->events[synth->next_event].frame == synth->position) {
            note_on(synth, &track->events[synth->next_event], synth->next_event);
            ++synth->next_event;
        }
        float left = 0, right = 0;
        for (int v = 0; v < SYNTH_VOICES; ++v) {
            SynthVoice *voice = &synth->voices[v];
            if (!voice->active) continue;
            float value = voice_sample(voice, &patches[synth->track - 1][voice->patch]);
            left += value * voice->left; right += value * voice->right;
        }
        int at = synth->delay_cursor;
        int tap = (at + SYNTH_DELAY - track->delay) % SYNTH_DELAY;
        float dl = synth->delay_left[tap], dr = synth->delay_right[tap];
        synth->delay_left[at] = left * track->wet + dr * track->feedback;
        synth->delay_right[at] = right * track->wet + dl * track->feedback;
        synth->delay_cursor = (at + 1) % SYNTH_DELAY;
        left = (left + dl) * 1.8f; right = (right + dr) * 1.8f;
        float fade = fminf(1, fminf(synth->position / 960.0f, (track->frames - 1 - synth->position) / 960.0f));
        /* Bounded soft saturation plus headroom; do not hard-clip the mix. */
        stereo[i * 2] = .85f * fade * left / (1 + fabsf(left));
        stereo[i * 2 + 1] = .85f * fade * right / (1 + fabsf(right));
        ++synth->position;
    }
}

bool synth_seek(Synth *synth, double progress) {
    if (!isfinite(progress) || progress < 0 || progress > 1) return false;
    if (!synth->track) synth_init(synth);
    const SynthTrack *track = &synth_tracks[synth->track - 1];
    uint32_t target = (uint32_t)(progress * track->frames);
    if (target >= track->frames) target = track->frames - 1;
    synth_select(synth, synth->track);
    float discard[1024 * 2];
    while (synth->position < target) {
        uint32_t remaining = target - synth->position;
        synth_render(synth, discard, remaining < 1024 ? (int)remaining : 1024);
    }
    return true;
}
