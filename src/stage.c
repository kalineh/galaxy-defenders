#include "stage.h"
#include "stage_data.inc"
#include "game.h"
#include <string.h>

const char *game_music(const Game *game) {
    const StageDefinition *definition = &stage_definitions[game->stage.index];
    return game->stage.music_event >= 0 ? definition->events[game->stage.music_event].name : definition->music;
}

void game_stage_update(Game *game) {
    StageState *state = &game->stage;
    const StageDefinition *definition = &stage_definitions[state->index];
    float line = state->camera_y - GAME_HEIGHT * 0.5f - 100;
    for (int i = 0; i < definition->event_count; ++i) {
        const StageEvent *event = &definition->events[i];
        if (state->activated[i] || event->y > state->previous_line || event->y <= line) continue;
        state->activated[i] = true;
        if (event->kind == STAGE_ENEMY) {
            int kind = -1;
            for (int k = 0; k < ENEMY_KIND_COUNT; ++k) if (!strcmp(event->name, enemy_names[k])) kind = k;
            int index = game_spawn_enemy(game, (EnemyKind)kind, event->spawn_x + GAME_WIDTH * 0.5f,
                event->spawn_y - state->camera_y + GAME_HEIGHT * 0.5f);
            if (index >= 0) {
                Enemy *enemy = &game->enemies[index];
                enemy->authored = true;
                enemy->movement = (MovementKind)movement_find(event->mover);
                enemy->move_speed = event->speed;
                enemy->move_transition = event->transition;
                enemy->powerup = event->powerup;
                if (event->no_coins) enemy->coins = 0;
                if (kind == ENEMY_BOSS || kind == ENEMY_BOSS2) game->boss_spawned = true;
            }
        } else if (event->kind == STAGE_MUSIC) {
            state->music_event = i;
            state->music_tick = game->tick;
        } else if (event->kind == STAGE_CAMERA_STOP) state->stopped = true;
        else if (event->kind == STAGE_HEALTH_BAR) state->health_bar = true;
        else if (event->kind == STAGE_BUILDING) {
            static const float big[] = {2.5f, 2.5f, 3, 5, 5, 6, 7, 8, 9};
            static const float small[] = {1, 1.5f, 1.5f, 2, 2.5f, 5, 6};
            bool is_small = !strncmp(event->name, "Small", 5);
            int n = event->name[strlen(event->name) - 1] - '1';
            state->building_health[i] = is_small ? small[n] : big[n];
        }
    }
    state->previous_line = line;
    if (state->stopped) {
        if (game_enemy_count(game)) game->scroll_speed *= 0.95f;
        else if (++state->end_countdown >= 220) {
            memset(game->bullets, 0, sizeof(game->bullets));
            game->complete = true;
        }
    }
    state->camera_y -= game->scroll_speed;
}
