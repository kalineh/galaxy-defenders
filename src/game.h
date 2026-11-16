#ifndef GALAXY_GAME_H
#define GALAXY_GAME_H

#include <stdbool.h>
#include <stdint.h>
#include "movement.h"
#include "stage.h"
#include "equipment.h"
#include "effects_data.h"

#define GAME_WIDTH 960
#define GAME_HEIGHT 1080
#define GAME_HZ 60
#define GAME_PLAYERS 2
#define GAME_ENEMIES 256
#define GAME_BULLETS 768
#define GAME_EFFECTS 8192
#define GAME_PICKUPS 512
#define GAME_CHASSIS 9
#define PLAYER_ARMOR_RADIUS 16.0f
#define ENEMY_SHOT_RADIUS 2.0f

typedef struct {
    const char *name;
    int price;
    float speed, friction, armor, collision_resistance;
} Chassis;

extern const Chassis game_chassis[GAME_CHASSIS];

typedef enum { MENU_NONE, MENU_MAIN, MENU_NEW_GAME, MENU_DIFFICULTY, MENU_PILOT,
    MENU_OPTIONS, MENU_QUIT, MENU_PAUSE, MENU_RETRY, MENU_OVER, MENU_CLEAR, MENU_SHOP, MENU_FINISH, MENU_STAGE_SELECT } MenuPage;
typedef enum { TRANSITION_NONE, TRANSITION_NEW_GAME, TRANSITION_CONTINUE, TRANSITION_RETRY,
    TRANSITION_SHOP, TRANSITION_STAGE, TRANSITION_STAGE_SELECT, TRANSITION_MAIN, TRANSITION_FINISH } Transition;
typedef enum { MENU_UP, MENU_DOWN, MENU_LEFT, MENU_RIGHT, MENU_CONFIRM, MENU_BACK, MENU_READY } MenuAction;
typedef struct {
    MenuPage page, parent;
    int cursor, players, difficulty, pilot[2], options[3];
    bool ready[2], quit;
    int shop_category[2], shop_item[2], shop_level[2];
    bool shop_items[2];
    int frame;
    int next_stage, shop_fly_frames[GAME_PLAYERS];
} GameMenu;

typedef struct {
    float x, y;
    bool fire, focus;
    bool sidekick_left, sidekick_right;
} PlayerInput;

typedef struct {
    bool joined, invincible;
    float x, y, vx, vy, health;
    int lives, invulnerable, score, age, shield_hit_frames;
    int money, chassis, pilot, weapon_level, powerup_levels;
    int primary, secondary, secondary_level, sidekick, generator, shield;
    float shield_health;
    WeaponState weapons[WEAPON_GROUPS][WEAPON_EMITTERS];
    float energy, weapon_cooldown[2], friction;
    PlayerInput input;
} Player;

typedef struct {
    int pilot, chassis, primary, level, secondary, secondary_level, sidekick, generator, shield, money;
} SavedPlayer;

typedef struct {
    bool valid, shop, cleared;
    int stage, difficulty, players;
    uint32_t seed;
    int stage_scores[12], stage_medals[12];
    SavedPlayer player[GAME_PLAYERS];
} SavedGame;

typedef enum { ENEMY_BALL, ENEMY_TRIANGLE, ENEMY_TURRET, ENEMY_BOSS,
    ENEMY_MINI_SQUARE, ENEMY_BIG_BALL, ENEMY_DOWN_TURRET, ENEMY_BOSS_CHUNK,
    ENEMY_SHOOT_BALL, ENEMY_BOSS2,
    ENEMY_AIRSHIP, ENEMY_BEAMER, ENEMY_BLACK_HOLE, ENEMY_CANNON, ENEMY_CUTTER, ENEMY_CUTTER_BALL,
    ENEMY_DOUBLE_TURRET, ENEMY_FENCE, ENEMY_FENCE_BEAM, ENEMY_GLOB, ENEMY_MISSILE_POD, ENEMY_MISSILE_STORM,
    ENEMY_MISSILE_TRIPLE, ENEMY_PYRAMID, ENEMY_RAID_TURRET, ENEMY_ROTATE_TURRET, ENEMY_SPIKE,
    ENEMY_SPLITTER, ENEMY_SPLITTER_FRAGMENT, ENEMY_SWIRL, ENEMY_SWIRL_BALL, ENEMY_TELEPORTER,
    ENEMY_TRIPLE_SHOT, ENEMY_BONUS_SHIP,
    ENEMY_BOSS3, ENEMY_BOSS4, ENEMY_BOSS5, ENEMY_BOSS6, ENEMY_BOSS7, ENEMY_BOSS8,
    ENEMY_BOSS9, ENEMY_BOSS10, ENEMY_BOSS11, ENEMY_BOSS12,
    ENEMY_BOSS3_CHUNK, ENEMY_BOSS4_CHUNK, ENEMY_BOSS5_CHUNK, ENEMY_BOSS6_SPHERE,
    ENEMY_BOSS7_CANNON_BIG, ENEMY_BOSS7_CANNON_SMALL, ENEMY_BOSS8_COLLISION,
    ENEMY_BOSS9_BALL, ENEMY_BOSS9_BAR, ENEMY_BOSS10_BALL, ENEMY_BOSS10_BLOCK,
    ENEMY_BOSS11_HAND, ENEMY_BOSS11_ARM, ENEMY_BOSS12_EYE, ENEMY_BOSS12_ARM,
    ENEMY_KIND_COUNT } EnemyKind;

enum { ENEMY_LAYER_ENTITY = 3, ENEMY_LAYER_PLAYER_OVERLAY, ENEMY_LAYER_WEAPON_OVERLAY };
extern const char *enemy_names[ENEMY_KIND_COUNT];
const char *enemy_corpse_name(EnemyKind kind);
int enemy_draw_layer(EnemyKind kind);
typedef enum { HOSTILE_SHOT, HOSTILE_LASER, HOSTILE_MISSILE, HOSTILE_CANNON, HOSTILE_PELLET, HOSTILE_MINI, HOSTILE_COUNT } HostileKind;

typedef struct {
    bool active;
    EnemyKind kind;
    float x, y, vx, vy, health, radius;
    int age, cooldown;
    float health_max, rotation, base_y, move_from, move_to, aim;
    int parent, children[8], attack, shot_index, move_wait, move_frame, move_phase, move_duration;
    int target, timer, invisible, hit_count;
    float spin, center_spin;
    float anchor_x, anchor_y, offset_x, offset_y, damage_taken;
    float orbit_angle, orbit_radius, collision_x, collision_y;
    int phase, aux_cooldown, aux_index;
    bool intangible;
    float box_width, box_height;
    int coins;
    bool powerup, authored;
    MovementKind movement;
    float move_speed, move_transition;
} Enemy;

typedef struct {
    bool active;
    int owner; /* Player slot, or -1 for hostile shots. */
    float x, y, vx, vy, radius, damage;
    int weapon, age, lifetime, bounces, target, ignore_enemy, group, emitter, hostile_kind, health, collide_wait;
    float speed, base_damage, rotation, base_rotation, spin, curve_speed, curve_magnitude, curve_offset;
    float direction_x, direction_y, friction, scale, offset_x, offset_y, width;
    bool splash, was_hit;
} Bullet;

typedef struct {
    float x, y, vx, vy, scale, scale_delta, angle, spin, alpha, alpha_delta;
    int life, kind;
    uint8_t r, g, b;
} Effect;

typedef enum { EFFECT_ENEMY_DEATH, EFFECT_ARMOR_HIT, EFFECT_PLAYER_DEATH,
    EFFECT_FOCUS, EFFECT_ENEMY_SHOT_HIT, EFFECT_LASER_HIT,
    EFFECT_BUILDING_SMALL, EFFECT_BUILDING_BIG, EFFECT_SHIELD_HIT } EffectKind;

typedef struct {
    bool active, magnet;
    int kind, age; /* 0: coin, 1: big coin, 2: primary upgrade. */
    float x, y, vx, vy, friction, force;
} Pickup;

typedef struct {
    int values[56], next, next_pair;
} GameRandom;

typedef struct {
    bool active, boss_spawned;
    Player players[GAME_PLAYERS];
    Enemy enemies[GAME_ENEMIES];
    Bullet bullets[GAME_BULLETS];
    Effect effects[GAME_EFFECTS];
    Pickup pickups[GAME_PICKUPS];
    StageState stage;
    GameRandom random;
    int tick;
    float scroll_speed;
} SecretReturn;

typedef struct {
    Player players[GAME_PLAYERS];
    Enemy enemies[GAME_ENEMIES];
    Bullet bullets[GAME_BULLETS];
    Effect effects[GAME_EFFECTS];
    Pickup pickups[GAME_PICKUPS];
    GameRandom random;
    uint32_t seed;
    int armor_hits, shield_hits;
    int tick, kills, building_kills, shots, hits, projectile_hits, deaths, dropped_spawns, effect_cursor;
    int pickups_collected;
    int weapon_shots[WEAPON_COUNT], impact_sounds[4], enemy_shots[2], enemy_deaths[3], overheats;
    bool waves, paused, over, complete, boss_spawned, seeking;
    int difficulty, over_frames, secret_finish_frames, secret_fade_frames;
    Transition transition;
    int transition_frames, transition_stage;
    float scroll_speed;
    StageState stage;
    GameMenu menu;
    SecretReturn secret_return;
    SavedGame saves[2];
    bool saves_dirty, cleared_game, clear_exit;
} Game;

void game_reset(Game *game, uint32_t seed);
void game_reset_session(Game *game, uint32_t seed);
void game_checkpoint(Game *game, bool shop);
void game_stats_init(Game *game);
int game_stats_percent(int count, int total);
int game_stage_medal(const Game *game);
void game_record_stage(Game *game);
bool game_continue(Game *game, int players);
bool game_save_read(SavedGame *save, const char *path);
bool game_save_write(const SavedGame *save, const char *path);
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
void game_drop_pickups(Game *game, float x, float y, int coins, bool powerup, bool combine);
int game_health_score(const Game *game, float health, float multiplier);
int game_enemy_count(const Game *game);
int game_bullet_count(const Game *game);
void game_effect(Game *game, EffectKind kind, float x, float y, int player, float vx, float vy);
void game_random_init(GameRandom *random, uint32_t seed);
uint32_t game_random_sample(GameRandom *random);
uint32_t game_random(Game *game);
float game_random_float(Game *game);
float game_random_signed(Game *game);
int game_random_range(Game *game, int maximum);
void enemy_update(Game *game, Enemy *enemy);
void enemy_init_extended(Game *game, Enemy *enemy);
void enemy_update_extended(Game *game, Enemy *enemy);
void enemy_die(Game *game, Enemy *enemy, int owner);
void enemy_take_damage(Game *game, Enemy *enemy, float damage, int owner);
/* color: -1 authored, 0/1 player, 2 orange, 3 gray, 4 building gray, 5 building cream. */
void game_particle(Game *game, ParticleKind kind, float x, float y, int color, float scale, float vx, float vy);
bool enemy_is_boss(EnemyKind kind);
void boss_init(Game *game, Enemy *enemy);
void boss_update_all(Game *game, Enemy *enemy);
bool boss_damage(Game *game, Enemy *enemy, float damage, int owner);
int enemy_shot(Game *game, const Enemy *enemy, int kind, float x, float y, float angle, float speed, float damage);
bool game_stage(Game *game, int index, float progress);
void game_stage_update(Game *game);
void game_secret_enter(Game *game, int index);
void game_secret_return(Game *game);
void game_scenery_init(Game *game);
void game_scenery_update(Game *game);
const char *game_music(const Game *game);
void game_shop_open(Game *game);
void game_shop_update(Game *game);
void game_transition(Game *game, Transition transition, int stage);
bool game_transition_update(Game *game);
void game_shop_preview_update(Game *preview, const Game *game);
void game_preview_tick(Game *preview);
void game_shop_input(Game *game, MenuAction action, int slot);
int game_shop_cost(const Game *game, int slot);
bool game_shop_buy(Game *game, int slot);
bool game_shop_chassis_available(const Game *game, int slot, int chassis);
int game_shop_stock(const Game *game, int slot, int category, int items[WEAPON_COUNT + 1]);
int game_shop_equipped(const Player *player, int category);
bool game_shop_item_available(int category, int item);
void game_update_weapons(Game *game, int slot);
void game_update_projectile(Game *game, Bullet *bullet);
void game_projectile_hit(Game *game, Bullet *bullet, int enemy);

#endif
