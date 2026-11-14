#include "console.h"
#include <ctype.h>
#include <errno.h>
#include <math.h>
#include <stdarg.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

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
    if (!strcmp(name, "tick")) *value = (float)game->tick;
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
    int value, slot, fire, focus;
    float x, y, expected;
    if (!strcmp(command, "help") && count == 1) {
        console_log(console, "game.reset SEED | game.waves 0/1 | game.pause 0/1 | game.step TICKS");
        console_log(console, "player.join 1/2 | player.input SLOT X Y FIRE FOCUS (axes -1..1, flags 0/1)");
        console_log(console, "enemy.spawn ball/triangle/turret/boss X Y | state | quit");
        console_log(console, "player.money SLOT AMOUNT (set) | player.invincible SLOT 0/1 | player.heal SLOT");
        console_log(console, "player.chassis SLOT NAME (equip, no cost) | chassis.list");
        console_log(console, "menu.open main/pause | menu.input up/down/left/right/confirm/back [SLOT]");
        console_log(console, "assert FIELD eq/ne/lt/le/gt/ge VALUE; p1/p2.x/y/health/lives/score/joined");
        console_log(console, "Fields: tick enemies bullets kills shots hits dropped over complete");
        return true;
    }
    if (!strcmp(command, "quit") && count == 1) { console->quit = true; return true; }
    if (!strcmp(command, "menu.open") && count == 2) {
        if (!strcmp(words[1], "main")) { game_menu_open(game, MENU_MAIN); return true; }
        if (!strcmp(words[1], "pause")) { game_menu_open(game, MENU_PAUSE); return true; }
    }
    if (!strcmp(command, "menu.input") && (count == 2 || count == 3)) {
        const char *actions[] = {"up", "down", "left", "right", "confirm", "back"};
        slot = 1;
        if (count == 3 && !integer(words[2], &slot, 1, 2)) return false;
        for (int i = 0; i < 6; ++i) if (!strcmp(words[1], actions[i])) {
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
    if (!strcmp(command, "game.reset") && count == 2 && integer(words[1], &value, 0, 2147483647)) {
        game_reset(game, (uint32_t)value);
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
        game->players[slot - 1].input = (PlayerInput){x, y, fire != 0, focus != 0};
        return true;
    }
    if (!strcmp(command, "enemy.spawn") && count == 4 &&
        number(words[2], &x, -200, GAME_WIDTH + 200) && number(words[3], &y, -250, GAME_HEIGHT + 200)) {
        const char *kinds[] = {"ball", "triangle", "turret", "boss"};
        for (int i = 0; i < 4; ++i) if (!strcmp(words[1], kinds[i])) {
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
