#ifndef GALAXY_STAGE_H
#define GALAXY_STAGE_H

#include <stdbool.h>

#define STAGE_COUNT 19

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
} StageEvent;

typedef struct {
    const char *name, *display_name, *background, *foreground, *music;
    float scroll_speed, length;
    const StageEvent *events;
    int event_count;
} StageDefinition;

extern const StageDefinition stage_definitions[STAGE_COUNT];

#endif
