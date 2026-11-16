#include "console.h"
#include "synth.h"
#include <ctype.h>
#include <errno.h>
#include <math.h>
#include <stdarg.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

typedef struct {
    const char *name, *usage, *description;
    const char *options[5];
} CommandHelp;

/* One catalog supplies help, usage errors and positional Tab completion. */
static const CommandHelp commands[] = {
    {"debug.music", "debug.music [TRACK [PROGRESS]|off]", "Audition synth independently of stage/menu; TRACK 1, PROGRESS 0..1. Off restores normal music. No arguments lists tracks/status.", {"1 off", "0 0.25 0.5 0.75 1"}},
    {"game.stage", "game.stage STAGE [PROGRESS]", "Seek main 1..12, bonus 13..19, or a stage name; PROGRESS 0..1 is nominal stage time. Preserves loadout and money, restores armor, synchronizes music.", {"1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16 17 18 19", "0 0.25 0.5 0.75 0.9 1"}},
    {"help", "help [COMMAND|FILTER|fields]", "List all commands, filter by name, or show command usage / assertion fields.", {NULL}},
    {"clear", "clear", "Clear console output; retain command history.", {NULL}},
    {"perf", "perf [on|off]", "Toggle the F3 top-right frame/work time graph. F2 remains the co-op join key.", {"on off"}},
    {"state", "state", "Show menu, difficulty, combat counters and each player's equipment/state.", {NULL}},
    {"quit", "quit", "Exit the game or stop the current script.", {NULL}},
    {"game.reset", "game.reset SEED", "Reset Stage 1 and debug loadout. SEED is an integer from 0 to 2147483647.", {NULL}},
    {"game.waves", "game.waves 0|1", "Disable/enable authored stage events and camera scrolling.", {"0 1"}},
    {"game.pause", "game.pause 0|1", "Resume simulation or open the pause menu.", {"0 1"}},
    {"game.step", "game.step TICKS", "Advance 1..36000 fixed ticks; maximum 72000 requested ticks per process.", {NULL}},
    {"player.join", "player.join SLOT", "Join player 1 or 2; fails if already joined or combat has ended.", {"1 2"}},
    {"player.input", "player.input SLOT X Y FIRE FOCUS", "Hold input: SLOT 1/2, axes -1..1, FIRE/FOCUS 0/1. Focus fires independently.", {"1 2", "-1 0 1", "-1 0 1", "0 1", "0 1"}},
    {"player.money", "player.money SLOT AMOUNT", "Set money (not add): integer 0..1000000000. Does not purchase equipment.", {"1 2"}},
    {"player.invincible", "player.invincible SLOT 0|1", "Toggle damage immunity for a joined player.", {"1 2", "0 1"}},
    {"player.heal", "player.heal SLOT", "Restore a living player's armor to the equipped chassis maximum.", {"1 2"}},
    {"player.chassis", "player.chassis SLOT NAME", "Equip an original chassis free; preserve current armor percentage. See chassis.list.", {"1 2"}},
    {"player.upgrade", "player.upgrade SLOT LEVEL", "Set the equipped primary weapon level free (zero-based 0..7).", {"1 2", "0 1 2 3 4 5 6 7"}},
    {"equipment.list", "equipment.list", "List original weapons, generators and shields with IDs.", {0}},
    {"player.weapon", "player.weapon SLOT primary|secondary|sidekick NAME LEVEL", "Equip a weapon free; use None for empty support/sidekick slots.", {"1 2", "primary secondary sidekick"}},
    {"player.generator", "player.generator SLOT ID", "Equip a generator free (0..11).", {"1 2"}},
    {"player.shield", "player.shield SLOT ID", "Equip a shield free (0..8).", {"1 2"}},
    {"chassis.list", "chassis.list", "List every supported chassis, its ID, armor, movement values and original price.", {NULL}},
    {"enemy.spawn", "enemy.spawn KIND X Y", "Spawn an original enemy. X -200..1160, Y -250..1280; fails when the pool is full.", {"ball triangle turret boss MiniSquare BigBall DownTurret"}},
    {"menu.open", "menu.open main|pause|shop", "Open the title or pause menu and clear held player input.", {"main pause shop"}},
    {"menu.input", "menu.input ACTION [SLOT]", "Navigate the real menu state; SLOT defaults to 1. Player 2 can select its pilot.", {"up down left right confirm back ready", "1 2"}},
    {"assert", "assert FIELD OP VALUE", "Check a numeric state field; failure stops a script. Use help fields for all fields.", {NULL, "eq ne lt le gt ge"}}
};
static const char *global_fields[] = {"tick", "enemies", "bullets", "kills", "shots", "hits", "dropped",
    "over", "complete", "menu", "menu.cursor", "difficulty", "volume.sfx", "volume.music", "stage", "stage.camera", "stage.stopped", "stage.music_time"};
static const char *player_fields[] = {"x", "y", "health", "lives", "score", "joined", "money", "chassis",
    "armor", "invincible", "pilot", "weapon_level", "energy"};

static const CommandHelp *command_help(const char *name) {
    for (size_t i = 0; i < sizeof(commands) / sizeof(commands[0]); ++i)
        if (!strcmp(name, commands[i].name)) return &commands[i];
    return NULL;
}

static bool print_help(Console *console, const char *filter) {
    if (filter && !strcmp(filter, "fields")) {
        console_log(console, "Global fields:");
        for (size_t i = 0; i < sizeof(global_fields) / sizeof(global_fields[0]); ++i) console_log(console, "  %s", global_fields[i]);
        console_log(console, "Player fields (both slots):");
        for (size_t i = 0; i < sizeof(player_fields) / sizeof(player_fields[0]); ++i) console_log(console, "  p1.%s  p2.%s", player_fields[i], player_fields[i]);
        console_log(console, "IDs: chassis.list; pilots 0=Kazuki 1=Rabbit 2=Gunthor; difficulty 0=Normal 1=Hard 2=Really Hard.");
        console_log(console, "Menu IDs: 0=play 1=main 2=new 3=difficulty 4=pilot 5=options 6=quit 7=pause 8=retry 9=game-over 10=clear 11=shop 12=ending 13=stage-select.");
        return true;
    }
    const CommandHelp *exact = filter ? command_help(filter) : NULL;
    int found = 0;
    for (size_t i = 0; i < sizeof(commands) / sizeof(commands[0]); ++i) {
        const CommandHelp *entry = &commands[i];
        if ((exact && entry != exact) || (!exact && filter && !strstr(entry->name, filter))) continue;
        console_log(console, "%s", entry->usage);
        console_log(console, "  %s", entry->description);
        ++found;
    }
    if (!found) console_log(console, "ERROR: no commands match '%s'", filter);
    else if (!filter) console_log(console, "Tab completes commands/arguments; Tab again lists choices. PgUp/PgDn or wheel scroll; Up/Down recall commands.");
    return found != 0;
}

void console_insert(Console *console, const char *text) {
    int length = (int)strlen(console->input);
    for (; *text; ++text) {
        if ((unsigned char)*text < 32 || (unsigned char)*text > 126 || length == CONSOLE_LINE - 1) continue;
        memmove(console->input + console->cursor + 1, console->input + console->cursor, (size_t)(length - console->cursor + 1));
        console->input[console->cursor++] = *text;
        ++length;
    }
    console->completion[0] = '\0';
}

void console_erase(Console *console, bool backward) {
    int length = (int)strlen(console->input);
    if ((backward && console->cursor == 0) || (!backward && console->cursor == length)) return;
    if (backward) --console->cursor;
    memmove(console->input + console->cursor, console->input + console->cursor + 1, (size_t)(length - console->cursor));
    console->completion[0] = '\0';
}

void console_history(Console *console, int direction) {
    if (direction < 0 && console->history_offset < console->command_count) {
        if (!console->history_offset) strcpy(console->draft, console->input);
        ++console->history_offset;
    } else if (direction > 0 && console->history_offset > 0) --console->history_offset;
    else return;
    strcpy(console->input, console->history_offset ? console->commands[console->command_count - console->history_offset] : console->draft);
    console->cursor = (int)strlen(console->input);
    console->completion[0] = '\0';
}

static void add_match(char matches[][64], int *count, const char *candidate, const char *prefix) {
    if (*count < 128 && !strncmp(candidate, prefix, strlen(prefix))) {
        snprintf(matches[(*count)++], 64, "%s", candidate);
    }
}

int console_complete(Console *console) {
    char matches[128][64], prefix[CONSOLE_LINE], command[CONSOLE_LINE];
    int start = console->cursor, end = console->cursor, segment = 0, argument = 0, count = 0;
    /* Complete the token under the caret, including inside a semicolon chain. */
    for (int i = 0; i < console->cursor; ++i) {
        if (console->input[i] == '#') return 0;
        if (console->input[i] == ';') segment = i + 1;
    }
    while (start > segment && !isspace((unsigned char)console->input[start - 1])) --start;
    while (console->input[end] && !isspace((unsigned char)console->input[end]) && console->input[end] != ';' && console->input[end] != '#') ++end;
    memcpy(prefix, console->input + start, (size_t)(console->cursor - start));
    prefix[console->cursor - start] = '\0';
    int pos = segment;
    command[0] = '\0';
    while (pos < start) {
        while (pos < start && isspace((unsigned char)console->input[pos])) ++pos;
        int begin = pos;
        while (pos < start && !isspace((unsigned char)console->input[pos])) ++pos;
        if (pos > begin) {
            if (!argument) { memcpy(command, console->input + begin, (size_t)(pos - begin)); command[pos - begin] = '\0'; }
            ++argument;
        }
    }
    const CommandHelp *entry = command_help(command);
    if (!argument || (!strcmp(command, "help") && argument == 1)) {
        for (size_t i = 0; i < sizeof(commands) / sizeof(commands[0]); ++i) add_match(matches, &count, commands[i].name, prefix);
        if (argument) add_match(matches, &count, "fields", prefix);
    } else if (!strcmp(command, "player.chassis") && argument == 2) {
        for (int i = 0; i < GAME_CHASSIS; ++i) add_match(matches, &count, game_chassis[i].name, prefix);
    } else if (!strcmp(command, "assert") && argument == 1) {
        for (size_t i = 0; i < sizeof(global_fields) / sizeof(global_fields[0]); ++i) add_match(matches, &count, global_fields[i], prefix);
        for (int slot = 1; slot <= 2; ++slot) for (size_t i = 0; i < sizeof(player_fields) / sizeof(player_fields[0]); ++i) {
            char name[64]; snprintf(name, sizeof(name), "p%d.%s", slot, player_fields[i]); add_match(matches, &count, name, prefix);
        }
    } else if (entry && argument <= 5 && entry->options[argument - 1]) {
        const char *option = entry->options[argument - 1];
        while (*option) {
            char name[64]; int n = 0;
            while (*option && *option != ' ') { if (n < 63) name[n++] = *option; ++option; }
            name[n] = '\0'; add_match(matches, &count, name, prefix);
            if (*option) ++option;
        }
    }
    bool repeat = !strcmp(console->input, console->completion);
    if (!count) {
        if (entry) { console_log(console, "%s", entry->usage); console_log(console, "  %s", entry->description); }
        else console_log(console, "No completion for '%s'. Type help for all commands.", prefix);
        return 0;
    }
    size_t common = strlen(matches[0]);
    for (int i = 1; i < count; ++i) {
        size_t n = 0; while (n < common && matches[0][n] == matches[i][n]) ++n; common = n;
    }
    int suffix = (int)strlen(console->input + end);
    bool space = count == 1 && !console->input[end];
    if (start + common + space + suffix < CONSOLE_LINE) {
        memmove(console->input + start + common + space, console->input + end, (size_t)suffix + 1);
        memcpy(console->input + start, matches[0], common);
        console->cursor = start + (int)common;
        if (space) console->input[console->cursor++] = ' ';
    }
    if (count > 1 && repeat) for (int i = 0; i < count; ++i) console_log(console, "  %s", matches[i]);
    strcpy(console->completion, console->input);
    console->scroll = 0;
    return count;
}

bool console_submit(Console *console, Game *game) {
    if (!console->input[0]) return true;
    if (!console->command_count || strcmp(console->commands[console->command_count - 1], console->input)) {
        if (console->command_count == CONSOLE_COMMAND_HISTORY) {
            memmove(console->commands, console->commands + 1, sizeof(console->commands[0]) * (CONSOLE_COMMAND_HISTORY - 1));
            --console->command_count;
        }
        strcpy(console->commands[console->command_count++], console->input);
    }
    console_log(console, "> %s", console->input);
    bool success = console_chain(console, game, console->input);
    console->input[0] = console->draft[0] = console->completion[0] = '\0';
    console->cursor = console->history_offset = console->scroll = 0;
    return success;
}

void console_log(Console *console, const char *format, ...) {
    if (console->count == CONSOLE_HISTORY) {
        memmove(console->lines, console->lines + 1, sizeof(console->lines[0]) * (CONSOLE_HISTORY - 1));
        --console->count;
    }
    va_list args;
    va_start(args, format);
    vsnprintf(console->lines[console->count], sizeof(console->lines[0]), format, args);
    va_end(args);
    puts(console->lines[console->count++]);
    fflush(stdout);
}

static bool number(const char *text, float *value, float low, float high) {
    char *end = NULL;
    errno = 0;
    *value = strtof(text, &end);
    return !errno && end != text && *end == '\0' && isfinite(*value) && *value >= low && *value <= high;
}

static bool integer(const char *text, int *value, int low, int high) {
    char *end = NULL;
    errno = 0;
    long parsed = strtol(text, &end, 10);
    if (errno || end == text || *end || parsed < low || parsed > high) return false;
    *value = (int)parsed;
    return true;
}

static bool field_get(const Game *game, const char *name, float *value) {
    if (!strcmp(name, "stage")) *value = (float)(game->stage.index + 1);
    else if (!strcmp(name, "stage.camera")) *value = game->stage.camera_y;
    else if (!strcmp(name, "stage.stopped")) *value = game->stage.stopped;
    else if (!strcmp(name, "stage.music_time")) *value = (game->tick - game->stage.music_tick) / 60.0f;
    else if (!strcmp(name, "tick")) *value = (float)game->tick;
    else if (!strcmp(name, "enemies")) *value = (float)game_enemy_count(game);
    else if (!strcmp(name, "bullets")) *value = (float)game_bullet_count(game);
    else if (!strcmp(name, "kills")) *value = (float)game->kills;
    else if (!strcmp(name, "shots")) *value = (float)game->shots;
    else if (!strcmp(name, "hits")) *value = (float)game->hits;
    else if (!strcmp(name, "dropped")) *value = (float)game->dropped_spawns;
    else if (!strcmp(name, "over")) *value = game->over;
    else if (!strcmp(name, "complete")) *value = game->complete;
    else if (!strcmp(name, "menu")) *value = (float)game->menu.page;
    else if (!strcmp(name, "menu.cursor")) *value = (float)game->menu.cursor;
    else if (!strcmp(name, "difficulty")) *value = (float)game->difficulty;
    else if (!strcmp(name, "volume.sfx")) *value = (float)game->menu.options[0];
    else if (!strcmp(name, "volume.music")) *value = (float)game->menu.options[1];
    else if (!strncmp(name, "p1.", 3) || !strncmp(name, "p2.", 3)) {
        const Player *player = &game->players[name[1] - '1'];
        name += 3;
        if (!strcmp(name, "x")) *value = player->x;
        else if (!strcmp(name, "y")) *value = player->y;
        else if (!strcmp(name, "health")) *value = player->health;
        else if (!strcmp(name, "lives")) *value = (float)player->lives;
        else if (!strcmp(name, "score")) *value = (float)player->score;
        else if (!strcmp(name, "joined")) *value = player->joined;
        else if (!strcmp(name, "money")) *value = (float)player->money;
        else if (!strcmp(name, "chassis")) *value = (float)player->chassis;
        else if (!strcmp(name, "armor")) *value = game_chassis[player->chassis].armor;
        else if (!strcmp(name, "invincible")) *value = player->invincible;
        else if (!strcmp(name, "pilot")) *value = (float)player->pilot;
        else if (!strcmp(name, "weapon_level")) *value = (float)player->weapon_level;
        else if (!strcmp(name, "energy")) *value = player->energy;
        else return false;
    } else return false;
    return true;
}

bool console_execute(Console *console, Game *game, const char *line) {
    char buffer[CONSOLE_LINE];
    if (strlen(line) >= sizeof(buffer)) { console_log(console, "ERROR: command too long"); return false; }
    strcpy(buffer, line);
    char *words[10];
    int count = 0;
    char *cursor = buffer;
    while (*cursor) {
        while (isspace((unsigned char)*cursor)) ++cursor;
        if (!*cursor || *cursor == '#') break;
        if (count == 10) { console_log(console, "ERROR: too many arguments"); return false; }
        words[count++] = cursor;
        while (*cursor && !isspace((unsigned char)*cursor)) ++cursor;
        if (*cursor) *cursor++ = '\0';
    }
    if (!count) return true;
    const char *command = words[0];
    if (!strcmp(command, "debug.music")) {
        if (count == 1) {
            console_log(console, "Music override: %s; last requested progress %.3f", console->music_track ? "on" : "off", console->music_progress);
            for (int track = 1; track <= SYNTH_TRACK_COUNT; ++track)
                console_log(console, "%d: %s (%.3fs)", track, synth_track_name(track), synth_track_frames(track) / (float)SYNTH_RATE);
            return true;
        }
        if (count == 2 && !strcmp(words[1], "off")) {
            console->music_track = 0;
            ++console->music_revision;
            console_log(console, "Music override off; normal music restored.");
            return true;
        }
        int track;
        float progress = 0;
        if ((count != 2 && count != 3) || !integer(words[1], &track, 1, SYNTH_TRACK_COUNT) ||
            (count == 3 && !number(words[2], &progress, 0, 1))) return false;
        console->music_track = track;
        console->music_progress = progress;
        ++console->music_revision;
        console_log(console, "Music override %d: %s at %.3f (%.3fs); headless/mute has no audio.",
            track, synth_track_name(track), progress, progress * synth_track_frames(track) / SYNTH_RATE);
        return true;
    }
    int value, slot, fire, focus;
    float x, y, expected;
    if (!strcmp(command, "help") && count <= 2) return print_help(console, count == 2 ? words[1] : NULL);
    if (!strcmp(command, "clear") && count == 1) { console->count = console->scroll = 0; return true; }
    if (!strcmp(command, "perf") && (count == 1 || (count == 2 && (!strcmp(words[1], "on") || !strcmp(words[1], "off"))))) {
        console->perf = count == 1 ? !console->perf : !strcmp(words[1], "on");
        console_log(console, "Frame/work graph: %s (F3)", console->perf ? "on" : "off");
        return true;
    }
    if (!strcmp(command, "quit") && count == 1) { console->quit = true; return true; }
    if (!strcmp(command, "player.upgrade") && count == 3 && integer(words[1], &slot, 1, 2) && integer(words[2], &value, 0, 7)) {
        if (!game_upgrade_weapon(game, slot - 1, value)) { console_log(console, "ERROR: player not joined"); return false; }
        console_log(console, "p%d weapon=FrontLaser level=%d", slot, value);
        return true;
    }
    if (!strcmp(command, "menu.open") && count == 2) {
        if (!strcmp(words[1], "shop")) { game_shop_open(game); return true; }
        if (!strcmp(words[1], "main")) { game_menu_open(game, MENU_MAIN); return true; }
        if (!strcmp(words[1], "pause")) { game_menu_open(game, MENU_PAUSE); return true; }
    }
    if (!strcmp(command, "menu.input") && (count == 2 || count == 3)) {
        const char *actions[] = {"up", "down", "left", "right", "confirm", "back", "ready"};
        slot = 1;
        if (count == 3 && !integer(words[2], &slot, 1, 2)) return false;
        for (int i = 0; i < 7; ++i) if (!strcmp(words[1], actions[i])) {
            game_menu_input(game, (MenuAction)i, slot - 1);
            return true;
        }
    }
    if (!strcmp(command, "chassis.list") && count == 1) {
        for (int i = 0; i < GAME_CHASSIS; ++i) {
            const Chassis *c = &game_chassis[i];
            console_log(console, "%d %s armor=%.0f speed=%.2f friction=%.3f price=%d", i, c->name, c->armor, c->speed, c->friction, c->price);
        }
        return true;
    }
    if ((!strcmp(command, "player.money") || !strcmp(command, "player.invincible") ||
         !strcmp(command, "player.chassis") || !strcmp(command, "player.heal")) &&
        count >= 2 && integer(words[1], &slot, 1, GAME_PLAYERS)) {
        Player *p = &game->players[slot - 1];
        if (!p->joined) { console_log(console, "ERROR: player not joined"); return false; }
        if (!strcmp(command, "player.money") && count == 3 && integer(words[2], &value, 0, 1000000000)) {
            p->money = value;
            console_log(console, "p%d money=%d", slot, p->money);
            return true;
        }
        if (!strcmp(command, "player.invincible") && count == 3 && integer(words[2], &value, 0, 1)) {
            p->invincible = value != 0;
            console_log(console, "p%d invincible=%d", slot, p->invincible);
            return true;
        }
        if (!strcmp(command, "player.heal") && count == 2 && p->lives > 0) {
            p->health = game_chassis[p->chassis].armor;
            return true;
        }
        if (!strcmp(command, "player.chassis") && count == 3) {
            for (int i = 0; i < GAME_CHASSIS; ++i) if (!strcmp(words[2], game_chassis[i].name)) {
                if (!game_equip_chassis(game, slot - 1, i)) return false;
                console_log(console, "p%d chassis=%s armor=%.0f", slot, game_chassis[i].name, game_chassis[i].armor);
                return true;
            }
        }
    }
    if (!strcmp(command, "equipment.list") && count == 1) {
        for (int i = 0; i < WEAPON_COUNT; ++i) if (equipment_weapon_category(i) >= 0)
            console_log(console, "%s: category=%d levels=0..%d", game_weapons[i].name, equipment_weapon_category(i), game_weapons[i].level_count - 1);
        for (int i = 0; i < GENERATOR_COUNT; ++i) console_log(console, "generator %d: %s", i, game_generators[i].name);
        for (int i = 0; i < SHIELD_COUNT; ++i) console_log(console, "shield %d: %s", i, game_shields[i].name);
        return true;
    }
    if (!strcmp(command, "player.weapon") && count == 5 && integer(words[1], &slot, 1, 2) && integer(words[4], &value, 0, 7)) {
        Player *p = &game->players[slot - 1];
        int category = !strcmp(words[2], "primary") ? 3 : !strcmp(words[2], "secondary") ? 4 : !strcmp(words[2], "sidekick") ? 5 : -1;
        int kind = equipment_find_weapon(words[3]);
        bool none = !strcmp(words[3], "None") && category >= 4;
        if (!p->joined || (!none && (kind < 0 || equipment_weapon_category(kind) != category || value >= game_weapons[kind].level_count))) return false;
        if (category == 3) { p->primary = kind; p->weapon_level = value; p->powerup_levels = 0; }
        else if (category == 4) { p->secondary = kind; p->secondary_level = value; }
        else if (category == 5) p->sidekick = kind;
        else return false;
        memset(p->weapons, 0, sizeof(p->weapons));
        return true;
    }
    if ((!strcmp(command, "player.generator") || !strcmp(command, "player.shield")) && count == 3 && integer(words[1], &slot, 1, 2)) {
        Player *p = &game->players[slot - 1];
        bool generator = !strcmp(command, "player.generator");
        if (!p->joined || !integer(words[2], &value, 0, (generator ? GENERATOR_COUNT : SHIELD_COUNT) - 1)) return false;
        if (generator) { p->generator = value; p->energy = game_generators[value].capacity; }
        else { p->shield = value; p->shield_health = game_shields[value].capacity; }
        return true;
    }
    if (!strcmp(command, "game.reset") && count == 2 && integer(words[1], &value, 0, 2147483647)) {
        game_reset(game, (uint32_t)value);
        return true;
    }
    if (!strcmp(command, "game.stage") && (count == 2 || count == 3)) {
        if (!integer(words[1], &value, 1, STAGE_COUNT)) {
            value = 0;
            for (int i = 0; i < STAGE_COUNT; ++i)
                if (!strcmp(words[1], stage_definitions[i].name)) value = i + 1;
            if (!value) return false;
        }
        x = 0;
        if (count == 3 && !number(words[2], &x, 0, 1)) return false;
        int ticks = (int)(x * (stage_definitions[value - 1].length + 400) / stage_definitions[value - 1].scroll_speed);
        if (ticks > CONSOLE_STEP_LIMIT - console->steps) {
            console_log(console, "ERROR: script exceeded %d requested ticks", CONSOLE_STEP_LIMIT);
            return false;
        }
        if (!game_stage(game, value - 1, x)) {
            console_log(console, "ERROR: stage %d could not be loaded", value);
            return false;
        }
        console->steps += ticks;
        console_log(console, "stage=%d tick=%d camera=%.2f music=%s music_time=%.2f", value, game->tick,
            game->stage.camera_y, game_music(game), (game->tick - game->stage.music_tick) / 60.0f);
        return true;
    }
    if (!strcmp(command, "game.waves") && count == 2 && integer(words[1], &value, 0, 1)) {
        game->waves = value != 0;
        return true;
    }
    if (!strcmp(command, "game.pause") && count == 2 && integer(words[1], &value, 0, 1)) {
        game_menu_open(game, value ? MENU_PAUSE : MENU_NONE);
        return true;
    }
    if (!strcmp(command, "game.step") && count == 2 && integer(words[1], &value, 1, 36000)) {
        if (value > CONSOLE_STEP_LIMIT - console->steps) {
            console_log(console, "ERROR: script exceeded %d requested ticks", CONSOLE_STEP_LIMIT);
            return false;
        }
        console->steps += value;
        for (int i = 0; i < value; ++i) game_tick(game);
        return true;
    }
    if (!strcmp(command, "player.join") && count == 2 && integer(words[1], &slot, 1, 2)) {
        if (!game_join(game, slot - 1)) { console_log(console, "ERROR: player cannot join"); return false; }
        return true;
    }
    if (!strcmp(command, "player.input") && count == 6 && integer(words[1], &slot, 1, 2) &&
        number(words[2], &x, -1, 1) && number(words[3], &y, -1, 1) &&
        integer(words[4], &fire, 0, 1) && integer(words[5], &focus, 0, 1)) {
        if (!game->players[slot - 1].joined) { console_log(console, "ERROR: player not joined"); return false; }
        game->players[slot - 1].input = (PlayerInput){.x=x, .y=y, .fire=fire != 0, .focus=focus != 0};
        return true;
    }
    if (!strcmp(command, "enemy.spawn") && count == 4 &&
        number(words[2], &x, -200, GAME_WIDTH + 200) && number(words[3], &y, -250, GAME_HEIGHT + 200)) {
        const char *kinds[] = {"ball", "triangle", "turret", "boss"};
        for (int i = 0; i < ENEMY_KIND_COUNT; ++i) if (!strcmp(words[1], enemy_names[i]) || (i < 4 && !strcmp(words[1], kinds[i]))) {
            if (game_spawn_enemy(game, (EnemyKind)i, x, y) < 0) { console_log(console, "ERROR: enemy pool full"); return false; }
            return true;
        }
    }
    if (!strcmp(command, "state") && count == 1) {
        console_log(console, "menu=%d cursor=%d difficulty=%d sfx=%d music=%d", game->menu.page,
            game->menu.cursor, game->difficulty, game->menu.options[0], game->menu.options[1]);
        console_log(console, "tick=%d enemies=%d bullets=%d kills=%d shots=%d hits=%d over=%d complete=%d",
            game->tick, game_enemy_count(game), game_bullet_count(game), game->kills, game->shots, game->hits, game->over, game->complete);
        for (int i = 0; i < GAME_PLAYERS; ++i) {
            const Player *p = &game->players[i];
            console_log(console, "p%d joined=%d x=%.2f y=%.2f health=%.2f lives=%d score=%d",
                i + 1, p->joined, p->x, p->y, p->health, p->lives, p->score);
            console_log(console, "p%d money=%d chassis=%s invincible=%d", i + 1, p->money, game_chassis[p->chassis].name, p->invincible);
            console_log(console, "p%d weapon=%s level=%d energy=%.3f shield=%.3f", i + 1, game_weapons[p->primary].name, p->weapon_level, p->energy, p->shield_health);
        }
        return true;
    }
    if (!strcmp(command, "assert") && count == 4 && field_get(game, words[1], &x) && number(words[3], &expected, -1e9f, 1e9f)) {
        bool pass;
        if (!strcmp(words[2], "eq")) pass = fabsf(x - expected) < 0.001f;
        else if (!strcmp(words[2], "ne")) pass = fabsf(x - expected) >= 0.001f;
        else if (!strcmp(words[2], "lt")) pass = x < expected;
        else if (!strcmp(words[2], "le")) pass = x <= expected;
        else if (!strcmp(words[2], "gt")) pass = x > expected;
        else if (!strcmp(words[2], "ge")) pass = x >= expected;
        else { console_log(console, "ERROR: unknown assertion operator '%s'", words[2]); return false; }
        if (!pass) console_log(console, "FAIL: %s = %.4f, expected %s %.4f", words[1], x, words[2], expected);
        return pass;
    }
    console_log(console, "ERROR: unknown command or invalid arguments: %s (type help)", command);
    const CommandHelp *entry = command_help(command);
    if (entry) console_log(console, "Usage: %s", entry->usage);
    return false;
}

bool console_chain(Console *console, Game *game, const char *commands) {
    const char *start = commands;
    while (*start && !console->quit) {
        const char *end = start;
        while (*end && *end != ';' && *end != '#') ++end;
        size_t length = (size_t)(end - start);
        char line[CONSOLE_LINE];
        if (length >= sizeof(line)) { console_log(console, "ERROR: command too long"); return false; }
        memcpy(line, start, length);
        line[length] = '\0';
        if (!console_execute(console, game, line)) return false;
        if (*end != ';') break; /* A comment consumes the rest of the physical line. */
        start = end + 1;
    }
    return true;
}

bool console_script(Console *console, Game *game, const char *path) {
    FILE *file = fopen(path, "rb");
    if (!file) { console_log(console, "ERROR: cannot open script %s", path); return false; }
    char line[CONSOLE_LINE];
    int line_number = 0;
    bool success = true;
    while (!console->quit && fgets(line, sizeof(line), file)) {
        ++line_number;
        if (line_number > 4096 || (!strchr(line, '\n') && !feof(file)) || !console_chain(console, game, line)) {
            console_log(console, "ERROR: %s:%d: script failed or exceeded line limits", path, line_number);
            success = false;
            break;
        }
    }
    if (ferror(file)) { console_log(console, "ERROR: reading script %s", path); success = false; }
    fclose(file);
    return success;
}
