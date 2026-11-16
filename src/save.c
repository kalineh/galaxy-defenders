#include "game.h"
#include <stdio.h>
#include <string.h>
#ifdef _WIN32
#define WIN32_LEAN_AND_MEAN
#include <windows.h>
#endif

static bool valid_save(const SavedGame *save) {
    if (!save->valid || save->stage < 0 || save->stage >= 12 || save->difficulty < 0 || save->difficulty > 2 ||
        save->players < 1 || save->players > 2) return false;
    for (int i = 0; i < 12; ++i)
        if (save->stage_scores[i] < -2000000000 || save->stage_scores[i] > 2000000000 ||
            save->stage_medals[i] < 0 || save->stage_medals[i] > 5) return false;
    for (int i = 0; i < save->players; ++i) {
        const SavedPlayer *p = &save->player[i];
        if (p->pilot < 0 || p->pilot > 2 || p->chassis < 0 || p->chassis >= GAME_CHASSIS ||
            p->generator < 0 || p->generator >= GENERATOR_COUNT || p->shield < -1 || p->shield >= SHIELD_COUNT ||
            p->money < 0 || p->money > 1000000000 || equipment_weapon_category(p->primary) != 3 ||
            p->level < 0 || p->level >= game_weapons[p->primary].level_count ||
            (p->secondary != -1 && equipment_weapon_category(p->secondary) != 4) ||
            p->secondary_level < 0 || (p->secondary >= 0 && p->secondary_level >= game_weapons[p->secondary].level_count) ||
            (p->sidekick != -1 && equipment_weapon_category(p->sidekick) != 5)) return false;
    }
    return save->players != 2 || save->player[0].pilot != save->player[1].pilot;
}

bool game_save_read(SavedGame *save, const char *path) {
    FILE *file = fopen(path, "rb");
    if (!file) return false;
    SavedGame candidate = {0};
    int version, shop, cleared;
    bool ok = fscanf(file, "GALAXY_SAVE %d %d %d %d %d %d", &version, &candidate.stage,
        &candidate.difficulty, &candidate.players, &shop, &cleared) == 6 && (version >= 1 && version <= 3) &&
        (shop == 0 || shop == 1) && (cleared == 0 || cleared == 1) && candidate.players >= 1 && candidate.players <= 2;
    candidate.valid = true;
    candidate.shop = ok && shop != 0; candidate.cleared = ok && cleared != 0;
    for (int i = 0; ok && i < candidate.players; ++i) {
        SavedPlayer *p = &candidate.player[i];
        ok = fscanf(file, "%d %d %d %d %d %d %d %d %d %d", &p->pilot, &p->chassis, &p->primary,
            &p->level, &p->secondary, &p->secondary_level, &p->sidekick, &p->generator, &p->shield, &p->money) == 10;
    }
    candidate.seed = 1;
    if (ok && version >= 2) ok = fscanf(file, "%u", &candidate.seed) == 1;
    for (int i = 0; ok && version >= 3 && i < 12; ++i)
        ok = fscanf(file, "%d %d", &candidate.stage_scores[i], &candidate.stage_medals[i]) == 2;
    char extra;
    ok = ok && fscanf(file, " %c", &extra) == EOF && valid_save(&candidate);
    fclose(file);
    if (ok) *save = candidate;
    return ok;
}

bool game_save_write(const SavedGame *save, const char *path) {
    if (!valid_save(save)) return false;
    char temporary[2048];
    int length = snprintf(temporary, sizeof(temporary), "%s.tmp", path);
    if (length < 0 || length >= (int)sizeof(temporary)) return false;
    FILE *file = fopen(temporary, "wb");
    if (!file) return false;
    bool ok = fprintf(file, "GALAXY_SAVE 3 %d %d %d %d %d\n", save->stage, save->difficulty,
        save->players, save->shop, save->cleared) > 0;
    for (int i = 0; i < save->players; ++i) {
        const SavedPlayer *p = &save->player[i];
        ok = fprintf(file, "%d %d %d %d %d %d %d %d %d %d\n", p->pilot, p->chassis, p->primary,
            p->level, p->secondary, p->secondary_level, p->sidekick, p->generator, p->shield, p->money) > 0 && ok;
    }
    ok = fprintf(file, "%u\n", save->seed) > 0 && ok;
    for (int i = 0; i < 12; ++i)
        ok = fprintf(file, "%d %d\n", save->stage_scores[i], save->stage_medals[i]) > 0 && ok;
    if (fclose(file)) ok = false;
    if (!ok) { remove(temporary); return false; }
#ifdef _WIN32
    ok = MoveFileExA(temporary, path, MOVEFILE_REPLACE_EXISTING | MOVEFILE_WRITE_THROUGH) != 0;
#else
    ok = rename(temporary, path) == 0;
#endif
    if (!ok) remove(temporary);
    return ok;
}

void game_reset_session(Game *game, uint32_t seed) {
    SavedGame saves[2];
    memcpy(saves, game->saves, sizeof(saves));
    bool dirty = game->saves_dirty, cleared = game->cleared_game;
    game_reset(game, seed);
    memcpy(game->saves, saves, sizeof(saves));
    game->saves_dirty = dirty;
    game->cleared_game = cleared;
}

void game_record_stage(Game *game) {
    if (game->stage.index >= 12 || game->stage.stats.recorded) return;
    SavedGame *save = &game->saves[game->players[1].joined ? 1 : 0];
    int earned = -game->stage.stats.starting_money;
    for (int i = 0; i < GAME_PLAYERS; ++i)
        if (game->players[i].joined) earned += game->players[i].money + game->players[i].score;
    save->stage_scores[game->stage.index] = earned;
    save->stage_medals[game->stage.index] = game_stage_medal(game);
    game->stage.stats.recorded = true;
}

void game_checkpoint(Game *game, bool shop) {
    if (game->stage.index >= 12) return;
    int players = game->players[1].joined ? 2 : 1;
    SavedGame *save = &game->saves[players - 1];
    save->valid = true; save->shop = shop; save->cleared = game->cleared_game;
    save->stage = shop ? game->menu.next_stage : game->stage.index;
    save->difficulty = game->difficulty; save->players = players; save->seed = game->seed;
    for (int i = 0; i < players; ++i) {
        const Player *p = &game->players[i];
        save->player[i] = (SavedPlayer){p->pilot, p->chassis, p->primary, p->weapon_level, p->secondary,
            p->secondary_level, p->sidekick, p->generator, p->shield, p->money};
    }
    game->saves_dirty = true;
}

bool game_continue(Game *game, int players) {
    if (players < 1 || players > 2 || !valid_save(&game->saves[players - 1])) return false;
    SavedGame save = game->saves[players - 1];
    int options[3];
    memcpy(options, game->menu.options, sizeof(options));
    game_reset_session(game, save.seed);
    memcpy(game->menu.options, options, sizeof(options));
    game->difficulty = save.difficulty;
    game->menu.players = players;
    game->menu.difficulty = save.difficulty;
    game->cleared_game = save.cleared;
    if (players == 2) game_join(game, 1);
    for (int i = 0; i < players; ++i) {
        Player *p = &game->players[i];
        const SavedPlayer *s = &save.player[i];
        p->pilot = s->pilot; p->chassis = s->chassis; p->primary = s->primary; p->weapon_level = s->level;
        p->secondary = s->secondary; p->secondary_level = s->secondary_level; p->sidekick = s->sidekick;
        p->generator = s->generator; p->shield = s->shield; p->money = s->money;
        p->energy = game_generators[p->generator].capacity;
        p->shield_health = p->shield >= 0 ? game_shields[p->shield].capacity : 0;
        p->friction = game_chassis[p->chassis].friction;
    }
    game_stage(game, save.stage, 0);
    if (save.shop) {
        game->stage.stats.recorded = true;
        game_shop_open(game);
    }
    return true;
}
