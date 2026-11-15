#include "equipment.h"
#include <string.h>
#include "equipment_data.inc"

int equipment_weapon_price(int kind, int level) {
    if (kind < 0 || kind >= WEAPON_COUNT || level < 0 || level >= game_weapons[kind].level_count) return 0;
    int total = 0, base = game_weapons[kind].price;
    for (int i = 0; i <= level; ++i) {
        int price = base + base * i * i * 3 / 4;
        total += price - price % 100;
    }
    return total;
}

int equipment_focus_weapon(int kind) {
    switch (kind) {
        case WEAPON_FRONT_LASER: case WEAPON_SPREAD_LASER: case WEAPON_PLASMA:
        case WEAPON_FLAME: case WEAPON_LIGHTNING: case WEAPON_BEAM: return kind + 1;
        default: return kind;
    }
}

int equipment_weapon_category(int kind) {
    switch (kind) {
        case WEAPON_FRONT_LASER: case WEAPON_SPREAD_LASER: case WEAPON_PLASMA:
        case WEAPON_FLAME: case WEAPON_LIGHTNING: case WEAPON_BEAM: return 3;
        case WEAPON_VULCAN: case WEAPON_BOMBLET: case WEAPON_MISSILE:
        case WEAPON_SEEK_BOMB: case WEAPON_BOOMERANG: case WEAPON_DRUNK_MISSILE: return 4;
        case WEAPON_MINI_SHOT: case WEAPON_CHARGE_SHOT: case WEAPON_BLADE:
        case WEAPON_MISSILE_VOLLEY: case WEAPON_MISSILE_LAUNCHER: case WEAPON_LASER_SHOT: return 5;
        default: return -1;
    }
}

int equipment_find_weapon(const char *name) {
    for (int i = 0; i < WEAPON_COUNT; ++i) if (!strcmp(name, game_weapons[i].name)) return i;
    return -1;
}
