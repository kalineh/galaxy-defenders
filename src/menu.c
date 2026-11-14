#include "game.h"

/* Labels/order/spacing come from StateMainMenu.cs, World.cs and OptionsMenu.cs.
   Keep this state independent of raylib so console checks exercise real input. */
static const char *labels[][4] = {
    [MENU_MAIN] = {"Solo Game", "Coop Game", "Options", "Quit"},
    [MENU_NEW_GAME] = {"Continue", "New Game", "Back"},
    [MENU_DIFFICULTY] = {"Normal", "Hard", "Really Hard", "Back"},
    [MENU_OPTIONS] = {"SFX Volume", "Music Volume", "Screen Size", "Back"},
    [MENU_QUIT] = {"Cancel", "Quit"},
    [MENU_PAUSE] = {"Resume", "Retry", "Options", "Quit"},
    [MENU_RETRY] = {"Cancel", "Retry"},
    [MENU_OVER] = {"Retry!", "Quit"}
};

int game_menu_count(const Game *game) {
    int count = 0;
    while (count < 4 && labels[game->menu.page][count]) ++count;
    return count;
}

const char *game_menu_label(const Game *game, int row) {
    return row >= 0 && row < game_menu_count(game) ? labels[game->menu.page][row] : "";
}

bool game_menu_enabled(const Game *game, int row) {
    /* Continue stays disabled until native persistence exists. */
    return row >= 0 && row < game_menu_count(game) && !(game->menu.page == MENU_NEW_GAME && row == 0);
}

bool game_menu_small(const Game *game, int row) {
    MenuPage page = game->menu.page;
    return row == game_menu_count(game) - 1 && page != MENU_RETRY;
}

void game_menu_open(Game *game, MenuPage page) {
    if (page < MENU_NONE || page > MENU_OVER) return;
    MenuPage previous = game->menu.page;
    if (page == MENU_OPTIONS || page == MENU_QUIT) game->menu.parent = previous;
    game->menu.page = page;
    game->menu.cursor = page == MENU_NEW_GAME ? 1 : 0;
    game->paused = page != MENU_NONE;
    for (int i = 0; i < GAME_PLAYERS; ++i) game->players[i].input = (PlayerInput){0};
}

void game_retry(Game *game) {
    Player players[GAME_PLAYERS];
    for (int i = 0; i < GAME_PLAYERS; ++i) players[i] = game->players[i];
    GameMenu menu = game->menu;
    int difficulty = game->difficulty;
    game_reset(game, 1);
    game->difficulty = difficulty;
    game->menu = menu;
    for (int i = 0; i < GAME_PLAYERS; ++i) {
        if (!players[i].joined) continue;
        if (i) game_join(game, i);
        Player *p = &game->players[i];
        p->chassis = players[i].chassis;
        p->pilot = players[i].pilot;
        p->weapon_level = players[i].weapon_level;
        p->friction = game_chassis[p->chassis].friction;
        p->money = players[i].money;
        p->invincible = players[i].invincible;
        p->health = game_chassis[p->chassis].armor;
    }
    game_menu_open(game, MENU_NONE);
}

static void start_game(Game *game) {
    static const int starting_money[] = {0, 1500, 2500};
    static const int starting_chassis[] = {1, 0, 2};
    GameMenu menu = game->menu;
    game_reset(game, 1);
    game->menu = menu;
    game->difficulty = menu.difficulty;
    if (menu.players == 2) game_join(game, 1);
    for (int i = 0; i < menu.players; ++i) {
        Player *p = &game->players[i];
        p->pilot = menu.pilot[i];
        p->money = starting_money[menu.difficulty];
        p->weapon_level = p->pilot == 0 && menu.difficulty < 2 ? 1 : 2;
        game_equip_chassis(game, i, starting_chassis[p->pilot]);
    }
    game_menu_open(game, MENU_NONE);
}

void game_menu_input(Game *game, MenuAction action, int slot) {
    GameMenu *menu = &game->menu;
    if (slot < 0 || slot >= GAME_PLAYERS || menu->page == MENU_NONE) return;
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
            if (menu->ready[0] && (menu->players == 1 || menu->ready[1])) start_game(game);
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
        case MENU_NEW_GAME: game_menu_open(game, row == 1 ? MENU_DIFFICULTY : MENU_MAIN); break;
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
            else game_menu_open(game, MENU_MAIN); /* Shop is not ported yet. */
            break;
        case MENU_PAUSE:
            game_menu_open(game, row == 0 ? MENU_NONE : row == 1 ? MENU_RETRY : row == 2 ? MENU_OPTIONS : MENU_QUIT);
            break;
        case MENU_RETRY: if (row == 0) game_menu_open(game, MENU_PAUSE); else game_retry(game); break;
        case MENU_OVER: if (row == 0) game_retry(game); else game_menu_open(game, MENU_MAIN); break;
        default: break;
    }
}
