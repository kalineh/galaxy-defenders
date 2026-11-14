#ifndef GALAXY_GAME_H
#define GALAXY_GAME_H

#include <stdbool.h>
#include <stdint.h>

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

typedef enum { ENEMY_BALL, ENEMY_TRIANGLE, ENEMY_TURRET, ENEMY_BOSS } EnemyKind;

typedef struct {
    bool active;
    EnemyKind kind;
    float x, y, vx, vy, health, radius;
    int age, cooldown;
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
    EFFECT_FOCUS, EFFECT_ENEMY_SHOT_HIT, EFFECT_LASER_HIT } EffectKind;

typedef struct {
    Player players[GAME_PLAYERS];
    Enemy enemies[GAME_ENEMIES];
    Bullet bullets[GAME_BULLETS];
    Effect effects[GAME_EFFECTS];
    uint32_t random;
    int tick, kills, shots, hits, projectile_hits, deaths, dropped_spawns, effect_cursor;
    bool waves, paused, over, complete, boss_spawned;
    int difficulty;
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

#endif
