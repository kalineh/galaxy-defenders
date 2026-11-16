#ifndef GALAXY_STAGE_H
#define GALAXY_STAGE_H

#include <stdbool.h>

#define STAGE_COUNT 19
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
    SceneryLayer layers[4];
} SceneryDefinition;

typedef struct { float x, y, vx, vy, target_x, target_y; } SceneryParticle;
extern const SceneryDefinition stage_scenery[14];

typedef struct {
    int index, end_countdown, music_event, music_tick, revision;
    float camera_x, camera_y, previous_line;
    bool stopped, health_bar;
    bool ignore_secrets;
    int secret_target, secret_countdown;
    float secret_x, secret_y;
    unsigned int scenery_random;
    SceneryParticle scenery[4][15];
    bool activated[STAGE_MAX_EVENTS];
    float building_health[STAGE_MAX_EVENTS];
} StageState;

#endif
