#include "game.h"
#include "console.h"
#include "stage.h"
#include "movement.h"
#include <math.h>
#include <stdio.h>
#include <string.h>

static int failures;
#define CHECK(test) do { if (!(test)) { fprintf(stderr, "%s:%d: %s\n", __FILE__, __LINE__, #test); ++failures; } } while (0)
static Game game, repeat;
static Console console;

static void reset(void) {
    game_reset(&game, 42);
    game.waves = false;
    memset(&console, 0, sizeof(console));
}

static void movement(void) {
    reset();
    game.players[0].input.x = 1;
    game_tick(&game);
    CHECK(fabsf(game.players[0].x - 401.8f) < 0.001f); /* Kazuki +20% speed */
    for (int i = 0; i < 500; ++i) game_tick(&game);
    CHECK(fabsf(game.players[0].x - (480 + 464.0f * 480 / 430)) < 0.001f);
    CHECK(game.players[0].vx == 0);
    game.players[0].input.x = -1;
    game_tick(&game);
    CHECK(game.players[0].x < 480 + 464.0f * 480 / 430);
    int tick = game.tick;
    game.paused = true;
    game_tick(&game);
    CHECK(game.tick == tick);
}

static void collisions(void) {
    reset();
    int enemy = game_spawn_enemy(&game, ENEMY_BALL, 400, 300);
    game_spawn_bullet(&game, 0, 400, 500, 0, -400, 1);
    game_tick(&game);
    CHECK(!game.enemies[enemy].active);
    CHECK(game.kills == 1 && game.players[0].score == 10);
    CHECK(game_bullet_count(&game) == 0);
    /* Friendly fire is disabled, including for the other player. */
    CHECK(game_join(&game, 1));
    game.players[1].invulnerable = 0;
    game_spawn_bullet(&game, 0, 560, 950, 0, -100, 50);
    game_tick(&game);
    CHECK(game.players[1].health == 20);
    game_spawn_bullet(&game, -1, 560, 950, 0, -100, 2);
    game_tick(&game);
    CHECK(game.players[1].health == 18.125f);
    game_damage_player(&game, 1, 5);
    CHECK(game.players[1].health == 13.125f); /* No automatic immunity after a hit. */
}

static void player_impact_damage(void) {
    reset();
    Player *p = &game.players[0];
    p->invulnerable = 0;
    p->shield_health = 5;
    game_spawn_bullet(&game, -1, p->x, p->y, 0, 0, 2);
    game_spawn_bullet(&game, -1, p->x, p->y, 0, 0, 4);
    game_tick(&game);
    CHECK(p->shield_health == 0 && p->health == 19.375f);
    CHECK(game.hits == 2 && game.shield_hits == 1 && game.armor_hits == 1);
    CHECK(p->invulnerable == 0); /* Both bullets hit in the same tick. */
    CHECK(game.effect_cursor == 19); /* 3 shield + 8 armor + two 4-dot shot effects. */
    p->invincible = true;
    game_spawn_bullet(&game, -1, p->x, p->y, 0, 0, 100);
    game_tick(&game);
    CHECK(p->health == 19.375f && game.hits == 2);
    reset();
    p = &game.players[0];
    p->invulnerable = 0;
    p->chassis = 7; /* Demon absorbs 21.25% of ordinary bullet damage. */
    game_spawn_bullet(&game, -1, p->x, p->y - 10, 0, 10, 4);
    game_tick(&game);
    CHECK(fabsf(p->health - 16.85f) < 0.0001f);
    CHECK(fabsf(p->vy - 6.3f) < 0.0001f);
}

static void legacy_circle_sizes(void) {
    reset();
    game.players[0].invulnerable = 0;
    int shot = game_spawn_bullet(&game, -1, 417, 880, 0, 0, 2);
    CHECK(game.bullets[shot].radius == 2);
    game_tick(&game);
    CHECK(game.players[0].health == 20 && game.bullets[shot].active);
    game.bullets[shot].x = 416;
    game_tick(&game);
    CHECK(game.players[0].health == 18.125f && !game.bullets[shot].active);
    /* Ball radius 36 + player radius 16: original squared threshold is 1552. */
    reset();
    game.players[0].invulnerable = 0;
    int ball = game_spawn_enemy(&game, ENEMY_BALL, 445, 880);
    CHECK(game.enemies[ball].radius == 36);
    game_tick(&game);
    CHECK(game.players[0].health == 20);
    game.enemies[ball].x = 439;
    game_tick(&game);
    CHECK(game.players[0].health == 17.890625f); /* Ball sends 2.5 + 0.5, then both resistance factors. */
    CHECK(!game.enemies[ball].active); /* Reciprocal 0.5 damage kills the ball. */
    reset();
    game.players[0].invulnerable = 0;
    shot = game_spawn_bullet(&game, -1, 416, 882, 0, 0, 2);
    game_tick(&game);
    CHECK(game.players[0].health == 20 && game.bullets[shot].active); /* Exact tangent: strict <. */
    reset();
    ball = game_spawn_enemy(&game, ENEMY_BALL, 400, 300);
    game_spawn_bullet(&game, 0, 436.01f, 300, 0, 0, 1);
    game_tick(&game);
    CHECK(game.enemies[ball].active); /* Laser is a 1 x 0.5 box, not a radius-5 circle. */
    game_spawn_bullet(&game, 0, 436, 300, 0, 0, 1);
    game_tick(&game);
    CHECK(!game.enemies[ball].active);
    /* Boss side guns are 120x120 AABBs, not circles of radius 60. */
    reset();
    game.players[0].invulnerable = 0;
    int parent = game_spawn_enemy(&game, ENEMY_BOSS, 800, 200);
    int gun = game_spawn_enemy(&game, ENEMY_BOSS_CHUNK, 470, 950);
    game.enemies[gun].parent = parent;
    game.enemies[gun].cooldown = 100;
    game_tick(&game);
    CHECK(game.players[0].health == 15.78125f); /* Boss part contact is capped at 6. */
}

static void shop_progression(void) {
    reset();
    game.complete = true;
    game.players[0].score = 5000;
    game.players[0].money = 250;
    game_shop_open(&game);
    CHECK(game.menu.page == MENU_SHOP && game.players[0].money == 5250 && game.players[0].score == 0);
    game_shop_open(&game);
    CHECK(game.players[0].money == 5250); /* No duplicate reward on reopen. */
    game_menu_input(&game, MENU_CONFIRM, 0);
    for (int i = 0; i < 3; ++i) game_menu_input(&game, MENU_DOWN, 0);
    CHECK(game.menu.shop_item[0] == 3 && game_shop_cost(&game, 0) == 6000);
    CHECK(!game_shop_buy(&game, 0) && game.players[0].money == 5250 && game.players[0].chassis == 0);
    game.players[0].money = 10000;
    CHECK(game_shop_buy(&game, 0));
    CHECK(game.players[0].chassis == 3 && game.players[0].money == 4000);
    for (int i = 0; i < 3; ++i) game_menu_input(&game, MENU_UP, 0);
    game_menu_input(&game, MENU_BACK, 0);
    CHECK(game.players[0].chassis == 3 && game.players[0].money == 4000); /* Cancel preview. */
    for (int i = 0; i < 3; ++i) game_menu_input(&game, MENU_DOWN, 0);
    game_menu_input(&game, MENU_CONFIRM, 0);
    game_menu_input(&game, MENU_RIGHT, 0);
    CHECK(game_shop_cost(&game, 0) == 5000 && !game_shop_buy(&game, 0));
    game.players[0].money = 10000;
    CHECK(game_shop_buy(&game, 0) && game.players[0].weapon_level == 3 && game.players[0].money == 5000);
    game_menu_input(&game, MENU_LEFT, 0);
    CHECK(game_shop_buy(&game, 0) && game.players[0].weapon_level == 2 && game.players[0].money == 10000);
    game_menu_input(&game, MENU_READY, 0);
    CHECK(game.stage.index == 1 && game.menu.page == MENU_NONE && !game.complete);
    CHECK(game.players[0].chassis == 3 && game.players[0].money == 10000 && game.players[0].weapon_level == 2);
    CHECK(!strcmp(game_music(&game), "Alkali Earth"));
    game_retry(&game);
    CHECK(game.stage.index == 1 && game.players[0].chassis == 3 && game.players[0].money == 10000);
}

static void stage2_runtime(void) {
    game_reset(&game, 42);
    CHECK(game_stage(&game, 1, 0.95f));
    CHECK(game.stage.index == 1 && game.stage.stopped && game.dropped_spawns == 0);
    int boss = -1;
    for (int i = 0; i < GAME_ENEMIES; ++i) if (game.enemies[i].active && game.enemies[i].kind == ENEMY_BOSS2) boss = i;
    CHECK(boss >= 0);
    if (boss < 0) return;
    Enemy *enemy = &game.enemies[boss];
    CHECK(enemy->health == 70 && enemy->box_width == 420 && enemy->box_height == 160);
    float x = enemy->x;
    game.players[0].invincible = true;
    for (int i = 0; i < 300; ++i) {
        game_tick(&game);
        CHECK(fabsf(enemy->x - x) < 8);
        x = enemy->x;
    }
    game_spawn_bullet(&game, 0, enemy->x, enemy->y + 140, 0, -140, 100);
    game_tick(&game);
    CHECK(!enemy->active && !game.complete && game_bullet_count(&game) == 0);
    for (int i = 0; i < 220; ++i) game_tick(&game);
    CHECK(game.complete && game.menu.page == MENU_CLEAR);
    game_menu_input(&game, MENU_CONFIRM, 0);
    CHECK(game.menu.page == MENU_SHOP && game.stage.index == 1);
    game_menu_input(&game, MENU_READY, 0);
    CHECK(game.menu.page == MENU_NONE && game.stage.index == 2);
    reset();
    boss = game_spawn_enemy(&game, ENEMY_BOSS2, 480, 200);
    game_tick(&game);
    enemy = &game.enemies[boss];
    CHECK(game_bullet_count(&game) == 0);
    enemy->attack = 0; enemy->shot_index = 8; enemy->cooldown = 1;
    game_tick(&game);
    CHECK(game_bullet_count(&game) == 1 && enemy->cooldown == 8);
    for (int i = 0; i < 8; ++i) game_tick(&game);
    CHECK(game_bullet_count(&game) == 33 && enemy->cooldown == 68);
    for (int i = 1; i < 33; ++i) {
        CHECK(game.bullets[i].radius == 2 && game.bullets[i].damage == 3);
        CHECK(fabsf(game.bullets[i].vx * game.bullets[i].vx + game.bullets[i].vy * game.bullets[i].vy - 64) < 0.001f);
    }
}

static void death_and_restart(void) {
    reset();
    game_damage_player(&game, 0, 20);
    CHECK(game.players[0].lives == 0);
    CHECK(game.players[0].health == 0 && game.deaths == 1);
    game_tick(&game);
    CHECK(game.over);
    CHECK(!strcmp(game_music(&game), "Try_Harder"));
    for (int i = 0; i < 119; ++i) game_tick(&game);
    CHECK(game.menu.page == MENU_NONE);
    game_tick(&game);
    CHECK(game.menu.page == MENU_OVER);
    CHECK(!game_join(&game, 1));
    game_reset(&game, 42);
    CHECK(!game.over && game.players[0].lives == 1 && game.tick == 0);
    CHECK(game_enemy_count(&game) == 0 && game_bullet_count(&game) == 0);
    game_join(&game, 1);
    game.players[0].lives = 0;
    game_tick(&game);
    CHECK(!game.over);
}

static void contact_rules(void) {
    reset();
    Player *p = &game.players[0];
    p->generator = 9; /* Kinetic: energy from actual incoming collision damage. */
    p->energy = 0;
    p->shield_health = 100;
    int n = game_spawn_enemy(&game, ENEMY_TURRET, p->x - 10, p->y);
    game.enemies[n].cooldown = 100;
    game_tick(&game);
    CHECK(fabsf(p->energy - 5.795f) < 0.0001f);
    CHECK(fabsf(p->vx - 9.84375f) < 0.0001f);
    CHECK(fabsf(p->shield_health - 97.36328125f) < 0.0001f);
    CHECK(game.enemies[n].health == 0.75f);
    reset();
    game_join(&game, 1);
    n = game_spawn_enemy(&game, ENEMY_BALL, 400, 880);
    game_tick(&game);
    CHECK(game.enemies[n].active && game.enemies[n].health == 0.125f);
    reset();
    game.players[0].invincible = true;
    n = game_spawn_enemy(&game, ENEMY_BIG_BALL, 400, 880);
    game_tick(&game);
    CHECK(!game.enemies[n].active && game.players[0].health == 20);
    reset();
    n = game_spawn_enemy(&game, ENEMY_SPIKE, 400, 880);
    game_tick(&game);
    CHECK(!game.enemies[n].active && game.hits == 2);
    CHECK(fabsf(game.players[0].health - 9.8046875f) < 0.0001f);
    reset();
    int parent = game_spawn_enemy(&game, ENEMY_BALL, 800, 100);
    n = game_spawn_enemy(&game, ENEMY_FENCE_BEAM, 400, 880);
    game.enemies[n].parent = parent;
    game.enemies[n].box_width = 200;
    game_tick(&game);
    CHECK(game.players[0].health == 18.59375f);
}

static void pools_and_completion(void) {
    reset();
    for (int i = 0; i < GAME_ENEMIES; ++i) CHECK(game_spawn_enemy(&game, ENEMY_BALL, 100, -50) == i);
    CHECK(game_spawn_enemy(&game, ENEMY_BALL, 100, -50) == -1);
    CHECK(game_enemy_count(&game) == GAME_ENEMIES && game.dropped_spawns == 1);
    for (int i = 0; i < GAME_BULLETS; ++i) CHECK(game_spawn_bullet(&game, 0, 100, -10, 0, -1000, 1) == i);
    CHECK(game_spawn_bullet(&game, 0, 100, -10, 0, -1000, 1) == -1);
    game_tick(&game);
    CHECK(game_bullet_count(&game) == 0);
    CHECK(game_spawn_bullet(&game, 0, 100, 100, 0, -1, 1) == 0);
    reset();
    game_spawn_enemy(&game, ENEMY_BOSS, 480, 200);
    game_spawn_bullet(&game, 0, 480, 400, 0, -200, 60);
    game_tick(&game);
    CHECK(game.complete && game.players[0].score == 500);
}

static void determinism(void) {
    game_reset(&game, 123);
    game_reset(&repeat, 123);
    game_join(&game, 1);
    game_join(&repeat, 1);
    for (int i = 0; i < 4000; ++i) {
        PlayerInput input = {.x=(i / 120) % 2 ? 1 : -1, .fire=true};
        game.players[0].input = repeat.players[0].input = input;
        game_tick(&game);
        game_tick(&repeat);
    }
    CHECK(!memcmp(&game, &repeat, sizeof(game)));
    CHECK(game.dropped_spawns == 0);
}

static void command_failures(void) {
    reset();
    CHECK(!console_execute(&console, &game, "not.a.command"));
    CHECK(!console_execute(&console, &game, "game.step -1"));
    CHECK(!console_execute(&console, &game, "game.step 1.5"));
    CHECK(!console_execute(&console, &game, "player.input 1 nan 0 1 0"));
    CHECK(!console_execute(&console, &game, "player.input 3 0 0 1 0"));
    CHECK(!console_execute(&console, &game, "assert p1.health eq 99"));
    CHECK(!console_execute(&console, &game, "assert nonsense eq 0"));
    CHECK(console_chain(&console, &game, "# comment; not a command"));
    CHECK(console_chain(&console, &game, "game.reset 7 # inline comment; not a command"));
    CHECK(console_chain(&console, &game, "game.reset 7;game.waves 0;player.join 2;assert p2.joined eq 1"));
    console.steps = CONSOLE_STEP_LIMIT;
    CHECK(!console_execute(&console, &game, "game.step 1"));
    CHECK(!console_chain(&console, &game, "game.reset 1;game.step 1")); /* reset cannot bypass budget */
}

static void legacy_effects(void) {
    reset();
    game_effect(&game, EFFECT_ENEMY_DEATH, 400, 300, 0, 0, 0);
    int orange = 0, gray = 0;
    for (int i = 0; i < GAME_EFFECTS; ++i) if (game.effects[i].life > 0) {
        orange += game.effects[i].r == 252 && game.effects[i].g == 124;
        gray += game.effects[i].r == 102 && game.effects[i].g == 102;
    }
    CHECK(orange == 24 && gray == 24);
    reset();
    game.players[0].lives = 1;
    game.players[0].invulnerable = 0;
    game_damage_player(&game, 0, 20);
    CHECK(game.deaths == 1 && game.effect_cursor == 24);
    game_tick(&game);
    CHECK(game.over);
    for (int frame = 0; frame < 90; ++frame) game_tick(&game);
    for (int i = 0; i < GAME_EFFECTS; ++i) CHECK(game.effects[i].life <= 0);
}

static void debug_equipment(void) {
    reset();
    CHECK(console_chain(&console, &game, "player.money 1 25000;player.chassis 1 Demon;assert p1.armor eq 50"));
    CHECK(game.players[0].health == 50 && game.players[0].money == 25000);
    game.players[0].invulnerable = 0;
    CHECK(console_execute(&console, &game, "player.invincible 1 1"));
    game_damage_player(&game, 0, 100);
    CHECK(game.players[0].health == 50 && game.players[0].lives == 1 && game.hits == 0);
    CHECK(console_execute(&console, &game, "player.invincible 1 0"));
    game_damage_player(&game, 0, 25);
    CHECK(game.players[0].health == 25);
    CHECK(console_execute(&console, &game, "player.chassis 1 Eagle"));
    CHECK(game.players[0].health == 7.5f);
    CHECK(console_execute(&console, &game, "player.heal 1"));
    CHECK(game.players[0].health == 15);
    game.players[0].input.x = 1;
    game_tick(&game);
    CHECK(fabsf(game.players[0].x - 402.1f) < 0.001f);
    CHECK(!console_execute(&console, &game, "player.money 1 -1"));
    CHECK(!console_execute(&console, &game, "player.money 1 2147483648"));
    CHECK(!console_execute(&console, &game, "player.invincible 1 2"));
    CHECK(!console_execute(&console, &game, "player.chassis 1 Missing"));
    CHECK(!console_execute(&console, &game, "player.chassis 2 Ace"));
    CHECK(game.players[0].money == 25000 && game.players[0].chassis == 1);
    game_reset(&game, 1);
    CHECK(!game.players[0].invincible && game.players[0].money == 0 && game.players[0].chassis == 0);
}

static void menu_flow(void) {
    reset();
    CHECK(console_chain(&console, &game, "menu.open main;menu.input down;menu.input confirm"));
    CHECK(game.menu.page == MENU_NEW_GAME && game.menu.players == 2);
    CHECK(console_chain(&console, &game, "menu.input up;menu.input confirm"));
    CHECK(game.menu.page == MENU_NEW_GAME); /* no pretend save */
    CHECK(console_chain(&console, &game, "menu.input down;menu.input confirm;menu.input down;menu.input confirm"));
    CHECK(game.menu.page == MENU_PILOT && game.menu.difficulty == 1);
    CHECK(console_execute(&console, &game, "menu.input right"));
    CHECK(game.menu.pilot[0] == 2); /* skip pilot reserved by P2 */
    CHECK(console_execute(&console, &game, "menu.input confirm"));
    CHECK(game.menu.page == MENU_PILOT && game.menu.ready[0]);
    CHECK(console_execute(&console, &game, "menu.input confirm 2"));
    CHECK(game.menu.page == MENU_NONE && !game.paused && game.players[1].joined);
    CHECK(game.difficulty == 1 && game.players[0].pilot == 2 && game.players[0].chassis == 2);
    CHECK(game.players[0].money == 1500 && game.players[1].money == 1500);
    CHECK(game.players[0].health == 30);
    int enemy = game_spawn_enemy(&game, ENEMY_BALL, 100, 100);
    CHECK(game.enemies[enemy].health == 0.625f);
    CHECK(console_chain(&console, &game, "menu.open pause;menu.input down;menu.input down;menu.input confirm"));
    CHECK(game.menu.page == MENU_OPTIONS);
    CHECK(console_execute(&console, &game, "menu.input left"));
    CHECK(game.menu.options[0] == 9);
    for (int i = 0; i < 20; ++i) game_menu_input(&game, MENU_LEFT, 0);
    CHECK(game.menu.options[0] == 0);
    CHECK(console_chain(&console, &game, "menu.input back;menu.input down;menu.input confirm"));
    CHECK(game.menu.page == MENU_RETRY);
    game_tick(&game);
    CHECK(game.tick == 0);
    CHECK(console_chain(&console, &game, "menu.input down;menu.input confirm"));
    CHECK(!game.paused && game.menu.page == MENU_NONE && game_enemy_count(&game) == 0);
    CHECK(game.players[1].joined && game.players[0].chassis == 2 && game.menu.options[0] == 0);
    CHECK(console_chain(&console, &game, "menu.open main;menu.input down;menu.input down;menu.input down;menu.input confirm;menu.input back"));
    CHECK(game.menu.page == MENU_MAIN && !game.menu.quit);
    CHECK(!console_execute(&console, &game, "menu.input confirm 3"));
    CHECK(!console_execute(&console, &game, "menu.input explode"));
}

static void weapon_upgrades(void) {
    reset();
    CHECK(console_execute(&console, &game, "player.upgrade 1 0"));
    game.players[0].input.fire = true;
    game_tick(&game);
    CHECK(game_bullet_count(&game) == 1 && game.shots == 1);
    CHECK(fabsf(game.bullets[0].damage - 0.1f) < 0.0001f && game.bullets[0].vy == -17);
    CHECK(fabsf(game.players[0].energy - 2.95f) < 0.0001f);
    for (int i = 0; i < 9; ++i) game_tick(&game);
    CHECK(game.shots == 1);
    game_tick(&game);
    CHECK(game.shots == 2);
    reset();
    CHECK(console_execute(&console, &game, "player.upgrade 1 7"));
    game.players[0].input.fire = true;
    game_tick(&game);
    CHECK(game_bullet_count(&game) == 8);
    CHECK(game.bullets[0].x == 365 && game.bullets[7].x == 435);
    CHECK(fabsf(game.bullets[0].damage - 0.225f) < 0.0001f);
    game.players[0].input.fire = false;
    game.players[0].input.focus = true;
    game_tick(&game); /* Focus has a separate cooldown and fires without FIRE. */
    CHECK(game_bullet_count(&game) == 11 && game.bullets[8].vy == -19);
    CHECK(game.bullets[8].x == 396 && game.bullets[10].x == 404);
    CHECK(fabsf(game.bullets[8].damage - 0.175f) < 0.0001f);
    CHECK(game.players[0].friction < game_chassis[0].friction);
    game.players[0].energy = 0;
    int shots = game.shots;
    game_tick(&game);
    CHECK(game.shots == shots && game.players[0].energy == 0.05f);
    CHECK(!console_execute(&console, &game, "player.upgrade 1 8"));
    CHECK(!console_execute(&console, &game, "player.upgrade 1 -1"));
    CHECK(!console_execute(&console, &game, "player.upgrade 2 7"));
    CHECK(game.players[0].weapon_level == 7);
}

static void console_editor(void) {
    reset();
    console_insert(&console, "player.inv");
    CHECK(console_complete(&console) == 1);
    CHECK(!strcmp(console.input, "player.invincible "));
    console_insert(&console, "1 ");
    CHECK(console_complete(&console) == 2);
    console_insert(&console, "1");
    CHECK(console_submit(&console, &game) && game.players[0].invincible);
    console_insert(&console, "player.money 1 42");
    CHECK(console_submit(&console, &game));
    console_insert(&console, "unfinished");
    console_history(&console, -1);
    CHECK(!strcmp(console.input, "player.money 1 42"));
    console_history(&console, -1);
    CHECK(!strcmp(console.input, "player.invincible 1 1"));
    console_history(&console, 1); console_history(&console, 1);
    CHECK(!strcmp(console.input, "unfinished"));
    console.input[0] = '\0'; console.cursor = 0;
    console_insert(&console, "game.waves 0; player.chassis 1 De");
    CHECK(console_complete(&console) == 1 && strstr(console.input, "Demon "));
    CHECK(console_submit(&console, &game) && game.players[0].chassis == 7);
    console_insert(&console, "assert p1.we eq 2");
    console.cursor = 12;
    CHECK(console_complete(&console) == 1);
    CHECK(!strcmp(console.input, "assert p1.weapon_level eq 2"));
    CHECK(console_submit(&console, &game));
    console_insert(&console, "help player.up");
    CHECK(console_complete(&console) == 1 && !strcmp(console.input, "help player.upgrade "));
    CHECK(console_submit(&console, &game));
    CHECK(console_execute(&console, &game, "help fields"));
    CHECK(console_execute(&console, &game, "help player."));
    CHECK(!console_execute(&console, &game, "help nonexistent"));
    console_insert(&console, "state"); console.cursor = 2;
    console_insert(&console, "X"); CHECK(!strcmp(console.input, "stXate"));
    console_erase(&console, true); CHECK(!strcmp(console.input, "state"));
    console_erase(&console, false); CHECK(!strcmp(console.input, "stte"));
    char large[CONSOLE_LINE * 2]; memset(large, 'x', sizeof(large) - 1); large[sizeof(large) - 1] = 0;
    console_insert(&console, large);
    CHECK(strlen(console.input) == CONSOLE_LINE - 1 && console.cursor < CONSOLE_LINE);
    int history = console.command_count;
    CHECK(console_execute(&console, &game, "clear"));
    CHECK(console.count == 0 && console.command_count == history);
}

static void stage_clear_exit(void) {
    reset();
    CHECK(game_join(&game, 1));
    game_spawn_enemy(&game, ENEMY_BOSS, 480, 200);
    game_spawn_bullet(&game, 0, 480, 400, 0, -200, 60);
    game_tick(&game);
    CHECK(game.complete && game.menu.page == MENU_CLEAR);
    CHECK(game_bullet_count(&game) == 0 && game_enemy_count(&game) == 0);
    CHECK(!strcmp(game_menu_label(&game, 0), "Continue"));
    int tick = game.tick;
    for (int i = 0; i < 90; ++i) game_tick(&game);
    CHECK(game.tick == tick);
    for (int i = 0; i < GAME_EFFECTS; ++i) CHECK(game.effects[i].life <= 0);
    CHECK(console_execute(&console, &game, "menu.input confirm 2"));
    CHECK(game.menu.page == MENU_SHOP && !game.menu.quit);
    CHECK(game.players[0].score == 0 && game.players[0].money == 500 && game.players[1].joined);
    CHECK(console_execute(&console, &game, "menu.input ready 1"));
    CHECK(game.menu.page == MENU_SHOP && game.menu.ready[0] && !game.menu.ready[1]);
    CHECK(console_execute(&console, &game, "menu.input ready 2"));
    CHECK(game.stage.index == 1 && game.players[0].money == 500);
    CHECK(game.menu.page == MENU_NONE && !game.complete && !game.over && game.tick == 0);
}

static void starting_primary_patterns(void) {
    /* Pilot.cs starting items, paired with WeaponDefinitions.cs shot arrays. */
    for (int pilot = 0; pilot <= 2; pilot += 2) for (int difficulty = 0; difficulty < 3; ++difficulty) {
        reset();
        game_menu_open(&game, MENU_MAIN);
        game_menu_input(&game, MENU_CONFIRM, 0); /* Solo */
        game_menu_input(&game, MENU_CONFIRM, 0); /* New Game */
        for (int i = 0; i < difficulty; ++i) game_menu_input(&game, MENU_DOWN, 0);
        game_menu_input(&game, MENU_CONFIRM, 0);
        for (int i = 0; i < pilot; ++i) game_menu_input(&game, MENU_RIGHT, 0);
        game_menu_input(&game, MENU_CONFIRM, 0);
        game.waves = false;
        game.players[0].input.fire = true;
        game_tick(&game);
        int shots = pilot == 0 && difficulty < 2 ? 2 : 4;
        int primary_shots = 0;
        for (int b = 0; b < GAME_BULLETS; ++b) if (game.bullets[b].active && game.bullets[b].weapon == WEAPON_FRONT_LASER) ++primary_shots;
        CHECK(primary_shots == shots && game_bullet_count(&game) > shots);
        CHECK(fabsf(game.bullets[0].damage - (pilot == 2 ? 0.115f : 0.1f)) < 0.0001f);
        CHECK(game.players[0].energy > 0); /* Starting primary/support loadout can fire together. */
    }
}

static void authored_stage_data(void) {
    int total = 0;
    for (int i = 0; i < STAGE_COUNT; ++i) {
        const StageDefinition *stage = &stage_definitions[i];
        int finish = 0, stop = 0;
        CHECK(stage->scroll_speed > 0 && stage->length > 0 && stage->event_count > 0);
        for (int j = 0; j < stage->event_count; ++j) {
            const StageEvent *event = &stage->events[j];
            finish += event->kind == STAGE_FINISH;
            stop += event->kind == STAGE_CAMERA_STOP;
            CHECK(isfinite(event->x) && isfinite(event->y));
            if (event->kind == STAGE_ENEMY) {
                CHECK(event->name && event->mover && event->speed >= 0);
                CHECK(movement_find(event->mover) >= 0);
            }
        }
        CHECK(finish == 1 && stop == 1);
        total += stage->event_count;
    }
    CHECK(total == 3448);
    const StageDefinition *first = &stage_definitions[0];
    CHECK(!strcmp(first->name, "Stage1") && !strcmp(first->music, "The_Voyage") && first->length == 21320);
    int boss = 0, squares = 0;
    for (int j = 0; j < first->event_count; ++j) {
        const StageEvent *event = &first->events[j];
        if (event->kind != STAGE_ENEMY) continue;
        squares += !strcmp(event->name, "MiniSquare");
        if (!strcmp(event->name, "Boss1")) {
            ++boss;
            CHECK(event->spawn_x == 8 && event->spawn_y == -18152 && !strcmp(event->mover, "None"));
        }
    }
    CHECK(boss == 1 && squares == 32);
}

static void authored_movement(void) {
    float vx = 99, vy = 99;
    movement_velocity(MOVE_DOWN, 0, 6, 1, &vx, &vy);
    CHECK(vx == 0 && vy == 3.5f); /* Preset anti-camera is fixed, not scroll speed. */
    movement_velocity(MOVE_IGNORE_CAMERA, 1, 6, 1, &vx, &vy);
    CHECK(vx == 0 && vy == 3.5f); /* IgnoreCamera preserves physical velocity. */
    movement_velocity(MOVE_DOWN_WAIT_UP, 0, 8, 2, &vx, &vy);
    CHECK(vy == 5.5f);
    movement_velocity(MOVE_DOWN_WAIT_UP, 120, 8, 2, &vx, &vy);
    CHECK(vy == 5.5f); /* Boundary is inclusive in MoverSequence. */
    movement_velocity(MOVE_DOWN_WAIT_UP, 121, 8, 2, &vx, &vy);
    CHECK(fabsf(vy - 5.34f) < 0.0001f);
    vx = 0; vy = -2.5f;
    movement_velocity(MOVE_DOWN_WAIT_UP, 181, 8, 2, &vx, &vy);
    CHECK(fabsf(vy + 2.66f) < 0.0001f);
    movement_velocity(MOVE_DOWN_RIGHT, 0, 6, 2, &vx, &vy);
    movement_velocity(MOVE_DOWN_RIGHT, 61, 6, 2, &vx, &vy);
    CHECK(fabsf(vx - 0.06f) < 0.0001f && fabsf(vy - 3.44f) < 0.0001f);
    movement_velocity(MOVE_SIN, 60, 3, 2, &vx, &vy);
    CHECK(fabsf(vx - cosf(2) * 3) < 0.0001f && vy == 3);
    CHECK(movement_find("unknown") == -1);
}

static void authored_stage_runtime(void) {
    game_reset(&game, 42);
    game.players[0].invincible = true;
    game.players[0].money = 1234;
    CHECK(game_stage(&game, 0, 0.5f));
    CHECK(game.tick == 4344 && !game.stage.stopped && !strcmp(game_music(&game), "The_Voyage"));
    CHECK(game.players[0].money == 1234 && game.players[0].invincible);
    game_reset(&repeat, 42);
    repeat.players[0].invincible = true;
    repeat.seeking = true; /* Seek suppresses ship interactions; invincibility now damages enemies on contact. */
    for (int i = 0; i < game.tick; ++i) game_tick(&repeat);
    CHECK(!memcmp(game.enemies, repeat.enemies, sizeof(game.enemies)));
    CHECK(game.stage.camera_y == repeat.stage.camera_y);
    CHECK(!memcmp(game.stage.activated, repeat.stage.activated, sizeof(game.stage.activated)));
    CHECK(game_stage(&game, 0, 0.9f));
    CHECK(game.stage.stopped && !strcmp(game_music(&game), "Boss 9"));
    CHECK(game.tick > game.stage.music_tick && game.dropped_spawns == 0);
    int boss = -1;
    for (int i = 0; i < GAME_ENEMIES; ++i) if (game.enemies[i].active && game.enemies[i].kind == ENEMY_BOSS) boss = i;
    CHECK(boss >= 0);
    if (boss >= 0) {
        Enemy *b = &game.enemies[boss];
        CHECK(b->health == 25 && b->y > 150 && b->y < 260);
        for (int i = 0; i < 2; ++i) {
            CHECK(b->children[i] >= 0);
            Enemy *child = &game.enemies[b->children[i]];
            CHECK(child->active && child->health == 8 && child->parent == boss);
            CHECK(fabsf(child->x - b->x - (i ? 150 : -150)) < 0.001f);
        }
        /* Destroy only one side gun; body and other gun must survive. */
        Enemy *left = &game.enemies[b->children[0]];
        game_spawn_bullet(&game, 0, left->x - 35, left->y + 100, 0, -100, 9);
        game_tick(&game);
        CHECK(!left->active && b->active && b->health == 25);
        CHECK(game.enemies[b->children[1]].active);
        game_spawn_bullet(&game, 0, b->x, b->y + 200, 0, -200, 100);
        game_tick(&game);
        CHECK(!game.complete && game_enemy_count(&game) == 0 && game_bullet_count(&game) == 0);
        for (int i = 0; i < 219; ++i) game_tick(&game);
        CHECK(!game.complete);
        game_tick(&game);
        CHECK(game.complete && game.menu.page == MENU_CLEAR);
    }
    int tick = game.tick;
    CHECK(!game_stage(&game, STAGE_COUNT, 0) && !game_stage(&game, 0, NAN) && !game_stage(&game, 0, 1.1f));
    CHECK(game.tick == tick);
    CHECK(console_execute(&console, &game, "game.stage 1 0"));
    CHECK(game.tick == 0 && game.stage.music_event == -1 && !game.complete && !game.paused);
    CHECK(!console_execute(&console, &game, "game.stage 1 -1"));
    CHECK(!console_execute(&console, &game, "game.stage 20"));
}

static void boss_patterns(void) {
    reset();
    int index = game_spawn_enemy(&game, ENEMY_BOSS, 480, 200);
    game_tick(&game);
    Enemy *boss = &game.enemies[index];
    CHECK(game_enemy_count(&game) == 3 && game_bullet_count(&game) == 8);
    /* Force a continuation of a paired downward burst. */
    memset(game.bullets, 0, sizeof(game.bullets));
    boss->attack = 0; boss->shot_index = 1; boss->cooldown = 1;
    game_tick(&game);
    CHECK(game_bullet_count(&game) == 2 && boss->cooldown == 4);
    CHECK(fabsf(game.bullets[0].vx) < 0.0001f && game.bullets[0].vy == 8 && game.bullets[0].damage == 3);
    CHECK(fabsf(game.bullets[0].x - game.bullets[1].x - 76) < 0.0001f);
    /* Unarmed boss must choose the eight-pair aimed sweep. */
    game.enemies[boss->children[0]].active = false;
    game.enemies[boss->children[1]].active = false;
    memset(game.bullets, 0, sizeof(game.bullets));
    boss->shot_index = 0; boss->cooldown = 1;
    game_tick(&game);
    CHECK(boss->attack == 2 && game_bullet_count(&game) == 2);
    for (int i = 0; i < 14; ++i) game_tick(&game);
    CHECK(game_bullet_count(&game) == 16 && boss->shot_index == 0 && boss->cooldown == 62);
    float last_x = boss->x;
    for (int i = 0; i < 200; ++i) {
        game_tick(&game);
        CHECK(fabsf(boss->x - last_x) < 10);
        last_x = boss->x;
    }
}

static void authored_buildings(void) {
    reset();
    int index = -1;
    for (int i = 0; i < stage_definitions[0].event_count; ++i) {
        if (stage_definitions[0].events[i].kind == STAGE_BUILDING) { index = i; break; }
    }
    CHECK(index >= 0);
    if (index < 0) return;
    const StageEvent *event = &stage_definitions[0].events[index];
    game.stage.camera_y = event->y + 200;
    game.stage.activated[index] = true;
    game.stage.building_health[index] = 1;
    float x = event->x + GAME_WIDTH * 0.5f;
    game_spawn_bullet(&game, 0, x, 440, 0, -100, 1);
    game_tick(&game);
    CHECK(game.stage.building_health[index] == 0 && game.building_kills == 0);
    game_spawn_bullet(&game, 0, x, 440, 0, -100, 0.1f);
    game_tick(&game);
    CHECK(game.stage.building_health[index] < 0 && game.building_kills == 1);
    int triangles = 0;
    for (int i = 0; i < GAME_EFFECTS; ++i) if (game.effects[i].life > 0 && game.effects[i].kind == 1) ++triangles;
    CHECK(triangles == 32); /* Two original sixteen-particle building bursts. */
    game_spawn_bullet(&game, 0, x, 440, 0, -100, 1);
    game_tick(&game);
    CHECK(game.building_kills == 1 && game_bullet_count(&game) == 1);
}

static void complete_equipment(void) {
    for (int kind = 0; kind < WEAPON_COUNT; ++kind) {
        for (int level = 0; level < game_weapons[kind].level_count; ++level) {
            reset();
            Player *p = &game.players[0];
            p->generator = 11; p->energy = 13;
            int category = equipment_weapon_category(kind);
            if (category == 5) p->sidekick = kind;
            else if (category == 4) { p->secondary = kind; p->secondary_level = level; }
            else { p->primary = kind; p->weapon_level = level; }
            p->input.fire = category != 5;
            p->input.sidekick_left = p->input.sidekick_right = category == 5;
            for (int tick = 0; tick < 60; ++tick) game_tick(&game);
            p->input.sidekick_left = p->input.sidekick_right = false;
            game_tick(&game);
            bool fired = false;
            for (int i = 0; i < GAME_BULLETS; ++i) {
                const Bullet *b = &game.bullets[i];
                fired |= b->weapon == kind && b->base_damage > 0;
                CHECK(isfinite(b->x) && isfinite(b->y) && isfinite(b->damage));
            }
            CHECK(fired && p->energy >= 0 && game.dropped_spawns == 0);
        }
    }
    reset();
    game.players[0].money = 1000000;
    game_shop_open(&game);
    for (int category = 0; category < 6; ++category) {
        game.menu.shop_category[0] = category;
        game.menu.shop_items[0] = true;
        for (int item = -1; item < WEAPON_COUNT; ++item) {
            if (!game_shop_item_available(category, item)) continue;
            game.menu.shop_item[0] = item;
            game.menu.shop_level[0] = category >= 3 && item >= 0 ? game_weapons[item].level_count - 1 : 0;
            CHECK(game_shop_buy(&game, 0));
            CHECK(game_shop_equipped(&game.players[0], category) == item);
        }
    }
    Player equipment = game.players[0];
    game_retry(&game);
    CHECK(game.players[0].primary == equipment.primary && game.players[0].secondary == equipment.secondary);
    CHECK(game.players[0].sidekick == equipment.sidekick && game.players[0].generator == equipment.generator);
    CHECK(game.players[0].shield == equipment.shield);
    reset();
    Player *p = &game.players[0];
    p->shield = 0; p->shield_health = 6; p->invulnerable = 0;
    game_damage_player(&game, 0, 8);
    CHECK(p->shield_health == 0 && p->health == 18);
    float energy = p->energy;
    game_tick(&game);
    CHECK(p->shield_health > 0 && p->energy < energy);
}

static void extended_enemies(void) {
    for (int kind = ENEMY_AIRSHIP; kind < ENEMY_BOSS3; ++kind) {
        reset();
        game.players[0].invincible = true;
        int index = game_spawn_enemy(&game, (EnemyKind)kind, 480, 220);
        CHECK(index >= 0 && (kind == ENEMY_CUTTER_BALL || game.enemies[index].health > 0));
        for (int tick = 0; tick < 180; ++tick) game_tick(&game);
        for (int i = 0; i < GAME_ENEMIES; ++i) CHECK(isfinite(game.enemies[i].x) && isfinite(game.enemies[i].y));
        CHECK(game.dropped_spawns == 0);
    }
    reset();
    int index = game_spawn_enemy(&game, ENEMY_AIRSHIP, 480, 200);
    for (int tick = 0; tick < 15; ++tick) game_tick(&game);
    CHECK(game_bullet_count(&game) == 2 && game.bullets[0].hostile_kind == HOSTILE_LASER);
    CHECK(game.bullets[0].damage == 3 && fabsf(game.bullets[1].x - game.bullets[0].x - 97) < 0.001f);
    reset();
    index = game_spawn_enemy(&game, ENEMY_SPLITTER, 480, 200);
    enemy_die(&game, &game.enemies[index], 0);
    CHECK(game_enemy_count(&game) == 2 && !game.enemies[index].active);
    reset();
    index = game_spawn_enemy(&game, ENEMY_SWIRL, 480, 200);
    game_tick(&game);
    CHECK(game_enemy_count(&game) == 4);
    enemy_die(&game, &game.enemies[index], 0);
    game_tick(&game);
    CHECK(game_enemy_count(&game) == 3);
    reset();
    index = game_spawn_enemy(&game, ENEMY_BLACK_HOLE, 480, 200);
    game_spawn_bullet(&game, 0, 480, 200, 0, 0, 100);
    game_tick(&game);
    CHECK(game_bullet_count(&game) == 0 && game.enemies[index].health == 100000);
    reset();
    index = game_spawn_enemy(&game, ENEMY_MISSILE_POD, 480, 200);
    game.enemies[index].cooldown = 1;
    game_tick(&game);
    CHECK(game.bullets[0].hostile_kind == HOSTILE_MISSILE);
    float speed = game.bullets[0].speed;
    game_tick(&game);
    CHECK(game.bullets[0].speed > speed);
}

static void pellet_turret_splitter_parity(void) {
    reset();
    Enemy source = {.x = 480, .y = 300};
    int pellet = enemy_shot(&game, &source, HOSTILE_PELLET, 0, 0, 0, 0, 2);
    CHECK(game.bullets[pellet].radius == 14 && game.bullets[pellet].health == 3);
    int laser = game_spawn_bullet(&game, 0, 465, 300, 0, 0, 10);
    game_tick(&game);
    CHECK(!game.bullets[laser].active && game.bullets[pellet].health == 2); /* Laser box reaches x=466. */
    CHECK(fabsf(fabsf(game.bullets[pellet].rotation) - 0.45f) < 0.0001f);
    laser = game_spawn_bullet(&game, 0, 464.99f, 300, 0, 0, 10);
    game_tick(&game);
    CHECK(game.bullets[laser].active && game.bullets[pellet].health == 2);
    game.bullets[laser].active = false;
    for (int i = 0; i < 2; ++i) {
        game_spawn_bullet(&game, 0, 480, 300, 0, 0, 0.01f);
        game_tick(&game);
    }
    CHECK(!game.bullets[pellet].active); /* Three hits, independent of weapon damage. */
    reset();
    pellet = enemy_shot(&game, &source, HOSTILE_PELLET, 0, 0, 0, 0, 2);
    int beam = game_spawn_bullet(&game, 0, 400, 800, 0, 0, 1);
    game.bullets[beam].weapon = WEAPON_BEAM;
    game.bullets[beam].offset_x = 80;
    game.bullets[beam].width = 8;
    game.bullets[beam].group = 2;
    game.players[0].weapons[2][0].toggled = true;
    game_tick(&game);
    CHECK(game.bullets[pellet].health == 2 && game.bullets[beam].active);
    reset();
    int turret = game_spawn_enemy(&game, ENEMY_ROTATE_TURRET, 480, 300);
    Enemy *e = &game.enemies[turret];
    e->cooldown = 1;
    game_tick(&game);
    CHECK(game_bullet_count(&game) == 2 && e->rotation == 0);
    for (int i = 0; i < 27; ++i) game_tick(&game);
    CHECK(game_bullet_count(&game) == 6 && e->shot_index == 0);
    CHECK(fabsf(e->rotation - 0.03f) < 0.0001f);
    for (int i = 0; i < 49; ++i) game_tick(&game);
    CHECK(e->shot_index == 1 && fabsf(e->rotation - 0.78539816f) < 0.0001f);
    int diagonal = 0;
    for (int i = 0; i < GAME_BULLETS; ++i) {
        Bullet *b = &game.bullets[i];
        if (b->active && b->age == 1 && fabsf(b->vx + 5.656854f) < 0.001f && fabsf(b->vy - 5.656854f) < 0.001f) ++diagonal;
    }
    CHECK(diagonal == 1);
    reset();
    int parent = game_spawn_enemy(&game, ENEMY_SPLITTER, 480, 300);
    game.enemies[parent].vx = 2; game.enemies[parent].vy = -3;
    enemy_die(&game, &game.enemies[parent], 0);
    int fragments = 0;
    for (int i = 0; i < GAME_ENEMIES; ++i) {
        Enemy *f = &game.enemies[i];
        if (!f->active || f->kind != ENEMY_SPLITTER_FRAGMENT) continue;
        int side = f->aux_index ? 1 : -1;
        CHECK(f->x == 480 + side * 11 && f->y == 300);
        CHECK(fabsf(f->spin + side * 0.1f) < 0.0001f);
        CHECK(fabsf(hypotf(f->vx - 2 - side, f->vy + 3) - 0.5f) < 0.0001f);
        CHECK(f->radius == 30 && f->coins == 3);
        float y = f->y, vy = f->vy;
        game.scroll_speed = 2;
        enemy_update_extended(&game, f);
        CHECK(fabsf(f->y - y - vy - 2) < 0.0001f);
        ++fragments;
    }
    CHECK(fragments == 2);
}

static void regular_enemy_parity(void) {
    /* Geometry from each legacy Initialize/UpdateCollision, including invisible attachments. */
    static const struct { EnemyKind kind; float radius, width, height; } shapes[] = {
        {ENEMY_BALL,36,0,0}, {ENEMY_TRIANGLE,28,0,0}, {ENEMY_TURRET,28,0,0},
        {ENEMY_MINI_SQUARE,25,0,0}, {ENEMY_BIG_BALL,64,0,0}, {ENEMY_DOWN_TURRET,28,0,0},
        {ENEMY_SHOOT_BALL,36,0,0}, {ENEMY_AIRSHIP,32,72,104}, {ENEMY_BEAMER,32,0,0},
        {ENEMY_BLACK_HOLE,300,0,0}, {ENEMY_CANNON,34,0,0}, {ENEMY_CUTTER,32,72,72},
        {ENEMY_CUTTER_BALL,30,0,0}, {ENEMY_DOUBLE_TURRET,34,0,0}, {ENEMY_FENCE,28,0,0},
        {ENEMY_GLOB,32,0,0}, {ENEMY_MISSILE_POD,32,0,0}, {ENEMY_MISSILE_STORM,58,0,0},
        {ENEMY_MISSILE_TRIPLE,34,0,0}, {ENEMY_PYRAMID,56,0,0}, {ENEMY_RAID_TURRET,28,0,0},
        {ENEMY_ROTATE_TURRET,32,56,56}, {ENEMY_SPIKE,34,0,0}, {ENEMY_SPLITTER,36,0,0},
        {ENEMY_SPLITTER_FRAGMENT,30,0,0}, {ENEMY_SWIRL,32,0,0}, {ENEMY_SWIRL_BALL,16,0,0},
        {ENEMY_TELEPORTER,34,0,0}, {ENEMY_TRIPLE_SHOT,34,0,0}, {ENEMY_BONUS_SHIP,32,0,0}
    };
    reset();
    for (unsigned i = 0; i < sizeof(shapes) / sizeof(shapes[0]); ++i) {
        int n = game_spawn_enemy(&game, shapes[i].kind, 480, 300);
        Enemy *e = &game.enemies[n];
        CHECK(e->radius == shapes[i].radius && e->box_width == shapes[i].width && e->box_height == shapes[i].height);
    }
    reset();
    game.tick = 400; /* Cutter's orbit starts at its own spawn, not the global frame. */
    int n = game_spawn_enemy(&game, ENEMY_CUTTER, 600, 300);
    game_tick(&game);
    Enemy *e = &game.enemies[n];
    CHECK(game.enemies[e->children[0]].x == 726 && game.enemies[e->children[1]].x == 474);
    game_tick(&game);
    CHECK(fabsf(game.enemies[e->children[0]].rotation - 0.09f) < 0.0001f);
    CHECK(fabsf(game.enemies[e->children[1]].rotation + 0.09f) < 0.0001f);
    float health = e->health;
    Enemy *ball = &game.enemies[e->children[0]];
    game_spawn_bullet(&game, 0, ball->x, ball->y, 0, 0, 100);
    game_tick(&game);
    CHECK(e->health == health && ball->active); /* Cutter endpoints absorb fire. */
    reset();
    n = game_spawn_enemy(&game, ENEMY_PYRAMID, 480, 300);
    e = &game.enemies[n];
    CHECK(fabsf(e->aim - 1.5707963f) < 0.0001f); /* Turret initially faces down. */
    enemy_die(&game, e, 0);
    int parts = 0;
    for (int i = 0; i < GAME_ENEMIES; ++i) {
        Enemy *part = &game.enemies[i];
        if (!part->active) continue;
        CHECK(part->kind == ENEMY_TRIANGLE && part->health == 1.125f);
        CHECK((part->x == 480 && part->y == 268) || (part->x == 432 && part->y == 332) || (part->x == 480 && part->y == 332));
        float vx = part->x == 432 ? -0.5f : 0, vy = part->y == 268 ? -0.2f : 0.2f;
        CHECK(hypotf(part->vx - vx, part->vy - vy) <= 0.50001f);
        CHECK(part->spin == (part->x == 480 && part->y == 332 ? -0.025f : 0.025f));
        ++parts;
    }
    CHECK(parts == 3);
    reset();
    int left = game_spawn_enemy(&game, ENEMY_FENCE, 300, 300);
    int right = game_spawn_enemy(&game, ENEMY_FENCE, 600, 300);
    game_tick(&game);
    int beam = game.enemies[left].children[1];
    CHECK(beam >= 0 && game.enemies[beam].box_width == 300 && game.enemies[beam].box_height == 8);
    game_spawn_bullet(&game, 0, 450, 310, 0, -20, 100);
    game_tick(&game);
    CHECK(game_bullet_count(&game) == 1); /* Beam is a ship hazard, not a shot blocker. */
    enemy_die(&game, &game.enemies[right], 0);
    CHECK(!game.enemies[left].active && !game.enemies[right].active && !game.enemies[beam].active);
    CHECK(game.kills == 0 && game.stage.corpses[0].active && game.stage.corpses[1].active);
    reset();
    n = game_spawn_enemy(&game, ENEMY_TURRET, 480, 300);
    game.enemies[n].movement = MOVE_DOWN;
    enemy_die(&game, &game.enemies[n], 0);
    CHECK(!game.stage.corpses[0].active); /* Moving turrets leave no stationary corpse. */
    CHECK(enemy_draw_layer(ENEMY_TELEPORTER) < enemy_draw_layer(ENEMY_TURRET));
    CHECK(enemy_draw_layer(ENEMY_TURRET) < enemy_draw_layer(ENEMY_BALL));
}

static void remaining_bosses(void) {
    for (int kind = ENEMY_BOSS3; kind <= ENEMY_BOSS12; ++kind) {
        reset();
        game.players[0].invincible = true;
        int index = game_spawn_enemy(&game, (EnemyKind)kind, 480, 220);
        Enemy *boss = &game.enemies[index];
        for (int phase = 0; phase < 3; ++phase) {
            boss->health = boss->health_max * (phase == 0 ? 1 : phase == 1 ? 0.59f : 0.3f);
            bool fired = false;
            for (int tick = 0; tick < 720; ++tick) {
                game_tick(&game);
                fired |= game_bullet_count(&game) > 0;
            }
            CHECK(boss->active && fired);
            for (int i = 0; i < GAME_ENEMIES; ++i)
                CHECK(isfinite(game.enemies[i].x) && isfinite(game.enemies[i].y));
        }
        CHECK(game.dropped_spawns == 0);
        CHECK(boss_damage(&game, boss, boss->health_max, 0));
        CHECK(game_enemy_count(&game) == 0 && game_bullet_count(&game) == 0);
    }
    reset();
    int index = game_spawn_enemy(&game, ENEMY_BOSS4, 480, 220);
    game_tick(&game);
    Enemy *boss = &game.enemies[index];
    float health = boss->health;
    CHECK(boss_damage(&game, boss, 1, 0) && boss->health == health);
    CHECK(boss_damage(&game, &game.enemies[boss->children[0]], 1, 0));
    CHECK(boss->health == health - 1);
    reset();
    index = game_spawn_enemy(&game, ENEMY_BOSS12, 480, 220);
    game_tick(&game);
    CHECK(game_enemy_count(&game) == 9);
    boss = &game.enemies[index];
    boss->shot_index = 1;
    boss->attack = 0;
    boss->cooldown = 1;
    game_tick(&game);
    CHECK(game_bullet_count(&game) == 16);
    CHECK(game.bullets[0].hostile_kind == HOSTILE_MINI && game.bullets[0].damage == 1.25f);
}

static void boss_visual_collision_parity(void) {
    static const struct { EnemyKind kind; float radius, width, height, x, y; int layer; } shapes[] = {
        {ENEMY_BOSS,150,0,0,0,0,-1}, {ENEMY_BOSS2,210,420,160,0,0,-1},
        {ENEMY_BOSS3,60,0,0,0,0,-1}, {ENEMY_BOSS4,0,260,215,0,-12.5f,-1},
        {ENEMY_BOSS5,0,170,170,0,0,-1}, {ENEMY_BOSS6,110,0,0,0,0,-2},
        {ENEMY_BOSS7,0,409,103,-0.5f,-12.5f,-2}, {ENEMY_BOSS8,0,360,170,0,5,-1},
        {ENEMY_BOSS9,75,0,0,0,0,0}, {ENEMY_BOSS10,60,0,0,0,0,0},
        {ENEMY_BOSS11,0,280,144,0,0,0}, {ENEMY_BOSS12,0,400,144,0,-8,0},
        {ENEMY_BOSS_CHUNK,60,120,120,0,0,0}, {ENEMY_BOSS3_CHUNK,0,120,120,0,0,0},
        {ENEMY_BOSS4_CHUNK,0,70,70,0,0,-2}, {ENEMY_BOSS5_CHUNK,36,0,0,0,0,-2},
        {ENEMY_BOSS6_SPHERE,38,0,0,0,0,0}, {ENEMY_BOSS9_BALL,32,0,0,0,0,-1},
        {ENEMY_BOSS10_BALL,31,0,0,0,0,ENEMY_LAYER_ENTITY},
        {ENEMY_BOSS10_BLOCK,0,110,89,-1,1.5f,-2},
        {ENEMY_BOSS11_HAND,51,0,0,0,0,-1}, {ENEMY_BOSS11_ARM,0,100,32,0,0,-2}
    };
    reset();
    for (unsigned i = 0; i < sizeof(shapes) / sizeof(shapes[0]); ++i) {
        int n = game_spawn_enemy(&game, shapes[i].kind, 480, 300);
        Enemy *e = &game.enemies[n];
        CHECK(e->radius == shapes[i].radius && e->box_width == shapes[i].width && e->box_height == shapes[i].height);
        CHECK(e->collision_x == shapes[i].x && e->collision_y == shapes[i].y);
        CHECK(enemy_draw_layer(e->kind) == shapes[i].layer);
    }
    reset();
    int n = game_spawn_enemy(&game, ENEMY_BOSS4, 480, 300);
    game_tick(&game);
    Enemy *boss = &game.enemies[n];
    static const float cx[] = {-1,175,-179}, cy[] = {175,3,3};
    for (int i = 0; i < 3; ++i) {
        Enemy *arm = &game.enemies[boss->children[i]];
        CHECK(arm->collision_x == cx[i] && arm->collision_y == cy[i]);
        CHECK(enemy_draw_layer(arm->kind) < enemy_draw_layer(boss->kind));
    }
    reset();
    n = game_spawn_enemy(&game, ENEMY_BOSS5, 480, 300);
    boss = &game.enemies[n]; boss->age = 100; boss->cooldown = 1000;
    int shot = game_spawn_bullet(&game, 0, 584, 404, 0, 0, 1);
    game.bullets[shot].radius = 20; game.bullets[shot].weapon = WEAPON_PLASMA;
    game_tick(&game);
    CHECK(boss->health == boss->health_max && game.bullets[shot].active); /* Outside the rounded corner. */
    game.bullets[shot].x = 577; game.bullets[shot].y = 397;
    game_tick(&game);
    CHECK(boss->health == boss->health_max - 1 && !game.bullets[shot].active);
    reset();
    n = game_spawn_enemy(&game, ENEMY_BOSS6, 480, 300);
    for (int tick = 0; tick < 89; ++tick) game_tick(&game);
    boss = &game.enemies[n];
    CHECK(boss->rotation == 0 && boss->center_spin == 0);
    game_tick(&game);
    CHECK(fabsf(boss->center_spin + 0.0001f) < 0.00001f);
    CHECK(enemy_draw_layer(boss->kind) < enemy_draw_layer(ENEMY_BOSS6_SPHERE));
    reset();
    n = game_spawn_enemy(&game, ENEMY_BOSS9, 480, 300);
    game_tick(&game);
    boss = &game.enemies[n];
    for (int i = 1; i <= 2; ++i) {
        Enemy *ball = &game.enemies[boss->children[i]];
        CHECK(fabsf(hypotf(ball->x - boss->x, ball->y - boss->y) - 120) < 0.001f);
        ball->active = false;
    }
    for (int i = 0; i < 59; ++i) game_tick(&game);
    CHECK(!game.enemies[boss->children[1]].active && !game.enemies[boss->children[2]].active);
    game_tick(&game);
    CHECK(game.enemies[boss->children[1]].active && game.enemies[boss->children[2]].active);
    reset();
    n = game_spawn_enemy(&game, ENEMY_BOSS10_BLOCK, 480, 300);
    Enemy *block = &game.enemies[n];
    int parent = game_spawn_enemy(&game, ENEMY_BOSS10, 480, 100);
    block->parent = parent; game.enemies[parent].age = 100;
    for (int i = 0; i < 30; ++i) boss_update_all(&game, block);
    CHECK(block->intangible && fabsf(block->orbit_radius - 0.5f) < 0.0001f);
    for (int i = 0; i < 30; ++i) boss_update_all(&game, block);
    CHECK(!block->intangible && block->orbit_radius == 1);
    block->timer = -3600; block->intangible = true;
    for (int i = 0; i < 61; ++i) boss_update_all(&game, block);
    CHECK(block->intangible && block->orbit_radius == 0);
    for (int i = 61; i < 3600; ++i) boss_update_all(&game, block);
    CHECK(!block->intangible && block->orbit_radius == 1);
    reset();
    n = game_spawn_enemy(&game, ENEMY_BOSS12, 480, 300);
    game_tick(&game);
    boss = &game.enemies[n];
    for (int i = 0; i < 8; ++i) {
        Enemy *part = &game.enemies[boss->children[i]];
        CHECK(part->intangible);
        CHECK(i < 6 ? enemy_draw_layer(part->kind) < enemy_draw_layer(boss->kind) : enemy_draw_layer(part->kind) > enemy_draw_layer(boss->kind));
    }
}

static void campaign_stages(void) {
    for (int stage = 0; stage < STAGE_COUNT; ++stage) {
        reset();
        game.players[0].invincible = true;
        CHECK(game_stage(&game, stage, 0));
        bool boss_seen = false;
        int max_enemies = 0, max_bullets = 0;
        for (int tick = 0; tick < 15000 && !game.complete; ++tick) {
            game_tick(&game);
            if (game_enemy_count(&game) > max_enemies) max_enemies = game_enemy_count(&game);
            if (game_bullet_count(&game) > max_bullets) max_bullets = game_bullet_count(&game);
            for (int i = 0; i < GAME_ENEMIES; ++i) {
                Enemy *enemy = &game.enemies[i];
                if (!enemy->active || !enemy_is_boss(enemy->kind)) continue;
                boss_seen = true;
                /* Let the authored encounter run before testing its clear path. */
                if (enemy->age >= 600) enemy_die(&game, enemy, 0);
            }
        }
        if (!game.complete || (stage < 12 && !boss_seen) || game.dropped_spawns)
            fprintf(stderr, "stage %d: complete=%d boss=%d dropped=%d peak_enemies=%d peak_bullets=%d camera=%f\n",
                stage + 1, game.complete, boss_seen, game.dropped_spawns, max_enemies, max_bullets, game.stage.camera_y);
        CHECK(game.complete && (stage >= 12 || boss_seen));
        CHECK(game.dropped_spawns == 0);
        if (stage < 11) {
            game_menu_input(&game, MENU_CONFIRM, 0);
            game_menu_input(&game, MENU_READY, 0);
            CHECK(game.stage.index == stage + 1 && game.menu.page == MENU_NONE);
        }
    }
}

static void secret_stage_roundtrip(void) {
    reset();
    CHECK(game_stage(&game, 1, 0.3f));
    game.stage.secret_x = 920;
    game.stage.secret_y = 500;
    game.players[0].health = 7;
    game.players[0].score = 100;
    repeat = game;
    game_secret_enter(&game, 12);
    CHECK(game.stage.index == 12 && game.secret_return.active);
    CHECK(game.players[0].health == 7 && game.players[0].score == 100);
    game.players[0].score += 250;
    game.players[0].health = 6;
    game_secret_return(&game);
    CHECK(game.stage.index == 1 && !game.secret_return.active && game.stage.ignore_secrets);
    CHECK(game.tick == repeat.tick && game.stage.camera_y == repeat.stage.camera_y);
    CHECK(!memcmp(game.enemies, repeat.enemies, sizeof(game.enemies)));
    CHECK(!memcmp(game.bullets, repeat.bullets, sizeof(game.bullets)));
    CHECK(game.players[0].health == 6 && game.players[0].score == 350);
    CHECK(game.players[0].vx < 0 && game.menu.page == MENU_NONE);
}

static void authored_secret_entries(void) {
    for (int stage = 0; stage < 12; ++stage) {
        const StageDefinition *definition = &stage_definitions[stage];
        for (int event = 0; event < definition->event_count; ++event) {
            const StageEvent *entry = &definition->events[event];
            if (entry->kind != STAGE_SECRET) continue;
            reset();
            game.players[0].invincible = true;
            CHECK(game_stage(&game, stage, (-entry->y - 240) / (definition->length + 400)));
            game.players[0].x = 480;
            game.players[0].y = 540;
            game.players[0].input.x = entry->x > 0 ? 1 : -1;
            for (int tick = 0; tick < 500 && !game.secret_return.active; ++tick) game_tick(&game);
            if (!game.secret_return.active) fprintf(stderr, "Unreachable secret: %s %s x=%.2f camera=%.2f\n",
                definition->name, entry->name, game.players[0].x, game.stage.camera_x);
            CHECK(game.secret_return.active && !strcmp(stage_definitions[game.stage.index].name, entry->name));
            game.players[0].input.x = 0;
            for (int tick = 0; tick < 3000 && game.secret_return.active; ++tick) game_tick(&game);
            CHECK(!game.secret_return.active && game.stage.index == stage && !game.complete);
        }
    }
}

static void collectible_rewards(void) {
    reset();
    Player *p = &game.players[0];
    p->pilot = 1;
    game_drop_pickups(&game, p->x, p->y, 21, true, true);
    CHECK(game.pickups[0].kind == 1 && game.pickups[1].kind == 1);
    CHECK(game.pickups[2].kind == 0 && game.pickups[3].kind == 2);
    int level = p->weapon_level;
    game_tick(&game);
    CHECK(p->score == 518 && p->weapon_level == level + 1 && game.pickups_collected == 4);
    reset();
    game_drop_pickups(&game, 400, 650, 1, false, false);
    for (int tick = 0; tick < 120; ++tick) game_tick(&game);
    CHECK(game.players[0].score == 15 && game.pickups_collected == 1);
    CHECK(game_health_score(&game, 2.5f, 15) == 30);
    game.difficulty = 2;
    CHECK(game_health_score(&game, 2.5f, 15) == 40);
}

static void saved_continue(void) {
    reset();
    CHECK(game_stage(&game, 4, 0));
    game.players[0].money = 12345;
    game.players[0].secondary = WEAPON_MISSILE;
    game.players[0].secondary_level = 2;
    game.complete = true;
    game_shop_open(&game);
    CHECK(game.saves[0].valid && game.saves[0].shop && game.saves[0].stage == 5);
    const char *path = "artifacts/native-port/test-save.txt";
    CHECK(game_save_write(&game.saves[0], path));
    SavedGame save = {0};
    CHECK(game_save_read(&save, path) && save.player[0].money == 12345);
    save.player[0].money = 10000;
    CHECK(game_save_write(&save, path));
    reset();
    CHECK(game_save_read(&game.saves[0], path));
    CHECK(game_continue(&game, 1));
    CHECK(game.stage.index == 5 && game.menu.page == MENU_SHOP && game.menu.next_stage == 5);
    CHECK(game.players[0].money == 10000 && game.players[0].secondary_level == 2);
    game_menu_input(&game, MENU_READY, 0);
    CHECK(game.stage.index == 5 && game.menu.page == MENU_NONE);
    game_shop_open(&game); /* Quit mid-stage must not skip it. */
    CHECK(game.menu.next_stage == 5);
    game.cleared_game = true;
    game_checkpoint(&game, true);
    CHECK(game_continue(&game, 1));
    game_menu_input(&game, MENU_READY, 0);
    CHECK(game.menu.page == MENU_STAGE_SELECT && game_menu_count(&game) == 13);
    for (int i = 0; i < 8; ++i) game_menu_input(&game, MENU_DOWN, 0);
    game_menu_input(&game, MENU_CONFIRM, 0);
    CHECK(game.stage.index == 8 && game.menu.page == MENU_NONE && game.cleared_game);
    FILE *file = fopen(path, "wb");
    CHECK(file != NULL);
    if (file) { fputs("GALAXY_SAVE 1 99 0 1 0 0\n", file); fclose(file); }
    SavedGame before = game.saves[0];
    CHECK(!game_save_read(&game.saves[0], path));
    CHECK(!memcmp(&before, &game.saves[0], sizeof(before)));
    remove(path);
}

int main(int argc, char **argv) {
    if (argc == 2 && !strcmp(argv[1], "--core")) {
        contact_rules(); death_and_restart(); player_impact_damage(); legacy_circle_sizes(); determinism();
        printf("Core gameplay checks: %s (%d failures)\n", failures ? "FAIL" : "PASS", failures);
        return failures ? 1 : 0;
    }
    contact_rules();
    authored_secret_entries();
    saved_continue();
    collectible_rewards();
    secret_stage_roundtrip();
    boss_visual_collision_parity();
    campaign_stages();
    pellet_turret_splitter_parity();
    regular_enemy_parity();
    remaining_bosses();
    extended_enemies();
    complete_equipment();
    legacy_circle_sizes();
    shop_progression();
    stage2_runtime();
    movement(); collisions(); player_impact_damage(); death_and_restart(); pools_and_completion(); determinism(); command_failures(); legacy_effects();
    debug_equipment();
    menu_flow();
    weapon_upgrades();
    console_editor();
    stage_clear_exit();
    starting_primary_patterns();
    authored_stage_data();
    authored_movement();
    authored_stage_runtime();
    boss_patterns();
    authored_buildings();
    printf("Game/console checks: %s (%d failures)\n", failures ? "FAIL" : "PASS", failures);
    return failures ? 1 : 0;
}
