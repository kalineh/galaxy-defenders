#include "game.h"
#include "console.h"
#include "raylib.h"
#include <math.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define CANVAS_WIDTH 1920
#define CANVAS_HEIGHT 1080
#define ARENA_X 480
#define FRAME_SAMPLES 240

typedef struct {
    float frame_ms[FRAME_SAMPLES], work_ms[FRAME_SAMPLES];
    int cursor, count;
} FrameGraph;

typedef struct {
    Texture2D ships[GAME_CHASSIS], enemies[ENEMY_KIND_COUNT], shot, hostile_shot;
    Texture2D panel_background, panel_frames, armor, armor_icon, energy_icon, shield_icon, portraits[3];
    Texture2D particle_dot, particle_triangle, energy;
    Texture2D menu[6], title, cloud, pilot_arrow;
    Texture2D stage_textures[STAGE_MAX_EVENTS], stage_destroyed[STAGE_MAX_EVENTS];
    int stage_texture_owner[STAGE_MAX_EVENTS];
    Music music, title_music, pause_music;
    int music_mode, music_revision, music_event, music_tick;
    const char *music_name;
    Sound shoot, explode, building_explode, hit, shot_hit, die;
    Sound menu_sounds[5];
    bool audio;
    Font console_font, ui_font;
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
    if (assets->console_font.texture.id && assets->console_font.texture.id != GetFontDefault().texture.id) UnloadFont(assets->console_font);
    if (assets->ui_font.texture.id && assets->ui_font.texture.id != GetFontDefault().texture.id) UnloadFont(assets->ui_font);
    for (int i = 0; i < GAME_CHASSIS; ++i) if (IsTextureValid(assets->ships[i])) UnloadTexture(assets->ships[i]);
    for (int i = 0; i < ENEMY_KIND_COUNT; ++i) if (IsTextureValid(assets->enemies[i])) UnloadTexture(assets->enemies[i]);
    if (IsTextureValid(assets->shot)) UnloadTexture(assets->shot);
    if (IsTextureValid(assets->hostile_shot)) UnloadTexture(assets->hostile_shot);
    if (IsTextureValid(assets->particle_dot)) UnloadTexture(assets->particle_dot);
    if (IsTextureValid(assets->particle_triangle)) UnloadTexture(assets->particle_triangle);
    if (IsTextureValid(assets->energy)) UnloadTexture(assets->energy);
    Texture2D hud[] = {assets->panel_background, assets->panel_frames, assets->armor, assets->armor_icon,
        assets->energy_icon, assets->shield_icon, assets->portraits[0], assets->portraits[1]};
    for (int i = 0; i < 8; ++i) if (IsTextureValid(hud[i])) UnloadTexture(hud[i]);
    if (IsTextureValid(assets->portraits[2])) UnloadTexture(assets->portraits[2]);
    for (int i = 0; i < 6; ++i) if (IsTextureValid(assets->menu[i])) UnloadTexture(assets->menu[i]);
    if (IsTextureValid(assets->title)) UnloadTexture(assets->title);
    if (IsTextureValid(assets->cloud)) UnloadTexture(assets->cloud);
    if (IsTextureValid(assets->pilot_arrow)) UnloadTexture(assets->pilot_arrow);
    for (int i = 0; i < stage_definitions[0].event_count; ++i) {
        if (assets->stage_texture_owner[i] != i) continue;
        if (IsTextureValid(assets->stage_textures[i])) UnloadTexture(assets->stage_textures[i]);
        if (IsTextureValid(assets->stage_destroyed[i])) UnloadTexture(assets->stage_destroyed[i]);
    }
    if (assets->audio) {
        if (IsMusicValid(assets->music)) UnloadMusicStream(assets->music);
        if (IsMusicValid(assets->title_music)) UnloadMusicStream(assets->title_music);
        if (IsMusicValid(assets->pause_music)) UnloadMusicStream(assets->pause_music);
        for (int i = 0; i < 5; ++i) if (IsSoundValid(assets->menu_sounds[i])) UnloadSound(assets->menu_sounds[i]);
        if (IsSoundValid(assets->shoot)) UnloadSound(assets->shoot);
        if (IsSoundValid(assets->building_explode)) UnloadSound(assets->building_explode);
        if (IsSoundValid(assets->explode)) UnloadSound(assets->explode);
        if (IsSoundValid(assets->hit)) UnloadSound(assets->hit);
        if (IsSoundValid(assets->shot_hit)) UnloadSound(assets->shot_hit);
        if (IsSoundValid(assets->die)) UnloadSound(assets->die);
        CloseAudioDevice();
    }
}

static bool assets_load(Assets *assets, Console *console, bool mute, float volume) {
    /* Solcraft's fixed-font preference: use the OS font, never copy Windows fonts. */
    assets->ui_font = LoadFontEx(asset_path("fonts/NotoSans-Regular.ttf"), 64, NULL, 95);
    const char *windows = getenv("WINDIR");
    const char *font_names[] = {"consola.ttf", "cour.ttf"};
    for (int i = 0; windows && i < 2; ++i) {
        char path[2048];
        snprintf(path, sizeof(path), "%s/Fonts/%s", windows, font_names[i]);
        if (!FileExists(path)) continue;
        Font font = LoadFontEx(path, 32, NULL, 95);
        if (font.texture.id && font.texture.id != GetFontDefault().texture.id) {
            assets->console_font = font;
            SetTextureFilter(font.texture, TEXTURE_FILTER_BILINEAR);
            console_log(console, "Console font: %s", font_names[i]);
            break;
        }
    }
    if (!assets->console_font.texture.id) {
        assets->console_font = LoadFontEx(asset_path("fonts/NotoSansMono-Regular.ttf"), 32, NULL, 95);
        console_log(console, "Console font: Noto Sans Mono");
    }
    if (!IsFontValid(assets->ui_font) || assets->ui_font.texture.id == GetFontDefault().texture.id ||
        !IsFontValid(assets->console_font) || assets->console_font.texture.id == GetFontDefault().texture.id) {
        console_log(console, "ERROR: required UI/console font could not load; default raylib text is disabled.");
        return false;
    }
    SetTextureFilter(assets->ui_font.texture, TEXTURE_FILTER_BILINEAR);
    SetTextureFilter(assets->console_font.texture, TEXTURE_FILTER_BILINEAR);
    for (int i = 0; i < GAME_CHASSIS; ++i) {
        char path[128];
        snprintf(path, sizeof(path), "textures/%s.png", game_chassis[i].name);
        assets->ships[i] = texture_load(path);
    }
    for (int i = 0; i < ENEMY_KIND_COUNT; ++i) {
        char path[128];
        snprintf(path, sizeof(path), "textures/%s.png", enemy_names[i]);
        assets->enemies[i] = texture_load(path);
    }
    assets->shot = texture_load("textures/Laser.png");
    assets->energy = texture_load("textures/Energy.png");
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
    valid = valid && IsTextureValid(assets->particle_dot) && IsTextureValid(assets->particle_triangle) && IsTextureValid(assets->energy);
    for (int i = 0; i < ENEMY_KIND_COUNT; ++i) valid = valid && IsTextureValid(assets->enemies[i]);
    Texture2D hud[] = {assets->panel_background, assets->panel_frames, assets->armor, assets->armor_icon,
        assets->energy_icon, assets->shield_icon, assets->portraits[0], assets->portraits[1]};
    for (int i = 0; i < 8; ++i) valid = valid && IsTextureValid(hud[i]);
    for (int i = 0; i < 6; ++i) valid = valid && IsTextureValid(assets->menu[i]);
    valid = valid && IsTextureValid(assets->title) && IsTextureValid(assets->cloud) &&
        IsTextureValid(assets->pilot_arrow) && IsTextureValid(assets->portraits[2]);
    const StageDefinition *stage = &stage_definitions[0];
    for (int i = 0; i < stage->event_count; ++i) {
        const StageEvent *event = &stage->events[i];
        assets->stage_texture_owner[i] = i;
        if (event->kind != STAGE_DECORATION && event->kind != STAGE_BUILDING) continue;
        int owner = i;
        for (int j = 0; j < i; ++j) if (stage->events[j].kind == event->kind &&
            !strcmp(stage->events[j].name, event->name)) { owner = j; break; }
        assets->stage_texture_owner[i] = owner;
        if (owner != i) {
            assets->stage_textures[i] = assets->stage_textures[owner];
            assets->stage_destroyed[i] = assets->stage_destroyed[owner];
            continue;
        }
        char path[160];
        snprintf(path, sizeof(path), "textures/stage/%s/%s.png",
            event->kind == STAGE_DECORATION ? "Decoration" : "Static", event->name);
        assets->stage_textures[i] = texture_load(path);
        valid = valid && IsTextureValid(assets->stage_textures[i]);
        if (event->kind == STAGE_BUILDING) {
            snprintf(path, sizeof(path), "textures/stage/Static/Destroyed/%s.png", event->name);
            assets->stage_destroyed[i] = texture_load(path);
            valid = valid && IsTextureValid(assets->stage_destroyed[i]);
        }
    }
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
    assets->shoot = LoadSound(asset_path("audio/WeaponShootLaser.wav"));
    assets->building_explode = LoadSound(asset_path("audio/BuildingExplode.wav"));
    assets->explode = LoadSound(asset_path("audio/EnemyDie1.wav"));
    assets->hit = LoadSound(asset_path("audio/PlayerArmorHit.wav"));
    assets->shot_hit = LoadSound(asset_path("audio/WeaponHitLaser.wav"));
    assets->die = LoadSound(asset_path("audio/PlayerDie.wav"));
    if (!IsMusicValid(assets->music) || !IsMusicValid(assets->title_music) || !IsMusicValid(assets->pause_music) || !IsSoundValid(assets->shoot) ||
        !IsSoundValid(assets->explode) || !IsSoundValid(assets->building_explode) || !IsSoundValid(assets->hit) || !IsSoundValid(assets->shot_hit) || !IsSoundValid(assets->die)) {
        console_log(console, "ERROR: missing or unsupported audio assets");
        return false;
    }
    assets->music.looping = true;
    assets->title_music.looping = true;
    assets->pause_music.looping = true;
    assets->music_mode = -1;
    assets->music_name = "The_Voyage";
    assets->music_revision = -1;
    assets->music_event = -2;
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

static void text_center(Font font, const char *text, int x, int y, int size, Color color) {
    DrawTextEx(font, text, (Vector2){x - MeasureTextEx(font, text, (float)size, 0).x / 2, (float)y}, (float)size, 0, color);
}

static void draw_player_panel(const Game *game, int slot, const Assets *assets) {
    const Player *player = &game->players[slot];
    int x = slot == 0 ? 0 : 1440;
    DrawTexture(assets->panel_background, x, 0, WHITE);
    if (!player->joined) {
        text_center(assets->ui_font, "Press Start", x + 240, 490, 30, dim);
        return;
    }
    /* Positions and original frames from legacy/Hud/Hud.cs + HudManager.cs. */
    DrawTexture(assets->panel_frames, x, 0, WHITE);
    static const char *pilot_names[] = {"Kazuki", "Rabbit", "Gunthor"};
    text_center(assets->ui_font, pilot_names[player->pilot], x + 242, 78, 30, dim);
    text_center(assets->ui_font, TextFormat("%d", player->score), x + 242, 154, 32, dim);
    sprite(assets->portraits[player->pilot], (float)x + 238, 356, 256, 0, WHITE);
    DrawTexture(assets->energy_icon, x + 70, 722, WHITE);
    DrawTexturePro(assets->energy, (Rectangle){0, 0, 256, 64},
        (Rectangle){(float)x + 140, 716, 281.6f * player->energy / 3, 73.6f}, (Vector2){0}, 0, WHITE);
    DrawTexture(assets->shield_icon, x + 70, 805, Fade(WHITE, 0.35f));
    DrawTexture(assets->armor_icon, x + 70, 881, WHITE);
    DrawTexturePro(assets->armor, (Rectangle){0, 0, 256, 64},
        (Rectangle){(float)x + 140, 876, 281.6f * player->health / game_chassis[player->chassis].armor, 73.6f}, (Vector2){0}, 0, WHITE);
}

static void draw_game(const Game *game, const Assets *assets) {
    ClearBackground(background);
    DrawRectangle(ARENA_X, 0, GAME_WIDTH, GAME_HEIGHT, background);
    BeginScissorMode(ARENA_X, 0, GAME_WIDTH, GAME_HEIGHT);
    ClearBackground((Color){92, 120, 196, 255});
    for (int layer = 0; layer < 2; ++layer) for (int i = 0; i < 10; ++i) {
        float x = (float)((i * 193 + layer * 311) % GAME_WIDTH);
        float speed = (layer ? 9.0f : 6.0f) * (1 - fabsf(x - 480) * 0.001f);
        float y = fmodf(i * 137.0f + layer * 317 + game->tick * speed + 400 - game->stage.camera_y,
                       GAME_HEIGHT + 128) - 64;
        sprite(assets->cloud, ARENA_X + x, y, (float)assets->cloud.width, 0, WHITE);
    }
    /* Legacy decoration depth offsets precede the main carrier tiles. */
    const StageDefinition *stage = &stage_definitions[game->stage.index];
    for (int pass = 0; pass < 3; ++pass) for (int i = 0; i < stage->event_count; ++i) {
        const StageEvent *event = &stage->events[i];
        if (!game->stage.activated[i]) continue;
        if (event->kind != STAGE_DECORATION && event->kind != STAGE_BUILDING) continue;
        int depth = event->kind == STAGE_BUILDING ? 2 : event->depth < 0 ? 0 : 1;
        if (depth != pass) continue;
        float y = event->y - game->stage.camera_y + GAME_HEIGHT * 0.5f;
        if (y < -event->height || y > GAME_HEIGHT + event->height) continue;
        Texture2D texture = event->kind == STAGE_BUILDING && game->stage.building_health[i] < 0 ?
            assets->stage_destroyed[i] : assets->stage_textures[i];
        sprite(texture, ARENA_X + GAME_WIDTH * 0.5f + event->x, y, (float)event->width, event->rotation, WHITE);
    }
    for (int i = 0; i < GAME_ENEMIES; ++i) {
        const Enemy *enemy = &game->enemies[i];
        if (!enemy->active) continue;
        float width = (float)assets->enemies[enemy->kind].width;
        sprite(assets->enemies[enemy->kind], ARENA_X + enemy->x, enemy->y, width, enemy->rotation * RAD2DEG, WHITE);
    }
    for (int i = 0; i < GAME_BULLETS; ++i) {
        const Bullet *bullet = &game->bullets[i];
        if (!bullet->active) continue;
        Color color = bullet->owner < 0 ? WHITE : player_colors[bullet->owner];
        sprite(bullet->owner < 0 ? assets->hostile_shot : assets->shot,
            ARENA_X + bullet->x, bullet->y, bullet->owner < 0 ? 24 : (float)assets->shot.width, bullet->owner < 0 ? 0 : -90, color);
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
        sprite(assets->ships[player->chassis], ARENA_X + player->x, player->y, 80, -90, color);
    }
    EndScissorMode();
    for (int i = 0; i < GAME_ENEMIES; ++i) {
        const Enemy *enemy = &game->enemies[i];
        if (!enemy->active || enemy->kind != ENEMY_BOSS) continue;
        DrawRectangle(ARENA_X + 170, 118, 620, 12, (Color){68, 45, 45, 255});
        DrawRectangle(ARENA_X + 170, 118, (int)(620 * enemy->health / enemy->health_max), 12, danger);
    }
    draw_player_panel(game, 0, assets);
    draw_player_panel(game, 1, assets);
    if (game->complete && game->menu.page == MENU_NONE) text_center(assets->ui_font, "Stage Clear", 960, 435, 52, accent);
}

static bool title_menu(const Game *game) {
    MenuPage page = game->menu.page;
    return page == MENU_MAIN || page == MENU_NEW_GAME || page == MENU_DIFFICULTY || page == MENU_PILOT ||
        ((page == MENU_OPTIONS || page == MENU_QUIT) && game->menu.parent == MENU_MAIN);
}

static void draw_menu(const Game *game, const Assets *assets, int frame) {
    if (game->menu.page == MENU_NONE) return;
    if (game->menu.page == MENU_CLEAR) {
        /* StageClearPanel.cs heading and Continue action; campaign stats/shop pending. */
        text_center(assets->ui_font, "Stage Clear", 960, 435, 52, accent);
        text_center(assets->ui_font, "Continue", 960, 525, 30, WHITE);
        return;
    }
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
        DrawTextEx(assets->ui_font, "@kalin_t", (Vector2){1300, 980}, 19, 0, WHITE);
        DrawTextEx(assets->ui_font, "@RushJet1", (Vector2){1300, 1000}, 19, 0, WHITE);
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
            DrawTextEx(assets->ui_font, game->menu.ready[slot] ? "Waiting..." : "Select Pilot", (Vector2){x - 120.0f, y - 240.0f}, 36, 0, WHITE);
            DrawRectangle(x - 77, y - 101, 159, 201, DARKGRAY);
            sprite(assets->portraits[pilot], (float)x, (float)y, 256, 0, WHITE);
            if (!game->menu.ready[slot]) {
                if (pilot > 0) sprite(assets->pilot_arrow, (float)x - 116, (float)y, 67, 0, WHITE);
                if (pilot < 2) DrawTexturePro(assets->pilot_arrow, (Rectangle){0, 0, -67, 99},
                    (Rectangle){(float)x + 116, (float)y, 67, 99}, (Vector2){33.5f, 49.5f}, 0, WHITE);
            }
            for (int row = 0; row < 5; ++row) {
                int text_y = y + 160 + row * 32 + (row == 4 ? 32 : 0);
                DrawTextEx(assets->ui_font, keys[row], (Vector2){x - 40 - MeasureTextEx(assets->ui_font, keys[row], 20, 0).x, (float)text_y}, 20, 0, WHITE);
                DrawTextEx(assets->ui_font, ":", (Vector2){x - 32.0f, (float)text_y}, 20, 0, WHITE);
                DrawTextEx(assets->ui_font, info[pilot][row], (Vector2){x - 16.0f, (float)text_y}, 20, 0, WHITE);
            }
        }
        return;
    }
    for (int row = 0; row < game_menu_count(game); ++row) {
        bool selected = row == game->menu.cursor, enabled = game_menu_enabled(game, row);
        int texture = enabled ? (selected ? 2 : 0) + (game_menu_small(game, row) ? 1 : 0) : selected ? 5 : 4;
        int y = 400 + row * 66;
        DrawTexture(assets->menu[texture], 832, y, WHITE);
        text_center(assets->ui_font, game_menu_label(game, row), 960, y + 20, 24, enabled ? WHITE : GRAY);
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
    const char *music_name = game_music(game);
    bool music_changed = strcmp(assets->music_name, music_name) != 0;
    bool seek = game->tick < assets->music_tick || assets->music_revision != game->stage.revision || assets->music_event != game->stage.music_event;
    if (music_changed) {
        StopMusicStream(assets->music);
        UnloadMusicStream(assets->music);
        char path[160];
        snprintf(path, sizeof(path), "audio/%s.wav", music_name);
        assets->music = LoadMusicStream(asset_path(path));
        assets->music.looping = true;
        assets->music_name = music_name;
        if (assets->music_mode == 0) assets->music_mode = -1;
    }
    assets->music_tick = game->tick;
    int mode = title_menu(game) ? 1 : game->paused ? 2 : 0;
    Music tracks[] = {assets->music, assets->title_music, assets->pause_music};
    if (assets->music_mode != mode) {
        if (assets->music_mode >= 0) PauseMusicStream(tracks[assets->music_mode]);
        if (mode != 0) StopMusicStream(tracks[mode]);
        if (mode == 0 && !seek && !music_changed && assets->music_mode >= 0) ResumeMusicStream(tracks[mode]);
        else PlayMusicStream(tracks[mode]);
        assets->music_mode = mode;
    }
    if (seek || music_changed) {
        float seconds = (game->tick - game->stage.music_tick) / (float)GAME_HZ;
        float duration = GetMusicTimeLength(assets->music);
        if (duration > 0) {
            float offset = fmodf(seconds, duration);
            SeekMusicStream(assets->music, offset);
            TraceLog(LOG_INFO, "Stage music: %s seek=%.3f reported=%.3f", music_name, offset, GetMusicTimePlayed(assets->music));
        }
        assets->music_revision = game->stage.revision;
        assets->music_event = game->stage.music_event;
    }
    float sfx = game->menu.options[0] / 10.0f;
    SetMusicVolume(tracks[mode], game->menu.options[1] / 10.0f);
    SetSoundVolume(assets->shoot, 0.13f * sfx);
    SetSoundVolume(assets->explode, 0.35f * sfx);
    SetSoundVolume(assets->building_explode, sfx);
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

static void draw_console(Console *console, Font font, bool failed, bool full_screen) {
    int panel_height = full_screen ? CANVAS_HEIGHT : 450;
    int visible = (panel_height - 100) / 31;
    float cell = MeasureTextEx(font, "M", 26, 0).x;
    int columns = (int)((CANVAS_WIDTH - 40) / cell);
    if (columns >= CONSOLE_LINE) columns = CONSOLE_LINE - 1;
    int total = 0;
    for (int i = 0; i < console->count; ++i) total += ((int)strlen(console->lines[i]) + columns - 1) / columns + (console->lines[i][0] == '\0');
    int max_scroll = total > visible ? total - visible : 0;
    if (console->scroll < 0) console->scroll = 0;
    if (console->scroll > max_scroll) console->scroll = max_scroll;
    int first = total - console->scroll - visible;
    if (first < 0) first = 0;
#ifdef GALAXY_MEMORY
    /* rlsw scissor coordinates are top-down; rcore applies an OpenGL Y flip. */
    BeginScissorMode(0, CANVAS_HEIGHT - panel_height, CANVAS_WIDTH, panel_height);
#else
    BeginScissorMode(0, 0, CANVAS_WIDTH, panel_height);
#endif
    DrawRectangle(0, 0, CANVAS_WIDTH, panel_height, (Color){5, 8, 12, 242});
    int row = 0, drawn = 0;
    for (int i = 0; i < console->count; ++i) {
        int length = (int)strlen(console->lines[i]);
        for (int offset = 0; offset < length || (offset == 0 && length == 0); offset += columns, ++row) {
            if (row < first || drawn == visible) continue;
            char text[CONSOLE_LINE];
            int n = length - offset < columns ? length - offset : columns;
            memcpy(text, console->lines[i] + offset, (size_t)n); text[n] = '\0';
            Color color = !strncmp(console->lines[i], "ERROR", 5) || !strncmp(console->lines[i], "FAIL", 4) ? danger : (Color){210, 224, 214, 255};
            DrawTextEx(font, text, (Vector2){18, 15 + drawn++ * 31.0f}, 26, 0, color);
        }
    }
    DrawTextEx(font, TextFormat("Tab complete | Up/Down history | PgUp/PgDn / wheel scroll | %d/%d lines", total - console->scroll, total),
        (Vector2){18, (float)panel_height - 80}, 22, 0, dim);
    DrawRectangle(0, panel_height - 48, CANVAS_WIDTH, 48, (Color){12, 16, 22, 255});
    DrawTextEx(font, ">", (Vector2){18, (float)panel_height - 42}, 26, 0, accent);
    int input_columns = columns - 3;
    int start = console->cursor >= input_columns ? console->cursor - input_columns + 1 : 0;
    DrawTextEx(font, console->input + start, (Vector2){51, (float)panel_height - 42}, 26, 0, failed ? danger : RAYWHITE);
    DrawRectangle((int)(51 + (console->cursor - start) * cell), panel_height - 43, 2, 28, accent);
    EndScissorMode();
}

static void draw_frame_graph(const FrameGraph *history, Font font) {
    /* Solcraft/MMOA: fixed 33.33 ms range, frame + CPU work traces, 60 Hz guide. */
    Rectangle panel = {CANVAS_WIDTH - 495, 15, 480, 189};
    Rectangle graph = {panel.x + 12, panel.y + 48, panel.width - 24, 126};
    DrawRectangleRec(panel, (Color){4, 7, 10, 235});
    DrawRectangleLinesEx(panel, 1, (Color){82, 116, 128, 230});
    int last = (history->cursor + FRAME_SAMPLES - 1) % FRAME_SAMPLES;
    float frame_ms = history->count ? history->frame_ms[last] : 0;
    float work_ms = history->count ? history->work_ms[last] : 0;
    DrawTextEx(font, TextFormat("%.1f FPS  frame %.2f  work %.2f ms", frame_ms > 0 ? 1000 / frame_ms : 0, frame_ms, work_ms),
        (Vector2){panel.x + 12, panel.y + 9}, 20, 0, WHITE);
    DrawRectangleRec(graph, (Color){2, 5, 8, 220});
    DrawLineV((Vector2){graph.x, graph.y + graph.height / 2}, (Vector2){graph.x + graph.width, graph.y + graph.height / 2}, (Color){104, 132, 140, 150});
    DrawTextEx(font, "16.67 ms", (Vector2){graph.x + graph.width - 103, graph.y + graph.height / 2 - 21}, 18, 0, dim);
    int oldest = (history->cursor + FRAME_SAMPLES - history->count) % FRAME_SAMPLES;
    for (int trace = 0; trace < 2; ++trace) {
        const float *samples = trace ? history->work_ms : history->frame_ms;
        Color color = trace ? (Color){83, 206, 232, 255} : (Color){246, 190, 82, 255};
        for (int i = 1; i < history->count; ++i) {
            float a = fminf(33.333f, fmaxf(0, samples[(oldest + i - 1) % FRAME_SAMPLES]));
            float b = fminf(33.333f, fmaxf(0, samples[(oldest + i) % FRAME_SAMPLES]));
            float x = graph.x + graph.width * (FRAME_SAMPLES - history->count + i) / FRAME_SAMPLES;
            DrawLineV((Vector2){x - graph.width / FRAME_SAMPLES, graph.y + graph.height * (1 - a / 33.333f)},
                (Vector2){x, graph.y + graph.height * (1 - b / 33.333f)}, color);
        }
    }
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
    int rendered = 0, result = 0, menu_frame = 0;
    double accumulator = 0;
    FrameGraph frame_graph = {0};
    console_log(console, "Ready. Type help; console commands and launch scripts share the same game.");
    while (!WindowShouldClose() && !console->quit && !game->menu.quit) {
        double frame_start = GetTime();
        if (IsKeyPressed(KEY_F3)) console->perf = !console->perf;
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
                if (character >= 32 && character < 127 && character != '`') {
                    char text[] = {(char)character, '\0'}; console_insert(console, text);
                }
            }
            if (IsKeyPressed(KEY_BACKSPACE) || IsKeyPressedRepeat(KEY_BACKSPACE)) console_erase(console, true);
            if (IsKeyPressed(KEY_DELETE) || IsKeyPressedRepeat(KEY_DELETE)) console_erase(console, false);
            if ((IsKeyPressed(KEY_LEFT) || IsKeyPressedRepeat(KEY_LEFT)) && console->cursor > 0) --console->cursor;
            if ((IsKeyPressed(KEY_RIGHT) || IsKeyPressedRepeat(KEY_RIGHT)) && console->input[console->cursor]) ++console->cursor;
            if (IsKeyPressed(KEY_HOME)) console->cursor = 0;
            if (IsKeyPressed(KEY_END)) console->cursor = (int)strlen(console->input);
            if (IsKeyPressed(KEY_UP) || IsKeyPressedRepeat(KEY_UP)) console_history(console, -1);
            if (IsKeyPressed(KEY_DOWN) || IsKeyPressedRepeat(KEY_DOWN)) console_history(console, 1);
            if ((IsKeyDown(KEY_LEFT_CONTROL) || IsKeyDown(KEY_RIGHT_CONTROL)) && IsKeyPressed(KEY_V)) {
                const char *clipboard = GetClipboardText();
                if (clipboard) console_insert(console, clipboard);
            }
            if (IsKeyPressed(KEY_TAB)) console_complete(console);
            if (IsKeyPressed(KEY_PAGE_UP) || IsKeyPressedRepeat(KEY_PAGE_UP)) console->scroll += console_full ? 28 : 9;
            if (IsKeyPressed(KEY_PAGE_DOWN) || IsKeyPressedRepeat(KEY_PAGE_DOWN)) console->scroll -= console_full ? 28 : 9;
            console->scroll += (int)(GetMouseWheelMove() * 3);
            if (IsKeyPressed(KEY_ENTER)) command_failed = !console_submit(console, game);
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
        int old_buildings = game->building_kills;
        int old_shots = game->shots, old_kills = game->kills, old_hits = game->hits;
        int old_projectile_hits = game->projectile_hits, old_deaths = game->deaths;
        if (!console_open && !toggled && ((!was_menu && !game->paused) || game->menu.page == MENU_OVER || game->menu.page == MENU_CLEAR) && (IsWindowFocused() || frames)) {
            if (!frames) for (int i = 0; i < GAME_PLAYERS; ++i) game->players[i].input = poll_input(i);
            accumulator += frames ? 1.0 / GAME_HZ : fmin(GetFrameTime(), 0.1);
            while (accumulator >= 1.0 / GAME_HZ) {
                game_tick(game);
                accumulator -= 1.0 / GAME_HZ;
            }
        } else accumulator = 0;
        if (assets.audio) {
            if (game->shots > old_shots) PlaySound(assets.shoot);
            if (game->building_kills > old_buildings) PlaySound(assets.building_explode);
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
        if (console_open) draw_console(console, assets.console_font, command_failed, console_full);
        if (console->perf) draw_frame_graph(&frame_graph, assets.console_font);
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
        double work_end = GetTime();
        EndDrawing();
        frame_graph.work_ms[frame_graph.cursor] = (float)((work_end - frame_start) * 1000);
        frame_graph.frame_ms[frame_graph.cursor] = (float)((GetTime() - frame_start) * 1000);
        frame_graph.cursor = (frame_graph.cursor + 1) % FRAME_SAMPLES;
        if (frame_graph.count < FRAME_SAMPLES) ++frame_graph.count;
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
