#ifndef GALAXY_STAGE_H
#define GALAXY_STAGE_H

#include <stdbool.h>

#define STAGE_CAMPAIGN_COUNT 19
#define STAGE_COUNT 31
#define SCENERY_LAYERS 6
#define STAGE_MAX_EVENTS 512

typedef enum {
    STAGE_DECORATION, STAGE_BUILDING, STAGE_ENEMY, STAGE_CAMERA_STOP,
    STAGE_FINISH, STAGE_MUSIC, STAGE_HEALTH_BAR, STAGE_SECRET
} StageEventKind;

typedef struct {
    StageEventKind kind;
    float x, y;
    const char *name, *mover;
    float spawn_x, spawn_y, speed, transition, rotation, depth;
    int coins;
    bool powerup, no_coins, expired;
    int width, height;
} StageEvent;

typedef struct {
    const char *name, *display_name, *background, *foreground, *music;
    float scroll_speed, length;
    const StageEvent *events;
    int event_count;
} StageDefinition;

extern const StageDefinition stage_definitions[STAGE_COUNT];

typedef struct {
    const char *texture;
    float scale, vx, vy, random_lerp;
    int count;
    bool foreground, stars;
} SceneryLayer;

typedef struct {
    unsigned char top[3], bottom[3];
    SceneryLayer layers[SCENERY_LAYERS];
} SceneryDefinition;

typedef struct { float x, y, vx, vy, target_x, target_y; } SceneryParticle;
typedef struct { bool active; int kind; float x, y; } EnemyCorpse;
extern const SceneryDefinition stage_scenery[20];
int game_scenery_index(int stage);

typedef struct {
    int coins_total, coins_collected, enemy_total, enemy_kills, building_total, building_kills;
    int starting_money;
    bool recorded;
} StageStats;

typedef struct {
    StageStats stats;
    int index, end_countdown, music_event, music_tick, revision;
    float camera_x, camera_y, previous_line;
    bool stopped, health_bar;
    bool ignore_secrets;
    int secret_target, secret_countdown;
    float secret_x, secret_y;
    int scenery_count[SCENERY_LAYERS];
    SceneryParticle scenery[SCENERY_LAYERS][15];
    EnemyCorpse corpses[STAGE_MAX_EVENTS];
    bool activated[STAGE_MAX_EVENTS];
    float building_health[STAGE_MAX_EVENTS];
} StageState;

#endif
