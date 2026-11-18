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
    Texture2D portraits[3], title_logo, title_squad;
    Texture2D particle_dot, particle_triangle;
    Texture2D water;
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
    Texture2D pickups[3], medals[5];
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
    Font console_font, ui_font, heading_font;
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
static const Color ink = {12, 18, 29, 255}, rule = {51, 64, 80, 255}, surface = {25, 36, 51, 255};
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
    if (IsTextureValid(assets->title_logo)) UnloadTexture(assets->title_logo);
    if (IsTextureValid(assets->title_squad)) UnloadTexture(assets->title_squad);
    if (IsTextureValid(assets->asteroid_cracks)) UnloadTexture(assets->asteroid_cracks);
    for (int i = 0; i < 5; ++i) if (IsTextureValid(assets->medals[i])) UnloadTexture(assets->medals[i]);
    for (int i = 0; i < 3; ++i) if (IsTextureValid(assets->pickups[i])) UnloadTexture(assets->pickups[i]);
    if (IsTextureValid(assets->boss4_cover)) UnloadTexture(assets->boss4_cover);
    if (IsTextureValid(assets->boss5_cover)) UnloadTexture(assets->boss5_cover);
    if (IsTextureValid(assets->boss6_center)) UnloadTexture(assets->boss6_center);
    for (int i = 0; i < 4; ++i) if (IsTextureValid(assets->scenery[i])) UnloadTexture(assets->scenery[i]);
    if (assets->console_font.texture.id && assets->console_font.texture.id != GetFontDefault().texture.id) UnloadFont(assets->console_font);
    if (assets->heading_font.texture.id && assets->heading_font.texture.id != GetFontDefault().texture.id) UnloadFont(assets->heading_font);
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
    for (int i = 0; i < 3; ++i)
        if (IsTextureValid(assets->portraits[i])) UnloadTexture(assets->portraits[i]);
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
    int ui_glyphs[97];
    for (int i = 0; i < 95; ++i) ui_glyphs[i] = 32 + i;
    ui_glyphs[95] = 0x00a5;
    ui_glyphs[96] = 0x00b2;
    assets->ui_font = LoadFontEx(asset_path("fonts/NotoSans-Regular.ttf"), 64, ui_glyphs, 97);
    assets->heading_font = LoadFontEx(asset_path("fonts/NotoSans-Bold.ttf"),64,ui_glyphs,97);
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
    if (!IsFontValid(assets->heading_font) || assets->heading_font.texture.id == GetFontDefault().texture.id ||
        !IsFontValid(assets->ui_font) || assets->ui_font.texture.id == GetFontDefault().texture.id ||
        !IsFontValid(assets->console_font) || assets->console_font.texture.id == GetFontDefault().texture.id) {
        console_log(console, "ERROR: required UI/console font could not load; default raylib text is disabled.");
        return false;
    }
    SetTextureFilter(assets->heading_font.texture,TEXTURE_FILTER_BILINEAR);
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
    assets->hostile_shot = texture_load("textures/EnemyShot.png");
    assets->particle_dot = texture_load("textures/DotParticle.png");
    assets->particle_triangle = texture_load("textures/TriangleParticle.png");
    assets->title_logo = texture_load("textures/GalaxyDefendersLogo.png");
    static const char *portrait_files[] = {"textures/KazukiPortrait.png","textures/RabbitPortrait.png","textures/GunthorPortrait.png"};
    static const Rectangle squad_positions[] = {{101,0,286,286},{-8,29,245,245},{251,18,252,252}};
    Image squad = GenImageColor(488,246,BLANK);
    for (int i=2; i>=0; --i) {
        Image pilot = LoadImage(asset_path(portrait_files[i]));
        if (IsImageValid(pilot)) {
            assets->portraits[i] = LoadTextureFromImage(pilot);
            SetTextureFilter(assets->portraits[i],TEXTURE_FILTER_BILINEAR);
            ImageDraw(&squad,pilot,(Rectangle){0,0,pilot.width,pilot.height},squad_positions[i],WHITE);
            UnloadImage(pilot);
        }
    }
    /* Feather just the outer shoulders, retaining the portraits' silhouette alpha. */
    Color *pixels = squad.data;
    for (int y=0; y<squad.height; ++y) {
        for (int x=0; x<24; ++x) {
            Color *row = pixels + y*squad.width;
            row[x].a = row[x].a*x/24;
            row[squad.width-1-x].a = row[squad.width-1-x].a*x/24;
        }
    }
    assets->title_squad = LoadTextureFromImage(squad);
    SetTextureFilter(assets->title_squad,TEXTURE_FILTER_BILINEAR);
    UnloadImage(squad);
    assets->water = texture_load("textures/Water.png");
    bool valid = IsTextureValid(assets->pyramid_turret) && IsTextureValid(assets->cutter_beam) && IsTextureValid(assets->rotate_turret_base) && IsTextureValid(assets->splitter_right) && IsTextureValid(assets->shot) && IsTextureValid(assets->hostile_shot);
    for (int i = 0; i < GAME_CHASSIS; ++i) valid = valid && IsTextureValid(assets->ships[i]);
    valid = valid && IsTextureValid(assets->particle_dot) && IsTextureValid(assets->particle_triangle);
    for (int i = 0; i < ENEMY_KIND_COUNT; ++i)
        if (i != ENEMY_BOSS8_COLLISION) valid = valid && IsTextureValid(assets->enemies[i]);
    for (int i = 0; i < ENEMY_KIND_COUNT; ++i)
        if (enemy_corpse_name((EnemyKind)i)) valid = valid && IsTextureValid(assets->enemy_corpses[i]);
    for (int i = 0; i < 3; ++i) valid = valid && IsTextureValid(assets->portraits[i]);
    valid = valid && IsTextureValid(assets->water) && IsTextureValid(assets->title_logo) && IsTextureValid(assets->title_squad);
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

typedef enum { GLYPH_CONFIRM, GLYPH_BACK, GLYPH_READY, GLYPH_START } ButtonGlyph;

static int key_prompt(Font font, int x, int y, const char *key, const char *action) {
    int width = (int)fmaxf(30, MeasureTextEx(font,key,20,0).x + 14);
    DrawRectangleRounded((Rectangle){x,y+3,width,29}, .28f, 6, (Color){95,113,135,255});
    DrawRectangleRounded((Rectangle){x,y,width,28}, .28f, 6, (Color){213,224,235,255});
    text_center(font,key,x+width/2,y+2,20,(Color){23,32,46,255});
    DrawTextEx(font,action,(Vector2){x+width+9,y+1},23,0,WHITE);
    return width + 9 + (int)MeasureTextEx(font,action,23,0).x;
}

static int button_prompt(Font font, int x, int y, int device, int slot, ButtonGlyph button, const char *action) {
    static const char *keys[2][4] = {{"X","Z","C","Enter"},{"R Ctrl","Backspace","R Shift","F2"}};
    if (device < 0 || device >= INPUT_PADS) return key_prompt(font,x,y,keys[slot][button],action);
    static const char *letters[] = {"A","B","X"};
    const char *pad = device < 4 ? GetGamepadName(device) : NULL;
    bool ps = device >= 4 || (pad && (strstr(pad,"Wireless Controller") || strstr(pad,"PS4") ||
        strstr(pad,"DualSense") || strstr(pad,"DualShock") || strstr(pad,"PS5")));
    if (button == GLYPH_START) {
        DrawRectangleRounded((Rectangle){x,y+4,39,23},1,8,(Color){76,91,110,255});
        for (int i = 0; i < 3; ++i) DrawRectangle(x+12,y+9+i*5,15,2,WHITE);
    } else {
        int cx=x+15, cy=y+15;
        DrawCircle(cx,cy,15,(Color){61,75,94,255});
        if (!ps) text_center(font,letters[button],cx,y+1,23,WHITE);
        else if (button == GLYPH_CONFIRM) {
            DrawLineEx((Vector2){cx-6,cy-6},(Vector2){cx+6,cy+6},3,SKYBLUE);
            DrawLineEx((Vector2){cx-6,cy+6},(Vector2){cx+6,cy-6},3,SKYBLUE);
        } else if (button == GLYPH_BACK) DrawRing((Vector2){cx,cy},6,9,0,360,32,(Color){255,155,141,255});
        else DrawRectangleLinesEx((Rectangle){cx-7,cy-7,14,14},3,(Color){251,174,213,255});
    }
    int offset = button == GLYPH_START ? 48 : 39;
    DrawTextEx(font,action,(Vector2){x+offset,y+1},23,0,WHITE);
    return offset + (int)MeasureTextEx(font,action,23,0).x;
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

static void resource_meter(Font font, int x, int y, const char *label, float value, float capacity, Color color) {
    DrawTextEx(font, label, (Vector2){x, y}, 21, 0, WHITE);
    const char *amount = TextFormat("%.0f / %.0f", value, capacity);
    DrawTextEx(font, amount, (Vector2){x + 240 - MeasureTextEx(font, amount, 18, 0).x, y + 3}, 18, 0, dim);
    DrawRectangle(x, y + 26, 240, 16, (Color){45,55,69,255});
    if (capacity > 0) DrawRectangle(x, y + 26, (int)(240 * fminf(1, fmaxf(0, value / capacity))), 16, color);
}

static const char *pilot_names[] = {"Kazuki", "Rabbit", "Gunthor"};

static void draw_pilot_rail(const Assets *assets, const Player *p, const Player *preview, int slot, bool shopping) {
    Font font = assets->ui_font;
    int x = slot ? ARENA_X + GAME_WIDTH : 0, left = x + 28, width = ARENA_X - 56;
    Color highlight = player_colors[slot];
    DrawRectangle(x, 0, ARENA_X, GAME_HEIGHT, ink);
    DrawRectangle(slot ? x : x + ARENA_X - 2, 0, 2, GAME_HEIGHT, rule);
    DrawRectangle(left, 32, 58, 36, p->joined ? highlight : rule);
    text_center(font, slot ? "P2" : "P1", left + 29, 33, 27, ink);
    if (!p->joined) {
        text_center(font, "Join flight", x + 240, 488, 36, WHITE);
        button_prompt(font,x+105,543,0,slot,GLYPH_START,"Join");
        key_prompt(font,x+245,543,slot ? "F2" : "Enter","Join");
        return;
    }
    DrawTextEx(font, pilot_names[p->pilot], (Vector2){left + 70, 26}, 38, 0, WHITE);
    const char *credits = shopping ? TextFormat("%d cr",p->money) : TextFormat("%d pts",p->score);
    DrawTextEx(font, credits, (Vector2){left + width - MeasureTextEx(font, credits, 27, 0).x, 38}, 27, 0, highlight);
    DrawLine(left, 88, left + width, 88, rule);
    sprite(assets->portraits[p->pilot], left + 82, 204, 164, 0, WHITE);
    resource_meter(font, left + 184, 115, "Armor", preview->health, game_chassis[preview->chassis].armor, highlight);
    resource_meter(font, left + 184, 172, "Shield", preview->shield_health,
        preview->shield < 0 ? 0 : game_shields[preview->shield].capacity, (Color){130,183,240,255});
    resource_meter(font, left + 184, 229, "Energy", preview->energy, game_generators[preview->generator].capacity, (Color){117,214,188,255});
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

static const char *loadout_slots[] = {"Chassis", "Generator", "Shield", "Primary", "Support", "Sidekick"};

static Texture2D equipment_icon(const Assets *assets, int category, int id) {
    if (id < 0) return assets->item_none;
    switch (category) {
        case 0: return assets->shop[6+id];
        case 1: return assets->generator_icons[id];
        case 2: return assets->shield_icons[id];
        default: return assets->weapon_icons[id];
    }
}

enum { LOADOUT_TOP = 312, LOADOUT_ROW = 49 };

static void draw_slot_row(const Assets *assets, const Player *p, int x, int y, int cat, bool focused, Color color) {
    Font font = assets->ui_font;
    int id = game_shop_equipped(p,cat);
    if (focused) {
        DrawRectangle(x,y,424,47,surface);
        DrawRectangle(x,y,3,47,color);
    }
    Texture2D icon = id < 0 ? assets->shop[cat] : equipment_icon(assets,cat,id);
    sprite(icon,x+24,y+23,32,0,id < 0 ? dim : WHITE);
    DrawTextEx(font,loadout_slots[cat],(Vector2){x+49,y+13},20,0,dim);
    const char *name = shop_item_name(cat,id);
    bool show_mark = id >= 0 && cat >= 3;
    float size = fminf(24,24*(show_mark ? 220 : 268)/fmaxf(1,MeasureTextEx(font,name,24,0).x));
    DrawTextEx(font,name,(Vector2){x+148,y+(47-size)/2},size,0,id < 0 ? dim : WHITE);
    if (show_mark)
        DrawTextEx(font,TextFormat("Mk %d",1+game_shop_equipped_level(p,cat)),
            (Vector2){x+375,y+14},18,0,color);
    DrawLine(x,y+47,x+424,y+47,rule);
}

static void draw_loadout(const Assets *assets, const Player *p, int x, int focus, Color color) {
    for (int c = 0; c < 6; ++c)
        draw_slot_row(assets,p,x,LOADOUT_TOP+c*LOADOUT_ROW,c,c == focus,color);
}

static void draw_flight_rail(const Game *game, const Assets *assets, int slot) {
    const Player *p = &game->players[slot];
    draw_pilot_rail(assets,p,p,slot,false);
    int x = (slot ? ARENA_X + GAME_WIDTH : 0) + 28;
    Font font = assets->ui_font;
    if (slot == 0) {
        int index = game->stage.index;
        const char *number = index < 12 ? TextFormat("Stage %d",index+1) : TextFormat("Bonus stage %d",index-11);
        DrawLine(x,947,x+424,947,rule);
        DrawTextEx(font,number,(Vector2){x,961},24,0,dim);
        DrawTextEx(font,stage_definitions[index].display_name,(Vector2){x,996},30,0,WHITE);
    }
    if (!p->joined) return;
    DrawLine(x,298,x+424,298,rule);
    draw_loadout(assets,p,x,-1,player_colors[slot]);
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
    EndScissorMode();
    for (int i = 0; i < GAME_ENEMIES; ++i) {
        const Enemy *enemy = &game->enemies[i];
        if (!enemy->active || !enemy_is_boss(enemy->kind)) continue;
        DrawRectangle(ARENA_X + 170, 118, 620, 12, (Color){68, 45, 45, 255});
        DrawRectangle(ARENA_X + 170, 118, (int)(620 * enemy->health / enemy->health_max), 12, danger);
    }
    for (int i=0; i<GAME_PLAYERS; ++i) draw_flight_rail(game,assets,i);
    if (game->stage.secret_countdown)
        DrawRectangle(0, 0, CANVAS_WIDTH, CANVAS_HEIGHT, Fade(BLACK, fminf(1, game->stage.secret_countdown / 120.0f)));
    if (game->secret_finish_frames || game->secret_fade_frames) {
        float alpha = game->secret_fade_frames ? game->secret_fade_frames / 15.0f :
            game->secret_finish_frames <= 30 ? fminf(0.5f, (game->secret_finish_frames - 1) / 15.0f) :
            fminf(1, 0.5f + (game->secret_finish_frames - 30) / 15.0f);
        DrawRectangle(0, 0, CANVAS_WIDTH, CANVAS_HEIGHT, Fade(BLACK, alpha));
    }
}

static const char *item_description(int category, int id) {
    static const char *const chassis[] = {
        "The flight school's old faithful.", "A light frame with a quick first step.",
        "Heavy plating for crowded lanes.", "Built to slip through closing gaps.",
        "A sturdy hull with a sprinter's legs.", "Quick off the line. Quick on the turn.",
        "A fast bruiser with armor to spare.", "A flying bunker. Give it room to turn.",
        "Race-bred handling in a lean hull."
    };
    static const char *const generators[] = {
        "The fleet's trusty starter cell.", "Copper rails feed a steady current.",
        "A deep reserve for long bursts.", "A quick refill between volleys.",
        "A pulse core for stop-start firing.", "A compact bank of resonant coils.",
        "A spinning core keeps the guns fed.", "Stores a long burst in one big bank.",
        "A deep cell with a lively recharge.", "Turns hull impacts into energy.",
        "Coins give this core an extra spark.", "A tiny star behind armored glass."
    };
    static const char *const shields[] = {
        "A light screen for a small hull.", "Woven field lines catch stray fire.",
        "An electric veil around the cockpit.", "A tuned field wastes little charge.",
        "A compact screen that rebuilds fast.", "Field coils brace for the next hit.",
        "A dense bubble for the front line.", "A deep buffer for heavy incoming fire.",
        "Precision coils stretch every charge."
    };
    static const char *const weapons[WEAPON_COUNT] = {
        [WEAPON_FRONT_LASER]="Straight bolts for a clean firing lane.",
        [WEAPON_SPREAD_LASER]="Fans laser fire across the approach.",
        [WEAPON_PLASMA]="Higher marks burst on impact.",
        [WEAPON_FLAME]="A hot spray that cools as it travels.",
        [WEAPON_LIGHTNING]="Arcs jump from one target to the next.",
        [WEAPON_BEAM]="Hold a column of light on target.",
        [WEAPON_MISSILE]="Rockets punch straight up the lane.",
        [WEAPON_DRUNK_MISSILE]="A rocket with a wandering flight path.",
        [WEAPON_SEEK_BOMB]="Hunts for a target after launch.",
        [WEAPON_BOOMERANG]="A spinning disc cuts through a crowd.",
        [WEAPON_VULCAN]="Rapid-fire barrels lead moving targets.",
        [WEAPON_BOMBLET]="Scatters small bombs around the ship.",
        [WEAPON_MINI_SHOT]="A small gun for each wing.",
        [WEAPON_CHARGE_SHOT]="Hold to build a heavier shot.",
        [WEAPON_BLADE]="Spinning blades sweep the flanks.",
        [WEAPON_MISSILE_VOLLEY]="Wing pods loose a flight of rockets.",
        [WEAPON_MISSILE_LAUNCHER]="A rocket tube under each wing.",
        [WEAPON_LASER_SHOT]="Wing-mounted guns add laser fire."
    };
    if (id < 0) return category == 2 ? "No shield fitted." :
        category == 4 ? "No support weapon fitted." : "No wing pods fitted.";
    return category == 0 ? chassis[id] : category == 1 ? generators[id] :
        category == 2 ? shields[id] : weapons[id];
}

static void draw_item_tip(const Game *game, const Assets *assets, int slot, int anchor, int cat, int item, int mark, int offset) {
    const GameMenu *menu = &game->menu;
    if (menu->ready[slot] || menu->shop_purchase[0] || menu->shop_purchase[1]) return;
    Font font = assets->ui_font;
    Color color = player_colors[slot];
    const int width = 374, x = slot ? 1440-40-width : ARENA_X+40;
    int y = anchor-42;
    int row_edge = slot ? ARENA_X+GAME_WIDTH+28+(menu->shop_catalog[slot] ? (menu->shop_tiers[slot] ? 30 : 14) : 0) : ARENA_X-28;
    row_edge += offset;
    DrawLine(row_edge,anchor,slot ? x+width : x,anchor,color);
    DrawCircle(row_edge,anchor,3,color);
    struct { const char *label; char value[40]; } stats[3] = {0};
    int count = 0;
    if (item >= 0 && cat == 0) {
        const Chassis *c = &game_chassis[item];
        stats[0].label = "Armor";
        snprintf(stats[0].value,40,"%g HP",c->armor);
        stats[1].label = "Acceleration";
        float thrust = c->speed * (game->players[slot].pilot == 0 ? 1.2f : 1);
        snprintf(stats[1].value,40,"%.0f px/s\xc2\xb2",thrust*GAME_HZ*GAME_HZ);
        stats[2].label = "Impact resistance";
        snprintf(stats[2].value,40,"%.0f%%",100*c->collision_resistance);
        count = 3;
    } else if (item >= 0 && cat == 1) {
        const Generator *g = &game_generators[item];
        stats[0].label = "Energy capacity";
        snprintf(stats[0].value,40,"%g",g->capacity);
        stats[1].label = "Energy recovery";
        snprintf(stats[1].value,40,"%g/s",g->regen*GAME_HZ);
        count = 2;
        if (g->on_coin || g->on_collision) {
            stats[2].label = g->on_coin ? "Energy per coin" : "Energy per impact HP";
            snprintf(stats[2].value,40,"+%g",g->on_coin ? g->on_coin : g->on_collision);
            ++count;
        }
    } else if (item >= 0 && cat == 2) {
        const Shield *h = &game_shields[item];
        stats[0].label = "Shield capacity";
        snprintf(stats[0].value,40,"%g HP",h->capacity);
        stats[1].label = "Recharge efficiency";
        snprintf(stats[1].value,40,"%.0f%%",100*h->efficiency);
        count = 2;
    } else if (item >= 0) {
        const WeaponLevel *w = &game_weapons[item].levels[mark];
        float min_hit = INFINITY, max_hit = 0, dps = 0;
        for (int i=0; i<w->count; ++i) {
            const WeaponEmitter *e = &w->emitters[i];
            float hit = e->damage * (game->players[slot].pilot == 2 ? 1.15f : 1);
            min_hit = fminf(min_hit,hit);
            max_hit = fmaxf(max_hit,hit);
            float ticks = fmaxf(1,ceilf((e->reload+.5f*e->random_reload)*GAME_HZ));
            dps += hit*GAME_HZ/(e->toggle ? 1 : ticks);
        }
        stats[0].label = item == WEAPON_CHARGE_SHOT ? "Full-charge damage" : "Damage per hit";
        if (min_hit == max_hit) snprintf(stats[0].value,40,"%.3g HP",max_hit);
        else snprintf(stats[0].value,40,"%.3g - %.3g HP",min_hit,max_hit);
        count = 1;
        if (cat == 3) {
            stats[1].label = "Estimated DPS";
            snprintf(stats[1].value,40,"%.3g HP/s",dps);
            ++count;
        } else if (cat == 5) {
            const WeaponEmitter *e = w->emitters;
            if (item == WEAPON_BLADE) {
                stats[1].label = "Sweep reach";
                snprintf(stats[1].value,40,"%.0f px",38*e->scale_override);
                stats[2].label = e->lifetime > 9 ? "Return hit" : "Recovery";
                if (e->lifetime > 9) snprintf(stats[2].value,40,"%.3g HP",max_hit*1.25f);
                else snprintf(stats[2].value,40,"%.2g s",e->reload);
                count = 3;
            } else if (item == WEAPON_CHARGE_SHOT) {
                stats[1].label = "Full charge";
                snprintf(stats[1].value,40,"%.2g s",(float)e->charge_frames/GAME_HZ);
                count = 2;
            } else {
                stats[1].label = "Shots per wing";
                snprintf(stats[1].value,40,"%d",w->count);
                count = 2;
                if (item == WEAPON_MISSILE_LAUNCHER && e->radius > 0) {
                    stats[2].label = "Blast radius";
                    snprintf(stats[2].value,40,"%.0f px",e->radius);
                    ++count;
                }
            }
            if (e->bounces) {
                stats[2].label = "Targets per shot";
                snprintf(stats[2].value,40,"%d",1+e->bounces);
                count = 3;
            }
        }
    }
    int height = count ? 101 + count*26 : 84;
    DrawRectangle(x,y,width,height,ink);
    DrawRectangleLinesEx((Rectangle){x,y,width,height},1,rule);
    DrawRectangle(slot ? x+width-3 : x,y,3,height,color);
    const char *name = shop_item_name(cat,item);
    if (cat >= 3 && item >= 0 && game_weapons[item].level_count > 1) name = TextFormat("%s  Mk %d",name,mark+1);
    DrawTextEx(font,name,(Vector2){x+16,y+10},25,0,WHITE);
    const char *desc = item_description(cat,item);
    float size = fminf(20,20*(width-32)/fmaxf(1,MeasureTextEx(font,desc,20,0).x));
    DrawTextEx(font,desc,(Vector2){x+16,y+49},size,0,dim);
    if (count) DrawLine(x+16,y+82,x+width-16,y+82,rule);
    for (int i=0; i<count; ++i) {
        int top = y+92+i*26;
        DrawTextEx(font,stats[i].label,(Vector2){x+16,top},19,0,dim);
        float value_x = x+width-16-MeasureTextEx(font,stats[i].value,20,0).x;
        DrawTextEx(font,stats[i].value,(Vector2){value_x,top-1},20,0,WHITE);
    }
}

static void draw_shop_catalog(const Game *game, const Assets *assets, int slot, int device, int left, int offset) {
    const Player *p = &game->players[slot];
    const GameMenu *menu = &game->menu;
    int cat = menu->shop_category[slot], owned = game_shop_equipped(p,cat), width = ARENA_X-56;
    Font font = assets->ui_font;
    Color highlight = player_colors[slot];
    int stock[WEAPON_COUNT + 1], count = game_shop_stock(game, slot, cat, stock);
    int item = menu->shop_item[slot], selected = 0;
    for (int i = 0; i < count; ++i) if (stock[i] == item) selected = i;
    bool tiers = menu->shop_tiers[slot];
    int mark = menu->shop_level[slot];
    int equipped_level = game_shop_equipped_level(p,cat);
    int rows = tiers ? game_weapons[item].level_count : count;
    int focus = tiers ? mark : selected;
    int first = tiers ? menu->shop_first_tier[slot] : menu->shop_first_item[slot];
    draw_slot_row(assets,p,left,LOADOUT_TOP,cat,false,highlight);
    left += offset;
    int y = LOADOUT_TOP + LOADOUT_ROW + 14;
    int indent = tiers ? 30 : 14;
    if (tiers) {
        sprite(equipment_icon(assets,cat,item),left+31,y+16,26,0,WHITE);
        DrawTextEx(font,shop_item_name(cat,item),(Vector2){left+55,y+2},23,0,WHITE);
        y += 42;
    }
    int list_top = y;
    bool show_select = true;
    int anchor = y+(focus-first)*LOADOUT_ROW+23;
    for (int i=first; i<rows && i<first+SHOP_VISIBLE_ROWS; ++i, y+=LOADOUT_ROW) {
        int id = tiers ? item : stock[i];
        bool focused = i == focus && !menu->ready[slot];
        int marks = game_shop_owned_marks(p,cat,id);
        bool equipped = id == owned && (!tiers || i == equipped_level);
        bool purchased = tiers ? i < marks : marks > 0;
        bool expandable = !tiers && cat >= 3 && id >= 0 && game_weapons[id].level_count > 1;
        const char *status = equipped ? "Equipped" : purchased ? "Owned" : "";
        Color status_color = equipped ? highlight : purchased ? dim : WHITE;
        if (!expandable && !purchased) {
            int price = game_shop_price(p,cat,id,tiers ? i : 0);
            status = TextFormat("%d cr",price);
            if (price > p->money) {
                status_color = (Color){212,151,148,255};
                if (focused) show_select = false;
            }
        }
        int right = left + width - 8 - (expandable ? 26 : 0) - (int)MeasureTextEx(font,status,21,0).x;
        if (focused) DrawRectangle(left+indent,y,width-indent,47,surface);
        DrawLine(left+indent,y+47,left+width,y+47,rule);
        if (focused) DrawRectangle(left+indent,y,3,47,highlight);
        int name_x = left+indent+(tiers ? 16 : 44);
        const char *name;
        if (tiers) name = TextFormat("Mk %d",i+1);
        else {
            sprite(equipment_icon(assets,cat,id),left+indent+21,y+23,30,0,WHITE);
            name = shop_item_name(cat,id);
        }
        if (expandable) {
            int cx = left+width-17;
            int levels = game_weapons[id].level_count;
            for (int tier=0; tier<levels; ++tier) {
                Color notch = tier < marks ? dim : rule;
                if (equipped && tier <= equipped_level) notch = highlight;
                DrawRectangle(cx-16-(levels-tier)*7,y+36,5,3,notch);
            }
            DrawLineEx((Vector2){cx-4,y+17},(Vector2){cx+2,y+23},2,highlight);
            DrawLineEx((Vector2){cx+2,y+23},(Vector2){cx-4,y+29},2,highlight);
        }
        float size = fminf(25,25*(right-name_x-10)/fmaxf(1,MeasureTextEx(font,name,25,0).x));
        DrawTextEx(font,name,(Vector2){name_x,y+(47-size)/2},size,0,WHITE);
        DrawTextEx(font,status,(Vector2){right,y+(expandable ? 6 : 12)},21,0,status_color);
    }
    DrawRectangle(left+indent-8,list_top,2,y-list_top-2,rule);
    if (rows > SHOP_VISIBLE_ROWS) {
        int track = y-list_top-2;
        int thumb = track*SHOP_VISIBLE_ROWS/rows;
        int position = first*(track-thumb)/(rows-SHOP_VISIBLE_ROWS);
        DrawRectangle(left+width+6,list_top,4,track,rule);
        DrawRectangle(left+width+6,list_top+position,4,thumb,dim);
    }
    draw_item_tip(game,assets,slot,anchor,cat,item,mark,offset);
    if (!menu->ready[slot] && !menu->shop_purchase[slot]) {
        int step = show_select ? button_prompt(font,left,y+18,device,slot,GLYPH_CONFIRM,"Select")+22 : 0;
        button_prompt(font,left+step,y+18,device,slot,GLYPH_BACK,"Cancel");
    }
}

static void draw_shop_panel(const Game *game, const Assets *assets, int slot, int device, int offset) {
    Font font = assets->ui_font;
    int x = slot ? ARENA_X + GAME_WIDTH : 0, left = x + 28, width = ARENA_X - 56;
    Color highlight = player_colors[slot];
    const Player *p = &game->players[slot];
    const GameMenu *menu = &game->menu;
    const Player *preview = shop_preview.players[slot].joined ? &shop_preview.players[slot] : p;
    draw_pilot_rail(assets,p,preview,slot,true);
    if (!p->joined) return;
    int cat = menu->shop_category[slot];
    DrawLine(left, 298, left + width, 298, rule);
    if (menu->shop_catalog[slot]) {
        draw_shop_catalog(game,assets,slot,device,left,offset);
    } else {
        draw_loadout(assets,p,left+offset,menu->ready[slot] ? -1 : cat,highlight);
        int mark = game_shop_equipped_level(p,cat);
        draw_item_tip(game,assets,slot,LOADOUT_TOP+cat*LOADOUT_ROW+23,cat,game_shop_equipped(p,cat),mark,offset);
        if (!menu->ready[slot]) button_prompt(font,left+offset,LOADOUT_TOP+6*LOADOUT_ROW+18,device,slot,GLYPH_CONFIRM,"Select");
    }
    DrawLine(left,974,left+width,974,rule);
    button_prompt(font,left,993,device,slot,GLYPH_READY,"Ready");
    int check = left + width - 34;
    DrawRectangleLinesEx((Rectangle){check,997,31,31}, 2, menu->ready[slot] ? highlight : dim);
    if (menu->ready[slot]) {
        DrawLineEx((Vector2){check+5,1012}, (Vector2){check+13,1020}, 4, highlight);
        DrawLineEx((Vector2){check+13,1020}, (Vector2){check+26,1004}, 4, highlight);
    }
}

static void dialog_frame(int y, Color border) {
    DrawRectangle(644,y,632,228,(Color){18,27,40,255});
    DrawRectangleLinesEx((Rectangle){644,y,632,228},2,border);
    DrawLine(672,y+156,1248,y+156,rule);
}

static void draw_shop_purchase(const Game *game, Font font, int slot, int device, int y) {
    const Player *p = &game->players[slot];
    int cat = game->menu.shop_category[slot], item = game->menu.shop_item[slot];
    int level = game->menu.shop_level[slot], cost = game_shop_cost(game, slot);
    bool upgrade = game_shop_owned_marks(p,cat,item) > 0;
    Color accent = player_colors[slot];
    dialog_frame(y,accent);
    DrawTextEx(font, TextFormat("P%d   /   %s", slot + 1, upgrade ? "CONFIRM UPGRADE" :
        item < 0 ? "CONFIRM UNEQUIP" : "CONFIRM PURCHASE"), (Vector2){672,y+18}, 22, 1, accent);
    const char *name = item < 0 ? shop_item_name(cat, game_shop_equipped(p, cat)) : shop_item_name(cat, item);
    DrawTextEx(font, name, (Vector2){672,y+55}, 36, 0, WHITE);
    if (cat >= 3 && item >= 0) DrawTextEx(font, TextFormat("Unlock through Mk %d", level+1), (Vector2){672,y+111}, 25, 0, dim);
    const char *price = cost < 0 ? TextFormat("Credit  +%d cr", -cost) : TextFormat("Cost  %d cr", cost);
    DrawTextEx(font, price, (Vector2){1248 - MeasureTextEx(font, price, 26, 0).x,y+110}, 26, 0, cost > p->money ? danger : WHITE);
    if (cost > p->money) DrawTextEx(font,"Insufficient credits",(Vector2){672,y+174},26,0,danger);
    else button_prompt(font,672,y+174,device,slot,GLYPH_CONFIRM,"Select");
    button_prompt(font,1030,y+174,device,slot,GLYPH_BACK,"Cancel");
}

static int menu_slide_offset(int elapsed, int direction) {
    float remaining = fmaxf(0,1-elapsed/8.0f);
    return (int)roundf(direction*22*remaining*remaining);
}

static void draw_shop(const Game *game, const Assets *assets, int frame, const PlayerDevices *devices, bool entering) {
    ClearBackground(BLACK);
    for (int layer = 0; layer < 2; ++layer) for (int i = 0; i < 10; ++i) {
        float x = ARENA_X + (i * 193 + layer * 311) % GAME_WIDTH;
        float y = fmodf(i * 137.0f + frame * (layer ? 2.1f : 3.5f), GAME_HEIGHT);
        sprite(assets->star, x, y, assets->star.width * (layer ? 0.6f : 0.4f), 0, WHITE);
    }
    BeginScissorMode(ARENA_X, 0, GAME_WIDTH, GAME_HEIGHT);
    draw_enemies(&shop_preview, assets, enemy_draw_layer(ENEMY_BALL));
    draw_combat_sprites(&shop_preview, assets);
    EndScissorMode();
    static int previous_depth[GAME_PLAYERS], changed_at[GAME_PLAYERS], direction[GAME_PLAYERS];
    for (int slot = 0; slot < GAME_PLAYERS; ++slot) {
        int depth = !game->menu.shop_catalog[slot] ? 0 : game->menu.shop_tiers[slot] ? 2 : 1;
        if (entering || !game->players[slot].joined) direction[slot] = 0;
        else if (depth != previous_depth[slot]) {
            direction[slot] = depth > previous_depth[slot] ? 1 : -1;
            changed_at[slot] = frame;
        }
        previous_depth[slot] = depth;
        int offset = menu_slide_offset(frame-changed_at[slot],direction[slot]);
        draw_shop_panel(game, assets, slot, devices->device[slot], offset);
    }
    bool both = game->players[0].joined && game->players[1].joined &&
        game->menu.shop_purchase[0] && game->menu.shop_purchase[1];
    for (int slot = 0; slot < GAME_PLAYERS; ++slot)
        if (game->players[slot].joined && game->menu.shop_purchase[slot])
            draw_shop_purchase(game, assets->ui_font, slot, devices->device[slot], both ? 300 + slot * 250 : 426);
}

static void menu_sky(int frame) {
    ClearBackground(ink);
    DrawRectangleGradientV(0,0,CANVAS_WIDTH,CANVAS_HEIGHT,(Color){15,24,42,255},(Color){8,13,23,255});
    for (int i=0; i<95; ++i) {
        int x = (i*743+71)%CANVAS_WIDTH;
        int y = (i*347+frame/(3+i%4))%CANVAS_HEIGHT;
        DrawCircle(x,y,i%5==0 ? 2 : 1,(Color){106,129,158,255});
    }
}

/* The original sample ship advances along its heading while turning ~0.025
   radians per tick. Reconstruct each laser from the pose at its launch tick. */
static Vector2 title_ship_position(Vector2 center, float heading) {
    return (Vector2){center.x + 61*sinf(heading), center.y - 61*cosf(heading)};
}

static void draw_title_ship(const Assets *assets, int slot, int tick, float clearance) {
    Vector2 center = slot ? (Vector2){1430,530} : (Vector2){490,510};
    center.x += (slot ? 350 : -350)*clearance;
    Color color = player_colors[slot];
    int phase = (tick + 91*slot) % 256;
    float turn = 2*PI/256;
    float heading = phase*turn;
    Vector2 ship = title_ship_position(center,heading);
    for (int age=27; age>=0; --age) {
        int launched = (phase - age + 256) % 256;
        /* Short bursts leave a beat of clear space between volleys. */
        int beat = launched % 64;
        if (beat != 0 && beat != 7 && beat != 28 && beat != 39) continue;
        float angle = launched*turn;
        Vector2 origin = title_ship_position(center,angle);
        float distance = 24 + age*6;
        sprite(assets->weapon_textures[WEAPON_FRONT_LASER],
            origin.x + distance*cosf(angle), origin.y + distance*sinf(angle),
            23,angle*RAD2DEG,Fade(color,fminf(1,(28-age)/8.0f)));
    }
    sprite(assets->ships[0],ship.x,ship.y,54,heading*RAD2DEG,color);
}

static void menu_row(Font font, const Game *game, int row, int x, int y, int width, int height) {
    bool behind_dialog = game->menu.page == MENU_DELETE;
    bool selected = row == (behind_dialog ? game->menu.delete_slot : game->menu.cursor);
    Color text = behind_dialog || game_menu_enabled(game,row) ? WHITE : (Color){126,139,156,255};
    DrawRectangle(x,y,width,height-3,selected ? surface : ink);
    DrawLine(x,y+height-3,x+width,y+height-3,rule);
    if (selected) DrawRectangle(x,y,5,height-3,accent);
    const char *label = behind_dialog ? game_slot_label(game,row) : game_menu_label(game,row);
    DrawTextEx(font,label,(Vector2){x+22,y+(height-30)/2-2},28,0,text);
    if (game->menu.page == MENU_OPTIONS && row < 3) {
        int value = game->menu.options[row];
        for (int i=0; i<10; ++i) DrawRectangle(x+width-199+17*i,y+height/2-5,13,10,i<value ? accent : rule);
    }
}

static void draw_pilot_select(const Game *game,const Assets *assets, const PlayerDevices *devices,int offset) {
    static const char *specialties[] = {"Ace pilot, Japan","Weapons researcher, Russia","Demolitions specialist, Norway"};
    static const char *stories[][3] = {
        {"Kazuki flew mail between Japan's island airstrips.", "His first carrier landing was in a typhoon, with", "the deck lights out and a sack of letters aboard."},
        {"Rabbit built the targeting rig in her fighter.", "When the lab refused her another test flight,", "she signed the whole prototype up for duty."},
        {"Gunthor used to clear rockslides above the fjords.", "He still carries his old survey notebook. The last", "few pages are sketches of enemy hulls."}
    };
    static const char *bonus[] = {"Speed +","Credits +","Damage +"};
    Font font = assets->ui_font;
    DrawTextEx(assets->heading_font,"Pilot selection",(Vector2){272+offset,92},62,1,WHITE);
    for (int slot=0; slot<GAME_PLAYERS; ++slot) {
        int x = 272 + offset + slot*704, pilot = game->menu.pilot[slot];
        Color color = player_colors[slot];
        DrawRectangle(x,216,664,666,ink);
        DrawRectangle(x,216,664,3,game->players[slot].joined ? color : rule);
        DrawTextEx(font,slot ? "P2" : "P1",(Vector2){x+32,242},28,0,color);
        if (!game->players[slot].joined) {
            button_prompt(font,x+172,526,0,slot,GLYPH_START,"Join");
            key_prompt(font,x+336,526,slot ? "F2":"Enter","Join");
            continue;
        }
        sprite(assets->portraits[pilot],x+332,437,312,0,WHITE);
        if (!game->menu.ready[slot]) {
            int other = game->players[1-slot].joined ? game->menu.pilot[1-slot] : -1;
            for (int dir=-1;dir<=1;dir+=2) {
                int next = pilot+dir;
                if (next==other) next+=dir;
                if (next<0 || next>2) continue;
                int xx=x+332+dir*244;
                DrawLineEx((Vector2){xx-dir*10,449},(Vector2){xx,460},3,color);
                DrawLineEx((Vector2){xx,460},(Vector2){xx-dir*10,471},3,color);
            }
        }
        DrawLine(x+32,593,x+632,593,rule);
        DrawTextEx(font,pilot_names[pilot],(Vector2){x+32,601},47,0,WHITE);
        DrawTextEx(font,bonus[pilot],(Vector2){x+632-MeasureTextEx(font,bonus[pilot],24,0).x,622},24,0,color);
        DrawTextEx(font,specialties[pilot],(Vector2){x+32,664},25,0,color);
        for (int line=0; line<3; ++line)
            DrawTextEx(font,stories[pilot][line],(Vector2){x+32,710+line*29},24,0,WHITE);
        if (game->menu.ready[slot]) DrawTextEx(font,"Ready",(Vector2){x+32,821},26,0,color);
        else button_prompt(font,x+32,819,devices->device[slot],slot,GLYPH_CONFIRM,"Select");
        button_prompt(font,x+455,819,devices->device[slot],slot,GLYPH_BACK,"Cancel");
    }
}

static void draw_menu_list(const Game *game,const Assets *assets,int frame,const PlayerDevices *devices,int offset,float clearance) {
    static const char *headings[MENU_DELETE+1] = {
        [MENU_OPTIONS]="Options",
        [MENU_QUIT]="Quit?", [MENU_OVER]="Game over", [MENU_PAUSE]="Paused", [MENU_RETRY]="Retry stage?",
        [MENU_STAGE_SELECT]="Stage select"
    };
    MenuPage page = game->menu.page;
    Font font = assets->ui_font;
    bool title = game_menu_is_title(game);
    bool stages = page == MENU_STAGE_SELECT;
    if (title || stages) menu_sky(frame);
    else DrawRectangle(ARENA_X,0,GAME_WIDTH,GAME_HEIGHT,Fade(BLACK,.60f));
    if (title) {
        if (game->players[0].joined || !game->players[1].joined) draw_title_ship(assets,0,frame,clearance);
        if (game->players[1].joined) draw_title_ship(assets,1,frame,clearance);
    }
    if (page == MENU_PILOT) { draw_pilot_select(game,assets,devices,offset); return; }
    int width = title ? 488 : 620, x = (CANVAS_WIDTH-width)/2;
    int row_height = stages ? 53 : 69;
    int y = title ? 606 : stages ? 180 : 437;
    int count = page == MENU_DELETE ? GAME_SAVE_SLOTS+1 : game_menu_count(game);
    if (title) {
        float squad_height = width * (float)assets->title_squad.height / assets->title_squad.width;
        sprite(assets->title_squad,x+width/2,y-16-squad_height/2,width,0,WHITE);
        DrawRectangle(x,y-9,width,2,rule);
        sprite(assets->title_logo,960,151,776,0,WHITE);
        DrawTextEx(font,"@kalin_t",(Vector2){80,989},22,0,dim);
    } else {
        DrawRectangle(x-32,y-114,width+64,192+count*row_height,ink);
        DrawRectangle(x-32,y-114,width+64,3,accent);
    }
    x += offset;
    const char *heading = headings[page];
    if (page==MENU_QUIT && game->menu.parent==MENU_PAUSE) heading = "Return to title?";
    if (heading && title)
        DrawTextEx(font,heading,(Vector2){(CANVAS_WIDTH-MeasureTextEx(font,heading,34,0).x)/2+offset,295},34,0,WHITE);
    else if (heading) DrawTextEx(font,heading,(Vector2){x,y-78},44,0,WHITE);
    for (int row=0; row<count; ++row) menu_row(font,game,row,x,y+row*row_height,width,row_height);
    int owner = game->players[0].joined ? 0 : 1, device = devices->device[owner];
    if (page == MENU_DELETE) {
        int top = (CANVAS_HEIGHT-228)/2;
        DrawRectangle(0,0,CANVAS_WIDTH,CANVAS_HEIGHT,Fade(BLACK,0.58f));
        dialog_frame(top,danger);
        DrawTextEx(font,TextFormat("Delete slot %d?",game->menu.delete_slot+1),(Vector2){672,top+24},38,0,WHITE);
        DrawTextEx(font,"This permanently deletes its saved progress.",(Vector2){672,top+89},25,0,dim);
        for (int i=0; i<2; ++i) {
            bool selected = game->menu.cursor == i;
            int left = 672+i*294;
            if (selected) {
                DrawRectangle(left,top+166,280,48,surface);
                DrawRectangle(left,top+166,4,48,i ? danger : accent);
                button_prompt(font,left+16,top+173,device,owner,GLYPH_CONFIRM,i ? "Delete" : "Cancel");
            } else DrawTextEx(font,i ? "Delete" : "Cancel",(Vector2){left+16,top+174},25,0,dim);
        }
        return;
    }
    int footer = y+row_height*(title ? 4 : count)+30;
    int advance = button_prompt(font,x,footer,device,owner,GLYPH_CONFIRM,"Select")+28;
    if (page != MENU_MAIN) advance += button_prompt(font,x+advance,footer,device,owner,GLYPH_BACK,"Cancel")+28;
    int row = game->menu.cursor;
    if (page == MENU_SLOTS && row < GAME_SAVE_SLOTS && (game->saves[row].valid || game->save_errors[row]))
        button_prompt(font,x+advance,footer,device,owner,GLYPH_READY,"Delete");
}

static void draw_menu(const Game *game, const Assets *assets, int frame, const PlayerDevices *devices) {
    /* The title backdrop runs on one clock. Only newly entered controls move. */
    static MenuPage previous_page;
    static int changed_at, direction;
    static float clearance_from, clearance_to;
    MenuPage page = game->menu.page;
    bool entering_shop = page == MENU_SHOP && page != previous_page;
    int elapsed = frame >= changed_at ? frame-changed_at : 18;
    float t = fminf(1,elapsed/18.0f);
    float clearance = clearance_from + (clearance_to-clearance_from)*t*t*(3-2*t);
    if (page != previous_page) {
        bool from_title = previous_page == MENU_MAIN || previous_page == MENU_SLOTS ||
            previous_page == MENU_DIFFICULTY || previous_page == MENU_PILOT ||
            previous_page == MENU_OPTIONS || previous_page == MENU_QUIT;
        bool slide = from_title && game_menu_is_title(game) && page != MENU_DELETE &&
            page != MENU_INTRO && !game->transition_frames;
        bool back = page == MENU_MAIN || previous_page == MENU_PILOT ||
            (previous_page == MENU_DIFFICULTY && page == MENU_SLOTS);
        direction = slide ? (back ? -1 : 1) : 0;
        clearance_from = clearance;
        clearance_to = page == MENU_PILOT ? 1 : 0;
        changed_at = frame;
        elapsed = 0;
        previous_page = page;
    }
    int offset = menu_slide_offset(elapsed,direction);
    if (game->menu.page == MENU_NONE) return;
    if (game->menu.page == MENU_INTRO) {
        int tick = game->menu.frame;
        menu_sky(tick);
        int lead = game->players[0].joined ? 0 : 1;
        float lift = fmaxf(0,tick-112);
        float y = 650 - 190*fminf(1,tick/70.0f) - lift*lift*0.065f;
        for (int i=0; i<GAME_PLAYERS; ++i) {
            const Player *p = &game->players[i];
            if (p->joined) sprite(assets->ships[p->chassis],i == lead ? 960 : 1060,
                y+(i == lead ? 0 : 68),86,-90,player_colors[i]);
        }
        int pilot = game->players[lead].pilot;
        DrawRectangle(570,755,780,135,ink);
        DrawRectangle(570,755,780,2,rule);
        sprite(assets->portraits[pilot],648,821,124,0,WHITE);
        DrawTextEx(assets->ui_font,pilot_names[pilot],(Vector2){735,774},24,0,accent);
        DrawTextEx(assets->ui_font,"Copy, Control. Launching.",(Vector2){735,812},34,0,WHITE);
        button_prompt(assets->ui_font,1180,923,devices->device[lead],lead,GLYPH_CONFIRM,"Skip");
        return;
    }
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
        ClearBackground(ink);
        for (int layer = 0; layer < 2; ++layer) for (int i = 0; i < 10; ++i) {
            float x = (float)((i * 193 + layer * 311) % CANVAS_WIDTH);
            float y = fmodf(i * 137.0f + game->menu.frame * (layer ? 2.1f : 3.5f), GAME_HEIGHT);
            sprite(assets->star, x, y, (float)assets->star.width, 0, WHITE);
        }
        for (unsigned int i = 0; i < sizeof(lines) / sizeof(lines[0]); ++i)
            text_center(assets->ui_font, lines[i], 960, 1080 - game->menu.frame * 0.7f + i * 50, 32, WHITE);
        return;
    }
    if (game->menu.page == MENU_SHOP) { draw_shop(game, assets, frame, devices, entering_shop); return; }
    if (game->menu.page == MENU_CLEAR) {
        float alpha = fminf(0.5f, game->menu.frame / 60.0f);
        DrawRectangle(0, 0, CANVAS_WIDTH, CANVAS_HEIGHT, Fade(BLACK, alpha));
        int counter = game->clear_exit ? 440 : game->menu.frame;
        if (counter >= 60) {
            DrawRectangle(672,115,576,590,ink);
            DrawRectangle(672,115,576,3,accent);
            DrawLine(704,248,1216,248,rule);
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
            DrawTextEx(assets->ui_font, labels[i], (Vector2){704, (float)y}, 26, 0, WHITE);
            const char *value = i < 4 ? TextFormat("%d%%", percent[i]) :
                TextFormat("+%d cr", i == 4 ? 10000 : bonus[medal]);
            DrawTextEx(assets->ui_font, value, (Vector2){1216-MeasureTextEx(assets->ui_font,value,26,0).x, (float)y}, 26, 0, i < 4 ? WHITE : accent);
        }
        if (counter >= 300) {
            Texture2D texture = assets->medals[medal - 1];
            float scale = fmaxf(1, 2 - (counter - 299) * 0.125f);
            sprite(texture, 968, 540, texture.width * scale, 0, WHITE);
        }
        if (counter >= 360) {
            int owner = game->players[0].joined ? 0 : 1;
            button_prompt(assets->ui_font,880,630,devices->device[owner],owner,GLYPH_CONFIRM,"Select");
        }
        return;
    }
    draw_menu_list(game,assets,frame,devices,offset,clearance);
}

static void menu_input(Game *game, Assets *assets, MenuAction action, int slot) {
    GameMenu before = game->menu;
    bool valid = before.page == MENU_PILOT || game_menu_enabled(game, before.cursor);
    if (before.page == MENU_SHOP) valid = !before.shop_purchase[slot] ||
        game_shop_cost(game, slot) <= game->players[slot].money;
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
            pressed[MENU_READY] = IsKeyPressed(KEY_C);
            pressed[MENU_BACK] = IsKeyPressed(KEY_Z) || IsKeyPressed(KEY_ESCAPE);
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
    int mode = game_menu_is_title(game) || game->menu.page == MENU_SHOP ? 1 :
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
    int hint_x = 380;
    hint_x += key_prompt(assets->console_font,hint_x,178,"Up / Down","Track") + 30;
    hint_x += key_prompt(assets->console_font,hint_x,178,"Left / Right","Seek") + 30;
    hint_x += key_prompt(assets->console_font,hint_x,178,"Enter","Restart") + 30;
    key_prompt(assets->console_font,hint_x,178,"Esc","Close");
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
    int hint_y = panel_height - 85, hint_x = 18;
    hint_x += key_prompt(font,hint_x,hint_y,"Tab","Complete") + 24;
    hint_x += key_prompt(font,hint_x,hint_y,"Up / Down","History") + 24;
    hint_x += key_prompt(font,hint_x,hint_y,"PgUp / PgDn","Scroll") + 24;
    DrawTextEx(font,TextFormat("%d/%d lines",total-console->scroll,total),(Vector2){hint_x,hint_y+2},22,0,dim);
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
    double selection_retry_at = 0;
    char save_paths[GAME_SAVE_SLOTS][2048] = {{0}};
    char selection_path[2048] = {0};
    int remembered_slot = -1;
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
                }
            }
            snprintf(selection_path, sizeof(selection_path), "%s/selected-slot", directory);
            FILE *selection = fopen(selection_path, "rb");
            if (selection) {
                int index = fgetc(selection) - '1';
                if (index >= 0 && index < GAME_SAVE_SLOTS) remembered_slot = index;
                fclose(selection);
            }
            if (remembered_slot >= 0) game->active_slot = remembered_slot;
            else for (int i=0; i<GAME_SAVE_SLOTS; ++i) {
                if (game->saves[i].valid) { game->active_slot = i; break; }
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
        if (!console->music_album && !console_open && !toggled && ((!was_menu && !game->paused) || game->menu.page == MENU_OVER || game->menu.page == MENU_CLEAR || game->menu.page == MENU_FINISH || game->menu.page == MENU_INTRO || game->menu.page == MENU_SHOP || game->transition_frames > 0) && (IsWindowFocused() || frames)) {
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
        if (game->menu.page == MENU_MAIN && game->active_slot < 0)
            game->active_slot = remembered_slot >= 0 ? remembered_slot : 0;
        if (selection_path[0] && game->active_slot >= 0 && remembered_slot != game->active_slot && GetTime() >= selection_retry_at) {
            char value[] = {(char)('1'+game->active_slot), '\0'};
            if (SaveFileText(selection_path,value)) remembered_slot = game->active_slot;
            else { console_log(console,"ERROR: could not remember save slot"); selection_retry_at = GetTime()+5; }
        }
        if (!frames && game->saves_dirty && GetTime() >= save_retry_at) {
            for (int i = 0; i < GAME_SAVE_SLOTS; ++i) if (game->saves_dirty & (1u << i)) {
                if (save_paths[i][0] && (game->saves[i].valid ? game_save_write(&game->saves[i], save_paths[i]) :
                    game_save_delete(save_paths[i]))) {
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
        if (!game_menu_is_title(game) && game->menu.page != MENU_SHOP && game->menu.page != MENU_FINISH &&
            game->menu.page != MENU_STAGE_SELECT)
            draw_game(game, &assets);
        int animation_tick = frames ? menu_frame++ : (int)fmod(GetTime()*GAME_HZ, 16777216.0);
        draw_menu(game, &assets, animation_tick, &devices);
        if (game->active_slot >= 0 && game->active_slot < GAME_SAVE_SLOTS && game->save_errors[game->active_slot])
            text_center(assets.ui_font, "Save unavailable - progress has not been written", 960, 940, 24, RED);
        if (game->transition_frames > 0) {
            float alpha = game->transition_frames > 15 ? (30 - game->transition_frames) / 15.0f : game->transition_frames / 15.0f;
            alpha = alpha*alpha*(3-2*alpha);
            DrawRectangle(0, 0, CANVAS_WIDTH, CANVAS_HEIGHT, Fade(BLACK, alpha));
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
