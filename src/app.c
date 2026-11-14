#include "game.h"
#include "console.h"
#include "raylib.h"
#include <math.h>
#include <stdio.h>
#include <string.h>

#define CANVAS_WIDTH 1920
#define CANVAS_HEIGHT 1080
#define ARENA_X 480

typedef struct {
    Texture2D ship, enemies[4], shot, hostile_shot;
    Texture2D panel_background, panel_frames, armor, armor_icon, energy_icon, shield_icon, portraits[2];
    Music music;
    Sound shoot, explode, hit;
    bool audio;
} Assets;

static const Color background = {48, 48, 48, 255};
static const Color dim = {160, 160, 160, 255};
static const Color accent = {245, 205, 82, 255};
static const Color danger = {255, 130, 104, 255};
static const Color player_colors[2] = {{255, 212, 79, 255}, {94, 209, 240, 255}};

static const char *asset_path(const char *name) {
    static char path[2048];
    snprintf(path, sizeof(path), "%sassets/%s", GetApplicationDirectory(), name);
    return path;
}

static Texture2D texture_load(const char *name) {
    Texture2D texture = LoadTexture(asset_path(name));
    if (IsTextureValid(texture)) SetTextureFilter(texture, TEXTURE_FILTER_BILINEAR);
    return texture;
}

static void assets_unload(Assets *assets) {
    if (IsTextureValid(assets->ship)) UnloadTexture(assets->ship);
    for (int i = 0; i < 4; ++i) if (IsTextureValid(assets->enemies[i])) UnloadTexture(assets->enemies[i]);
    if (IsTextureValid(assets->shot)) UnloadTexture(assets->shot);
    if (IsTextureValid(assets->hostile_shot)) UnloadTexture(assets->hostile_shot);
    Texture2D hud[] = {assets->panel_background, assets->panel_frames, assets->armor, assets->armor_icon,
        assets->energy_icon, assets->shield_icon, assets->portraits[0], assets->portraits[1]};
    for (int i = 0; i < 8; ++i) if (IsTextureValid(hud[i])) UnloadTexture(hud[i]);
    if (assets->audio) {
        if (IsMusicValid(assets->music)) UnloadMusicStream(assets->music);
        if (IsSoundValid(assets->shoot)) UnloadSound(assets->shoot);
        if (IsSoundValid(assets->explode)) UnloadSound(assets->explode);
        if (IsSoundValid(assets->hit)) UnloadSound(assets->hit);
        CloseAudioDevice();
    }
}

static bool assets_load(Assets *assets, Console *console, bool mute, float volume) {
    assets->ship = texture_load("textures/Rookie.png");
    assets->enemies[0] = texture_load("textures/Ball.png");
    assets->enemies[1] = texture_load("textures/Isosceles.png");
    assets->enemies[2] = texture_load("textures/ShootBall.png");
    assets->enemies[3] = texture_load("textures/Boss1.png");
    assets->shot = texture_load("textures/MiniShot.png");
    assets->hostile_shot = texture_load("textures/EnemyShot.png");
    assets->panel_background = texture_load("textures/PanelBackground.png");
    assets->panel_frames = texture_load("textures/PanelFrames.png");
    assets->armor = texture_load("textures/Armor.png");
    assets->armor_icon = texture_load("textures/ArmorIcon.png");
    assets->energy_icon = texture_load("textures/EnergyIcon.png");
    assets->shield_icon = texture_load("textures/ShieldIcon.png");
    assets->portraits[0] = texture_load("textures/KazukiPortrait.png");
    assets->portraits[1] = texture_load("textures/RabbitPortrait.png");
    bool valid = IsTextureValid(assets->ship) && IsTextureValid(assets->shot) && IsTextureValid(assets->hostile_shot);
    for (int i = 0; i < 4; ++i) valid = valid && IsTextureValid(assets->enemies[i]);
    Texture2D hud[] = {assets->panel_background, assets->panel_frames, assets->armor, assets->armor_icon,
        assets->energy_icon, assets->shield_icon, assets->portraits[0], assets->portraits[1]};
    for (int i = 0; i < 8; ++i) valid = valid && IsTextureValid(hud[i]);
    if (!valid) { console_log(console, "ERROR: missing textures beside executable in assets/"); return false; }
    if (mute) return true;
    InitAudioDevice();
    assets->audio = IsAudioDeviceReady();
    if (!assets->audio) { console_log(console, "Audio unavailable; continuing without sound."); return true; }
    SetMasterVolume(volume);
    assets->music = LoadMusicStream(asset_path("audio/The_Voyage.wav"));
    assets->shoot = LoadSound(asset_path("audio/WeaponShootMiniShot.wav"));
    assets->explode = LoadSound(asset_path("audio/EnemyDie1.wav"));
    assets->hit = LoadSound(asset_path("audio/PlayerArmorHit.wav"));
    if (!IsMusicValid(assets->music) || !IsSoundValid(assets->shoot) ||
        !IsSoundValid(assets->explode) || !IsSoundValid(assets->hit)) {
        console_log(console, "ERROR: missing or unsupported audio assets");
        return false;
    }
    assets->music.looping = true;
    SetMusicVolume(assets->music, 0.4f);
    SetSoundVolume(assets->shoot, 0.13f);
    SetSoundVolume(assets->explode, 0.35f);
    SetSoundVolume(assets->hit, 0.45f);
    PlayMusicStream(assets->music);
    return true;
}

static void sprite(Texture2D texture, float x, float y, float width, float rotation, Color color) {
    float height = width * texture.height / texture.width;
    DrawTexturePro(texture, (Rectangle){0, 0, (float)texture.width, (float)texture.height},
        (Rectangle){x, y, width, height}, (Vector2){width / 2, height / 2}, rotation, color);
}

static void text_center(const char *text, int x, int y, int size, Color color) {
    DrawText(text, x - MeasureText(text, size) / 2, y, size, color);
}

static void draw_player_panel(const Game *game, int slot, const Assets *assets) {
    const Player *player = &game->players[slot];
    int x = slot == 0 ? 0 : 1440;
    DrawTexture(assets->panel_background, x, 0, WHITE);
    if (!player->joined) {
        text_center("PRESS START", x + 240, 490, 30, dim);
        text_center("F2 / GAMEPAD 2 START", x + 240, 540, 22, dim);
        return;
    }
    /* Positions and original frames from legacy/Hud/Hud.cs + HudManager.cs. */
    DrawTexture(assets->panel_frames, x, 0, WHITE);
    text_center(slot ? "RABBIT" : "KAZUKI", x + 242, 78, 30, dim);
    text_center(TextFormat("%d", player->score), x + 242, 154, 32, dim);
    sprite(assets->portraits[slot], (float)x + 238, 356, 256, 0, WHITE);
    text_center(TextFormat("SHIPS: %d", player->lives), x + 240, 495, 25, dim);
    DrawText(slot ? "I J K L      MOVE" : "WASD/ARROWS  MOVE", x + 70, 560, 22, dim);
    DrawText(slot ? "ENTER        FIRE" : "SPACE / Z    FIRE", x + 70, 597, 22, dim);
    DrawText(slot ? "R-SHIFT      FOCUS" : "L-SHIFT      FOCUS", x + 70, 634, 22, dim);
    DrawTexture(assets->energy_icon, x + 70, 722, Fade(WHITE, 0.35f));
    DrawTexture(assets->shield_icon, x + 70, 805, Fade(WHITE, 0.35f));
    DrawText("--", x + 260, 744, 23, DARKGRAY);
    DrawText("--", x + 260, 827, 23, DARKGRAY);
    DrawTexture(assets->armor_icon, x + 70, 881, WHITE);
    DrawTexturePro(assets->armor, (Rectangle){0, 0, 256, 64},
        (Rectangle){(float)x + 140, 876, 281.6f * player->health / 20, 73.6f}, (Vector2){0}, 0, WHITE);
    text_center(TextFormat("ARMOR %.0f / 20", player->health), x + 280, 980, 22, dim);
}

static void draw_game(const Game *game, const Assets *assets, bool muted) {
    ClearBackground(background);
    DrawRectangle(ARENA_X, 0, GAME_WIDTH, GAME_HEIGHT, background);
    BeginScissorMode(ARENA_X, 0, GAME_WIDTH, GAME_HEIGHT);
    /* Simple trial backdrop; authored stage scenery is not ported yet. */
    for (int i = 0; i < 60; ++i) {
        int x = ARENA_X + (i * 193 + 17) % GAME_WIDTH;
        int y = (i * 137 + game->tick * (i % 3 + 2)) % (GAME_HEIGHT + 80) - 40;
        DrawLine(x, y, x, y + 12 + i % 30, (Color){60, 66, 73, 255});
    }
    for (int i = 0; i < GAME_ENEMIES; ++i) {
        const Enemy *enemy = &game->enemies[i];
        if (!enemy->active) continue;
        float width = enemy->kind == ENEMY_BOSS ? 360 : 80;
        sprite(assets->enemies[enemy->kind], ARENA_X + enemy->x, enemy->y, width, 0, WHITE);
    }
    for (int i = 0; i < GAME_BULLETS; ++i) {
        const Bullet *bullet = &game->bullets[i];
        if (!bullet->active) continue;
        Color color = bullet->owner < 0 ? WHITE : player_colors[bullet->owner];
        sprite(bullet->owner < 0 ? assets->hostile_shot : assets->shot,
            ARENA_X + bullet->x, bullet->y, bullet->owner < 0 ? 24 : 20, 0, color);
    }
    for (int i = 0; i < GAME_EFFECTS; ++i) {
        const Effect *effect = &game->effects[i];
        if (effect->life <= 0) continue;
        float age = 1 - effect->life / 24.0f;
        float radius = (effect->kind == 2 ? 220 : 50) * age + 5;
        Color color = Fade(effect->kind == 1 ? SKYBLUE : ORANGE, 1 - age);
        DrawCircleLines((int)(ARENA_X + effect->x), (int)effect->y, radius, color);
        for (int p = 0; p < 8; ++p) {
            float angle = p * PI / 4;
            DrawRectangle((int)(ARENA_X + effect->x + cosf(angle) * radius),
                (int)(effect->y + sinf(angle) * radius), 6, 6, color);
        }
    }
    for (int i = 0; i < GAME_PLAYERS; ++i) {
        const Player *player = &game->players[i];
        if (!player->joined || player->lives <= 0) continue;
        Color color = player_colors[i];
        if (player->invulnerable && game->tick % 8 < 4) color = Fade(color, 0.4f);
        DrawTriangle((Vector2){ARENA_X + player->x - 9, player->y + 28},
            (Vector2){ARENA_X + player->x, player->y + 55 + game->tick % 10},
            (Vector2){ARENA_X + player->x + 9, player->y + 28}, Fade(color, 0.7f));
        sprite(assets->ship, ARENA_X + player->x, player->y, 80, -90 + player->vx * 1.3f, color);
        if (player->input.focus) DrawCircleLines((int)(ARENA_X + player->x), (int)player->y, 18, WHITE);
    }
    EndScissorMode();
    DrawText("NATIVE COMBAT TRIAL", ARENA_X + 18, 16, 20, dim);
    DrawText(TextFormat("%02d:%02d", game->tick / 3600, game->tick / 60 % 60), ARENA_X + 850, 16, 20, dim);
    for (int i = 0; i < GAME_ENEMIES; ++i) {
        const Enemy *enemy = &game->enemies[i];
        if (!enemy->active || enemy->kind != ENEMY_BOSS) continue;
        DrawRectangle(ARENA_X + 170, 118, 620, 12, (Color){68, 45, 45, 255});
        DrawRectangle(ARENA_X + 170, 118, (int)(620 * enemy->health / 60), 12, danger);
    }
    DrawRectangle(ARENA_X, 1034, GAME_WIDTH, 46, (Color){25, 29, 36, 235});
    text_center(muted ? "P PAUSE    R RESTART    M UNMUTE    ` CONSOLE" : "P PAUSE    R RESTART    M MUTE    ` CONSOLE",
        CANVAS_WIDTH / 2, 1047, 20, dim);
    draw_player_panel(game, 0, assets);
    draw_player_panel(game, 1, assets);
    if (game->over || game->complete || game->paused) {
        DrawRectangle(ARENA_X, 375, GAME_WIDTH, 260, (Color){15, 18, 24, 240});
        const char *title = game->complete ? "TRIAL COMPLETE" : game->over ? "GAME OVER" : "PAUSED";
        text_center(title, 960, 435, 52, game->over ? danger : accent);
        text_center(game->paused ? "P / START TO RESUME" : "R / GAMEPAD Y TO RESTART", 960, 525, 26, RAYWHITE);
        if (game->complete) text_center("Next: the original stages, weapons, and bosses.", 960, 580, 20, dim);
    }
}

static PlayerInput poll_input(int slot) {
    PlayerInput input = {0};
    if (slot == 0) {
        input.x = (float)((IsKeyDown(KEY_D) || IsKeyDown(KEY_RIGHT)) - (IsKeyDown(KEY_A) || IsKeyDown(KEY_LEFT)));
        input.y = (float)((IsKeyDown(KEY_S) || IsKeyDown(KEY_DOWN)) - (IsKeyDown(KEY_W) || IsKeyDown(KEY_UP)));
        input.fire = IsKeyDown(KEY_SPACE) || IsKeyDown(KEY_Z);
        input.focus = IsKeyDown(KEY_LEFT_SHIFT);
    } else {
        input.x = (float)(IsKeyDown(KEY_L) - IsKeyDown(KEY_J));
        input.y = (float)(IsKeyDown(KEY_K) - IsKeyDown(KEY_I));
        input.fire = IsKeyDown(KEY_ENTER) || IsKeyDown(KEY_RIGHT_CONTROL);
        input.focus = IsKeyDown(KEY_RIGHT_SHIFT);
    }
    if (IsGamepadAvailable(slot)) {
        float x = GetGamepadAxisMovement(slot, GAMEPAD_AXIS_LEFT_X);
        float y = GetGamepadAxisMovement(slot, GAMEPAD_AXIS_LEFT_Y);
        if (fabsf(x) > 0.18f) input.x += x;
        if (fabsf(y) > 0.18f) input.y += y;
        input.x += IsGamepadButtonDown(slot, GAMEPAD_BUTTON_LEFT_FACE_RIGHT) - IsGamepadButtonDown(slot, GAMEPAD_BUTTON_LEFT_FACE_LEFT);
        input.y += IsGamepadButtonDown(slot, GAMEPAD_BUTTON_LEFT_FACE_DOWN) - IsGamepadButtonDown(slot, GAMEPAD_BUTTON_LEFT_FACE_UP);
        input.fire = input.fire || IsGamepadButtonDown(slot, GAMEPAD_BUTTON_RIGHT_FACE_DOWN) ||
            GetGamepadAxisMovement(slot, GAMEPAD_AXIS_RIGHT_TRIGGER) > 0.2f;
        input.focus = input.focus || IsGamepadButtonDown(slot, GAMEPAD_BUTTON_LEFT_TRIGGER_1);
    }
    return input;
}

static void draw_console(const Console *console, const char *line, bool failed, bool full_screen) {
    /* Solcraft's debug_console_draw pattern: full-width compact output + input strip. */
    int visible = full_screen ? CONSOLE_HISTORY : 4;
    int panel_height = full_screen ? CANVAS_HEIGHT : 4 * 30 + 87;
    if (visible > console->count) visible = console->count;
    BeginScissorMode(0, 0, CANVAS_WIDTH, panel_height);
    DrawRectangle(0, 0, CANVAS_WIDTH, panel_height, (Color){5, 8, 12, 224});
    for (int i = 0; i < visible; ++i) DrawText(console->lines[console->count - visible + i], 18, 15 + i * 30, 25, (Color){210, 224, 214, 255});
    DrawRectangle(0, panel_height - 48, CANVAS_WIDTH, 48, (Color){12, 16, 22, 255});
    DrawText(">", 18, panel_height - 42, 28, accent);
    DrawText(line, 51, panel_height - 42, 28, failed ? danger : RAYWHITE);
    EndScissorMode();
}

int app_run(Game *game, Console *console, int frames, const char *capture, bool mute, float volume, bool show_console) {
    Assets assets = {0};
    SetConfigFlags(FLAG_WINDOW_RESIZABLE | FLAG_VSYNC_HINT | FLAG_MSAA_4X_HINT);
    InitWindow(1280, 720, "Galaxy Defenders - native first pass");
    if (!IsWindowReady()) { console_log(console, "ERROR: could not create window"); return 1; }
    SetWindowMinSize(960, 540);
    SetExitKey(KEY_NULL);
    SetTargetFPS(60);
    RenderTexture2D canvas = LoadRenderTexture(CANVAS_WIDTH, CANVAS_HEIGHT);
    if (!IsRenderTextureValid(canvas) || !assets_load(&assets, console, mute, volume)) {
        assets_unload(&assets);
        if (IsRenderTextureValid(canvas)) UnloadRenderTexture(canvas);
        CloseWindow();
        return 1;
    }
    SetTextureFilter(canvas.texture, TEXTURE_FILTER_BILINEAR);
    bool console_open = show_console, console_full = false, command_failed = false, muted = mute;
    char command[CONSOLE_LINE] = {0};
    char last_command[CONSOLE_LINE] = {0};
    int command_length = 0, rendered = 0, result = 0;
    double accumulator = 0;
    console_log(console, "Ready. Type help; console commands and launch scripts share the same game.");
    while (!WindowShouldClose() && !console->quit) {
        bool toggled = IsKeyPressed(KEY_GRAVE);
        if (toggled) {
            if (IsKeyDown(KEY_LEFT_SHIFT) || IsKeyDown(KEY_RIGHT_SHIFT)) {
                console_full = !console_open || !console_full;
                console_open = true;
            } else { console_open = !console_open; console_full = false; }
            while (GetCharPressed()) {} /* Don't insert the opening backtick. */
        }
        if (console_open) {
            if (IsKeyPressed(KEY_ESCAPE)) console_open = false;
            int character;
            while ((character = GetCharPressed()) != 0) {
                if (character >= 32 && character < 127 && character != '`' && command_length < CONSOLE_LINE - 1) {
                    command[command_length++] = (char)character;
                    command[command_length] = '\0';
                }
            }
            if ((IsKeyPressed(KEY_BACKSPACE) || IsKeyPressedRepeat(KEY_BACKSPACE)) && command_length) command[--command_length] = '\0';
            if (IsKeyPressed(KEY_UP)) { strcpy(command, last_command); command_length = (int)strlen(command); }
            if (IsKeyPressed(KEY_DOWN)) { command_length = 0; command[0] = '\0'; }
            if (IsKeyPressed(KEY_ENTER) && command_length) {
                strcpy(last_command, command);
                console_log(console, "> %s", command);
                command_failed = !console_chain(console, game, command);
                command_length = 0;
                command[0] = '\0';
            }
        } else if (!toggled) {
            bool start[2] = {false, false};
            for (int i = 0; i < GAME_PLAYERS; ++i) start[i] = IsGamepadAvailable(i) && IsGamepadButtonPressed(i, GAMEPAD_BUTTON_MIDDLE_RIGHT);
            bool joining = !game->players[1].joined && (IsKeyPressed(KEY_F2) || start[1]);
            if (joining) game_join(game, 1);
            if (IsKeyPressed(KEY_P) || IsKeyPressed(KEY_ESCAPE) || start[0] || (start[1] && !joining)) game->paused = !game->paused;
            bool restart = IsKeyPressed(KEY_R);
            for (int i = 0; i < GAME_PLAYERS; ++i) {
                if ((game->over || game->complete) && IsGamepadAvailable(i) && IsGamepadButtonPressed(i, GAMEPAD_BUTTON_RIGHT_FACE_UP)) restart = true;
            }
            if (restart) {
                bool co_op = game->players[1].joined;
                game_reset(game, 1);
                if (co_op) game_join(game, 1);
                if (assets.audio) { StopMusicStream(assets.music); PlayMusicStream(assets.music); }
            }
            if (IsKeyPressed(KEY_M) && assets.audio) { muted = !muted; SetMasterVolume(muted ? 0 : volume); }
        }
        int old_shots = game->shots, old_kills = game->kills, old_hits = game->hits;
        if (!console_open && !toggled && !game->paused && (IsWindowFocused() || frames)) {
            if (!frames) for (int i = 0; i < GAME_PLAYERS; ++i) game->players[i].input = poll_input(i);
            accumulator += frames ? 1.0 / GAME_HZ : fmin(GetFrameTime(), 0.1);
            while (accumulator >= 1.0 / GAME_HZ) {
                game_tick(game);
                accumulator -= 1.0 / GAME_HZ;
            }
        } else accumulator = 0;
        if (assets.audio) {
            UpdateMusicStream(assets.music);
            if (game->shots > old_shots) PlaySound(assets.shoot);
            if (game->kills > old_kills) PlaySound(assets.explode);
            if (game->hits > old_hits) PlaySound(assets.hit);
        }
        BeginTextureMode(canvas);
        draw_game(game, &assets, muted);
        if (console_open) draw_console(console, command, command_failed, console_full);
        EndTextureMode();
        BeginDrawing();
        ClearBackground(BLACK);
        float scale = fminf(GetScreenWidth() / (float)CANVAS_WIDTH, GetScreenHeight() / (float)CANVAS_HEIGHT);
        Rectangle destination = {(GetScreenWidth() - CANVAS_WIDTH * scale) / 2,
            (GetScreenHeight() - CANVAS_HEIGHT * scale) / 2, CANVAS_WIDTH * scale, CANVAS_HEIGHT * scale};
        DrawTexturePro(canvas.texture, (Rectangle){0, 0, CANVAS_WIDTH, -CANVAS_HEIGHT}, destination, (Vector2){0}, 0, WHITE);
        EndDrawing();
        if (frames && ++rendered >= frames) {
            if (capture) {
                Image image = LoadImageFromTexture(canvas.texture);
                ImageFlipVertical(&image);
                if (!ExportImage(image, capture)) result = 1;
                UnloadImage(image);
            }
            break;
        }
    }
    console_execute(console, game, "state");
    assets_unload(&assets);
    UnloadRenderTexture(canvas);
    CloseWindow();
    return result;
}
