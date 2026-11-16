#include "synth.h"
#include <math.h>
#include <stdio.h>
#include <stdlib.h>

static Synth synth;

static void write_u(FILE *f, unsigned value, int bytes) {
    for (int i = 0; i < bytes; ++i) fputc((value >> (i * 8)) & 255, f);
}

int main(int argc, char **argv) {
    if (argc != 2) { fprintf(stderr, "Usage: render-music OUTPUT.wav\n"); return 1; }
    FILE *f = fopen(argv[1], "wb");
    if (!f) return 1;
    unsigned frames = synth_track_frames(1), bytes = frames * 4;
    fwrite("RIFF", 1, 4, f); write_u(f, 36 + bytes, 4); fwrite("WAVEfmt ", 1, 8, f);
    write_u(f, 16, 4); write_u(f, 1, 2); write_u(f, 2, 2); write_u(f, SYNTH_RATE, 4);
    write_u(f, SYNTH_RATE * 4, 4); write_u(f, 4, 2); write_u(f, 16, 2); fwrite("data", 1, 4, f); write_u(f, bytes, 4);
    synth_init(&synth);
    float buffer[2048 * 2], peak = 0;
    double energy = 0;
    for (unsigned at = 0; at < frames;) {
        int count = frames - at < 2048 ? (int)(frames - at) : 2048;
        synth_render(&synth, buffer, count);
        for (int i = 0; i < count * 2; ++i) {
            if (!isfinite(buffer[i]) || fabsf(buffer[i]) >= 1) { fclose(f); return 1; }
            if (fabsf(buffer[i]) > peak) peak = fabsf(buffer[i]);
            energy += buffer[i] * buffer[i];
            write_u(f, (unsigned)(int)(buffer[i] * 32767), 2);
        }
        at += count;
    }
    int failed = ferror(f);
    if (fclose(f)) failed = 1;
    printf("Rendered %.6fs stereo; peak=%.4f RMS=%.4f stolen_voices=%u\n", frames / (double)SYNTH_RATE,
        peak, sqrt(energy / (frames * 2)), synth.stolen_voices);
    return failed || synth.stolen_voices || peak < .01f;
}
