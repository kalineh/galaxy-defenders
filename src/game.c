#include "game.h"
#include <math.h>
#include <string.h>

static float clamp(float value, float low, float high) {
    return value < low ? low : value > high ? high : value;
}

uint32_t game_random(Game *game) {
    game->random = game->random * 1664525u + 1013904223u;
    return game->random;
}

static float segment_distance_squared(float ax, float ay, float bx, float by, float x, float y) {
    float dx = bx - ax, dy = by - ay;
    float length = dx * dx + dy * dy;
    float t = length > 0 ? clamp(((x - ax) * dx + (y - ay) * dy) / length, 0, 1) : 0;
    dx = ax + t * dx - x;
    dy = ay + t * dy - y;
    return dx * dx + dy * dy;
}

static bool segment_hits(float ax, float ay, float bx, float by, float x, float y, float radius) {
    return segment_distance_squared(ax, ay, bx, by, x, y) <= radius * radius;
}

static bool segment_box(float ax, float ay, float bx, float by, float x, float y, float half_x, float half_y) {
    float low = 0, high = 1;
    const float start[] = {ax - x, ay - y}, delta[] = {bx - ax, by - ay}, extent[] = {half_x, half_y};
    for (int i = 0; i < 2; ++i) {
        if (fabsf(delta[i]) < 0.00001f) {
            if (fabsf(start[i]) > extent[i]) return false;
        } else {
            float a = (-extent[i] - start[i]) / delta[i], b = (extent[i] - start[i]) / delta[i];
            low = fmaxf(low, fminf(a, b));
            high = fminf(high, fmaxf(a, b));
            if (low > high) return false;
        }
    }
    return true;
}

static bool laser_hits_circle(float ax, float ay, float bx, float by, const Enemy *enemy) {
    /* Laser.cs uses a 1 x 0.5 AABB whose top-left is the projectile position.
       Sweep its center through the circle's exact rounded-rectangle expansion. */
    ax += 0.5f; bx += 0.5f; ay += 0.25f; by += 0.25f;
    float r = enemy->radius, x = enemy->x, y = enemy->y;
    if (segment_box(ax, ay, bx, by, x, y, r + 0.5f, 0.25f) ||
        segment_box(ax, ay, bx, by, x, y, 0.5f, r + 0.25f)) return true;
    for (int sx = -1; sx <= 1; sx += 2) for (int sy = -1; sy <= 1; sy += 2)
        if (segment_hits(ax, ay, bx, by, x + sx * 0.5f, y + sy * 0.25f, r)) return true;
    return false;
}

static float random_unit(Game *game) {
    return (game_random(game) >> 8) / 16777216.0f;
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
        {4, 0, 60, 15, 0, 3, 0, 1, 0.7f, 0, -0.02f, 0, 0},
        {2, 0, 60, 15, 0, 3, 0, 1, 0.5f, 0, -0.015f, 0, 0},
        {16, 1, 60, 15, 0, 0, 32, 2.5f, 1.5f, 0.2f, -0.06f, 2.5f, 0.1f},
        {24, 1, 60, 15, 0, 0, 64, 3.5f, 2.2f, 0.2f, -0.085f, 3.5f, 0.1f}
    };
    if (kind < 0 || kind > EFFECT_BUILDING_BIG) return;
    bool building = kind == EFFECT_BUILDING_SMALL || kind == EFFECT_BUILDING_BIG;
    int passes = kind == EFFECT_ENEMY_DEATH || building ? 2 : 1;
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
            if (building) {
                effect->r = pass ? 229 : 102;
                effect->g = effect->b = pass ? 214 : 102;
            } else if (kind == EFFECT_ENEMY_DEATH || kind == EFFECT_ENEMY_SHOT_HIT) {
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
    player->friction = game_chassis[chassis].friction;
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
    player->pilot = slot;
    player->weapon_level = 2;
    player->energy = 3;
    player->friction = game_chassis[0].friction;
    player->invulnerable = 90;
    return true;
}

void game_reset(Game *game, uint32_t seed) {
    memset(game, 0, sizeof(*game));
    game->random = game->seed = seed;
    game->stage.camera_y = 400;
    game->stage.previous_line = 100000;
    game->stage.music_event = -1;
    game->scroll_speed = stage_definitions[0].scroll_speed;
    game->waves = true;
    game->menu.players = 1;
    for (int i = 0; i < 3; ++i) game->menu.options[i] = 10;
    game_join(game, 0);
}

int game_spawn_enemy(Game *game, EnemyKind kind, float x, float y) {
    if (kind < ENEMY_BALL || kind >= ENEMY_KIND_COUNT || !isfinite(x) || !isfinite(y)) return -1;
    for (int i = 0; i < GAME_ENEMIES; ++i) {
        if (game->enemies[i].active) continue;
        Enemy *enemy = &game->enemies[i];
        *enemy = (Enemy){0};
        enemy->active = true;
        enemy->kind = kind;
        enemy->x = x;
        enemy->y = y;
        static const float health[] = {0.5f, 0.75f, 1.25f, 25, 0.15f, 4, 1, 8, 1, 70};
        static const float radius[] = {36, 28, 28, 150, 25, 64, 28, 60, 36, 210};
        static const int coins[] = {1, 1, 3, 0, 1, 5, 2, 0, 2, 0};
        static const float health_scale[] = {1, 1.25f, 1.55f};
        static const float boss_scale[] = {1, 1.5f, 2};
        enemy->health = health[kind] * health_scale[game->difficulty];
        if (kind == ENEMY_BOSS || kind == ENEMY_BOSS2 || kind == ENEMY_BOSS_CHUNK) enemy->health *= boss_scale[game->difficulty];
        enemy->health_max = enemy->health;
        enemy->radius = radius[kind];
        enemy->coins = coins[kind];
        if (kind == ENEMY_BOSS_CHUNK) enemy->box_width = enemy->box_height = 120;
        if (kind == ENEMY_BOSS2) { enemy->box_width = 420; enemy->box_height = 160; }
        enemy->parent = enemy->children[0] = enemy->children[1] = -1;
        enemy->move_frame = -1;
        enemy->move_speed = enemy->move_transition = 1;
        if (kind == ENEMY_TURRET || kind == ENEMY_DOWN_TURRET || kind == ENEMY_SHOOT_BALL)
            enemy->cooldown = (int)(random_unit(game) * (kind == ENEMY_TURRET ? 45 : kind == ENEMY_DOWN_TURRET ? 60 : 90));
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
        Bullet *bullet = &game->bullets[i];
        /* Keep pool snapshots deterministic, including padding after active. */
        memset(bullet, 0, sizeof(*bullet));
        bullet->active = true;
        bullet->owner = owner;
        bullet->x = x; bullet->y = y;
        bullet->vx = vx; bullet->vy = vy;
        bullet->radius = owner < 0 ? ENEMY_SHOT_RADIUS : 0; /* Player laser is an AABB. */
        bullet->damage = damage;
        return i;
    }
    ++game->dropped_spawns;
    return -1;
}

void game_damage_player(Game *game, int slot, float damage) {
    if (game->seeking || slot < 0 || slot >= GAME_PLAYERS || damage <= 0 || !isfinite(damage)) return;
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
        player->energy = 3;
        player->weapon_cooldown[0] = player->weapon_cooldown[1] = 0;
        player->x = slot == 0 ? 400 : 560;
        player->y = 880;
        player->vx = player->vy = 0;
        player->invulnerable = 120;
    }
}

bool game_upgrade_weapon(Game *game, int slot, int level) {
    if (slot < 0 || slot >= GAME_PLAYERS || level < 0 || level > 7 || !game->players[slot].joined) return false;
    game->players[slot].weapon_level = level;
    return true;
}

static void fire_front_laser(Game *game, int slot) {
    /* WeaponDefinitions.cs FrontLaser/FrontLaserFocus, zero-based levels. */
    static const struct { int count, big_mask; float offsets[8]; } normal[] = {
        {1, 0, {0}}, {2, 0, {-10, 10}}, {4, 0, {-15, -5, 5, 15}},
        {5, 4, {-20, -10, 0, 10, 20}}, {6, 12, {-25, -15, -5, 5, 15, 25}},
        {6, 30, {-25, -15, -5, 5, 15, 25}}, {6, 63, {-25, -15, -5, 5, 15, 25}},
        {8, 255, {-35, -25, -15, -5, 5, 15, 25, 35}}
    };
    static const int focus_count[] = {1, 1, 1, 2, 2, 2, 3, 3};
    static const float focus_reload[] = {0.06f, 0.05f, 0.04f, 0.05f, 0.04f, 0.03f, 0.04f, 0.03f};
    Player *p = &game->players[slot];
    int mode = p->input.focus ? 1 : 0, level = p->weapon_level;
    if (p->weapon_cooldown[mode] > 0) return;
    int count = mode ? focus_count[level] : normal[level].count;
    float cost = 0;
    for (int i = 0; i < count; ++i) cost += mode ? 0.225f : normal[level].big_mask & (1 << i) ? 0.15f : 0.1f;
    if (p->energy < cost) return;
    p->energy -= cost;
    for (int i = 0; i < count; ++i) {
        float x = mode ? (i - (count - 1) * 0.5f) * (count == 2 ? 6 : 4) : normal[level].offsets[i];
        float damage = mode ? 0.175f : normal[level].big_mask & (1 << i) ? 0.225f : 0.1f;
        if (p->pilot == 2) damage *= 1.15f;
        game_spawn_bullet(game, slot, p->x + x, p->y - (mode ? 0 : 20), 0, mode ? -19 : -17, damage);
    }
    p->weapon_cooldown[mode] = mode ? focus_reload[level] : 0.16f;
    ++game->shots;
}

static void update_players(Game *game) {
    for (int i = 0; i < GAME_PLAYERS; ++i) {
        Player *player = &game->players[i];
        if (!player->joined || player->lives <= 0) continue;
        if (player->invulnerable > 0) --player->invulnerable;
        /* Legacy input/fire runs before generator and cooldown updates. */
        if (player->input.fire || player->input.focus) fire_front_laser(game, i);
        player->energy = fminf(3, player->energy + 0.05f);
        for (int mode = 0; mode < 2; ++mode) player->weapon_cooldown[mode] = fmaxf(0, player->weapon_cooldown[mode] - 1.0f / GAME_HZ);
        float x = player->input.x, y = player->input.y;
        float length = sqrtf(x * x + y * y);
        if (length > 1) { x /= length; y /= length; }
        /* Rookie acceleration and friction from legacy/Ship/ShipDefinitions.cs. */
        const Chassis *chassis = &game_chassis[player->chassis];
        float acceleration = chassis->speed * (player->pilot == 0 ? 1.2f : 1);
        player->vx += x * acceleration;
        player->vy += y * acceleration;
        player->x = clamp(player->x + player->vx, 30, GAME_WIDTH - 30);
        player->y = clamp(player->y + player->vy, 40, GAME_HEIGHT - 40);
        float target_friction = chassis->friction * (player->input.focus ? 0.6f : 1);
        player->friction += (target_friction - player->friction) * (player->input.focus ? 0.2f : 0.4f);
        player->vx *= player->friction;
        player->vy *= player->friction;
        if (player->input.focus) game_effect(game, EFFECT_FOCUS, player->x, player->y, i, player->vx, player->vy - 2.5f);
        if ((player->x <= 30 && player->vx < 0) || (player->x >= GAME_WIDTH - 30 && player->vx > 0)) player->vx = 0;
        if ((player->y <= 40 && player->vy < 0) || (player->y >= GAME_HEIGHT - 40 && player->vy > 0)) player->vy = 0;
    }
}

static void update_enemies(Game *game) {
    for (int i = 0; i < GAME_ENEMIES; ++i) {
        Enemy *enemy = &game->enemies[i];
        if (!enemy->active) continue;
        enemy_update(game, enemy);
        for (int p = 0; p < GAME_PLAYERS; ++p) {
            Player *player = &game->players[p];
            float dx = player->x - enemy->x, dy = player->y - enemy->y;
            bool touching;
            if (enemy->box_width > 0) {
                dx = fmaxf(fabsf(dx) - enemy->box_width * 0.5f, 0);
                dy = fmaxf(fabsf(dy) - enemy->box_height * 0.5f, 0);
                touching = dx * dx + dy * dy <= PLAYER_ARMOR_RADIUS * PLAYER_ARMOR_RADIUS;
            } else {
                /* Collision.cs deliberately uses r1*r1 + r2*r2, not (r1+r2)^2. */
                touching = dx * dx + dy * dy < enemy->radius * enemy->radius + PLAYER_ARMOR_RADIUS * PLAYER_ARMOR_RADIUS;
            }
            if (player->joined && player->lives > 0 && touching) game_damage_player(game, p, 4);
        }
        if (enemy->y > GAME_HEIGHT + 200 || enemy->y < -240 || enemy->x < -150 || enemy->x > GAME_WIDTH + 150) enemy->active = false;
    }
}

static void update_bullets(Game *game) {
    for (int i = 0; i < GAME_BULLETS; ++i) {
        Bullet *bullet = &game->bullets[i];
        if (!bullet->active) continue;
        float old_x = bullet->x, old_y = bullet->y;
        bullet->x += bullet->vx;
        bullet->y += bullet->vy + game->scroll_speed;
        if (bullet->owner >= 0) {
            for (int e = 0; e < GAME_ENEMIES; ++e) {
                Enemy *enemy = &game->enemies[e];
                bool hit = enemy->box_width > 0 ?
                    segment_box(old_x + 0.5f, old_y + 0.25f, bullet->x + 0.5f, bullet->y + 0.25f, enemy->x, enemy->y,
                        enemy->box_width * 0.5f + 0.5f, enemy->box_height * 0.5f + 0.25f) :
                    laser_hits_circle(old_x, old_y, bullet->x, bullet->y, enemy);
                if (!enemy->active || !hit) continue;
                bullet->active = false;
                ++game->projectile_hits;
                game_effect(game, EFFECT_LASER_HIT, bullet->x, bullet->y, bullet->owner, 0, 1);
                enemy->health -= bullet->damage;
                if (enemy->health <= 0) {
                    enemy->active = false;
                    ++game->kills;
                    game->players[bullet->owner].score += enemy->kind == ENEMY_BOSS ? 5000 : 100;
                    game_effect(game, EFFECT_ENEMY_DEATH, enemy->x, enemy->y, 0, 0, -1.25f);
                    if (enemy->kind == ENEMY_BOSS || enemy->kind == ENEMY_BOSS2) {
                        /* Boss1.OnDie clears enemies/projectiles before results. */
                        if (!enemy->authored) game->complete = true;
                        for (int n = 0; n < GAME_BULLETS; ++n) game->bullets[n].active = false;
                        for (int n = 0; n < GAME_ENEMIES; ++n) game->enemies[n].active = false;
                        return;
                    }
                }
                break;
            }
            if (bullet->active) {
                const StageDefinition *stage = &stage_definitions[game->stage.index];
                for (int i = 0; i < stage->event_count; ++i) {
                    const StageEvent *event = &stage->events[i];
                    if (event->kind != STAGE_BUILDING || !game->stage.activated[i] || game->stage.building_health[i] < 0) continue;
                    float x = event->x + GAME_WIDTH * 0.5f, y = event->y - game->stage.camera_y + GAME_HEIGHT * 0.5f;
                    if (!segment_box(old_x + 0.5f, old_y + 0.25f, bullet->x + 0.5f, bullet->y + 0.25f, x, y,
                        event->width * 0.4f + 0.5f, event->height * 0.4f + 0.25f)) continue;
                    game->stage.building_health[i] -= bullet->damage;
                    bullet->active = false;
                    ++game->projectile_hits;
                    game_effect(game, EFFECT_LASER_HIT, bullet->x, bullet->y, bullet->owner, 0, 1);
                    if (game->stage.building_health[i] < 0) {
                        /* Keep the original strict <0 destruction boundary. */
                        ++game->building_kills;
                        float angle = random_unit(game) * 6.2831853f;
                        game_effect(game, !strncmp(event->name, "Small", 5) ? EFFECT_BUILDING_SMALL : EFFECT_BUILDING_BIG,
                            x, y, 0, cosf(angle) * 0.75f, sinf(angle) * 0.75f);
                    }
                    break;
                }
            }
        } else {
            for (int p = 0; p < GAME_PLAYERS; ++p) {
                Player *player = &game->players[p];
                if (!player->joined || player->lives <= 0 || segment_distance_squared(old_x, old_y, bullet->x, bullet->y,
                    player->x, player->y) >= PLAYER_ARMOR_RADIUS * PLAYER_ARMOR_RADIUS + bullet->radius * bullet->radius) continue;
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
    if (game->paused && !game->over && !game->complete) return;
    update_effects(game);
    if (game->over || game->complete) return;
    ++game->tick;
    if (!game->seeking) update_players(game);
    if (game->waves) game_stage_update(game);
    else game->scroll_speed = 0;
    update_enemies(game);
    update_bullets(game);
    bool alive = false;
    for (int p = 0; p < GAME_PLAYERS; ++p) if (game->players[p].joined && game->players[p].lives > 0) alive = true;
    if (!alive && !game->seeking) game->over = true;
    if (game->complete && !game->over) game_menu_open(game, MENU_CLEAR);
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

bool game_stage(Game *game, int index, float progress) {
    /* Other stages remain unavailable until all their behavior contracts exist. */
    if (index < 0 || index > 1 || !isfinite(progress) || progress < 0 || progress > 1) return false;
    Player players[GAME_PLAYERS];
    memcpy(players, game->players, sizeof(players));
    GameMenu menu = game->menu;
    int difficulty = game->difficulty, revision = game->stage.revision + 1;
    game_reset(game, game->seed);
    game->difficulty = difficulty;
    game->menu = menu;
    memcpy(game->players, players, sizeof(players));
    game->stage.index = index;
    game->scroll_speed = stage_definitions[index].scroll_speed;
    game->stage.revision = revision;
    game_menu_open(game, MENU_NONE);
    game->seeking = true;
    int ticks = (int)(progress * (stage_definitions[index].length + 400) / game->scroll_speed);
    for (int i = 0; i < ticks; ++i) game_tick(game);
    game->seeking = false;
    memcpy(game->players, players, sizeof(players));
    for (int i = 0; i < GAME_PLAYERS; ++i) {
        game->players[i].input = (PlayerInput){0};
        if (game->players[i].joined) {
            game->players[i].lives = game->players[i].lives > 0 ? game->players[i].lives : 3;
            game->players[i].health = game_chassis[game->players[i].chassis].armor;
            game->players[i].invulnerable = 120;
        }
    }
    game->over = false;
    return true;
}
