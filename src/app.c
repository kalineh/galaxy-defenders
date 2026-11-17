#include "game.h"
#include "console.h"
#include "synth.h"
#include "input.h"
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
    Texture2D shop[15], star;
    Texture2D weapon_icons[WEAPON_COUNT], weapon_textures[WEAPON_COUNT];
    Texture2D hostile_textures[HOSTILE_COUNT];
    Texture2D big_laser, big_flame, shield_overlay, asteroid_cracks;
    Texture2D generator_icons[GENERATOR_COUNT], shield_icons[SHIELD_COUNT], item_none, sidekick;
    Texture2D stage_textures[STAGE_MAX_EVENTS], stage_destroyed[STAGE_MAX_EVENTS];
    Texture2D scenery[4];
    Texture2D boss4_cover, boss5_cover, boss6_center;
    Texture2D rotate_turret_base, splitter_right, pyramid_turret, cutter_beam;
    Texture2D enemy_corpses[ENEMY_KIND_COUNT];
    Texture2D pickups[3], medals[5], continue_button;
    int stage_texture_owner[STAGE_MAX_EVENTS], stage_index;
    Music music, title_music, pause_music;
    Music audition_stream;
    unsigned audition_revision;
    bool audition_active, album_paused;
    int music_mode, music_revision, music_event, music_tick;
    const char *music_name;
    Sound shoot, explode[3], building_explode, hit, shield_hit, shot_hit, die;
    Sound bonus_get;
    Sound weapon_fire[WEAPON_COUNT], impacts[4], enemy_fire[2], overheat;
    Sound menu_sounds[5];
    bool audio, stage_music_started;
    Font console_font, ui_font;
} Assets;

static const char *synth_path(const char *source);

static const char *shop_texture_names[] = {
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
    if (IsTextureValid(assets->asteroid_cracks)) UnloadTexture(assets->asteroid_cracks);
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
    for (int i = 0; i < 15; ++i) if (IsTextureValid(assets->shop[i])) UnloadTexture(assets->shop[i]);
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
        if (IsMusicValid(assets->audition_stream)) UnloadMusicStream(assets->audition_stream);
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
        for (int i = 0; i < 3; ++i) if (IsSoundValid(assets->explode[i])) UnloadSound(assets->explode[i]);
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
        snprintf(path, sizeof(path), "textures/%s.png", i == ENEMY_SPLITTER_FRAGMENT ? "SplitterFragmentLeft" : enemy_is_asteroid(i) ? "Asteroid" : enemy_names[i]);
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
    assets->asteroid_cracks = texture_load("textures/Cracks.png");
    if (!IsTextureValid(assets->asteroid_cracks)) return false;
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
    for (int i = 0; i < 15; ++i) {
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
    assets->music = LoadMusicStream(synth_path("The_Voyage"));
    assets->title_music = LoadMusicStream(synth_path("Konami's_Moon_Base"));
    assets->pause_music = LoadMusicStream(synth_path("1-X-0"));
    const char *menu_sounds[] = {"MenuMoveItem", "MenuSelect", "MenuCancel", "MenuUpgrade", "MenuDowngrade"};
    for (int i = 0; i < 5; ++i) {
        char path[128];
        snprintf(path, sizeof(path), "audio/%s.wav", menu_sounds[i]);
        assets->menu_sounds[i] = LoadSound(asset_path(path));
        if (!IsSoundValid(assets->menu_sounds[i])) return false;
    }
    assets->shoot = LoadSound(asset_path("audio/WeaponShootLaser.wav"));
    assets->building_explode = LoadSound(asset_path("audio/BuildingExplode.wav"));
    for (int i = 0; i < 3; ++i) {
        char path[64];
        snprintf(path, sizeof(path), "audio/EnemyDie%d.wav", i + 1);
        assets->explode[i] = LoadSound(asset_path(path));
        if (!IsSoundValid(assets->explode[i])) return false;
    }
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
        !IsSoundValid(assets->building_explode) || !IsSoundValid(assets->hit) || !IsSoundValid(assets->shield_hit) || !IsSoundValid(assets->shot_hit) || !IsSoundValid(assets->die)) {
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
    for (int i = 0; i < 3; ++i) SetSoundVolume(assets->explode[i], 0.35f);
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
        if (enemy->kind == ENEMY_BOSS10_BLOCK || enemy_is_asteroid(enemy->kind)) scale = enemy->orbit_radius;
        float width = enemy->kind == ENEMY_FENCE_BEAM ? enemy->box_width : source.width * scale;
        Vector2 origin = {width * 0.5f, source.height * 0.5f * scale};
        if (enemy->kind == ENEMY_BOSS4_CHUNK) origin.x = 0;
        if (enemy->kind == ENEMY_BOSS7_CANNON_BIG || enemy->kind == ENEMY_BOSS7_CANNON_SMALL) origin.y = 0;
        if (enemy->kind == ENEMY_BOSS12_EYE) origin.x = (float)texture.width;
        DrawTexturePro(texture, source,
            (Rectangle){view_x + enemy->x,enemy->y,width,source.height * scale}, origin, rotation * RAD2DEG, WHITE);
        if (enemy_is_asteroid(enemy->kind))
            sprite(assets->asteroid_cracks, view_x + enemy->x, enemy->y, 80 * scale, rotation * RAD2DEG,
                Fade(WHITE, fminf(1, fmaxf(0, 1 - enemy->health / enemy->health_max))));

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
    return page == MENU_SLOTS || page == MENU_OVERWRITE || page == MENU_MAIN || page == MENU_NEW_GAME || page == MENU_DIFFICULTY || page == MENU_PILOT ||
        ((page == MENU_OPTIONS || page == MENU_QUIT) && game->menu.parent == MENU_MAIN);
}

static const char *shop_item_name(int category, int item) {
    if (item < 0) return "Empty";
    switch (category) {
        case 0: return game_chassis[item].name;
        case 1: return game_generators[item].name;
        case 2: return game_shields[item].name;
        default: return game_weapons[item].display_name;
    }
}

static const char *shop_button(int device, int slot, MenuAction action) {
    static const char *labels[][3] = {
        {"X", "C", "Z"}, {"R Ctrl", "Backspace", "R Shift"},
        {"A", "B", "X"}, {"Cross", "Circle", "Square"}
    };
    int layout = device >= 0 && device < INPUT_PADS ? (device >= 4 ? 3 : 2) : slot;
    return labels[layout][action == MENU_CONFIRM ? 0 : action == MENU_BACK ? 1 : 2];
}

static void shop_meter(Font font, int x, int y, const char *label, float value, float capacity, Color color) {
    DrawTextEx(font, label, (Vector2){x, y}, 21, 0, WHITE);
    const char *amount = TextFormat("%.0f / %.0f", value, capacity);
    DrawTextEx(font, amount, (Vector2){x + 240 - MeasureTextEx(font, amount, 18, 0).x, y + 3}, 18, 0, dim);
    DrawRectangle(x, y + 26, 240, 16, (Color){45,55,69,255});
    if (capacity > 0) DrawRectangle(x, y + 26, (int)(240 * fminf(1, fmaxf(0, value / capacity))), 16, color);
}

static void draw_shop_panel(const Game *game, const Assets *assets, int slot, int device) {
    static const char *names[] = {"Kazuki", "Rabbit", "Gunthor"};
    static const char *categories[] = {"Chassis", "Generator", "Shield", "Main weapon", "Support weapon", "Sidekick"};
    Font font = assets->ui_font;
    const Color ink = {12, 18, 29, 255}, rule = {51, 64, 80, 255}, surface = {25, 36, 51, 255};
    Color highlight = player_colors[slot];
    int x = slot ? ARENA_X + GAME_WIDTH : 0;
    int left = x + 28, width = ARENA_X - 56;
    const Player *p = &game->players[slot];
    const GameMenu *menu = &game->menu;
    DrawRectangle(x, 0, ARENA_X, GAME_HEIGHT, ink);
    DrawRectangle(slot ? x : x + ARENA_X - 2, 0, 2, GAME_HEIGHT, rule);
    DrawRectangle(left, 32, 58, 36, p->joined ? highlight : rule);
    text_center(font, slot ? "P2" : "P1", left + 29, 33, 27, ink);
    if (!p->joined) {
        text_center(font, "Join flight", x + 240, 488, 36, WHITE);
        text_center(font, slot ? "Press Start or F2" : "Press Start or Enter", x + 240, 543, 23, dim);
        return;
    }
    DrawTextEx(font, names[p->pilot], (Vector2){left + 70, 26}, 38, 0, WHITE);
    const char *credits = TextFormat("%d cr", p->money);
    DrawTextEx(font, credits, (Vector2){left + width - MeasureTextEx(font, credits, 27, 0).x, 38}, 27, 0, highlight);
    DrawLine(left, 88, left + width, 88, rule);
    sprite(assets->portraits[p->pilot], left + 82, 204, 164, 0, WHITE);
    const Player *preview = shop_preview.players[slot].joined ? &shop_preview.players[slot] : p;
    shop_meter(font, left + 184, 115, "Armor", preview->health, game_chassis[preview->chassis].armor, highlight);
    shop_meter(font, left + 184, 172, "Shield", preview->shield_health,
        preview->shield < 0 ? 0 : game_shields[preview->shield].capacity, (Color){130,183,240,255});
    shop_meter(font, left + 184, 229, "Energy", preview->energy, game_generators[preview->generator].capacity, (Color){117,214,188,255});
    int cat = menu->shop_category[slot], owned = game_shop_equipped(p, cat);
    DrawLine(left, 298, left + width, 298, rule);
    DrawTextEx(font, "SHOP", (Vector2){left, 315}, 36, 1, WHITE);
    const char *category = categories[cat];
    DrawTextEx(font, category, (Vector2){left + width - MeasureTextEx(font, category, 26, 0).x, 324}, 26, 0, highlight);
    for (int c = 0; c < 6; ++c) {
        int xx = left + c * 72;
        DrawRectangle(xx, 368, 64, 52, cat == c ? surface : ink);
        sprite(assets->shop[c], xx + 32, 393, 42, 0, cat == c ? highlight : dim);
        DrawRectangle(xx, 418, 64, cat == c ? 4 : 1, cat == c ? highlight : rule);
    }
    int stock[WEAPON_COUNT + 1], count = game_shop_stock(game, slot, cat, stock);
    int item = menu->shop_item[slot], selected = 0;
    for (int i = 0; i < count; ++i) if (stock[i] == item) selected = i;
    int first = selected > 6 ? selected - 6 : 0;
    int y = 441;
    for (int i = first; i < count && i < first + 7; ++i, y += 58) {
        int id = stock[i];
        bool focused = id == item && !menu->ready[slot];
        DrawRectangle(left, y, width, 55, focused ? surface : ink);
        DrawLine(left, y + 55, left + width, y + 55, rule);
        if (focused) DrawRectangle(left, y, 4, 55, highlight);
        Texture2D icon = id < 0 ? assets->item_none : cat == 0 ? assets->shop[6 + id] :
            cat == 1 ? assets->generator_icons[id] : cat == 2 ? assets->shield_icons[id] : assets->weapon_icons[id];
        sprite(icon, left + 32, y + 27, 39, 0, WHITE);
        DrawTextEx(font, shop_item_name(cat, id), (Vector2){left + 63, y + 11}, 26, 0, WHITE);
        if (id == owned) DrawTextEx(font, "EQ", (Vector2){left + width - 36, y + 15}, 19, 0, highlight);
    }
    int cost = game_shop_cost(game, slot);
    int level = cat == 3 ? p->weapon_level : cat == 4 ? p->secondary_level : 0;
    bool upgrade = owned == item && menu->shop_level[slot] > level;
    if (!menu->ready[slot] && !menu->shop_purchase[slot] && (item != owned || upgrade)) {
        const char *action = upgrade ? "Upgrade" : item < 0 ? "Unequip" : "Buy & equip";
        DrawTextEx(font, TextFormat("[%s]  %s", shop_button(device, slot, MENU_CONFIRM), action),
            (Vector2){left, y + 16}, 25, 0, WHITE);
        const char *price = cost < 0 ? TextFormat("+%d cr", -cost) : TextFormat("%d cr", cost);
        DrawTextEx(font, price, (Vector2){left + width - MeasureTextEx(font, price, 25, 0).x, y + 16}, 25, 0, cost > p->money ? danger : highlight);
    }
    DrawLine(left, 974, left + width, 974, rule);
    DrawTextEx(font, TextFormat("[%s]  Ready", shop_button(device, slot, MENU_READY)), (Vector2){left, 993}, 30, 0, WHITE);
    int check = left + width - 34;
    DrawRectangleLinesEx((Rectangle){check,997,31,31}, 2, menu->ready[slot] ? highlight : dim);
    if (menu->ready[slot]) {
        DrawLineEx((Vector2){check+5,1012}, (Vector2){check+13,1020}, 4, highlight);
        DrawLineEx((Vector2){check+13,1020}, (Vector2){check+26,1004}, 4, highlight);
    }
}

static void draw_shop_purchase(const Game *game, Font font, int slot, int device, int y) {
    const Player *p = &game->players[slot];
    int cat = game->menu.shop_category[slot], item = game->menu.shop_item[slot];
    int level = game->menu.shop_level[slot], cost = game_shop_cost(game, slot);
    bool upgrade = item == game_shop_equipped(p, cat);
    Color accent = player_colors[slot];
    DrawRectangle(644, y, 632, 228, (Color){18,27,40,255});
    DrawRectangleLinesEx((Rectangle){644,y,632,228}, 2, accent);
    DrawTextEx(font, TextFormat("P%d   /   %s", slot + 1, upgrade ? "CONFIRM UPGRADE" :
        item < 0 ? "CONFIRM UNEQUIP" : "CONFIRM PURCHASE"), (Vector2){672,y+18}, 22, 1, accent);
    const char *name = item < 0 ? shop_item_name(cat, game_shop_equipped(p, cat)) : shop_item_name(cat, item);
    DrawTextEx(font, name, (Vector2){672,y+55}, 36, 0, WHITE);
    if (upgrade) DrawTextEx(font, TextFormat("Mk %d  >  Mk %d", level, level+1), (Vector2){672,y+111}, 25, 0, dim);
    const char *price = cost < 0 ? TextFormat("Credit  +%d cr", -cost) : TextFormat("Cost  %d cr", cost);
    DrawTextEx(font, price, (Vector2){1248 - MeasureTextEx(font, price, 26, 0).x,y+110}, 26, 0, cost > p->money ? danger : WHITE);
    DrawLine(672, y+156, 1248, y+156, (Color){62,75,90,255});
    const char *buy = cost > p->money ? "Insufficient credits" : TextFormat("[%s]  Confirm", shop_button(device,slot,MENU_CONFIRM));
    DrawTextEx(font, buy, (Vector2){672,y+174}, 26, 0, cost > p->money ? danger : accent);
    const char *cancel = TextFormat("[%s]  Cancel", shop_button(device,slot,MENU_BACK));
    DrawTextEx(font, cancel, (Vector2){1248 - MeasureTextEx(font,cancel,25,0).x,y+174}, 25, 0, WHITE);
}

static void draw_shop(const Game *game, const Assets *assets, int frame, const PlayerDevices *devices) {
    ClearBackground(BLACK);
    for (int layer = 0; layer < 2; ++layer) for (int i = 0; i < 10; ++i) {
        float x = ARENA_X + (i * 193 + layer * 311) % GAME_WIDTH;
        float y = fmodf(i * 137.0f + frame * (layer ? 2.1f : 3.5f), GAME_HEIGHT);
        sprite(assets->star, x, y, assets->star.width * (layer ? 0.6f : 0.4f), 0, WHITE);
    }
    BeginScissorMode(ARENA_X, 0, GAME_WIDTH, GAME_HEIGHT);
    draw_combat_sprites(&shop_preview, assets);
    EndScissorMode();
    for (int slot = 0; slot < GAME_PLAYERS; ++slot) draw_shop_panel(game, assets, slot, devices->device[slot]);
    bool both = game->players[0].joined && game->players[1].joined &&
        game->menu.shop_purchase[0] && game->menu.shop_purchase[1];
    for (int slot = 0; slot < GAME_PLAYERS; ++slot)
        if (game->players[slot].joined && game->menu.shop_purchase[slot])
            draw_shop_purchase(game, assets->ui_font, slot, devices->device[slot], both ? 300 + slot * 250 : 426);
}

static void draw_menu(const Game *game, const Assets *assets, int frame, const PlayerDevices *devices) {
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
    if (game->menu.page == MENU_SHOP) { draw_shop(game, assets, frame, devices); return; }
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
    }
    if (game->menu.page == MENU_NEW_GAME)
        text_center(assets->ui_font, TextFormat("Slot %d", game->active_slot + 1), 960, 345, 28, WHITE);
    if (game->menu.page == MENU_OVERWRITE)
        text_center(assets->ui_font, TextFormat("Replace Slot %d?", game->active_slot + 1), 960, 330, 32, WHITE);
    if (game->menu.page == MENU_PILOT) {
        static const char *info[3][5] = {
            {"Kazuki", "Japan", "Ace Pilot", "B", "Speed+"},
            {"Rabbit", "Russia", "Wpn Research", "A", "Money+"},
            {"Gunthor", "Norway", "Demolitions", "O", "Damage+"}
        };
        static const char *keys[] = {"Name", "Country", "Profession", "Blood Type", "Bonus"};
        for (int slot = 0; slot < GAME_PLAYERS; ++slot) {
            if (!game->players[slot].joined) continue;
            int x = (slot ? 1440 : 0) + 238, y = 472;
            int pilot = game->menu.pilot[slot];
            DrawTextEx(assets->ui_font, game->menu.ready[slot] ? "Waiting..." : "Select Pilot", (Vector2){x - 120.0f, y - 240.0f}, 36, 0, WHITE);
            DrawRectangle(x - 100, y - 100, 200, 200, (Color){18,27,40,255});
            sprite(assets->portraits[pilot], (float)x, (float)y, 196, 0, WHITE);
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
        if (game->menu.page == MENU_SLOTS && row < GAME_SAVE_SLOTS) {
            Texture2D button = assets->menu[texture];
            DrawTexturePro(button, (Rectangle){0,0,(float)button.width,(float)button.height},
                (Rectangle){720,(float)y,480,(float)button.height}, (Vector2){0}, 0, WHITE);
        } else DrawTexture(assets->menu[texture], 832, y, WHITE);
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
    if (before.page == MENU_SHOP) valid = game_shop_cost(game, slot) <= game->players[slot].money;
    game_menu_input(game, action, slot);
    if (!assets->audio) return;
    int sound = -1;
    if (action == MENU_BACK) sound = 2;
    else if (action == MENU_CONFIRM) sound = valid ? 1 : 2;
    else if (before.page == MENU_SHOP && action == MENU_READY) sound = game->menu.ready[slot] ? 1 : 0;
    else if (before.page == MENU_SHOP && (before.shop_category[slot] != game->menu.shop_category[slot] ||
        before.shop_item[slot] != game->menu.shop_item[slot])) sound = 0;
    else if (before.cursor != game->menu.cursor || before.pilot[slot] != game->menu.pilot[slot]) sound = 0;
    else if (before.page == MENU_OPTIONS && memcmp(before.options, game->menu.options, sizeof(before.options))) sound = action == MENU_RIGHT ? 3 : 4;
    if (sound >= 0) PlaySound(assets->menu_sounds[sound]);
}

static void poll_controllers(PadInput pads[INPUT_PADS]) {
    static uint32_t navigation_before[INPUT_PADS];
    ds4_update(&pads[4]);
    bool native_ds4 = pads[4].connected || pads[5].connected;
    const int ray_buttons[] = {GAMEPAD_BUTTON_LEFT_FACE_UP, GAMEPAD_BUTTON_LEFT_FACE_RIGHT,
        GAMEPAD_BUTTON_LEFT_FACE_DOWN, GAMEPAD_BUTTON_LEFT_FACE_LEFT, GAMEPAD_BUTTON_RIGHT_FACE_DOWN,
        GAMEPAD_BUTTON_RIGHT_FACE_RIGHT, GAMEPAD_BUTTON_RIGHT_FACE_UP, GAMEPAD_BUTTON_LEFT_TRIGGER_1,
        GAMEPAD_BUTTON_RIGHT_TRIGGER_1, GAMEPAD_BUTTON_MIDDLE_RIGHT, GAMEPAD_BUTTON_MIDDLE_LEFT, GAMEPAD_BUTTON_RIGHT_FACE_LEFT};
    const uint32_t bits[] = {PAD_UP, PAD_RIGHT, PAD_DOWN, PAD_LEFT, PAD_CONFIRM, PAD_BACK,
        PAD_RETRY, PAD_FOCUS, PAD_SIDEKICK, PAD_START, PAD_SELECT, PAD_READY};
    for (int device = 0; device < 4; ++device) {
        uint32_t before = pads[device].buttons;
        pads[device] = (PadInput){0};
        if (!IsGamepadAvailable(device)) continue;
        const char *name = GetGamepadName(device);
        /* Native DS4 reports replace the same device's GLFW view. */
        if (native_ds4 && name && !strstr(name, "DualSense") && !strstr(name, "PS5") &&
            (strstr(name, "Wireless Controller") || strstr(name, "PS4") ||
            strstr(name, "DualShock") || strstr(name, "DUALSHOCK"))) continue;
        PadInput *pad = &pads[device];
        pad->connected = true;
        for (int b = 0; b < (int)(sizeof(bits) / sizeof(bits[0])); ++b) if (IsGamepadButtonDown(device, ray_buttons[b])) pad->buttons |= bits[b];
        pad->pressed = pad->buttons & ~before;
        pad->x = GetGamepadAxisMovement(device, GAMEPAD_AXIS_LEFT_X);
        pad->y = GetGamepadAxisMovement(device, GAMEPAD_AXIS_LEFT_Y);
        pad->trigger = GetGamepadAxisCount(device) > GAMEPAD_AXIS_RIGHT_TRIGGER ?
            (GetGamepadAxisMovement(device, GAMEPAD_AXIS_RIGHT_TRIGGER) + 1) * .5f : 0;
    }
    for (int device = 0; device < INPUT_PADS; ++device) {
        uint32_t navigation = 0;
        if (pads[device].connected) {
            if (pads[device].x < -.55f) navigation |= PAD_LEFT;
            if (pads[device].x > .55f) navigation |= PAD_RIGHT;
            if (pads[device].y < -.55f) navigation |= PAD_UP;
            if (pads[device].y > .55f) navigation |= PAD_DOWN;
        }
        pads[device].pressed |= navigation & ~navigation_before[device];
        navigation_before[device] = navigation;
    }
}

static void poll_joining(Game *game, Assets *assets, PlayerDevices *devices, PadInput pads[INPUT_PADS], bool accept_input) {
    for (int slot = 0; slot < GAME_PLAYERS; ++slot) {
        int device = devices->device[slot];
        if (device >= 0 && device < INPUT_PADS && !pads[device].connected) input_drop(game, devices, device);
    }
    if (!accept_input) return;
    bool start = IsKeyPressed(KEY_ENTER);
    bool keyboard = start || IsKeyPressed(KEY_X) || IsKeyPressed(KEY_UP) || IsKeyPressed(KEY_DOWN);
    if (keyboard && devices->device[0] == -1) {
        input_claim(game, devices, INPUT_KEYBOARD);
    } else if (start && devices->device[0] == INPUT_KEYBOARD) {
        if (game->menu.page == MENU_NONE || game->menu.page == MENU_SHOP) game_menu_open(game, MENU_PAUSE);
        else if (game->menu.page == MENU_PAUSE) menu_input(game, assets, MENU_BACK, 0);
    }
    if (IsKeyPressed(KEY_F2)) {
        if (devices->device[1] == INPUT_KEYBOARD_P2) input_drop(game, devices, INPUT_KEYBOARD_P2);
        else input_claim(game, devices, INPUT_KEYBOARD_P2);
    }
    /* Prefer native reports if the same DS4 is also exposed through an adapter. */
    const int order[] = {4,5,0,1,2,3};
    bool handled_start = false, owned_start = false;
    for (int i = 0; i < INPUT_PADS; ++i)
        if (input_owner(devices, i) >= 0 && (pads[i].pressed & PAD_START)) owned_start = true;
    for (int i = 0; i < INPUT_PADS; ++i) {
        int device = order[i];
        PadInput *pad = &pads[device];
        if (!(pad->pressed & PAD_START)) continue;
        int slot = input_owner(devices, device);
        if (handled_start || (slot < 0 && owned_start)) { pad->pressed = 0; continue; }
        if (slot < 0) {
            bool empty = !game->players[0].joined && !game->players[1].joined;
            if (input_claim(game, devices, device) >= 0) {
                if (empty && game->menu.page == MENU_PAUSE) menu_input(game, assets, MENU_BACK, 0);
            }
        } else if (pad->buttons & PAD_SELECT) input_drop(game, devices, device);
        else if (!game->players[slot].joined) game_join(game, slot);
        else if (game->menu.page == MENU_NONE || game->menu.page == MENU_SHOP) game_menu_open(game, MENU_PAUSE);
        else if (game->menu.page == MENU_PAUSE) menu_input(game, assets, MENU_BACK, slot);
        handled_start = true;
        pad->pressed = 0; /* The join/pause press must not also select a menu row. */
    }
}

static void poll_menu(Game *game, Assets *assets, const PlayerDevices *devices, const PadInput pads[INPUT_PADS]) {
    if (game->menu.page == MENU_PAUSE && IsKeyPressed(KEY_P)) {
        menu_input(game, assets, MENU_BACK, 0); return;
    }
    for (int slot = 0; slot < GAME_PLAYERS; ++slot) {
        bool pressed[7] = {false};
        int device = devices->device[slot];
        if (slot == 0 && (device == INPUT_KEYBOARD || device == -1)) {
            pressed[MENU_UP] = IsKeyPressed(KEY_UP);
            pressed[MENU_DOWN] = IsKeyPressed(KEY_DOWN);
            pressed[MENU_LEFT] = IsKeyPressed(KEY_LEFT);
            pressed[MENU_RIGHT] = IsKeyPressed(KEY_RIGHT);
            pressed[MENU_CONFIRM] = IsKeyPressed(KEY_X);
            pressed[MENU_READY] = IsKeyPressed(KEY_Z);
            pressed[MENU_BACK] = IsKeyPressed(KEY_C) || IsKeyPressed(KEY_ESCAPE);
        } else if (device == INPUT_KEYBOARD_P2) {
            pressed[MENU_UP] = IsKeyPressed(KEY_I);
            pressed[MENU_DOWN] = IsKeyPressed(KEY_K);
            pressed[MENU_READY] = IsKeyPressed(KEY_RIGHT_SHIFT);
            pressed[MENU_LEFT] = IsKeyPressed(KEY_J);
            pressed[MENU_RIGHT] = IsKeyPressed(KEY_L);
            pressed[MENU_CONFIRM] = IsKeyPressed(KEY_RIGHT_CONTROL);
            pressed[MENU_BACK] = IsKeyPressed(KEY_BACKSPACE);
        }
        if (device >= 0 && device < INPUT_PADS && pads[device].connected) {
            const uint32_t buttons[] = {PAD_UP, PAD_DOWN, PAD_LEFT, PAD_RIGHT, PAD_CONFIRM, PAD_BACK, PAD_READY};
            for (int i = 0; i < 7; ++i) pressed[i] |= (pads[device].pressed & buttons[i]) != 0;
        }
        for (int i = 0; i < 7; ++i) if (pressed[i]) {
            MenuPage previous = game->menu.page;
            menu_input(game, assets, (MenuAction)i, slot);
            if (game->menu.page != previous) return;
            break;
        }
    }
}

static const char *synth_path(const char *source) {
    static char path[80];
    snprintf(path, sizeof(path), "synth/%d.wav", synth_track_find(source));
    return asset_path(path);
}

static void update_music_override(const Game *game, Console *console, Assets *assets) {
    if (assets->audition_revision != console->music_revision) {
        assets->audition_revision = console->music_revision;
        if (IsMusicValid(assets->audition_stream)) UnloadMusicStream(assets->audition_stream);
        assets->audition_stream = (Music){0};
        assets->audition_active = false;
        if (console->music_track) {
            char path[80]; snprintf(path, sizeof(path), "synth/%d.wav", console->music_track);
            assets->audition_stream = LoadMusicStream(asset_path(path));
            if (!IsMusicValid(assets->audition_stream)) console_log(console, "ERROR: synth track missing; rebuild native.");
            else {
                assets->audition_active = true;
                assets->audition_stream.looping = true;
                PlayMusicStream(assets->audition_stream);
                SeekMusicStream(assets->audition_stream, console->music_progress * (GetMusicTimeLength(assets->audition_stream) - .001f));
            }
        }
    }
    if (assets->audition_active) {
        SetMusicVolume(assets->audition_stream, game->menu.options[1] / 10.0f);
        UpdateMusicStream(assets->audition_stream);
    }
}

static void update_audio(const Game *game, Console *console, Assets *assets) {
    if (!assets->audio) return;
    update_music_override(game, console, assets);
    const char *music_name = game_music(game);
    bool music_changed = strcmp(assets->music_name, music_name) != 0;
    bool seek = game->tick < assets->music_tick || assets->music_revision != game->stage.revision || assets->music_event != game->stage.music_event;
    if (music_changed) {
        StopMusicStream(assets->music);
        UnloadMusicStream(assets->music);
        assets->music = LoadMusicStream(synth_path(music_name));
        assets->music.looping = true;
        assets->music_name = music_name;
        assets->stage_music_started = false;
        if (assets->music_mode == 0) assets->music_mode = -1;
    }
    assets->music_tick = game->tick;
    int mode = title_menu(game) || game->menu.page == MENU_SHOP ? 1 :
        game->paused && !game->over && !game->complete && game->menu.page != MENU_FINISH && game->menu.page != MENU_STAGE_SELECT ? 2 : 0;
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
    SetMusicVolume(tracks[mode], assets->audition_active ? 0 : game->menu.options[1] / 10.0f);
    SetSoundVolume(assets->shoot, 0.13f * sfx);
    for (int i = 0; i < 3; ++i) SetSoundVolume(assets->explode[i], 0.35f * sfx);
    SetSoundVolume(assets->building_explode, sfx);
    SetSoundVolume(assets->hit, 0.45f * sfx);
    SetSoundVolume(assets->shield_hit, 0.45f * sfx);
    SetSoundVolume(assets->shot_hit, 0.2f * sfx);
    SetSoundVolume(assets->die, 0.5f * sfx);
    for (int i = 0; i < 5; ++i) SetSoundVolume(assets->menu_sounds[i], sfx);
    if (console->music_album) {
        PauseMusicStream(tracks[mode]);
        assets->album_paused = true;
    } else {
        if (assets->album_paused) ResumeMusicStream(tracks[mode]);
        assets->album_paused = false;
        UpdateMusicStream(tracks[mode]);
    }
}

static void music_album_input(Console *console, Game *game, Assets *assets) {
    int track = console->music_track ? console->music_track : 1;
    float progress = console->music_progress;
    if (assets->audition_active) progress = GetMusicTimePlayed(assets->audition_stream) / GetMusicTimeLength(assets->audition_stream);
    bool change = !console->music_track;
    if (IsKeyPressed(KEY_UP)) { track = track == 1 ? SYNTH_TRACK_COUNT : track - 1; progress = 0; change = true; }
    if (IsKeyPressed(KEY_DOWN)) { track = track % SYNTH_TRACK_COUNT + 1; progress = 0; change = true; }
    if (IsKeyPressed(KEY_LEFT) || IsKeyPressedRepeat(KEY_LEFT)) { progress = fmaxf(0, progress - .05f); change = true; }
    if (IsKeyPressed(KEY_RIGHT) || IsKeyPressedRepeat(KEY_RIGHT)) { progress = fminf(.999f, progress + .05f); change = true; }
    if (IsKeyPressed(KEY_ENTER)) { progress = 0; change = true; }
    if (change) {
        char command[80]; snprintf(command, sizeof(command), "debug.music %d %.6f", track, progress);
        console_execute(console, game, command);
    }
    if (IsKeyPressed(KEY_ESCAPE)) console_execute(console, game, "debug.music off");
}

static void draw_music_album(const Console *console, const Assets *assets) {
    DrawRectangle(340, 100, 1240, 880, (Color){8,14,24,250});
    DrawTextEx(assets->console_font, "DEVELOPER MUSIC ALBUM", (Vector2){380,130}, 32, 0, accent);
    DrawTextEx(assets->console_font, "Up/Down: track   Left/Right: seek 5%   Enter: restart   Esc: close", (Vector2){380,180}, 22, 0, WHITE);
    for (int i = 1; i <= SYNTH_TRACK_COUNT; ++i) {
        bool selected = console->music_track == i;
        if (selected) DrawRectangle(370, 224 + (i-1)*34, 1180, 33, (Color){38,62,85,255});
        DrawTextEx(assets->console_font, TextFormat("%02d  %-28s  %6.1fs", i, synth_track_name(i), synth_track_frames(i)/(float)SYNTH_RATE),
            (Vector2){390,228+(i-1)*34.0f}, 24, 0, selected ? accent : WHITE);
    }
    float progress = console->music_progress;
    if (assets->audio && assets->audition_active) progress = GetMusicTimePlayed(assets->audition_stream)/GetMusicTimeLength(assets->audition_stream);
    DrawRectangle(390, 905, 1140, 10, (Color){40,48,60,255});
    DrawRectangle(390, 905, (int)(1140*fminf(1,progress)), 10, accent);
    DrawTextEx(assets->console_font, TextFormat("%.1f%%  |  C synth soundtrack - first drafts", progress*100), (Vector2){390,930}, 22, 0, WHITE);
}

static PlayerInput poll_keyboard(int slot) {
    PlayerInput input = {0};
    if (slot == 0) {
        input.x = (float)(IsKeyDown(KEY_RIGHT) - IsKeyDown(KEY_LEFT));
        input.y = (float)(IsKeyDown(KEY_DOWN) - IsKeyDown(KEY_UP));
        input.fire = IsKeyDown(KEY_X);
        input.focus = IsKeyDown(KEY_C);
        input.sidekick_left = input.sidekick_right = IsKeyDown(KEY_Z);
    } else {
        input.x = (float)(IsKeyDown(KEY_L) - IsKeyDown(KEY_J));
        input.y = (float)(IsKeyDown(KEY_K) - IsKeyDown(KEY_I));
        input.fire = IsKeyDown(KEY_RIGHT_CONTROL);
        input.focus = IsKeyDown(KEY_U);
        input.sidekick_left = input.sidekick_right = IsKeyDown(KEY_O);
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
    double save_retry_at = 0;
    char save_paths[GAME_SAVE_SLOTS][2048] = {{0}};
    if (!frames) {
        const char *root = getenv("LOCALAPPDATA");
        char directory[1800];
        int length = snprintf(directory, sizeof(directory), "%s/GalaxyDefenders", root ? root : ".");
        if (length > 0 && length < (int)sizeof(directory) && (DirectoryExists(directory) || MakeDirectory(directory) == 0)) {
            for (int i = 0; i < GAME_SAVE_SLOTS; ++i) {
                snprintf(save_paths[i], sizeof(save_paths[i]), "%s/slot%d.save", directory, i + 1);
                char backup[2048]; snprintf(backup, sizeof(backup), "%s/slot%d.save.bak", directory, i + 1);
                if (FileExists(save_paths[i]) || FileExists(backup)) {
                    if (!game_save_read(&game->saves[i], save_paths[i])) {
                        game->save_errors[i] = true;
                        console_log(console, "ERROR: could not read save slot %d", i + 1);
                    }
                } else if (i < 2) {
                    char legacy[2048]; snprintf(legacy, sizeof(legacy), "%s/%s.save", directory, i ? "coop" : "solo");
                    if (game_save_read(&game->saves[i], legacy)) game->saves_dirty |= 1u << i;
                }
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
    PlayerDevices devices;
    PadInput pads[INPUT_PADS] = {0};
    input_devices_init(&devices);
    if (!frames && !ds4_init()) console_log(console, "Native DS4 input unavailable; standard gamepads remain enabled.");
    console_log(console, "Ready. Type help; console commands and launch scripts share the same game.");
    while (!WindowShouldClose() && !console->quit && !game->menu.quit) {
        double frame_start = GetTime();
        if (IsKeyPressed(KEY_F3)) console->perf = !console->perf;
        update_audio(game, console, &assets);
        if (!frames) {
            poll_controllers(pads);
            poll_joining(game, &assets, &devices, pads, !console_open && !console->music_album && IsWindowFocused());
            if (!IsWindowFocused()) for (int i = 0; i < INPUT_PADS; ++i) pads[i].pressed = 0;
        }
        bool was_menu = game->menu.page != MENU_NONE;
        bool toggled = IsKeyPressed(KEY_GRAVE);
        if (toggled) {
            if (IsKeyDown(KEY_LEFT_SHIFT) || IsKeyDown(KEY_RIGHT_SHIFT)) {
                console_full = !console_open || !console_full;
                console_open = true;
            } else { console_open = !console_open; console_full = false; }
            while (GetCharPressed()) {} /* Don't insert the opening backtick. */
        }
        if (console->music_album) {
            console_open = false;
            music_album_input(console, game, &assets);
        } else if (console_open) {
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
            poll_menu(game, &assets, &devices, pads);
        } else if (!toggled) {
            if (IsKeyPressed(KEY_P) || IsKeyPressed(KEY_ESCAPE)) game_menu_open(game, MENU_PAUSE);
            bool restart = IsKeyPressed(KEY_R) || ((game->over || game->complete) && IsKeyPressed(KEY_D));
            for (int i = 0; i < GAME_PLAYERS; ++i) {
                int device = devices.device[i];
                if ((game->over || game->complete) && device >= 0 && device < INPUT_PADS && (pads[device].pressed & PAD_RETRY)) restart = true;
            }
            if (restart) {
                game_retry(game);
                if (assets.audio) { StopMusicStream(assets.music); PlayMusicStream(assets.music); }
            }
            if (IsKeyPressed(KEY_M) && assets.audio) { muted = !muted; SetMasterVolume(muted ? 0 : volume); }
        }
        int old_buildings = game->building_kills;
        int old_pickups = game->pickups_collected;
        int old_hits = game->armor_hits, old_shield_hits = game->shield_hits, old_deaths = game->deaths;
        int old_enemy_deaths[3];
        memcpy(old_enemy_deaths, game->enemy_deaths, sizeof(old_enemy_deaths));
        int old_weapon_shots[WEAPON_COUNT], old_impacts[4], old_enemy_shots[2], old_overheats = game->overheats;
        memcpy(old_weapon_shots, game->weapon_shots, sizeof(old_weapon_shots));
        memcpy(old_impacts, game->impact_sounds, sizeof(old_impacts));
        memcpy(old_enemy_shots, game->enemy_shots, sizeof(old_enemy_shots));
        if (!console->music_album && !console_open && !toggled && ((!was_menu && !game->paused) || game->menu.page == MENU_OVER || game->menu.page == MENU_CLEAR || game->menu.page == MENU_FINISH || game->menu.page == MENU_SHOP || game->transition_frames > 0) && (IsWindowFocused() || frames)) {
            if (!frames) for (int i = 0; i < GAME_PLAYERS; ++i) {
                int device = devices.device[i];
                game->players[i].input = device >= 0 && device < INPUT_PADS ? input_player(&pads[device]) :
                    (device == INPUT_KEYBOARD || device == INPUT_KEYBOARD_P2) ? poll_keyboard(i) : (PlayerInput){0};
            }
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
            for (int i = 0; i < 3; ++i) if (game->enemy_deaths[i] > old_enemy_deaths[i]) PlaySound(assets.explode[i]);
            if (game->armor_hits > old_hits) PlaySound(assets.hit);
            if (game->shield_hits > old_shield_hits) PlaySound(assets.shield_hit);
            if (game->deaths > old_deaths) PlaySound(assets.die);
        }
        if (assets.stage_index != game->stage.index && !stage_assets_load(&assets, game->stage.index)) {
            console_log(console, "ERROR: stage assets failed to load");
            command_failed = true;
            break;
        }
        if (!frames && game->saves_dirty && GetTime() >= save_retry_at) {
            for (int i = 0; i < GAME_SAVE_SLOTS; ++i) if (game->saves_dirty & (1u << i)) {
                if (save_paths[i][0] && game_save_write(&game->saves[i], save_paths[i])) {
                    game->saves_dirty &= ~(1u << i);
                    game->save_errors[i] = false;
                } else {
                    if (!game->save_errors[i]) console_log(console, "ERROR: could not write save slot %d; retrying", i + 1);
                    game->save_errors[i] = true;
                }
            }
            save_retry_at = game->saves_dirty ? GetTime() + 5 : 0;
        }
#ifdef GALAXY_MEMORY
        BeginDrawing();
#else
        BeginTextureMode(canvas);
#endif
        draw_game(game, &assets);
        draw_menu(game, &assets, menu_frame++, &devices);
        if (game->menu.page != MENU_SHOP && game->menu.page != MENU_FINISH && game->menu.page != MENU_CLEAR) {
            int owner = game->players[0].joined ? 0 : 1;
            const char *hint = game->menu.page == MENU_NONE ? "Enter / Options   Pause     F2   Keyboard P2" :
                TextFormat("[%s]  Confirm     [%s]  Back     Enter / Options   Join",
                    shop_button(devices.device[owner], owner, MENU_CONFIRM), shop_button(devices.device[owner], owner, MENU_BACK));
            text_center(assets.ui_font, hint, 960, 1040, 19, LIGHTGRAY);
        }
        if (game->active_slot >= 0 && game->active_slot < GAME_SAVE_SLOTS && game->save_errors[game->active_slot])
            text_center(assets.ui_font, "Save unavailable - progress has not been written", 960, 940, 24, RED);
        if (game->transition_frames > 0) {
            float alpha = game->transition_frames > 15 ? (30 - game->transition_frames) / 15.0f : game->transition_frames / 15.0f;
            DrawRectangle(ARENA_X, 0, GAME_WIDTH, GAME_HEIGHT, Fade(BLACK, alpha));
        }
        if (console->music_album) draw_music_album(console, &assets);
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
    if (!frames) ds4_shutdown();
    CloseWindow();
    return result;
}
