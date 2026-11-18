#ifndef GALAXY_RENDER_MOTION_H
#define GALAXY_RENDER_MOTION_H

#include "game.h"

typedef struct { float x, y, angle; int age; } RenderPose;
typedef struct { int hz; double offset; } RenderClock;
typedef struct {
    const Game *source;
    int tick, stage_revision, stage_index;
    MenuPage page;
    float camera_x, camera_y;
    RenderPose players[GAME_PLAYERS], enemies[GAME_ENEMIES], bullets[GAME_BULLETS], pickups[GAME_PICKUPS];
    float enemy_orbit[GAME_ENEMIES], enemy_aim[GAME_ENEMIES];
    SceneryParticle dust[4][15];
    int dust_count[4], effect_cursor;
} RenderMotion;

void render_motion_capture(RenderMotion *previous, const Game *game);
double render_clock_offset(RenderClock *clock, double elapsed, int hz);
float render_motion_weight(const RenderMotion *previous, const Game *game, float fraction);
RenderPose render_pose(RenderPose previous, int age, float x, float y, float angle, float weight);
float render_angle(float previous, float current, float weight);

#endif
