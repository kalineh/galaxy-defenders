#include "synth.h"
#include <math.h>
#include <stdio.h>
#include <string.h>

static Synth sequential, sought;
static float reference[3][2048], block[2048];
static int failures;
#define CHECK(x) do { if (!(x)) { fprintf(stderr, "synth:%d: %s\n", __LINE__, #x); ++failures; } } while (0)

int main(void) {
    uint32_t length = synth_track_frames(1);
    CHECK(length > SYNTH_RATE * 120 && length < SYNTH_RATE * 121);
    CHECK(!synth_track_name(0) && synth_track_frames(20) == 0);
    synth_init(&sequential);
    double energy = 0, dc = 0, stereo = 0;
    float peak = 0;
    uint32_t targets[] = {length / 4, length / 2, length * 3 / 4};
    int target = 0;
    while (sequential.position < length) {
        uint32_t start = sequential.position;
        int count = length - start < 1024 ? (int)(length - start) : 1024;
        if (target < 3 && start < targets[target] && start + count > targets[target]) count = targets[target] - start;
        synth_render(&sequential, block, count);
        if (target < 3 && start == targets[target]) {
            CHECK(count == 1024);
            memcpy(reference[target++], block, sizeof(block));
        }
        for (int i = 0; i < count * 2; ++i) {
            CHECK(isfinite(block[i]) && fabsf(block[i]) < .85f);
            if (fabsf(block[i]) > peak) peak = fabsf(block[i]);
            energy += block[i] * block[i]; dc += block[i];
        }
        for (int i = 0; i < count; ++i) stereo += fabsf(block[i * 2] - block[i * 2 + 1]);
    }
    CHECK(target == 3 && sequential.stolen_voices == 0);
    CHECK(peak > .1f && energy / (length * 2) > .0001);
    CHECK(fabs(dc / (length * 2)) < .005 && stereo / length > .001);
    for (int i = 0; i < 3; ++i) {
        CHECK(synth_seek(&sought, (targets[i] + .01) / length));
        CHECK(sought.position == targets[i]);
        /* Different block boundaries must not change phase, noise or delay. */
        synth_render(&sought, block, 17);
        synth_render(&sought, block + 34, 1007);
        CHECK(!memcmp(block, reference[i], sizeof(block)));
    }
    uint32_t before = sought.position;
    CHECK(!synth_seek(&sought, NAN) && !synth_seek(&sought, -1) && !synth_seek(&sought, 1.1));
    CHECK(sought.position == before);
    CHECK(synth_seek(&sought, 1) && sought.position == length - 1);
    synth_render(&sought, block, 2);
    CHECK(block[0] == 0 && block[1] == 0 && block[2] == 0 && block[3] == 0);
    CHECK(sought.position == 1);
    CHECK(synth_seek(&sought, 0) && sought.position == 0);
    /* Selecting/seeking must retain each song's own patch bank and delay. */
    for (int track = 1; track <= SYNTH_TRACK_COUNT; ++track) {
        CHECK(synth_select(&sequential, track));
        for (int i = 0; i < 8; ++i) synth_render(&sequential, block, 1024);
        synth_render(&sequential, reference[0], 1024);
        CHECK(synth_select(&sought, track));
        CHECK(synth_seek(&sought, 8192.01 / synth_track_frames(track)));
        CHECK(sought.track == track && sought.position == 8192);
        synth_render(&sought, block, 1024);
        CHECK(!memcmp(block, reference[0], sizeof(block)));
    }
    printf("Synth checks: %s; peak %.3f, RMS %.3f; deterministic seek/chunks, loop and voice capacity\n",
        failures ? "FAIL" : "PASS", peak, sqrt(energy / (length * 2)));
    return failures ? 1 : 0;
}
