#include "game.h"
#include <stdio.h>
#include <string.h>
#include <errno.h>
#include <limits.h>
#include <stdlib.h>
#ifdef _WIN32
#define WIN32_LEAN_AND_MEAN
#include <windows.h>
#include <io.h>
#endif

static bool valid_save(const SavedGame *save) {
    if (!save->valid || save->stage < 0 || save->stage >= 12 || save->difficulty < 0 || save->difficulty > 2 ||
        save->players < 1 || save->players > 2) return false;
    for (int i = 0; i < 12; ++i)
        if (save->stage_scores[i] < -2000000000 || save->stage_scores[i] > 2000000000 ||
            save->stage_medals[i] < 0 || save->stage_medals[i] > 5) return false;
    for (int i = 0; i < save->players; ++i) {
        const SavedPlayer *p = &save->player[i];
        const int counts[] = {GAME_CHASSIS, GENERATOR_COUNT, SHIELD_COUNT};
        for (int c = 0; c < 3; ++c)
            if ((unsigned)p->inventory.gear_mask[c] >= (1u << counts[c])) return false;
        for (int w = 0; w < WEAPON_COUNT; ++w)
            if (p->inventory.weapon_marks[w] > game_weapons[w].level_count ||
                (p->inventory.weapon_marks[w] && equipment_weapon_category(w) < 0)) return false;
        if (p->pilot < 0 || p->pilot > 2 || p->chassis < 0 || p->chassis >= GAME_CHASSIS ||
            p->generator < 0 || p->generator >= GENERATOR_COUNT || p->shield < -1 || p->shield >= SHIELD_COUNT ||
            p->money < 0 || p->money > 1000000000 || equipment_weapon_category(p->primary) != 3 ||
            p->level < 0 || p->level >= game_weapons[p->primary].level_count ||
            (p->secondary != -1 && equipment_weapon_category(p->secondary) != 4) ||
            p->secondary_level < 0 || (p->secondary >= 0 && p->secondary_level >= game_weapons[p->secondary].level_count) ||
            (p->sidekick != -1 && equipment_weapon_category(p->sidekick) != 5) ||
            p->sidekick_level < 0 || p->sidekick_level >= (p->sidekick < 0 ? 1 : game_weapons[p->sidekick].level_count)) return false;
    }
    return save->players != 2 || save->player[0].pilot != save->player[1].pilot;
}

static bool read_number(FILE *file, long long minimum, long long maximum, long long *value) {
    char token[64], *end;
    if (fscanf(file, "%63s", token) != 1) return false;
    errno = 0;
    long long parsed = strtoll(token, &end, 10);
    if (errno || end == token || *end || parsed < minimum || parsed > maximum) return false;
    *value = parsed;
    return true;
}

static bool read_int(FILE *file, int *value) {
    long long parsed;
    if (!read_number(file, INT_MIN, INT_MAX, &parsed)) return false;
    *value = (int)parsed;
    return true;
}

bool game_options_read(int options[OPTION_COUNT], const char *path) {
    FILE *file = fopen(path,"rb");
    if (!file) return false;
    int values[OPTION_COUNT] = {[OPTION_MSAA] = 1};
    bool ok = true;
    for (int i = 0; i < OPTION_MSAA && ok; ++i)
        ok = read_int(file,&values[i]) && values[i] >= 0 && values[i] <= game_option_max[i];
    char extra;
    for (int i = OPTION_MSAA; ok && i < OPTION_COUNT; ++i) {
        if (fscanf(file," %c",&extra) != 1) break;
        ungetc(extra,file);
        ok = read_int(file,&values[i]) && values[i] >= 0 && values[i] <= game_option_max[i];
    }
    ok = ok && fscanf(file," %c",&extra) == EOF && !ferror(file);
    fclose(file);
    if (ok) memcpy(options,values,sizeof(values));
    return ok;
}

bool game_options_write(const int options[OPTION_COUNT], const char *path) {
    char temporary[2048];
    int length = snprintf(temporary,sizeof(temporary),"%s.tmp",path);
    if (length < 0 || length >= (int)sizeof(temporary)) return false;
    FILE *file = fopen(temporary,"wb");
    if (!file) return false;
    bool ok = true;
    for (int i = 0; i < OPTION_COUNT; ++i)
        if (fprintf(file,"%d\n",options[i]) < 0) ok = false;
    if (fclose(file)) ok = false;
#ifdef _WIN32
    if (ok) ok = MoveFileExA(temporary,path,MOVEFILE_REPLACE_EXISTING | MOVEFILE_WRITE_THROUGH) != 0;
#else
    if (ok) ok = rename(temporary,path) == 0;
#endif
    if (!ok) remove(temporary);
    return ok;
}

static bool read_save(SavedGame *save, const char *path) {
    FILE *file = fopen(path, "rb");
    if (!file) return false;
    SavedGame candidate = {0};
    int version = 0, shop = 0, cleared = 0;
    char magic[32];
    bool ok = fscanf(file, "%31s", magic) == 1 && !strcmp(magic, "GALAXY_SAVE") &&
        read_int(file, &version) && read_int(file, &candidate.stage) && read_int(file, &candidate.difficulty) &&
        read_int(file, &candidate.players) && read_int(file, &shop) && read_int(file, &cleared) &&
        version >= 1 && version <= 5 && (shop == 0 || shop == 1) && (cleared == 0 || cleared == 1) &&
        candidate.players >= 1 && candidate.players <= 2;
    candidate.valid = true;
    candidate.shop = shop != 0; candidate.cleared = cleared != 0;
    for (int i = 0; ok && i < candidate.players; ++i) {
        SavedPlayer *p = &candidate.player[i];
        int *fields[] = {&p->pilot, &p->chassis, &p->primary, &p->level, &p->secondary,
            &p->secondary_level, &p->sidekick, &p->generator, &p->shield, &p->money};
        for (int j = 0; ok && j < 10; ++j) ok = read_int(file, fields[j]);
        if (ok && version >= 5) ok = read_int(file, &p->sidekick_level);
    }
    candidate.seed = 1;
    if (ok && version >= 2) {
        long long seed;
        ok = read_number(file, 0, UINT32_MAX, &seed);
        if (ok) candidate.seed = (uint32_t)seed;
    }
    for (int i = 0; ok && version >= 3 && i < 12; ++i)
        ok = read_int(file, &candidate.stage_scores[i]) && read_int(file, &candidate.stage_medals[i]);
    for (int i = 0; ok && version >= 4 && i < candidate.players; ++i) {
        Inventory *inv = &candidate.player[i].inventory;
        for (int c = 0; ok && c < 3; ++c) ok = read_int(file, &inv->gear_mask[c]);
        for (int w = 0; ok && w < WEAPON_COUNT; ++w) {
            long long marks;
            ok = read_number(file, 0, game_weapons[w].level_count, &marks);
            if (ok) inv->weapon_marks[w] = (unsigned char)marks;
        }
    }
    if (ok && version < 5) {
        const int kinds[] = {WEAPON_BLADE, WEAPON_MISSILE_VOLLEY};
        const int marks[] = {2, 4};
        for (int i = 0; i < candidate.players; ++i) {
            SavedPlayer *p = &candidate.player[i];
            for (int j = 0; j < 2; ++j) {
                int kind = kinds[j];
                if (p->sidekick == kind) p->sidekick_level = marks[j] - 1;
                if (p->sidekick == kind || p->inventory.weapon_marks[kind])
                    p->inventory.weapon_marks[kind] = marks[j];
            }
        }
    }
    char extra;
    ok = ok && fscanf(file, " %c", &extra) == EOF && valid_save(&candidate);
    fclose(file);
    if (ok) *save = candidate;
    return ok;
}

bool game_save_read(SavedGame *save, const char *path) {
    if (read_save(save, path)) return true;
    char backup[2048];
    int length = snprintf(backup, sizeof(backup), "%s.bak", path);
    return length > 0 && length < (int)sizeof(backup) && read_save(save, backup);
}

bool game_save_delete(const char *path) {
    char backup[2048];
    int length = snprintf(backup, sizeof(backup), "%s.bak", path);
    if (length < 0 || length >= (int)sizeof(backup)) return false;
    if (remove(backup) != 0 && errno != ENOENT) return false;
    return remove(path) == 0 || errno == ENOENT;
}

bool game_save_write(const SavedGame *save, const char *path) {
    if (!valid_save(save)) return false;
    char temporary[2048];
    int length = snprintf(temporary, sizeof(temporary), "%s.tmp", path);
    if (length < 0 || length >= (int)sizeof(temporary)) return false;
    FILE *file = fopen(temporary, "wb");
    if (!file) return false;
    bool ok = fprintf(file, "GALAXY_SAVE 5 %d %d %d %d %d\n", save->stage, save->difficulty,
        save->players, save->shop, save->cleared) > 0;
    for (int i = 0; i < save->players; ++i) {
        const SavedPlayer *p = &save->player[i];
        ok = fprintf(file, "%d %d %d %d %d %d %d %d %d %d %d\n", p->pilot, p->chassis, p->primary,
            p->level, p->secondary, p->secondary_level, p->sidekick, p->generator, p->shield, p->money, p->sidekick_level) > 0 && ok;
    }
    ok = fprintf(file, "%u\n", save->seed) > 0 && ok;
    for (int i = 0; i < 12; ++i)
        ok = fprintf(file, "%d %d\n", save->stage_scores[i], save->stage_medals[i]) > 0 && ok;
    for (int i = 0; i < save->players; ++i) {
        const Inventory *inv = &save->player[i].inventory;
        for (int c = 0; c < 3; ++c) ok = fprintf(file, "%d ", inv->gear_mask[c]) > 0 && ok;
        for (int w = 0; w < WEAPON_COUNT; ++w) ok = fprintf(file, "%u ", (unsigned)inv->weapon_marks[w]) > 0 && ok;
        ok = fputc('\n', file) != EOF && ok;
    }
    if (fflush(file)) ok = false;
#ifdef _WIN32
    if (ok && _commit(_fileno(file))) ok = false;
#endif
    if (fclose(file)) ok = false;
    if (!ok) { remove(temporary); return false; }
#ifdef _WIN32
    char backup[2048];
    SavedGame previous;
    length = snprintf(backup, sizeof(backup), "%s.bak", path);
    if (length < 0 || length >= (int)sizeof(backup)) { remove(temporary); return false; }
    /* Never replace the recovery copy with a corrupt primary. */
    if (read_save(&previous, path) && !CopyFileA(path, backup, FALSE)) { remove(temporary); return false; }
    ok = MoveFileExA(temporary, path, MOVEFILE_REPLACE_EXISTING | MOVEFILE_WRITE_THROUGH) != 0;
#else
    ok = rename(temporary, path) == 0;
#endif
    if (!ok) remove(temporary);
    return ok;
}

void game_reset_session(Game *game, uint32_t seed) {
    SavedGame saves[GAME_SAVE_SLOTS];
    memcpy(saves, game->saves, sizeof(saves));
    unsigned dirty = game->saves_dirty;
    bool cleared = game->cleared_game;
    int active_slot = game->active_slot;
    bool errors[GAME_SAVE_SLOTS];
    memcpy(errors, game->save_errors, sizeof(errors));
    game_reset(game, seed);
    memcpy(game->saves, saves, sizeof(saves));
    game->saves_dirty = dirty;
    game->active_slot = active_slot;
    memcpy(game->save_errors, errors, sizeof(errors));
    game->cleared_game = cleared;
}

void game_record_stage(Game *game) {
    if (game->active_slot < 0 || game->active_slot >= GAME_SAVE_SLOTS || game->stage.index >= 12 || game->stage.stats.recorded) return;
    SavedGame *save = &game->saves[game->active_slot];
    int earned = -game->stage.stats.starting_money;
    for (int i = 0; i < GAME_PLAYERS; ++i)
        if (game->players[i].initialized) earned += game->players[i].money + game->players[i].score;
    save->stage_scores[game->stage.index] = earned;
    save->stage_medals[game->stage.index] = game_stage_medal(game);
    game->stage.stats.recorded = true;
}

void game_checkpoint(Game *game, bool shop) {
    if (game->active_slot < 0 || game->active_slot >= GAME_SAVE_SLOTS || game->stage.index >= 12) return;
    int players = game->players[1].initialized ? 2 : 1;
    SavedGame *save = &game->saves[game->active_slot];
    save->valid = true; save->shop = shop; save->cleared = game->cleared_game;
    save->stage = shop ? game->menu.next_stage : game->stage.index;
    save->difficulty = game->difficulty; save->players = players; save->seed = game->seed;
    for (int i = 0; i < players; ++i) {
        const Player *p = &game->players[i];
        save->player[i] = (SavedPlayer){p->pilot, p->chassis, p->primary, p->weapon_level, p->secondary,
            p->secondary_level, p->sidekick, p->sidekick_level, p->generator, p->shield,
            p->money + (shop && game->menu.page == MENU_CLEAR ? p->score : 0), p->inventory};
    }
    game->saves_dirty |= 1u << game->active_slot;
}

bool game_continue(Game *game, int slot) {
    if (slot < 0 || slot >= GAME_SAVE_SLOTS || !valid_save(&game->saves[slot])) return false;
    SavedGame save = game->saves[slot];
    int players = save.players;
    bool joined[GAME_PLAYERS] = {game->players[0].joined, game->players[1].joined};
    game->active_slot = slot;
    int options[OPTION_COUNT];
    memcpy(options, game->menu.options, sizeof(options));
    game_reset_session(game, save.seed);
    memcpy(game->menu.options, options, sizeof(options));
    game->difficulty = save.difficulty;
    game->menu.players = players;
    game->menu.difficulty = save.difficulty;
    game->cleared_game = save.cleared;
    if (players == 2 || joined[1]) game_join(game, 1);
    for (int i = 0; i < players; ++i) {
        Player *p = &game->players[i];
        const SavedPlayer *s = &save.player[i];
        p->pilot = s->pilot; p->chassis = s->chassis; p->primary = s->primary; p->weapon_level = s->level;
        p->secondary = s->secondary; p->secondary_level = s->secondary_level; p->sidekick = s->sidekick; p->sidekick_level = s->sidekick_level;
        p->generator = s->generator; p->shield = s->shield; p->money = s->money; p->inventory = s->inventory;
        p->energy = game_generators[p->generator].capacity;
        p->shield_health = p->shield >= 0 ? game_shields[p->shield].capacity : 0;
        p->friction = game_chassis[p->chassis].friction;
        p->health = game_chassis[p->chassis].armor;
        p->lives = 1;
    }
    for (int i = 0; i < GAME_PLAYERS; ++i) game->players[i].joined = joined[i];
    if (game->players[1].initialized && game->players[0].pilot == game->players[1].pilot)
        game->players[1].pilot = (game->players[0].pilot + 1) % 3;
    game->menu.players = joined[1] ? 2 : 1;
    game_stage(game, save.stage, 0);
    game->stage.stats.recorded = true;
    game_shop_open(game);
    if (!joined[0] && !joined[1]) game_menu_open(game,MENU_CONTROLLERS);
    return true;
}
