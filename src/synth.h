#ifndef GALAXY_SYNTH_H
#define GALAXY_SYNTH_H

#include <stdbool.h>
#include <stdint.h>

#define SYNTH_RATE 48000
#define SYNTH_VOICES 24
#define SYNTH_DELAY 24000
#define SYNTH_TRACK_COUNT 1

typedef struct {
    uint32_t age, gate, release, noise;
    int patch;
    bool active;
    float phase[4], frequency, amplitude, left, right, filter, release_level;
} SynthVoice;

typedef struct {
    uint32_t position, next_event, stolen_voices;
    int delay_cursor;
    SynthVoice voices[SYNTH_VOICES];
    float delay_left[SYNTH_DELAY], delay_right[SYNTH_DELAY];
} Synth;

const char *synth_track_name(int track);
uint32_t synth_track_frames(int track);
void synth_init(Synth *synth);
/* Deterministic preroll restores envelopes, oscillator phase, noise and delay. */
bool synth_seek(Synth *synth, double progress);
/* Interleaved float stereo, sample-clock scheduling; no allocation or audio API. */
void synth_render(Synth *synth, float *stereo, int frames);

#endif
