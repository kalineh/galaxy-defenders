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
    Texture2D ships[GAME_CHASSIS], enemies[4], shot, hostile_shot;
    Texture2D panel_background, panel_frames, armor, armor_icon, energy_icon, shield_icon, portraits[3];
    Texture2D particle_dot, particle_triangle;
    Texture2D menu[6], title, cloud, pilot_arrow;
    Music music, title_music, pause_music;
    int music_mode;
    Sound shoot, explode, hit, shot_hit, die;
    Sound menu_sounds[5];
    bool audio;
} Assets;

static const Color background = {48, 48, 48, 255};
static const Color dim = {160, 160, 160, 255};
static const Color accent = {245, 205, 82, 255};
static const Color danger = {255, 130, 104, 255};
static const Color player_colors[2] = {{247, 208, 74, 255}, {195, 150, 255, 255}};

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
    for (int i = 0; i < GAME_CHASSIS; ++i) if (IsTextureValid(assets->ships[i])) UnloadTexture(assets->ships[i]);
    for (int i = 0; i < 4; ++i) if (IsTextureValid(assets->enemies[i])) UnloadTexture(assets->enemies[i]);
    if (IsTextureValid(assets->shot)) UnloadTexture(assets->shot);
    if (IsTextureValid(assets->hostile_shot)) UnloadTexture(assets->hostile_shot);
    if (IsTextureValid(assets->particle_dot)) UnloadTexture(assets->particle_dot);
    if (IsTextureValid(assets->particle_triangle)) UnloadTexture(assets->particle_triangle);
    Texture2D hud[] = {assets->panel_background, assets->panel_frames, assets->armor, assets->armor_icon,
        assets->energy_icon, assets->shield_icon, assets->portraits[0], assets->portraits[1]};
    for (int i = 0; i < 8; ++i) if (IsTextureValid(hud[i])) UnloadTexture(hud[i]);
    if (IsTextureValid(assets->portraits[2])) UnloadTexture(assets->portraits[2]);
    for (int i = 0; i < 6; ++i) if (IsTextureValid(assets->menu[i])) UnloadTexture(assets->menu[i]);
    if (IsTextureValid(assets->title)) UnloadTexture(assets->title);
    if (IsTextureValid(assets->cloud)) UnloadTexture(assets->cloud);
    if (IsTextureValid(assets->pilot_arrow)) UnloadTexture(assets->pilot_arrow);
    if (assets->audio) {
        if (IsMusicValid(assets->music)) UnloadMusicStream(assets->music);
        if (IsMusicValid(assets->title_music)) UnloadMusicStream(assets->title_music);
        if (IsMusicValid(assets->pause_music)) UnloadMusicStream(assets->pause_music);
        for (int i = 0; i < 5; ++i) if (IsSoundValid(assets->menu_sounds[i])) UnloadSound(assets->menu_sounds[i]);
        if (IsSoundValid(assets->shoot)) UnloadSound(assets->shoot);
        if (IsSoundValid(assets->explode)) UnloadSound(assets->explode);
        if (IsSoundValid(assets->hit)) UnloadSound(assets->hit);
        if (IsSoundValid(assets->shot_hit)) UnloadSound(assets->shot_hit);
        if (IsSoundValid(assets->die)) UnloadSound(assets->die);
        CloseAudioDevice();
    }
}

static bool assets_load(Assets *assets, Console *console, bool mute, float volume) {
    for (int i = 0; i < GAME_CHASSIS; ++i) {
        char path[128];
        snprintf(path, sizeof(path), "textures/%s.png", game_chassis[i].name);
        assets->ships[i] = texture_load(path);
    }
    assets->enemies[0] = texture_load("textures/Ball.png");
    assets->enemies[1] = texture_load("textures/Isosceles.png");
    assets->enemies[2] = texture_load("textures/ShootBall.png");
    assets->enemies[3] = texture_load("textures/Boss1.png");
    assets->shot = texture_load("textures/MiniShot.png");
    assets->hostile_shot = texture_load("textures/EnemyShot.png");
    assets->particle_dot = texture_load("textures/DotParticle.png");
    assets->particle_triangle = texture_load("textures/TriangleParticle.png");
    assets->panel_background = texture_load("textures/PanelBackground.png");
    assets->panel_frames = texture_load("textures/PanelFrames.png");
    assets->armor = texture_load("textures/Armor.png");
    assets->armor_icon = texture_load("textures/ArmorIcon.png");
    assets->energy_icon = texture_load("textures/EnergyIcon.png");
    assets->shield_icon = texture_load("textures/ShieldIcon.png");
    assets->portraits[0] = texture_load("textures/KazukiPortrait.png");
    assets->portraits[1] = texture_load("textures/RabbitPortrait.png");
    assets->portraits[2] = texture_load("textures/GunthorPortrait.png");
    assets->title = texture_load("textures/Title.png");
    assets->cloud = texture_load("textures/Cloud.png");
    assets->pilot_arrow = texture_load("textures/PilotSelectArrow.png");
    const char *menu_textures[] = {"MenuItem", "MenuItemSmall", "MenuItemSelected", "MenuItemSelectedSmall", "MenuItemInvalid", "MenuItemInvalidSelected"};
    for (int i = 0; i < 6; ++i) {
        char path[128];
        snprintf(path, sizeof(path), "textures/%s.png", menu_textures[i]);
        assets->menu[i] = texture_load(path);
    }
    bool valid = IsTextureValid(assets->shot) && IsTextureValid(assets->hostile_shot);
    for (int i = 0; i < GAME_CHASSIS; ++i) valid = valid && IsTextureValid(assets->ships[i]);
    valid = valid && IsTextureValid(assets->particle_dot) && IsTextureValid(assets->particle_triangle);
    for (int i = 0; i < 4; ++i) valid = valid && IsTextureValid(assets->enemies[i]);
    Texture2D hud[] = {assets->panel_background, assets->panel_frames, assets->armor, assets->armor_icon,
        assets->energy_icon, assets->shield_icon, assets->portraits[0], assets->portraits[1]};
    for (int i = 0; i < 8; ++i) valid = valid && IsTextureValid(hud[i]);
    for (int i = 0; i < 6; ++i) valid = valid && IsTextureValid(assets->menu[i]);
    valid = valid && IsTextureValid(assets->title) && IsTextureValid(assets->cloud) &&
        IsTextureValid(assets->pilot_arrow) && IsTextureValid(assets->portraits[2]);
    if (!valid) { console_log(console, "ERROR: missing textures beside executable in assets/"); return false; }
    if (mute) return true;
    InitAudioDevice();
    assets->audio = IsAudioDeviceReady();
    if (!assets->audio) { console_log(console, "Audio unavailable; continuing without sound."); return true; }
    SetMasterVolume(volume);
    assets->music = LoadMusicStream(asset_path("audio/The_Voyage.wav"));
    assets->title_music = LoadMusicStream(asset_path("audio/Konami's_Moon_Base.wav"));
    assets->pause_music = LoadMusicStream(asset_path("audio/1-X-0.wav"));
    const char *menu_sounds[] = {"MenuMoveItem", "MenuSelect", "MenuCancel", "MenuUpgrade", "MenuDowngrade"};
    for (int i = 0; i < 5; ++i) {
        char path[128];
        snprintf(path, sizeof(path), "audio/%s.wav", menu_sounds[i]);
        assets->menu_sounds[i] = LoadSound(asset_path(path));
        if (!IsSoundValid(assets->menu_sounds[i])) return false;
    }
    assets->shoot = LoadSound(asset_path("audio/WeaponShootMiniShot.wav"));
    assets->explode = LoadSound(asset_path("audio/EnemyDie1.wav"));
    assets->hit = LoadSound(asset_path("audio/PlayerArmorHit.wav"));
    assets->shot_hit = LoadSound(asset_path("audio/WeaponHitMiniShot.wav"));
    assets->die = LoadSound(asset_path("audio/PlayerDie.wav"));
    if (!IsMusicValid(assets->music) || !IsMusicValid(assets->title_music) || !IsMusicValid(assets->pause_music) || !IsSoundValid(assets->shoot) ||
        !IsSoundValid(assets->explode) || !IsSoundValid(assets->hit) || !IsSoundValid(assets->shot_hit) || !IsSoundValid(assets->die)) {
        console_log(console, "ERROR: missing or unsupported audio assets");
        return false;
    }
    assets->music.looping = true;
    assets->title_music.looping = true;
    assets->pause_music.looping = true;
    assets->music_mode = -1;
    SetMusicVolume(assets->music, 1.0f);
    SetSoundVolume(assets->shoot, 0.13f);
    SetSoundVolume(assets->explode, 0.35f);
    SetSoundVolume(assets->hit, 0.45f);
    SetSoundVolume(assets->shot_hit, 0.2f);
    SetSoundVolume(assets->die, 0.5f);
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
        text_center("Press Start", x + 240, 490, 30, dim);
        return;
    }
    /* Positions and original frames from legacy/Hud/Hud.cs + HudManager.cs. */
    DrawTexture(assets->panel_frames, x, 0, WHITE);
    static const char *pilot_names[] = {"Kazuki", "Rabbit", "Gunthor"};
    text_center(pilot_names[player->pilot], x + 242, 78, 30, dim);
    text_center(TextFormat("%d", player->score), x + 242, 154, 32, dim);
    sprite(assets->portraits[player->pilot], (float)x + 238, 356, 256, 0, WHITE);
    DrawTexture(assets->energy_icon, x + 70, 722, Fade(WHITE, 0.35f));
    DrawTexture(assets->shield_icon, x + 70, 805, Fade(WHITE, 0.35f));
    DrawTexture(assets->armor_icon, x + 70, 881, WHITE);
    DrawTexturePro(assets->armor, (Rectangle){0, 0, 256, 64},
        (Rectangle){(float)x + 140, 876, 281.6f * player->health / game_chassis[player->chassis].armor, 73.6f}, (Vector2){0}, 0, WHITE);
}

static void draw_game(const Game *game, const Assets *assets) {
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
        Texture2D texture = effect->kind ? assets->particle_triangle : assets->particle_dot;
        sprite(texture, ARENA_X + effect->x, effect->y, texture.width * effect->scale,
            effect->angle, (Color){effect->r, effect->g, effect->b, 255});
    }
    for (int i = 0; i < GAME_PLAYERS; ++i) {
        const Player *player = &game->players[i];
        if (!player->joined || player->lives <= 0) continue;
        Color color = player_colors[i];
        if (player->invulnerable && game->tick % 8 < 4) color = Fade(color, 0.4f);
        sprite(assets->ships[player->chassis], ARENA_X + player->x, player->y, 80, -90 + player->vx * 1.3f, color);
    }
    EndScissorMode();
    for (int i = 0; i < GAME_ENEMIES; ++i) {
        const Enemy *enemy = &game->enemies[i];
        if (!enemy->active || enemy->kind != ENEMY_BOSS) continue;
        DrawRectangle(ARENA_X + 170, 118, 620, 12, (Color){68, 45, 45, 255});
        DrawRectangle(ARENA_X + 170, 118, (int)(620 * enemy->health / 60), 12, danger);
    }
    draw_player_panel(game, 0, assets);
    draw_player_panel(game, 1, assets);
    if (game->complete && game->menu.page == MENU_NONE) text_center("Stage Clear", 960, 435, 52, accent);
}

static bool title_menu(const Game *game) {
    MenuPage page = game->menu.page;
    return page == MENU_MAIN || page == MENU_NEW_GAME || page == MENU_DIFFICULTY || page == MENU_PILOT ||
        ((page == MENU_OPTIONS || page == MENU_QUIT) && game->menu.parent == MENU_MAIN);
}

static void draw_menu(const Game *game, const Assets *assets, int frame) {
    if (game->menu.page == MENU_NONE) return;
    if (title_menu(game)) {
        ClearBackground((Color){92, 120, 196, 255});
        BeginScissorMode(ARENA_X, 0, GAME_WIDTH, GAME_HEIGHT);
        /* BlueSky's two cloud layers, source texture, scales and velocities. */
        for (int layer = 0; layer < 2; ++layer) for (int i = 0; i < 10; ++i) {
            float x = (float)((i * 193 + layer * 311) % GAME_WIDTH);
            float speed = (layer ? 9.0f : 6.0f) * (1 - fabsf(x - 480) * 0.001f);
            float y = fmodf(i * 137.0f + layer * 317 + frame * speed, GAME_HEIGHT + 64) - 32;
            sprite(assets->cloud, ARENA_X + x, y, assets->cloud.width * (layer ? 0.8f : 1.2f), 0, WHITE);
        }
        EndScissorMode();
        DrawTexture(assets->panel_background, 0, 0, WHITE);
        DrawTexture(assets->panel_background, 1440, 0, WHITE);
        DrawTexture(assets->title, 704, 120, WHITE);
        DrawText("@kalin_t", 1300, 980, 19, WHITE);
        DrawText("@RushJet1", 1300, 1000, 19, WHITE);
    }
    if (game->menu.page == MENU_PILOT) {
        static const char *info[3][5] = {
            {"Kazuki", "Japan", "Ace Pilot", "B", "Speed+"},
            {"Rabbit", "Russia", "Wpn Research", "A", "Money+"},
            {"Gunthor", "Norway", "Demolitions", "O", "Damage+"}
        };
        static const char *keys[] = {"Name", "Country", "Profession", "Blood Type", "Bonus"};
        for (int slot = 0; slot < game->menu.players; ++slot) {
            int x = (slot ? 1440 : 0) + 238, y = 472;
            int pilot = game->menu.pilot[slot];
            DrawText(game->menu.ready[slot] ? "Waiting..." : "Select Pilot", x - 120, y - 240, 36, WHITE);
            DrawRectangle(x - 77, y - 101, 159, 201, DARKGRAY);
            sprite(assets->portraits[pilot], (float)x, (float)y, 256, 0, WHITE);
            if (!game->menu.ready[slot]) {
                if (pilot > 0) sprite(assets->pilot_arrow, (float)x - 116, (float)y, 67, 0, WHITE);
                if (pilot < 2) DrawTexturePro(assets->pilot_arrow, (Rectangle){0, 0, -67, 99},
                    (Rectangle){(float)x + 116, (float)y, 67, 99}, (Vector2){33.5f, 49.5f}, 0, WHITE);
            }
            for (int row = 0; row < 5; ++row) {
                int text_y = y + 160 + row * 32 + (row == 4 ? 32 : 0);
                DrawText(keys[row], x - 40 - MeasureText(keys[row], 20), text_y, 20, WHITE);
                DrawText(":", x - 32, text_y, 20, WHITE);
                DrawText(info[pilot][row], x - 16, text_y, 20, WHITE);
            }
        }
        return;
    }
    for (int row = 0; row < game_menu_count(game); ++row) {
        bool selected = row == game->menu.cursor, enabled = game_menu_enabled(game, row);
        int texture = enabled ? (selected ? 2 : 0) + (game_menu_small(game, row) ? 1 : 0) : selected ? 5 : 4;
        int y = 400 + row * 66;
        DrawTexture(assets->menu[texture], 832, y, WHITE);
        text_center(game_menu_label(game, row), 960, y + 20, 24, enabled ? WHITE : GRAY);
        if (game->menu.page == MENU_OPTIONS && row < 3) {
            for (int i = 0; i < 10; ++i) DrawRectangle(864 + i * 20, y + 46, 16, 8,
                game->menu.options[row] > i ? LIGHTGRAY : GRAY);
        }
    }
}

static void menu_input(Game *game, Assets *assets, MenuAction action, int slot) {
    GameMenu before = game->menu;
    bool valid = before.page == MENU_PILOT || game_menu_enabled(game, before.cursor);
    game_menu_input(game, action, slot);
    if (!assets->audio) return;
    int sound = -1;
    if (action == MENU_BACK) sound = 2;
    else if (action == MENU_CONFIRM) sound = valid ? 1 : 2;
    else if (before.cursor != game->menu.cursor || before.pilot[slot] != game->menu.pilot[slot]) sound = 0;
    else if (before.page == MENU_OPTIONS && memcmp(before.options, game->menu.options, sizeof(before.options))) sound = action == MENU_RIGHT ? 3 : 4;
    if (sound >= 0) PlaySound(assets->menu_sounds[sound]);
}

static void poll_menu(Game *game, Assets *assets) {
    if (game->menu.page == MENU_PAUSE) {
        bool resume = IsKeyPressed(KEY_P);
        for (int slot = 0; slot < GAME_PLAYERS; ++slot) resume |= IsGamepadAvailable(slot) &&
            IsGamepadButtonPressed(slot, GAMEPAD_BUTTON_MIDDLE_RIGHT);
        if (resume) { menu_input(game, assets, MENU_BACK, 0); return; }
    }
    for (int slot = 0; slot < GAME_PLAYERS; ++slot) {
        bool pressed[6] = {false};
        if (slot == 0) {
            pressed[MENU_UP] = IsKeyPressed(KEY_UP);
            pressed[MENU_DOWN] = IsKeyPressed(KEY_DOWN);
            pressed[MENU_LEFT] = IsKeyPressed(KEY_LEFT);
            pressed[MENU_RIGHT] = IsKeyPressed(KEY_RIGHT);
            pressed[MENU_CONFIRM] = IsKeyPressed(KEY_ENTER);
            pressed[MENU_BACK] = IsKeyPressed(KEY_ESCAPE);
        } else if (game->menu.page == MENU_PILOT) {
            pressed[MENU_LEFT] = IsKeyPressed(KEY_J);
            pressed[MENU_RIGHT] = IsKeyPressed(KEY_L);
            pressed[MENU_CONFIRM] = IsKeyPressed(KEY_RIGHT_CONTROL);
            pressed[MENU_BACK] = IsKeyPressed(KEY_BACKSPACE);
        }
        if (IsGamepadAvailable(slot)) {
            const int buttons[] = {GAMEPAD_BUTTON_LEFT_FACE_UP, GAMEPAD_BUTTON_LEFT_FACE_DOWN,
                GAMEPAD_BUTTON_LEFT_FACE_LEFT, GAMEPAD_BUTTON_LEFT_FACE_RIGHT,
                GAMEPAD_BUTTON_RIGHT_FACE_DOWN, GAMEPAD_BUTTON_RIGHT_FACE_RIGHT};
            for (int i = 0; i < 6; ++i) pressed[i] |= IsGamepadButtonPressed(slot, buttons[i]);
        }
        for (int i = 0; i < 6; ++i) if (pressed[i]) { menu_input(game, assets, (MenuAction)i, slot); break; }
    }
}

static void update_audio(const Game *game, Assets *assets) {
    if (!assets->audio) return;
    int mode = title_menu(game) ? 1 : game->paused ? 2 : 0;
    Music tracks[] = {assets->music, assets->title_music, assets->pause_music};
    if (assets->music_mode != mode) {
        if (assets->music_mode >= 0) PauseMusicStream(tracks[assets->music_mode]);
        if (mode != 0) StopMusicStream(tracks[mode]);
        PlayMusicStream(tracks[mode]);
        assets->music_mode = mode;
    }
    float sfx = game->menu.options[0] / 10.0f;
    SetMusicVolume(tracks[mode], game->menu.options[1] / 10.0f);
    SetSoundVolume(assets->shoot, 0.13f * sfx);
    SetSoundVolume(assets->explode, 0.35f * sfx);
    SetSoundVolume(assets->hit, 0.45f * sfx);
    SetSoundVolume(assets->shot_hit, 0.2f * sfx);
    SetSoundVolume(assets->die, 0.5f * sfx);
    for (int i = 0; i < 5; ++i) SetSoundVolume(assets->menu_sounds[i], sfx);
    UpdateMusicStream(tracks[mode]);
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
#ifndef GALAXY_MEMORY
    SetConfigFlags(FLAG_WINDOW_RESIZABLE | FLAG_VSYNC_HINT | FLAG_MSAA_4X_HINT);
#endif
#ifdef GALAXY_MEMORY
    InitWindow(CANVAS_WIDTH, CANVAS_HEIGHT, "Galaxy Defenders - software check");
#else
    InitWindow(1280, 720, "Galaxy Defenders - native first pass");
#endif
    if (!IsWindowReady()) { console_log(console, "ERROR: could not create window"); return 1; }
    SetExitKey(KEY_NULL);
#ifdef GALAXY_MEMORY
    SetTargetFPS(0);
#else
    SetWindowMinSize(960, 540);
    SetTargetFPS(60);
#endif
    RenderTexture2D canvas = {0};
#ifndef GALAXY_MEMORY
    canvas = LoadRenderTexture(CANVAS_WIDTH, CANVAS_HEIGHT);
    if (!IsRenderTextureValid(canvas)) { CloseWindow(); return 1; }
#endif
    if (!assets_load(&assets, console, mute, volume)) {
        assets_unload(&assets);
        if (IsRenderTextureValid(canvas)) UnloadRenderTexture(canvas);
        CloseWindow();
        return 1;
    }
    if (IsRenderTextureValid(canvas)) SetTextureFilter(canvas.texture, TEXTURE_FILTER_BILINEAR);
    bool console_open = show_console, console_full = false, command_failed = false, muted = mute;
    char command[CONSOLE_LINE] = {0};
    char last_command[CONSOLE_LINE] = {0};
    int command_length = 0, rendered = 0, result = 0, menu_frame = 0;
    double accumulator = 0;
    console_log(console, "Ready. Type help; console commands and launch scripts share the same game.");
    while (!WindowShouldClose() && !console->quit && !game->menu.quit) {
        update_audio(game, &assets);
        bool was_menu = game->menu.page != MENU_NONE;
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
        } else if (!toggled && was_menu) {
            poll_menu(game, &assets);
        } else if (!toggled) {
            bool start[2] = {false, false};
            for (int i = 0; i < GAME_PLAYERS; ++i) start[i] = IsGamepadAvailable(i) && IsGamepadButtonPressed(i, GAMEPAD_BUTTON_MIDDLE_RIGHT);
            bool joining = !game->players[1].joined && (IsKeyPressed(KEY_F2) || start[1]);
            if (joining) game_join(game, 1);
            if (IsKeyPressed(KEY_P) || IsKeyPressed(KEY_ESCAPE) || start[0] || (start[1] && !joining)) game_menu_open(game, MENU_PAUSE);
            bool restart = IsKeyPressed(KEY_R);
            for (int i = 0; i < GAME_PLAYERS; ++i) {
                if ((game->over || game->complete) && IsGamepadAvailable(i) && IsGamepadButtonPressed(i, GAMEPAD_BUTTON_RIGHT_FACE_UP)) restart = true;
            }
            if (restart) {
                game_retry(game);
                if (assets.audio) { StopMusicStream(assets.music); PlayMusicStream(assets.music); }
            }
            if (IsKeyPressed(KEY_M) && assets.audio) { muted = !muted; SetMasterVolume(muted ? 0 : volume); }
        }
        int old_shots = game->shots, old_kills = game->kills, old_hits = game->hits;
        int old_projectile_hits = game->projectile_hits, old_deaths = game->deaths;
        if (!console_open && !toggled && ((!was_menu && !game->paused) || game->menu.page == MENU_OVER) && (IsWindowFocused() || frames)) {
            if (!frames) for (int i = 0; i < GAME_PLAYERS; ++i) game->players[i].input = poll_input(i);
            accumulator += frames ? 1.0 / GAME_HZ : fmin(GetFrameTime(), 0.1);
            while (accumulator >= 1.0 / GAME_HZ) {
                game_tick(game);
                accumulator -= 1.0 / GAME_HZ;
            }
        } else accumulator = 0;
        if (assets.audio) {
            if (game->shots > old_shots) PlaySound(assets.shoot);
            if (game->kills > old_kills) PlaySound(assets.explode);
            if (game->hits > old_hits) PlaySound(assets.hit);
            if (game->projectile_hits > old_projectile_hits) PlaySound(assets.shot_hit);
            if (game->deaths > old_deaths) PlaySound(assets.die);
        }
        if (game->over && game->menu.page == MENU_NONE) game_menu_open(game, MENU_OVER);
#ifdef GALAXY_MEMORY
        BeginDrawing();
#else
        BeginTextureMode(canvas);
#endif
        draw_game(game, &assets);
        draw_menu(game, &assets, menu_frame++);
        if (console_open) draw_console(console, command, command_failed, console_full);
#ifndef GALAXY_MEMORY
        EndTextureMode();
        BeginDrawing();
        ClearBackground(BLACK);
        float scale = fminf(GetScreenWidth() / (float)CANVAS_WIDTH, GetScreenHeight() / (float)CANVAS_HEIGHT);
        scale *= 0.875f + game->menu.options[2] * 0.0125f;
        Rectangle destination = {(GetScreenWidth() - CANVAS_WIDTH * scale) / 2,
            (GetScreenHeight() - CANVAS_HEIGHT * scale) / 2, CANVAS_WIDTH * scale, CANVAS_HEIGHT * scale};
        DrawTexturePro(canvas.texture, (Rectangle){0, 0, CANVAS_WIDTH, -CANVAS_HEIGHT}, destination, (Vector2){0}, 0, WHITE);
#endif
        EndDrawing();
        if (frames && ++rendered >= frames) {
            if (capture) {
#ifdef GALAXY_MEMORY
                /* raylib 6.0 rlsw stubs glGetTexImage; read its screen framebuffer. */
                Image image = LoadImageFromScreen();
                /* rlsw rows are top-down; undo rlReadScreenPixels' OpenGL flip. */
                ImageFlipVertical(&image);
#else
                Image image = LoadImageFromTexture(canvas.texture);
                ImageFlipVertical(&image);
#endif
                if (!ExportImage(image, capture)) result = 1;
                UnloadImage(image);
            }
            break;
        }
    }
    console_execute(console, game, "state");
    assets_unload(&assets);
    if (IsRenderTextureValid(canvas)) UnloadRenderTexture(canvas);
    CloseWindow();
    return result;
}
