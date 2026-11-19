#include "game.h"
#include <math.h>
#include <stddef.h>

static float limit(float value, float low, float high) {
    return fminf(high, fmaxf(low, value));
}

bool ai_team(const Game *g) {
    bool active = false;
    for (int i = 0; i < GAME_PLAYERS; ++i) {
        if (!g->players[i].joined) continue;
        if (!g->ai.mode[i]) return false;
        active = true;
    }
    return active;
}

bool ai_set(Game *g, int slot, int mode) {
    if (slot < 0 || slot >= GAME_PLAYERS || mode < AI_OFF || mode > AI_DODGE) return false;
    if (mode && !g->players[slot].joined) {
        bool fresh = !g->players[slot].initialized;
        if (!game_join(g,slot)) return false;
        if (fresh) game_starting_loadout(g,slot);
        g->ai.joined[slot] = true;
    }
    g->ai.mode[slot] = mode;
    g->players[slot].input = (PlayerInput){0};
    if (!mode && g->ai.joined[slot]) {
        g->ai.joined[slot] = false;
        game_leave(g,slot);
    }
    return true;
}

void ai_campaign(Game *g, int difficulty, int players, int mode, bool beam, unsigned seed) {
    int options[OPTION_COUNT];
    for (int i = 0; i < OPTION_COUNT; ++i) options[i] = g->menu.options[i];
    game_reset_session(g,seed);
    g->ai = (PilotAI){.campaign=true,.beam=beam};
    for (int i = 0; i < OPTION_COUNT; ++i) g->menu.options[i] = options[i];
    g->active_slot = -1;
    g->cleared_game = false;
    g->difficulty = difficulty;
    for (int i = 0; i < players; ++i) {
        if (i) { game_join(g,i); g->ai.joined[i] = true; }
        game_starting_loadout(g,i);
        g->ai.mode[i] = mode;
    }
    game_stage(g,0,0);
}

/* Aim below a reachable target. Lanes are a preference so both pilots can
   converge on a boss or cover an otherwise empty half of the screen. */
static void consider(const Player *p, float lane, float x, float y, float bias,
                     float *best, float *tx, float *ty) {
    if (y < -30 || y > GAME_HEIGHT-65 || x < -30 || x > GAME_WIDTH+30) return;
    float distance = fabsf(x-p->x) + 0.36f*fabsf(p->y-y-270)
        + 0.45f*fabsf(x-lane) + fmaxf(0,y-p->y)*1.7f + bias;
    if (distance >= *best) return;
    *best = distance;
    *tx = x;
    *ty = limit(y+265,410,GAME_HEIGHT-45);
}

static void evade(const Game *g, const Player *p, float *tx, float *ty) {
    float nearest = 1, move_x = 0, move_y = 0;
    for (int i = 0; i < GAME_BULLETS; ++i) {
        const Bullet *b = &g->bullets[i];
        if (!b->active || b->owner >= 0) continue;
        float dx = b->x-p->x, dy = b->y-p->y;
        if (fabsf(dx)>230 || fabsf(dy)>230) continue;
        float vx = b->vx-p->vx, vy = b->vy-p->vy;
        float t = limit(-(dx*vx+dy*vy)/(1+vx*vx+vy*vy),0,20);
        float x = dx+t*vx, y = dy+t*vy;
        float risk = (x*x+y*y)/((48+b->radius)*(48+b->radius)) + t/36;
        if (risk >= nearest) continue;
        nearest = risk;
        if (fabsf(x)+fabsf(y) < 3) { x = -b->vy; y = b->vx; }
        move_x = -x; move_y = -y;
        if (p->x < 70 && move_x < 0) move_x = -move_x;
        if (p->x > GAME_WIDTH-70 && move_x > 0) move_x = -move_x;
    }
    for (int i = 0; i < GAME_ENEMIES; ++i) {
        const Enemy *e = &g->enemies[i];
        if (!e->active || e->intangible || e->invisible || e->kind == ENEMY_BLACK_HOLE ||
            e->kind == ENEMY_BOSS10_BALL) continue;
        float dx = p->x+5*p->vx - (e->x+5*e->vx+e->collision_x);
        float dy = p->y+5*p->vy - (e->y+5*e->vy+e->collision_y);
        float rx = (e->box_width ? e->box_width/2 : e->radius)+40;
        float ry = (e->box_height ? e->box_height/2 : e->radius)+40;
        float risk = dx*dx/(rx*rx) + dy*dy/(ry*ry);
        if (risk >= nearest) continue;
        nearest = risk;
        move_x = dx/rx; move_y = dy/ry;
    }
    if (nearest < 1) {
        float strength = 165/fmaxf(1,hypotf(move_x,move_y));
        *tx = p->x+move_x*strength;
        *ty = p->y+move_y*strength;
    }
}

/* The Stage 10 barricade opens when a pilot returns a ball into a block. */
static void return_ball(const Game *g, const Player *p, float *tx, float *ty) {
    const Enemy *boss = NULL, *ball = NULL;
    float nearest = 1e9f;
    for (int i = 0; i < GAME_ENEMIES; ++i) {
        const Enemy *e = &g->enemies[i];
        if (!e->active) continue;
        if (e->kind == ENEMY_BOSS10) boss = e;
        if (e->kind != ENEMY_BOSS10_BALL || e->timer > 0 || e->vy <= 0) continue;
        float distance = fabsf(e->x-p->x) + fabsf(e->y-p->y);
        if (distance < nearest) { nearest = distance; ball = e; }
    }
    if (!boss || !ball) return;
    for (int i = 0; i < GAME_ENEMIES; ++i) {
        const Enemy *e = &g->enemies[i];
        if (!e->active || e->intangible || e->kind != ENEMY_BOSS10_BLOCK ||
            fabsf(e->x-boss->x)>e->box_width/2 || e->y<boss->y) continue;
        *tx = ball->x + ball->vx*5 + (boss->x > ball->x ? -10 : 10);
        *ty = limit(ball->y + 18,310,GAME_HEIGHT-36);
        break;
    }
}

void ai_fly(Game *g, int slot) {
    Player *p = &g->players[slot];
    float lane = GAME_WIDTH*0.5f;
    if (g->players[1-slot].joined && g->players[1-slot].lives > 0)
        lane = GAME_WIDTH*(slot ? 0.74f : 0.26f);
    float tx=lane, ty=800, best=1e9f;
    for (int i=0; i<GAME_ENEMIES; ++i) {
        const Enemy *e=&g->enemies[i];
        if (!e->active || e->intangible || e->invisible || enemy_is_asteroid(e->kind) ||
            e->kind==ENEMY_BOSS10_BALL || e->kind==ENEMY_BOSS10_BLOCK ||
            e->kind==ENEMY_BLACK_HOLE || e->kind==ENEMY_CUTTER_BALL || e->kind==ENEMY_FENCE_BEAM ||
            (e->kind==ENEMY_BOSS4 && e->health>0.6f*e->health_max)) continue;
        consider(p,lane,e->x+limit(e->vx*8,-50,50),e->y,0,&best,&tx,&ty);
    }
    const StageDefinition *stage = &stage_definitions[g->stage.index];
    for (int i=0; i<stage->event_count; ++i) {
        const StageEvent *e=&stage->events[i];
        if (e->kind==STAGE_BUILDING && g->stage.activated[i] && g->stage.building_health[i]>=0)
            consider(p,lane,e->x+GAME_WIDTH/2,e->y-g->stage.camera_y+GAME_HEIGHT/2,110,&best,&tx,&ty);
    }
    for (int i=0; i<GAME_PICKUPS; ++i) {
        const Pickup *coin=&g->pickups[i];
        if (!coin->active || coin->magnet || coin->y < 80 || coin->y > GAME_HEIGHT) continue;
        float distance=hypotf(coin->x-p->x,coin->y-p->y);
        if (distance+230 < best) { best=distance+230; tx=coin->x; ty=coin->y; }
    }
    if (g->stage.index == 9 && g->boss_spawned) return_ball(g,p,&tx,&ty);
    bool aligned = fabsf(tx-p->x)<48;
    if (g->ai.mode[slot]==AI_DODGE) evade(g,p,&tx,&ty);
    p->input=(PlayerInput){.x=limit((tx-p->x-4*p->vx)/50,-1,1),
        .y=limit((ty-p->y-4*p->vy)/50,-1,1),.fire=true,
        .focus=aligned && fabsf(ty-p->y)<90 && p->energy>0.55f*game_generators[p->generator].capacity,
        .sidekick_left=true,.sidekick_right=true};
}

static void purchase(Game *g,int slot,int category,int item,int level) {
    g->menu.shop_category[slot]=category;
    g->menu.shop_item[slot]=item;
    g->menu.shop_level[slot]=level;
    game_shop_buy(g,slot);
}

void ai_shop(Game *g) {
    if (game_shop_departing(g)) return;
    ++g->ai.shop_ticks;
    for (int slot=0; slot<GAME_PLAYERS; ++slot) {
        Player *p=&g->players[slot];
        if (!g->ai.campaign || !p->joined || !g->ai.mode[slot] || g->ai.shop_ticks!=15) continue;
        /* Both comparison policies use the same support budget. Switch the
           primary after stage four in the Beam policy. */
        if (p->generator < 2) purchase(g,slot,1,p->generator+1,0);
        if (g->menu.next_stage>=2 && p->shield<1) purchase(g,slot,2,1,0);
        int weapon=g->ai.beam && g->menu.next_stage>=4 ? WEAPON_BEAM : p->primary;
        int level=game_shop_owned_marks(p,3,weapon);
        for (; level<game_weapons[weapon].level_count; ++level) {
            if (game_shop_price(p,3,weapon,level)>p->money) break;
            purchase(g,slot,3,weapon,level);
        }
        g->menu.shop_category[slot]=0;
    }
    if (g->ai.shop_ticks>=90 && ai_team(g)) {
        int slot=g->players[0].joined ? 0 : 1;
        g->menu.shop_catalog[slot] = g->menu.shop_tiers[slot] = g->menu.shop_purchase[slot] = false;
        g->menu.shop_category[slot]=SHOP_NEXT_STAGE_ROW;
        game_shop_input(g,MENU_CONFIRM,slot);
    }
}
