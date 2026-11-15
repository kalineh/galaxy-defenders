#include "game.h"
#include <string.h>

bool game_shop_chassis_available(const Game *game, int slot, int chassis) {
    if (slot < 0 || slot >= GAME_PLAYERS || chassis < 0 || chassis >= GAME_CHASSIS) return false;
    return chassis == game->players[slot].chassis || chassis == 0 || chassis == 2 || chassis == 3 ||
        (game->stage.index == 0 && chassis == 1);
}

static int laser_total_price(int level) {
    int total = 0;
    for (int i = 0; i <= level; ++i) {
        int price = 650 + 650 * i * i * 3 / 4;
        total += price - price % 100;
    }
    return total;
}

int game_shop_cost(const Game *game, int slot) {
    if (slot < 0 || slot >= GAME_PLAYERS || !game->players[slot].joined) return 0;
    const Player *p = &game->players[slot];
    int item = game->menu.shop_item[slot];
    /* ShopMenu.cs refunds the full currently equipped item's price. */
    if (game->menu.shop_category[slot] == 0 && item >= 0 && item < GAME_CHASSIS)
        return game_chassis[item].price - game_chassis[p->chassis].price;
    if (game->menu.shop_category[slot] == 3 && item >= 0 && item < 8)
        return laser_total_price(item) - laser_total_price(p->weapon_level);
    return 0;
}

bool game_shop_buy(Game *game, int slot) {
    if (slot < 0 || slot >= GAME_PLAYERS || game->menu.page != MENU_SHOP || !game->players[slot].joined ||
        !game->menu.shop_items[slot] || game->menu.ready[slot]) return false;
    Player *p = &game->players[slot];
    int category = game->menu.shop_category[slot], item = game->menu.shop_item[slot];
    if (category != 0 && category != 3) return false;
    if (category == 0 && !game_shop_chassis_available(game, slot, item)) return false;
    int cost = game_shop_cost(game, slot);
    if (cost > p->money) return false;
    bool equipped = category == 0 ? game_equip_chassis(game, slot, item) : game_upgrade_weapon(game, slot, item);
    if (!equipped) return false;
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
                p->energy = 3;
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
        if (action == MENU_CONFIRM && (*category == 0 || *category == 3)) {
            menu->shop_items[slot] = true;
            *item = *category == 0 ? game->players[slot].chassis : game->players[slot].weapon_level;
        }
    } else {
        bool decrease = *category == 0 ? action == MENU_UP : action == MENU_LEFT;
        bool increase = *category == 0 ? action == MENU_DOWN : action == MENU_RIGHT;
        int maximum = *category == 0 ? GAME_CHASSIS - 1 : 7;
        if (decrease || increase) {
            int step = decrease ? -1 : 1, candidate = *item + step;
            while (*category == 0 && candidate >= 0 && candidate <= maximum &&
                   !game_shop_chassis_available(game, slot, candidate)) candidate += step;
            if (candidate >= 0 && candidate <= maximum) *item = candidate;
        }
        if (action == MENU_CONFIRM) game_shop_buy(game, slot);
    }
}
