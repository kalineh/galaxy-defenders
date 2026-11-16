#include "game.h"
#include <math.h>
#include <stddef.h>

#define PI 3.141592654f
static float random_float(Game *g) { return (game_random(g)>>8)/16777216.0f; }
static float angle_to_player(const Game *g, const Enemy *e, float lead) {
    float nearest=1e20f, angle=PI*0.5f;
    for(int i=0;i<GAME_PLAYERS;++i) {
        const Player *p=&g->players[i];
        float dx=p->x+p->vx*lead-e->x,dy=p->y+p->vy*lead-e->y,d=dx*dx+dy*dy;
        if(p->joined && p->lives>0 && d<nearest) { nearest=d; angle=atan2f(dy,dx); }
    }
    return angle;
}
static float approach_angle(float from,float to,float speed) {
    return from+fmaxf(-speed,fminf(speed,remainderf(to-from,2*PI)));
}
static void shot(Game *g,const Enemy *e,float x,float y,float angle) { enemy_shot(g,e,HOSTILE_SHOT,x,y,angle,8,2); }
static Enemy *child(Game *g,Enemy *e,int i) {
    int index=e->children[i];
    if(index<0 || !g->enemies[index].active || g->enemies[index].parent!=(int)(e-g->enemies)) return NULL;
    return &g->enemies[index];
}
static Enemy *make_child(Game *g,Enemy *e,int i,EnemyKind kind,float x,float y) {
    int index=game_spawn_enemy(g,kind,e->x+x,e->y+y);
    if(i>=0) e->children[i]=index;
    if(index<0) return NULL;
    Enemy *c=&g->enemies[index];
    c->parent=(int)(e-g->enemies); c->authored=e->authored;
    c->offset_x=x; c->offset_y=y;
    return c;
}
bool enemy_is_boss(EnemyKind kind) {
    return kind==ENEMY_BOSS || kind==ENEMY_BOSS2 || (kind>=ENEMY_BOSS3 && kind<=ENEMY_BOSS12);
}

void boss_init(Game *g,Enemy *e) {
    float health=1000;
    e->coins=0; e->radius=0;
    e->anchor_x=e->x; e->anchor_y=e->y; e->cooldown=120;
    switch(e->kind) {
        case ENEMY_BOSS3: health=80; e->radius=60; e->orbit_radius=80; break;
        case ENEMY_BOSS4: health=100; e->box_width=260; e->box_height=215; e->collision_y=-12.5f; break;
        case ENEMY_BOSS5: health=120; e->box_width=e->box_height=170; break;
        case ENEMY_BOSS6: health=100; e->radius=110; e->cooldown=150; break;
        case ENEMY_BOSS7: health=140; e->box_width=409; e->box_height=103; e->collision_x=-0.5f; e->collision_y=-12.5f; break;
        case ENEMY_BOSS8: health=180; e->box_width=360; e->box_height=170; e->collision_y=5; break;
        case ENEMY_BOSS9: health=210; e->radius=75; break;
        case ENEMY_BOSS10: health=60; e->radius=60; e->cooldown=150; e->aux_cooldown=300; break;
        case ENEMY_BOSS11: health=180; e->box_width=280; e->box_height=144; break;
        case ENEMY_BOSS12: health=250; e->box_width=400; e->box_height=144; e->collision_y=-8; break;
        case ENEMY_BOSS3_CHUNK: health=100; e->box_width=e->box_height=120; e->cooldown=30; break;
        case ENEMY_BOSS4_CHUNK: e->box_width=e->box_height=70; e->timer=30+(int)(game_random(g)%120); break;
        case ENEMY_BOSS5_CHUNK: health=40; e->radius=36; e->cooldown=240+(int)(game_random(g)%30); break;
        case ENEMY_BOSS6_SPHERE: health=130; e->radius=38; break;
        case ENEMY_BOSS7_CANNON_BIG: case ENEMY_BOSS7_CANNON_SMALL: e->intangible=true; e->cooldown=30; e->aim=-PI*0.5f; break;
        case ENEMY_BOSS8_COLLISION: e->radius=75; break;
        case ENEMY_BOSS9_BALL: e->radius=32; e->timer=60+(int)(random_float(g)*180); break;
        case ENEMY_BOSS9_BAR: e->intangible=true; break;
        case ENEMY_BOSS10_BALL: e->radius=31; break;
        case ENEMY_BOSS10_BLOCK: health=100000; e->box_width=110; e->box_height=89; e->collision_x=-1; e->collision_y=1.5f; e->timer=60; e->intangible=true; break;
        case ENEMY_BOSS11_HAND: e->radius=51; e->move_frame=0; break;
        case ENEMY_BOSS11_ARM: e->box_width=100; e->box_height=32; break;
        case ENEMY_BOSS12_EYE: case ENEMY_BOSS12_ARM: e->intangible=true; break;
        default: break;
    }
    static const float scale[]={1,1.25f,1.55f},boss_scale[]={1,1.5f,2};
    if(enemy_is_boss(e->kind)) health*=boss_scale[g->difficulty];
    e->health=e->health_max=health*scale[g->difficulty];
}

static void idle(Enemy *e,float speed) {
    e->x+=(e->anchor_x+sinf(e->age*0.01f)*200-e->x)*speed;
    e->y+=(e->anchor_y-sinf(e->age*0.02f)*80-e->y)*speed;
}

static void horizontal_patrol(Game *g, Enemy *e) {
    if (e->move_wait>0) { --e->move_wait; return; }
    if (e->move_frame<0) {
        e->move_frame=0; e->move_from=e->x;
        e->move_duration=e->move_phase==0?90:e->phase<2?180:90;
        e->move_to=e->x+(e->move_phase==0?-110:e->move_phase%2?220:-220);
    }
    float t=e->move_frame++/(float)e->move_duration;
    e->x=e->move_from+(e->move_to-e->move_from)*t*t*(3-2*t);
    if(e->move_frame>=e->move_duration) {
        if(e->move_phase>0 && e->phase==0) e->move_wait=e->move_phase%2?30:30+game_random(g)%60;
        ++e->move_phase; e->move_frame=-1;
    }
}

static void boss3(Game *g,Enemy *e) {
    if(e->age==1) for(int i=0;i<2;++i) make_child(g,e,i,ENEMY_BOSS3_CHUNK,i?80:-80,0);
    e->phase=e->health<e->health_max*0.45f?2:e->health<e->health_max*0.75f?1:0;
    idle(e,0.1f);
    if(e->phase) { e->spin+=(e->phase==2?0.05f-e->spin:0.025f-e->spin)*(e->phase==2?0.005f:0.015f); e->rotation+=e->spin; }
    e->orbit_radius+=((e->phase==2?160:e->phase==1?100:80)-e->orbit_radius)*0.01f;
    for(int i=0;i<2;++i) {
        Enemy *c=child(g,e,i); if(!c) continue;
        float angle=e->rotation+(i?0:PI);
        c->x=e->x+cosf(angle)*e->orbit_radius; c->y=e->y+sinf(angle)*e->orbit_radius;
        if(e->phase==2) c->spin+=(-0.01f-c->spin)*0.01f;
        c->rotation+=c->spin;
    }
    if(--e->cooldown>0) return;
    if (e->aux_cooldown) {
        e->aux_cooldown=0;
        if (e->aux_index<e->phase && e->aux_index<2) {
            ++e->aux_index; e->cooldown=60+(60-e->aux_index*10); return;
        }
    }
    int phase=e->aux_index;
    if(!e->shot_index) e->attack=game_random(g)%2;
    int count,delay;
    if(!e->attack) {
        if(phase==2) for(int i=0;i<12;++i) shot(g,e,0,0,PI*0.5f-0.5f+i/12.0f);
        else shot(g,e,0,0,angle_to_player(g,e,0));
        count=phase==2?2:phase==1?12:3; delay=phase==2?8:4;
    } else {
        float angle=angle_to_player(g,e,0);
        for(int i=phase==1?-1:0;i<=(phase==1?1:0);++i) enemy_shot(g,e,HOSTILE_PELLET,0,0,angle+i*0.3f,5,2);
        count=phase==2?16:phase==1?4:6; delay=6;
    }
    e->cooldown=delay;
    if(++e->shot_index==count) {
        e->shot_index=0;
        if (phase<e->phase) { e->aux_cooldown=1; if(phase==1 && !e->attack)e->cooldown+=4; }
        else e->cooldown+=60-phase*10+(phase==1 && !e->attack?4:0);
    }
}

static void boss4(Game *g,Enemy *e) {
    if(e->age==1) for(int i=0;i<3;++i) {
        Enemy *c=make_child(g,e,i,ENEMY_BOSS4_CHUNK,i==0?0:i==1?-140:140,i==0?-140:0);
        if(!c) continue;
        c->attack=game_random(g)%2; c->move_phase=i; c->timer+=i?120:0;
        c->rotation=i==0?PI*0.5f:i==2?PI:0;
        c->collision_x=i==0?-1:i==1?175:-179; c->collision_y=i==0?175:3;
    }
    for(int i=0;i<3;++i) {
        Enemy *c=child(g,e,i); if(!c) continue;
        c->x=e->x+c->offset_x+(i==0?0:i==1?-c->orbit_radius:c->orbit_radius);
        c->y=e->y+c->offset_y+(i==0?-c->orbit_radius:0);
    }
    if(e->health>e->health_max*0.6f || --e->cooldown>0) return;
    if(!e->shot_index) e->attack=game_random(g)%3;
    if(e->attack==0) {
        float angle=angle_to_player(g,e,0); shot(g,e,-75,-58,angle); shot(g,e,75,-58,angle);
    } else enemy_shot(g,e,HOSTILE_PELLET,2,-58,e->attack==1?angle_to_player(g,e,0):PI*0.5f+(random_float(g)*2-1)*0.1f*random_float(g),5,2);
    e->cooldown=e->attack==0?4:6;
    if(++e->shot_index==(e->attack==0?3:8)) { e->shot_index=0; e->cooldown+=30; }
}

static void boss5(Game *g,Enemy *e) {
    if(e->age==1) for(int i=0;i<2;++i) make_child(g,e,i,ENEMY_BOSS5_CHUNK,i?80:-80,0);
    int phase=e->phase;
    if (phase == 2 && e->orbit_angle < PI * 0.5f) e->orbit_angle = fminf(PI * 0.5f, e->orbit_angle + PI / 240);
    if(--e->cooldown>0) return;
    if(!e->shot_index) { e->attack=game_random(g)%2; e->timer=4+game_random(g)%4; e->aim=angle_to_player(g,e,0); }
    int count=e->phase==2?6:4;
    static const float xs4[]={-54,49,-54,49},ys4[]={50,50,-53,-53};
    static const float xs6[]={-54,-3,49,-54,-3,49},ys6[]={50,50,50,-53,-53,-53};
    if(e->phase<2 && e->attack==1) {
        int i=e->shot_index%4; shot(g,e,xs4[i],ys4[i],angle_to_player(g,e,0));
    } else for(int i=0;i<count;++i) shot(g,e,count==4?xs4[i]:xs6[i],count==4?ys4[i]:ys6[i],e->phase==2 && e->attack==0?e->aim:angle_to_player(g,e,0));
    e->cooldown=e->phase==2?15:3;
    int steps=e->phase==2?(e->attack==0?e->timer:1):(e->attack==1?18:1);
    if(++e->shot_index==steps) { e->shot_index=0; e->cooldown+=e->phase==2?30:60; }
}

static void boss6(Game *g,Enemy *e) {
    if(e->age==1) for(int i=0;i<6;++i) make_child(g,e,i,ENEMY_BOSS6_SPHERE,0,0);
    int phase=e->health<e->health_max*0.45f?2:e->health<e->health_max*0.75f?1:0;
    if(phase!=e->phase) { e->phase=phase; e->shot_index=0; e->cooldown=60+game_random(g)%30; e->move_wait=0; e->aux_index=0; }
    /* The core and orbit accelerate independently; neither spins during the opening. */
    for (int pass = 0; pass < (e->age == 1 ? 2 : 1); ++pass) {
        for (int i = 0; i < 6; ++i) {
            Enemy *c = child(g, e, i);
            if (!c) continue;
            float angle = e->orbit_angle + i * 2 * PI / 6;
            c->x = e->x + cosf(angle) * e->orbit_radius;
            c->y = e->y + sinf(angle) * e->orbit_radius;
            if (e->health < 2.5f + 0.5f * i) enemy_die(g,c,-1);
        }
        e->orbit_radius += ((e->age < 90 ? 120 : 145) - e->orbit_radius) * 0.02f;
        e->spin += ((e->aux_index ? 0.2f : phase == 2 ? 0.1f : phase == 1 ? 0.05f : e->age >= 90 ? 0.02f : 0) - e->spin) * 0.025f;
        e->orbit_angle += e->spin;
        e->center_spin += ((phase == 2 ? -0.05f : phase == 1 ? -0.02f : e->age >= 90 ? -0.01f : 0) - e->center_spin) * 0.01f;
        e->rotation += e->center_spin;
    }
    if(e->age>=90) {
        if(!phase) idle(e,0.01f);
        else {
            if(--e->move_wait<=0) { float angle=random_float(g)*2*PI; e->move_from=480+cosf(angle)*300; e->move_to=420+sinf(angle)*300; e->move_wait=phase==2?90:180; }
            float speed=phase==2?0.02f:0.01f; e->x+=(e->move_from-e->x)*speed; e->y+=(e->move_to-e->y)*speed;
        }
    }
    if(--e->cooldown>0) return;
    if(!e->shot_index && !e->aux_index) {
        e->attack=game_random(g)%3; e->aim=angle_to_player(g,e,phase==2?32:0);
        if(e->attack==2 && phase<2) { e->aux_index=1; e->cooldown=60; return; }
    }
    int index=e->shot_index,count=1,delay=phase==0 && e->attack==0?2:3;
    if(e->attack==2 && phase<2) {
        Enemy *c=child(g,e,index%6); if(c) shot(g,c,0,0,angle_to_player(g,c,0)); count=30;
    } else if(e->attack==0 && phase>0) {
        int c=index/(phase==2?4:8);
        for(int j=0;j<(phase==2?6:2);++j) { int n=(c+j*(phase==2?1:3))%6; Enemy *part=child(g,e,n); if(part) shot(g,part,0,0,e->orbit_angle+n*2*PI/6); }
        count=phase==2?24:48; delay=2;
    } else {
        int steps=phase==0?(e->attack==0?6:5):phase==1?14:10;
        int cannon=(index/steps)%4;
        float base=e->attack==0?PI*0.5f:e->aim;
        float angle=base+(index%steps)*0.05f*(cannon%2?-1:1);
        if(phase==2 && e->attack==2) { cannon=index%4; angle=e->aim; count=32; delay=4; }
        else count=steps*4;
        float offset=e->rotation+cannon*PI*0.5f+PI;
        shot(g,e,cosf(offset)*40,sinf(offset)*40,angle);
    }
    e->cooldown=delay;
    if(++e->shot_index==count) { e->shot_index=0; e->aux_index=0; e->cooldown+=60+game_random(g)%30; }
}

static void boss7(Game *g,Enemy *e) {
    if(e->age==1) { make_child(g,e,0,ENEMY_BOSS7_CANNON_BIG,-1,-57); make_child(g,e,1,ENEMY_BOSS7_CANNON_SMALL,-128,0); make_child(g,e,2,ENEMY_BOSS7_CANNON_SMALL,124,0); }
    e->phase=e->health<e->health_max*0.45f?2:e->health<e->health_max*0.75f?1:0;
    if(e->age>150) horizontal_patrol(g,e);
    for(int i=0;i<3;++i) { Enemy *c=child(g,e,i); if(c) { c->x=e->x+c->offset_x; c->y=e->y+c->offset_y; } }
    if(e->phase && --e->cooldown<=0) {
        enemy_shot(g,e,HOSTILE_MISSILE,-183,-38,PI*0.5f,2.5f,4); enemy_shot(g,e,HOSTILE_MISSILE,178,-38,PI*0.5f,2.5f,4);
        e->cooldown=e->phase==2?60:90;
    }
}

static void boss8(Game *g,Enemy *e) {
    idle(e,0.1f);
    if(e->timer>0) { --e->timer; e->rotation+=PI/50; }
    if(e->phase && --e->aux_cooldown<=0) { e->aux_index^=1; e->aux_cooldown=(e->aux_index?120:200)+game_random(g)%90; }
    if(e->aux_index) for(int i=0;i<GAME_PLAYERS;++i) {
        Player *p=&g->players[i]; if(!p->joined || p->lives<=0) continue;
        float dx=p->x-e->x,dy=p->y-e->y,d=hypotf(dx,dy);
        if(d>0){p->vx+=dx/d;p->vy+=dy/d;}
        for(int side=-1;side<=1;side+=2) {
            float angle=atan2f(dy,dx)+(random_float(g)*2-1)*0.2f;
            game_particle(g,PARTICLE_BOSS8_WIND,e->x+side*116,e->y+12,2,1,cosf(angle)*18,sinf(angle)*18);
        }
    }
    if(--e->cooldown>0) return;
    if(!e->shot_index) { e->attack=game_random(g)%(e->phase?4:3); e->aim=angle_to_player(g,e,0); }
    static const float x[]={-128,-108,-83,85,109,133},y[]={82,92,82,84,92,80};
    int index=e->shot_index,count,delay;
    if(e->attack==0) {
        for(int i=0;i<3;++i) { int n=i+index%2*3; shot(g,e,x[n],y[n],PI*0.5f); }
        count=e->phase?12:8;delay=e->phase?2:8;
    } else if(e->attack==1) {
        if(index%12==0) e->aim=angle_to_player(g,e,0);
        int n=index%6; shot(g,e,x[n],y[n],e->aim);count=36;delay=3;
    } else {
        int n=index%3,m=5-n,j=index%18;
        if(e->attack==3 && j==0) e->aim=angle_to_player(g,e,0);
        shot(g,e,x[n],y[n],e->aim-0.3f+j*0.05f); shot(g,e,x[m],y[m],e->aim+0.3f-j*0.05f);
        count=e->attack==3?54:e->phase?24:18;delay=2;
    }
    e->cooldown=delay;
    if(++e->shot_index==count) {e->shot_index=0;e->cooldown+=e->phase?30:60;}
}

static void boss9(Game *g,Enemy *e) {
    if(e->age==1) {
        make_child(g,e,0,ENEMY_BOSS9_BAR,0,0);
        for(int i=1;i<=2;++i) { Enemy *c=make_child(g,e,i,ENEMY_BOSS9_BALL,0,0); if(c)c->orbit_radius=120; }
    }
    idle(e,0.1f); e->orbit_angle+=0.02f+0.075f*(1-e->health/e->health_max);
    Enemy *bar=child(g,e,0);if(bar){bar->x=e->x;bar->y=e->y;bar->rotation=e->orbit_angle;}
    for(int i=1;i<=2;++i) {
        Enemy *c=child(g,e,i);
        if(!c) {
            int *wait = i == 1 ? &e->aux_cooldown : &e->aux_index;
            if(++*wait>=60) {c=make_child(g,e,i,ENEMY_BOSS9_BALL,0,0);*wait=0;} else continue;
        }
        if(!c) continue;
        float a=e->orbit_angle+(i==1?PI:0),nx=cosf(a),ny=sinf(a);
        c->x=e->x+nx*c->orbit_radius;c->y=e->y+ny*c->orbit_radius;c->orbit_radius=fminf(120,c->orbit_radius+4);
        float target=angle_to_player(g,c,0),dot=cosf(target)*nx+sinf(target)*ny,front=cosf(target)*-ny+sinf(target)*nx;
        if(--c->timer<=0 && fabsf(dot)<=0.05f && front>0) { float speed=4+6*(1-e->health/e->health_max);c->vx=-ny*speed;c->vy=nx*speed;c->parent=-1;e->children[i]=-1; }
    }
    if(--e->cooldown>0)return;
    if(!e->shot_index)e->attack=game_random(g)%4;
    int j=e->shot_index,count=1,delay=2;
    for(int i=0;i<(e->attack==0?4:e->attack==3?1:8);++i) {
        int n=e->attack==0?i*2+j%2:e->attack==3?j%8:i;
        float a=n*PI*0.25f-PI*0.5f,dir;
        if(e->attack==0){dir=a-0.4f+j/6*0.2f;count=24;}
        else if(e->attack==1){dir=angle_to_player(g,e,10);count=4;delay=15;}
        else {dir=n*PI*0.25f;count=e->attack==2?6:32;delay=e->attack==2?6:2;}
        enemy_shot(g,e,HOSTILE_SHOT,cosf(a)*40,sinf(a)*40,dir,8,3);
    }
    e->cooldown=delay;if(e->attack==0 && (j+1)%6==0)e->cooldown+=6;
    if(++e->shot_index==count){e->shot_index=0;e->cooldown+=60;}
}

static void boss10(Game *g,Enemy *e) {
    if(e->age==1)for(int row=0;row<3;++row)for(int col=0;col<7;++col)make_child(g,e,-1,ENEMY_BOSS10_BLOCK,120+col*112-e->x,110+row*92);
    if(e->age==60)for(int i=0;i<2;++i){Enemy *c=make_child(g,e,i,ENEMY_BOSS10_BALL,480-e->x,400);if(c){c->vx=i?0.7071f:-0.7071f;c->vy=0.7071f;}}
    if(--e->move_wait<=0){float x=sinf(e->age*0.01f)*200;e->move_to=e->anchor_x-fmodf(e->anchor_x-480,112)+x-fmodf(x,112)-22;e->move_wait=90+game_random(g)%30;e->move_frame=60;}
    if(e->move_frame>0){--e->move_frame;e->x+=(e->move_to-e->x)*0.15f;}
    if(--e->cooldown<=0){
        if(!e->shot_index){e->attack=game_random(g)%3;e->aim=angle_to_player(g,e,0);}
        int j=e->shot_index;
        if(e->attack<2)for(int i=0;i<2;++i)enemy_shot(g,e,HOSTILE_SHOT,i?16:-16,0,e->attack==0?PI*0.5f+(i?0.4f-j*0.1f:-0.4f+j*0.1f):e->aim+(i?0.2f:-0.2f),8,3);
        e->cooldown=e->attack==0?3:4;
        if(++e->shot_index==(e->attack==0?14:e->attack==1?8:1)){e->shot_index=0;e->cooldown+=90;}
    }
    if(--e->aux_cooldown<=0){
        if(!e->aux_index)e->hit_count=game_random(g)%3;
        if(e->hit_count<2)enemy_shot(g,e,HOSTILE_LASER,0,64,PI*0.5f,8,2);
        e->aux_cooldown=e->hit_count==0?10:16;
        if(++e->aux_index==(e->hit_count==0?3:e->hit_count==1?6:1)){e->aux_index=0;e->aux_cooldown+=120+game_random(g)%30;}
    }
}

static void boss11(Game *g,Enemy *e) {
    if(e->age==1) {
        for(int i=0;i<2;++i){Enemy *arm=make_child(g,e,i,ENEMY_BOSS11_ARM,0,0);if(arm)arm->rotation=i?0:PI;
            make_child(g,e,i+2,ENEMY_BOSS11_HAND,i?44:-44,0);}
        e->cooldown=30;
    }
    e->phase=e->health<e->health_max*0.45f?2:e->health<e->health_max*0.8f?1:0;
    if(e->age>90) horizontal_patrol(g,e);
    for(int i=0;i<2;++i){Enemy *arm=child(g,e,i),*hand=child(g,e,i+2);if(!arm || !hand)continue;
        if(e->phase)arm->orbit_radius=fminf(192,arm->orbit_radius+192.0f/120);
        arm->x=e->x+(i?1:-1)*arm->orbit_radius;arm->y=e->y;
        hand->x=arm->x+(i?44:-44);hand->y=arm->y;
        if(arm->orbit_radius>=192)hand->phase=1;
    }
    if(--e->cooldown<=0){
        if(!e->shot_index)e->timer=1+game_random(g)%5;
        enemy_shot(g,e,HOSTILE_LASER,-54,91,PI*0.5f,8,2);enemy_shot(g,e,HOSTILE_LASER,54,91,PI*0.5f,8,2);
        e->cooldown=12;if(++e->shot_index==e->timer){e->shot_index=0;e->cooldown+=e->phase==2?30:e->phase==1?45:60;}
    }
    if(e->phase==2 && --e->aux_cooldown<=0){
        static const float cannons[]={-106,-91,-76,-15,0,15,79,94,109};
        if(!e->aux_index)e->attack=game_random(g)%3;
        int j=e->aux_index,count,delay;
        if(e->attack==0){for(int i=0;i<3;++i)enemy_shot(g,e,HOSTILE_MISSILE,cannons[j*3+i],-32,PI*0.5f,3.5f,4);count=3;delay=10;}
        else if(e->attack==1){int n=(j/3%3)*3+j%3;enemy_shot(g,e,HOSTILE_MISSILE,cannons[n],-32,PI*0.5f,3.5f,4);count=27;delay=6;}
        else {int a=j<3?j:j==3?3:4,b=j<3?8-j:j==3?5:4;enemy_shot(g,e,HOSTILE_MISSILE,cannons[a],-32,PI*0.5f,3.5f,4);if(a!=b)enemy_shot(g,e,HOSTILE_MISSILE,cannons[b],-32,PI*0.5f,3.5f,4);count=5;delay=8;}
        e->aux_cooldown=delay;if(++e->aux_index==count){e->aux_index=0;e->aux_cooldown+=60;}
    }
}

static void boss12(Game *g, Enemy *e) {
    if (e->age == 1) {
        for (int side = 0; side < 2; ++side) for (int row = 0; row < 3; ++row)
            make_child(g, e, side * 3 + row, ENEMY_BOSS12_ARM, side ? -110 : 110, -60 + row * 50);
        make_child(g, e, 6, ENEMY_BOSS12_EYE, -38, 42);
        make_child(g, e, 7, ENEMY_BOSS12_EYE, 52, 42);
        e->cooldown = 30;
        e->collision_y = -8;
    }
    e->phase = e->health < e->health_max * 0.45f ? 2 : e->health < e->health_max * 0.8f ? 1 : 0;
    if (e->age > 90 && --e->move_wait <= 0) {
        int duration = e->move_phase == 0 || e->phase == 2 ? 90 : 180;
        if (e->move_frame < 0) {
            e->move_frame = 0;
            e->move_from = e->x;
            e->move_to = e->anchor_x + (e->move_phase % 2 ? 110 : -110);
        }
        float t = e->move_frame++ / (float)duration;
        e->x = e->move_from + (e->move_to - e->move_from) * t * t * (3 - 2 * t);
        if (e->move_frame > duration) {
            ++e->move_phase;
            e->move_frame = -1;
            e->move_wait = e->phase ? 0 : 30 + (e->move_phase % 2 ? game_random(g) % 60 : 0);
        }
    }
    for (int i = 0; i < 8; ++i) {
        Enemy *c = child(g, e, i);
        if (!c) continue;
        c->x = e->x + c->offset_x;
        c->y = e->y + c->offset_y;
        if (i < 6) c->rotation = i < 3 ? -PI + sinf(c->age * 0.03f) * 0.06f - 0.07f :
            sinf(c->age * 0.03f) * -0.06f + 0.07f;
        else {
            if (e->age % 8 == 0) c->aim = angle_to_player(g, c, 0);
            c->orbit_angle = approach_angle(c->orbit_angle, c->aim, 0.02f);
            c->x += cosf(c->orbit_angle) * 10;
            c->y += sinf(c->orbit_angle) * 10;
        }
    }
    if (--e->cooldown > 0) return;
    if (!e->shot_index) {
        e->attack = game_random(g) % (e->phase == 0 ? 4 : e->phase == 1 ? 2 : 3);
        e->aux_index = e->phase;
    }
    int phase = e->aux_index, count = 1, delay = 0;
    static const float cannons[] = {-70, -23, 23, 70};
    float speed = 8 + phase * 3;
    if (e->attack == 0) {
        for (int row = 0; row < 4; ++row) for (int col = 0; col < (phase == 1 ? 2 : 4); ++col)
            enemy_shot(g, e, HOSTILE_MINI, cannons[col], -54 + row * 12, PI * 0.5f, speed, 1.25f);
        count = phase == 0 ? 8 : phase == 1 ? 9 : 3;
        delay = phase == 0 ? 8 : 6;
    } else if (e->attack == 1) {
        float angle = angle_to_player(g, e, 0);
        for (int col = 0; col < (phase == 1 ? 2 : 4); ++col)
            enemy_shot(g, e, HOSTILE_MINI, cannons[col], -18, angle, speed, 1.25f);
        count = phase == 0 ? 8 : phase == 1 ? 12 : 20;
        delay = phase == 0 ? 6 : phase == 1 ? 3 : 10;
    } else if (e->attack == 2) {
        if (e->shot_index % 2 == 0) e->aim = angle_to_player(g, e, 4);
        for (int col = 0; col < 4; ++col)
            enemy_shot(g, e, HOSTILE_MINI, cannons[col], -18, e->aim + (col == 0 || col == (phase==2?3:2) ? -0.2f : 0.2f), speed, 1.25f);
        count = phase == 0 ? 28 : 12;
        delay = phase == 0 && e->shot_index % 2 == 0 ? 1 : 3;
    }
    e->cooldown = delay;
    if (++e->shot_index == count) {
        e->shot_index = 0;
        e->cooldown += phase == 0 ? 20 : phase == 1 ? 15 : e->health > e->health_max * 0.05f ? 10 : 0;
    }
}

static void part_update(Game *g,Enemy *e) {
    Enemy *boss=e->parent>=0?&g->enemies[e->parent]:NULL;
    if(boss && !boss->active){e->active=false;return;}
    if(e->kind==ENEMY_BOSS9_BALL && !boss){e->x+=e->vx;e->y+=e->vy+g->scroll_speed;return;}
    if(!boss)return;
    if(e->kind==ENEMY_BOSS3_CHUNK){
        if(boss->phase && --e->cooldown<=0){for(int x=-1;x<=1;x+=2)for(int y=-1;y<=1;y+=2)shot(g,e,x*34,y*44,PI*0.5f);e->cooldown=boss->phase==2?60:90;}
    } else if(e->kind==ENEMY_BOSS4_CHUNK){
        if(e->timer>0){--e->timer;return;}
        if(e->phase==0){e->orbit_radius+=((e->move_phase==0?-100:-140)-e->orbit_radius)*0.05f;if(++e->move_frame>=60){e->phase=1;e->move_frame=0;}}
        else if(e->phase==2){e->orbit_radius+=(0-e->orbit_radius)*0.1f;if(++e->move_frame>=30){e->phase=0;e->move_frame=0;e->timer=150+game_random(g)%120;}}
        else {
            if (--e->aux_cooldown<=0) {
                e->aux_cooldown=30;
                if(e->damage_taken>=5 && boss->health>=boss->health_max*(e->move_phase==0?0.3f:0.25f)) {
                    e->damage_taken=0;e->phase=2;
                    for(int color=2;color<=3;++color) game_particle(g,PARTICLE_ENEMY_DEATH_EXPLOSION,
                        e->x+(e->move_phase==0?-1:e->move_phase==1?182:-182),e->y+(e->move_phase==0?180:-6),color,1,0,0);
                    return;
                }
            }
            if(--e->cooldown>0)return;
            if(!e->shot_index)e->aim=e->attack==0?PI*0.5f:angle_to_player(g,e,0);
            float x=e->move_phase==0?-1:e->move_phase==1?182:-182,y=e->move_phase==0?180:-6;
            shot(g,e,x,y,e->aim-0.15f+0.3f/(e->attack==0?6:3)*e->shot_index);
            e->cooldown=e->attack==0?6:3;if(++e->shot_index==(e->attack==0?6:3)){e->shot_index=0;e->cooldown+=e->attack==0?90:60;}
        }
    } else if(e->kind==ENEMY_BOSS5_CHUNK){
        if(!boss->phase){e->x=boss->x+e->offset_x;e->y=boss->y;return;}
        ++e->timer;
        if(e->timer<150 && e->timer>=30)e->x=e->anchor_x+(e->offset_x<0?-300:300)*(e->timer-30)/120.0f;
        else if(e->timer>=210){if(--e->move_wait<=0){e->move_from=e->y;e->move_to=e->anchor_y+(e->move_phase++%2?0:(game_random(g)%2?200:-200));e->move_frame=0;e->move_wait=180+game_random(g)%30;}float t=fminf(1,e->move_frame++/120.0f);e->y=e->move_from+(e->move_to-e->move_from)*t;}
        if(--e->cooldown<=0){if(!e->shot_index){e->attack=game_random(g)%2;e->aim=angle_to_player(g,e,0);}int count=e->attack==0?e->shot_index+1:1;for(int i=0;i<count;++i)shot(g,e,0,0,(e->attack==0?e->aim:angle_to_player(g,e,0))+(i-(count-1)*0.5f)*(count==2?0.08f:0.06f));e->cooldown=4;if(++e->shot_index==3){e->shot_index=0;e->cooldown+=60+game_random(g)%30;}}
    } else if(e->kind==ENEMY_BOSS7_CANNON_BIG || e->kind==ENEMY_BOSS7_CANNON_SMALL){
        bool big=e->kind==ENEMY_BOSS7_CANNON_BIG;
        if (!big && e->timer>0) { e->rotation=approach_angle(e->rotation,e->aim,0.025f); --e->timer; return; }
        if(--e->cooldown<=0) {
            if(!e->shot_index) {
                e->aim=angle_to_player(g,e,0)-PI*0.5f;
                if(!big) { e->timer=14; e->shot_index=1; e->rotation=approach_angle(e->rotation,e->aim,0.025f); return; }
            }
            float a=e->rotation+PI*0.5f,d=big?94:50;shot(g,e,cosf(a)*d,sinf(a)*d,a);
            e->cooldown=big?4:30;
            if(!big || ++e->shot_index==4) { e->shot_index=0;e->cooldown=30; }
        }
        if(big) e->rotation=approach_angle(e->rotation,e->aim,0.025f);
    } else if(e->kind==ENEMY_BOSS11_HAND && e->phase){
        if(++e->timer>30) {
            if(e->move_wait>0) --e->move_wait;
            else if(++e->move_frame<=120)e->spin+=0.004f;
            else if(e->move_frame<=240)e->spin-=0.004f;
            else {e->spin=0;e->move_frame=0;e->move_wait=30+game_random(g)%3*15;}
            e->rotation+=e->spin;
            if((e->spin>=0.4f || e->shot_index>0) && --e->cooldown<=0) {
                static const float angles[]={0,PI,PI*0.5f,-PI*0.5f};
                float angle=e->rotation+angles[e->shot_index%4];
                shot(g,e,cosf(angle)*14,sinf(angle)*14,angle_to_player(g,e,0));e->cooldown=4;e->shot_index=(e->shot_index+1)%15;
            }
        }
    } else if(e->kind==ENEMY_BOSS10_BLOCK){
        if (e->timer > 0) { --e->timer; e->orbit_radius += 1.0f/60; }
        else if (e->timer < 0) {
            ++e->timer;
            e->orbit_radius = e->timer > -60 ? e->orbit_radius + 1.0f/60 : fmaxf(0, e->orbit_radius - 1.0f/60);
        }
        if (!e->timer) { e->orbit_radius = 1; e->intangible = false; }
    }
    else if(e->kind==ENEMY_BOSS10_BALL){
        e->x+=e->vx;e->y+=e->vy;if(e->timer>0)--e->timer;
        if(e->x<=g->stage.camera_x+31 || e->x>=g->stage.camera_x+GAME_WIDTH-31){e->x=fmaxf(g->stage.camera_x+31,fminf(g->stage.camera_x+GAME_WIDTH-31,e->x));e->vx=-e->vx;e->x+=e->vx;e->y+=e->vy;}
        if(e->y<=31 || e->y>=GAME_HEIGHT-31){e->y=fmaxf(31,fminf(GAME_HEIGHT-31,e->y));e->vy=-e->vy;e->x+=e->vx;e->y+=e->vy;}
        if(e->timer>0)game_particle(g,PARTICLE_BOSS10_BALL_TRAIL,e->x,e->y,3,1,0,0);
        for(int i=0;i<GAME_ENEMIES;++i){Enemy *block=&g->enemies[i];if(!block->active || block->kind!=ENEMY_BOSS10_BLOCK || block->intangible)continue;float dx=e->x-block->x-block->collision_x,dy=e->y-block->y-block->collision_y;
            float near_x=fmaxf(fabsf(dx)-55,0),near_y=fmaxf(fabsf(dy)-44.5f,0);
            if(near_x*near_x+near_y*near_y>31*31)continue;bool horizontal=fabsf(dx)>fabsf(dy);if((horizontal?dx*e->vx:dy*e->vy)>0)continue;if(horizontal)e->vx=-e->vx;else e->vy=-e->vy;e->x+=e->vx;e->y+=e->vy;float d=hypotf(e->vx,e->vy);if(d>0){e->vx=e->vx/d*6;e->vy=e->vy/d*6;}if(e->timer){block->timer=-3600;block->intangible=true;e->timer=0;}}
        for(int i=0;i<GAME_PLAYERS;++i) {
            Player *p=&g->players[i];
            float dx=e->x-p->x,dy=e->y-p->y,radius=p->shield_health>0?24:PLAYER_ARMOR_RADIUS;
            if(g->seeking || e->timer>5940 || !p->joined || p->lives<=0 || dx*dx+dy*dy>=31*31+radius*radius)continue;
            e->vx=(dx<0?-1:1)*fabsf(e->vx);e->vy=-fabsf(e->vy);
            float d=hypotf(e->vx,e->vy);if(d>0){e->vx=e->vx/d*8;e->vy=e->vy/d*8;}
            e->x+=e->vx;e->y+=e->vy;e->timer=6000;p->vx-=dx*0.5f;p->vy-=dy*0.5f;
        }
    }
}

void boss_update_all(Game *g,Enemy *e) {
    if(e->kind>=ENEMY_BOSS3_CHUNK){++e->age;part_update(g,e);return;}
    if(e->authored){e->y+=g->scroll_speed;e->anchor_y+=g->scroll_speed;}
    ++e->age;
    switch(e->kind){
        case ENEMY_BOSS3:boss3(g,e);break;case ENEMY_BOSS4:boss4(g,e);break;
        case ENEMY_BOSS5:boss5(g,e);break;case ENEMY_BOSS6:boss6(g,e);break;
        case ENEMY_BOSS7:boss7(g,e);break;case ENEMY_BOSS8:boss8(g,e);break;
        case ENEMY_BOSS9:boss9(g,e);break;case ENEMY_BOSS10:boss10(g,e);break;
        case ENEMY_BOSS11:boss11(g,e);break;case ENEMY_BOSS12:boss12(g,e);break;
        default:break;
    }
}

bool boss_damage(Game *g,Enemy *e,float damage,int owner) {
    if(e->kind<ENEMY_BOSS3)return false;
    if(e->intangible || e->invisible || e->kind==ENEMY_BOSS10_BLOCK)return true;
        bool shared=e->kind==ENEMY_BOSS3_CHUNK || e->kind==ENEMY_BOSS4_CHUNK || e->kind==ENEMY_BOSS6_SPHERE ||
        e->kind==ENEMY_BOSS8_COLLISION || e->kind==ENEMY_BOSS11_HAND || e->kind==ENEMY_BOSS11_ARM;
    if(shared){e->damage_taken+=damage;if(e->parent<0 || !g->enemies[e->parent].active)return true;e=&g->enemies[e->parent];}
    else if(e->kind==ENEMY_BOSS4 && e->health>e->health_max*0.6f)return true;
    if (e->kind==ENEMY_BOSS5) {
        if(e->health>=e->health_max*0.8f && e->health-damage<e->health_max*0.8f) e->phase=1;
        else if(e->health>=e->health_max*0.5f && e->health-damage<e->health_max*0.5f) {
            e->phase=2; e->cooldown=150; e->shot_index=0;
        }
    }
    if (e->kind==ENEMY_BOSS8 && e->health>e->health_max*0.5f && e->health-damage<=e->health_max*0.5f) {
        e->phase=1; e->cooldown=170; e->shot_index=0; e->timer=150; e->aux_cooldown=200+game_random(g)%90;
    }
    if (e->kind==ENEMY_BOSS4 && e->health>e->health_max*0.6f && e->health-damage<e->health_max*0.6f) {
        for(int i=0;i<4;++i) for(int color=2;color<=3;++color) {
            float angle=random_float(g)*2*PI, radius=(random_float(g)*2-1)*100;
            game_particle(g,PARTICLE_ENEMY_DEATH_EXPLOSION,e->x+cosf(angle)*radius,e->y+sinf(angle)*radius,color,1.2f,0,0);
        }
        e->cooldown=30; e->shot_index=0;
    }
    e->health-=damage*(g->players[0].joined && g->players[1].joined?0.75f:1);
    if(e->health<=0) { ++g->stage.stats.enemy_kills; enemy_die(g,e,owner); }
    return true;
}
