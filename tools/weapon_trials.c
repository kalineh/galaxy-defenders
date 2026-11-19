#include "game.h"
#include <errno.h>
#include <limits.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

static Game game, range;

/* Isolate the featured mount on a stationary, 96-unit-wide target. Keep the
   purchased generator and actual charge, overheat, collision and reload rules. */
static float target_dps(const Player *kit, int category, int dx, int dy, bool focus, int seed) {
    game_reset(&range,seed);
    range.waves = false;
    Player *p = &range.players[0];
    *p = *kit;
    if (category != 3) p->primary = -1;
    if (category != 4) p->secondary = -1;
    if (category != 5) p->sidekick = -1;
    Enemy *target = &range.enemies[game_spawn_enemy(&range,ENEMY_BALL,480+dx,600-dy)];
    target->radius = 48;
    target->health_max = 1000;
    float damage = 0;
    for (int t = 0; t < 23*GAME_HZ; ++t) {
        p->x = 480;
        p->y = 600;
        target->x = p->x+dx;
        target->y = p->y-dy;
        p->invulnerable = 2; /* Exclude ship impact damage at melee range. */
        p->vx = p->vy = target->vx = target->vy = 0;
        target->health = target->health_max;
        bool held = game_auto_hold_sidekick(p);
        p->input = (PlayerInput){.fire=true,.focus=focus,.sidekick_left=held,.sidekick_right=held};
        game_tick(&range);
        if (t >= 3*GAME_HZ) damage += target->health_max-target->health;
    }
    return damage*100/20; /* Shop damage units per second. */
}

static void trial(int stage, int seed, int difficulty, int weapon) {
    game_reset(&game,seed);
    game.difficulty = difficulty;
    game_starting_loadout(&game,0);
    Player starting = game.players[0];
    int budget = game_debug_loadout(&game,stage,weapon,false);
    int cat = equipment_weapon_category(weapon);
    Player *p = &game.players[0];
    int level = game_shop_equipped_level(p,cat);
    printf("%d,%d,%d,%s,%d,%d,",stage,seed,difficulty,game_weapons[weapon].name,cat,budget);
    if (game_shop_equipped(p,cat) != weapon) { puts("unaffordable,,,,,,,,,,,"); return; }
    float near = target_dps(p,cat,40,40,false,seed);
    float far = target_dps(p,cat,0,265,false,seed);
    float focus = cat == 3 ? target_dps(p,cat,0,265,true,seed) : 0;
    game_stage(&game,stage-1,0);
    ai_set(&game,0,AI_AIM);
    game.stage.ignore_secrets = true;
    int boss_start = -1, boss_end = -1;
    while (game.tick < 24000 && !game.complete && !game.over) {
        game_tick(&game);
        if (game.boss_spawned && boss_start < 0) boss_start = game.tick;
        if (boss_start >= 0 && game.stage.end_countdown && boss_end < 0) boss_end = game.tick;
    }
    const StageStats *stats = &game.stage.stats;
    printf("%s,%d,%d,%.1f,%.1f,%.1f,%d,%d,%d,%d,%d,%d\n",
        game.complete ? "clear" : "timeout",level+1,game_shop_price(&starting,cat,weapon,level),
        near,far,focus,stats->enemy_kills,stats->enemy_total,stats->building_kills,stats->building_total,
        boss_end >= 0 ? boss_end-boss_start : -1,game.weapon_shots[weapon]);
}

static int argument(const char *s, int low, int high) {
    char *end;
    errno = 0;
    long n = strtol(s,&end,10);
    return errno || end == s || *end || n < low || n > high ? -1 : (int)n;
}

static void mark_trials(int seed) {
    puts("weapon,mark,total_price,near_dps,far_dps,overcharge_far_dps");
    game_reset(&game,seed);
    game_starting_loadout(&game,0);
    Player kit = game.players[0];
    kit.generator = GENERATOR_COUNT-1;
    kit.energy = game_generators[kit.generator].capacity;
    for (int w = 0; w < WEAPON_COUNT; ++w) {
        int cat = equipment_weapon_category(w);
        if (cat < 3) continue;
        for (int level = 0; level < game_weapons[w].level_count; ++level) {
            if (cat == 3) { kit.primary = w; kit.weapon_level = level; }
            if (cat == 4) { kit.secondary = w; kit.secondary_level = level; }
            if (cat == 5) { kit.sidekick = w; kit.sidekick_level = level; }
            printf("%s,%d,%d,%.1f,%.1f,%.1f\n",game_weapons[w].name,level+1,equipment_weapon_price(w,level),
                target_dps(&kit,cat,40,40,false,seed),target_dps(&kit,cat,0,265,false,seed),
                cat == 3 ? target_dps(&kit,cat,0,265,true,seed) : 0.0f);
        }
    }
}

int main(int argc, char **argv) {
    int stage = argc > 1 ? argument(argv[1],1,12) : -1;
    int seed = argc > 2 ? argument(argv[2],0,INT_MAX) : 42;
    int difficulty = argc > 3 ? argument(argv[3],0,2) : 0;
    if (argc >= 2 && argc <= 3 && !strcmp(argv[1],"marks") && seed >= 0) {
        mark_trials(seed);
        return 0;
    }
    if (argc > 4 || stage < 0 || seed < 0 || difficulty < 0) {
        fputs("Usage: weapon-trials STAGE [SEED [DIFFICULTY]] or weapon-trials marks [SEED]\n",stderr);
        return 2;
    }
    puts("stage,seed,difficulty,weapon,category,budget,result,mark,weapon_spend,near_dps,far_dps,overcharge_far_dps,enemies,enemy_total,buildings,building_total,boss_ticks,normal_shots");
    for (int w = 0; w < WEAPON_COUNT; ++w)
        if (equipment_weapon_category(w) >= 3) trial(stage,seed,difficulty,w);
    return 0;
}
