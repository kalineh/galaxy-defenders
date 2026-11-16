/* Offline evidence for music replacement. No window, audio device or game edits. */
#include "game.h"
#include <math.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

static Game game;

static int stages(void) {
    puts("stage,tick,kind,event,name,world_x,world_y,screen_x,screen_y,music,music_tick,visible_enemies,bullets");
    const char *kinds[] = {"decoration","building","enemy","camera_stop","finish","music","health_bar","secret"};
    for (int s = 0; s < STAGE_COUNT; ++s) {
        game_reset(&game, 1);
        if (!game_stage(&game, s, 0)) return 1;
        game.players[0].invincible = true;
        game.stage.ignore_secrets = true;
        const StageDefinition *d = &stage_definitions[s];
        bool seen[STAGE_MAX_EVENTS] = {0};
        int stop_tick = 0;
        for (int t = 0; t < 18000 && !game.complete; ++t) {
            game_tick(&game);
            int visible = 0;
            for (int i = 0; i < GAME_ENEMIES; ++i) {
                const Enemy *e = &game.enemies[i];
                if (e->active && e->x >= 0 && e->x <= GAME_WIDTH && e->y >= 0 && e->y <= GAME_HEIGHT) ++visible;
            }
            for (int i = 0; i < d->event_count; ++i) {
                if (seen[i] || !game.stage.activated[i]) continue;
                seen[i] = true;
                const StageEvent *e = &d->events[i];
                float x = e->kind == STAGE_ENEMY ? e->spawn_x : e->x;
                float y = e->kind == STAGE_ENEMY ? e->spawn_y : e->y;
                printf("%s,%d,%s,%d,%s,%.3f,%.3f,%.3f,%.3f,%s,%d,%d,%d\n",
                    d->name, game.tick, kinds[e->kind], i, e->name ? e->name : "", x, y,
                    x + GAME_WIDTH / 2, y - game.stage.camera_y + GAME_HEIGHT / 2,
                    game_music(&game), game.tick - game.stage.music_tick, visible, game_bullet_count(&game));
            }
            if (game.tick % GAME_HZ == 0)
                printf("%s,%d,snapshot,-1,,0,%.3f,0,0,%s,%d,%d,%d\n", d->name, game.tick,
                    game.stage.camera_y, game_music(&game), game.tick - game.stage.music_tick, visible, game_bullet_count(&game));
            if (game.stage.stopped && !stop_tick) stop_tick = game.tick;
            if (stop_tick && game.tick >= stop_tick + 12 * GAME_HZ) break;
        }
        for (int i = 0; i < d->event_count; ++i) {
            if (seen[i]) continue;
            const StageEvent *e = &d->events[i];
            printf("%s,-1,unreached_%s,%d,%s,%.3f,%.3f,0,0,,0,0,0\n",
                d->name, kinds[e->kind], i, e->name ? e->name : "", e->x, e->y);
        }
        if (game.dropped_spawns) { fprintf(stderr, "%s dropped spawns: %d\n", d->name, game.dropped_spawns); return 1; }
    }
    return 0;
}

static unsigned read_u(FILE *f, int bytes) {
    unsigned n = 0;
    for (int i = 0; i < bytes; ++i) { int c = fgetc(f); if (c == EOF) return 0; n |= (unsigned)c << (8 * i); }
    return n;
}

static int features(const char *path) {
    FILE *f = fopen(path, "rb");
    if (!f) return 1;
    char id[4];
    if (fread(id, 1, 4, f) != 4 || memcmp(id, "RIFF", 4)) { fclose(f); return 1; }
    read_u(f, 4);
    if (fread(id, 1, 4, f) != 4 || memcmp(id, "WAVE", 4)) { fclose(f); return 1; }
    unsigned format = 0, channels = 0, rate = 0, bits = 0, bytes = 0;
    while (fread(id, 1, 4, f) == 4) {
        unsigned size = read_u(f, 4);
        if (!memcmp(id, "fmt ", 4) && size >= 16) {
            format = read_u(f, 2); channels = read_u(f, 2); rate = read_u(f, 4);
            read_u(f, 4); read_u(f, 2); bits = read_u(f, 2);
            fseek(f, size - 16 + (size & 1), SEEK_CUR);
        } else if (!memcmp(id, "data", 4)) { bytes = size; break; }
        else fseek(f, size + (size & 1), SEEK_CUR);
    }
    if (format != 1 || bits != 16 || channels < 1 || channels > 2 || rate < 8000 || !bytes) { fclose(f); return 1; }
    double low = 0, mid = 0, energy[3] = {0};
    double a = 1 - exp(-6.283185307 * 250 / rate), b = 1 - exp(-6.283185307 * 2500 / rate);
    unsigned frames = bytes / (2 * channels), count = 0, frame = 0;
    puts("time,low,mid,high");
    for (unsigned i = 0; i < frames; ++i) {
        double x = 0;
        for (unsigned c = 0; c < channels; ++c) { unsigned u = read_u(f, 2); x += (u >= 32768 ? (int)u - 65536 : (int)u) / 32768.0 / channels; }
        low += a * (x - low); mid += b * (x - mid);
        energy[0] += low * low; energy[1] += (mid - low) * (mid - low); energy[2] += (x - mid) * (x - mid); ++count;
        if ((unsigned long long)(i + 1) * 60 >= (unsigned long long)(frame + 1) * rate || i + 1 == frames) {
            printf("%.6f,%.7f,%.7f,%.7f\n", frame / 60.0, sqrt(energy[0] / count), sqrt(energy[1] / count), sqrt(energy[2] / count));
            memset(energy, 0, sizeof(energy)); count = 0; ++frame;
        }
    }
    fclose(f);
    return 0;
}

int main(int argc, char **argv) {
    if (argc == 2 && !strcmp(argv[1], "--stages")) return stages();
    if (argc == 3 && !strcmp(argv[1], "--wav")) return features(argv[2]);
    fprintf(stderr, "Usage: music-trace --stages | --wav FILE\n");
    return 1;
}
