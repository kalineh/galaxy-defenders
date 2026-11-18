#include "render_motion.h"
#include <math.h>
#include <string.h>

/* Capture just the visible transforms before a simulation tick. Reused slots
   and scene changes must appear at their new position immediately. */
void render_motion_capture(RenderMotion *previous, const Game *game) {
    previous->source = game;
    previous->tick = game->tick;
    previous->stage_revision = game->stage.revision;
    previous->stage_index = game->stage.index;
    previous->page = game->menu.page;
    previous->camera_x = game->stage.camera_x;
    previous->camera_y = game->stage.camera_y;
    memcpy(previous->dust,game->stage.scenery,sizeof(previous->dust));
    memcpy(previous->dust_count,game->stage.scenery_count,sizeof(previous->dust_count));
    previous->effect_cursor = game->effect_cursor;
    for (int i=0; i<GAME_PLAYERS; ++i) {
        const Player *p = &game->players[i];
        previous->players[i] = (RenderPose){p->x,p->y,0,p->joined && p->lives > 0 ? p->age : -1};
    }
    for (int i=0; i<GAME_ENEMIES; ++i) {
        const Enemy *e = &game->enemies[i];
        previous->enemies[i] = (RenderPose){e->x,e->y,e->rotation,e->active ? e->age : -1};
        previous->enemy_orbit[i] = e->orbit_angle;
        previous->enemy_aim[i] = e->aim;
    }
    for (int i=0; i<GAME_BULLETS; ++i) {
        const Bullet *b = &game->bullets[i];
        previous->bullets[i] = (RenderPose){b->x,b->y,b->rotation,b->active ? b->age : -1};
    }
    for (int i=0; i<GAME_PICKUPS; ++i) {
        const Pickup *p = &game->pickups[i];
        previous->pickups[i] = (RenderPose){p->x,p->y,0,p->active ? p->age : -1};
    }
}

/* Weight of the previous pose. Zero draws the authoritative state directly. */
float render_motion_weight(const RenderMotion *previous, const Game *game, float fraction) {
    if (previous->source != game || previous->tick + 1 != game->tick ||
        previous->stage_revision != game->stage.revision || previous->stage_index != game->stage.index ||
        previous->page != game->menu.page) return 0;
    return 1 - fminf(1,fmaxf(0,fraction));
}

float render_angle(float previous, float current, float weight) {
    float delta = previous-current;
    if (fabsf(delta) > 3.14159265f) delta = remainderf(delta,6.28318531f);
    return current + delta*weight;
}

RenderPose render_pose(RenderPose previous, int age, float x, float y, float angle, float weight) {
    /* A new pool occupant can already be one tick old when first drawn. */
    if (weight <= 0 || age <= 1 || previous.age != age-1) return (RenderPose){x,y,angle,age};
    return (RenderPose){x+(previous.x-x)*weight,y+(previous.y-y)*weight,
        render_angle(previous.angle,angle,weight),age};
}
