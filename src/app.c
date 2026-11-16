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
    Texture2D menu[6], title, cloud, water, pilot_arrow;
    Texture2D shop[22], star;
    Texture2D weapon_icons[WEAPON_COUNT], weapon_textures[WEAPON_COUNT];
    Texture2D hostile_textures[HOSTILE_COUNT];
    Texture2D big_laser, big_flame, shield_overlay;
    Texture2D generator_icons[GENERATOR_COUNT], shield_icons[SHIELD_COUNT], item_none, sidekick;
    Texture2D stage_textures[STAGE_MAX_EVENTS], stage_destroyed[STAGE_MAX_EVENTS];
    Texture2D scenery[4];
    Texture2D boss4_cover, boss5_cover, boss6_center;
    Texture2D rotate_turret_base, splitter_right, pyramid_turret, cutter_beam;
    Texture2D enemy_corpses[ENEMY_KIND_COUNT];
    Texture2D pickups[3], medals[5], continue_button;
    int stage_texture_owner[STAGE_MAX_EVENTS], stage_index;
    Music music, title_music, pause_music;
    int music_mode, music_revision, music_event, music_tick;
    const char *music_name;
    Sound shoot, explode, building_explode, hit, shield_hit, shot_hit, die;
    Sound bonus_get;
    Sound weapon_fire[WEAPON_COUNT], impacts[4], enemy_fire[2], overheat;
    Sound menu_sounds[5];
    bool audio, stage_music_started;
    Font console_font, ui_font;
} Assets;

static const char *shop_texture_names[] = {
    "ShopPanel", "ShopItemPanel", "ShopUpgradePanel", "ShopPurchaseButtonPanel",
    "IconCategoryCursor", "IconItemCursor", "IconWeaponFrontLaser",
    "IconCategoryChassis", "IconCategoryGenerator", "IconCategoryShield",
    "IconCategoryWeaponPrimary", "IconCategoryWeaponSecondary", "IconCategoryWeaponSidekick",
    "IconChassisRookie", "IconChassisEagle", "IconChassisCrusher", "IconChassisInterceptor",
    "IconChassisPhoenix", "IconChassisLightning", "IconChassisDragon", "IconChassisDemon", "IconChassisAce"
};

static const char *weapon_texture_names[WEAPON_COUNT] = {
    "Laser", "Laser", "Laser", "Laser", "SmallPlasma", "BigPlasma", "Flame", "Flame", "Vulcan", "Bomblet",
    "Lightning", "Lightning", "Beam", "BeamFocus", "Missile", "SeekBomb", "Boomerang", "DrunkMissile",
    "MiniShot", "ChargeShot", "Blade", "DrunkMissile", "Missile", "Laser"
};

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
    for (int i = 0; i < 5; ++i) if (IsTextureValid(assets->medals[i])) UnloadTexture(assets->medals[i]);
    if (IsTextureValid(assets->continue_button)) UnloadTexture(assets->continue_button);
    for (int i = 0; i < 3; ++i) if (IsTextureValid(assets->pickups[i])) UnloadTexture(assets->pickups[i]);
    if (IsTextureValid(assets->boss4_cover)) UnloadTexture(assets->boss4_cover);
    if (IsTextureValid(assets->boss5_cover)) UnloadTexture(assets->boss5_cover);
    if (IsTextureValid(assets->boss6_center)) UnloadTexture(assets->boss6_center);
    for (int i = 0; i < 4; ++i) if (IsTextureValid(assets->scenery[i])) UnloadTexture(assets->scenery[i]);
    if (assets->console_font.texture.id && assets->console_font.texture.id != GetFontDefault().texture.id) UnloadFont(assets->console_font);
    if (assets->ui_font.texture.id && assets->ui_font.texture.id != GetFontDefault().texture.id) UnloadFont(assets->ui_font);
    for (int i = 0; i < GAME_CHASSIS; ++i) if (IsTextureValid(assets->ships[i])) UnloadTexture(assets->ships[i]);
    for (int i = 0; i < ENEMY_KIND_COUNT; ++i)
        if (IsTextureValid(assets->enemy_corpses[i])) UnloadTexture(assets->enemy_corpses[i]);
    if (IsTextureValid(assets->pyramid_turret)) UnloadTexture(assets->pyramid_turret);
    if (IsTextureValid(assets->cutter_beam)) UnloadTexture(assets->cutter_beam);
    if (IsTextureValid(assets->rotate_turret_base)) UnloadTexture(assets->rotate_turret_base);
    if (IsTextureValid(assets->splitter_right)) UnloadTexture(assets->splitter_right);
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
    for (int i = 0; i < 22; ++i) if (IsTextureValid(assets->shop[i])) UnloadTexture(assets->shop[i]);
    for (int i = 0; i < WEAPON_COUNT; ++i) {
        if (IsTextureValid(assets->weapon_icons[i])) UnloadTexture(assets->weapon_icons[i]);
        if (IsTextureValid(assets->weapon_textures[i])) UnloadTexture(assets->weapon_textures[i]);
    }
    for (int i = 0; i < HOSTILE_COUNT; ++i) if (IsTextureValid(assets->hostile_textures[i])) UnloadTexture(assets->hostile_textures[i]);
    for (int i = 0; i < GENERATOR_COUNT; ++i) if (IsTextureValid(assets->generator_icons[i])) UnloadTexture(assets->generator_icons[i]);
    for (int i = 0; i < SHIELD_COUNT; ++i) if (IsTextureValid(assets->shield_icons[i])) UnloadTexture(assets->shield_icons[i]);
    if (IsTextureValid(assets->item_none)) UnloadTexture(assets->item_none);
    if (IsTextureValid(assets->sidekick)) UnloadTexture(assets->sidekick);
    if (IsTextureValid(assets->big_laser)) UnloadTexture(assets->big_laser);
    if (IsTextureValid(assets->big_flame)) UnloadTexture(assets->big_flame);
    if (IsTextureValid(assets->shield_overlay)) UnloadTexture(assets->shield_overlay);
    if (IsTextureValid(assets->star)) UnloadTexture(assets->star);
    if (IsTextureValid(assets->water)) UnloadTexture(assets->water);
    if (IsTextureValid(assets->cloud)) UnloadTexture(assets->cloud);
    if (IsTextureValid(assets->pilot_arrow)) UnloadTexture(assets->pilot_arrow);
    for (int i = 0; i < stage_definitions[assets->stage_index].event_count; ++i) {
        if (assets->stage_texture_owner[i] != i) continue;
        if (IsTextureValid(assets->stage_textures[i])) UnloadTexture(assets->stage_textures[i]);
        if (IsTextureValid(assets->stage_destroyed[i])) UnloadTexture(assets->stage_destroyed[i]);
    }
    if (assets->audio) {
        for (int i = 0; i < WEAPON_COUNT; ++i) if (IsSoundValid(assets->weapon_fire[i])) UnloadSound(assets->weapon_fire[i]);
        for (int i = 0; i < 4; ++i) if (IsSoundValid(assets->impacts[i])) UnloadSound(assets->impacts[i]);
        for (int i = 0; i < 2; ++i) if (IsSoundValid(assets->enemy_fire[i])) UnloadSound(assets->enemy_fire[i]);
        if (IsSoundValid(assets->overheat)) UnloadSound(assets->overheat);
        if (IsSoundValid(assets->bonus_get)) UnloadSound(assets->bonus_get);
        if (IsMusicValid(assets->music)) UnloadMusicStream(assets->music);
        if (IsMusicValid(assets->title_music)) UnloadMusicStream(assets->title_music);
        if (IsMusicValid(assets->pause_music)) UnloadMusicStream(assets->pause_music);
        for (int i = 0; i < 5; ++i) if (IsSoundValid(assets->menu_sounds[i])) UnloadSound(assets->menu_sounds[i]);
        if (IsSoundValid(assets->shoot)) UnloadSound(assets->shoot);
        if (IsSoundValid(assets->building_explode)) UnloadSound(assets->building_explode);
        if (IsSoundValid(assets->explode)) UnloadSound(assets->explode);
        if (IsSoundValid(assets->hit)) UnloadSound(assets->hit);
        if (IsSoundValid(assets->shield_hit)) UnloadSound(assets->shield_hit);
        if (IsSoundValid(assets->shot_hit)) UnloadSound(assets->shot_hit);
        if (IsSoundValid(assets->die)) UnloadSound(assets->die);
        CloseAudioDevice();
    }
}

static bool stage_assets_load(Assets *assets, int index) {
    const SceneryDefinition *scene = &stage_scenery[index < 13 ? index : 13];
    bool valid = true;
    for (int i = 0; i < 4; ++i) {
        if (IsTextureValid(assets->scenery[i])) UnloadTexture(assets->scenery[i]);
        assets->scenery[i] = (Texture2D){0};
        if (!scene->layers[i].texture) continue;
        char path[160];
        snprintf(path, sizeof(path), "textures/%s.png", scene->layers[i].texture);
        assets->scenery[i] = texture_load(path);
        valid = valid && IsTextureValid(assets->scenery[i]);
    }
    for (int i = 0; i < stage_definitions[assets->stage_index].event_count; ++i) {
        if (assets->stage_texture_owner[i] != i) continue;
        if (IsTextureValid(assets->stage_textures[i])) UnloadTexture(assets->stage_textures[i]);
        if (IsTextureValid(assets->stage_destroyed[i])) UnloadTexture(assets->stage_destroyed[i]);
    }
    memset(assets->stage_textures, 0, sizeof(assets->stage_textures));
    memset(assets->stage_destroyed, 0, sizeof(assets->stage_destroyed));
    assets->stage_index = index;
    const StageDefinition *stage = &stage_definitions[index];
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
    return valid;
}

static bool assets_load(Assets *assets, Console *console, bool mute, float volume) {
    /* Solcraft's fixed-font preference: use the OS font, never copy Windows fonts. */
    int ui_glyphs[96];
    for (int i = 0; i < 95; ++i) ui_glyphs[i] = 32 + i;
    ui_glyphs[95] = 0x00a5;
    assets->ui_font = LoadFontEx(asset_path("fonts/NotoSans-Regular.ttf"), 64, ui_glyphs, 96);
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
        if (i == ENEMY_BOSS8_COLLISION) continue;
        char path[128];
        snprintf(path, sizeof(path), "textures/%s.png", i == ENEMY_SPLITTER_FRAGMENT ? "SplitterFragmentLeft" : enemy_names[i]);
        assets->enemies[i] = texture_load(path);
    }
    for (int i = 0; i < ENEMY_KIND_COUNT; ++i) {
        const char *name = enemy_corpse_name((EnemyKind)i);
        if (!name) continue;
        char path[128];
        snprintf(path, sizeof(path), "textures/%s.png", name);
        assets->enemy_corpses[i] = texture_load(path);
    }
    static const char *medals[] = {"Clear", "Bronze", "Silver", "Gold", "Platinum"};
    for (int i = 0; i < 5; ++i) {
        char path[128];
        snprintf(path, sizeof(path), "textures/Medal%s.png", medals[i]);
        assets->medals[i] = texture_load(path);
        if (!IsTextureValid(assets->medals[i])) return false;
    }
    assets->continue_button = texture_load("textures/xboxControllerButtonX.png");
    if (!IsTextureValid(assets->continue_button)) return false;
    assets->pyramid_turret = texture_load("textures/PyramidTurret.png");
    assets->cutter_beam = texture_load("textures/CutterBeam.png");
    assets->rotate_turret_base = texture_load("textures/RotateTurretBase.png");
    assets->splitter_right = texture_load("textures/SplitterFragmentRight.png");
    assets->shot = texture_load("textures/Laser.png");
    assets->pickups[0] = texture_load("textures/Bonus.png");
    assets->pickups[1] = texture_load("textures/BigBonus.png");
    assets->pickups[2] = texture_load("textures/Powerup.png");
    assets->boss4_cover = texture_load("textures/Boss4Cover.png");
    assets->boss5_cover = texture_load("textures/Boss5Cover.png");
    assets->boss6_center = texture_load("textures/Boss6Center.png");
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
    assets->water = texture_load("textures/Water.png");
    assets->cloud = texture_load("textures/Cloud.png");
    assets->pilot_arrow = texture_load("textures/PilotSelectArrow.png");
    const char *menu_textures[] = {"MenuItem", "MenuItemSmall", "MenuItemSelected", "MenuItemSelectedSmall", "MenuItemInvalid", "MenuItemInvalidSelected"};
    for (int i = 0; i < 6; ++i) {
        char path[128];
        snprintf(path, sizeof(path), "textures/%s.png", menu_textures[i]);
        assets->menu[i] = texture_load(path);
    }
    bool valid = IsTextureValid(assets->pyramid_turret) && IsTextureValid(assets->cutter_beam) && IsTextureValid(assets->rotate_turret_base) && IsTextureValid(assets->splitter_right) && IsTextureValid(assets->shot) && IsTextureValid(assets->hostile_shot);
    for (int i = 0; i < GAME_CHASSIS; ++i) valid = valid && IsTextureValid(assets->ships[i]);
    valid = valid && IsTextureValid(assets->particle_dot) && IsTextureValid(assets->particle_triangle) && IsTextureValid(assets->energy);
    for (int i = 0; i < ENEMY_KIND_COUNT; ++i)
        if (i != ENEMY_BOSS8_COLLISION) valid = valid && IsTextureValid(assets->enemies[i]);
    for (int i = 0; i < ENEMY_KIND_COUNT; ++i)
        if (enemy_corpse_name((EnemyKind)i)) valid = valid && IsTextureValid(assets->enemy_corpses[i]);
    Texture2D hud[] = {assets->panel_background, assets->panel_frames, assets->armor, assets->armor_icon,
        assets->energy_icon, assets->shield_icon, assets->portraits[0], assets->portraits[1]};
    for (int i = 0; i < 8; ++i) valid = valid && IsTextureValid(hud[i]);
    for (int i = 0; i < 6; ++i) valid = valid && IsTextureValid(assets->menu[i]);
    valid = valid && IsTextureValid(assets->water) && IsTextureValid(assets->title) && IsTextureValid(assets->cloud) &&
        IsTextureValid(assets->pilot_arrow) && IsTextureValid(assets->portraits[2]);
    for (int i = 0; i < 22; ++i) {
        char path[160];
        snprintf(path, sizeof(path), "textures/shop/%s.png", shop_texture_names[i]);
        assets->shop[i] = texture_load(path);
        valid = IsTextureValid(assets->shop[i]) && valid;
    }
    assets->star = texture_load("textures/Star.png");
    static const char *hostile_names[] = {"EnemyShot", "EnemyLaser", "EnemyMissile", "EnemyCannonShot", "EnemyPellet", "EnemyMiniShot"};
    assets->sidekick = texture_load("textures/player/Sidekick.png");
    assets->shield_overlay = texture_load("textures/effects/PlayerShieldHit.png");
    assets->big_laser = texture_load("textures/weapons/BigLaser.png");
    assets->big_flame = texture_load("textures/weapons/BigFlame.png");
    valid = valid && IsTextureValid(assets->sidekick) && IsTextureValid(assets->shield_overlay) &&
        IsTextureValid(assets->big_laser) && IsTextureValid(assets->big_flame);
    for (int i = 0; i < HOSTILE_COUNT; ++i) {
        char path[160];
        snprintf(path, sizeof(path), "textures/weapons/%s.png", hostile_names[i]);
        assets->hostile_textures[i] = texture_load(path);
        valid = IsTextureValid(assets->hostile_textures[i]) && valid;
    }
    for (int i = 0; i < WEAPON_COUNT; ++i) {
        char path[160];
        snprintf(path, sizeof(path), "textures/weapons/%s.png", weapon_texture_names[i]);
        assets->weapon_textures[i] = texture_load(path);
        valid = IsTextureValid(assets->weapon_textures[i]) && valid;
        if (equipment_weapon_category(i) >= 0) {
            snprintf(path, sizeof(path), "textures/shop/IconWeapon%s.png", game_weapons[i].name);
            assets->weapon_icons[i] = texture_load(path);
            valid = IsTextureValid(assets->weapon_icons[i]) && valid;
        }
    }
    for (int i = 0; i < GENERATOR_COUNT; ++i) {
        char path[160];
        snprintf(path, sizeof(path), "textures/shop/%s.png", game_generators[i].icon);
        assets->generator_icons[i] = texture_load(path);
        valid = IsTextureValid(assets->generator_icons[i]) && valid;
    }
    for (int i = 0; i < SHIELD_COUNT; ++i) {
        char path[160];
        snprintf(path, sizeof(path), "textures/shop/%s.png", game_shields[i].icon);
        assets->shield_icons[i] = texture_load(path);
        valid = IsTextureValid(assets->shield_icons[i]) && valid;
    }
    assets->item_none = texture_load("textures/shop/IconItemNone.png");
    valid = IsTextureValid(assets->item_none) && valid;
    valid = IsTextureValid(assets->star) && stage_assets_load(assets, 0) && valid;
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
    assets->shield_hit = LoadSound(asset_path("audio/PlayerShieldHit.wav"));
    assets->shot_hit = LoadSound(asset_path("audio/WeaponHitLaser.wav"));
    assets->die = LoadSound(asset_path("audio/PlayerDie.wav"));
    assets->bonus_get = LoadSound(asset_path("audio/BonusGet.wav"));
    for (int i = 0; i < WEAPON_COUNT; ++i) {
        if (!game_weapons[i].sound || !game_weapons[i].sound[0]) continue;
        char path[128];
        snprintf(path, sizeof(path), "audio/%s.wav", game_weapons[i].sound);
        assets->weapon_fire[i] = LoadSound(asset_path(path));
        if (!IsSoundValid(assets->weapon_fire[i])) return false;
    }
    const char *impacts[] = {"WeaponHitLaser", "WeaponHitMiniShot", "WeaponHitMissile", "WeaponHitSeekBomb"};
    for (int i = 0; i < 4; ++i) {
        char path[128];
        snprintf(path, sizeof(path), "audio/%s.wav", impacts[i]);
        assets->impacts[i] = LoadSound(asset_path(path));
        if (!IsSoundValid(assets->impacts[i])) return false;
    }
    assets->enemy_fire[0] = LoadSound(asset_path("audio/EnemyShoot.wav"));
    assets->enemy_fire[1] = LoadSound(asset_path("audio/EnemyCannonShoot.wav"));
    assets->overheat = LoadSound(asset_path("audio/WeaponOverheat.wav"));
    if (!IsMusicValid(assets->music) || !IsMusicValid(assets->title_music) || !IsMusicValid(assets->pause_music) || !IsSoundValid(assets->shoot) ||
        !IsSoundValid(assets->explode) || !IsSoundValid(assets->building_explode) || !IsSoundValid(assets->hit) || !IsSoundValid(assets->shield_hit) || !IsSoundValid(assets->shot_hit) || !IsSoundValid(assets->die)) {
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
    SetSoundVolume(assets->shield_hit, 0.45f);
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
    text_center(assets->ui_font, TextFormat("%d", game->menu.page == MENU_SHOP ? player->money : player->score), x + 242, 154, 32, dim);
    sprite(assets->portraits[player->pilot], (float)x + 238, 356, 256, 0, WHITE);
    DrawTexture(assets->energy_icon, x + 70, 722, WHITE);
    DrawTexturePro(assets->energy, (Rectangle){0, 0, 256, 64},
        (Rectangle){(float)x + 140, 716, 281.6f * player->energy / game_generators[player->generator].capacity, 73.6f}, (Vector2){0}, 0, WHITE);
    DrawTexture(assets->shield_icon, x + 70, 805, WHITE);
    if (player->shield >= 0) DrawTexturePro(assets->energy, (Rectangle){0, 0, 256, 64},
        (Rectangle){(float)x + 140, 800, 281.6f * player->shield_health / game_shields[player->shield].capacity, 73.6f}, (Vector2){0}, 0, SKYBLUE);
    DrawTexture(assets->armor_icon, x + 70, 881, WHITE);
    DrawTexturePro(assets->armor, (Rectangle){0, 0, 256, 64},
        (Rectangle){(float)x + 140, 876, 281.6f * player->health / game_chassis[player->chassis].armor, 73.6f}, (Vector2){0}, 0, WHITE);
}

static void draw_scenery(const Game *game, const Assets *assets, bool foreground) {
    const SceneryDefinition *scene = &stage_scenery[game->stage.index < 13 ? game->stage.index : 13];
    if (!foreground) {
        Color top = {scene->top[0], scene->top[1], scene->top[2], 255};
        Color bottom = {scene->bottom[0], scene->bottom[1], scene->bottom[2], 255};
        DrawRectangleGradientV(ARENA_X, 0, GAME_WIDTH, GAME_HEIGHT, top, bottom);
        if (game->stage.index == 6 || game->stage.index == 12) {
            /* SceneryPresets: TempestNebulaBG and BonusStage1, TL/TR/BR/BL. */
            static const Color nebula[4][4] = {
                {{0,32,0,16},{0,32,0,16},{0,0,0,16},{0,0,0,16}},
                {{0,32,0,16},{0,32,0,16},{0,32,0,16},{0,32,0,16}},
                {{0,32,0,16},{0,32,0,16},{0,32,0,16},{0,32,0,16}},
                {{0,0,0,0},{0,0,0,0},{0,32,0,16},{0,32,0,16}}
            };
            static const Color bonus[] = {{0,196,0,128},{0,64,0,64},{0,0,0,0},{0,64,0,64}};
            int frame=game->tick>0?game->tick-1:0, cycle=frame/60%4;
            float t=frame%60/60.0f; t=t*t*(3-2*t);
            Color corners[4];
            for(int i=0;i<4;++i) {
                Color a=game->stage.index==6?nebula[cycle][i]:bonus[(i-cycle+4)%4];
                Color b=game->stage.index==6?nebula[(cycle+1)%4][i]:bonus[(i-cycle+3)%4];
                corners[i]=(Color){a.r+(b.r-a.r)*t,a.g+(b.g-a.g)*t,a.b+(b.b-a.b)*t,a.a+(b.a-a.a)*t};
            }
            DrawRectangleGradientEx((Rectangle){ARENA_X,0,GAME_WIDTH,GAME_HEIGHT},corners[0],corners[3],corners[2],corners[1]);
        }
    }
    for (int layer = 0; layer < 4; ++layer) {
        const SceneryLayer *definition = &scene->layers[layer];
        if (definition->foreground != foreground) continue;
        for (int i = 0; i < game->stage.scenery_count[layer]; ++i) {
            const SceneryParticle *p = &game->stage.scenery[layer][i];
            Texture2D texture = assets->scenery[layer];
            sprite(texture, ARENA_X - game->stage.camera_x + p->x, p->y, texture.width * definition->scale, 0, WHITE);
        }
    }
}

static void draw_enemies(const Game *game, const Assets *assets, int layer) {
    float view_x = ARENA_X - game->stage.camera_x;
    if (layer == 0) for (int i = 0; i < STAGE_MAX_EVENTS; ++i) {
        const EnemyCorpse *corpse = &game->stage.corpses[i];
        if (!corpse->active) continue;
        Texture2D texture = assets->enemy_corpses[corpse->kind];
        float y = corpse->y - game->stage.camera_y + GAME_HEIGHT * 0.5f;
        sprite(texture, view_x + corpse->x, y, (float)texture.width, 0, WHITE);
    }
    for (int i = 0; i < GAME_ENEMIES; ++i) {
        const Enemy *enemy = &game->enemies[i];
        if (!enemy->active || enemy->invisible || enemy->kind == ENEMY_BOSS8_COLLISION) continue;
        if (enemy->kind == ENEMY_CUTTER && layer == -1)
            sprite(assets->cutter_beam, view_x + enemy->x, enemy->y, (float)assets->cutter_beam.width, enemy->orbit_angle * RAD2DEG, WHITE);
        if (enemy->kind == ENEMY_PYRAMID && layer == 1)
            sprite(assets->pyramid_turret, view_x + enemy->x, enemy->y, (float)assets->pyramid_turret.width, (enemy->aim - PI/2) * RAD2DEG, WHITE);
        if (enemy->kind == ENEMY_ROTATE_TURRET && layer == 0)
            sprite(assets->rotate_turret_base, view_x + enemy->x, enemy->y, (float)assets->rotate_turret_base.width, 0, WHITE);
        Texture2D cover = enemy->kind == ENEMY_BOSS4 && enemy->health > enemy->health_max * 0.6f ? assets->boss4_cover :
            enemy->kind == ENEMY_BOSS5 ? assets->boss5_cover : enemy->kind == ENEMY_BOSS6 ? assets->boss6_center : (Texture2D){0};
        float cover_rotation = enemy->kind == ENEMY_BOSS5 ? 90 + enemy->orbit_angle * RAD2DEG :
            enemy->kind == ENEMY_BOSS6 ? enemy->rotation * RAD2DEG : 0;
        int cover_layer = enemy->kind == ENEMY_BOSS6 ? -1 : 2;
        if (layer == cover_layer && IsTextureValid(cover)) sprite(cover, view_x + enemy->x, enemy->y, (float)cover.width, cover_rotation, WHITE);
        int depth = enemy->kind == ENEMY_ROTATE_TURRET ? 1 : enemy_draw_layer(enemy->kind);
        if (depth != layer) continue;
        Texture2D texture = enemy->kind == ENEMY_SPLITTER_FRAGMENT && enemy->aux_index ?
            assets->splitter_right : assets->enemies[enemy->kind];
        float rotation = enemy->kind == ENEMY_BOSS6 ? 0 : enemy->rotation;
        Rectangle source = {0, 0, (float)texture.width, (float)texture.height};
        if (enemy->kind == ENEMY_SPIKE) { source.width /= 2; source.x = (enemy->age / 20 % 2) * source.width; }
        if (enemy->kind == ENEMY_FENCE_BEAM) { source.height /= 4; source.y = (enemy->age / 4 % 4) * source.height; }
        float scale = enemy->kind == ENEMY_GLOB ? 1 + sinf(game->tick * (enemy->attack ? 0.3f : 0.1f)) * (enemy->attack ? 0.08f : 0.04f) : 1;
        if (enemy->kind == ENEMY_BOSS10_BLOCK) scale = enemy->orbit_radius;
        Vector2 origin = {source.width * 0.5f * scale, source.height * 0.5f * scale};
        if (enemy->kind == ENEMY_BOSS4_CHUNK) origin.x = 0;
        if (enemy->kind == ENEMY_BOSS7_CANNON_BIG || enemy->kind == ENEMY_BOSS7_CANNON_SMALL) origin.y = 0;
        if (enemy->kind == ENEMY_BOSS12_EYE) origin.x = (float)texture.width;
        DrawTexturePro(texture, source,
            (Rectangle){view_x + enemy->x,enemy->y,source.width * scale,source.height * scale}, origin, rotation * RAD2DEG, WHITE);

    }
}

static Game shop_preview;

static void draw_combat_sprites(const Game *game, const Assets *assets) {
    float view_x = ARENA_X - game->stage.camera_x;
    for (int i = 0; i < GAME_PLAYERS; ++i) {
        const Player *player = &game->players[i];
        if (!player->joined || player->lives <= 0) continue;
        Color color = player_colors[i];
        if (player->invulnerable && game->tick % 8 < 4) color = Fade(color, 0.4f);
        sprite(assets->ships[player->chassis], view_x + player->x, player->y, 80, -90, color);
        if (player->sidekick >= 0) for (int side = -1; side <= 1; side += 2)
            sprite(assets->sidekick, view_x + player->x + side * 40, player->y, assets->sidekick.width, 0, color);
        if (player->shield_hit_frames > 0)
            sprite(assets->shield_overlay, view_x + player->x, player->y, assets->shield_overlay.width * 1.25f,
                -90, Fade(player_colors[i], 128.0f / 255));
    }
    draw_enemies(game, assets, ENEMY_LAYER_PLAYER_OVERLAY);
    for (int i = 0; i < GAME_BULLETS; ++i) {
        const Bullet *bullet = &game->bullets[i];
        if (!bullet->active || bullet->splash) continue;
        Color color = bullet->owner < 0 ? WHITE : player_colors[bullet->owner];
        Texture2D texture = bullet->owner < 0 ? assets->hostile_textures[bullet->hostile_kind] : assets->weapon_textures[bullet->weapon];
        if (bullet->owner >= 0) {
            if ((bullet->weapon == WEAPON_FRONT_LASER || bullet->weapon == WEAPON_FRONT_LASER_FOCUS ||
                 bullet->weapon == WEAPON_SPREAD_LASER || bullet->weapon == WEAPON_SPREAD_LASER_FOCUS ||
                 bullet->weapon == WEAPON_LASER_SHOT) && bullet->base_damage >= 0.4f) texture = assets->big_laser;
            if ((bullet->weapon == WEAPON_FLAME || bullet->weapon == WEAPON_FLAME_FOCUS) && bullet->base_damage >= 1)
                texture = assets->big_flame;
        }
        if (bullet->owner >= 0 && bullet->weapon == WEAPON_BEAM) {
            DrawTexturePro(texture, (Rectangle){0,0,(float)texture.width,(float)texture.height},
                (Rectangle){view_x + bullet->x, bullet->y - 540, 1080, bullet->width * 2},
                (Vector2){540, bullet->width}, -90, color);
        } else {
            float width = texture.width * (bullet->owner < 0 ? 1 : bullet->scale);
            if (bullet->owner >= 0 && bullet->weapon == WEAPON_BEAM_FOCUS) width = bullet->radius / 0.46f * 2;
            float rotation = bullet->owner < 0 && bullet->hostile_kind != HOSTILE_PELLET ? atan2f(bullet->vy, bullet->vx) : bullet->rotation;
            if (bullet->owner >= 0 && bullet->weapon == WEAPON_BLADE && bullet->offset_x < 0) {
                float height = width * texture.height / texture.width;
                DrawTexturePro(texture, (Rectangle){0, 0, texture.width, -texture.height},
                    (Rectangle){view_x + bullet->x, bullet->y, width, height}, (Vector2){width * 0.5f, height * 0.5f}, rotation * RAD2DEG, color);
            } else sprite(texture, view_x + bullet->x, bullet->y, width, rotation * RAD2DEG, color);
        }
    }
    draw_enemies(game, assets, ENEMY_LAYER_WEAPON_OVERLAY);
    for (int i = 0; i < GAME_EFFECTS; ++i) {
        const Effect *effect = &game->effects[i];
        if (effect->life <= 0) continue;
        Texture2D texture = effect->kind ? assets->particle_triangle : assets->particle_dot;
        sprite(texture, view_x + effect->x, effect->y, texture.width * effect->scale,
            effect->angle, (Color){effect->r, effect->g, effect->b, (unsigned char)(fminf(1, fmaxf(0, effect->alpha)) * 255)});
    }
}

static void draw_game(const Game *game, const Assets *assets) {
    float view_x = ARENA_X - game->stage.camera_x;
    ClearBackground(background);
    DrawRectangle(ARENA_X, 0, GAME_WIDTH, GAME_HEIGHT, background);
    BeginScissorMode(ARENA_X, 0, GAME_WIDTH, GAME_HEIGHT);
    draw_scenery(game, assets, false);
    /* Legacy decoration depth offsets precede the main carrier tiles. */
    const StageDefinition *stage = &stage_definitions[game->stage.index];
    int order[STAGE_MAX_EVENTS], count=0;
    for(int i=0;i<stage->event_count;++i) {
        const StageEvent *event=&stage->events[i];
        if(!game->stage.activated[i] || (event->kind!=STAGE_DECORATION && event->kind!=STAGE_BUILDING)) continue;
        float depth=event->kind==STAGE_BUILDING?0.15f:0.10f+event->depth;
        int at=count;
        while(at>0) {
            const StageEvent *previous=&stage->events[order[at-1]];
            float previous_depth=previous->kind==STAGE_BUILDING?0.15f:0.10f+previous->depth;
            if(previous_depth<=depth)break;
            order[at]=order[at-1]; --at;
        }
        order[at]=i; ++count;
    }
    for (int row = 0; row < count; ++row) {
        int i=order[row];
        const StageEvent *event = &stage->events[i];
        if (!game->stage.activated[i]) continue;
        if (event->kind != STAGE_DECORATION && event->kind != STAGE_BUILDING) continue;
        float y = event->y - game->stage.camera_y + GAME_HEIGHT * 0.5f;
        if (y < -event->height || y > GAME_HEIGHT + event->height) continue;
        Texture2D texture = event->kind == STAGE_BUILDING && game->stage.building_health[i] < 0 ?
            assets->stage_destroyed[i] : assets->stage_textures[i];
        sprite(texture, view_x + GAME_WIDTH * 0.5f + event->x, y, (float)event->width, event->rotation, WHITE);
    }
    for (int layer = -2; layer <= 2; ++layer) draw_enemies(game, assets, layer);
    draw_enemies(game, assets, ENEMY_LAYER_ENTITY);
    for (int i = 0; i < GAME_PICKUPS; ++i) {
        const Pickup *p = &game->pickups[i];
        if (!p->active) continue;
        Texture2D texture = assets->pickups[p->kind];
        sprite(texture, view_x + p->x, p->y, (float)texture.width, p->age * 0.1f * RAD2DEG, WHITE);
    }
    draw_combat_sprites(game, assets);
    draw_scenery(game, assets, true);
    if (game->stage.secret_countdown)
        DrawRectangle(ARENA_X, 0, GAME_WIDTH, GAME_HEIGHT, Fade(BLACK, fminf(1, game->stage.secret_countdown / 120.0f)));
    if (game->secret_finish_frames || game->secret_fade_frames) {
        float alpha = game->secret_fade_frames ? game->secret_fade_frames / 15.0f :
            game->secret_finish_frames <= 30 ? fminf(0.5f, (game->secret_finish_frames - 1) / 15.0f) :
            fminf(1, 0.5f + (game->secret_finish_frames - 30) / 15.0f);
        DrawRectangle(ARENA_X, 0, GAME_WIDTH, GAME_HEIGHT, Fade(BLACK, alpha));
    }
    EndScissorMode();
    for (int i = 0; i < GAME_ENEMIES; ++i) {
        const Enemy *enemy = &game->enemies[i];
        if (!enemy->active || !enemy_is_boss(enemy->kind)) continue;
        DrawRectangle(ARENA_X + 170, 118, 620, 12, (Color){68, 45, 45, 255});
        DrawRectangle(ARENA_X + 170, 118, (int)(620 * enemy->health / enemy->health_max), 12, danger);
    }
    draw_player_panel(game, 0, assets);
    draw_player_panel(game, 1, assets);
}

static bool title_menu(const Game *game) {
    MenuPage page = game->menu.page;
    return page == MENU_MAIN || page == MENU_NEW_GAME || page == MENU_DIFFICULTY || page == MENU_PILOT ||
        ((page == MENU_OPTIONS || page == MENU_QUIT) && game->menu.parent == MENU_MAIN);
}

static void draw_shop(const Game *game, const Assets *assets, int frame) {
    ClearBackground(BLACK);
    for (int layer = 0; layer < 2; ++layer) for (int i = 0; i < 10; ++i) {
        float x = ARENA_X + (i * 193 + layer * 311) % GAME_WIDTH;
        float y = fmodf(i * 137.0f + frame * (layer ? 2.1f : 3.5f), GAME_HEIGHT);
        sprite(assets->star, x, y, assets->star.width * (layer ? 0.6f : 0.4f), 0, WHITE);
    }
    draw_player_panel(game, 0, assets);
    draw_player_panel(game, 1, assets);
    text_center(assets->ui_font, "Welcome to Randy's Random Item Shop", 960, 50, 28, WHITE);
    text_center(assets->ui_font, "Press TAB to proceed", 960, 104, 24, dim);
    static const char *category_names[] = {"Chassis", "Generator", "Shield", "Main Weapon", "Support Weapon", "Sidekick"};
    static const char *levels[] = {"I", "II", "III", "IV", "V", "VI", "VII", "VIII"};
    for (int slot = 0; slot < GAME_PLAYERS; ++slot) {
        const Player *p = &game->players[slot];
        if (!p->joined) continue;
        int category = game->menu.shop_category[slot], item = game->menu.shop_item[slot];
        bool items = game->menu.shop_items[slot], ready = game->menu.ready[slot];
        float menu_x = CANVAS_WIDTH * (slot ? 0.707f : 0.291f), menu_y = CANVAS_HEIGHT * 0.351f;
        float panel_x = menu_x - CANVAS_WIDTH * (slot ? 0.047f : 0.065f), panel_y = menu_y - CANVAS_HEIGHT * 0.060f;
        DrawTexturePro(assets->shop[0], (Rectangle){0, 0, slot ? -220 : 220, 620},
            (Rectangle){panel_x, panel_y, 220, 620}, (Vector2){0}, 0, WHITE);
        for (int c = 0; c < 6; ++c)
            sprite(assets->shop[7 + c], menu_x, menu_y + c * 96, 96, 0, ready ? dim : WHITE);
        if (!ready) sprite(assets->shop[4], menu_x, menu_y + category * 96, 96, 0, WHITE);
        if (items && !ready) {
            int available[WEAPON_COUNT + 1], selected = 0;
            int count=game_shop_stock(game,slot,category,available);
            for(int i=0;i<count;++i)if(available[i]==item)selected=i;
            int first = selected > 5 ? selected - 5 : 0;
            int shown = count - first < 7 ? count - first : 7;
            float item_x = menu_x + (slot ? -134 : 134);
            float item_y = menu_y;
            DrawTexturePro(assets->shop[1], (Rectangle){(float)(shown - 1) * 128, 0, 128, 672},
                (Rectangle){item_x - 70, item_y - 58, 128, 672}, (Vector2){0}, 0, WHITE);
            for (int row = 0; row < shown; ++row) {
                int c = available[first + row];
                Texture2D icon = c < 0 ? assets->item_none : category == 0 ? assets->shop[13 + c] :
                    category == 1 ? assets->generator_icons[c] : category == 2 ? assets->shield_icons[c] : assets->weapon_icons[c];
                float y = item_y + row * 80;
                sprite(icon, item_x, y, 80, 0, WHITE);
                if (item == c) sprite(assets->shop[5], item_x, y, 80, 0, WHITE);
            }
        }
        float info_x = CANVAS_WIDTH * (slot ? 0.765f : 0.020f), info_y = CANVAS_HEIGHT * 0.400f;
        DrawTexture(assets->shop[2], (int)info_x, (int)info_y, WHITE);
        const char *name = category_names[category];
        if (items) name = item < 0 ? "None" : category == 0 ? game_chassis[item].name :
            category == 1 ? game_generators[item].name : category == 2 ? game_shields[item].name : game_weapons[item].display_name;
        text_center(assets->ui_font, name, (int)info_x + 207, (int)info_y + 22, 24, WHITE);
        if (items) {
            int cost = game_shop_cost(game, slot);
            Color color = cost <= p->money ? WHITE : dim;
            text_center(assets->ui_font, TextFormat("Cost: %d", cost), (int)info_x + 207, (int)info_y + 72, 24, color);
            text_center(assets->ui_font, TextFormat("Money: %d", p->money - cost), (int)info_x + 207, (int)info_y + 112, 24, color);
            if (category >= 3 && item >= 0) {
                int level = game->menu.shop_level[slot];
                text_center(assets->ui_font, TextFormat("<  Mk %s / %s  >", levels[level], levels[game_weapons[item].level_count - 1]),
                    (int)info_x + 207, (int)info_y + 160, 24, WHITE);
            }
            if (category == 1) text_center(assets->ui_font, TextFormat("Energy %.1f   Regen %.2f", game_generators[item].capacity, game_generators[item].regen),
                (int)info_x + 207, (int)info_y + 160, 22, WHITE);
            if (category == 2) text_center(assets->ui_font, TextFormat("Shield %.1f   Efficiency %.0f%%", game_shields[item].capacity, game_shields[item].efficiency * 100),
                (int)info_x + 207, (int)info_y + 160, 22, WHITE);
            text_center(assets->ui_font, "Up/Down: item   Left/Right: level", (int)info_x + 207, (int)info_y + 220, 19, dim);
            text_center(assets->ui_font, "Enter: purchase   Esc: back", (int)info_x + 207, (int)info_y + 252, 20, WHITE);
            if (cost > p->money) text_center(assets->ui_font, "Not enough money", (int)info_x + 207, (int)info_y + 300, 23, dim);
            DrawTexture(assets->shop[3], (int)menu_x - 54, (int)menu_y + 554, WHITE);
            text_center(assets->ui_font, "Purchase", (int)menu_x + 58, (int)menu_y + 580, 24, color);
        } else text_center(assets->ui_font, "Up/Down: category   Enter: browse", (int)info_x + 207, (int)info_y + 80, 20, WHITE);
    }
    BeginScissorMode(ARENA_X, 0, GAME_WIDTH, GAME_HEIGHT);
    draw_combat_sprites(&shop_preview, assets);
    EndScissorMode();
}

static void draw_menu(const Game *game, const Assets *assets, int frame) {
    if (game->menu.page == MENU_NONE) return;
    if (game->menu.page == MENU_FINISH) {
        static const char *lines[] = {
            "",
            "",
            "AS THE EVIL KURGLON EMPIRE",
            "RECEIVED DEFEAT",
            "",
            "",
            "OUR VALLIANT HERO COULD",
            "ENJOY TRIUMPH FROM THE",
            "PLANET POPULATION",
            "",
            "",
            "THE TASTE OF FREEDOM AND PEACE",
            "COULD IT LAST FOREVER?",
            "",
            "",
            "THE TALE OF YOUR TREMENDOUS",
            "VICTORY OVER THE KURGLON EMPIRE",
            "SHALL BE PASSED DOWN FROM",
            "GENERATION TO GENERATION",
            "",
            "",
            "AS THE HERO WHO BROUGHT PEACE",
            "PEACE TO FANTORA-K",
            "",
            "",
            "EVEN THOUGH THE WAR HAS BEEN OVER",
            "THE SCAR OF DECIMATED POPULATION",
            "IS REMEMBERED",
            "",
            "",
            "ONLY CONTINUED PEACE WILL",
            "ADVENGE IT",
            "",
            "",
            "UNTIL A TRUE HERO CAN PERFORM....."
        };
        ClearBackground(BLACK);
        for (int layer = 0; layer < 2; ++layer) for (int i = 0; i < 10; ++i) {
            float x = (float)((i * 193 + layer * 311) % CANVAS_WIDTH);
            float y = fmodf(i * 137.0f + game->menu.frame * (layer ? 2.1f : 3.5f), GAME_HEIGHT);
            sprite(assets->star, x, y, (float)assets->star.width, 0, WHITE);
        }
        for (unsigned int i = 0; i < sizeof(lines) / sizeof(lines[0]); ++i)
            text_center(assets->ui_font, lines[i], 960, 1080 - game->menu.frame * 0.7f + i * 50, 32, WHITE);
        return;
    }
    if (game->menu.page == MENU_SHOP) { draw_shop(game, assets, frame); return; }
    if (game->menu.page == MENU_STAGE_SELECT) {
        ClearBackground(BLACK);
        for (int i = 0; i < 20; ++i) {
            float x = (float)((i * 193) % CANVAS_WIDTH);
            float y = fmodf(i * 137.0f + frame * (i % 2 ? 14 : 18), GAME_HEIGHT);
            sprite(assets->star, x, y, (float)assets->star.width, 0, WHITE);
        }
    }
    if (game->menu.page == MENU_CLEAR) {
        float alpha = game->clear_exit ? fminf(1, 0.5f + (game->menu.frame - 360) / 60.0f) :
            fminf(0.5f, game->menu.frame / 60.0f);
        DrawRectangle(ARENA_X, 0, GAME_WIDTH, GAME_HEIGHT, Fade(BLACK, alpha));
        int counter = game->clear_exit ? 440 : game->menu.frame;
        if (counter >= 60) {
            DrawRectangle(745, 115, 450, 570, (Color){76,76,76,255});
            DrawRectangle(755, 125, 430, 550, (Color){30,30,30,255});
            text_center(assets->ui_font, "Stage Clear", 968, 180, 40, WHITE);
        }
        const StageStats *stats = &game->stage.stats;
        int percent[] = {game_stats_percent(stats->coins_collected, stats->coins_total),
            game_stats_percent(stats->enemy_kills, stats->enemy_total),
            game_stats_percent(stats->building_kills, stats->building_total), 0};
        percent[3] = percent[0] + percent[1] + percent[2];
        static const char *labels[] = {"Coins", "Enemies", "Buildings", "Total", "Stage Clear", "Skill Bonus"};
        static const int times[] = {100,120,140,180,240,260};
        static const int bonus[] = {0,0,250,500,1000,1500};
        int medal = game_stage_medal(game);
        for (int i = 0; i < 6; ++i) {
            if (counter < times[i] || (i == 5 && medal == 1)) continue;
            int y = i < 4 ? 280 + i * 30 : 460 + (i - 4) * 30;
            float width = MeasureTextEx(assets->ui_font, labels[i], 26, 0).x;
            DrawTextEx(assets->ui_font, labels[i], (Vector2){960 - width, (float)y}, 26, 0, WHITE);
            const char *value = i < 4 ? TextFormat("%d%%", percent[i]) :
                TextFormat("+%d\xc2\xa5", i == 4 ? 10000 : bonus[medal]);
            DrawTextEx(assets->ui_font, value, (Vector2){976, (float)y}, 26, 0, i < 4 ? WHITE : GREEN);
        }
        if (counter >= 300) {
            Texture2D texture = assets->medals[medal - 1];
            float scale = fmaxf(1, 2 - (counter - 299) * 0.125f);
            sprite(texture, 968, 540, texture.width * scale, 0, WHITE);
        }
        if (counter >= 360) {
            DrawTextEx(assets->ui_font, "Continue", (Vector2){908,630}, 26, 0, LIGHTGRAY);
            sprite(assets->continue_button, 888, 630, assets->continue_button.width, 0, WHITE);
        }
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
        int y = game->menu.page == MENU_STAGE_SELECT ? 150 + row * 66 : 400 + row * 66;
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
    if (before.page == MENU_SHOP) valid = before.shop_items[slot] ?
        game_shop_cost(game, slot) <= game->players[slot].money :
        true;
    game_menu_input(game, action, slot);
    if (!assets->audio) return;
    int sound = -1;
    if (action == MENU_BACK) sound = 2;
    else if (action == MENU_CONFIRM) sound = valid ? 1 : 2;
    else if (before.page == MENU_SHOP && action == MENU_READY) sound = game->stage.index == 1 && game->menu.page == MENU_NONE ? 1 : 0;
    else if (before.page == MENU_SHOP && (before.shop_category[slot] != game->menu.shop_category[slot] ||
        before.shop_item[slot] != game->menu.shop_item[slot])) sound = 0;
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
        bool pressed[7] = {false};
        if (slot == 0) {
            pressed[MENU_UP] = IsKeyPressed(KEY_UP);
            pressed[MENU_DOWN] = IsKeyPressed(KEY_DOWN);
            pressed[MENU_LEFT] = IsKeyPressed(KEY_LEFT);
            pressed[MENU_RIGHT] = IsKeyPressed(KEY_RIGHT);
            pressed[MENU_CONFIRM] = IsKeyPressed(KEY_ENTER);
            pressed[MENU_READY] = IsKeyPressed(KEY_TAB);
            pressed[MENU_BACK] = IsKeyPressed(KEY_ESCAPE);
        } else if (game->menu.page == MENU_PILOT || game->menu.page == MENU_SHOP) {
            pressed[MENU_UP] = IsKeyPressed(KEY_I);
            pressed[MENU_DOWN] = IsKeyPressed(KEY_K);
            pressed[MENU_READY] = IsKeyPressed(KEY_RIGHT_SHIFT);
            pressed[MENU_LEFT] = IsKeyPressed(KEY_J);
            pressed[MENU_RIGHT] = IsKeyPressed(KEY_L);
            pressed[MENU_CONFIRM] = IsKeyPressed(KEY_RIGHT_CONTROL);
            pressed[MENU_BACK] = IsKeyPressed(KEY_BACKSPACE);
        }
        if (IsGamepadAvailable(slot)) {
            const int buttons[] = {GAMEPAD_BUTTON_LEFT_FACE_UP, GAMEPAD_BUTTON_LEFT_FACE_DOWN,
                GAMEPAD_BUTTON_LEFT_FACE_LEFT, GAMEPAD_BUTTON_LEFT_FACE_RIGHT,
                GAMEPAD_BUTTON_RIGHT_FACE_DOWN, GAMEPAD_BUTTON_RIGHT_FACE_RIGHT, GAMEPAD_BUTTON_MIDDLE_RIGHT};
            for (int i = 0; i < 7; ++i) pressed[i] |= IsGamepadButtonPressed(slot, buttons[i]);
        }
        for (int i = 0; i < 7; ++i) if (pressed[i]) { menu_input(game, assets, (MenuAction)i, slot); break; }
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
        assets->stage_music_started = false;
        if (assets->music_mode == 0) assets->music_mode = -1;
    }
    assets->music_tick = game->tick;
    int mode = title_menu(game) || game->menu.page == MENU_SHOP ? 1 :
        game->paused && game->menu.page != MENU_FINISH && game->menu.page != MENU_STAGE_SELECT ? 2 : 0;
    Music tracks[] = {assets->music, assets->title_music, assets->pause_music};
    if (assets->music_mode != mode) {
        if (assets->music_mode >= 0) PauseMusicStream(tracks[assets->music_mode]);
        if (mode != 0) StopMusicStream(tracks[mode]);
        if (mode == 0 && assets->stage_music_started && !seek && !music_changed) ResumeMusicStream(tracks[mode]);
        else PlayMusicStream(tracks[mode]);
        if (mode == 0) assets->stage_music_started = true;
        assets->music_mode = mode;
    }
    if (seek || music_changed) {
        float seconds = (game->tick - game->stage.music_tick) / (float)GAME_HZ;
        float duration = GetMusicTimeLength(assets->music);
        if (duration > 0) {
            float offset = game->menu.page == MENU_FINISH || game->menu.page == MENU_STAGE_SELECT ? 0 : fmodf(seconds, duration);
            SeekMusicStream(assets->music, offset);
            TraceLog(LOG_INFO, "Stage music: %s seek=%.3f reported=%.3f", music_name, offset, GetMusicTimePlayed(assets->music));
        }
        assets->music_revision = game->stage.revision;
        assets->music_event = game->stage.music_event;
    }
    float sfx = game->menu.options[0] / 10.0f;
    for (int i = 0; i < WEAPON_COUNT; ++i) if (IsSoundValid(assets->weapon_fire[i])) SetSoundVolume(assets->weapon_fire[i], 0.13f * sfx);
    for (int i = 0; i < 4; ++i) SetSoundVolume(assets->impacts[i], 0.2f * sfx);
    for (int i = 0; i < 2; ++i) SetSoundVolume(assets->enemy_fire[i], 0.2f * sfx);
    SetSoundVolume(assets->overheat, 0.3f * sfx);
    SetSoundVolume(assets->bonus_get, sfx);
    SetMusicVolume(tracks[mode], game->menu.options[1] / 10.0f);
    SetSoundVolume(assets->shoot, 0.13f * sfx);
    SetSoundVolume(assets->explode, 0.35f * sfx);
    SetSoundVolume(assets->building_explode, sfx);
    SetSoundVolume(assets->hit, 0.45f * sfx);
    SetSoundVolume(assets->shield_hit, 0.45f * sfx);
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
        input.sidekick_left = input.sidekick_right = IsKeyDown(KEY_X);
    } else {
        input.x = (float)(IsKeyDown(KEY_L) - IsKeyDown(KEY_J));
        input.y = (float)(IsKeyDown(KEY_K) - IsKeyDown(KEY_I));
        input.fire = IsKeyDown(KEY_ENTER) || IsKeyDown(KEY_RIGHT_CONTROL);
        input.focus = IsKeyDown(KEY_RIGHT_SHIFT);
        input.sidekick_left = input.sidekick_right = IsKeyDown(KEY_O);
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
        input.sidekick_left |= IsGamepadButtonDown(slot, GAMEPAD_BUTTON_RIGHT_TRIGGER_1);
        input.sidekick_right |= IsGamepadButtonDown(slot, GAMEPAD_BUTTON_RIGHT_TRIGGER_1);
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
    char save_paths[2][2048] = {{0}};
    if (!frames) {
        const char *root = getenv("LOCALAPPDATA");
        char directory[1800];
        int length = snprintf(directory, sizeof(directory), "%s/GalaxyDefenders", root ? root : ".");
        if (length > 0 && length < (int)sizeof(directory) && (DirectoryExists(directory) || MakeDirectory(directory) == 0)) {
            for (int i = 0; i < 2; ++i) {
                snprintf(save_paths[i], sizeof(save_paths[i]), "%s/%s.save", directory, i ? "coop" : "solo");
                if (FileExists(save_paths[i]) && !game_save_read(&game->saves[i], save_paths[i]))
                    console_log(console, "ERROR: could not read %s save", i ? "co-op" : "solo");
            }
        } else console_log(console, "ERROR: could not create save directory");
    }
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
        int old_pickups = game->pickups_collected;
        int old_kills = game->kills, old_hits = game->armor_hits, old_shield_hits = game->shield_hits, old_deaths = game->deaths;
        int old_weapon_shots[WEAPON_COUNT], old_impacts[4], old_enemy_shots[2], old_overheats = game->overheats;
        memcpy(old_weapon_shots, game->weapon_shots, sizeof(old_weapon_shots));
        memcpy(old_impacts, game->impact_sounds, sizeof(old_impacts));
        memcpy(old_enemy_shots, game->enemy_shots, sizeof(old_enemy_shots));
        if (!console_open && !toggled && ((!was_menu && !game->paused) || game->menu.page == MENU_OVER || game->menu.page == MENU_CLEAR || game->menu.page == MENU_FINISH || game->menu.page == MENU_SHOP || game->transition_frames > 0) && (IsWindowFocused() || frames)) {
            if (!frames) for (int i = 0; i < GAME_PLAYERS; ++i) game->players[i].input = poll_input(i);
            accumulator += frames ? 1.0 / GAME_HZ : fmin(GetFrameTime(), 0.1);
            while (accumulator >= 1.0 / GAME_HZ) {
                game_tick(game);
                if (game->menu.page == MENU_SHOP && game->transition_frames <= 15) {
                    int shots[WEAPON_COUNT];
                    memcpy(shots, shop_preview.weapon_shots, sizeof(shots));
                    game_shop_preview_update(&shop_preview, game);
                    if (assets.audio) for (int i = 0; i < WEAPON_COUNT; ++i)
                        if (shop_preview.weapon_shots[i] > shots[i] && IsSoundValid(assets.weapon_fire[i]))
                            PlaySound(assets.weapon_fire[i]);
                } else if (game->menu.page != MENU_SHOP) shop_preview.tick = 0;
                accumulator -= 1.0 / GAME_HZ;
            }
        } else accumulator = 0;
        if (assets.audio) {
            if (game->pickups_collected > old_pickups) PlaySound(assets.bonus_get);
            for (int i = 0; i < WEAPON_COUNT; ++i) if (game->weapon_shots[i] > old_weapon_shots[i] && IsSoundValid(assets.weapon_fire[i])) PlaySound(assets.weapon_fire[i]);
            for (int i = 0; i < 4; ++i) if (game->impact_sounds[i] > old_impacts[i]) PlaySound(assets.impacts[i]);
            for (int i = 0; i < 2; ++i) if (game->enemy_shots[i] > old_enemy_shots[i]) PlaySound(assets.enemy_fire[i]);
            if (game->overheats > old_overheats) PlaySound(assets.overheat);
            if (game->building_kills > old_buildings) PlaySound(assets.building_explode);
            if (game->kills > old_kills) PlaySound(assets.explode);
            if (game->armor_hits > old_hits) PlaySound(assets.hit);
            if (game->shield_hits > old_shield_hits) PlaySound(assets.shield_hit);
            if (game->deaths > old_deaths) PlaySound(assets.die);
        }
        if (assets.stage_index != game->stage.index && !stage_assets_load(&assets, game->stage.index)) {
            console_log(console, "ERROR: stage assets failed to load");
            command_failed = true;
            break;
        }
        if (!frames && game->saves_dirty) {
            for (int i = 0; i < 2; ++i) if (game->saves[i].valid &&
                (!save_paths[i][0] || !game_save_write(&game->saves[i], save_paths[i])))
                console_log(console, "ERROR: could not write %s save", i ? "co-op" : "solo");
            game->saves_dirty = false;
        }
#ifdef GALAXY_MEMORY
        BeginDrawing();
#else
        BeginTextureMode(canvas);
#endif
        draw_game(game, &assets);
        draw_menu(game, &assets, menu_frame++);
        if (game->transition_frames > 0) {
            float alpha = game->transition_frames > 15 ? (30 - game->transition_frames) / 15.0f : game->transition_frames / 15.0f;
            DrawRectangle(ARENA_X, 0, GAME_WIDTH, GAME_HEIGHT, Fade(BLACK, alpha));
        }
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
