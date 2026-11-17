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

int game_shop_owned_marks(const Player *p, int category, int item) {
    if (item < 0) return 1;
    int marks = category < 3 ? !!(p->inventory.gear_mask[category] & (1 << item)) : p->inventory.weapon_marks[item];
    if (item == game_shop_equipped(p, category)) {
        int equipped = category == 3 ? p->weapon_level + 1 : category == 4 ? p->secondary_level + 1 : 1;
        if (equipped > marks) marks = equipped;
    }
    return marks;
}

int game_shop_price(const Player *p, int category, int item, int level) {
    int owned = game_shop_owned_marks(p, category, item);
    if (level < owned) return 0;
    return item_price(category, item, level) - (owned ? item_price(category, item, owned - 1) : 0);
}

static void remember_item(Inventory *inventory, int category, int item, int marks) {
    if (item < 0) return;
    if (category < 3) inventory->gear_mask[category] |= 1 << item;
    else if (marks > inventory->weapon_marks[item]) inventory->weapon_marks[item] = (unsigned char)marks;
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
    int count = 0;
    if(category>=4)items[count++]=-1;
    for(int i=0;i<total;++i) {
        int id=category<3?i:weapons[category-3][i];
        bool available=game->cleared_game?(category>=3 || i>=(category==1?6:3)):chosen[i];
        if(category == 1 || available || game_shop_owned_marks(&game->players[slot], category, id))items[count++]=id;
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
    return game_shop_price(p, category, item, game->menu.shop_level[slot]);
}

bool game_shop_buy(Game *game, int slot) {
    if (slot < 0 || slot >= GAME_PLAYERS || game->menu.page != MENU_SHOP || !game->players[slot].joined ||
        game->menu.ready[slot]) return false;
    Player *p = &game->players[slot];
    int category = game->menu.shop_category[slot], item = game->menu.shop_item[slot], level = game->menu.shop_level[slot];
    if (!game_shop_item_available(category, item) || !in_stock(game,slot,category,item)) return false;
    if (category >= 3 && item >= 0 && (level < 0 || level >= game_weapons[item].level_count)) return false;
    int cost = game_shop_cost(game, slot);
    if (cost > p->money) return false;
    int previous = game_shop_equipped(p, category);
    int previous_marks = game_shop_owned_marks(p, category, previous);
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
    remember_item(&p->inventory, category, previous, previous_marks);
    remember_item(&p->inventory, category, item, level + 1);
    p->money -= cost;
    game_checkpoint(game, true);
    return true;
}

static void shop_select(Game *game, int slot, int item) {
    GameMenu *menu = &game->menu;
    const Player *p = &game->players[slot];
    int category = menu->shop_category[slot];
    int marks = game_shop_owned_marks(p, category, item);
    int level = marks ? marks - 1 : 0;
    if (item == game_shop_equipped(p, category))
        level = category == 3 ? p->weapon_level : category == 4 ? p->secondary_level : 0;
    menu->shop_item[slot] = item;
    menu->shop_level[slot] = level;
}

static void reveal_row(int *first, int row) {
    if (row < *first) *first = row;
    else if (row >= *first + SHOP_VISIBLE_ROWS) *first = row - SHOP_VISIBLE_ROWS + 1;
}

static void shop_select_category(Game *game, int slot, int category) {
    int stock[WEAPON_COUNT + 1];
    game->menu.shop_category[slot] = category;
    game->menu.shop_tiers[slot] = false;
    int count = game_shop_stock(game, slot, category, stock);
    int owned = game_shop_equipped(&game->players[slot], category);
    int row = 0;
    for (int i = 0; i < count; ++i) if (stock[i] == owned) row = i;
    game->menu.shop_first_item[slot] = 0;
    reveal_row(&game->menu.shop_first_item[slot],row);
    shop_select(game, slot, count ? stock[row] : owned);
}

void game_shop_open(Game *game) {
    bool entering = game->menu.page != MENU_SHOP;
    if (entering) {
        game->menu.next_stage = (game->complete || game->menu.page == MENU_CLEAR) && game->stage.index < 11 ?
            game->stage.index + 1 : game->stage.index;
        game_record_stage(game);
    }
    for (int i = 0; i < GAME_PLAYERS; ++i) {
        Player *p = &game->players[i];
        game->menu.shop_catalog[i] = false;
        game->menu.shop_tiers[i] = false;
        game->menu.shop_category[i] = 0;
        game->menu.shop_purchase[i] = false;
        game->menu.ready[i] = false;
        game->menu.shop_fly_frames[i] = 0;
        if (!entering || !p->initialized) continue;
        p->money += p->score;
        p->score = 0;
        p->lives = 1;
        p->health = game_chassis[p->chassis].armor;
    }
    game_menu_open(game, MENU_SHOP);
    if (entering) game_checkpoint(game, true);
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
    if (action == MENU_READY) {
        menu->ready[slot] = !menu->ready[slot];
        menu->shop_catalog[slot] = false;
        menu->shop_tiers[slot] = false;
        menu->shop_purchase[slot] = false;
        menu->shop_fly_frames[slot] = 0;
        return;
    }
    if (action == MENU_BACK) {
        if (menu->shop_purchase[slot]) { menu->shop_purchase[slot] = false; return; }
        if (menu->ready[slot]) {
            menu->ready[slot] = false;
            menu->shop_fly_frames[slot] = 0;
        } else if (menu->shop_tiers[slot]) {
            shop_select(game, slot, menu->shop_item[slot]);
            menu->shop_tiers[slot] = false;
        } else menu->shop_catalog[slot] = false;
        return;
    }
    if (menu->ready[slot]) return;
    int category = menu->shop_category[slot];
    if (!menu->shop_catalog[slot]) {
        if (action == MENU_UP && category > 0) --menu->shop_category[slot];
        if (action == MENU_DOWN && category < 5) ++menu->shop_category[slot];
        if (action == MENU_CONFIRM) {
            shop_select_category(game, slot, category);
            menu->shop_catalog[slot] = true;
        }
        return;
    }
    int owned = game_shop_equipped(&game->players[slot], category);
    int item = menu->shop_item[slot];
    if (menu->shop_purchase[slot]) {
        if (action == MENU_CONFIRM && game_shop_buy(game, slot)) {
            menu->shop_purchase[slot] = false;
            shop_select(game, slot, item);
        }
        return;
    }
    if (action == MENU_CONFIRM) {
        const Player *p = &game->players[slot];
        int level = category == 3 ? p->weapon_level : category == 4 ? p->secondary_level : 0;
        if (category >= 3 && item >= 0 && game_weapons[item].level_count > 1 && !menu->shop_tiers[slot]) {
            menu->shop_tiers[slot] = true;
            menu->shop_first_tier[slot] = 0;
            reveal_row(&menu->shop_first_tier[slot],menu->shop_level[slot]);
        } else if (item != owned || menu->shop_level[slot] != level) {
            if (game_shop_owned_marks(p, category, item) > menu->shop_level[slot]) game_shop_buy(game, slot);
            else if (game_shop_cost(game, slot) <= p->money) menu->shop_purchase[slot] = true;
        }
    } else if (action == MENU_UP || action == MENU_DOWN) {
        if (menu->shop_tiers[slot]) {
            int next = menu->shop_level[slot] + (action == MENU_UP ? -1 : 1);
            if (next >= 0 && next < game_weapons[item].level_count) {
                menu->shop_level[slot] = next;
                reveal_row(&menu->shop_first_tier[slot],next);
            }
            return;
        }
        int stock[WEAPON_COUNT + 1];
        int count = game_shop_stock(game, slot, category, stock);
        for (int i = 0; i < count; ++i) if (stock[i] == item) {
            int next = i + (action == MENU_UP ? -1 : 1);
            if (next >= 0 && next < count) {
                shop_select(game, slot, stock[next]);
                reveal_row(&menu->shop_first_item[slot],next);
            }
            break;
        }
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
        if (desired.joined && game->menu.shop_catalog[slot] && !game->menu.ready[slot]) {
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
        p->x = game->players[0].joined && game->players[1].joined ? 480 + (slot ? 90 : -90) : 480;
        int fly = game->menu.shop_fly_frames[slot];
        p->y = 790 - 0.75f * fly * (fly + 1);
        p->vx = 0; p->vy = -1.5f * fly;
        p->input = (PlayerInput){0};
    }
    game_preview_tick(preview);
}
