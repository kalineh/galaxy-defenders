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

static float random_unit(Game *game) {
    return (game_random(game) >> 8) / 16777216.0f;
}

static void particle_reset(Game *game, SceneryParticle *p, const SceneryLayer *layer, bool initial) {
    p->x = game->stage.camera_x + random_unit(game) * GAME_WIDTH;
    p->y = initial ? random_unit(game) * GAME_HEIGHT : layer->stars ? -100 : -32;
    p->vx = layer->vx; p->vy = layer->vy;
    float angle = layer->stars ? 0 : (random_unit(game) * 6.283185307f - 1.570796327f) * layer->random_lerp;
    p->target_x = layer->vx * cosf(angle) - layer->vy * sinf(angle);
    p->target_y = layer->vx * sinf(angle) + layer->vy * cosf(angle);
}

static void grow_layer(Game *game, int layer, const SceneryLayer *definition) {
    int *count = &game->stage.scenery_count[layer];
    /* Legacy loop reevaluates Count - Clouds.Count after each addition. */
    for (int i = 0; i < definition->count - *count; ++i) {
        particle_reset(game, &game->stage.scenery[layer][*count], definition, true);
        ++*count;
    }
}

void game_scenery_init(Game *game) {
    const SceneryDefinition *scene = &stage_scenery[game->stage.index < 13 ? game->stage.index : 13];
    for (int layer = 0; layer < 4; ++layer) {
        game->stage.scenery_count[layer] = 0;
        grow_layer(game, layer, &scene->layers[layer]);
    }
}

void game_scenery_update(Game *game) {
    StageState *state = &game->stage;
    const SceneryDefinition *scene = &stage_scenery[state->index < 13 ? state->index : 13];
    for (int layer = 0; layer < 4; ++layer) {
        const SceneryLayer *definition = &scene->layers[layer];
        grow_layer(game, layer, definition);
        for (int i = 0; i < state->scenery_count[layer]; ++i) {
            SceneryParticle *p = &state->scenery[layer][i];
            if (definition->stars ? p->y > GAME_HEIGHT + 1 :
                p->x < state->camera_x - 64 || p->x > state->camera_x + GAME_WIDTH + 64 || p->y < -64 || p->y > GAME_HEIGHT + 64)
                particle_reset(game, p, definition, false);
            float speed = 1 - fabsf(p->x - GAME_WIDTH * 0.5f) * 0.001f;
            p->x += p->vx * speed;
            p->y += p->vy * speed + game->scroll_speed;
            if (!definition->stars) {
                p->vx = p->vx * 0.999f + p->target_x * 0.001f;
                p->vy = p->vy * 0.999f + p->target_y * 0.001f;
            }
        }
    }
}
