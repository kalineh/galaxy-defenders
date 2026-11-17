#include "game.h"
#include <string.h>
#include <math.h>

bool game_shop_chassis_available(const Game *game, int slot, int chassis) {
    int items[WEAPON_COUNT+1], count=game_shop_stock(game,slot,0,items);
    for(int i=0;i<count;++i)if(items[i]==chassis)return true;
    return false;
}

int game_shop_equipped(const Player *p, int category) {
    switch (category) {
        case 0: return p->chassis;
        case 1: return p->generator;
        case 2: return p->shield;
        case 3: return p->primary;
        case 4: return p->secondary;
        case 5: return p->sidekick;
        default: return -1;
    }
}

bool game_shop_item_available(int category, int item) {
    if (category == 0) return item >= 0 && item < GAME_CHASSIS;
    if (category == 1) return item >= 0 && item < GENERATOR_COUNT;
    if (category == 2) return item >= 0 && item < SHIELD_COUNT;
    if (category == 4 || category == 5) if (item == -1) return true;
    return item >= 0 && item < WEAPON_COUNT && equipment_weapon_category(item) == category;
}

static int item_price(int category, int item, int level) {
    if (item < 0) return 0;
    if (category == 0) return game_chassis[item].price;
    if (category == 1) return game_generators[item].price;
    if (category == 2) return game_shields[item].price;
    return equipment_weapon_price(item, level);
}

static float stock_random(GameRandom *state) {
    float magnitude = (float)(game_random_sample(state) / 2147483647.0);
    return magnitude * (game_random_sample(state) / 2147483647.0 < 0.5 ? -1 : 1);
}

int game_shop_stock(const Game *game, int slot, int category, int items[WEAPON_COUNT+1]) {
    if(slot<0 || slot>=GAME_PLAYERS || category<0 || category>5)return 0;
    static const int weapons[3][6]={
        {WEAPON_FRONT_LASER,WEAPON_SPREAD_LASER,WEAPON_PLASMA,WEAPON_FLAME,WEAPON_LIGHTNING,WEAPON_BEAM},
        {WEAPON_MISSILE,WEAPON_DRUNK_MISSILE,WEAPON_SEEK_BOMB,WEAPON_BOOMERANG,WEAPON_VULCAN,WEAPON_BOMBLET},
        {WEAPON_MINI_SHOT,WEAPON_CHARGE_SHOT,WEAPON_BLADE,WEAPON_MISSILE_VOLLEY,WEAPON_MISSILE_LAUNCHER,WEAPON_LASER_SHOT}
    };
    int total=category==0?GAME_CHASSIS:category==1?GENERATOR_COUNT:category==2?SHIELD_COUNT:6;
    bool chosen[WEAPON_COUNT]={0};
    int stage=game->menu.next_stage;
    /* Stage1..12 GetHashCode values from the legacy x86 .NET Framework runtime. */
    static const int stage_hashes[12] = {843427117,843230509,843296045,843623725,843689261,843492653,
        843558189,842837293,842902829,1165835275,-1563048080,3035861};
    GameRandom seed;
    game_random_init(&seed, game->seed + (uint32_t)stage_hashes[stage < 12 ? stage : 11] +
        (category < 3 ? category + 4 : category - 2));
    float ratio=category<3?stage/12.0f+0.20f:0.5f;
    int desired=(int)nearbyintf(total*(0.4f+(category<3?0.4f:0.5f)*stock_random(&seed)));
    if(desired<2)desired=2; if(desired>total)desired=total;
    int center=(int)nearbyintf(total*ratio), selected=0;
    for(int attempt=0;attempt<30 && selected<desired;++attempt) {
        int index=center+(int)(stock_random(&seed)*total*ratio);
        if(index<0)index=0; if(index>=total)index=total-1;
        if(!chosen[index]) {chosen[index]=true;++selected;}
    }
    int count=0, owned=game_shop_equipped(&game->players[slot],category);
    if(!game->cleared_game && owned>=0)items[count++]=owned;
    if(category>=4)items[count++]=-1;
    for(int i=0;i<total;++i) {
        int id=category<3?i:weapons[category-3][i];
        bool available=game->cleared_game?(category>=3 || i>=(category==1?6:3)):chosen[i];
        if(available && (game->cleared_game || id!=owned))items[count++]=id;
    }
    for(int i=1;i<count;++i) {
        int id=items[i], at=i;
        while(at>0 && item_price(category,items[at-1],0)>item_price(category,id,0)) {items[at]=items[at-1];--at;}
        items[at]=id;
    }
    return count;
}

static bool in_stock(const Game *game,int slot,int category,int item) {
    int items[WEAPON_COUNT+1], count=game_shop_stock(game,slot,category,items);
    for(int i=0;i<count;++i)if(items[i]==item)return true;
    return false;
}

int game_shop_cost(const Game *game, int slot) {
    if (slot < 0 || slot >= GAME_PLAYERS || !game->players[slot].joined) return 0;
    const Player *p = &game->players[slot];
    int category = game->menu.shop_category[slot], item = game->menu.shop_item[slot];
    if (!game_shop_item_available(category, item)) return 0;
    int old_level = category == 3 ? p->weapon_level : category == 4 ? p->secondary_level : 0;
    return item_price(category, item, game->menu.shop_level[slot]) -
        item_price(category, game_shop_equipped(p, category), old_level);
}

bool game_shop_buy(Game *game, int slot) {
    if (slot < 0 || slot >= GAME_PLAYERS || game->menu.page != MENU_SHOP || !game->players[slot].joined ||
        !game->menu.shop_items[slot] || game->menu.ready[slot]) return false;
    Player *p = &game->players[slot];
    int category = game->menu.shop_category[slot], item = game->menu.shop_item[slot], level = game->menu.shop_level[slot];
    if (!game_shop_item_available(category, item) || !in_stock(game,slot,category,item)) return false;
    if (category >= 3 && item >= 0 && (level < 0 || level >= game_weapons[item].level_count)) return false;
    int cost = game_shop_cost(game, slot);
    if (cost > p->money) return false;
    switch (category) {
        case 0: if (!game_equip_chassis(game, slot, item)) return false; break;
        case 1: p->generator = item; p->energy = game_generators[item].capacity; break;
        case 2: p->shield = item; p->shield_health = game_shields[item].capacity; break;
        case 3: p->primary = item; p->weapon_level = level; p->powerup_levels = 0; break;
        case 4: p->secondary = item; p->secondary_level = level; break;
        case 5: p->sidekick = item; break;
        default: return false;
    }
    memset(p->weapons, 0, sizeof(p->weapons));
    p->money -= cost;
    game_checkpoint(game, true);
    return true;
}

void game_shop_open(Game *game) {
    game->menu.next_stage = (game->complete || game->menu.page == MENU_CLEAR) && game->stage.index < 11 ?
        game->stage.index + 1 : game->stage.index;
    game_record_stage(game);
    /* World.SetScoreSaveData transfers score exactly once before the shop. */
    for (int i = 0; i < GAME_PLAYERS; ++i) {
        Player *p = &game->players[i];
        if (!p->initialized) continue;
        p->money += p->score;
        p->score = 0;
        p->lives = 1;
        p->health = game_chassis[p->chassis].armor;
        game->menu.ready[i] = false;
        game->menu.shop_fly_frames[i] = 0;
        game->menu.shop_category[i] = 0;
        game->menu.shop_item[i] = p->chassis;
        game->menu.shop_items[i] = false;
    }
    game_menu_open(game, MENU_SHOP);
    game_checkpoint(game, true);
}

void game_shop_update(Game *game) {
    GameMenu *menu = &game->menu;
    bool all_ready = game->players[0].joined || game->players[1].joined;
    for (int i = 0; i < GAME_PLAYERS; ++i) {
        if (!menu->ready[i]) menu->shop_fly_frames[i] = 0;
        else if (menu->shop_fly_frames[i] < 34) ++menu->shop_fly_frames[i];
    }
    for (int i = 0; i < GAME_PLAYERS; ++i) if (game->players[i].joined && menu->shop_fly_frames[i] < 34) all_ready = false;
    if (all_ready && game->transition_frames == 0)
        game_transition(game, game->cleared_game ? TRANSITION_STAGE_SELECT : TRANSITION_STAGE, menu->next_stage);
}

void game_shop_input(Game *game, MenuAction action, int slot) {
    if (slot < 0 || slot >= GAME_PLAYERS || !game->players[slot].joined) return;
    GameMenu *menu = &game->menu;
    if (action == MENU_BACK) {
        if (menu->ready[slot]) { menu->ready[slot] = false; menu->shop_fly_frames[slot] = 0; }
        else menu->shop_items[slot] = false;
        return;
    }
    if (action == MENU_READY) {
        menu->ready[slot] = true;
        return;
    }
    if (menu->ready[slot]) return;
    int *category = &menu->shop_category[slot], *item = &menu->shop_item[slot];
    if (!menu->shop_items[slot]) {
        if (action == MENU_UP && *category > 0) --*category;
        if (action == MENU_DOWN && *category < 5) ++*category;
        if (action == MENU_CONFIRM) {
            menu->shop_items[slot] = true;
            *item = game_shop_equipped(&game->players[slot], *category);
            if (!game_shop_item_available(*category, *item)) *item = 0;
            menu->shop_level[slot] = *category == 3 ? game->players[slot].weapon_level :
                *category == 4 ? game->players[slot].secondary_level : 0;
        }
    } else {
        if (action == MENU_UP || action == MENU_DOWN) {
            int stock[WEAPON_COUNT+1],count=game_shop_stock(game,slot,*category,stock),selected=-1;
            for(int i=0;i<count;++i)if(stock[i]==*item)selected=i;
            selected+=action==MENU_UP?-1:1;
            if (selected>=0 && selected<count) {
                int candidate=stock[selected];
                *item = candidate;
                const Player *p = &game->players[slot];
                menu->shop_level[slot] = candidate == game_shop_equipped(p, *category) ?
                    (*category == 3 ? p->weapon_level : *category == 4 ? p->secondary_level : 0) : 0;
            }
        }
        if (*category >= 3 && *item >= 0) {
            int *level = &menu->shop_level[slot];
            if (action == MENU_LEFT && *level > 0) --*level;
            if (action == MENU_RIGHT && *level + 1 < game_weapons[*item].level_count) ++*level;
        }
        if (action == MENU_CONFIRM) game_shop_buy(game, slot);
    }
}

void game_shop_preview_update(Game *preview, const Game *game) {
    if (preview->tick == 0) {
        game_reset(preview, game->seed);
        preview->waves = false;
        preview->scroll_speed = 0;
        memset(preview->players, 0, sizeof(preview->players));
    }
    for (int slot = 0; slot < GAME_PLAYERS; ++slot) {
        Player desired = game->players[slot];
        Player *p = &preview->players[slot];
        if (game->menu.shop_items[slot] && !game->menu.ready[slot]) {
            int item = game->menu.shop_item[slot], level = game->menu.shop_level[slot];
            switch (game->menu.shop_category[slot]) {
                case 0: desired.chassis = item; break;
                case 1: desired.generator = item; break;
                case 2: desired.shield = item; break;
                case 3: desired.primary = item; desired.weapon_level = level; break;
                case 4: desired.secondary = item; desired.secondary_level = level; break;
                case 5: desired.sidekick = item; break;
            }
        }
        bool changed = p->joined != desired.joined || p->weapon_level != desired.weapon_level ||
            p->secondary_level != desired.secondary_level;
        for (int category = 0; category < 6; ++category)
            changed |= game_shop_equipped(p, category) != game_shop_equipped(&desired, category);
        if (changed) {
            *p = desired;
            p->age = 0; p->powerup_levels = 0;
            p->lives = 1; p->invincible = true;
            p->health = game_chassis[p->chassis].armor;
            p->energy = game_generators[p->generator].capacity;
            p->shield_health = p->shield >= 0 ? game_shields[p->shield].capacity : 0;
            memset(p->weapons, 0, sizeof(p->weapons));
            for (int i = 0; i < GAME_BULLETS; ++i) {
                Bullet *b = &preview->bullets[i];
                if (b->owner == slot && (b->weapon == WEAPON_BEAM || b->weapon == WEAPON_BEAM_FOCUS)) b->active = false;
            }
        }
        p->x = game->players[1].joined ? 480 + (slot ? 90 : -90) : 635;
        int fly = game->menu.shop_fly_frames[slot];
        p->y = 790 - 0.75f * fly * (fly + 1);
        p->vx = 0; p->vy = -1.5f * fly;
        p->input = (PlayerInput){0};
    }
    game_preview_tick(preview);
}
