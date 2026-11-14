#include "game.h"
#include <math.h>
#include <string.h>

static float clamp(float value, float low, float high) {
    return value < low ? low : value > high ? high : value;
}

static uint32_t random_next(Game *game) {
    game->random = game->random * 1664525u + 1013904223u;
    return game->random;
}

static bool segment_hits(float ax, float ay, float bx, float by, float x, float y, float radius) {
    float dx = bx - ax, dy = by - ay;
    float length = dx * dx + dy * dy;
    float t = length > 0 ? clamp(((x - ax) * dx + (y - ay) * dy) / length, 0, 1) : 0;
    dx = ax + t * dx - x;
    dy = ay + t * dy - y;
    return dx * dx + dy * dy <= radius * radius;
}

static float random_unit(Game *game) {
    return (random_next(game) >> 8) / 16777216.0f;
}

void game_effect(Game *game, EffectKind kind, float x, float y, int player, float vx, float vy) {
    /* Active definitions from legacy/Effects/ParticleEffectDefinitions.cs. */
    static const struct {
        int count, triangle, lifetime, variation;
        float offset, circle, box, speed, scale, scale_variation, shrink, outward, spin;
    } definitions[] = {
        {24, 1, 90, 0, 0, 0, 24, 2, 1.5f, 0.2f, -0.06f, 2.5f, 0.2f},
        {8, 1, 60, 15, 8, 10, 0, 2.5f, 1.4f, 0.2f, -0.1f, 0.5f, 0.1f},
        {24, 1, 60, 15, 0, 8, 0, 3, 2, 0.2f, -0.05f, 1, 0.2f},
        {2, 0, 15, 0, 0, 40, 0, 1, 0.3f, 0, -0.025f, -4, 0.2f},
        {4, 0, 60, 15, 0, 3, 0, 1, 0.7f, 0, -0.02f, 0, 0}
    };
    if (kind < 0 || kind > EFFECT_ENEMY_SHOT_HIT) return;
    int passes = kind == EFFECT_ENEMY_DEATH ? 2 : 1;
    for (int pass = 0; pass < passes; ++pass) {
        for (int i = 0; i < definitions[kind].count; ++i) {
            Effect *effect = &game->effects[game->effect_cursor];
            game->effect_cursor = (game->effect_cursor + 1) % GAME_EFFECTS;
            float angle = random_unit(game) * 6.2831853f;
            float dx = (random_unit(game) - 0.5f) * definitions[kind].box + cosf(angle) * definitions[kind].circle + definitions[kind].offset;
            float dy = (random_unit(game) - 0.5f) * definitions[kind].box + sinf(angle) * definitions[kind].circle + definitions[kind].offset;
            float length = sqrtf(dx * dx + dy * dy);
            *effect = (Effect){0};
            effect->x = x + dx;
            effect->y = y + dy;
            effect->vx = vx + (random_unit(game) - 0.5f) * definitions[kind].speed;
            effect->vy = vy + (random_unit(game) - 0.5f) * definitions[kind].speed;
            if (length > 0.001f) {
                effect->vx += dx / length * definitions[kind].outward;
                effect->vy += dy / length * definitions[kind].outward;
            }
            effect->scale = definitions[kind].scale + (random_unit(game) - 0.5f) * definitions[kind].scale_variation;
            effect->scale_delta = definitions[kind].shrink;
            effect->angle = (random_unit(game) - 0.5f) * 360;
            effect->spin = (random_unit(game) - 0.5f) * definitions[kind].spin * 57.29578f;
            effect->kind = definitions[kind].triangle;
            /* Preserve the active legacy manager's lifetime/2 calculation. */
            effect->life = definitions[kind].lifetime + (int)(-definitions[kind].lifetime * 0.5f + definitions[kind].variation * random_unit(game));
            if (kind == EFFECT_ENEMY_DEATH || kind == EFFECT_ENEMY_SHOT_HIT) {
                effect->r = pass ? 102 : 252;
                effect->g = pass ? 102 : 124;
                effect->b = pass ? 102 : 85;
            } else {
                effect->r = player ? 195 : 247;
                effect->g = player ? 150 : 208;
                effect->b = player ? 255 : 74;
            }
        }
    }
}

static void update_effects(Game *game) {
    for (int i = 0; i < GAME_EFFECTS; ++i) {
        Effect *effect = &game->effects[i];
        if (effect->life <= 0) continue;
        --effect->life;
        effect->x += effect->vx;
        effect->y += effect->vy;
        effect->scale += effect->scale_delta;
        effect->angle += effect->spin;
        if (effect->scale <= 0) effect->life = 0;
    }
}

/* legacy/Ship/ShipDefinitions.cs: ChassisDefinitions.Items. */
const Chassis game_chassis[GAME_CHASSIS] = {
    {"Rookie", 1000, 1.5f, 0.85f, 20, 0.25f},
    {"Eagle", 1250, 1.75f, 0.825f, 15, 0.15f},
    {"Crusher", 2500, 0.7f, 0.92f, 30, 0.60f},
    {"Interceptor", 7000, 2.0f, 0.80f, 25, 0.35f},
    {"Phoenix", 9500, 1.8f, 0.85f, 30, 0.50f},
    {"Lightning", 14000, 3.0f, 0.775f, 25, 0.35f},
    {"Dragon", 22500, 2.15f, 0.825f, 35, 0.60f},
    {"Demon", 22500, 0.75f, 0.92f, 50, 0.85f},
    {"Ace", 25000, 2.75f, 0.75f, 40, 0.75f}
};

bool game_equip_chassis(Game *game, int slot, int chassis) {
    if (slot < 0 || slot >= GAME_PLAYERS || chassis < 0 || chassis >= GAME_CHASSIS) return false;
    Player *player = &game->players[slot];
    if (!player->joined || player->lives <= 0) return false;
    float ratio = player->health / game_chassis[player->chassis].armor;
    player->chassis = chassis;
    player->health = ratio * game_chassis[chassis].armor;
    return true;
}

bool game_join(Game *game, int slot) {
    if (slot < 0 || slot >= GAME_PLAYERS || game->players[slot].joined || game->over || game->complete) return false;
    Player *player = &game->players[slot];
    *player = (Player){0};
    player->joined = true;
    player->x = slot == 0 ? 400 : 560;
    player->y = 880;
    player->health = 20;
    player->lives = 3;
    player->invulnerable = 90;
    return true;
}

void game_reset(Game *game, uint32_t seed) {
    memset(game, 0, sizeof(*game));
    game->random = seed;
    game->waves = true;
    game_join(game, 0);
}

int game_spawn_enemy(Game *game, EnemyKind kind, float x, float y) {
    if (kind < ENEMY_BALL || kind > ENEMY_BOSS || !isfinite(x) || !isfinite(y)) return -1;
    for (int i = 0; i < GAME_ENEMIES; ++i) {
        if (game->enemies[i].active) continue;
        Enemy *enemy = &game->enemies[i];
        *enemy = (Enemy){0};
        enemy->active = true;
        enemy->kind = kind;
        enemy->x = x;
        enemy->y = y;
        enemy->vy = kind == ENEMY_TRIANGLE ? 4.5f : 2.5f;
        enemy->radius = kind == ENEMY_BOSS ? 150 : kind == ENEMY_TRIANGLE ? 28 : 36;
        enemy->health = kind == ENEMY_BOSS ? 60 : kind == ENEMY_TURRET ? 1.5f : 0.5f;
        enemy->cooldown = 75;
        return i;
    }
    ++game->dropped_spawns;
    return -1;
}

int game_spawn_bullet(Game *game, int owner, float x, float y, float vx, float vy, float damage) {
    if (owner < -1 || owner >= GAME_PLAYERS || damage <= 0 || !isfinite(damage) ||
        !isfinite(x) || !isfinite(y) || !isfinite(vx) || !isfinite(vy)) return -1;
    for (int i = 0; i < GAME_BULLETS; ++i) {
        if (game->bullets[i].active) continue;
        game->bullets[i] = (Bullet){true, owner, x, y, vx, vy, owner < 0 ? 9 : 5, damage};
        return i;
    }
    ++game->dropped_spawns;
    return -1;
}

void game_damage_player(Game *game, int slot, float damage) {
    if (slot < 0 || slot >= GAME_PLAYERS || damage <= 0 || !isfinite(damage)) return;
    Player *player = &game->players[slot];
    if (!player->joined || player->lives <= 0 || player->invulnerable > 0 || player->invincible) return;
    player->health -= damage;
    player->invulnerable = 45;
    ++game->hits;
    game_effect(game, EFFECT_ARMOR_HIT, player->x, player->y, slot, 0, -2);
    if (player->health > 0) return;
    player->health = 0;
    ++game->deaths;
    game_effect(game, EFFECT_PLAYER_DEATH, player->x, player->y, slot, 0, 0);
    --player->lives;
    if (player->lives > 0) {
        player->health = game_chassis[player->chassis].armor;
        player->x = slot == 0 ? 400 : 560;
        player->y = 880;
        player->vx = player->vy = 0;
        player->invulnerable = 120;
    }
}

static void update_players(Game *game) {
    for (int i = 0; i < GAME_PLAYERS; ++i) {
        Player *player = &game->players[i];
        if (!player->joined || player->lives <= 0) continue;
        if (player->invulnerable > 0) --player->invulnerable;
        if (player->cooldown > 0) --player->cooldown;
        float x = player->input.x, y = player->input.y;
        float length = sqrtf(x * x + y * y);
        if (length > 1) { x /= length; y /= length; }
        /* Rookie acceleration and friction from legacy/Ship/ShipDefinitions.cs. */
        const Chassis *chassis = &game_chassis[player->chassis];
        float acceleration = player->input.focus ? chassis->speed * (0.65f / 1.5f) : chassis->speed;
        player->vx += x * acceleration;
        player->vy += y * acceleration;
        player->x = clamp(player->x + player->vx, 30, GAME_WIDTH - 30);
        player->y = clamp(player->y + player->vy, 40, GAME_HEIGHT - 40);
        player->vx *= chassis->friction;
        player->vy *= chassis->friction;
        if (player->input.focus) game_effect(game, EFFECT_FOCUS, player->x, player->y, i, player->vx, player->vy - 2.5f);
        if ((player->x <= 30 && player->vx < 0) || (player->x >= GAME_WIDTH - 30 && player->vx > 0)) player->vx = 0;
        if ((player->y <= 40 && player->vy < 0) || (player->y >= GAME_HEIGHT - 40 && player->vy > 0)) player->vy = 0;
        if (player->input.fire && player->cooldown == 0) {
            float spread = player->input.focus ? 0 : 0.8f;
            game_spawn_bullet(game, i, player->x - 13, player->y - 32, -spread, -18, 0.25f);
            game_spawn_bullet(game, i, player->x + 13, player->y - 32, spread, -18, 0.25f);
            player->cooldown = 7;
            ++game->shots;
        }
    }
}

static void spawn_wave(Game *game) {
    if (!game->waves || game->boss_spawned) return;
    if (game->tick >= 2700) {
        if (game_spawn_enemy(game, ENEMY_BOSS, 480, -180) >= 0) game->boss_spawned = true;
        return;
    }
    if (game->tick % 180 != 60) return;
    int wave = game->tick / 180;
    EnemyKind kind = (EnemyKind)(wave % 3);
    float offset = (float)(random_next(game) % 100) - 50;
    for (int i = 0; i < 5; ++i) {
        int index = game_spawn_enemy(game, kind, 160 + i * 160 + offset, -50 - i * 36);
        if (index >= 0 && kind == ENEMY_TRIANGLE) game->enemies[index].vx = wave % 2 ? 0.8f : -0.8f;
    }
}

static void enemy_fire(Game *game, Enemy *enemy) {
    if (--enemy->cooldown > 0) return;
    int target = -1;
    float nearest = 1e20f;
    for (int i = 0; i < GAME_PLAYERS; ++i) {
        Player *player = &game->players[i];
        if (!player->joined || player->lives <= 0) continue;
        float dx = player->x - enemy->x, dy = player->y - enemy->y;
        float distance = dx * dx + dy * dy;
        if (distance < nearest) { nearest = distance; target = i; }
    }
    if (target < 0) return;
    float dx = game->players[target].x - enemy->x, dy = game->players[target].y - enemy->y;
    float length = sqrtf(dx * dx + dy * dy);
    if (length < 1) length = 1;
    game_spawn_bullet(game, -1, enemy->x, enemy->y + 24, dx / length * 5, dy / length * 5, 2);
    if (enemy->kind == ENEMY_BOSS) {
        for (int i = -3; i <= 3; ++i) {
            float angle = i * 0.18f + sinf(enemy->age * 0.025f) * 0.25f;
            game_spawn_bullet(game, -1, enemy->x, enemy->y + 90, sinf(angle) * 5, cosf(angle) * 5, 2);
        }
    }
    enemy->cooldown = enemy->kind == ENEMY_BOSS ? 40 : 110;
}

static void update_enemies(Game *game) {
    for (int i = 0; i < GAME_ENEMIES; ++i) {
        Enemy *enemy = &game->enemies[i];
        if (!enemy->active) continue;
        ++enemy->age;
        if (enemy->kind == ENEMY_BOSS && enemy->y >= 200) {
            enemy->x = 480 + sinf(enemy->age * 0.012f) * 240;
        } else {
            enemy->x += enemy->vx;
            enemy->y += enemy->vy;
        }
        if ((enemy->kind == ENEMY_TURRET || enemy->kind == ENEMY_BOSS) && enemy->y > 0 && enemy->y < 900) enemy_fire(game, enemy);
        for (int p = 0; p < GAME_PLAYERS; ++p) {
            Player *player = &game->players[p];
            if (player->joined && player->lives > 0 && segment_hits(enemy->x, enemy->y, enemy->x, enemy->y,
                player->x, player->y, enemy->radius + 18)) game_damage_player(game, p, 4);
        }
        if (enemy->y > GAME_HEIGHT + 200) enemy->active = false;
    }
}

static void update_bullets(Game *game) {
    for (int i = 0; i < GAME_BULLETS; ++i) {
        Bullet *bullet = &game->bullets[i];
        if (!bullet->active) continue;
        float old_x = bullet->x, old_y = bullet->y;
        bullet->x += bullet->vx;
        bullet->y += bullet->vy;
        if (bullet->owner >= 0) {
            for (int e = 0; e < GAME_ENEMIES; ++e) {
                Enemy *enemy = &game->enemies[e];
                if (!enemy->active || !segment_hits(old_x, old_y, bullet->x, bullet->y, enemy->x, enemy->y, enemy->radius + bullet->radius)) continue;
                bullet->active = false;
                ++game->projectile_hits;
                enemy->health -= bullet->damage;
                if (enemy->health <= 0) {
                    enemy->active = false;
                    ++game->kills;
                    game->players[bullet->owner].score += enemy->kind == ENEMY_BOSS ? 5000 : 100;
                    game_effect(game, EFFECT_ENEMY_DEATH, enemy->x, enemy->y, 0, 0, -1.25f);
                    if (enemy->kind == ENEMY_BOSS) game->complete = true;
                }
                break;
            }
        } else {
            for (int p = 0; p < GAME_PLAYERS; ++p) {
                Player *player = &game->players[p];
                if (!player->joined || player->lives <= 0 || !segment_hits(old_x, old_y, bullet->x, bullet->y,
                    player->x, player->y, 18 + bullet->radius)) continue;
                game_damage_player(game, p, bullet->damage);
                game_effect(game, EFFECT_ENEMY_SHOT_HIT, bullet->x, bullet->y, 0, 0, 0);
                bullet->active = false;
                break;
            }
        }
        if (bullet->x < -100 || bullet->x > GAME_WIDTH + 100 || bullet->y < -250 || bullet->y > GAME_HEIGHT + 100) bullet->active = false;
    }
}

void game_tick(Game *game) {
    if (game->paused) return;
    update_effects(game);
    if (game->over || game->complete) return;
    ++game->tick;
    update_players(game);
    spawn_wave(game);
    update_enemies(game);
    update_bullets(game);
    bool alive = false;
    for (int p = 0; p < GAME_PLAYERS; ++p) if (game->players[p].joined && game->players[p].lives > 0) alive = true;
    if (!alive) game->over = true;
}

int game_enemy_count(const Game *game) {
    int count = 0;
    for (int i = 0; i < GAME_ENEMIES; ++i) count += game->enemies[i].active;
    return count;
}

int game_bullet_count(const Game *game) {
    int count = 0;
    for (int i = 0; i < GAME_BULLETS; ++i) count += game->bullets[i].active;
    return count;
}
