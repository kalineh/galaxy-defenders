#ifndef GALAXY_EQUIPMENT_H
#define GALAXY_EQUIPMENT_H

#include <stdbool.h>
#include "equipment_data.h"

#define WEAPON_EMITTERS 8
#define WEAPON_GROUPS 5
typedef struct {
    float reload, speed, damage, kickback, rotation, energy;
    float offset_x, offset_y, charge_speed, drain, random_rotation, random_reload;
    int auto_discharge;
    bool toggle, show_sidekick;
    int lifetime, bounces;
    float friction, spray_angle, start_rotation, fire_rotation_speed;
    float anti_camera_speed, scale_override, width, radius;
} WeaponEmitter;
typedef struct { int count; const WeaponEmitter *emitters; } WeaponLevel;
typedef struct {
    const char *name, *display_name, *sound;
    int price, level_count;
    WeaponLevel levels[8];
} WeaponDefinition;
typedef struct {
    const char *name, *icon;
    int price;
    float capacity, regen, on_collision, on_coin;
} Generator;
typedef struct {
    const char *name, *icon;
    int price;
    float capacity, drain, efficiency;
} Shield;
typedef struct {
    float cooldown, fire_rotation;
    int charge, overheat;
    bool toggled;
} WeaponState;

extern const WeaponDefinition game_weapons[WEAPON_COUNT];
extern const Generator game_generators[GENERATOR_COUNT];
extern const Shield game_shields[SHIELD_COUNT];
int equipment_weapon_price(int kind, int level);
int equipment_focus_weapon(int kind);
int equipment_weapon_category(int kind);
int equipment_find_weapon(const char *name);

#endif
