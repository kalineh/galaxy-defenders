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

typedef struct {
    float x, y;
    bool fire, focus;
} PlayerInput;

typedef struct {
    bool joined;
    float x, y, vx, vy, health;
    int lives, cooldown, invulnerable, score;
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
    EFFECT_FOCUS, EFFECT_ENEMY_SHOT_HIT } EffectKind;

typedef struct {
    Player players[GAME_PLAYERS];
    Enemy enemies[GAME_ENEMIES];
    Bullet bullets[GAME_BULLETS];
    Effect effects[GAME_EFFECTS];
    uint32_t random;
    int tick, kills, shots, hits, projectile_hits, deaths, dropped_spawns, effect_cursor;
    bool waves, paused, over, complete, boss_spawned;
} Game;

void game_reset(Game *game, uint32_t seed);
bool game_join(Game *game, int slot);
void game_tick(Game *game);
int game_spawn_enemy(Game *game, EnemyKind kind, float x, float y);
int game_spawn_bullet(Game *game, int owner, float x, float y, float vx, float vy, float damage);
void game_damage_player(Game *game, int slot, float damage);
int game_enemy_count(const Game *game);
int game_bullet_count(const Game *game);
void game_effect(Game *game, EffectKind kind, float x, float y, int player, float vx, float vy);

#endif
