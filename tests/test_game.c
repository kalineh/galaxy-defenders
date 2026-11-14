#include "game.h"
#include "console.h"
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
    CHECK(game.players[0].x == GAME_WIDTH - 30);
    CHECK(game.players[0].vx == 0);
    game.players[0].input.x = -1;
    game_tick(&game);
    CHECK(game.players[0].x < GAME_WIDTH - 30);
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
    CHECK(game.kills == 1 && game.players[0].score == 100);
    CHECK(game_bullet_count(&game) == 0);
    /* Friendly fire is disabled, including for the other player. */
    CHECK(game_join(&game, 1));
    game.players[1].invulnerable = 0;
    game_spawn_bullet(&game, 0, 560, 950, 0, -100, 50);
    game_tick(&game);
    CHECK(game.players[1].health == 20);
    game_spawn_bullet(&game, -1, 560, 950, 0, -100, 2);
    game_tick(&game);
    CHECK(game.players[1].health == 18);
    game_damage_player(&game, 1, 5);
    CHECK(game.players[1].health == 18); /* invulnerability */
}

static void death_and_restart(void) {
    reset();
    for (int i = 0; i < 3; ++i) {
        game.players[0].invulnerable = 0;
        game_damage_player(&game, 0, 20);
    }
    CHECK(game.players[0].lives == 0);
    game_tick(&game);
    CHECK(game.over);
    CHECK(!game_join(&game, 1));
    game_reset(&game, 42);
    CHECK(!game.over && game.players[0].lives == 3 && game.tick == 0);
    CHECK(game_enemy_count(&game) == 0 && game_bullet_count(&game) == 0);
    game_join(&game, 1);
    game.players[0].lives = 0;
    game_tick(&game);
    CHECK(!game.over);
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
    CHECK(game.complete && game.players[0].score == 5000);
}

static void determinism(void) {
    game_reset(&game, 123);
    game_reset(&repeat, 123);
    game_join(&game, 1);
    game_join(&repeat, 1);
    for (int i = 0; i < 4000; ++i) {
        PlayerInput input = {(i / 120) % 2 ? 1 : -1, 0, true, false};
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
    CHECK(game.deaths == 1 && game.effect_cursor == 32);
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
    CHECK(game.players[0].health == 50 && game.players[0].lives == 3 && game.hits == 0);
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

int main(void) {
    movement(); collisions(); death_and_restart(); pools_and_completion(); determinism(); command_failures(); legacy_effects();
    debug_equipment();
    menu_flow();
    weapon_upgrades();
    printf("Game/console checks: %s (%d failures)\n", failures ? "FAIL" : "PASS", failures);
    return failures ? 1 : 0;
}
