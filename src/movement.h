#ifndef GALAXY_MOVEMENT_H
#define GALAXY_MOVEMENT_H

typedef enum {
    MOVE_NONE, MOVE_IGNORE_CAMERA, MOVE_SIN, MOVE_SIN_HORIZONTAL,
    MOVE_UP, MOVE_DOWN, MOVE_RIGHT, MOVE_LEFT, MOVE_TO_UP,
    MOVE_DOWN_WAIT_UP, MOVE_DOWN_WAIT_DOWN, MOVE_DOWN_WAIT,
    MOVE_DOWN_LEFT, MOVE_DOWN_RIGHT, MOVE_COUNT
} MovementKind;

extern const char *movement_names[MOVE_COUNT];
int movement_find(const char *name);
/* World-space velocity, before camera displacement. Age starts at zero. */
void movement_velocity(MovementKind kind, int age, float speed, float transition,
                       float scroll_speed, float *vx, float *vy);

#endif
