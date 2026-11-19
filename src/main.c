#include "game.h"
#include "console.h"
#include "version.h"
#include <errno.h>
#include <math.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#ifndef GALAXY_HEADLESS
int app_run(Game *game, Console *console, int frames, const char *capture, bool mute, float volume, bool show_console);
#endif

static Game game;
static Console console;

static int team_wallet(const Game *g) {
    int total = 0;
    for (int i = 0; i < GAME_PLAYERS; ++i) if (g->players[i].joined) total += g->players[i].money;
    return total;
}

static int playthrough(Game *g) {
    if (!g->ai.campaign || !ai_team(g)) {
        fputs("--playthrough requires --command 'debug.run DIFFICULTY PLAYERS MODE BUILD SEED'\n",stderr);
        return 2;
    }
    puts("seed,difficulty,players,mode,policy,stage,result,ticks,boss_ticks,stars,"
         "enemies,enemy_total,buildings,building_total,coins,coin_total,deaths,credits,"
         "spent_before_stage,wallet_at_start,p1_primary,p1_mark,p2_primary,p2_mark,"
         "most_escaped,escaped_count,p1_generator,p1_shield,p1_support,p1_support_mark,"
         "p2_generator,p2_shield,p2_support,p2_support_mark,boss_hp_left_pct,p1_wallet,p2_wallet,escaped_by_kind");
    int before_shop = team_wallet(g);
    for (int stage=g->stage.index; stage<12; ++stage) {
        int starting_wallet=team_wallet(g), spent=before_shop-starting_wallet;
        int boss_start=-1, boss_end=-1;
        const int limit = 24000;
        while (g->tick < limit && !g->complete && !g->over) {
            game_tick(g);
            if (g->boss_spawned && boss_start<0) boss_start=g->tick;
            if (boss_start>=0 && g->stage.end_countdown && boss_end<0) boss_end=g->tick;
        }
        const StageStats *s=&g->stage.stats;
        const Player *p=g->players;
        int most=0;
        for (int i=1; i<ENEMY_KIND_COUNT; ++i) if (g->escaped[i]>g->escaped[most]) most=i;
        int earned=p[0].score+p[1].score, boss_remaining=-1;
        for (int i=0; i<GAME_ENEMIES; ++i) {
            const Enemy *e=&g->enemies[i];
            if (e->active && enemy_is_boss(e->kind))
                boss_remaining=(int)ceilf(100*e->health/e->health_max);
        }
        const char *result = g->over ? "dead" : g->complete ? "clear" : "timeout";
        printf("%u,%d,%d,%s,%s,%d,%s,%d,%d,%d,", g->seed, g->difficulty, 1+p[1].joined,
               g->ai.mode[0] == AI_AIM ? "aim" : "dodge", g->ai.beam ? "beam" : "starter",
               stage+1, result, g->tick, boss_end >= 0 ? boss_end-boss_start : -1,
               g->complete ? game_stage_stars(g) : 0);
        printf("%d,%d,%d,%d,%d,%d,%d,%d,%d,%d,", s->enemy_kills, s->enemy_total,
               s->building_kills, s->building_total, s->coins_collected, s->coins_total,
               g->deaths, earned, spent, starting_wallet);
        for (int i = 0; i < GAME_PLAYERS; ++i)
            printf("%s,%d,", p[i].joined ? game_weapons[p[i].primary].name : "",
                   p[i].joined ? p[i].weapon_level+1 : 0);
        printf("%s,%d,", g->escaped[most] ? enemy_names[most] : "", g->escaped[most]);
        for (int i = 0; i < GAME_PLAYERS; ++i) {
            bool support = p[i].joined && p[i].secondary >= 0;
            printf("%d,%d,%s,%d,", p[i].joined ? p[i].generator : -1, p[i].joined ? p[i].shield : -1,
                   support ? game_weapons[p[i].secondary].name : "", support ? p[i].secondary_level+1 : 0);
        }
        printf("%d,%d,%d,", boss_remaining, p[0].money, p[1].joined ? p[1].money : 0);
        bool first = true;
        for (int i=0; i<ENEMY_KIND_COUNT; ++i) {
            if (!g->escaped[i]) continue;
            printf("%s%s=%d",first ? "" : "|",enemy_names[i],g->escaped[i]);
            first = false;
        }
        putchar('\n');
        fflush(stdout);
        if (!g->complete) return 0;
        if (stage==11) {
            for (int t=0; t<300 && g->menu.page!=MENU_FINISH; ++t) game_tick(g);
            return g->menu.page == MENU_FINISH ? 0 : 1;
        }
        before_shop=starting_wallet+earned;
        for (int t=0; t<600 && g->stage.index==stage; ++t) game_tick(g);
        if (g->stage.index!=stage+1 || g->menu.page!=MENU_NONE) {
            fputs("AI failed to advance through the shop\n",stderr);
            return 1;
        }
    }
    return 0;
}

int main(int argc, char **argv) {
    bool simulate = false, headless = false, mute = false, show_console = false;
    int frames = 0;
    float volume = -1;
    const char *script = NULL, *commands = NULL, *capture = NULL;
#ifdef GALAXY_HEADLESS
    headless = true;
#endif
    for (int i = 1; i < argc; ++i) {
        if (!strcmp(argv[i], "--version")) {
            puts(GALAXY_VERSION);
            return 0;
        } else if (!strcmp(argv[i], "--help")) {
            puts("Super Galaxy Defenders native port\n"
                 "  --version            print the build version and exit\n"
                 "  --headless           no window/audio; exits after commands\n"
                 "  --playthrough        uncapped AI campaign; print one CSV row per stage\n"
                 "  --script PATH        bounded command file\n"
                 "  --command COMMANDS   semicolon-separated console commands\n"
                 "  --frames N           graphical smoke: exit after N frames\n"
                 "  --capture PATH       save the final graphical frame (requires --frames)\n"
                 "  --mute               disable audio device\n"
                 "  --volume 0..1        master volume (default .3; --frames default .01)\n"
                 "  --console            open the graphical debug console at launch\n"
                 "Use --command help for the shared command list.");
            return 0;
        } else if (!strcmp(argv[i], "--headless")) headless = true;
        else if (!strcmp(argv[i], "--playthrough")) { headless = true; simulate = true; }
        else if (!strcmp(argv[i], "--mute")) mute = true;
        else if (!strcmp(argv[i], "--console")) show_console = true;
        else if (!strcmp(argv[i], "--script") && i + 1 < argc && !script) script = argv[++i];
        else if (!strcmp(argv[i], "--command") && i + 1 < argc && !commands) commands = argv[++i];
        else if (!strcmp(argv[i], "--capture") && i + 1 < argc && !capture) capture = argv[++i];
        else if (!strcmp(argv[i], "--volume") && i + 1 < argc && volume < 0) {
            char *end;
            errno = 0;
            const char *text = argv[++i];
            volume = strtof(text, &end);
            if (errno || end == text || *end || !isfinite(volume) || volume < 0 || volume > 1) {
                fputs("Invalid --volume value (expected 0..1)\n", stderr);
                return 2;
            }
        }
        else if (!strcmp(argv[i], "--frames") && i + 1 < argc && !frames) {
            char *end;
            errno = 0;
            long value = strtol(argv[++i], &end, 10);
            if (errno || *end || value < 1 || value > 36000) { fputs("Invalid --frames value\n", stderr); return 2; }
            frames = (int)value;
        } else { fprintf(stderr, "Unknown or incomplete argument: %s\n", argv[i]); return 2; }
    }
    if ((headless && (frames || capture || show_console)) || (capture && !frames)) {
        fputs("--frames/--capture require a graphical run; capture also requires --frames\n", stderr);
        return 2;
    }
#ifdef GALAXY_MEMORY
    mute = true; /* Software captures never open an audio device. */
    if (!headless && !frames) {
        fputs("Software render checks require --frames N (no interactive window)\n", stderr);
        return 2;
    }
#endif
    game_reset(&game, 1);
    if (!headless && !script && !commands) game_menu_open(&game, MENU_MAIN);
    if (script && !console_script(&console, &game, script)) return 1;
    if (commands && !console.quit && !console_chain(&console, &game, commands)) return 1;
    if (simulate && !console.quit) return playthrough(&game);
    if (headless || console.quit) {
        console_execute(&console, &game, "state");
        return 0;
    }
#ifndef GALAXY_HEADLESS
    if (volume < 0) volume = frames ? 0.01f : 0.3f;
    return app_run(&game, &console, frames, capture, mute, volume, show_console);
#else
    (void)mute;
    return 0;
#endif
}
