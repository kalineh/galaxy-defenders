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
    CHECK(fabsf(game.players[0].x - 401.5f) < 0.001f);
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
    CHECK(console_chain(&console, &game, "game.reset 7;game.waves 0;player.join 2;assert p2.joined eq 1"));
    console.steps = CONSOLE_STEP_LIMIT;
    CHECK(!console_execute(&console, &game, "game.step 1"));
    CHECK(!console_chain(&console, &game, "game.reset 1;game.step 1")); /* reset cannot bypass budget */
}

int main(void) {
    movement(); collisions(); death_and_restart(); pools_and_completion(); determinism(); command_failures();
    printf("Game/console checks: %s (%d failures)\n", failures ? "FAIL" : "PASS", failures);
    return failures ? 1 : 0;
}
