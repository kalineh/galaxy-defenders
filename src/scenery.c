#include "game.h"
#include <math.h>

/* SceneryPresets.cs: texture, scale, velocity, wandering, count, foreground, stars. */
const SceneryDefinition stage_scenery[14] = {
    {{92,120,196},{92,120,196},{{"Cloud",1,0,6,0,10,0,0},{"Cloud",1,0,9,0,10,0,0}}},
    {{5,5,98},{5,5,98},{{"Water",1,0,1,0,10,0,0},{"Water",1,0,1,0,10,0,0},{"Water",1,0,1,0,10,0,0},{"Cloud",1,0,9,0,10,0,0}}},
    {{27,40,87},{47,76,87},{{"Droplet",1,0,6,0,10,0,0},{"Droplet",1,0,7,0,10,0,0}}},
    {{0,0,0},{0,0,0},{{"Star",1,0,6,0,10,0,1},{"Star",1,0,9,0,10,0,1}}},
    {{8,8,42},{8,8,42},{{"BoxCloud",1.2f,2,3,0,10,0,0},{"BoxCloud",0.8f,-2,3,0,10,0,0}}},
    {{0,0,0},{0,0,0},{{"Vertical",1,0,6,0,10,0,1},{"Vertical",1,0,9,0,10,0,1},{"Vertical",1,0,12,0,10,0,1}}},
    {{35,51,51},{35,51,51},{{"Snow",1,3,11,0,12,1,0},{"Snow",0.8f,2,14,0,12,1,0},{"Snow",0.8f,3,12,2,12,1,0}}},
    {{98,110,186},{98,110,186},{{"SpaceCloud1",1.2f,0,6,0,3,1,0},{"SpaceCloud2",0.7f,0,9,0,3,1,0},{"SpaceCloud3",0.8f,0,9,0,3,1,0}}},
    {{48,48,48},{48,48,48},{{"Vertical",1,0,6,0,10,0,1},{"Vertical",1,0,9,0,10,0,1},{"Vertical",1,0,12,0,10,0,1}}},
    {{5,5,98},{5,5,98},{{"Water",1,0,1,0,10,0,0},{"Water",1,0,1,0,10,0,0},{"Water",1,0,1,0,10,0,0},{"Droplet",0.6f,0,12,0,7,1,0}}},
    {{92,120,196},{92,120,196},{{"Snow",1,3,11,0,8,1,0},{"Snow",0.8f,2,14,0,8,1,0},{"Snow",0.8f,3,12,2,8,1,0}}},
    {{0,0,0},{0,0,0},{{"Star",1.2f,0,6,3,12,0,0},{"Star",0.8f,0,9,2,15,0,0}}},
    {{46,46,170},{46,46,170},{{"Snow",1,0,8,0,10,0,1},{"Snow",1,0,11,0,10,0,1}}},
    {{0,0,0},{0,0,0},{{"Star",1,0,6,0,10,0,1},{"Star",1,0,9,0,10,0,1}}}
};

static float random_unit(StageState *state) {
    state->scenery_random = state->scenery_random * 1664525u + 1013904223u;
    return (state->scenery_random >> 8) / 16777216.0f;
}

static void particle_reset(StageState *state, SceneryParticle *p, const SceneryLayer *layer, bool initial) {
    p->x = random_unit(state) * GAME_WIDTH;
    p->y = initial ? random_unit(state) * GAME_HEIGHT : layer->stars ? -100 : -32;
    p->vx = layer->vx; p->vy = layer->vy;
    float angle = (random_unit(state) * 6.283185307f - 1.570796327f) * layer->random_lerp;
    p->target_x = layer->vx * cosf(angle) - layer->vy * sinf(angle);
    p->target_y = layer->vx * sinf(angle) + layer->vy * cosf(angle);
}

void game_scenery_init(Game *game) {
    StageState *state = &game->stage;
    state->scenery_random = game->seed ^ (unsigned int)(state->index + 1) * 2654435761u;
    const SceneryDefinition *scene = &stage_scenery[state->index < 13 ? state->index : 13];
    for (int layer = 0; layer < 4; ++layer)
        for (int i = 0; i < scene->layers[layer].count; ++i)
            particle_reset(state, &state->scenery[layer][i], &scene->layers[layer], true);
}

void game_scenery_update(Game *game) {
    StageState *state = &game->stage;
    const SceneryDefinition *scene = &stage_scenery[state->index < 13 ? state->index : 13];
    for (int layer = 0; layer < 4; ++layer) for (int i = 0; i < scene->layers[layer].count; ++i) {
        const SceneryLayer *definition = &scene->layers[layer];
        SceneryParticle *p = &state->scenery[layer][i];
        if (definition->stars ? p->y > GAME_HEIGHT + 1 :
            p->x < -64 || p->x > GAME_WIDTH + 64 || p->y < -64 || p->y > GAME_HEIGHT + 64)
            particle_reset(state, p, definition, false);
        float speed = 1 - fabsf(p->x - GAME_WIDTH * 0.5f) * 0.001f;
        p->x += p->vx * speed;
        p->y += p->vy * speed + game->scroll_speed;
        p->vx += (p->target_x - p->vx) * 0.001f;
        p->vy += (p->target_y - p->vy) * 0.001f;
    }
}
