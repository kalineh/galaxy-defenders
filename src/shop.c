#include "game.h"
#include <string.h>

bool game_shop_chassis_available(const Game *game, int slot, int chassis) {
    (void)game;
    return slot >= 0 && slot < GAME_PLAYERS && chassis >= 0 && chassis < GAME_CHASSIS;
}

int game_shop_equipped(const Player *p, int category) {
    switch (category) {
        case 0: return p->chassis;
        case 1: return p->generator;
        case 2: return p->shield;
        case 3: return p->primary;
        case 4: return p->secondary;
        case 5: return p->sidekick;
        default: return -1;
    }
}

bool game_shop_item_available(int category, int item) {
    if (category == 0) return item >= 0 && item < GAME_CHASSIS;
    if (category == 1) return item >= 0 && item < GENERATOR_COUNT;
    if (category == 2) return item >= 0 && item < SHIELD_COUNT;
    if (category == 4 || category == 5) if (item == -1) return true;
    return item >= 0 && item < WEAPON_COUNT && equipment_weapon_category(item) == category;
}

static int item_price(int category, int item, int level) {
    if (item < 0) return 0;
    if (category == 0) return game_chassis[item].price;
    if (category == 1) return game_generators[item].price;
    if (category == 2) return game_shields[item].price;
    return equipment_weapon_price(item, level);
}

int game_shop_cost(const Game *game, int slot) {
    if (slot < 0 || slot >= GAME_PLAYERS || !game->players[slot].joined) return 0;
    const Player *p = &game->players[slot];
    int category = game->menu.shop_category[slot], item = game->menu.shop_item[slot];
    if (!game_shop_item_available(category, item)) return 0;
    int old_level = category == 3 ? p->weapon_level : category == 4 ? p->secondary_level : 0;
    return item_price(category, item, game->menu.shop_level[slot]) -
        item_price(category, game_shop_equipped(p, category), old_level);
}

bool game_shop_buy(Game *game, int slot) {
    if (slot < 0 || slot >= GAME_PLAYERS || game->menu.page != MENU_SHOP || !game->players[slot].joined ||
        !game->menu.shop_items[slot] || game->menu.ready[slot]) return false;
    Player *p = &game->players[slot];
    int category = game->menu.shop_category[slot], item = game->menu.shop_item[slot], level = game->menu.shop_level[slot];
    if (!game_shop_item_available(category, item)) return false;
    if (category >= 3 && item >= 0 && (level < 0 || level >= game_weapons[item].level_count)) return false;
    int cost = game_shop_cost(game, slot);
    if (cost > p->money) return false;
    switch (category) {
        case 0: if (!game_equip_chassis(game, slot, item)) return false; break;
        case 1: p->generator = item; p->energy = game_generators[item].capacity; break;
        case 2: p->shield = item; p->shield_health = game_shields[item].capacity; break;
        case 3: p->primary = item; p->weapon_level = level; break;
        case 4: p->secondary = item; p->secondary_level = level; break;
        case 5: p->sidekick = item; break;
        default: return false;
    }
    memset(p->weapons, 0, sizeof(p->weapons));
    p->money -= cost;
    return true;
}

void game_shop_open(Game *game) {
    /* World.SetScoreSaveData transfers score exactly once before the shop. */
    for (int i = 0; i < GAME_PLAYERS; ++i) {
        Player *p = &game->players[i];
        if (!p->joined) continue;
        p->money += p->score;
        p->score = 0;
        p->lives = 3;
        p->health = game_chassis[p->chassis].armor;
        game->menu.ready[i] = false;
        game->menu.shop_category[i] = 0;
        game->menu.shop_item[i] = p->chassis;
        game->menu.shop_items[i] = false;
    }
    game_menu_open(game, MENU_SHOP);
}

void game_shop_input(Game *game, MenuAction action, int slot) {
    if (slot < 0 || slot >= GAME_PLAYERS || !game->players[slot].joined) return;
    GameMenu *menu = &game->menu;
    if (action == MENU_BACK) {
        if (menu->ready[slot]) menu->ready[slot] = false;
        else menu->shop_items[slot] = false;
        return;
    }
    if (action == MENU_READY) {
        if (game->stage.index + 1 > 1) return; /* No substitute stages. */
        menu->ready[slot] = true;
        bool all_ready = true;
        for (int i = 0; i < GAME_PLAYERS; ++i) if (game->players[i].joined && !menu->ready[i]) all_ready = false;
        if (all_ready) {
            int next = game->stage.index + 1;
            game_stage(game, next, 0);
            for (int i = 0; i < GAME_PLAYERS; ++i) {
                Player *p = &game->players[i];
                if (!p->joined) continue;
                p->x = i ? 560 : 400; p->y = 880;
                p->vx = p->vy = 0;
                p->energy = game_generators[p->generator].capacity;
                p->shield_health = p->shield >= 0 ? game_shields[p->shield].capacity : 0;
                memset(p->weapons, 0, sizeof(p->weapons));
                p->weapon_cooldown[0] = p->weapon_cooldown[1] = 0;
            }
        }
        return;
    }
    if (menu->ready[slot]) return;
    int *category = &menu->shop_category[slot], *item = &menu->shop_item[slot];
    if (!menu->shop_items[slot]) {
        if (action == MENU_UP && *category > 0) --*category;
        if (action == MENU_DOWN && *category < 5) ++*category;
        if (action == MENU_CONFIRM) {
            menu->shop_items[slot] = true;
            *item = game_shop_equipped(&game->players[slot], *category);
            if (!game_shop_item_available(*category, *item)) *item = 0;
            menu->shop_level[slot] = *category == 3 ? game->players[slot].weapon_level :
                *category == 4 ? game->players[slot].secondary_level : 0;
        }
    } else {
        if (action == MENU_UP || action == MENU_DOWN) {
            int step = action == MENU_UP ? -1 : 1, candidate = *item + step;
            int maximum = *category == 0 ? GAME_CHASSIS : *category == 1 ? GENERATOR_COUNT :
                *category == 2 ? SHIELD_COUNT : WEAPON_COUNT;
            while (candidate >= -1 && candidate < maximum && !game_shop_item_available(*category, candidate)) candidate += step;
            if (candidate >= -1 && candidate < maximum) {
                *item = candidate;
                const Player *p = &game->players[slot];
                menu->shop_level[slot] = candidate == game_shop_equipped(p, *category) ?
                    (*category == 3 ? p->weapon_level : *category == 4 ? p->secondary_level : 0) : 0;
            }
        }
        if (*category >= 3 && *item >= 0) {
            int *level = &menu->shop_level[slot];
            if (action == MENU_LEFT && *level > 0) --*level;
            if (action == MENU_RIGHT && *level + 1 < game_weapons[*item].level_count) ++*level;
        }
        if (action == MENU_CONFIRM) game_shop_buy(game, slot);
    }
}
