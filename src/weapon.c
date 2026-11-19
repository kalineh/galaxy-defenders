#include "game.h"
#include <math.h>

#define PI 3.141592654f

static float random_float(Game *game) { return game_random_float(game); }
static float random_signed(Game *game) { return game_random_signed(game); }

static bool seekable(const Game *game, const Enemy *e) {
    return e->active && !enemy_is_asteroid(e->kind) && e->health > 0 && !e->intangible && !e->invisible && e->kind != ENEMY_BLACK_HOLE &&
        e->kind != ENEMY_FENCE && e->kind != ENEMY_FENCE_BEAM && e->kind != ENEMY_BOSS10_BALL &&
        e->x >= game->stage.camera_x && e->x <= game->stage.camera_x + GAME_WIDTH && e->y >= 0 && e->y <= GAME_HEIGHT;
}

static void target_position(const Game *game, int target, float *x, float *y) {
    if (target < GAME_ENEMIES) { *x = game->enemies[target].x; *y = game->enemies[target].y; }
    else {
        const StageEvent *event = &stage_definitions[game->stage.index].events[target - GAME_ENEMIES];
        *x = event->x + GAME_WIDTH * 0.5f;
        *y = event->y - game->stage.camera_y + GAME_HEIGHT * 0.5f;
    }
}

static int nearest_enemy(const Game *game, const Bullet *b, bool forward, bool unclaimed) {
    int result = -1;
    float nearest = 1e20f;
    for (int i = 0; i < GAME_ENEMIES; ++i) {
        const Enemy *e = &game->enemies[i];
        if (!e->active || enemy_is_asteroid(e->kind) || i == b->ignore_enemy || e->y < 0 || e->y > GAME_HEIGHT ||
            e->x < game->stage.camera_x || e->x > game->stage.camera_x + GAME_WIDTH) continue;
        if (!forward && !seekable(game, e)) continue;
        if (unclaimed) {
            bool claimed = false;
            for (int j = 0; j < GAME_BULLETS; ++j)
                if (&game->bullets[j] != b && game->bullets[j].active && game->bullets[j].owner >= 0 &&
                    game->bullets[j].weapon == WEAPON_SEEK_BOMB && game->bullets[j].target == i) { claimed = true; break; }
            if (claimed) continue;
        }
        float dx = e->x - b->x, dy = e->y - b->y, distance = dx * dx + dy * dy;
        if (forward && dx * b->vx + dy * b->vy < 0) continue;
        if (distance < nearest) { nearest = distance; result = i; }
    }
    if (forward) {
        const StageDefinition *stage = &stage_definitions[game->stage.index];
        for (int i = 0; i < stage->event_count; ++i) {
            if (stage->events[i].kind != STAGE_BUILDING || !game->stage.activated[i] || game->stage.building_health[i] < 0 ||
                b->ignore_enemy == GAME_ENEMIES + i) continue;
            float x, y; target_position(game, GAME_ENEMIES + i, &x, &y);
            if (x < game->stage.camera_x || x > game->stage.camera_x + GAME_WIDTH || y < 0 || y > GAME_HEIGHT) continue;
            float dx = x - b->x, dy = y - b->y, distance = dx * dx + dy * dy;
            if (dx * b->vx + dy * b->vy < 0) continue;
            if (distance < nearest) { nearest = distance; result = GAME_ENEMIES + i; }
        }
    }
    return result;
}

static void spawn(Game *game, int slot, int kind, int group, int emitter, const WeaponEmitter *e, int charge, int barrels) {
    Player *p = &game->players[slot];
    float side = group == 3 ? -40 : group == 4 ? 40 : 0;
    if (kind == WEAPON_VULCAN) side += (emitter - (barrels - 1) * 0.5f) * 12;
    float angle = -PI * 0.5f + e->rotation + random_signed(game) * e->random_rotation;
    float damage = e->damage * (p->pilot == 2 ? 1.15f : 1);
    if (kind == WEAPON_CHARGE_SHOT) damage *= fminf((float)charge / e->charge_frames, 1);
    if (kind == WEAPON_VULCAN || kind == WEAPON_BOOMERANG) angle += random_signed(game) * e->spray_angle;
    if (kind == WEAPON_FLAME || kind == WEAPON_FLAME_FOCUS) {
        WeaponState *state = &p->weapons[group][emitter];
        angle = state->fire_rotation + e->start_rotation + random_signed(game) * e->spray_angle;
        state->fire_rotation = p->age * e->fire_rotation_speed;
    }
    if (kind == WEAPON_BOMBLET) angle = random_float(game) * 2 * PI;
    int index = game_spawn_bullet(game, slot, p->x + e->offset_y + side, p->y - e->offset_x - (group == 0 ? 20 : 0),
        cosf(angle) * e->speed, sinf(angle) * e->speed, damage);
    if (index < 0) return;
    Bullet *b = &game->bullets[index];
    b->weapon = kind; b->group = group; b->emitter = emitter;
    b->speed = e->speed; b->base_damage = damage; b->rotation = angle;
    b->direction_x = cosf(angle); b->direction_y = sinf(angle);
    b->offset_x = b->x - p->x; b->offset_y = b->y - p->y;
    b->bounces = e->bounces; b->lifetime = e->lifetime;
    b->friction = e->friction ? e->friction : 1;
    b->scale = e->scale_override ? e->scale_override : 1;
    b->target = b->ignore_enemy = -1;
    switch (kind) {
        case WEAPON_LIGHTNING: case WEAPON_LIGHTNING_FOCUS: b->radius = 16; break;
        case WEAPON_PLASMA: case WEAPON_PLASMA_FOCUS: b->radius = damage < 0.5f ? 2 : 38; break;
        case WEAPON_FLAME: case WEAPON_FLAME_FOCUS:
            b->radius = damage < 1 ? 16 : 30;
            b->rotation = random_signed(game) * PI;
            b->spin = random_signed(game) * PI * 0.05f;
            b->width = e->scale_override;
            b->vy -= game->scroll_speed * e->anti_camera_speed;
            p->vx *= 1.15f; p->vy *= 1.15f;
            break;
        case WEAPON_VULCAN: {
            b->radius = 1;
            int target = nearest_enemy(game, b, false, false);
            if (target >= 0) {
                const Enemy *enemy = &game->enemies[target];
                b->rotation = atan2f(enemy->y + 25*enemy->vy - b->y, enemy->x + 25*enemy->vx - b->x);
                b->direction_x = cosf(b->rotation);
                b->direction_y = sinf(b->rotation);
                b->vx = e->speed * b->direction_x;
                b->vy = e->speed * b->direction_y;
            }
            break;
        }
        case WEAPON_MINI_SHOT: case WEAPON_SEEK_BOMB: b->radius = 1; break;
        case WEAPON_BOMBLET:
            b->radius = 12; b->friction = 0.98f; b->lifetime = 481;
            b->vy -= game->scroll_speed * 3.5f;
            break;
        case WEAPON_BOOMERANG: b->radius = 16; b->rotation = random_signed(game) * PI; break;
        case WEAPON_CHARGE_SHOT: b->radius = 12 * damage; b->scale = 0.25f + damage * 0.5f; break;
        case WEAPON_BLADE: b->radius = 22 * b->scale; break;
        case WEAPON_BEAM: b->width = 8 * e->width; break;
        case WEAPON_BEAM_FOCUS: b->width = e->radius; break;
        case WEAPON_MISSILE: case WEAPON_MISSILE_LAUNCHER:
            b->width = e->radius;
            b->vx = b->direction_x * 6; b->vy = b->direction_y * 6;
            b->rotation -= PI;
            break;
        case WEAPON_DRUNK_MISSILE: case WEAPON_MISSILE_VOLLEY:
            b->curve_speed = 0.03f + random_float(game) * 0.03f;
            b->curve_magnitude = (game_random_float(game) < 0.5f ? -0.3f : 0.3f) + random_signed(game) * 0.2f;
            b->curve_offset = game_random_float(game) < 0.5f ? -1 : 1;
            b->rotation -= PI - random_signed(game) * 0.25f;
            b->base_rotation = b->rotation;
            break;
        default: break;
    }
    p->vy += e->kickback;
    ++game->shots;
    ++game->weapon_shots[kind];
}

static bool can_fire(const WeaponEmitter *e, const WeaponState *s) {
    return s->cooldown <= 0 && s->overheat == 0 &&
        (!e->charge_speed || (s->charge > 0 && (!e->auto_discharge || s->charge >= e->auto_discharge)));
}

static void fire_group(Game *game, int slot, int group, int kind, int level, bool held) {
    if (kind < 0 || kind >= WEAPON_COUNT) return;
    Player *p = &game->players[slot];
    const WeaponLevel *data = &game_weapons[kind].levels[level];
    bool sidekick = group >= 3;
    float cost = 0;
    for (int i = 0; i < data->count; ++i) {
        const WeaponEmitter *e = &data->emitters[i];
        WeaponState *s = &p->weapons[group][i];
        if (sidekick && held && s->cooldown <= 0) {
            int full = e->charge_frames ? e->charge_frames : 60;
            if (s->charge < full) {
                p->energy = fmaxf(0, p->energy - e->energy);
                ++s->charge;
            }
            if (!e->auto_discharge || s->charge != e->auto_discharge)
                game_particle(game, PARTICLE_WEAPON_CHARGE, p->x + e->offset_y + (group == 3 ? -40 : 40),
                    p->y - e->offset_x, slot, (float)s->charge / full, 0, 0);
        }
        if (held && !sidekick && can_fire(e, s)) cost += e->energy + e->drain;
    }
    bool enough = p->energy >= cost;
    if (held && !sidekick && enough) p->energy -= cost;
    for (int i = 0; i < data->count; ++i) {
        const WeaponEmitter *e = &data->emitters[i];
        WeaponState *s = &p->weapons[group][i];
        bool trigger = sidekick ? (!held || e->auto_discharge > 0) : held;
        if (!held || !enough) s->toggled = false;
        if (!enough && e->toggle) {
            if (!s->overheat) ++game->overheats;
            s->overheat = 60;
        }
        if (trigger && enough && can_fire(e, s)) {
            if (!s->toggled) spawn(game, slot, kind, group, i, e, s->charge, data->count);
            s->toggled = e->toggle;
            s->charge = 0;
            float remainder = kind == WEAPON_VULCAN ? s->cooldown : 0;
            s->cooldown = remainder + e->reload + random_float(game) * e->random_reload;
        }
        /* Ship.UpdateWeapons also drains toggled weapons each tick. */
        if (s->toggled) p->energy = fmaxf(0, p->energy - e->drain);
        if (kind == WEAPON_VULCAN && s->cooldown > 0) s->cooldown -= 1.0f / GAME_HZ;
        else s->cooldown = fmaxf(0, s->cooldown - 1.0f / GAME_HZ);
        if (s->overheat > 0) --s->overheat;
    }
}

bool game_auto_hold_sidekick(const Player *p) {
    return p->sidekick != WEAPON_CHARGE_SHOT ||
        p->weapons[3][0].charge < game_weapons[WEAPON_CHARGE_SHOT].levels[p->sidekick_level].emitters[0].charge_frames;
}

void game_update_weapons(Game *game, int slot) {
    Player *p = &game->players[slot];
    fire_group(game, slot, 0, p->primary, p->weapon_level, p->input.fire && !p->input.focus);
    fire_group(game, slot, 1, equipment_focus_weapon(p->primary), p->weapon_level, p->input.focus);
    fire_group(game, slot, 2, p->secondary, p->secondary_level, p->input.fire && !p->input.focus);
    fire_group(game, slot, 3, p->sidekick, p->sidekick_level, p->input.sidekick_left);
    fire_group(game, slot, 4, p->sidekick, p->sidekick_level, p->input.sidekick_right);
    for (int mode = 0; mode < 2; ++mode) p->weapon_cooldown[mode] = p->weapons[mode][0].cooldown;
    const Generator *generator = &game_generators[p->generator];
    p->energy = fminf(generator->capacity, p->energy + generator->regen);
    if (p->shield >= 0) {
        float reserve = 0;
        int kinds[] = {p->primary, p->secondary}, levels[] = {p->weapon_level, p->secondary_level};
        for (int k = 0; k < 2; ++k) if (kinds[k] >= 0) {
            const WeaponLevel *data = &game_weapons[kinds[k]].levels[levels[k]];
            for (int i = 0; i < data->count; ++i) reserve += data->emitters[i].energy;
        }
        const Shield *shield = &game_shields[p->shield];
        if (p->energy >= reserve * 2) {
            float used = fminf(p->energy, fminf(shield->drain, shield->capacity - p->shield_health));
            p->shield_health += used * shield->efficiency;
            p->energy -= used;
        }
    }
}

void game_update_projectile(Game *game, Bullet *b) {
    ++b->age;
    if (b->owner < 0) {
        if (b->collide_wait > 0) --b->collide_wait;
        if (b->hostile_kind == HOSTILE_MISSILE) {
            game_particle(game, PARTICLE_ENEMY_MISSILE_TRAIL, b->x - cosf(b->rotation) * 10,
                b->y - sinf(b->rotation) * 10, -1, 1, 0, 0);
            if (b->target >= 0 && game->players[b->target].lives <= 0) { b->target = -1; return; }
            int target = b->target;
            float nearest = 2000;
            for (int i = 0; target < 0 && i < GAME_PLAYERS; ++i) {
                const Player *p = &game->players[i];
                float d = hypotf(p->x-b->x,p->y-b->y);
                if (p->joined && p->lives > 0 && d < nearest) { b->target=i; nearest=d; }
            }
            target = b->target;
            if (target >= 0) {
                const Player *p=&game->players[target];
                float cross=(p->x-b->x)*-sinf(b->rotation)+(p->y-b->y)*cosf(b->rotation);
                b->rotation += cross > 0 ? 0.0035f : cross < 0 ? -0.0035f : 0;
                b->vx=cosf(b->rotation)*b->speed; b->vy=sinf(b->rotation)*b->speed;
                b->speed+=0.075f;
            }
        } else if (b->hostile_kind == HOSTILE_PELLET) b->rotation+=b->curve_speed;
        return;
    }
    Player *p = &game->players[b->owner];
    if ((b->splash && b->was_hit) || (b->lifetime > 0 && b->age > b->lifetime)) {
        b->active = false;
        if (b->weapon == WEAPON_BOMBLET) game_projectile_hit(game, b, -1);
        return;
    }
    switch (b->weapon) {
        case WEAPON_MISSILE: case WEAPON_MISSILE_LAUNCHER:
            b->vx += (-b->direction_x * b->speed - b->vx) * 0.05f;
            b->vy += (-b->direction_y * b->speed - b->vy) * 0.05f;
            if (b->vy < 0) {
                float length = hypotf(b->vx, b->vy);
                game_particle(game, PARTICLE_WEAPON_MISSILE_TRAIL, b->x - b->vx / length * 8,
                    b->y - b->vy / length * 8, b->owner, 1, -b->vx / length, -b->vy / length);
            }
            break;
        case WEAPON_DRUNK_MISSILE: case WEAPON_MISSILE_VOLLEY: {
            float curve = sinf(b->curve_offset + b->age * b->curve_speed) * b->curve_magnitude;
            float angle = b->base_rotation + (curve - b->base_rotation) * curve;
            b->rotation = angle;
            b->vx = cosf(angle) * b->speed; b->vy = sinf(angle) * b->speed;
            if (b->vy < 0) game_particle(game, PARTICLE_WEAPON_DRUNK_MISSILE_TRAIL,
                b->x - cosf(angle) * 4, b->y - sinf(angle) * 4, b->owner, 1, -cosf(angle), -sinf(angle));
            break;
        }
        case WEAPON_SEEK_BOMB: {
            if (b->target < 0) {
                b->target = nearest_enemy(game, b, false, true);
                if (b->target < 0) b->target = nearest_enemy(game, b, false, false);
            }
            if (b->target >= 0 && !seekable(game, &game->enemies[b->target])) b->target = -1;
            float dx = b->vx, dy = b->vy;
            if (b->age < 90) {
                dx = b->target < 0 ? 0 : game->enemies[b->target].x - b->x;
                dy = b->target < 0 ? -100 : game->enemies[b->target].y - b->y;
            }
            float length = hypotf(dx, dy);
            if (length > 0) { b->vx += (dx / length * b->speed - b->vx) * 0.15f; b->vy += (dy / length * b->speed - b->vy) * 0.15f; }
            b->rotation += 0.1f;
            break;
        }
        case WEAPON_FLAME: case WEAPON_FLAME_FOCUS:
            b->damage = b->base_damage * (1 - 0.5f * b->age / b->lifetime);
            b->rotation += b->spin;
            b->scale = b->width > 0 ? b->width : 0.5f + 0.5f * b->age / b->lifetime;
            break;
        case WEAPON_BOMBLET: b->y -= game->scroll_speed; b->vy -= 0.005f; b->rotation += 0.02f + b->age * 0.0001f; break;
        case WEAPON_BOOMERANG:
            b->rotation += 0.175f; b->radius = 16 * b->scale;
            if (b->scale < 0.4f) {
                float length = hypotf(b->vx, b->vy);
                for (int i = 0; i < 2; ++i) game_particle(game, PARTICLE_WEAPON_BOOMERANG_HIT, b->x, b->y,
                    b->owner, 1, length > 0 ? -b->vx / length : 0, length > 0 ? -b->vy / length : 0);
                b->active = false;
            }
            break;
        case WEAPON_BLADE: {
            b->rotation += b->offset_x < 0 ? -0.15f : 0.15f;
            int phase = b->age;
            if (phase > 9) {
                phase = 18 - phase;
                b->damage = b->base_damage * 1.25f;
            }
            float angle = phase * 0.22f * (b->offset_x < 0 ? -1 : 1);
            b->x = p->x + b->offset_x + 38 * b->scale * sinf(angle);
            b->y = p->y + b->offset_y - 38 * b->scale * cosf(angle) - game->scroll_speed;
            b->vx = b->vy = 0;
            if (p->lives <= 0) b->active = false;
            break;
        }
        case WEAPON_BEAM: case WEAPON_BEAM_FOCUS:
            if (p->lives <= 0 || !p->weapons[b->group][b->emitter].toggled) { b->active = false; break; }
            b->x = p->x + (b->weapon == WEAPON_BEAM ? b->offset_x : 0);
            b->y = p->y + (b->weapon == WEAPON_BEAM ? b->offset_y + 42 : 0) - game->scroll_speed;
            b->vx = b->vy = 0;
            if (b->weapon == WEAPON_BEAM_FOCUS) {
                b->radius = fminf(b->age * 20, b->width) * 0.46f;
                b->rotation = b->age * 0.25f;
            }
            break;
        default: break;
    }
    if (!b->splash && (b->weapon == WEAPON_PLASMA || b->weapon == WEAPON_PLASMA_FOCUS) && b->base_damage >= 0.5f) {
        float length = hypotf(b->vx, b->vy);
        game_particle(game, PARTICLE_WEAPON_PLASMA_TRAIL, b->x, b->y, b->owner, 1,
            length > 0 ? -b->vx / length : 0, length > 0 ? -b->vy / length : 0);
    }
    b->vx *= b->friction; b->vy *= b->friction;
}

void game_projectile_hit(Game *game, Bullet *b, int enemy) {
    if (b->splash) { b->active = true; b->was_hit = true; return; }
    ParticleKind effect = PARTICLE_WEAPON_LASER_HIT;
    float x = b->x, y = b->y, scale = 1;
    float length = hypotf(b->vx, b->vy), vx = length > 0 ? -b->vx / length : 0, vy = length > 0 ? -b->vy / length : 0;
    switch (b->weapon) {
        case WEAPON_MINI_SHOT: effect = PARTICLE_COUNT; break;
        case WEAPON_BLADE:
            effect = PARTICLE_WEAPON_BEAM_HIT; vx = vy = 0;
            x += random_signed(game) * 16; y += random_signed(game) * 16; break;
        case WEAPON_CHARGE_SHOT: effect = PARTICLE_WEAPON_CHARGE_HIT; scale = 0.5f + b->damage * 0.25f; break;
        case WEAPON_FLAME: case WEAPON_FLAME_FOCUS: effect = PARTICLE_WEAPON_FLAME_HIT; break;
        case WEAPON_VULCAN: effect = PARTICLE_WEAPON_VULCAN_HIT; break;
        case WEAPON_BOOMERANG: {
            effect = PARTICLE_WEAPON_BOOMERANG_HIT;
            float offset = random_float(game) * -3;
            x += b->vx * offset; y += b->vy * offset; break;
        }
        case WEAPON_BOMBLET: effect = PARTICLE_WEAPON_BOMBLET_HIT; vx = vy = 0; break;
        case WEAPON_SEEK_BOMB: effect = PARTICLE_WEAPON_SEEK_BOMB_HIT; vx = vy = 0; break;
        case WEAPON_MISSILE: case WEAPON_MISSILE_LAUNCHER: case WEAPON_DRUNK_MISSILE: case WEAPON_MISSILE_VOLLEY:
            effect = PARTICLE_WEAPON_MISSILE_HIT; vx = vy = 0; break;
        case WEAPON_LIGHTNING: case WEAPON_LIGHTNING_FOCUS: effect = PARTICLE_WEAPON_LIGHTNING_HIT; break;
        case WEAPON_PLASMA: case WEAPON_PLASMA_FOCUS:
            effect = PARTICLE_WEAPON_PLASMA_HIT; scale = b->base_damage >= 0.5f ? 2 : 1; break;
        case WEAPON_BEAM: case WEAPON_BEAM_FOCUS:
            effect = PARTICLE_WEAPON_BEAM_HIT; vx = vy = 0;
            if (enemy >= 0) {
                float tx, ty; target_position(game, enemy, &tx, &ty);
                x = b->weapon == WEAPON_BEAM ? fminf(b->x + b->width, fmaxf(b->x - b->width, tx)) : b->x + (tx - b->x) * 0.8f;
                y = b->weapon == WEAPON_BEAM ? ty : b->y + (ty - b->y) * 0.8f;
                x += random_signed(game) * 16; y += random_signed(game) * 16;
            }
            break;
        default: break;
    }
    if (effect != PARTICLE_COUNT) game_particle(game, effect, x, y, b->owner, scale, vx, vy);
    switch (b->weapon) {
        case WEAPON_LIGHTNING: case WEAPON_LIGHTNING_FOCUS: case WEAPON_CHARGE_SHOT:
        case WEAPON_BEAM: case WEAPON_BEAM_FOCUS: case WEAPON_BLADE: ++game->impact_sounds[0]; break;
        case WEAPON_VULCAN: case WEAPON_MINI_SHOT: case WEAPON_BOOMERANG: ++game->impact_sounds[1]; break;
        case WEAPON_DRUNK_MISSILE: case WEAPON_MISSILE_VOLLEY: ++game->impact_sounds[2]; break;
        case WEAPON_SEEK_BOMB: case WEAPON_BOMBLET: ++game->impact_sounds[3]; break;
        default: break;
    }
    bool plasma = (b->weapon == WEAPON_PLASMA || b->weapon == WEAPON_PLASMA_FOCUS) && b->base_damage >= 0.5f;
    if (plasma || (b->weapon == WEAPON_MISSILE_LAUNCHER && b->width > 0)) {
        if (plasma) game_particle(game, PARTICLE_WEAPON_PLASMA_SPLASH, b->x, b->y, b->owner, 1, -vx, -vy);
        /* Keep the impact projectile allocated until the splash is initialized. */
        b->active = true;
        int index = game_spawn_bullet(game, b->owner, b->x, b->y, 0, 0, b->damage * 0.5f);
        if (index >= 0) {
            Bullet *splash = &game->bullets[index];
            splash->splash = true;
            splash->weapon = plasma ? WEAPON_PLASMA_FOCUS : WEAPON_MISSILE_LAUNCHER;
            splash->radius = plasma ? 210 : b->width;
            splash->ignore_enemy = plasma ? -1 : enemy;
            splash->lifetime = plasma ? 2 : 1;
            splash->scale = 2;
        }
        b->active = false;
    } else if (b->weapon == WEAPON_LIGHTNING || b->weapon == WEAPON_LIGHTNING_FOCUS) {
        b->ignore_enemy = enemy;
        int target = nearest_enemy(game, b, true, false);
        if (--b->bounces >= 0 && target >= 0) {
            float x, y; target_position(game, target, &x, &y);
            float angle = atan2f(y - b->y, x - b->x) + random_signed(game) * 0.3f;
            b->x += b->vx; b->y += b->vy;
            b->vx = cosf(angle) * b->speed; b->vy = sinf(angle) * b->speed;
            b->x += b->vx; b->y += b->vy;
            b->rotation = angle;
            b->active = true;
        }
    } else if ((b->weapon == WEAPON_CHARGE_SHOT || b->weapon == WEAPON_LASER_SHOT) && enemy >= 0 && b->bounces > 0) {
        --b->bounces;
        b->ignore_enemy = enemy;
        b->active = true;
    } else if (b->weapon == WEAPON_BOOMERANG) {
        b->scale -= 0.05f;
        b->active = true;
    } else if (b->weapon == WEAPON_BLADE || b->weapon == WEAPON_BEAM || b->weapon == WEAPON_BEAM_FOCUS) b->active = true;
}
