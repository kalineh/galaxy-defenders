#include "movement.h"
#include <math.h>
#include <string.h>

const char *movement_names[MOVE_COUNT] = {
    "None", "IgnoreCamera", "Sin", "SinHorizontal", "Up", "Down", "Right", "Left",
    "ToUp", "DownWaitUp", "DownWaitDown", "DownWait", "DownLeft", "DownRight"
};

int movement_find(const char *name) {
    if (!name) return -1;
    for (int i = 0; i < MOVE_COUNT; ++i) if (!strcmp(name, movement_names[i])) return i;
    return -1;
}

void movement_velocity(MovementKind kind, int age, float speed, float transition,
                       float *vx, float *vy) {
    /* MoverPresets.cs uses a fixed -2.5 anti-camera term, even on fast stages. */
    float x = 0, y = 0, seconds = age / 60.0f, rate = 1;
    switch (kind) {
        case MOVE_NONE: return;
        case MOVE_IGNORE_CAMERA: return; /* Position compensation belongs to the caller. */
        case MOVE_SIN: x = cosf(seconds * transition) * speed; y = speed; break;
        case MOVE_SIN_HORIZONTAL: x = speed; y = cosf(seconds * transition) * speed; break;
        case MOVE_UP: y = -speed - 2.5f; break;
        case MOVE_DOWN: y = speed - 2.5f; break;
        case MOVE_RIGHT: x = speed; break;
        case MOVE_LEFT: x = -speed; break;
        case MOVE_TO_UP:
        case MOVE_DOWN_WAIT_UP:
        case MOVE_DOWN_WAIT_DOWN:
        case MOVE_DOWN_WAIT:
            rate = 0.02f;
            if (seconds <= transition) y = speed;
            else if (kind == MOVE_TO_UP) y = -speed;
            else if (seconds > transition * 1.5f) {
                if (kind == MOVE_DOWN_WAIT_UP) y = -speed;
                if (kind == MOVE_DOWN_WAIT_DOWN) y = speed;
            }
            y -= 2.5f;
            break;
        case MOVE_DOWN_LEFT:
        case MOVE_DOWN_RIGHT:
            rate = 0.01f;
            if (seconds <= transition * 0.5f) y = speed;
            else x = kind == MOVE_DOWN_LEFT ? -speed : speed;
            y -= 2.5f;
            break;
        default: return;
    }
    if (age == 0) { *vx = x; *vy = y; }
    else { *vx += (x - *vx) * rate; *vy += (y - *vy) * rate; }
}
