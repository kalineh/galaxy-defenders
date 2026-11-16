#include "stage.h"
#include "stage_data.inc"
#include "game.h"
#include <string.h>
#include <math.h>

void game_secret_enter(Game *game, int index) {
    if (index < 12 || index >= STAGE_COUNT || game->secret_return.active) return;
    SecretReturn *saved = &game->secret_return;
    game->stage.ignore_secrets = true;
    saved->active = true;
    saved->stage = game->stage;
    saved->tick = game->tick;
    saved->random = game->random;
    saved->scroll_speed = game->scroll_speed;
    saved->boss_spawned = game->boss_spawned;
    memcpy(saved->players, game->players, sizeof(saved->players));
    memcpy(saved->enemies, game->enemies, sizeof(saved->enemies));
    memcpy(saved->bullets, game->bullets, sizeof(saved->bullets));
    memcpy(saved->effects, game->effects, sizeof(saved->effects));
    int revision = game->stage.revision + 1;
    memset(&game->stage, 0, sizeof(game->stage));
    game->stage.index = index;
    game->stage.revision = revision;
    game->stage.camera_y = 400;
    game->stage.previous_line = 100000;
    game->stage.music_event = -1;
    game->stage.ignore_secrets = true;
    game->tick = 0;
    game_scenery_init(game);
    game->scroll_speed = stage_definitions[index].scroll_speed;
    game->boss_spawned = game->complete = false;
    memset(game->enemies, 0, sizeof(game->enemies));
    memset(game->bullets, 0, sizeof(game->bullets));
    memset(game->effects, 0, sizeof(game->effects));
    for (int i = 0; i < GAME_PLAYERS; ++i) {
        game->players[i].x = i ? 560 : 400;
        game->players[i].y = 880;
        game->players[i].vx = game->players[i].vy = 0;
    }
}

void game_secret_return(Game *game) {
    SecretReturn *saved = &game->secret_return;
    if (!saved->active) return;
    int revision = game->stage.revision + 1;
    game->stage = saved->stage;
    game->stage.revision = revision;
    game->stage.secret_countdown = 0;
    game->tick = saved->tick;
    game->random = saved->random;
    game->scroll_speed = saved->scroll_speed;
    game->boss_spawned = saved->boss_spawned;
    memcpy(game->enemies, saved->enemies, sizeof(game->enemies));
    memcpy(game->bullets, saved->bullets, sizeof(game->bullets));
    memcpy(game->effects, saved->effects, sizeof(game->effects));
    for (int i = 0; i < GAME_PLAYERS; ++i) {
        Player *p = &game->players[i];
        int score = p->score;
        float energy = p->energy, shield = p->shield_health, health = p->health;
        *p = saved->players[i];
        p->score = score; p->energy = energy; p->shield_health = shield; p->health = health;
        p->x = fmaxf(32, fminf(GAME_WIDTH - 32, game->stage.secret_x));
        p->y = fmaxf(32, fminf(GAME_HEIGHT - 32, game->stage.secret_y));
        float dx = GAME_WIDTH * 0.5f - p->x, dy = GAME_HEIGHT * 0.5f - p->y, length = hypotf(dx, dy);
        p->vx = length > 0 ? dx / length * 40 : 0;
        p->vy = length > 0 ? dy / length * 40 : 0;
    }
    saved->active = false;
    game->complete = game->over = false;
    game_menu_open(game, MENU_NONE);
}

const char *game_music(const Game *game) {
    if (game->menu.page == MENU_FINISH) return "fading-memories";
    const StageDefinition *definition = &stage_definitions[game->stage.index];
    return game->stage.music_event >= 0 ? definition->events[game->stage.music_event].name : definition->music;
}

void game_stage_update(Game *game) {
    StageState *state = &game->stage;
    const StageDefinition *definition = &stage_definitions[state->index];
    if (state->secret_countdown && ++state->secret_countdown > 120) {
        game_secret_enter(game, state->secret_target);
        return;
    }
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
                if (enemy_is_boss((EnemyKind)kind)) game->boss_spawned = true;
            }
        } else if (event->kind == STAGE_MUSIC) {
            state->music_event = i;
            state->music_tick = game->tick;
        } else if (event->kind == STAGE_CAMERA_STOP) state->stopped = true;
        else if (event->kind == STAGE_FINISH && state->index >= 12) {
            memset(game->bullets, 0, sizeof(game->bullets));
            game->complete = true;
        }
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
    if (!game->seeking && !state->ignore_secrets && state->index < 12) {
        for (int i = 0; i < definition->event_count; ++i) {
            const StageEvent *event = &definition->events[i];
            if (event->kind != STAGE_SECRET || !state->activated[i] || event->expired) continue;
            float x = event->x + GAME_WIDTH * 0.5f, y = event->y - state->camera_y + GAME_HEIGHT * 0.5f;
            for (int p = 0; p < GAME_PLAYERS; ++p) {
                const Player *player = &game->players[p];
                if (!player->joined || player->lives <= 0 || hypotf(player->x - x, player->y - y) >= 64) continue;
                for (int target = 12; target < STAGE_COUNT; ++target) {
                    if (strcmp(event->name, stage_definitions[target].name)) continue;
                    state->ignore_secrets = true;
                    state->secret_target = target;
                    state->secret_countdown = 1;
                    state->secret_x = x; state->secret_y = y;
                }
            }
        }
    }
    if (state->stopped) {
        bool enemies = false;
        for (int i = 0; i < GAME_ENEMIES; ++i)
            if (game->enemies[i].active && game->enemies[i].kind != ENEMY_FENCE) enemies = true;
        if (state->index < 12 && enemies) game->scroll_speed *= 0.95f;
        else if (++state->end_countdown >= 220) {
            memset(game->bullets, 0, sizeof(game->bullets));
            game->complete = true;
        }
    }
    state->camera_y -= game->scroll_speed;
}
