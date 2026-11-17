#include "game.h"
#include <math.h>
#include <stddef.h>

#define PI 3.141592654f
static float random_float(Game *g) { return game_random_float(g); }
static int sign(Game *g) { return game_random_float(g) < 0.5f ? -1 : 1; }
static int reload(const Game *g, float ticks) {
    static const float scale[] = {1.1f, 0.9f, 0.8f};
    return (int)(ticks * scale[g->difficulty]);
}

static int nearest_player(const Game *g, const Enemy *e) {
    int result = -1;
    float distance = 1e20f;
    for (int i = 0; i < GAME_PLAYERS; ++i) {
        const Player *p = &g->players[i];
        float d = (p->x-e->x)*(p->x-e->x) + (p->y-e->y)*(p->y-e->y);
        if (p->joined && p->lives > 0 && d < distance) { result = i; distance = d; }
    }
    return result;
}

static float aim(const Game *g, const Enemy *e, float lead) {
    int slot = nearest_player(g, e);
    if (slot < 0) return PI * 0.5f;
    const Player *p = &g->players[slot];
    return atan2f(p->y + p->vy * lead - e->y, p->x + p->vx * lead - e->x);
}

int enemy_shot(Game *g, const Enemy *e, int kind, float x, float y, float angle, float speed, float damage) {
    int i = game_spawn_bullet(g, -1, e->x+x, e->y+y, cosf(angle)*speed, sinf(angle)*speed, damage);
    if (i >= 0) {
        Bullet *b = &g->bullets[i];
        static const float radius[] = {2, 0.5f, 10, 21, 14, 1};
        b->hostile_kind = kind; b->radius = radius[kind]; b->speed = speed; b->rotation = angle;
        b->health = kind == HOSTILE_PELLET ? 3 : 4;
        if (kind == HOSTILE_PELLET) b->curve_speed = 0.45f * sign(g);
        ++g->enemy_shots[kind == HOSTILE_CANNON || e->kind == ENEMY_BOSS12 || e->kind == ENEMY_PYRAMID ||
            (e->kind == ENEMY_BOSS10 && kind == HOSTILE_LASER) ? 1 : 0];
    }
    return i;
}

void enemy_init_extended(Game *g, Enemy *e) {
    if (enemy_is_asteroid(e->kind)) {
        e->vx = 0.1f + 0.3f * game_random_signed(g);
        e->vy = 1 + 2 * random_float(g);
        e->spin = 0.05f + 0.1f * game_random_signed(g);
        e->orbit_radius = 1 + 0.15f * game_random_signed(g);
        e->health = e->health_max = e->orbit_radius;
        if (e->kind == ENEMY_BIG_ASTEROID) {
            float bigger = 3 + 0.2f * random_float(g);
            e->spin *= 0.2f; e->orbit_radius *= bigger;
            e->health = e->health_max *= bigger * 3;
        }
        e->radius = 16; e->coins = 0;
        return;
    }
    if (e->kind >= ENEMY_BOSS3) { boss_init(g, e); return; }
    float health = 1, radius = 32, delay = 0;
    switch (e->kind) {
        case ENEMY_AIRSHIP: health=7; e->box_width=72; e->box_height=104; e->coins=6; delay=15; break;
        case ENEMY_BEAMER: health=2; delay=45; break;
        case ENEMY_BLACK_HOLE: health=100000; radius=300; e->coins=0; break;
        case ENEMY_CANNON: health=4; radius=34; delay=random_float(g)*75; e->coins=8; break;
        case ENEMY_CUTTER: health=5; e->box_width=e->box_height=72; e->coins=6; break;
        case ENEMY_CUTTER_BALL: health=0; radius=30; e->coins=3; break;
        case ENEMY_DOUBLE_TURRET: health=2; radius=34; e->coins=4; delay=random_float(g)*45; break;
        case ENEMY_FENCE: health=4; radius=28; e->coins=0; break;
        case ENEMY_FENCE_BEAM: health=100000; e->box_width=1; e->box_height=8; e->coins=0; break;
        case ENEMY_GLOB: health=3.5f; e->coins=3; e->aim=random_float(g)*2*PI; e->timer=90; break;
        case ENEMY_MISSILE_POD: health=2; delay=random_float(g)*90; break;
        case ENEMY_MISSILE_STORM: health=5; radius=58; e->timer=60; e->attack=sign(g); e->aim=PI*0.5f*(1-e->attack); break;
        case ENEMY_MISSILE_TRIPLE: health=3; radius=34; delay=random_float(g)*111; break;
        case ENEMY_PYRAMID: health=6; radius=56; e->aim=PI*0.5f; e->spin=0.005f*sign(g); break;
        case ENEMY_RAID_TURRET: health=2; radius=28; delay=random_float(g)*24; break;
        case ENEMY_ROTATE_TURRET: health=2.25f; e->box_width=e->box_height=56; delay=random_float(g)*45; break;
        case ENEMY_SPIKE: health=2; radius=34; e->coins=0; e->timer=60; break;
        case ENEMY_SPLITTER: health=1.25f; radius=36; e->coins=0; e->spin=0.015f*sign(g); break;
        case ENEMY_SPLITTER_FRAGMENT: health=1; radius=30; e->spin=0.1f*sign(g); break;
        case ENEMY_SWIRL: health=3; e->coins=1; break;
        case ENEMY_SWIRL_BALL: health=0.75f; radius=16; e->coins=1; break;
        case ENEMY_TELEPORTER: health=4; radius=34; e->attack=-1; e->spin=0.005f; break;
        case ENEMY_TRIPLE_SHOT: health=2; radius=34; delay=random_float(g)*90; break;
        case ENEMY_BONUS_SHIP: health=3; break;
        default: break;
    }
    static const float scale[] = {1,1.25f,1.55f};
    e->health = e->health_max = health * scale[g->difficulty];
    e->radius=radius; e->cooldown=(int)delay;
}

static void orbit(Game *g, Enemy *e, int count, EnemyKind kind, float radius, float speed) {
    if (e->age == 1) for (int i=0; i<count; ++i) {
        int index=game_spawn_enemy(g,kind,e->x,e->y);
        e->children[i]=index;
        if (index>=0) { g->enemies[index].parent=(int)(e-g->enemies); g->enemies[index].authored=e->authored; }
    }
    for (int i=0; i<count; ++i) {
        int index=e->children[i];
        if (index<0) continue;
        Enemy *child=&g->enemies[index];
        if (!child->active || child->parent!=(int)(e-g->enemies)) continue;
        float angle=(kind == ENEMY_CUTTER_BALL ? e->orbit_angle : g->tick*speed)+i*2*PI/count;
        child->x=e->x+cosf(angle)*radius; child->y=e->y+sinf(angle)*radius;
        if (kind == ENEMY_CUTTER_BALL) child->rotation=e->orbit_angle*(i ? -3 : 3);
    }
}

static void black_hole(Game *g, Enemy *e) {
    e->rotation+=0.075f;
    game_particle(g, PARTICLE_ENEMY_BLACK_HOLE_CENTER, e->x, e->y, 2, 1, 0, 0);
    float angle = random_float(g) * 2 * PI;
    game_particle(g, PARTICLE_ENEMY_BLACK_HOLE_CENTER, e->x - cosf(angle) * 72, e->y - sinf(angle) * 72, -1, 1, 0, 0);
    for (int i=0; i<GAME_PLAYERS; ++i) {
        Player *p=&g->players[i];
        float dx=e->x-p->x,dy=e->y-p->y,d=hypotf(dx,dy);
        if (!p->joined || p->lives<=0 || d>=300) continue;
        float scale=dx*p->input.x+dy*p->input.y<0 ? 0.5f : 0.75f;
        if (d>0) { p->vx=(p->vx+dx/d*1.5f*scale)*0.9f; p->vy=(p->vy+dy/d*1.5f*scale)*0.9f; }
        if (d>0) game_particle(g, PARTICLE_ENEMY_BLACK_HOLE_PULL_SHIP, p->x, p->y, -1, 1, dx/d*4, dy/d*4);
        if (d<1) game_damage_player(g,i,10000);
    }
    for (int i=0; i<GAME_BULLETS; ++i) {
        Bullet *b=&g->bullets[i];
        if (!b->active || (b->owner>=0 && (b->weapon==WEAPON_BEAM || b->weapon==WEAPON_BEAM_FOCUS))) continue;
        if (b->owner < 0 && b->hostile_kind == HOSTILE_MISSILE) continue;
        float dx=e->x-b->x,dy=e->y-b->y,d=hypotf(dx,dy);
        if (d>=300) continue;
        float strength=(300-d)*(300-d)*0.0000025f;
        b->vx=(b->vx+dx*strength)*0.97f; b->vy=(b->vy+dy*strength)*0.97f;
        if (d>0) game_particle(g, PARTICLE_ENEMY_BLACK_HOLE_PULL, e->x-dx/d*58, e->y-dy/d*58, -1, 1, 0, 0);
        if (d<42) b->active=false;
    }
}

static Enemy *fence_partner(Game *g, const Enemy *e) {
    int index=e->children[0];
    if (index<0) return NULL;
    Enemy *other=&g->enemies[index];
    return other->active && other->kind==ENEMY_FENCE && other->parent==(int)(e-g->enemies) ? other : NULL;
}

static void fence(Game *g, Enemy *e) {
    if (e->age==1 && e->parent<0 && e->children[0]<0) for (int i=0;i<GAME_ENEMIES;++i) {
        Enemy *other=&g->enemies[i];
        if (other==e || !other->active || other->kind!=ENEMY_FENCE || other->children[0]>=0 || other->parent>=0 ||
            fabsf(other->y-e->y)>16 || fabsf(other->x-e->x)>340) continue;
        int beam=game_spawn_enemy(g,ENEMY_FENCE_BEAM,(e->x+other->x)*0.5f,e->y);
        if (beam<0) break;
        e->children[0]=i; other->parent=(int)(e-g->enemies);
        e->children[1]=beam;
        g->enemies[beam].parent=(int)(e-g->enemies); g->enemies[beam].collision_y=4;
        break;
    }
    int index=e->children[1];
    if (index>=0) {
        Enemy *beam=&g->enemies[index], *other=fence_partner(g,e);
        if (!beam->active || beam->kind!=ENEMY_FENCE_BEAM || beam->parent!=(int)(e-g->enemies)) return;
        if (!other) { beam->active=false; e->children[0]=e->children[1]=-1; return; }
        beam->x=(e->x+other->x)*0.5f;
        beam->y=(e->y+other->y)*0.5f;
        beam->box_width=fabsf(e->x-other->x);
    }
}

void enemy_update_extended(Game *g, Enemy *e) {
    bool attached=e->kind==ENEMY_FENCE_BEAM ||
        ((e->kind==ENEMY_CUTTER_BALL || e->kind==ENEMY_SWIRL_BALL) && e->parent>=0);
    bool chaser=e->kind==ENEMY_SPIKE || e->kind==ENEMY_GLOB;
    /* A burst replaces an existing mover with IgnoreCamera. None is a null
       mover in the original: ground turrets keep scrolling with the scenery. */
    bool pause_mover=e->authored && e->movement!=MOVE_NONE &&
        (e->kind==ENEMY_TRIPLE_SHOT || e->kind==ENEMY_ROTATE_TURRET) && e->shot_index>0;
    if (attached) {
        if (e->parent<0 || !g->enemies[e->parent].active) { e->active=false; return; }
        if (e->kind==ENEMY_FENCE_BEAM) {
            Enemy *owner=&g->enemies[e->parent];
            if (owner->kind!=ENEMY_FENCE || owner->children[1]!=(int)(e-g->enemies) || !fence_partner(g,owner)) {
                e->active=false; return;
            }
        }
    } else {
        if (e->authored && !chaser) {
            if (!pause_mover) movement_velocity(e->movement,e->age,e->move_speed,e->move_transition,&e->vx,&e->vy);
            if (e->movement!=MOVE_IGNORE_CAMERA && !pause_mover) e->y+=g->scroll_speed;
        }
        e->x+=e->vx; e->y+=e->vy;
        /* Spike clears its mover, but unlike Glob never cancels camera scroll. */
        if (e->authored && e->kind==ENEMY_SPIKE) e->y+=g->scroll_speed;
        if (!e->authored && (e->kind == ENEMY_SPLITTER_FRAGMENT ||
            ((e->kind == ENEMY_CUTTER_BALL || e->kind == ENEMY_SWIRL_BALL) && e->parent < 0))) e->y += g->scroll_speed;
    }
    ++e->age; e->rotation+=e->spin;
    if (e->invisible>0) --e->invisible;
    if (e->kind==ENEMY_BLACK_HOLE) { black_hole(g,e); return; }
    if (e->kind==ENEMY_CUTTER) {
        orbit(g,e,2,ENEMY_CUTTER_BALL,126,0);
        e->orbit_angle += 0.03f * (e->x < GAME_WIDTH * 0.5f ? -1 : e->x > GAME_WIDTH * 0.5f ? 1 : 0);
        return;
    }
    if (e->kind==ENEMY_SWIRL) { orbit(g,e,3,ENEMY_SWIRL_BALL,48,0.1f); return; }
    if (e->kind==ENEMY_FENCE) { fence(g,e); return; }
    if (chaser) {
        int slot=e->kind == ENEMY_GLOB && e->target >= 0 ? e->target : nearest_player(g,e);
        if (slot<0) return;
        if (e->kind == ENEMY_GLOB) e->target = slot;
        Player *p=&g->players[slot];
        float dx=p->x-e->x,dy=p->y-e->y,d=hypotf(dx,dy);
        if (e->kind==ENEMY_GLOB) {
            if (p->lives <= 0) { enemy_die(g,e,-1); return; }
            if (e->attack) {
                if (d < 12) e->attack = 2;
                e->intangible=true;
                if (e->attack == 2) { e->x=p->x; e->y=p->y; }
                else { e->x+=dx*0.35f; e->y+=dy*0.35f; }
                e->vx=e->vy=0;
                float shield=p->shield_health; p->shield_health=0;
                if (!p->invincible && !g->seeking) {
                    int ignore=p->invulnerable; p->invulnerable=0;
                    game_damage_player(g,slot,0.05f); p->invulnerable=ignore;
                }
                p->shield_health=shield; p->vx*=0.6f; p->vy*=0.6f; e->health+=0.5f;
                if (--e->timer<=0) enemy_die(g,e,-1);
            } else {
                e->vx+=dx*0.0075f; e->vy+=dy*0.0075f;
                float length=hypotf(e->vx,e->vy),speed=3.5f+sinf(g->tick*0.075f+e->aim)*3;
                if (length>0) { e->vx=e->vx/length*speed; e->vy=e->vy/length*speed; }
                for (int i=0; i<GAME_PLAYERS; ++i) {
                    Player *candidate=&g->players[i];
                    float px=candidate->x-e->x, py=candidate->y-e->y;
                    float radius=candidate->shield_health>0?24:PLAYER_ARMOR_RADIUS;
                    if (!g->seeking && candidate->joined && candidate->lives>0 && px*px+py*py<32*32+radius*radius) {
                        e->target=i; e->attack=1; break;
                    }
                }
            }
        } else {
            e->spin=fminf(0.275f,e->spin+0.0075f);
            if (e->spin>0.25f && d>0 && d<1200) { e->vx+=dx/d*0.2f; e->vy+=dy/d*0.2f; }
            e->vx*=0.98f; e->vy*=0.98f;
            float left=g->stage.camera_x, right=left+GAME_WIDTH;
            if (e->timer>0) --e->timer;
            else if (e->hit_count<=5 && (e->x<left || e->x>right || e->y<0 || e->y>GAME_HEIGHT)) {
                float angle=atan2f(GAME_HEIGHT*0.5f-e->y,left+GAME_WIDTH*0.5f-e->x),speed=hypotf(e->vx,e->vy);
                e->vx=cosf(angle)*speed; e->vy=sinf(angle)*speed; ++e->hit_count; e->timer=6;
                e->x=fmaxf(left,fminf(right,e->x)); e->y=fmaxf(0,fminf(GAME_HEIGHT,e->y+2.5f));
            }
        }
        return;
    }
    if (e->kind==ENEMY_PYRAMID) {
        int phase=e->age%60;
        if (phase>0 && phase<20) {
            float target=aim(g,e,5),difference=remainderf(target-e->aim,2*PI);
            e->aim+=fmaxf(-0.03f,fminf(0.03f,difference));
        } else if (phase<40 && phase%4==0) {
            int i=enemy_shot(g,e,HOSTILE_MINI,cosf(e->aim)*34,sinf(e->aim)*34,e->aim,9,1);
            if (i>=0) g->bullets[i].vy-=g->scroll_speed;
        }
        return;
    }
    if (e->kind==ENEMY_TELEPORTER) {
        if (e->aux_index) { e->vx*=0.99f; e->vy*=0.99f; }
        if (e->invisible == 15) game_particle(g, PARTICLE_ENEMY_TELEPORTER_APPEAR, e->x,e->y,-1,1,0,0);
        if (e->timer>0) e->spin+=0.01f;
        if (e->timer>0 && --e->timer==0) {
            game_particle(g, PARTICLE_ENEMY_TELEPORTER_VANISH,e->x,e->y,-1,1,0,0);
            e->x=g->stage.camera_x+GAME_WIDTH*0.5f+random_float(g)*300*sign(g); e->y=-150+random_float(g)*100;
            float angle=aim(g,e,0)+PI*0.5f*sign(g);
            float center=atan2f(GAME_HEIGHT*0.5f-e->y,g->stage.camera_x+GAME_WIDTH*0.5f-e->x);
            e->vx=cosf(angle)*2+cosf(center)*2; e->vy=sinf(angle)*2+sinf(center)*2;
            e->spin=0.005f; e->aux_index=1; e->cooldown=0;
            e->invisible=90; e->attack=-1;
        }
        if (e->invisible || e->timer || e->attack<0) return;
    }
    if (e->kind==ENEMY_MISSILE_STORM) {
        if (e->timer < -26) { e->timer=120; e->attack=sign(g); e->aim=PI*0.5f*(1-e->attack); }
        if (--e->timer>0) return;
    }
    if (e->kind == ENEMY_ROTATE_TURRET) {
        if (--e->cooldown <= 0) {
            if (e->shot_index < 3) {
                float angle = e->rotation + e->attack * PI * 0.5f;
                enemy_shot(g, e, HOSTILE_SHOT, 0, 0, angle, 8, 3);
                enemy_shot(g, e, HOSTILE_SHOT, 0, 0, angle + PI, 8, 3);
                ++e->shot_index;
                e->cooldown = 9;
            } else {
                e->shot_index = 0;
                ++e->attack;
                e->aim += PI * 0.25f;
                e->cooldown = reload(g, 45);
            }
        }
        if (!e->shot_index) e->rotation = fminf(e->aim, e->rotation + 0.03f);
        return;
    }
    if (--e->cooldown>0) return;
    switch (e->kind) {
        case ENEMY_AIRSHIP:
            enemy_shot(g,e,HOSTILE_LASER,-50,0,PI*0.5f,8,3); enemy_shot(g,e,HOSTILE_LASER,47,0,PI*0.5f,8,3);
            e->cooldown=reload(g,30); break;
        case ENEMY_BEAMER: enemy_shot(g,e,HOSTILE_LASER,2,16,PI*0.5f,8,5); e->cooldown=reload(g,90); break;
        case ENEMY_CANNON: enemy_shot(g,e,HOSTILE_CANNON,0,0,PI*0.5f,10,2); e->cooldown=reload(g,75); break;
        case ENEMY_DOUBLE_TURRET:
            if (!e->shot_index) e->aim=aim(g,e,0)+random_float(g)*2*PI*0.01f*sign(g);
            enemy_shot(g,e,HOSTILE_SHOT,0,0,e->aim,8,3); e->shot_index^=1;
            e->cooldown=e->shot_index?6:reload(g,45); break;
        case ENEMY_MISSILE_POD: enemy_shot(g,e,HOSTILE_MISSILE,0,0,PI*0.5f,2.5f,6); e->cooldown=reload(g,90); break;
        case ENEMY_MISSILE_TRIPLE:
            for(int i=-1;i<=1;++i) enemy_shot(g,e,HOSTILE_MISSILE,0,0,PI*0.5f+i*PI*0.25f,2.5f,6);
            e->cooldown=reload(g,111); break;
        case ENEMY_MISSILE_STORM:
            enemy_shot(g,e,HOSTILE_MISSILE,0,0,e->aim,2.5f,6); e->aim+=PI/8*e->attack; e->cooldown=reload(g,3); break;
        case ENEMY_RAID_TURRET: enemy_shot(g,e,HOSTILE_PELLET,0,0,aim(g,e,0)+random_float(g)*2*PI*0.015f,2.5f,2); e->cooldown=reload(g,24); break;
        case ENEMY_TRIPLE_SHOT:
            if (e->shot_index<3) {
                if (!e->shot_index) e->aim=aim(g,e,10);
                float angle=e->aim+random_float(g)*2*PI*0.015f;
                enemy_shot(g,e,HOSTILE_SHOT,0,0,angle,8,3);
                ++e->shot_index; e->cooldown=9;
            } else { e->shot_index=0; ++e->attack; e->aim+=PI*0.25f; e->cooldown=reload(g,90+random_float(g)*60); }
            break;
        case ENEMY_TELEPORTER:
            for(int i=0;i<4;++i) enemy_shot(g,e,HOSTILE_MISSILE,0,0,aim(g,e,0)+e->attack*PI*0.25f+i*PI*0.5f,4,6);
            e->cooldown=reload(g,6); if (++e->attack>1) { e->attack=-1; e->timer=30; } break;
        default: break;
    }
}

void enemy_take_damage(Game *g, Enemy *e, float damage, int owner) {
    if (e->active && enemy_is_asteroid(e->kind)) {
        e->health -= damage;
        if (e->health < 0) {
            if (owner >= 0) g->players[owner].score += 100;
            e->active = false;
        }
        return;
    }
    if (!e->active || e->intangible || e->invisible || e->kind == ENEMY_CUTTER_BALL || e->kind == ENEMY_FENCE_BEAM) return;
    if (e->kind == ENEMY_SWIRL_BALL && e->parent >= 0 && g->enemies[e->parent].active)
        e = &g->enemies[e->parent];
    if (boss_damage(g, e, damage, owner)) return;
    damage *= g->players[0].joined && g->players[1].joined ? 0.75f : 1;
    e->health -= damage;
    if (e->kind == ENEMY_RAID_TURRET) e->cooldown = 16;
    if (e->kind == ENEMY_TELEPORTER && ++e->hit_count > 6 && e->attack < 0) { e->hit_count = 0; e->attack = 0; }
    if (e->health <= 0) {
        if (e->kind != ENEMY_FENCE) ++g->stage.stats.enemy_kills;
        enemy_die(g, e, owner);
    }
}

static void death_burst(Game *g, const Enemy *e, float scale, bool drift) {
    float angle = random_float(g) * 2 * PI;
    float vx = drift ? cosf(angle) * 1.5f : 0;
    float vy = drift ? sinf(angle) * 1.5f - (e->authored && e->movement != MOVE_NONE ? g->scroll_speed * 0.5f : 0) : 0;
    for (int color = 2; color <= 3; ++color)
        game_particle(g, PARTICLE_ENEMY_DEATH_EXPLOSION, e->x, e->y, color, scale, vx, vy);
}

void enemy_die(Game *g, Enemy *e, int owner) {
    if (!e->active) return;
    if (enemy_is_asteroid(e->kind)) { e->active = false; return; }
    if (e->kind==ENEMY_FENCE_BEAM) { e->active=false; return; }
    if (e->kind != ENEMY_FENCE) ++g->kills;
    if (enemy_corpse_name(e->kind) && e->movement == MOVE_NONE) {
        for (int i = 0; i < STAGE_MAX_EVENTS; ++i) {
            EnemyCorpse *corpse = &g->stage.corpses[i];
            if (corpse->active && corpse->y - g->stage.camera_y + GAME_HEIGHT * 0.5f < GAME_HEIGHT + 200) continue;
            corpse->active = true;
            corpse->kind = e->kind;
            corpse->x = e->x;
            corpse->y = e->y + g->stage.camera_y - GAME_HEIGHT * 0.5f;
            break;
        }
    }
    if (owner>=0) g->players[owner].score += game_health_score(g, e->health_max, 20);
    if (enemy_is_boss(e->kind)) {
        Enemy body = *e;
        e->active = false;
        bool victims[GAME_ENEMIES];
        for (int i=0;i<GAME_ENEMIES;++i) victims[i]=g->enemies[i].active && g->enemies[i].kind!=ENEMY_FENCE && !enemy_is_asteroid(g->enemies[i].kind);
        for (int i=0;i<GAME_BULLETS;++i) {
            Bullet *b=&g->bullets[i];
            if (!b->active) continue;
            game_particle(g,PARTICLE_OBJECT_DESTROYED,b->x,b->y,-1,1,0,0);
            b->bounces=0;
            if (b->owner>=0 && !b->splash) game_projectile_hit(g,b,-1);
            else if (b->hostile_kind==HOSTILE_MISSILE) game_particle(g,PARTICLE_WEAPON_MISSILE_HIT,b->x,b->y,2,1,0,0);
            b->active=false;
        }
        for (int i=0;i<GAME_PICKUPS;++i) if (g->pickups[i].active && g->pickups[i].kind==0) {
            game_particle(g,PARTICLE_OBJECT_DESTROYED,g->pickups[i].x,g->pickups[i].y,-1,1,0,0);
            g->pickups[i].active=false;
        }
        for (int i=0;i<GAME_ENEMIES;++i) if (victims[i] && g->enemies[i].active) {
            enemy_die(g,&g->enemies[i],-1);
        }
        bool final=body.kind==ENEMY_BOSS12;
        for (int i=0;i<(final?30:14);++i) for (int color=2;color<=3;++color) {
            float angle=random_float(g)*2*PI, radius=game_random_signed(g)*(final?440:240);
            game_particle(g,PARTICLE_ENEMY_DEATH_EXPLOSION,body.x+cosf(angle)*radius,body.y+sinf(angle)*radius,
                color,final?1.75f:1.5f,0,0);
        }
        if (!body.authored) g->complete=true;
        return;
    }
    if (e->kind != ENEMY_BOSS3_CHUNK) ++g->enemy_deaths[game_random(g) % 3];
    game_drop_pickups(g, e->x, e->y, e->coins, e->powerup, true);
    bool part = e->kind==ENEMY_BOSS3_CHUNK || e->kind==ENEMY_BOSS6_SPHERE;
    death_burst(g,e,part?1.5f:e->kind==ENEMY_MINI_SQUARE?0.5f:1,!part);
    if (e->kind==ENEMY_BIG_BALL || e->kind==ENEMY_AIRSHIP || e->kind==ENEMY_PYRAMID || e->kind==ENEMY_BOSS_CHUNK)
        death_burst(g,e,1.5f,e->kind!=ENEMY_BOSS_CHUNK);
    if (e->kind == ENEMY_SPLITTER) {
        for (int side = -1; side <= 1; side += 2) {
            int n = game_spawn_enemy(g, ENEMY_SPLITTER_FRAGMENT, e->x + side * 11, e->y);
            if (n < 0) continue;
            Enemy *fragment = &g->enemies[n];
            float angle = random_float(g) * 2 * PI;
            fragment->vx = e->vx + side + cosf(angle) * 0.5f;
            fragment->vy = e->vy + sinf(angle) * 0.5f;
            fragment->spin = -side * 0.1f;
            fragment->aux_index = side > 0; /* Original right-hand shell sprite. */
        }
    }
    if (e->kind==ENEMY_PYRAMID) {
        static const float x[] = {0, -48, 0}, y[] = {-32, 32, 32};
        static const float vx[] = {0, -0.5f, 0}, vy[] = {-0.2f, 0.2f, 0.2f};
        for (int i = 0; i < 3; ++i) {
            int n = game_spawn_enemy(g, ENEMY_TRIANGLE, e->x + x[i], e->y + y[i]);
            if (n < 0) continue;
            Enemy *part = &g->enemies[n];
            float speed = random_float(g) * 0.5f, angle = random_float(g) * 2 * PI;
            part->vx = vx[i] + cosf(angle) * speed; part->vy = vy[i] + sinf(angle) * speed;
            part->spin = i == 2 ? -0.025f : 0.025f;
            static const float scale[] = {1,1.25f,1.55f};
            part->health = part->health_max *= 1.5f * scale[g->difficulty];
        }
    }
    if (e->kind == ENEMY_FENCE) {
        e->active = false;
        if (e->parent >= 0 && g->enemies[e->parent].active && g->enemies[e->parent].kind == ENEMY_FENCE &&
            g->enemies[e->parent].children[0] == (int)(e-g->enemies))
            enemy_die(g, &g->enemies[e->parent], -1);
        Enemy *other=fence_partner(g,e);
        if (other) enemy_die(g, other, -1);
    }
    for(int i=0;i<8;++i) {
        int n=e->children[i];
        if(n<0 || !g->enemies[n].active) continue;
        Enemy *child=&g->enemies[n];
        if(child->parent!=(int)(e-g->enemies)) continue;
        if(e->kind==ENEMY_CUTTER) {
            /* Release at the current orbit tangent (126 * 0.03 units/tick).
               The balls remain shot-blocking hazards until they leave view. */
            float spin=e->x<GAME_WIDTH*0.5f?-0.03f:0.03f;
            child->vx=-(child->y-e->y)*spin;
            child->vy=(child->x-e->x)*spin;
            child->spin=spin*(i?-3:3);
            child->parent=-1; child->authored=false; child->coins=0;
        } else if(e->kind==ENEMY_SWIRL) {
            float angle=g->tick*0.1f+i*2*PI/3+PI*0.5f+0.1f;
            child->vx=cosf(angle)*2.5f; child->vy=sinf(angle)*2.5f; child->parent=-1; child->authored=false;
        } else enemy_die(g, child, -1);
    }
    e->active=false;
}
