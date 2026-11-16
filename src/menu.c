#include "game.h"
#include <string.h>

/* Labels/order/spacing come from StateMainMenu.cs, World.cs and OptionsMenu.cs.
   Keep this state independent of raylib so console checks exercise real input. */
static const char *labels[MENU_STAGE_SELECT + 1][4] = {
    [MENU_MAIN] = {"Solo Game", "Coop Game", "Options", "Quit"},
    [MENU_NEW_GAME] = {"Continue", "New Game", "Back"},
    [MENU_DIFFICULTY] = {"Normal", "Hard", "Really Hard", "Back"},
    [MENU_OPTIONS] = {"SFX Volume", "Music Volume", "Screen Size", "Back"},
    [MENU_QUIT] = {"Cancel", "Quit"},
    [MENU_PAUSE] = {"Resume", "Retry", "Options", "Quit"},
    [MENU_RETRY] = {"Cancel", "Retry"},
    [MENU_OVER] = {"Retry!", "Quit"},
    [MENU_CLEAR] = {"Continue"}
};

int game_menu_count(const Game *game) {
    if (game->menu.page == MENU_STAGE_SELECT) return 13;
    int count = 0;
    while (count < 4 && labels[game->menu.page][count]) ++count;
    return count;
}

const char *game_menu_label(const Game *game, int row) {
    static const char *stages[] = {"Stage 1", "Stage 2", "Stage 3", "Stage 4", "Stage 5", "Stage 6",
        "Stage 7", "Stage 8", "Stage 9", "Stage 10", "Stage 11", "Stage 12", "Back"};
    if (game->menu.page == MENU_STAGE_SELECT) return row >= 0 && row < 13 ? stages[row] : "";
    return row >= 0 && row < game_menu_count(game) ? labels[game->menu.page][row] : "";
}

bool game_menu_enabled(const Game *game, int row) {
    return row >= 0 && row < game_menu_count(game) && !(game->menu.page == MENU_NEW_GAME && row == 0 &&
        !game->saves[game->menu.players == 2 ? 1 : 0].valid);
}

bool game_menu_small(const Game *game, int row) {
    MenuPage page = game->menu.page;
    return row == game_menu_count(game) - 1 && page != MENU_RETRY;
}

void game_menu_open(Game *game, MenuPage page) {
    if (page < MENU_NONE || page > MENU_STAGE_SELECT) return;
    MenuPage previous = game->menu.page;
    if (page == MENU_OPTIONS || page == MENU_QUIT) game->menu.parent = previous;
    game->menu.page = page;
    game->menu.frame = 0;
    game->menu.cursor = page == MENU_NEW_GAME && !game->saves[game->menu.players == 2 ? 1 : 0].valid ? 1 : 0;
    game->paused = page != MENU_NONE;
    for (int i = 0; i < GAME_PLAYERS; ++i) memset(&game->players[i].input, 0, sizeof(game->players[i].input));
}

void game_retry(Game *game) {
    Player players[GAME_PLAYERS];
    for (int i = 0; i < GAME_PLAYERS; ++i) players[i] = game->players[i];
    GameMenu menu = game->menu;
    int difficulty = game->difficulty;
    int stage = game->stage.index;
    game_reset_session(game, game->seed);
    game->stage.index = stage;
    game->random = game->seed;
    game_scenery_init(game);
    game->scroll_speed = stage_definitions[stage].scroll_speed;
    game->difficulty = difficulty;
    game->menu = menu;
    for (int i = 0; i < GAME_PLAYERS; ++i) {
        if (!players[i].joined) continue;
        if (i) game_join(game, i);
        Player *p = &game->players[i];
        p->chassis = players[i].chassis;
        p->pilot = players[i].pilot;
        p->weapon_level = players[i].weapon_level;
        p->primary = players[i].primary;
        p->secondary = players[i].secondary;
        p->secondary_level = players[i].secondary_level;
        p->sidekick = players[i].sidekick;
        p->generator = players[i].generator;
        p->shield = players[i].shield;
        p->energy = game_generators[p->generator].capacity;
        p->shield_health = p->shield >= 0 ? game_shields[p->shield].capacity : 0;
        p->friction = game_chassis[p->chassis].friction;
        p->money = players[i].money;
        p->invincible = players[i].invincible;
        p->health = game_chassis[p->chassis].armor;
    }
    game_stats_init(game);
    game_menu_open(game, MENU_NONE);
}

static void start_game(Game *game) {
    static const int starting_money[] = {0, 1500, 2500};
    static const int starting_chassis[] = {1, 0, 2};
    GameMenu menu = game->menu;
    game_reset_session(game, 1);
    game->cleared_game = false;
    game->menu = menu;
    game->difficulty = menu.difficulty;
    if (menu.players == 2) game_join(game, 1);
    for (int i = 0; i < menu.players; ++i) {
        Player *p = &game->players[i];
        p->pilot = menu.pilot[i];
        p->money = starting_money[menu.difficulty];
        p->weapon_level = p->pilot == 0 && menu.difficulty < 2 ? 1 : 2;
        p->primary = p->pilot == 1 ? WEAPON_LIGHTNING : WEAPON_FRONT_LASER;
        if (p->pilot == 1 && menu.difficulty == 2) p->weapon_level = 3;
        p->secondary = p->pilot == 0 ? WEAPON_VULCAN : p->pilot == 1 ? WEAPON_SEEK_BOMB : WEAPON_MISSILE;
        p->secondary_level = p->pilot == 0 ? (menu.difficulty == 0 ? 1 : 2) :
            p->pilot == 1 ? (menu.difficulty > 0 ? 1 : 0) : menu.difficulty;
        p->generator = menu.difficulty == 2 ? 1 : 0;
        p->shield = menu.difficulty > 0 ? 1 : 0;
        p->energy = game_generators[p->generator].capacity;
        p->shield_health = game_shields[p->shield].capacity;
        game_equip_chassis(game, i, starting_chassis[p->pilot]);
    }
    game_stats_init(game);
    memset(&game->saves[menu.players - 1], 0, sizeof(SavedGame));
    game_menu_open(game, MENU_NONE);
    game_checkpoint(game, false);
}

void game_transition(Game *game, Transition transition, int stage) {
    if (game->transition_frames > 15) return;
    game->transition = transition;
    game->transition_stage = stage;
    game->transition_frames = 30;
}

bool game_transition_update(Game *game) {
    if (game->transition_frames <= 0) return false;
    if (--game->transition_frames > 15) return true;
    if (game->transition_frames != 15) return false;
    Transition transition = game->transition;
    int stage = game->transition_stage;
    switch (transition) {
        case TRANSITION_NEW_GAME: start_game(game); break;
        case TRANSITION_CONTINUE: game_continue(game, game->menu.players); break;
        case TRANSITION_RETRY: game_retry(game); break;
        case TRANSITION_SHOP: game_shop_open(game); break;
        case TRANSITION_STAGE_SELECT: game_menu_open(game, MENU_STAGE_SELECT); break;
        case TRANSITION_MAIN: game_menu_open(game, MENU_MAIN); break;
        case TRANSITION_FINISH:
            game_record_stage(game);
            for (int i = 0; i < GAME_PLAYERS; ++i)
                if (game->players[i].joined) game->players[i].money = 999999999;
            game_menu_open(game, MENU_FINISH);
            game->cleared_game = true;
            game->menu.next_stage = 0;
            game_checkpoint(game, true);
            break;
        case TRANSITION_STAGE:
            game_stage(game, stage, 0);
            for (int i = 0; i < GAME_PLAYERS; ++i) {
                Player *p = &game->players[i];
                if (!p->joined) continue;
                p->x = i ? 560 : 400; p->y = 880; p->vx = p->vy = 0;
                p->energy = game_generators[p->generator].capacity;
                p->shield_health = p->shield >= 0 ? game_shields[p->shield].capacity : 0;
                memset(p->weapons, 0, sizeof(p->weapons));
                p->weapon_cooldown[0] = p->weapon_cooldown[1] = 0;
            }
            game_checkpoint(game, false);
            break;
        default: break;
    }
    game->transition = TRANSITION_NONE;
    game->transition_frames = 15;
    return true;
}

void game_menu_input(Game *game, MenuAction action, int slot) {
    GameMenu *menu = &game->menu;
    if (game->transition_frames > 15 || slot < 0 || slot >= GAME_PLAYERS || menu->page == MENU_NONE) return;
    if (menu->page == MENU_FINISH) {
        if (action == MENU_BACK && menu->frame > 3200) game_transition(game, TRANSITION_MAIN, 0);
        return;
    }
    if (menu->page == MENU_SHOP) { game_shop_input(game, action, slot); return; }
    if (menu->page == MENU_STAGE_SELECT) {
        if (slot != 0) return;
        if (action == MENU_UP && menu->cursor > 0) --menu->cursor;
        if (action == MENU_DOWN && menu->cursor < 12) ++menu->cursor;
        if (action == MENU_BACK || (action == MENU_CONFIRM && menu->cursor == 12)) game_transition(game, TRANSITION_SHOP, 0);
        else if (action == MENU_CONFIRM) {
            game_transition(game, TRANSITION_STAGE, menu->cursor);
        }
        return;
    }
    if (menu->page == MENU_CLEAR) {
        if ((action == MENU_CONFIRM || action == MENU_BACK) && menu->frame > 380 && !game->clear_exit) {
            game->clear_exit = true;
            menu->frame = 360;
        }
        return;
    }
    if (menu->page == MENU_PILOT) {
        if (slot >= menu->players) return;
        if (action == MENU_BACK) {
            if (menu->ready[slot]) menu->ready[slot] = false;
            else game_menu_open(game, MENU_DIFFICULTY);
        } else if (!menu->ready[slot] && (action == MENU_LEFT || action == MENU_RIGHT)) {
            int step = action == MENU_LEFT ? -1 : 1;
            int pilot = menu->pilot[slot] + step;
            if (menu->players == 2 && pilot == menu->pilot[1 - slot]) pilot += step;
            if (pilot >= 0 && pilot < 3) menu->pilot[slot] = pilot;
        } else if (action == MENU_CONFIRM) {
            menu->ready[slot] = true;
            if (menu->ready[0] && (menu->players == 1 || menu->ready[1])) game_transition(game, TRANSITION_NEW_GAME, 0);
        }
        return;
    }
    if (slot != 0) return;
    int count = game_menu_count(game);
    if (action == MENU_UP && menu->cursor > 0) --menu->cursor;
    if (action == MENU_DOWN && menu->cursor + 1 < count) ++menu->cursor;
    if (menu->page == MENU_OPTIONS && menu->cursor < 3) {
        int *value = &menu->options[menu->cursor];
        if (action == MENU_LEFT && *value > 0) --*value;
        if (action == MENU_RIGHT && *value < 10) ++*value;
    }
    if (action == MENU_BACK) {
        switch (menu->page) {
            case MENU_MAIN: break;
            case MENU_NEW_GAME: game_menu_open(game, MENU_MAIN); break;
            case MENU_DIFFICULTY: game_menu_open(game, MENU_NEW_GAME); break;
            case MENU_OPTIONS: case MENU_QUIT: game_menu_open(game, menu->parent); break;
            case MENU_RETRY: game_menu_open(game, MENU_PAUSE); break;
            case MENU_PAUSE: game_menu_open(game, MENU_NONE); break;
            default: break;
        }
        return;
    }
    if (action != MENU_CONFIRM || !game_menu_enabled(game, menu->cursor)) return;
    int row = menu->cursor;
    switch (menu->page) {
        case MENU_MAIN:
            if (row < 2) { menu->players = row + 1; game_menu_open(game, MENU_NEW_GAME); }
            else game_menu_open(game, row == 2 ? MENU_OPTIONS : MENU_QUIT);
            break;
        case MENU_NEW_GAME:
            if (row == 0) game_transition(game, TRANSITION_CONTINUE, 0);
            else game_menu_open(game, row == 1 ? MENU_DIFFICULTY : MENU_MAIN);
            break;
        case MENU_DIFFICULTY:
            if (row == 3) game_menu_open(game, MENU_NEW_GAME);
            else {
                menu->difficulty = row;
                menu->pilot[0] = 0; menu->pilot[1] = 1;
                menu->ready[0] = menu->ready[1] = false;
                game_menu_open(game, MENU_PILOT);
            }
            break;
        case MENU_OPTIONS: if (row == 3) game_menu_open(game, menu->parent); break;
        case MENU_QUIT:
            if (row == 0) game_menu_open(game, menu->parent);
            else if (menu->parent == MENU_MAIN) menu->quit = true;
            else game_transition(game, TRANSITION_SHOP, 0);
            break;
        case MENU_PAUSE:
            game_menu_open(game, row == 0 ? MENU_NONE : row == 1 ? MENU_RETRY : row == 2 ? MENU_OPTIONS : MENU_QUIT);
            break;
        case MENU_RETRY: if (row == 0) game_menu_open(game, MENU_PAUSE); else game_transition(game, TRANSITION_RETRY, 0); break;
        case MENU_OVER: game_transition(game, row == 0 ? TRANSITION_RETRY : TRANSITION_MAIN, 0); break;
        default: break;
    }
}
