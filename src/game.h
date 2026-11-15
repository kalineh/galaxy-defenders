#ifndef GALAXY_GAME_H
#define GALAXY_GAME_H

#include <stdbool.h>
#include <stdint.h>
#include "movement.h"
#include "stage.h"

#define GAME_WIDTH 960
#define GAME_HEIGHT 1080
#define GAME_HZ 60
#define GAME_PLAYERS 2
#define GAME_ENEMIES 96
#define GAME_BULLETS 768
#define GAME_EFFECTS 8192
#define GAME_CHASSIS 9

typedef struct {
    const char *name;
    int price;
    float speed, friction, armor, collision_resistance;
} Chassis;

extern const Chassis game_chassis[GAME_CHASSIS];

typedef enum { MENU_NONE, MENU_MAIN, MENU_NEW_GAME, MENU_DIFFICULTY, MENU_PILOT,
    MENU_OPTIONS, MENU_QUIT, MENU_PAUSE, MENU_RETRY, MENU_OVER, MENU_CLEAR } MenuPage;
typedef enum { MENU_UP, MENU_DOWN, MENU_LEFT, MENU_RIGHT, MENU_CONFIRM, MENU_BACK } MenuAction;
typedef struct {
    MenuPage page, parent;
    int cursor, players, difficulty, pilot[2], options[3];
    bool ready[2], quit;
} GameMenu;

typedef struct {
    float x, y;
    bool fire, focus;
} PlayerInput;

typedef struct {
    bool joined, invincible;
    float x, y, vx, vy, health;
    int lives, invulnerable, score;
    int money, chassis, pilot, weapon_level;
    float energy, weapon_cooldown[2], friction;
    PlayerInput input;
} Player;

typedef enum { ENEMY_BALL, ENEMY_TRIANGLE, ENEMY_TURRET, ENEMY_BOSS,
    ENEMY_MINI_SQUARE, ENEMY_BIG_BALL, ENEMY_DOWN_TURRET, ENEMY_BOSS_CHUNK,
    ENEMY_KIND_COUNT } EnemyKind;

extern const char *enemy_names[ENEMY_KIND_COUNT];

typedef struct {
    bool active;
    EnemyKind kind;
    float x, y, vx, vy, health, radius;
    int age, cooldown;
    float health_max, rotation, base_y, move_from, move_to, aim;
    int parent, children[2], attack, shot_index, move_wait, move_frame;
    int coins;
    bool powerup, authored;
    MovementKind movement;
    float move_speed, move_transition;
} Enemy;

typedef struct {
    bool active;
    int owner; /* Player slot, or -1 for hostile shots. */
    float x, y, vx, vy, radius, damage;
} Bullet;

typedef struct {
    float x, y, vx, vy, scale, scale_delta, angle, spin;
    int life, kind;
    uint8_t r, g, b;
} Effect;

typedef enum { EFFECT_ENEMY_DEATH, EFFECT_ARMOR_HIT, EFFECT_PLAYER_DEATH,
    EFFECT_FOCUS, EFFECT_ENEMY_SHOT_HIT, EFFECT_LASER_HIT,
    EFFECT_BUILDING_SMALL, EFFECT_BUILDING_BIG } EffectKind;

typedef struct {
    Player players[GAME_PLAYERS];
    Enemy enemies[GAME_ENEMIES];
    Bullet bullets[GAME_BULLETS];
    Effect effects[GAME_EFFECTS];
    uint32_t random, seed;
    int tick, kills, building_kills, shots, hits, projectile_hits, deaths, dropped_spawns, effect_cursor;
    bool waves, paused, over, complete, boss_spawned, seeking;
    int difficulty;
    float scroll_speed;
    StageState stage;
    GameMenu menu;
} Game;

void game_reset(Game *game, uint32_t seed);
bool game_join(Game *game, int slot);
bool game_equip_chassis(Game *game, int slot, int chassis);
void game_menu_open(Game *game, MenuPage page);
void game_menu_input(Game *game, MenuAction action, int slot);
int game_menu_count(const Game *game);
const char *game_menu_label(const Game *game, int row);
bool game_menu_enabled(const Game *game, int row);
bool game_menu_small(const Game *game, int row);
void game_retry(Game *game);
bool game_upgrade_weapon(Game *game, int slot, int level);
void game_tick(Game *game);
int game_spawn_enemy(Game *game, EnemyKind kind, float x, float y);
int game_spawn_bullet(Game *game, int owner, float x, float y, float vx, float vy, float damage);
void game_damage_player(Game *game, int slot, float damage);
int game_enemy_count(const Game *game);
int game_bullet_count(const Game *game);
void game_effect(Game *game, EffectKind kind, float x, float y, int player, float vx, float vy);
uint32_t game_random(Game *game);
void enemy_update(Game *game, Enemy *enemy);
bool game_stage(Game *game, int index, float progress);
void game_stage_update(Game *game);
const char *game_music(const Game *game);

#endif
