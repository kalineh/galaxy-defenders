#define GALAXY_EFFECTS_IMPLEMENTATION
#include "game.h"
#include <math.h>
#include <string.h>

static float clamp(float value, float low, float high) {
    return value < low ? low : value > high ? high : value;
}

/* Seeded System.Random used by XNA/.NET Framework (subtractive generator). */
void game_random_init(GameRandom *random, uint32_t seed) {
    memset(random, 0, sizeof(*random));
    int signed_seed = (int32_t)seed;
    int magnitude = signed_seed == INT32_MIN ? INT32_MAX : signed_seed < 0 ? -signed_seed : signed_seed;
    int current = 161803398 - magnitude, previous = 1;
    random->values[55] = current;
    for (int i = 1; i < 55; ++i) {
        int index = 21 * i % 55;
        random->values[index] = previous;
        previous = (int32_t)((uint32_t)current - (uint32_t)previous);
        if (previous < 0) previous += INT32_MAX;
        current = random->values[index];
    }
    for (int pass = 0; pass < 4; ++pass) for (int i = 1; i < 56; ++i) {
        random->values[i] = (int32_t)((uint32_t)random->values[i] - (uint32_t)random->values[1 + (i + 30) % 55]);
        if (random->values[i] < 0) random->values[i] += INT32_MAX;
    }
    random->next_pair = 21;
}

uint32_t game_random_sample(GameRandom *random) {
    if (++random->next >= 56) random->next = 1;
    if (++random->next_pair >= 56) random->next_pair = 1;
    int value = (int32_t)((uint32_t)random->values[random->next] - (uint32_t)random->values[random->next_pair]);
    if (value == INT32_MAX) --value;
    if (value < 0) value += INT32_MAX;
    random->values[random->next] = value;
    return (uint32_t)value;
}

uint32_t game_random(Game *game) { return game_random_sample(&game->random); }
float game_random_float(Game *game) { return (float)(game_random(game) / 2147483647.0); }
float game_random_signed(Game *game) {
    float magnitude = game_random_float(game);
    return magnitude * (game_random_float(game) < 0.5f ? -1 : 1);
}
int game_random_range(Game *game, int maximum) { return (int)(game_random(game) / 2147483647.0 * maximum); }

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

static bool swept_circle_box(float ax, float ay, float bx, float by,
                             float x, float y, float half_x, float half_y, float radius) {
    /* Rounded rectangle expansion: square-expanded corners produce false hits. */
    if (segment_box(ax, ay, bx, by, x, y, half_x + radius, half_y) ||
        segment_box(ax, ay, bx, by, x, y, half_x, half_y + radius)) return true;
    for (int sx = -1; sx <= 1; sx += 2) for (int sy = -1; sy <= 1; sy += 2)
        if (segment_hits(ax, ay, bx, by, x + sx * half_x, y + sy * half_y, radius)) return true;
    return false;
}

static bool laser_hits_circle(float ax, float ay, float bx, float by, const Enemy *enemy) {
    /* Laser.cs uses a 1 x 0.5 AABB whose top-left is the projectile position. */
    return swept_circle_box(ax + 0.5f, ay + 0.25f, bx + 0.5f, by + 0.25f,
        enemy->x + enemy->collision_x, enemy->y + enemy->collision_y, 0.5f, 0.25f, enemy->radius);
}

static float random_unit(Game *game) {
    return game_random_float(game);
}

void game_particle(Game *game, ParticleKind kind, float x, float y, int color, float scale, float vx, float vy) {
    if (kind < 0 || kind >= PARTICLE_COUNT) return;
    const ParticleDefinition *d = &particle_definitions[kind];
    static const uint8_t colors[][3] = {{247,208,74},{195,150,255},{252,124,85},{102,102,102},{102,102,102},{229,214,214}};
    for (int i = 0; i < d->count; ++i) {
        Effect *e = &game->effects[game->effect_cursor];
        game->effect_cursor = (game->effect_cursor + 1) % GAME_EFFECTS;
        memset(e, 0, sizeof(*e));
        float box_x = (random_unit(game) - 0.5f) * d->PositionVariationBox[0];
        float box_y = (random_unit(game) - 0.5f) * d->PositionVariationBox[1];
        float angle = random_unit(game) * 6.2831853f;
        float dx = d->Position[0] + box_x + cosf(angle) * d->PositionVariationCircle[0];
        float dy = d->Position[1] + box_y + sinf(angle) * d->PositionVariationCircle[1];
        float length = hypotf(dx, dy);
        e->x = x + dx; e->y = y + dy;
        e->vx = vx + (random_unit(game) - 0.5f) * d->PositionDeltaVariation[0];
        e->vy = vy + (random_unit(game) - 0.5f) * d->PositionDeltaVariation[1];
        if (length > 0.001f) { e->vx += dx / length * d->OutDelta; e->vy += dy / length * d->OutDelta; }
        e->angle = (d->Angle + (random_unit(game) - 0.5f) * d->AngleVariation) * 57.29578f;
        e->spin = (d->AngleDelta + (random_unit(game) - 0.5f) * d->AngleDeltaVariation) * 57.29578f;
        e->scale = scale * (d->Scale[0] + (random_unit(game) - 0.5f) * d->ScaleVariation[0]);
        e->scale_delta = d->ScaleDelta[0] + (random_unit(game) - 0.5f) * d->ScaleDeltaVariation[0];
        e->alpha = d->Alpha + (random_unit(game) - 0.5f) * d->AlphaVariation;
        e->alpha_delta = d->AlphaDelta + (random_unit(game) - 0.5f) * d->AlphaDeltaVariation;
        e->life = d->lifetime + (int)(-d->lifetime * 0.5f + d->variation * random_unit(game));
        e->kind = d->triangle;
        const uint8_t *rgb = color >= 0 && color < 6 ? colors[color] : d->color;
        e->r = rgb[0]; e->g = rgb[1]; e->b = rgb[2];
    }
}

void game_effect(Game *game, EffectKind kind, float x, float y, int player, float vx, float vy) {
    static const ParticleKind types[] = {PARTICLE_ENEMY_DEATH_EXPLOSION, PARTICLE_PLAYER_SHIP_ARMOR_DAMAGE,
        PARTICLE_PLAYER_SHIP_DESTROYED, PARTICLE_PLAYER_FOCUS_MODE, PARTICLE_ENEMY_SHOT_HIT,
        PARTICLE_WEAPON_LASER_HIT, PARTICLE_BUILDING_DESTROYED_SMALL, PARTICLE_BUILDING_DESTROYED_BIG,
        PARTICLE_PLAYER_SHIP_SHIELD_DAMAGE};
    if (kind < 0 || kind > EFFECT_SHIELD_HIT) return;
    if (kind == EFFECT_ENEMY_DEATH || kind == EFFECT_BUILDING_SMALL || kind == EFFECT_BUILDING_BIG) {
        int color = kind == EFFECT_ENEMY_DEATH ? 2 : 4;
        game_particle(game, types[kind], x, y, color, 1, vx, vy);
        game_particle(game, types[kind], x, y, color + 1, 1, vx, vy);
    } else game_particle(game, types[kind], x, y, kind == EFFECT_ENEMY_SHOT_HIT ? 2 : player, 1, vx, vy);
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
        effect->alpha += effect->alpha_delta;
        if (effect->scale <= 0 || effect->alpha <= 0) effect->life = 0;
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
    if (slot < 0 || slot >= GAME_PLAYERS || game->players[slot].joined) return false;
    bool lobby = game->menu.page == MENU_MAIN || game->menu.page == MENU_SLOTS ||
        game->menu.page == MENU_NEW_GAME || game->menu.page == MENU_DIFFICULTY ||
        game->menu.page == MENU_PILOT || game->menu.page == MENU_SHOP;
    if ((game->over || game->complete) && !lobby) return false;
    Player *player = &game->players[slot];
    if (player->initialized) {
        player->joined = true;
        player->input = (PlayerInput){0};
        game->menu.ready[slot] = false;
        if (game->menu.page == MENU_PILOT) {
            game->menu.pilot[slot] = player->pilot;
            if (game->players[1 - slot].joined && game->menu.pilot[slot] == game->menu.pilot[1 - slot])
                game->menu.pilot[slot] = (game->menu.pilot[1 - slot] + 1) % 3;
        }
        game->menu.players = game->players[1].joined ? 2 : 1;
        return true;
    }
    *player = (Player){0};
    player->joined = player->initialized = true;
    player->x = slot == 0 ? 400 : 560;
    player->y = 880;
    player->health = 20;
    player->lives = 1; /* Alive flag; legacy has no extra lives or automatic respawn. */
    player->pilot = slot;
    if (game->players[1 - slot].initialized && player->pilot == game->players[1 - slot].pilot)
        player->pilot = (player->pilot + 1) % 3;
    if (game->menu.page == MENU_PILOT && game->players[1 - slot].joined && player->pilot == game->menu.pilot[1 - slot])
        player->pilot = (game->menu.pilot[1 - slot] + 1) % 3;
    game->menu.pilot[slot] = player->pilot;
    game->menu.ready[slot] = false;
    game->menu.players = game->players[1].joined ? 2 : 1;
    player->weapon_level = 2;
    player->primary = WEAPON_FRONT_LASER;
    player->secondary = player->sidekick = player->shield = -1;
    player->energy = 3;
    player->friction = game_chassis[0].friction;
    player->invulnerable = 0;
    return true;
}

bool game_leave(Game *game, int slot) {
    if (slot < 0 || slot >= GAME_PLAYERS || !game->players[slot].joined) return false;
    Player *player = &game->players[slot];
    player->joined = false;
    player->input = (PlayerInput){0};
    player->vx = player->vy = 0;
    memset(player->weapons, 0, sizeof(player->weapons));
    game->menu.ready[slot] = false;
    game->menu.shop_fly_frames[slot] = 0;
    game->menu.shop_purchase[slot] = false;
    game->menu.players = game->players[1].joined ? 2 : 1;
    for (int i = 0; i < GAME_BULLETS; ++i) if (game->bullets[i].owner == slot) game->bullets[i].active = false;
    if (!game->players[0].joined && !game->players[1].joined) {
        if (game->transition == TRANSITION_NEW_GAME || game->transition == TRANSITION_CONTINUE ||
            game->transition == TRANSITION_RETRY || game->transition == TRANSITION_STAGE) {
            game->transition = TRANSITION_NONE;
            game->transition_frames = 0;
        }
        if (game->menu.page == MENU_NONE) game_menu_open(game, MENU_PAUSE);
    }
    return true;
}

void game_reset(Game *game, uint32_t seed) {
    memset(game, 0, sizeof(*game));
    game->seed = seed;
    game_random_init(&game->random, seed);
    game_stats_init(game);
    game->stage.camera_y = 400;
    game->stage.previous_line = 100000;
    game->stage.music_event = -1;
    game->scroll_speed = stage_definitions[0].scroll_speed;
    game->waves = true;
    game->menu.players = 1;
    for (int i = 0; i < 3; ++i) game->menu.options[i] = 10;
    game_join(game, 0);
    game_scenery_init(game);
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
        enemy->health = (kind < ENEMY_AIRSHIP ? health[kind] : 1) * health_scale[game->difficulty];
        if (kind == ENEMY_BOSS || kind == ENEMY_BOSS2 || kind == ENEMY_BOSS_CHUNK) enemy->health *= boss_scale[game->difficulty];
        enemy->health_max = enemy->health;
        enemy->radius = kind < ENEMY_AIRSHIP ? radius[kind] : 32;
        enemy->coins = kind < ENEMY_AIRSHIP ? coins[kind] : 3;
        if (kind == ENEMY_BOSS_CHUNK) enemy->box_width = enemy->box_height = 120;
        if (kind == ENEMY_BOSS2) { enemy->box_width = 420; enemy->box_height = 160; }
        enemy->parent = enemy->target = -1;
        for (int n = 0; n < 8; ++n) enemy->children[n] = -1;
        enemy->move_frame = -1;
        enemy->move_speed = enemy->move_transition = 1;
        if (kind >= ENEMY_AIRSHIP) enemy_init_extended(game, enemy);
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
        bullet->friction = bullet->scale = 1;
        bullet->ignore_enemy = bullet->target = -1;
        return i;
    }
    ++game->dropped_spawns;
    return -1;
}

void game_damage_player(Game *game, int slot, float damage) {
    if (game->seeking || slot < 0 || slot >= GAME_PLAYERS || damage <= 0 || !isfinite(damage)) return;
    Player *player = &game->players[slot];
    if (!player->joined || player->lives <= 0 || player->invulnerable > 0 || player->invincible) return;
    float shield_damage = fminf(player->shield_health, damage);
    player->shield_health -= shield_damage;
    player->health -= damage - shield_damage;
    ++game->hits;
    if (player->health > 0) return;
    player->health = 0;
    ++game->deaths;
    game_effect(game, EFFECT_PLAYER_DEATH, player->x, player->y, slot, 0, 0);
    player->lives = 0;
    memset(&player->input, 0, sizeof(player->input));
}

/* Ship.TakeDamage: ordinary impacts apply chassis resistance; direct damage does not. */
static void player_impact(Game *game, int slot, float damage, float x, float y, float vx, float vy) {
    Player *player = &game->players[slot];
    if (game->seeking || !player->joined || player->lives <= 0 || player->invulnerable > 0 || player->invincible) return;
    float adjusted = damage * (1 - game_chassis[player->chassis].collision_resistance * 0.25f);
    float effect_x = player->x + (x - player->x) * 0.35f;
    float effect_y = player->y + (y - player->y) * 0.35f;
    bool shield = player->shield_health > adjusted;
    float speed = sqrtf(vx * vx + vy * vy);
    game_damage_player(game, slot, adjusted);
    if (shield) {
        player->shield_hit_frames = 4;
        ++game->shield_hits;
        game_effect(game, EFFECT_SHIELD_HIT, effect_x, effect_y, slot, 0, 0);
    } else {
        ++game->armor_hits;
        game_effect(game, EFFECT_ARMOR_HIT, effect_x, effect_y, slot,
            speed > 0 ? -2 * vx / speed : 0, speed > 0 ? -2 * vy / speed : 0);
        player->vx = player->vx * 0.15f + vx * 0.20f * adjusted;
        player->vy = player->vy * 0.15f + vy * 0.20f * adjusted;
    }
}

bool game_upgrade_weapon(Game *game, int slot, int level) {
    if (slot < 0 || slot >= GAME_PLAYERS || level < 0 || level > 7 || !game->players[slot].joined) return false;
    game->players[slot].weapon_level = level;
    game->players[slot].powerup_levels = 0;
    return true;
}

int game_health_score(const Game *game, float health, float multiplier) {
    static const float scale[] = {1, 1.1f, 1.25f};
    int score = (int)(health * multiplier * scale[game->difficulty]);
    return score - score % 10;
}

void game_drop_pickups(Game *game, float x, float y, int coins, bool powerup, bool combine) {
    int big = combine ? coins / 10 : 0;
    int small = coins - big * 10;
    int count = big + small + (powerup ? 1 : 0);
    for (int n = 0; n < count; ++n) {
        int slot = -1;
        for (int i = 0; i < GAME_PICKUPS; ++i) if (!game->pickups[i].active) { slot = i; break; }
        if (slot < 0) { ++game->dropped_spawns; return; }
        Pickup *p = &game->pickups[slot];
        *p = (Pickup){0};
        p->active = true;
        p->kind = n < big ? 1 : n < big + small ? 0 : 2;
        ++game->stage.stats.coins_total;
        p->x = x; p->y = y;
        float angle = random_unit(game) * 6.2831853f;
        p->vx = cosf(angle) * 3; p->vy = sinf(angle) * 3;
        p->friction = 0.95f + random_unit(game) * 0.03f;
    }
}

static void update_pickups(Game *game) {
    for (int i = 0; i < GAME_PICKUPS; ++i) {
        Pickup *pickup = &game->pickups[i];
        if (!pickup->active) continue;
        ++pickup->age;
        pickup->x += pickup->vx; pickup->y += pickup->vy;
        pickup->vx *= pickup->friction * 0.97f;
        pickup->vy = pickup->vy * pickup->friction * 0.97f + 0.06f;
        int nearest = -1;
        float distance = 1e20f;
        for (int slot = 0; slot < GAME_PLAYERS; ++slot) {
            Player *p = &game->players[slot];
            if (!p->joined || p->lives <= 0) continue;
            float length = hypotf(p->x - pickup->x, p->y - pickup->y);
            if (length < distance) { nearest = slot; distance = length; }
        }
        if (nearest >= 0 && !game->seeking) {
            Player *p = &game->players[nearest];
            if (pickup->age >= 20 && distance < 275) pickup->magnet = true;
            if (pickup->magnet && distance > 0) {
                pickup->force += 2;
                float power = fmaxf(pickup->force, 275 - distance);
                float dx = (p->x - pickup->x) / distance, dy = (p->y - pickup->y) / distance;
                pickup->x += dx * power * 0.04f; pickup->y += dy * power * 0.04f;
                pickup->vx += dx * power * 0.0125f; pickup->vy += dy * power * 0.0125f;
            }
            float radius = p->shield_health > 0 ? 24 : PLAYER_ARMOR_RADIUS;
            float dx = p->x - pickup->x, dy = p->y - pickup->y;
            if (dx * dx + dy * dy < 16 * 16 + radius * radius) {
                if (pickup->kind == 2) {
                    if (p->weapon_level + 1 < game_weapons[p->primary].level_count) { ++p->weapon_level; ++p->powerup_levels; }
                    memset(p->weapons[0], 0, sizeof(p->weapons[0]));
                    memset(p->weapons[1], 0, sizeof(p->weapons[1]));
                    /* Retire continuous shots before the upgraded emitters
                       toggle on; old beams must not survive as duplicates. */
                    for (int i = 0; i < GAME_BULLETS; ++i) {
                        Bullet *b = &game->bullets[i];
                        if (b->active && b->owner == nearest &&
                            (b->weapon == WEAPON_BEAM || b->weapon == WEAPON_BEAM_FOCUS)) b->active = false;
                    }
                } else {
                    game->stage.stats.coins_collected += pickup->kind == 1 ? 10 : 1;
                    p->score += pickup->kind == 1 ? 250 : 15 + (p->pilot == 1 ? 3 : 0);
                    if (pickup->kind == 1) p->energy = fminf(game_generators[p->generator].capacity,
                        p->energy + game_generators[p->generator].on_coin);
                }
                pickup->active = false;
                ++game->pickups_collected;
            }
        } else pickup->magnet = false;
        if (pickup->age <= 20 || pickup->magnet) {
            pickup->x = clamp(pickup->x, game->stage.camera_x, game->stage.camera_x + GAME_WIDTH);
            pickup->y = fminf(pickup->y, GAME_HEIGHT);
        }
        if (pickup->y > GAME_HEIGHT + 32) pickup->active = false;
    }
}

static void update_players(Game *game) {
    for (int i = 0; i < GAME_PLAYERS; ++i) {
        Player *player = &game->players[i];
        if (!player->joined || player->lives <= 0) continue;
        ++player->age;
        if (player->shield_hit_frames > 0) --player->shield_hit_frames;
        if (player->invulnerable > 0) --player->invulnerable;
        float x = player->input.x, y = player->input.y;
        float length = sqrtf(x * x + y * y);
        if (length > 1) { x /= length; y /= length; }
        /* Rookie acceleration and friction from legacy/Ship/ShipDefinitions.cs. */
        const Chassis *chassis = &game_chassis[player->chassis];
        float acceleration = chassis->speed * (player->pilot == 0 ? 1.2f : 1);
        player->vx += x * acceleration;
        player->vy += y * acceleration;
        /* Ship.UpdateInput applies movement force before firing. */
        game_update_weapons(game, i);
        float target_friction = chassis->friction * (player->input.focus ? 0.6f : 1);
        player->friction += (target_friction - player->friction) * (player->input.focus ? 0.2f : 0.4f);
        float radius = player->shield_health > 0 ? 24 : PLAYER_ARMOR_RADIUS;
        float left = game->stage.camera_x + radius, right = game->stage.camera_x + GAME_WIDTH - radius;
        player->x = clamp(player->x + player->vx, left, right);
        player->y = clamp(player->y + player->vy, radius, GAME_HEIGHT - radius);
        player->vx *= player->friction;
        player->vy *= player->friction;
        if (player->input.focus) game_effect(game, EFFECT_FOCUS, player->x, player->y, i, player->vx, player->vy - 2.5f);
        if ((player->x <= left && player->vx < 0) || (player->x >= right && player->vx > 0)) player->vx = 0;
        if ((player->y <= radius && player->vy < 0) || (player->y >= GAME_HEIGHT - radius && player->vy > 0)) player->vy = 0;
        /* Ship.Update binds the camera to each living ship in update order. */
        if (!game->stage.secret_countdown)
            game->stage.camera_x = (player->x - GAME_WIDTH * 0.5f) / (GAME_WIDTH * 0.5f) * 50;
    }
}

static void update_enemies(Game *game) {
    for (int i = 0; i < GAME_ENEMIES; ++i) {
        Enemy *enemy = &game->enemies[i];
        if (!enemy->active) continue;
        enemy_update(game, enemy);
        if (!enemy->active) continue;
        for (int p = 0; p < GAME_PLAYERS; ++p) {
            Player *player = &game->players[p];
            float player_radius = player->shield_health > 0 ? 24 : PLAYER_ARMOR_RADIUS;
            float dx = player->x - enemy->x - enemy->collision_x, dy = player->y - enemy->y - enemy->collision_y;
            bool touching;
            if (enemy->box_width > 0) {
                dx = fmaxf(fabsf(dx) - enemy->box_width * 0.5f, 0);
                dy = fmaxf(fabsf(dy) - enemy->box_height * 0.5f, 0);
                touching = dx * dx + dy * dy <= player_radius * player_radius;
            } else {
                /* Collision.cs deliberately uses r1*r1 + r2*r2, not (r1+r2)^2. */
                touching = dx * dx + dy * dy < enemy->radius * enemy->radius + player_radius * player_radius;
            }
            if (!game->seeking && player->joined && player->lives > 0 && touching && !enemy->intangible && !enemy->invisible && enemy->kind != ENEMY_BLACK_HOLE && enemy->kind != ENEMY_GLOB && enemy->kind != ENEMY_BOSS10_BALL) {
                if (player->invulnerable > 0 && !enemy_is_asteroid(enemy->kind)) continue;
                float damage = enemy_is_asteroid(enemy->kind) ? 1 : enemy->kind == ENEMY_FENCE_BEAM ? 2 : fminf(2.5f + enemy->health_max, 6);
                player->energy += damage * game_generators[player->generator].on_collision;
                float adjusted = damage * (1 - game_chassis[player->chassis].collision_resistance);
                float source_x = enemy->kind == ENEMY_FENCE_BEAM ? player->x : enemy->x;
                float dx = player->x - source_x, dy = player->y - enemy->y, length = hypotf(dx, dy);
                if (length > 0.00001f && !player->invincible) {
                    player->vx = player->vx * 0.8f + dx / length * 3.5f * adjusted;
                    player->vy = player->vy * 0.8f + dy / length * 3.5f * adjusted;
                }
                player_impact(game, p, adjusted, source_x, enemy->y, player->vx, player->vy);
                if (enemy->kind != ENEMY_FENCE_BEAM)
                    enemy_take_damage(game, enemy, enemy_is_asteroid(enemy->kind) ? 1 : player->invincible ? 10 : 0.5f, p);
                if (enemy->kind == ENEMY_SPIKE) {
                    for (int color = 2; color <= 3; ++color)
                        game_particle(game, PARTICLE_ENEMY_DEATH_EXPLOSION, enemy->x, enemy->y, color, 1.25f, enemy->vx * 0.25f, enemy->vy * 0.25f);
                    player_impact(game, p, 7.5f, enemy->x, enemy->y, enemy->vx, enemy->vy);
                    enemy_die(game, enemy, -1);
                }
                if (!enemy->active) break;
            }
        }
        if (enemy_is_asteroid(enemy->kind)) continue;
        if (enemy->y > GAME_HEIGHT + 200 || enemy->y < -240 || enemy->x < game->stage.camera_x - 150 || enemy->x > game->stage.camera_x + GAME_WIDTH + 150) enemy->active = false;
    }
}

static void update_bullets(Game *game) {
    for (int i = 0; i < GAME_BULLETS; ++i) {
        Bullet *bullet = &game->bullets[i];
        if (!bullet->active) continue;
        game_update_projectile(game, bullet);
        if (!bullet->active) continue;
        bullet->x += bullet->vx;
        bullet->y += bullet->vy + game->scroll_speed;
    }
    /* Legacy collects collisions only after all entities have updated. */
    for (int i = 0; i < GAME_BULLETS; ++i) {
        Bullet *bullet = &game->bullets[i];
        if (!bullet->active) continue;
        float old_x = bullet->x, old_y = bullet->y;
        if (bullet->owner >= 0) {
            for (int n = 0; n < GAME_BULLETS && bullet->active; ++n) {
                Bullet *hostile = &game->bullets[n];
                if (!hostile->active || hostile->owner >= 0 ||
                    (hostile->hostile_kind != HOSTILE_PELLET && hostile->hostile_kind != HOSTILE_MISSILE)) continue;
                if (bullet->weapon == WEAPON_CHARGE_SHOT) continue;
                if (hostile->hostile_kind == HOSTILE_MISSILE &&
                    (bullet->splash || bullet->weapon == WEAPON_BLADE || bullet->weapon == WEAPON_BEAM ||
                     bullet->weapon == WEAPON_BEAM_FOCUS || bullet->weapon == WEAPON_BOOMERANG ||
                     bullet->weapon == WEAPON_VULCAN || bullet->weapon == WEAPON_DRUNK_MISSILE ||
                     bullet->weapon == WEAPON_MISSILE_VOLLEY || bullet->weapon == WEAPON_LIGHTNING ||
                     bullet->weapon == WEAPON_LIGHTNING_FOCUS)) continue;
                const Enemy circle = {.x = hostile->x, .y = hostile->y, .radius = hostile->radius};
                bool hit = bullet->radius > 0 ?
                    segment_distance_squared(old_x, old_y, bullet->x, bullet->y, hostile->x, hostile->y) <
                        hostile->radius * hostile->radius + bullet->radius * bullet->radius :
                    laser_hits_circle(old_x, old_y, bullet->x, bullet->y, &circle);
                if (bullet->weapon == WEAPON_BEAM) hit = swept_circle_box(hostile->x, hostile->y, hostile->x, hostile->y,
                    bullet->x, bullet->y - 540, bullet->width * 0.5f, 540, hostile->radius);
                if (!hit) continue;
                if (hostile->hostile_kind == HOSTILE_PELLET) {
                    if (--hostile->health <= 0 || bullet->weapon == WEAPON_BOMBLET) hostile->active = false;
                } else if (bullet->weapon == WEAPON_BOMBLET) {
                    hostile->active = false;
                    game_particle(game, PARTICLE_WEAPON_MISSILE_HIT, hostile->x, hostile->y, 2, 1, 0, 0);
                }
                if (hostile->hostile_kind == HOSTILE_PELLET && (bullet->splash || bullet->weapon == WEAPON_BOMBLET ||
                    bullet->weapon == WEAPON_BEAM || bullet->weapon == WEAPON_BEAM_FOCUS)) continue;
                bullet->active = false;
                game_projectile_hit(game, bullet, -1);
                if (hostile->hostile_kind == HOSTILE_PELLET &&
                    (bullet->weapon == WEAPON_BLADE || bullet->weapon == WEAPON_LIGHTNING || bullet->weapon == WEAPON_LIGHTNING_FOCUS))
                    bullet->active = false; /* Pellet handlers consume these instead of piercing/chaining. */
            }
            if (!bullet->active) continue;
            for (int e = 0; e < GAME_ENEMIES; ++e) {
                Enemy *enemy = &game->enemies[e];
                bool hit = enemy->box_width > 0 ?
                    segment_box(old_x + 0.5f, old_y + 0.25f, bullet->x + 0.5f, bullet->y + 0.25f, enemy->x + enemy->collision_x, enemy->y + enemy->collision_y,
                        enemy->box_width * 0.5f + 0.5f, enemy->box_height * 0.5f + 0.25f) :
                    laser_hits_circle(old_x, old_y, bullet->x, bullet->y, enemy);
                if (bullet->radius > 0) hit = enemy->box_width > 0 ?
                    swept_circle_box(old_x, old_y, bullet->x, bullet->y, enemy->x + enemy->collision_x, enemy->y + enemy->collision_y,
                        enemy->box_width * 0.5f, enemy->box_height * 0.5f, bullet->radius) :
                    segment_distance_squared(old_x, old_y, bullet->x, bullet->y, enemy->x + enemy->collision_x, enemy->y + enemy->collision_y) <
                        bullet->radius * bullet->radius + enemy->radius * enemy->radius;
                if (bullet->weapon == WEAPON_BEAM) hit = enemy->box_width > 0 ?
                    segment_box(bullet->x, bullet->y, bullet->x, bullet->y - 1080,
                        enemy->x + enemy->collision_x, enemy->y + enemy->collision_y,
                        enemy->box_width * 0.5f + bullet->width * 0.5f, enemy->box_height * 0.5f) :
                    swept_circle_box(enemy->x + enemy->collision_x, enemy->y + enemy->collision_y,
                        enemy->x + enemy->collision_x, enemy->y + enemy->collision_y,
                        bullet->x, bullet->y - 540, bullet->width * 0.5f, 540, enemy->radius);
                if (!enemy->active || enemy->intangible || enemy->invisible || enemy->kind == ENEMY_BLACK_HOLE || enemy->kind == ENEMY_FENCE_BEAM || !hit || bullet->ignore_enemy == e) continue;
                if (enemy_is_asteroid(enemy->kind)) {
                    int w = bullet->weapon;
                    bool accepts = w == WEAPON_FRONT_LASER || w == WEAPON_FRONT_LASER_FOCUS ||
                        w == WEAPON_SPREAD_LASER || w == WEAPON_SPREAD_LASER_FOCUS || w == WEAPON_LASER_SHOT ||
                        w == WEAPON_MISSILE || w == WEAPON_MISSILE_LAUNCHER || w == WEAPON_PLASMA ||
                        w == WEAPON_PLASMA_FOCUS || w == WEAPON_MINI_SHOT || w == WEAPON_CHARGE_SHOT;
                    if (!accepts || bullet->splash) continue;
                    enemy->vx += cosf(bullet->rotation) * bullet->damage;
                    enemy->vy += sinf(bullet->rotation) * bullet->damage;
                }
                if (bullet->weapon == WEAPON_SEEK_BOMB && (enemy->kind == ENEMY_FENCE || enemy->kind == ENEMY_BOSS10_BALL)) continue;
                bullet->active = false;
                ++game->projectile_hits;
                bool boss = enemy_is_boss(enemy->kind);
                enemy_take_damage(game, enemy, bullet->damage, bullet->owner);
                if ((boss && !enemy->active) || (game->boss_spawned && game_enemy_count(game) == 0)) return;
                game_projectile_hit(game, bullet, e);
                if (!bullet->active || bullet->weapon == WEAPON_LIGHTNING || bullet->weapon == WEAPON_LIGHTNING_FOCUS) break;
            }
            if (bullet->active) {
                const StageDefinition *stage = &stage_definitions[game->stage.index];
                for (int i = 0; i < stage->event_count; ++i) {
                    const StageEvent *event = &stage->events[i];
                    if (event->kind != STAGE_BUILDING || !game->stage.activated[i] || game->stage.building_health[i] < 0) continue;
                    if (bullet->ignore_enemy == GAME_ENEMIES + i) continue;
                    float x = event->x + GAME_WIDTH * 0.5f, y = event->y - game->stage.camera_y + GAME_HEIGHT * 0.5f;
                    bool hit = bullet->radius > 0 ? swept_circle_box(bullet->x, bullet->y, bullet->x, bullet->y,
                        x, y, event->width * 0.4f, event->height * 0.4f, bullet->radius) :
                        segment_box(bullet->x + 0.5f, bullet->y + 0.25f, bullet->x + 0.5f, bullet->y + 0.25f,
                            x, y, event->width * 0.4f + 0.5f, event->height * 0.4f + 0.25f);
                    if (bullet->weapon == WEAPON_BEAM) hit = segment_box(bullet->x, bullet->y - 1080, bullet->x, bullet->y,
                        x, y, event->width * 0.4f + bullet->width * 0.5f, event->height * 0.4f);
                    if (!hit) continue;
                    game->stage.building_health[i] -= bullet->damage;
                    bullet->active = false;
                    ++game->projectile_hits;
                    if (game->stage.building_health[i] < 0) {
                        /* Keep the original strict <0 destruction boundary. */
                        ++game->building_kills;
                        ++game->stage.stats.building_kills;
                        static const float big_health[] = {2.5f,2.5f,3,5,5,6,7,8,9};
                        static const float small_health[] = {1,1.5f,1.5f,2,2.5f,5,6};
                        bool small = !strncmp(event->name, "Small", 5);
                        int number = event->name[strlen(event->name) - 1] - '1';
                        game->players[bullet->owner].score += game_health_score(game,
                            small ? small_health[number] : big_health[number], 15);
                        game_drop_pickups(game, x, y, event->coins, event->powerup, false);
                        float angle = random_unit(game) * 6.2831853f;
                        game_effect(game, !strncmp(event->name, "Small", 5) ? EFFECT_BUILDING_SMALL : EFFECT_BUILDING_BIG,
                            x, y, 0, cosf(angle) * 0.75f, sinf(angle) * 0.75f);
                    }
                    game_projectile_hit(game, bullet, GAME_ENEMIES + i);
                    if (!bullet->active || bullet->weapon == WEAPON_LIGHTNING || bullet->weapon == WEAPON_LIGHTNING_FOCUS) break;
                }
            }
        } else {
            for (int p = 0; p < GAME_PLAYERS; ++p) {
                Player *player = &game->players[p];
                float radius = player->shield_health > 0 ? 24 : PLAYER_ARMOR_RADIUS;
                if (!player->joined || player->lives <= 0 || player->invulnerable > 0 || game->seeking || bullet->collide_wait > 0) continue;
                bool hit = segment_distance_squared(bullet->x, bullet->y, bullet->x, bullet->y,
                    player->x, player->y) < radius * radius + bullet->radius * bullet->radius;
                if (bullet->hostile_kind == HOSTILE_LASER || bullet->hostile_kind == HOSTILE_CANNON) {
                    bool cannon = bullet->hostile_kind == HOSTILE_CANNON;
                    hit = swept_circle_box(player->x, player->y, player->x, player->y,
                        bullet->x + (cannon ? -0.5f : 0.5f), bullet->y + (cannon ? 0 : 0.25f),
                        cannon ? 5.5f : 0.5f, cannon ? 21 : 0.25f, radius);
                }
                if (!hit) continue;
                player_impact(game, p, bullet->damage, bullet->x, bullet->y, bullet->vx, bullet->vy);
                if (bullet->hostile_kind == HOSTILE_CANNON) {
                    float dx = player->x - bullet->x, dy = player->y - bullet->y, length = hypotf(dx, dy);
                    if (length > 0) { player->vx += dx / length * 5; player->vy += dy / length * 5; }
                    bullet->collide_wait = 3;
                    if (--bullet->health <= 0) bullet->active = false;
                    break;
                }
                game_particle(game, bullet->hostile_kind == HOSTILE_MISSILE ? PARTICLE_WEAPON_MISSILE_HIT :
                    bullet->hostile_kind == HOSTILE_MINI ? PARTICLE_ENEMY_MINI_SHOT_HIT : PARTICLE_ENEMY_SHOT_HIT,
                    bullet->x, bullet->y, 2, 1, 0, 0);
                bullet->active = false;
                break;
            }
        }
        if (bullet->x < game->stage.camera_x - 100 || bullet->x > game->stage.camera_x + GAME_WIDTH + 100 || bullet->y < -250 || bullet->y > GAME_HEIGHT + 100) bullet->active = false;
    }
}

void game_preview_tick(Game *preview) {
    ++preview->tick;
    update_effects(preview);
    /* A single practice target crosses the firing lanes between quiet intervals. */
    if (preview->tick % (3 * GAME_HZ) == GAME_HZ && !game_enemy_count(preview) &&
        (preview->tick == GAME_HZ || game_random_float(preview) < 0.7f)) {
        bool from_left = game_random_float(preview) < 0.5f;
        float x = GAME_WIDTH * (from_left ? 0.30f : 0.70f);
        float y = 180 + game_random_float(preview) * 180;
        int index = game_spawn_enemy(preview, ENEMY_BALL, x, y);
        if (index >= 0) {
            Enemy *target = &preview->enemies[index];
            target->vx = from_left ? 2 : -2;
            target->vy = 0.6f;
            target->coins = 0;
        }
    }
    for (int i = 0; i < GAME_PLAYERS; ++i) {
        Player *p = &preview->players[i];
        if (!p->joined) continue;
        ++p->age;
        p->input.fire = p->age >= 15;
        bool charge = p->sidekick != WEAPON_CHARGE_SHOT ||
            p->weapons[3][0].charge < game_weapons[WEAPON_CHARGE_SHOT].levels[p->sidekick_level].emitters[0].charge_frames;
        p->input.sidekick_left = p->input.sidekick_right = charge;
        if (p->age >= 15) game_update_weapons(preview, i);
    }
    update_enemies(preview);
    update_bullets(preview);
}

void game_tick(Game *game) {
    if (game_transition_update(game)) return;
    if (game->menu.page == MENU_SHOP) { game_shop_update(game); return; }
    if (game->menu.page == MENU_FINISH) { ++game->menu.frame; return; }
    if (game->paused && !game->over && !game->complete) return;
    if (game->secret_fade_frames > 0) --game->secret_fade_frames;
    update_effects(game);
    if (game->over) {
        if (game->over_frames < 120 && ++game->over_frames == 120) game_menu_open(game, MENU_OVER);
        return;
    }
    if (game->complete) {
        if (game->secret_return.active) {
            if (++game->secret_finish_frames > 38) game_secret_return(game);
            return;
        }
        if (game->menu.page == MENU_CLEAR) {
            if (game->clear_exit && game->menu.frame > 440) { game_shop_open(game); return; }
            if (game->clear_exit || game->menu.frame <= 440) ++game->menu.frame;
            if (!game->clear_exit && (game->menu.frame == 240 || game->menu.frame == 260)) {
                static const int skill_bonus[] = {0, 0, 250, 500, 1000, 1500};
                int bonus = game->menu.frame == 240 ? 10000 : skill_bonus[game_stage_medal(game)];
                if (game->players[0].joined && game->players[1].joined) bonus = bonus * 3 / 4;
                for (int i = 0; i < GAME_PLAYERS; ++i) if (game->players[i].joined) game->players[i].score += bonus;
                if (game->menu.frame == 260) {
                    game_record_stage(game);
                    game->menu.next_stage = game->stage.index < 11 ? game->stage.index + 1 : 0;
                    game_checkpoint(game, true);
                }
            }
            for (int i = 0; i < GAME_PLAYERS; ++i) {
                Player *p = &game->players[i];
                if (!p->joined || p->lives <= 0) continue;
                p->vy -= 0.1f * game->menu.frame;
                game_particle(game, PARTICLE_PLAYER_STAGE_END_SHIP_TRAIL, p->x, p->y, i, 1, 0, 1);
                p->x += p->vx; p->y += p->vy;
                p->vx *= p->friction; p->vy *= p->friction;
            }
        }
        return;
    }
    ++game->tick;
    if (game->waves) game_stage_update(game);
    else game->scroll_speed = 0;
    game_scenery_update(game);
    if (!game->seeking) update_players(game);
    else for (int p = 0; p < GAME_PLAYERS; ++p)
        if (game->players[p].joined && game->players[p].lives > 0)
            game->stage.camera_x = (game->players[p].x - GAME_WIDTH * 0.5f) / (GAME_WIDTH * 0.5f) * 50;
    update_enemies(game);
    update_bullets(game);
    update_pickups(game);
    bool alive = false;
    for (int p = 0; p < GAME_PLAYERS; ++p) if (game->players[p].joined && game->players[p].lives > 0) alive = true;
    if (!alive && !game->seeking) game->over = true;
    if (game->complete && game->secret_return.active) game->secret_finish_frames = 1;
    else if (game->complete && !game->over) {
        if (game->stage.index == 11 && !game->cleared_game) {
            game_transition(game, TRANSITION_FINISH, 0);
        } else game_menu_open(game, MENU_CLEAR);
    }
}

int game_enemy_count(const Game *game) {
    int count = 0;
    for (int i = 0; i < GAME_ENEMIES; ++i) count += game->enemies[i].active && !enemy_is_asteroid(game->enemies[i].kind);
    return count;
}

int game_bullet_count(const Game *game) {
    int count = 0;
    for (int i = 0; i < GAME_BULLETS; ++i) count += game->bullets[i].active;
    return count;
}

bool game_stage(Game *game, int index, float progress) {
    if (index < 0 || index >= STAGE_COUNT || !isfinite(progress) || progress < 0 || progress > 1) return false;
    Player players[GAME_PLAYERS];
    memcpy(players, game->players, sizeof(players));
    GameMenu menu = game->menu;
    int difficulty = game->difficulty, revision = game->stage.revision + 1;
    game_reset_session(game, game->seed);
    game->difficulty = difficulty;
    game->menu = menu;
    memcpy(game->players, players, sizeof(players));
    game->stage.index = index;
    game_random_init(&game->random, game->seed);
    game_stats_init(game);
    game_scenery_init(game);
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
            game->players[i].lives = 1;
            game->players[i].health = game_chassis[game->players[i].chassis].armor;
            game->players[i].invulnerable = 0;
        }
    }
    game->over = false;
    return true;
}
