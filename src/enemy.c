#include "game.h"
#include <math.h>

const char *enemy_names[ENEMY_KIND_COUNT] = {
    "Ball", "Isosceles", "Turret", "Boss1", "MiniSquare", "BigBall", "DownTurret", "Boss1Chunk", "ShootBall", "Boss2",
    "Airship", "Beamer", "BlackHole", "Cannon", "Cutter", "CutterBall", "DoubleTurret", "Fence", "FenceBeam", "Glob",
    "MissilePod", "MissileStorm", "MissileTriple", "Pyramid", "RaidTurret", "RotateTurret", "Spike", "Splitter",
    "SplitterFragment", "Swirl", "SwirlBall", "Teleporter", "TripleShot", "BonusShip"
};

static float aim_at_player(const Game *game, const Enemy *enemy) {
    float distance = 1e30f, angle = 1.570796327f;
    for (int i = 0; i < GAME_PLAYERS; ++i) {
        const Player *p = &game->players[i];
        if (!p->joined || p->lives <= 0) continue;
        float dx = p->x - enemy->x, dy = p->y - enemy->y;
        if (dx * dx + dy * dy < distance) {
            distance = dx * dx + dy * dy;
            angle = atan2f(dy, dx);
        }
    }
    return angle;
}

static void shot(Game *game, const Enemy *enemy, float x, float y, float angle, float damage) {
    int i = game_spawn_bullet(game, -1, enemy->x + x, enemy->y + y,
        cosf(angle) * 8, sinf(angle) * 8, damage);
    if (i >= 0) game->bullets[i].radius = ENEMY_SHOT_RADIUS;
}

static bool children_dead(const Game *game, const Enemy *enemy) {
    for (int i = 0; i < 2; ++i) {
        int child = enemy->children[i];
        if (child >= 0 && game->enemies[child].active && game->enemies[child].kind == ENEMY_BOSS_CHUNK &&
            game->enemies[child].parent == (int)(enemy - game->enemies)) return false;
    }
    return true;
}

static void boss_update(Game *game, Enemy *enemy) {
    bool unarmed = children_dead(game, enemy);
    if (enemy->age == 1) {
        enemy->base_y = enemy->y;
        enemy->move_frame = -1;
        enemy->move_wait = 61;
        enemy->cooldown = 91;
        for (int i = 0; i < 2; ++i) {
            int index = game_spawn_enemy(game, ENEMY_BOSS_CHUNK, enemy->x + (i ? 150 : -150), enemy->y + 90);
            enemy->children[i] = index;
            if (index >= 0) {
                game->enemies[index].parent = (int)(enemy - game->enemies);
                game->enemies[index].authored = enemy->authored;
            }
        }
    } else enemy->base_y += enemy->authored ? game->scroll_speed : 0;
    enemy->y = enemy->base_y + sinf((enemy->age - 1) * 0.05f) * 12;

    if (enemy->move_frame >= 0) {
        float t = enemy->move_frame / 60.0f;
        t = t * t * (3 - 2 * t);
        enemy->x = enemy->move_from + (enemy->move_to - enemy->move_from) * t;
        if (++enemy->move_frame == 60) {
            enemy->move_frame = -1;
            enemy->move_wait = 1 + (unarmed ? 10 : 30) + (int)(game_random(game) % (unarmed ? 30 : 90));
        }
    } else if (--enemy->move_wait <= 0) {
        if (enemy->age == 61) {
            enemy->move_wait = (unarmed ? 10 : 30) + (int)(game_random(game) % (unarmed ? 30 : 90));
        } else {
            enemy->move_from = enemy->x;
            enemy->move_to = GAME_WIDTH * 0.5f + (game_random(game) % 2 ? 180 : -180);
            if (game_random(game) % 100 < 20) enemy->move_to = GAME_WIDTH * 0.5f;
            enemy->move_frame = 1; /* This tick is SmoothStep(..., 0), at move_from. */
        }
    }

    if (--enemy->cooldown <= 0) {
        if (enemy->shot_index == 0) {
            enemy->attack = unarmed ? 2 : (int)(game_random(game) % 4);
            enemy->aim = aim_at_player(game, enemy);
        }
        int attack = enemy->attack;
        if (attack != 3) {
            float angle = attack == 0 ? 1.570796327f : attack == 1 ? aim_at_player(game, enemy) :
                enemy->aim - 0.25f + 0.0625f * enemy->shot_index;
            shot(game, enemy, 38, 78, angle, 3);
            shot(game, enemy, -38, 78, angle, 3);
        }
        ++enemy->shot_index;
        int count = attack == 2 ? 8 : attack == 3 ? 1 : 3;
        enemy->cooldown = attack == 2 ? 2 : 4;
        if (enemy->shot_index == count) {
            enemy->shot_index = 0;
            enemy->cooldown += attack == 2 ? 60 : 30;
            if (attack == 3) enemy->cooldown = 30;
        }
    }
    for (int i = 0; i < 2; ++i) {
        int index = enemy->children[i];
        if (index < 0) continue;
        Enemy *child = &game->enemies[index];
        if (child->active && child->kind == ENEMY_BOSS_CHUNK && child->parent == (int)(enemy - game->enemies)) {
            child->x = enemy->x + (i ? 150 : -150);
            child->y = enemy->y + 90;
        }
    }
}

static void boss2_rings(Game *game, Enemy *enemy) {
    for (int side = -1; side <= 1; side += 2) for (int i = 0; i < 16; ++i)
        shot(game, enemy, side * 146.0f, 32, i * 6.2831853f / 16, 3);
}

static void boss2_update(Game *game, Enemy *enemy) {
    /* Boss2.cs: initial half-width glide, then alternating full-width glides. */
    if (enemy->age == 1) {
        enemy->cooldown = 121;
        enemy->move_frame = 0;
        enemy->move_phase = 0;
    }
    if (enemy->age >= 61) {
        if (enemy->move_wait > 0) --enemy->move_wait;
        else {
            int duration = enemy->move_phase == 0 ? 30 : 60;
            if (!enemy->move_frame) {
                enemy->move_from = enemy->x;
                enemy->move_to = enemy->x + (enemy->move_phase == 0 ? -125 : enemy->move_phase == 1 ? 250 : -250);
            }
            float t = enemy->move_frame / (float)duration;
            enemy->x = enemy->move_from + (enemy->move_to - enemy->move_from) * t * t * (3 - 2 * t);
            if (++enemy->move_frame == duration) {
                enemy->move_frame = 0;
                if (enemy->move_phase == 0) enemy->move_phase = 1;
                else if (enemy->move_phase == 1) { enemy->move_phase = 2; enemy->move_wait = 30; }
                else { enemy->move_phase = 1; enemy->move_wait = 30 + (int)(game_random(game) % 60); }
            }
        }
    }
    if (--enemy->cooldown > 0) return;
    if (!enemy->shot_index) enemy->attack = (int)(game_random(game) % 4);
    int attack = enemy->attack, index = enemy->shot_index;
    if (attack == 0 && index < 9) shot(game, enemy, 0, 132, 1.570796327f, 3);
    if ((attack == 0 && index == 9) || (attack == 2 && index == 0) || attack == 3) boss2_rings(game, enemy);
    if (attack == 1 || attack == 2) {
        shot(game, enemy, -64, 110, 1.570796327f, 3);
        shot(game, enemy, 64, 110, 1.570796327f, 3);
    }
    static const int counts[] = {10, 5, 3, 3};
    enemy->cooldown = attack == 0 ? (index < 8 ? 3 : 8) : attack == 3 ? 16 : 4;
    if (++enemy->shot_index == counts[attack]) { enemy->shot_index = 0; enemy->cooldown += 60; }
}

void enemy_update(Game *game, Enemy *enemy) {
    if (enemy->kind >= ENEMY_AIRSHIP) { enemy_update_extended(game, enemy); return; }
    if (enemy->kind != ENEMY_BOSS_CHUNK) {
        if (enemy->authored) {
            movement_velocity(enemy->movement, enemy->age, enemy->move_speed, enemy->move_transition,
                              &enemy->vx, &enemy->vy);
            if (enemy->movement != MOVE_IGNORE_CAMERA) enemy->y += game->scroll_speed;
        }
        enemy->x += enemy->vx;
        enemy->y += enemy->vy;
    }
    ++enemy->age;
    if (enemy->kind == ENEMY_BOSS) boss_update(game, enemy);
    else if (enemy->kind == ENEMY_BOSS2) boss2_update(game, enemy);
    else if (enemy->kind == ENEMY_MINI_SQUARE) enemy->rotation += 0.01f;
    else if (enemy->kind == ENEMY_TURRET || enemy->kind == ENEMY_DOWN_TURRET || enemy->kind == ENEMY_SHOOT_BALL) {
        if (--enemy->cooldown <= 0) {
            static const float reload[] = {1.1f, 0.9f, 0.8f};
            bool down = enemy->kind == ENEMY_DOWN_TURRET;
            float angle = down ? 1.570796327f : aim_at_player(game, enemy) +
                (game_random(game) >> 8) / 16777216.0f * 6.2831853f * 0.015f;
            shot(game, enemy, 0, 0, angle, down ? 4 : 3);
            enemy->cooldown = (int)((down ? 60 : enemy->kind == ENEMY_SHOOT_BALL ? 90 : 45) * reload[game->difficulty]);
        }
    } else if (enemy->kind == ENEMY_BOSS_CHUNK && --enemy->cooldown <= 0) {
        if (enemy->parent < 0 || !game->enemies[enemy->parent].active) { enemy->active = false; return; }
        const Enemy *boss = &game->enemies[enemy->parent];
        shot(game, enemy, -16, 24, 1.570796327f, 2);
        shot(game, enemy, 16, 24, 1.570796327f, 2);
        shot(game, enemy, -16, -24, 1.570796327f, 2);
        shot(game, enemy, 16, -24, 1.570796327f, 2);
        enemy->cooldown = boss->health < boss->health_max * 0.3f ? 30 : 60;
    }
}
