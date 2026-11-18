#include "game.h"
#include <string.h>
#include <stdio.h>

/* Labels/order/spacing come from StateMainMenu.cs, World.cs and OptionsMenu.cs.
   Keep this state independent of raylib so console checks exercise real input. */
const int game_option_max[OPTION_COUNT] = {10, 10, 10, 3, 1, 2, 1};
const int render_scale_percent[4] = {75, 100, 150, 200};

static const char *labels[MENU_CONTROLLERS + 1][OPTION_COUNT+1] = {
    [MENU_MAIN] = {"Play", "Options", "Quit"},
    [MENU_SLOTS] = {"Slot 1", "Slot 2", "Slot 3", "Back"},
    [MENU_DELETE] = {"Cancel", "Delete slot"},
    [MENU_DIFFICULTY] = {"Normal", "Hard", "Really Hard", "Back"},
    [MENU_OPTIONS] = {"SFX Volume", "Music Volume", "Screen Size", "Render Scale", "FXAA", "MSAA", "Display mode", "Back"},
    [MENU_QUIT] = {"Cancel", "Quit"},
    [MENU_PAUSE] = {"Resume", "Retry", "Options", "Controllers", "Quit"},
    [MENU_RETRY] = {"Cancel", "Retry"},
    [MENU_OVER] = {"Retry!", "Quit"},
    [MENU_CLEAR] = {"Continue"}
};

int game_menu_page_count(MenuPage page) {
    if (page == MENU_STAGE_SELECT) return 13;
    int count = 0;
    while (count < OPTION_COUNT+1 && labels[page][count]) ++count;
    return count;
}

int game_menu_count(const Game *game) {
    return game_menu_page_count(game->menu.page);
}

const char *game_slot_label(const Game *game, int row) {
    static char slots[GAME_SAVE_SLOTS][100];
    if (row >= 0 && row < GAME_SAVE_SLOTS) {
        const SavedGame *save = &game->saves[row];
        if (save->valid) snprintf(slots[row], sizeof(slots[row]), "Slot %d - Stage %d", row + 1, save->stage + 1);
        else snprintf(slots[row], sizeof(slots[row]), "Slot %d - %s", row + 1, game->save_errors[row] ? "Unreadable" : "Empty");
        return slots[row];
    }
    return "Back";
}

const char *game_menu_page_label(const Game *game, MenuPage page, int row) {
    if (page == MENU_SLOTS) return game_slot_label(game,row);
    static const char *stages[] = {"Stage 1", "Stage 2", "Stage 3", "Stage 4", "Stage 5", "Stage 6",
        "Stage 7", "Stage 8", "Stage 9", "Stage 10", "Stage 11", "Stage 12", "Back"};
    if (page == MENU_STAGE_SELECT) return row >= 0 && row < 13 ? stages[row] : "";
    return row >= 0 && row < game_menu_page_count(page) ? labels[page][row] : "";
}

const char *game_menu_label(const Game *game, int row) {
    return game_menu_page_label(game,game->menu.page,row);
}

bool game_menu_enabled(const Game *game, int row) {
    return row >= 0 && row < game_menu_count(game);
}

bool game_menu_is_title(const Game *game) {
    MenuPage page = game->menu.page;
    return page == MENU_SLOTS || page == MENU_DELETE || page == MENU_MAIN || page == MENU_INTRO || page == MENU_DIFFICULTY || page == MENU_PILOT ||
        ((page == MENU_OPTIONS || page == MENU_QUIT) && game->menu.parent == MENU_MAIN);
}

void game_menu_open(Game *game, MenuPage page) {
    if (page < MENU_NONE || page > MENU_CONTROLLERS) return;
    MenuPage previous = game->menu.page;
    if (page == MENU_CONTROLLERS && previous != MENU_CONTROLLERS) {
        game->menu.controllers_origin = previous;
        game->menu.controllers_cursor = game->menu.cursor;
        game->menu.controllers_frame = game->menu.frame;
    }
    if (page == MENU_QUIT || page == MENU_DELETE) game->menu.dialog_cursor = game->menu.cursor;
    if (page == MENU_OPTIONS || page == MENU_QUIT) game->menu.parent = previous;
    if (page == MENU_PAUSE && (previous == MENU_NONE || previous == MENU_SHOP))
        game->menu.pause_origin = previous;
    game->menu.page = page;
    game->menu.frame = 0;
    game->menu.cursor = page == MENU_SLOTS && game->active_slot >= 0 ? game->active_slot : 0;
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
    game_random_init(&game->random, game->seed);
    game_scenery_init(game);
    game->scroll_speed = stage_definitions[stage].scroll_speed;
    game->difficulty = difficulty;
    game->menu = menu;
    for (int i = 0; i < GAME_PLAYERS; ++i) {
        if (!players[i].initialized) continue;
        if (i) game_join(game, i);
        Player *p = &game->players[i];
        p->joined = players[i].joined;
        p->chassis = players[i].chassis;
        p->pilot = players[i].pilot;
        p->weapon_level = players[i].weapon_level;
        p->primary = players[i].primary;
        p->secondary = players[i].secondary;
        p->secondary_level = players[i].secondary_level;
        p->sidekick = players[i].sidekick;
        p->sidekick_level = players[i].sidekick_level;
        p->generator = players[i].generator;
        p->shield = players[i].shield;
        p->energy = game_generators[p->generator].capacity;
        p->shield_health = p->shield >= 0 ? game_shields[p->shield].capacity : 0;
        p->friction = game_chassis[p->chassis].friction;
        p->money = players[i].money;
        p->inventory = players[i].inventory;
        p->invincible = players[i].invincible;
        p->health = game_chassis[p->chassis].armor;
    }
    game_stats_init(game);
    game_menu_open(game, MENU_NONE);
}

void game_starting_loadout(Game *game, int slot) {
    static const int starting_money[] = {0, 1500, 2500};
    static const int starting_chassis[] = {1, 0, 2};
    Player *p = &game->players[slot];
    p->money = starting_money[game->difficulty];
    p->weapon_level = p->pilot == 0 && game->difficulty < 2 ? 1 : 2;
    p->primary = p->pilot == 1 ? WEAPON_LIGHTNING : WEAPON_FRONT_LASER;
    if (p->pilot == 1 && game->difficulty == 2) p->weapon_level = 3;
    p->secondary = p->pilot == 0 ? WEAPON_VULCAN : p->pilot == 1 ? WEAPON_SEEK_BOMB : WEAPON_MISSILE;
    p->secondary_level = p->pilot == 0 ? (game->difficulty == 0 ? 1 : 2) :
        p->pilot == 1 ? (game->difficulty > 0 ? 1 : 0) : game->difficulty;
    p->generator = game->difficulty == 2 ? 1 : 0;
    p->shield = game->difficulty > 0 ? 1 : 0;
    p->energy = game_generators[p->generator].capacity;
    p->shield_health = game_shields[p->shield].capacity;
    game_equip_chassis(game, slot, starting_chassis[p->pilot]);
}

static void start_game(Game *game) {
    GameMenu menu = game->menu;
    bool joined[GAME_PLAYERS] = {game->players[0].joined, game->players[1].joined};
    if (!joined[0] && menu.pilot[0] == menu.pilot[1]) menu.pilot[0] = (menu.pilot[1] + 1) % 3;
    game_reset_session(game, 1);
    game->cleared_game = false;
    game->menu = menu;
    game->difficulty = menu.difficulty;
    if (joined[1]) game_join(game, 1);
    for (int i = 0; i < menu.players; ++i) {
        game->players[i].pilot = menu.pilot[i];
        game_starting_loadout(game,i);
    }
    for (int i = 0; i < GAME_PLAYERS; ++i) game->players[i].joined = joined[i];
    game_stats_init(game);
    game_menu_open(game, MENU_INTRO);
}

void game_transition(Game *game, Transition transition, int stage) {
    if (game->transition_frames > 15) return;
    game->transition = transition;
    game->transition_stage = stage;
    /* Skipping a newly entered scene reverses its fade at the current opacity. */
    game->fade_start = game->transition_frames / 15.0f;
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
        case TRANSITION_CONTINUE: game_continue(game, game->active_slot); break;
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
            break;
        default: break;
    }
    game->transition = TRANSITION_NONE;
    game->transition_frames = 15;
    return true;
}

void game_menu_input(Game *game, MenuAction action, int slot) {
    GameMenu *menu = &game->menu;
    if (slot < 0 || slot >= GAME_PLAYERS || menu->page == MENU_NONE) return;
    if (menu->page == MENU_CONTROLLERS) {
        if (action == MENU_BACK && (game->players[0].joined || game->players[1].joined)) {
            menu->page = menu->controllers_origin;
            menu->cursor = menu->controllers_cursor;
            menu->frame = menu->controllers_frame;
            game->paused = menu->page != MENU_NONE;
        }
        return;
    }
    if (game->transition_frames > 15) return;
    if (menu->page == MENU_FINISH) {
        if (action == MENU_BACK && menu->frame > 3200) game_transition(game, TRANSITION_MAIN, 0);
        return;
    }
    if (menu->page == MENU_SHOP) { game_shop_input(game, action, slot); return; }
    if (menu->page == MENU_INTRO) {
        if (game->players[slot].joined && action == MENU_CONFIRM) game_transition(game, TRANSITION_STAGE, 0);
        return;
    }
    if (menu->page == MENU_STAGE_SELECT) {
        if (!game->players[slot].joined) return;
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
        if (!game->players[slot].joined) return;
        if (action == MENU_BACK) {
            if (menu->ready[slot]) menu->ready[slot] = false;
            else game_menu_open(game, MENU_DIFFICULTY);
        } else if (!menu->ready[slot] && (action == MENU_LEFT || action == MENU_RIGHT)) {
            int step = action == MENU_LEFT ? -1 : 1;
            int pilot = menu->pilot[slot] + step;
            if (game->players[1 - slot].joined && pilot == menu->pilot[1 - slot]) pilot += step;
            if (pilot >= 0 && pilot < 3) menu->pilot[slot] = pilot;
        } else if (action == MENU_CONFIRM) {
            menu->ready[slot] = true;
            if ((!game->players[0].joined || menu->ready[0]) && (!game->players[1].joined || menu->ready[1])) game_transition(game, TRANSITION_NEW_GAME, 0);
        }
        return;
    }
    if (!game->players[slot].joined) return;
    int count = game_menu_count(game);
    if ((menu->page == MENU_DELETE || menu->page == MENU_QUIT) && action == MENU_LEFT) menu->cursor = 0;
    if ((menu->page == MENU_DELETE || menu->page == MENU_QUIT) && action == MENU_RIGHT) menu->cursor = 1;
    if (action == MENU_UP && menu->cursor > 0) --menu->cursor;
    if (action == MENU_DOWN && menu->cursor + 1 < count) ++menu->cursor;
    if (menu->page == MENU_OPTIONS && menu->cursor < OPTION_COUNT) {
        int *value = &menu->options[menu->cursor];
        if (action == MENU_LEFT && *value > 0) --*value;
        if (action == MENU_RIGHT && *value < game_option_max[menu->cursor]) ++*value;
    }
    if (action == MENU_BACK) {
        switch (menu->page) {
            case MENU_MAIN: break;
            case MENU_SLOTS: game_menu_open(game, MENU_MAIN); break;
            case MENU_DELETE:
                game_menu_open(game, MENU_SLOTS);
                menu->cursor = menu->delete_slot;
                break;
            case MENU_DIFFICULTY: game_menu_open(game, MENU_SLOTS); break;
            case MENU_QUIT:
                game_menu_open(game, menu->parent);
                menu->cursor = menu->dialog_cursor;
                break;
            case MENU_OPTIONS: game_menu_open(game, menu->parent); break;
            case MENU_RETRY: game_menu_open(game, MENU_PAUSE); break;
            case MENU_PAUSE: game_menu_open(game, menu->pause_origin); break;
            default: break;
        }
        return;
    }
    if (action == MENU_READY && menu->page == MENU_SLOTS && menu->cursor < GAME_SAVE_SLOTS &&
        (game->saves[menu->cursor].valid || game->save_errors[menu->cursor])) {
        menu->delete_slot = menu->cursor;
        game_menu_open(game, MENU_DELETE);
        return;
    }
    if (action != MENU_CONFIRM || !game_menu_enabled(game, menu->cursor)) return;
    int row = menu->cursor;
    switch (menu->page) {
        case MENU_MAIN:
            game_menu_open(game, row == 0 ? MENU_SLOTS : row == 1 ? MENU_OPTIONS : MENU_QUIT);
            break;
        case MENU_SLOTS:
            if (row == GAME_SAVE_SLOTS) game_menu_open(game, MENU_MAIN);
            else if (game->saves[row].valid || !game->save_errors[row]) {
                game->active_slot = row;
                if (game->saves[row].valid) game_transition(game, TRANSITION_CONTINUE, 0);
                else game_menu_open(game, MENU_DIFFICULTY);
            }
            break;
        case MENU_DELETE:
            if (row == 1) {
                int target = menu->delete_slot;
                game->saves[target] = (SavedGame){0};
                game->saves_dirty |= 1u << target;
                game->save_errors[target] = false;
            }
            game_menu_open(game, MENU_SLOTS);
            menu->cursor = menu->delete_slot;
            break;
        case MENU_DIFFICULTY:
            if (row == 3) game_menu_open(game, MENU_SLOTS);
            else {
                menu->difficulty = row;
                menu->pilot[0] = 0; menu->pilot[1] = 1;
                menu->ready[0] = menu->ready[1] = false;
                game_menu_open(game, MENU_PILOT);
            }
            break;
        case MENU_OPTIONS: if (row == OPTION_COUNT) game_menu_open(game, menu->parent); break;
        case MENU_QUIT:
            if (row == 0) {
                game_menu_open(game, menu->parent);
                menu->cursor = menu->dialog_cursor;
            }
            else if (menu->parent == MENU_MAIN) menu->quit = true;
            else game_transition(game, TRANSITION_MAIN, 0);
            break;
        case MENU_PAUSE:
            game_menu_open(game, row == 0 ? menu->pause_origin : row == 1 ? MENU_RETRY : row == 2 ? MENU_OPTIONS : row == 3 ? MENU_CONTROLLERS : MENU_QUIT);
            break;
        case MENU_RETRY: if (row == 0) game_menu_open(game, MENU_PAUSE); else game_transition(game, TRANSITION_RETRY, 0); break;
        case MENU_OVER:
            if (row == 0) game_transition(game, TRANSITION_RETRY, 0);
            else game_menu_open(game, MENU_QUIT);
            break;
        default: break;
    }
}
