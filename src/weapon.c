#include "game.h"
#include <math.h>

#define PI 3.141592654f

static float random_float(Game *game) { return (game_random(game) >> 8) / 16777216.0f; }
static float random_signed(Game *game) { return random_float(game) * 2 - 1; }

static int nearest_enemy(const Game *game, const Bullet *b, bool forward) {
    int result = -1;
    float nearest = 1e20f;
    for (int i = 0; i < GAME_ENEMIES; ++i) {
        const Enemy *e = &game->enemies[i];
        if (!e->active || i == b->ignore_enemy || e->y < 0 || e->y > GAME_HEIGHT) continue;
        float dx = e->x - b->x, dy = e->y - b->y, distance = dx * dx + dy * dy;
        if (forward && dx * b->vx + dy * b->vy < 0) continue;
        if (distance < nearest) { nearest = distance; result = i; }
    }
    return result;
}

static void spawn(Game *game, int slot, int kind, int group, int emitter, const WeaponEmitter *e, int charge) {
    Player *p = &game->players[slot];
    float side = group == 3 ? -40 : group == 4 ? 40 : 0;
    float angle = -PI * 0.5f + e->rotation + random_signed(game) * e->random_rotation;
    float damage = e->damage * (p->pilot == 2 ? 1.15f : 1);
    if (kind == WEAPON_CHARGE_SHOT) damage *= fminf(charge / 60.0f, 1);
    if (kind == WEAPON_VULCAN || kind == WEAPON_BOOMERANG) angle += random_signed(game) * e->spray_angle;
    if (kind == WEAPON_FLAME || kind == WEAPON_FLAME_FOCUS)
        angle = game->tick * e->fire_rotation_speed + e->start_rotation + random_signed(game) * e->spray_angle;
    if (kind == WEAPON_BOMBLET) angle = random_float(game) * 2 * PI;
    int index = game_spawn_bullet(game, slot, p->x + e->offset_y + side, p->y - e->offset_x,
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
            b->vy -= game->scroll_speed * e->anti_camera_speed;
            p->vx *= 1.15f; p->vy *= 1.15f;
            break;
        case WEAPON_VULCAN: case WEAPON_MINI_SHOT: case WEAPON_SEEK_BOMB: b->radius = 1; break;
        case WEAPON_BOMBLET:
            b->radius = 12; b->friction = 0.98f; b->lifetime = 481;
            b->vy -= game->scroll_speed * 3.5f;
            break;
        case WEAPON_BOOMERANG: b->radius = 16; break;
        case WEAPON_CHARGE_SHOT: b->radius = 12 * damage; b->scale = 0.25f + damage * 0.5f; break;
        case WEAPON_BLADE: b->radius = 22; b->lifetime = 9; break;
        case WEAPON_BEAM: b->width = 8 * e->width; break;
        case WEAPON_BEAM_FOCUS: b->width = e->radius; break;
        case WEAPON_MISSILE: case WEAPON_MISSILE_LAUNCHER:
            b->vx = b->direction_x * 6; b->vy = b->direction_y * 6;
            break;
        case WEAPON_DRUNK_MISSILE: case WEAPON_MISSILE_VOLLEY:
            b->curve_speed = 0.03f + random_float(game) * 0.03f;
            b->curve_magnitude = (game_random(game) & 1 ? 0.3f : -0.3f) + random_signed(game) * 0.2f;
            b->curve_offset = game_random(game) & 1 ? 1 : -1;
            b->rotation -= PI - random_signed(game) * 0.25f;
            break;
        default: break;
    }
    p->vy += e->kickback;
    ++game->shots;
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
            if (s->charge < 60) p->energy = fmaxf(0, p->energy - e->energy);
            ++s->charge;
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
        if (!enough && e->toggle) s->overheat = 60;
        if (trigger && enough && can_fire(e, s)) {
            if (!s->toggled) spawn(game, slot, kind, group, i, e, s->charge);
            s->toggled = e->toggle;
            s->charge = 0;
            s->cooldown = e->reload + random_float(game) * e->random_reload;
        }
        /* Ship.UpdateWeapons also drains toggled weapons each tick. */
        if (s->toggled) p->energy = fmaxf(0, p->energy - e->drain);
        s->cooldown = fmaxf(0, s->cooldown - 1.0f / GAME_HZ);
        if (s->overheat > 0) --s->overheat;
    }
}

void game_update_weapons(Game *game, int slot) {
    Player *p = &game->players[slot];
    fire_group(game, slot, 0, p->primary, p->weapon_level, p->input.fire && !p->input.focus);
    fire_group(game, slot, 1, equipment_focus_weapon(p->primary), p->weapon_level, p->input.focus);
    fire_group(game, slot, 2, p->secondary, p->secondary_level, p->input.fire && !p->input.focus);
    fire_group(game, slot, 3, p->sidekick, 0, p->input.sidekick_left);
    fire_group(game, slot, 4, p->sidekick, 0, p->input.sidekick_right);
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
    if (b->owner < 0) return;
    Player *p = &game->players[b->owner];
    if (b->lifetime > 0 && b->age > b->lifetime) { b->active = false; return; }
    switch (b->weapon) {
        case WEAPON_MISSILE: case WEAPON_MISSILE_LAUNCHER:
            b->vx += (-b->direction_x * b->speed - b->vx) * 0.05f;
            b->vy += (-b->direction_y * b->speed - b->vy) * 0.05f;
            break;
        case WEAPON_DRUNK_MISSILE: case WEAPON_MISSILE_VOLLEY: {
            float curve = sinf(b->curve_offset + b->age * b->curve_speed) * b->curve_magnitude;
            float angle = b->rotation + (curve - b->rotation) * curve;
            b->vx = cosf(angle) * b->speed; b->vy = sinf(angle) * b->speed;
            break;
        }
        case WEAPON_SEEK_BOMB: {
            if (b->target < 0 || !game->enemies[b->target].active) b->target = nearest_enemy(game, b, false);
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
            break;
        case WEAPON_BOMBLET: b->y -= game->scroll_speed; b->vy -= 0.005f; b->rotation += 0.02f + b->age * 0.0001f; break;
        case WEAPON_BOOMERANG: b->rotation += 0.175f; b->radius = 16 * b->scale; break;
        case WEAPON_BLADE: {
            float angle = b->age * 0.22f * (b->offset_x < 0 ? -1 : 1);
            b->x = p->x + b->offset_x + 38 * sinf(angle);
            b->y = p->y + b->offset_y - 38 * cosf(angle) - game->scroll_speed;
            b->vx = b->vy = 0;
            if (p->lives <= 0) b->active = false;
            break;
        }
        case WEAPON_BEAM: case WEAPON_BEAM_FOCUS:
            if (p->lives <= 0 || !p->weapons[b->group][b->emitter].toggled) { b->active = false; break; }
            b->x = p->x + (b->weapon == WEAPON_BEAM ? b->offset_x : 0);
            b->y = p->y + (b->weapon == WEAPON_BEAM ? b->offset_y + 42 : 0) - game->scroll_speed;
            b->vx = b->vy = 0;
            if (b->weapon == WEAPON_BEAM_FOCUS) b->radius = fminf(b->age * 20, b->width) * 0.46f;
            break;
        default: break;
    }
    b->vx *= b->friction; b->vy *= b->friction;
}

void game_projectile_hit(Game *game, Bullet *b, int enemy) {
    if (b->splash) { b->active = true; return; }
    if ((b->weapon == WEAPON_PLASMA || b->weapon == WEAPON_PLASMA_FOCUS) && b->base_damage >= 0.5f) {
        /* Keep the impact projectile allocated until the splash is initialized. */
        b->active = true;
        int index = game_spawn_bullet(game, b->owner, b->x, b->y, 0, 0, b->damage * 0.5f);
        if (index >= 0) {
            Bullet *splash = &game->bullets[index];
            splash->splash = true; splash->weapon = WEAPON_PLASMA_FOCUS;
            splash->radius = 210; splash->lifetime = 1; splash->scale = 2;
        }
        b->active = false;
    } else if (b->weapon == WEAPON_LIGHTNING || b->weapon == WEAPON_LIGHTNING_FOCUS) {
        b->ignore_enemy = enemy;
        int target = nearest_enemy(game, b, true);
        if (--b->bounces >= 0 && target >= 0) {
            float angle = atan2f(game->enemies[target].y - b->y, game->enemies[target].x - b->x) + random_signed(game) * 0.3f;
            b->vx = cosf(angle) * b->speed; b->vy = sinf(angle) * b->speed;
            b->active = true;
        }
    } else if (b->weapon == WEAPON_BOOMERANG) {
        b->scale -= 0.05f;
        b->active = b->scale >= 0.4f;
    } else if (b->weapon == WEAPON_BLADE || b->weapon == WEAPON_BEAM || b->weapon == WEAPON_BEAM_FOCUS) b->active = true;
}
