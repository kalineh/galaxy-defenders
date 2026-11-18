#include "game.h"
#include "console.h"
#include "synth.h"
#include "input.h"
#include "raylib.h"
#include "rlgl.h"
#ifndef GALAXY_MEMORY
#include "external/glad.h"
#endif
#include <math.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#include "ui_strings.inc"
static int ui_language;

static const char *ui_text(const char *english) {
    if (ui_language && english) {
        for (size_t i=0; i<sizeof(ui_strings)/sizeof(ui_strings[0]); ++i)
            if (!strcmp(english,ui_strings[i][0])) return ui_strings[i][ui_language];
    }
    return english;
}

#define CANVAS_WIDTH 1920
#define CANVAS_HEIGHT 1080
#define ARENA_X 480
#define FRAME_SAMPLES 240

typedef struct {
    float frame_ms[FRAME_SAMPLES], work_ms[FRAME_SAMPLES];
    int cursor, count;
} FrameGraph;

typedef struct {
    const char *name;
    int page;
    Rectangle region;
    int width, height;
    Rectangle bounds; /* Visible portion of the original canvas, normalized. */
} AtlasEntry;

#include "sprite_regions.inc"

typedef struct {
    Texture2D texture;
    Rectangle region, bounds;
    int width, height;
} Sprite;

typedef struct {
    Texture2D pages[sizeof(atlas_files)/sizeof(atlas_files[0])];
    Sprite ships[GAME_CHASSIS], enemies[ENEMY_KIND_COUNT], shot, hostile_shot;
    Texture2D portraits[3], title_logo, title_squad;
    Sprite particle_dot, particle_triangle;
    Sprite shop[15], star;
    Sprite weapon_icons[WEAPON_COUNT], weapon_textures[WEAPON_COUNT];
    Sprite hostile_textures[HOSTILE_COUNT];
    Sprite big_laser, big_flame, shield_overlay, asteroid_cracks;
    Sprite generator_icons[GENERATOR_COUNT], shield_icons[SHIELD_COUNT], item_none, sidekick;
    Sprite stage_textures[STAGE_MAX_EVENTS], stage_destroyed[STAGE_MAX_EVENTS];
    Texture2D scenery[4];
    Sprite boss4_cover, boss5_cover, boss6_center;
    Sprite rotate_turret_base, splitter_right, pyramid_turret, cutter_beam;
    Sprite enemy_corpses[ENEMY_KIND_COUNT];
    Sprite pickups[3], medals[5];
    int stage_index;
    Color stage_tints[STAGE_MAX_EVENTS];
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
    Font console_font, ui_font, heading_font, western_body, western_heading, cjk_font;
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

static Sprite atlas_sprite(const Assets *assets, const char *name) {
    int lo = 0, hi = (int)(sizeof(atlas_entries)/sizeof(atlas_entries[0]));
    while (lo < hi) {
        int mid = lo + (hi-lo)/2;
        const AtlasEntry *entry = &atlas_entries[mid];
        int cmp = strcmp(name,entry->name);
        if (!cmp) return (Sprite){assets->pages[entry->page],entry->region,entry->bounds,entry->width,entry->height};
        if (cmp < 0) hi = mid;
        else lo = mid+1;
    }
    TraceLog(LOG_ERROR,"Missing atlas sprite: %s",name);
    return (Sprite){0};
}

static Sprite whole_texture(Texture2D texture) {
    return (Sprite){texture,{0,0,texture.width,texture.height},{0,0,1,1},texture.width,texture.height};
}

static void assets_unload(Assets *assets) {
    for (unsigned i = 0; i < sizeof(atlas_files)/sizeof(atlas_files[0]); ++i)
        if (IsTextureValid(assets->pages[i])) UnloadTexture(assets->pages[i]);
    if (IsTextureValid(assets->title_logo)) UnloadTexture(assets->title_logo);
    if (IsTextureValid(assets->title_squad)) UnloadTexture(assets->title_squad);
    for (int i = 0; i < 4; ++i) if (IsTextureValid(assets->scenery[i])) UnloadTexture(assets->scenery[i]);
    if (assets->console_font.texture.id && assets->console_font.texture.id != GetFontDefault().texture.id) UnloadFont(assets->console_font);
    Font owned[] = {assets->western_body,assets->western_heading,assets->cjk_font};
    for (int i=0; i<3; ++i)
        if (owned[i].texture.id && owned[i].texture.id != GetFontDefault().texture.id) UnloadFont(owned[i]);
    for (int i = 0; i < 3; ++i)
        if (IsTextureValid(assets->portraits[i])) UnloadTexture(assets->portraits[i]);
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

typedef struct {
    const char *name;
    Color base, horizon, wash, dust;
    Vector2 mote, streak;
    int fast_layer;
    enum { WEATHER_NONE, WEATHER_RAIN, WEATHER_SNOW } weather;
} StageBackdrop;

static const StageBackdrop backdrops[STAGE_COUNT] = {
    /* Mask, upper and lower sky, drifting wash, particles, slow/fast sizes, fast particle layer. */
    {"Tide",   {15,28,50,255}, {15,28,50,255}, {25,39,65,255}, {150,177,218,39}, {4,15}, {5,27}, 1,WEATHER_NONE},
    {"Shoals", {14,35,49,255}, {14,35,49,255}, {27,53,62,255}, {136,185,183,36}, {5,7}, {5,14}, 3,WEATHER_NONE},
    {"Halo",   {16,24,46,255}, {23,44,55,255}, {41,55,70,255}, {143,197,207,52}, {4,10}, {7,35}, 1,WEATHER_RAIN},
    {"Cluster",{5,8,15,255},  {10,15,25,255}, {107,123,151,255}, {181,197,226,68}, {4,4}, {5,9}, 1,WEATHER_NONE},
    {"Strata", {19,17,39,255}, {30,25,53,255}, {49,39,66,255}, {173,157,204,42}, {7,9}, {9,16}, 1,WEATHER_NONE},
    {"",       {1,1,2,255}, {1,1,2,255}, {0,0,0,0}, {146,155,171,40}, {3,18}, {4,37}, 2,WEATHER_NONE},
    {"Storm",  {18,33,38,255}, {30,49,49,255}, {65,82,76,255}, {186,215,208,76}, {8,11}, {15,21}, 1,WEATHER_SNOW},
    {"Vapor",  {26,28,48,255}, {28,30,52,255}, {40,41,65,255}, {176,174,209,43}, {6,9}, {7,18}, 2,WEATHER_NONE},
    {"Rift",   {24,23,32,255}, {42,34,39,255}, {57,45,50,255}, {197,173,164,51}, {4,22}, {5,43}, 2,WEATHER_NONE},
    {"Ripple", {10,26,48,255}, {18,40,61,255}, {46,77,87,255}, {133,183,209,55}, {6,7}, {8,50}, 3,WEATHER_RAIN},
    {"Mist",   {26,32,52,255}, {39,49,70,255}, {58,66,84,255}, {207,217,235,81}, {10,12}, {17,20}, 1,WEATHER_SNOW},
    {"Eclipse",{12,12,28,255}, {32,24,47,255}, {64,49,77,255}, {201,179,220,65}, {4,5}, {5,19}, 1,WEATHER_NONE},
    {"Fold",   {9,18,30,255}, {12,36,41,255}, {39,160,122,255}, {121,223,180,94}, {4,5}, {5,15}, 1,WEATHER_NONE},
    {"Prism",  {21,13,35,255}, {36,22,51,255}, {142,73,164,255}, {191,153,203,83}, {4,5}, {5,10}, 1,WEATHER_NONE},
    {"Tunnel", {10,20,38,255}, {12,32,57,255}, {45,123,192,255}, {121,192,225,80}, {3,12}, {4,23}, 1,WEATHER_NONE},
    {"Echo",   {26,14,26,255}, {43,22,29,255}, {177,92,67,255}, {215,178,144,83}, {5,6}, {5,14}, 1,WEATHER_NONE},
    {"Circuit",{11,24,27,255}, {23,39,37,255}, {115,157,75,255}, {172,209,151,83}, {4,7}, {5,18}, 1,WEATHER_NONE},
    {"Curtain",{17,16,40,255}, {32,25,56,255}, {103,83,187,255}, {162,180,221,83}, {4,8}, {5,19}, 1,WEATHER_NONE},
    {"Orrery", {18,14,37,255}, {40,25,50,255}, {167,102,148,255}, {209,177,198,88}, {4,5}, {5,14}, 1,WEATHER_NONE},
};

/* Decoration textures supply silhouettes. Color them once when loading the stage. */
static Color deck_tint(int stage, const char *name) {
    static const Color deck[STAGE_COUNT] = {
        {49,65,87,255}, {44,68,75,255}, {49,67,77,255}, {48,54,66,255},
        {65,57,83,255}, {47,48,55,255}, {58,77,71,255}, {60,59,84,255},
        {71,59,62,255}, {42,63,80,255}, {65,74,88,255}, {66,54,79,255},
        [12] = {255,255,255,255}, [15] = {82,57,58,255}, [16] = {60,76,66,255},
    };
    Color tint = deck[stage];
    if (!strcmp(name,"SecretHint")) {
        tint.r += 22;
        tint.g += 22;
        tint.b += 22;
    }
    return tint;
}

static bool stage_assets_load(Assets *assets, int index) {
    bool valid = true;
    for (int i = 0; i < 4; ++i) {
        if (IsTextureValid(assets->scenery[i])) UnloadTexture(assets->scenery[i]);
        assets->scenery[i] = (Texture2D){0};
        char path[160];
        if (i == 2) {
            /* Bake the horizon at canvas resolution with a stationary sub-level dither. */
            const StageBackdrop *sky = &backdrops[index];
            Image gradient = GenImageColor(64,GAME_HEIGHT,sky->base);
            if (!gradient.data) return false;
            Color *pixels = gradient.data;
            unsigned seed = 1;
            for (int y = 0; y < gradient.height; ++y) {
                float t = (float)y / (gradient.height-1);
                for (int x = 0; x < gradient.width; ++x) {
                    seed = seed*1664525u + 1013904223u;
                    float d = (seed >> 8) / 16777216.0f;
                    pixels[y*gradient.width+x] = (Color){
                        (unsigned char)(sky->base.r + t*(sky->horizon.r-sky->base.r) + d),
                        (unsigned char)(sky->base.g + t*(sky->horizon.g-sky->base.g) + d),
                        (unsigned char)(sky->base.b + t*(sky->horizon.b-sky->base.b) + d),255};
                }
            }
            assets->scenery[i] = LoadTextureFromImage(gradient);
            UnloadImage(gradient);
            valid = valid && IsTextureValid(assets->scenery[i]);
            continue;
        }
        if (i == 3) {
            if (!backdrops[index].weather) continue;
            assets->scenery[i] = texture_load(backdrops[index].weather == WEATHER_SNOW ?
                "textures/scenery/Snowflake.png" : "textures/scenery/Raindrop.png");
            valid = valid && IsTextureValid(assets->scenery[i]);
            continue;
        }
        if (!backdrops[index].name[0]) continue;
        snprintf(path, sizeof(path), "textures/scenery/%s%s.png", backdrops[index].name, i ? "Near" : "Far");
        assets->scenery[i] = texture_load(path);
        valid = valid && IsTextureValid(assets->scenery[i]);
    }
    assets->stage_index = index;
    const StageDefinition *stage = &stage_definitions[index];
    for (int i = 0; i < stage->event_count; ++i) {
        const StageEvent *event = &stage->events[i];
        if (event->kind != STAGE_DECORATION && event->kind != STAGE_BUILDING) continue;
        char path[160];
        snprintf(path, sizeof(path), "textures/stage/%s/%s.png",
            event->kind == STAGE_DECORATION ? "Decoration" : "Static", event->name);
        assets->stage_textures[i] = atlas_sprite(assets,path);
        assets->stage_tints[i] = event->kind == STAGE_DECORATION ? deck_tint(index,event->name) : WHITE;
        valid = valid && IsTextureValid(assets->stage_textures[i].texture);
        if (event->kind == STAGE_BUILDING) {
            snprintf(path, sizeof(path), "textures/stage/Static/Destroyed/%s.png", event->name);
            assets->stage_destroyed[i] = atlas_sprite(assets,path);
            valid = valid && IsTextureValid(assets->stage_destroyed[i].texture);
        }
    }
    return valid;
}

static bool assets_load(Assets *assets, Console *console, bool mute, float volume) {
    /* Solcraft's fixed-font preference: use the OS font, never copy Windows fonts. */
    int count = sizeof(western_glyphs)/sizeof(western_glyphs[0]);
    assets->western_body = LoadFontEx(asset_path("fonts/NotoSans-Regular.ttf"),64,western_glyphs,count);
    assets->western_heading = LoadFontEx(asset_path("fonts/NotoSans-Bold.ttf"),64,western_glyphs,count);
    assets->cjk_font = LoadFontEx(asset_path("fonts/NotoSansCJKsc-Regular.otf"),64,cjk_glyphs,sizeof(cjk_glyphs)/sizeof(cjk_glyphs[0]));
    assets->ui_font = assets->western_body;
    assets->heading_font = assets->western_heading;
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
    if (!IsFontValid(assets->cjk_font) || assets->cjk_font.texture.id == GetFontDefault().texture.id ||
        !IsFontValid(assets->heading_font) || assets->heading_font.texture.id == GetFontDefault().texture.id ||
        !IsFontValid(assets->ui_font) || assets->ui_font.texture.id == GetFontDefault().texture.id ||
        !IsFontValid(assets->console_font) || assets->console_font.texture.id == GetFontDefault().texture.id) {
        console_log(console, "ERROR: required UI/console font could not load; default raylib text is disabled.");
        return false;
    }
    SetTextureFilter(assets->cjk_font.texture,TEXTURE_FILTER_BILINEAR);
    SetTextureFilter(assets->heading_font.texture,TEXTURE_FILTER_BILINEAR);
    SetTextureFilter(assets->ui_font.texture, TEXTURE_FILTER_BILINEAR);
    SetTextureFilter(assets->console_font.texture, TEXTURE_FILTER_BILINEAR);
    for (unsigned i = 0; i < sizeof(atlas_files)/sizeof(atlas_files[0]); ++i) {
        assets->pages[i] = texture_load(atlas_files[i]);
        if (!IsTextureValid(assets->pages[i])) return false;
#ifndef GALAXY_MEMORY
        GenTextureMipmaps(&assets->pages[i]);
        SetTextureFilter(assets->pages[i],TEXTURE_FILTER_TRILINEAR);
#endif
    }
    for (int i = 0; i < GAME_CHASSIS; ++i) {
        char path[128];
        snprintf(path, sizeof(path), "textures/%s.png", game_chassis[i].name);
        assets->ships[i] = atlas_sprite(assets,path);
    }
    for (int i = 0; i < ENEMY_KIND_COUNT; ++i) {
        if (i == ENEMY_BOSS8_COLLISION) continue;
        char path[128];
        snprintf(path, sizeof(path), "textures/%s.png", i == ENEMY_SPLITTER_FRAGMENT ? "SplitterFragmentLeft" : enemy_is_asteroid(i) ? "Asteroid" : enemy_names[i]);
        assets->enemies[i] = atlas_sprite(assets,path);
    }
    for (int i = 0; i < ENEMY_KIND_COUNT; ++i) {
        const char *name = enemy_corpse_name((EnemyKind)i);
        if (!name) continue;
        char path[128];
        snprintf(path, sizeof(path), "textures/%s.png", name);
        assets->enemy_corpses[i] = atlas_sprite(assets,path);
    }
    static const char *medals[] = {"Clear", "Bronze", "Silver", "Gold", "Platinum"};
    for (int i = 0; i < 5; ++i) {
        char path[128];
        snprintf(path, sizeof(path), "textures/Medal%s.png", medals[i]);
        assets->medals[i] = atlas_sprite(assets,path);
        if (!IsTextureValid(assets->medals[i].texture)) return false;
    }
    assets->asteroid_cracks = atlas_sprite(assets,"textures/Cracks.png");
    if (!IsTextureValid(assets->asteroid_cracks.texture)) return false;
    assets->pyramid_turret = atlas_sprite(assets,"textures/PyramidTurret.png");
    assets->cutter_beam = atlas_sprite(assets,"textures/CutterBeam.png");
    assets->rotate_turret_base = atlas_sprite(assets,"textures/RotateTurretBase.png");
    assets->splitter_right = atlas_sprite(assets,"textures/SplitterFragmentRight.png");
    assets->shot = atlas_sprite(assets,"textures/Laser.png");
    assets->pickups[0] = atlas_sprite(assets,"textures/Bonus.png");
    assets->pickups[1] = atlas_sprite(assets,"textures/BigBonus.png");
    assets->pickups[2] = atlas_sprite(assets,"textures/Powerup.png");
    assets->boss4_cover = atlas_sprite(assets,"textures/Boss4Cover.png");
    assets->boss5_cover = atlas_sprite(assets,"textures/Boss5Cover.png");
    assets->boss6_center = atlas_sprite(assets,"textures/Boss6Center.png");
    assets->hostile_shot = atlas_sprite(assets,"textures/EnemyShot.png");
    assets->particle_dot = atlas_sprite(assets,"textures/DotParticle.png");
    assets->particle_triangle = atlas_sprite(assets,"textures/TriangleParticle.png");
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
    bool valid = IsTextureValid(assets->pyramid_turret.texture) && IsTextureValid(assets->cutter_beam.texture) && IsTextureValid(assets->rotate_turret_base.texture) && IsTextureValid(assets->splitter_right.texture) && IsTextureValid(assets->shot.texture) && IsTextureValid(assets->hostile_shot.texture);
    for (int i = 0; i < GAME_CHASSIS; ++i) valid = valid && IsTextureValid(assets->ships[i].texture);
    valid = valid && IsTextureValid(assets->particle_dot.texture) && IsTextureValid(assets->particle_triangle.texture);
    for (int i = 0; i < ENEMY_KIND_COUNT; ++i)
        if (i != ENEMY_BOSS8_COLLISION) valid = valid && IsTextureValid(assets->enemies[i].texture);
    for (int i = 0; i < ENEMY_KIND_COUNT; ++i)
        if (enemy_corpse_name((EnemyKind)i)) valid = valid && IsTextureValid(assets->enemy_corpses[i].texture);
    for (int i = 0; i < 3; ++i) valid = valid && IsTextureValid(assets->portraits[i]);
    valid = valid && IsTextureValid(assets->title_logo) && IsTextureValid(assets->title_squad);
    for (int i = 0; i < 15; ++i) {
        char path[160];
        snprintf(path, sizeof(path), "textures/shop/%s.png", shop_texture_names[i]);
        assets->shop[i] = atlas_sprite(assets,path);
        valid = IsTextureValid(assets->shop[i].texture) && valid;
    }
    assets->star = atlas_sprite(assets,"textures/Star.png");
    static const char *hostile_names[] = {"EnemyShot", "EnemyLaser", "EnemyMissile", "EnemyCannonShot", "EnemyPellet", "EnemyMiniShot"};
    assets->sidekick = atlas_sprite(assets,"textures/player/Sidekick.png");
    assets->shield_overlay = atlas_sprite(assets,"textures/effects/PlayerShieldHit.png");
    assets->big_laser = atlas_sprite(assets,"textures/weapons/BigLaser.png");
    assets->big_flame = atlas_sprite(assets,"textures/weapons/BigFlame.png");
    valid = valid && IsTextureValid(assets->sidekick.texture) && IsTextureValid(assets->shield_overlay.texture) &&
        IsTextureValid(assets->big_laser.texture) && IsTextureValid(assets->big_flame.texture);
    for (int i = 0; i < HOSTILE_COUNT; ++i) {
        char path[160];
        snprintf(path, sizeof(path), "textures/weapons/%s.png", hostile_names[i]);
        assets->hostile_textures[i] = atlas_sprite(assets,path);
        valid = IsTextureValid(assets->hostile_textures[i].texture) && valid;
    }
    for (int i = 0; i < WEAPON_COUNT; ++i) {
        char path[160];
        snprintf(path, sizeof(path), "textures/weapons/%s.png", weapon_texture_names[i]);
        assets->weapon_textures[i] = atlas_sprite(assets,path);
        valid = IsTextureValid(assets->weapon_textures[i].texture) && valid;
        if (equipment_weapon_category(i) >= 0) {
            snprintf(path, sizeof(path), "textures/shop/IconWeapon%s.png", game_weapons[i].name);
            assets->weapon_icons[i] = atlas_sprite(assets,path);
            valid = IsTextureValid(assets->weapon_icons[i].texture) && valid;
        }
    }
    for (int i = 0; i < GENERATOR_COUNT; ++i) {
        char path[160];
        snprintf(path, sizeof(path), "textures/shop/%s.png", game_generators[i].icon);
        assets->generator_icons[i] = atlas_sprite(assets,path);
        valid = IsTextureValid(assets->generator_icons[i].texture) && valid;
    }
    for (int i = 0; i < SHIELD_COUNT; ++i) {
        char path[160];
        snprintf(path, sizeof(path), "textures/shop/%s.png", game_shields[i].icon);
        assets->shield_icons[i] = atlas_sprite(assets,path);
        valid = IsTextureValid(assets->shield_icons[i].texture) && valid;
    }
    assets->item_none = atlas_sprite(assets,"textures/shop/IconItemNone.png");
    valid = IsTextureValid(assets->item_none.texture) && valid;
    valid = IsTextureValid(assets->star.texture) && stage_assets_load(assets, 0) && valid;
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

/* Cropping the transparent margins changes only the drawn rectangle, never the pivot. */
static void sprite_part(Sprite image, Rectangle src, Rectangle dest, Vector2 origin, float angle, Color tint) {
    Rectangle region = image.region;
    Rectangle bounds = image.bounds;
    float left = src.width < 0 ? 1-bounds.x-bounds.width : bounds.x;
    float top = src.height < 0 ? 1-bounds.y-bounds.height : bounds.y;
    origin.x -= dest.width*left;
    origin.y -= dest.height*top;
    dest.width *= bounds.width;
    dest.height *= bounds.height;
    region.x += src.x*region.width/image.width;
    region.y += src.y*region.height/image.height;
    region.width *= src.width/image.width;
    region.height *= src.height/image.height;
    DrawTexturePro(image.texture,region,dest,origin,angle,tint);
}

static void sprite(Sprite image, float x, float y, float width, float angle, Color tint) {
    float height = width*image.height/image.width;
    sprite_part(image,(Rectangle){0,0,image.width,image.height},(Rectangle){x,y,width,height},
        (Vector2){width/2,height/2},angle,tint);
}

static void text_center(Font font, const char *text, int x, int y, int size, Color color) {
    DrawTextEx(font, text, (Vector2){x - MeasureTextEx(font, text, (float)size, 0).x / 2, (float)y}, (float)size, 0, color);
}

typedef enum { GLYPH_CONFIRM, GLYPH_BACK, GLYPH_READY, GLYPH_START, GLYPH_HORIZONTAL, GLYPH_VERTICAL } ButtonGlyph;

static int key_prompt(Font font, int x, int y, const char *key, const char *action) {
    int width = (int)fmaxf(30, MeasureTextEx(font,key,20,0).x + 14);
    Rectangle cap = {x,y,width,28};
    DrawRectangleRounded(cap,.28f,6,surface);
    DrawRectangleRoundedLinesEx(cap,.28f,6,1.5f,(Color){124,147,170,255});
    text_center(font,key,x+width/2,y+2,20,WHITE);
    DrawTextEx(font,action,(Vector2){x+width+9,y+1},23,0,(Color){201,213,226,255});
    return width + 9 + (int)MeasureTextEx(font,action,23,0).x;
}

static int button_prompt(Font font, int x, int y, int device, ButtonGlyph button, const char *action) {
    action = ui_text(action);
    if (button == GLYPH_HORIZONTAL || button == GLYPH_VERTICAL) {
        bool horizontal = button == GLYPH_HORIZONTAL;
        bool pad = device >= 0 && device < INPUT_PADS;
        Rectangle cap = {x,y,36,28};
        if (pad) DrawCircle(x+18,y+14,15,surface);
        else DrawRectangleRounded(cap,.28f,6,surface);
        for (int dir=-1;dir<=1;dir+=2) {
            int dx = horizontal ? dir : 0, dy = horizontal ? 0 : dir;
            Vector2 tip = {x+18+dx*10,y+14+dy*10};
            Vector2 a = {tip.x-dx*6-dy*5,tip.y-dy*6+dx*5};
            Vector2 b = {tip.x-dx*6+dy*5,tip.y-dy*6-dx*5};
            DrawLineEx(a,tip,2,WHITE);
            DrawLineEx(b,tip,2,WHITE);
        }
        DrawTextEx(font,action,(Vector2){x+45,y+1},23,0,(Color){201,213,226,255});
        return 45+(int)MeasureTextEx(font,action,23,0).x;
    }
    static const char *keys[] = {"X","Z","C","Enter"};
    if (device < 0 || device >= INPUT_PADS) return key_prompt(font,x,y,keys[button],action);
    static const char *letters[] = {"A","B","X"};
    const char *pad = device < 4 ? GetGamepadName(device) : NULL;
    bool ps = device >= 4 || (pad && (strstr(pad,"Wireless Controller") || strstr(pad,"PS4") ||
        strstr(pad,"DualSense") || strstr(pad,"DualShock") || strstr(pad,"PS5")));
    if (button == GLYPH_START) {
        DrawRectangleRounded((Rectangle){x,y+4,39,23},1,8,(Color){76,91,110,255});
        for (int i = 0; i < 3; ++i) DrawRectangle(x+12,y+9+i*5,15,2,WHITE);
    } else {
        int cx=x+15, cy=y+15;
        DrawCircle(cx,cy,15,surface);
        DrawRing((Vector2){cx,cy},14,15,0,360,32,(Color){124,147,170,255});
        if (!ps) text_center(font,letters[button],cx,y+1,23,WHITE);
        else if (button == GLYPH_CONFIRM) {
            DrawLineEx((Vector2){cx-6,cy-6},(Vector2){cx+6,cy+6},3,SKYBLUE);
            DrawLineEx((Vector2){cx-6,cy+6},(Vector2){cx+6,cy-6},3,SKYBLUE);
        } else if (button == GLYPH_BACK) DrawRing((Vector2){cx,cy},6,9,0,360,32,(Color){255,155,141,255});
        else DrawRectangleLinesEx((Rectangle){cx-7,cy-7,14,14},3,(Color){251,174,213,255});
    }
    int offset = button == GLYPH_START ? 48 : 39;
    DrawTextEx(font,action,(Vector2){x+offset,y+1},23,0,(Color){201,213,226,255});
    return offset + (int)MeasureTextEx(font,action,23,0).x;
}

/* Matches the publisher capsule used on kalin.games. Both links share the menu edges. */
static int title_links(Font font, Vector2 pointer) {
    const int left = (CANVAS_WIDTH-488)/2, right = left+488, y = 849;
    Rectangle brand = {left,y,170,46};
    const char *handle = "@kalin_t";
    int handle_width = (int)ceilf(MeasureTextEx(font,handle,31,0).x);
    Rectangle social = {right-handle_width-12,y,handle_width+12,46};
    bool over_brand = CheckCollisionPointRec(pointer,brand);
    bool over_social = CheckCollisionPointRec(pointer,social);
    DrawRectangleRounded(brand,1,16,over_brand ? (Color){32,53,80,255} : (Color){23,38,56,255});
    DrawRectangleRoundedLinesEx(brand,1,16,1.5f,over_brand ? dim : (Color){64,85,113,255});
    text_center(font,"kalin.games",left+85,y+7,26,over_brand ? WHITE : (Color){195,215,244,255});
    DrawTextEx(font,handle,(Vector2){right-handle_width,y+4},31,0,over_social ? accent : WHITE);
    if (over_social) DrawLine(right-handle_width,y+39,right,y+39,accent);
    return over_brand ? 1 : over_social ? 2 : 0;
}

static void draw_flight_dust(const StageState *stage, Sprite dust, bool foreground) {
    const StageBackdrop *backdrop = &backdrops[stage->index];
    Rectangle source = {0, 0, (float)dust.width, (float)dust.height};
    for (int pass = 0; pass < 2; ++pass) {
        if (foreground != (pass == 1 && backdrop->weather)) continue;
        int layer = pass ? backdrop->fast_layer : 0;
        Vector2 size = pass ? backdrop->streak : backdrop->mote;
        for (int i = 0; i < stage->scenery_count[layer]; ++i) {
            const SceneryParticle *p = &stage->scenery[layer][i];
            float width = size.x + i%3, height = size.y + i%5;
            float angle = -atan2f(p->vx,p->vy)*RAD2DEG;
            Color tint = backdrop->dust;
            if (foreground) {
                if (backdrop->weather == WEATHER_SNOW) {
                    width = height = 10 + (i*7 % 17);
                    tint = (Color){237,245,255,168 + i%4*16};
                } else {
                    angle += 8;
                    tint = (Color){182,215,236,156 + i%4*12};
                }
            }
            Rectangle dest = {ARENA_X + p->x - stage->camera_x, p->y, width, height};
            sprite_part(dust,source,dest,(Vector2){width/2,height/2},angle,tint);
        }
    }
}

static void draw_scenery(const Game *game, const Assets *assets, bool foreground) {
    int index = game->stage.index;
    const StageBackdrop *backdrop = &backdrops[index];
    if (foreground) {
        if (backdrop->weather) draw_flight_dust(&game->stage,whole_texture(assets->scenery[3]),true);
        return;
    }
    Texture2D sky = assets->scenery[2];
    for (int x = 0; x < GAME_WIDTH; x += sky.width)
        DrawTexture(sky,ARENA_X+x,0,WHITE);
    for (int i = 0; i < 2; ++i) {
        Texture2D texture = assets->scenery[i];
        if (!IsTextureValid(texture)) continue;
        float depth = i ? 0.30f : 0.13f;
        float width = GAME_WIDTH + 112;
        float height = width * texture.height / texture.width;
        float wind = game->tick * (i ? 0.19f : 0.08f);
        float y = fmodf(wind - game->stage.camera_y * depth, height);
        if (y > 0) y -= height;
        float sway = 14*sinf(game->tick * (i ? 0.0020f : 0.0013f) + i);
        float pan = fmaxf(-40,fminf(40,game->stage.camera_x*depth));
        float x = ARENA_X - 56 + sway - pan;
        Rectangle source = {0,0,(float)texture.width,(float)texture.height};
        Color tint = backdrop->wash;
        if (index >= 12 && i == 0) {
            /* A second ink moves beneath the bright near pattern. */
            tint = (Color){tint.b, tint.r, tint.g, 160};
        }
        for (; y < GAME_HEIGHT; y += height)
            DrawTexturePro(texture,source,(Rectangle){x,y,width,height},(Vector2){0},0,tint);
    }
    draw_flight_dust(&game->stage,assets->particle_dot,false);
}

static void draw_enemies(const Game *game, const Assets *assets, int layer) {
    float view_x = ARENA_X - game->stage.camera_x;
    if (layer == 0) for (int i = 0; i < STAGE_MAX_EVENTS; ++i) {
        const EnemyCorpse *corpse = &game->stage.corpses[i];
        if (!corpse->active) continue;
        Sprite texture = assets->enemy_corpses[corpse->kind];
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
        Sprite cover = enemy->kind == ENEMY_BOSS4 && enemy->health > enemy->health_max * 0.6f ? assets->boss4_cover :
            enemy->kind == ENEMY_BOSS5 ? assets->boss5_cover : enemy->kind == ENEMY_BOSS6 ? assets->boss6_center : (Sprite){0};
        float cover_rotation = enemy->kind == ENEMY_BOSS5 ? 90 + enemy->orbit_angle * RAD2DEG :
            enemy->kind == ENEMY_BOSS6 ? enemy->rotation * RAD2DEG : 0;
        int cover_layer = enemy->kind == ENEMY_BOSS6 ? -1 : 2;
        if (layer == cover_layer && IsTextureValid(cover.texture)) sprite(cover, view_x + enemy->x, enemy->y, (float)cover.width, cover_rotation, WHITE);
        int depth = enemy->kind == ENEMY_ROTATE_TURRET ? 1 : enemy_draw_layer(enemy->kind);
        if (depth != layer) continue;
        Sprite texture = enemy->kind == ENEMY_SPLITTER_FRAGMENT && enemy->aux_index ?
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
        sprite_part(texture, source,
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
        Sprite texture = bullet->owner < 0 ? assets->hostile_textures[bullet->hostile_kind] : assets->weapon_textures[bullet->weapon];
        if (bullet->owner >= 0) {
            if ((bullet->weapon == WEAPON_FRONT_LASER || bullet->weapon == WEAPON_FRONT_LASER_FOCUS ||
                 bullet->weapon == WEAPON_SPREAD_LASER || bullet->weapon == WEAPON_SPREAD_LASER_FOCUS ||
                 bullet->weapon == WEAPON_LASER_SHOT) && bullet->base_damage >= 0.4f) texture = assets->big_laser;
            if ((bullet->weapon == WEAPON_FLAME || bullet->weapon == WEAPON_FLAME_FOCUS) && bullet->base_damage >= 1)
                texture = assets->big_flame;
        }
        if (bullet->owner >= 0 && bullet->weapon == WEAPON_BEAM) {
            sprite_part(texture, (Rectangle){0,0,(float)texture.width,(float)texture.height},
                (Rectangle){view_x + bullet->x, bullet->y - 540, 1080, bullet->width * 2},
                (Vector2){540, bullet->width}, -90, color);
        } else {
            float width = texture.width * (bullet->owner < 0 ? 1 : bullet->scale);
            if (bullet->owner >= 0 && bullet->weapon == WEAPON_BEAM_FOCUS) width = bullet->radius / 0.46f * 2;
            float rotation = bullet->owner < 0 && bullet->hostile_kind != HOSTILE_PELLET ? atan2f(bullet->vy, bullet->vx) : bullet->rotation;
            if (bullet->owner >= 0 && bullet->weapon == WEAPON_BLADE && bullet->offset_x < 0) {
                float height = width * texture.height / texture.width;
                sprite_part(texture, (Rectangle){0, 0, texture.width, -texture.height},
                    (Rectangle){view_x + bullet->x, bullet->y, width, height}, (Vector2){width * 0.5f, height * 0.5f}, rotation * RAD2DEG, color);
            } else sprite(texture, view_x + bullet->x, bullet->y, width, rotation * RAD2DEG, color);
        }
    }
    draw_enemies(game, assets, ENEMY_LAYER_WEAPON_OVERLAY);
    for (int i = 0; i < GAME_EFFECTS; ++i) {
        const Effect *effect = &game->effects[i];
        if (effect->life <= 0) continue;
        Sprite texture = effect->kind ? assets->particle_triangle : assets->particle_dot;
        sprite(texture, view_x + effect->x, effect->y, texture.width * effect->scale,
            effect->angle, (Color){effect->r, effect->g, effect->b, (unsigned char)(fminf(1, fmaxf(0, effect->alpha)) * 255)});
    }
}

static int combat_points(float value) {
    return (int)lroundf(value * 100.0f);
}

static void resource_meter(Font font, int x, int y, const char *label, float value, float capacity, Color color) {
    DrawTextEx(font, ui_text(label), (Vector2){x, y}, 21, 0, WHITE);
    const char *amount = TextFormat("%d / %d", combat_points(value), combat_points(capacity));
    DrawTextEx(font, amount, (Vector2){x + 240 - MeasureTextEx(font, amount, 18, 0).x, y + 3}, 18, 0, dim);
    DrawRectangle(x, y + 26, 240, 16, (Color){45,55,69,255});
    if (capacity > 0) DrawRectangle(x, y + 26, (int)(240 * fminf(1, fmaxf(0, value / capacity))), 16, color);
}

static const char *pilot_names[] = {"Kazuki", "Rabbit", "Gunthor"};

static void draw_pilot_rail(const Assets *assets, const Player *p, const Player *preview, int slot, bool shopping, bool keyboard_free) {
    Font font = assets->ui_font;
    int x = slot ? ARENA_X + GAME_WIDTH : 0, left = x + 28, width = ARENA_X - 56;
    Color highlight = player_colors[slot];
    DrawRectangle(x, 0, ARENA_X, GAME_HEIGHT, ink);
    DrawRectangle(slot ? x : x + ARENA_X - 2, 0, 2, GAME_HEIGHT, rule);
    DrawRectangle(left, 32, 58, 36, p->joined ? highlight : rule);
    text_center(font, slot ? "P2" : "P1", left + 29, 33, 27, ink);
    if (!p->joined && p->initialized) {
        DrawTextEx(font,pilot_names[p->pilot],(Vector2){left+70,26},38,0,dim);
        DrawLine(left,88,left+width,88,rule);
        sprite(whole_texture(assets->portraits[p->pilot]),left+82,204,164,0,dim);
        DrawTextEx(font,"Disconnected",(Vector2){left+184,181},26,0,dim);
        return;
    }
    if (!p->joined) {
        text_center(font, "Join flight", x + 240, 488, 36, WHITE);
        button_prompt(font,x+105,543,0,GLYPH_START,"Join");
        if (keyboard_free) key_prompt(font,x+245,543,"F2","Join");
        return;
    }
    DrawTextEx(font, pilot_names[p->pilot], (Vector2){left + 70, 26}, 38, 0, WHITE);
    const char *credits = shopping ? TextFormat("%d cr",p->money) : TextFormat("%d pts",p->score);
    DrawTextEx(font, credits, (Vector2){left + width - MeasureTextEx(font, credits, 27, 0).x, 38}, 27, 0, highlight);
    DrawLine(left, 88, left + width, 88, rule);
    sprite(whole_texture(assets->portraits[p->pilot]), left + 82, 204, 164, 0, WHITE);
    resource_meter(font, left + 184, 115, "Armor", preview->health, game_chassis[preview->chassis].armor, highlight);
    resource_meter(font, left + 184, 172, "Shield", preview->shield_health,
        preview->shield < 0 ? 0 : game_shields[preview->shield].capacity, (Color){130,183,240,255});
    resource_meter(font, left + 184, 229, "Energy", preview->energy, game_generators[preview->generator].capacity, (Color){117,214,188,255});
}

static const char *shop_item_name(int category, int item) {
    if (item < 0) return ui_text("Empty");
    switch (category) {
        case 0: return game_chassis[item].name;
        case 1: return game_generators[item].name;
        case 2: return game_shields[item].name;
        default: return game_weapons[item].display_name;
    }
}

static const char *loadout_slots[] = {"Chassis", "Generator", "Shield", "Primary", "Support", "Sidekick"};

static Sprite equipment_icon(const Assets *assets, int category, int id) {
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
    Sprite icon = id < 0 ? assets->shop[cat] : equipment_icon(assets,cat,id);
    sprite(icon,x+24,y+23,32,0,id < 0 ? dim : WHITE);
    const char *slot = ui_text(loadout_slots[cat]);
    float label_size = fminf(24,24*104/fmaxf(1,MeasureTextEx(font,slot,24,0).x));
    DrawTextEx(font,slot,(Vector2){x+49,y+(47-label_size)/2},label_size,0,dim);
    const char *name = shop_item_name(cat,id);
    bool show_mark = id >= 0 && cat >= 3;
    float size = fminf(31,31*(show_mark ? 205 : 258)/fmaxf(1,MeasureTextEx(font,name,31,0).x));
    DrawTextEx(font,name,(Vector2){x+160,y+(47-size)/2},size,0,id < 0 ? dim : WHITE);
    if (show_mark)
        DrawTextEx(font,TextFormat("Mk %d",1+game_shop_equipped_level(p,cat)),
            (Vector2){x+371,y+12},21,0,color);
    DrawLine(x,y+47,x+424,y+47,rule);
}

static void draw_loadout(const Assets *assets, const Player *p, int x, int focus, Color color) {
    for (int c = 0; c < 6; ++c)
        draw_slot_row(assets,p,x,LOADOUT_TOP+c*LOADOUT_ROW,c,c == focus,color);
}

static void draw_flight_rail(const Game *game, const Assets *assets, int slot, bool keyboard_free) {
    const Player *p = &game->players[slot];
    draw_pilot_rail(assets,p,p,slot,false,keyboard_free);
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

static float canvas_zoom = 1.0f;

static void canvas_scissor(int x, int y, int w, int h) {
    int left = (int)roundf(x*canvas_zoom), top = (int)roundf(y*canvas_zoom);
    BeginScissorMode(left,top,(int)roundf((x+w)*canvas_zoom)-left,(int)roundf((y+h)*canvas_zoom)-top);
}

static void draw_game(const Game *game, const Assets *assets, bool keyboard_free) {
    float view_x = ARENA_X - game->stage.camera_x;
    ClearBackground(background);
    DrawRectangle(ARENA_X, 0, GAME_WIDTH, GAME_HEIGHT, background);
    canvas_scissor(ARENA_X, 0, GAME_WIDTH, GAME_HEIGHT);
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
        Sprite texture = event->kind == STAGE_BUILDING && game->stage.building_health[i] < 0 ?
            assets->stage_destroyed[i] : assets->stage_textures[i];
        float x = view_x + GAME_WIDTH*0.5f + event->x;
        /* A one-pixel apron on each side closes subpixel joins between authored deck pieces. */
        float width = event->width;
        if (event->kind == STAGE_DECORATION && strcmp(event->name,"SecretHint") != 0) width += 2;
        sprite(texture,x,y,width,event->rotation,assets->stage_tints[i]);
    }
    for (int layer = -2; layer <= 2; ++layer) draw_enemies(game, assets, layer);
    draw_enemies(game, assets, ENEMY_LAYER_ENTITY);
    for (int i = 0; i < GAME_PICKUPS; ++i) {
        const Pickup *p = &game->pickups[i];
        if (!p->active) continue;
        Sprite texture = assets->pickups[p->kind];
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
    for (int i=0; i<GAME_PLAYERS; ++i) draw_flight_rail(game,assets,i,keyboard_free);
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
        snprintf(stats[0].value,40,"%d HP",combat_points(c->armor));
        stats[1].label = "Acceleration";
        float thrust = c->speed * (game->players[slot].pilot == 0 ? 1.2f : 1);
        /* The hangar scale rates the stock Rookie at 6 g. */
        snprintf(stats[1].value,40,"%.1f g",6.0f*thrust/game_chassis[0].speed);
        stats[2].label = "Impact resistance";
        snprintf(stats[2].value,40,"%.0f%%",100*c->collision_resistance);
        count = 3;
    } else if (item >= 0 && cat == 1) {
        const Generator *g = &game_generators[item];
        stats[0].label = "Energy capacity";
        snprintf(stats[0].value,40,"%d EN",combat_points(g->capacity));
        stats[1].label = "Energy recovery";
        snprintf(stats[1].value,40,"%d EN/s",combat_points(g->regen*GAME_HZ));
        count = 2;
        if (g->on_coin || g->on_collision) {
            if (g->on_coin) {
                stats[2].label = "Energy per coin";
                snprintf(stats[2].value,40,"+%g EN",g->on_coin * 100.0);
            } else {
                stats[2].label = "Impact recovery";
                snprintf(stats[2].value,40,"%d EN / 100 impact HP",combat_points(g->on_collision));
            }
            ++count;
        }
    } else if (item >= 0 && cat == 2) {
        const Shield *h = &game_shields[item];
        stats[0].label = "Shield capacity";
        snprintf(stats[0].value,40,"%d HP",combat_points(h->capacity));
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
        int low = combat_points(min_hit), high = combat_points(max_hit);
        if (low == high) snprintf(stats[0].value,40,"%d HP",high);
        else snprintf(stats[0].value,40,"%d - %d HP",low,high);
        count = 1;
        if (cat == 3) {
            stats[1].label = "Estimated DPS";
            snprintf(stats[1].value,40,"%d HP/s",combat_points(dps));
            ++count;
        } else if (cat == 5) {
            const WeaponEmitter *e = w->emitters;
            if (item == WEAPON_BLADE) {
                stats[1].label = "Sweep reach";
                snprintf(stats[1].value,40,"%.0f arena units",38*e->scale_override);
                stats[2].label = e->lifetime > 9 ? "Return hit" : "Recovery";
                if (e->lifetime > 9) snprintf(stats[2].value,40,"%d HP",combat_points(max_hit*1.25f));
                else snprintf(stats[2].value,40,"%d ms",(int)lroundf(e->reload*1000));
                count = 3;
            } else if (item == WEAPON_CHARGE_SHOT) {
                stats[1].label = "Full charge";
                snprintf(stats[1].value,40,"%d ms",(int)lroundf(e->charge_frames*1000.0f/GAME_HZ));
                count = 2;
            } else {
                stats[1].label = "Shots per wing";
                snprintf(stats[1].value,40,"%d",w->count);
                count = 2;
                if (item == WEAPON_MISSILE_LAUNCHER && e->radius > 0) {
                    stats[2].label = "Blast radius";
                    snprintf(stats[2].value,40,"%.0f arena units",e->radius);
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

static bool draw_shop_catalog(const Game *game, const Assets *assets, int slot, int left, int offset) {
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
        const char *status = ui_text(equipped ? "Equipped" : purchased ? "Owned" : "");
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
    return show_select;
}

static void draw_shop_panel(const Game *game, const Assets *assets, int slot, int device, int offset, bool keyboard_free) {
    Font font = assets->ui_font;
    int x = slot ? ARENA_X + GAME_WIDTH : 0, left = x + 28, width = ARENA_X - 56;
    Color highlight = player_colors[slot];
    const Player *p = &game->players[slot];
    const GameMenu *menu = &game->menu;
    const Player *preview = shop_preview.players[slot].joined ? &shop_preview.players[slot] : p;
    draw_pilot_rail(assets,p,preview,slot,true,keyboard_free);
    if (!p->joined) return;
    int cat = menu->shop_category[slot];
    DrawLine(left, 298, left + width, 298, rule);
    bool can_select = true;
    if (menu->shop_catalog[slot]) {
        can_select = draw_shop_catalog(game,assets,slot,left,offset);
    } else {
        draw_loadout(assets,p,left+offset,menu->ready[slot] ? -1 : cat,highlight);
        int mark = game_shop_equipped_level(p,cat);
        draw_item_tip(game,assets,slot,LOADOUT_TOP+cat*LOADOUT_ROW+23,cat,game_shop_equipped(p,cat),mark,offset);
    }
    DrawLine(left,974,left+width,974,rule);
    if (menu->shop_purchase[slot]) return;
    if (!menu->ready[slot]) {
        if (can_select) button_prompt(font,left,993,device,GLYPH_CONFIRM,"Select");
        if (menu->shop_catalog[slot]) button_prompt(font,left+130,993,device,GLYPH_BACK,"Back");
    }
    button_prompt(font,left+width-145,993,device,GLYPH_READY,"Ready");
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
    else button_prompt(font,672,y+174,device,GLYPH_CONFIRM,cost > 0 ? "Buy" : "Confirm");
    button_prompt(font,1030,y+174,device,GLYPH_BACK,"Cancel");
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
    canvas_scissor(ARENA_X, 0, GAME_WIDTH, GAME_HEIGHT);
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
        draw_shop_panel(game, assets, slot, devices->device[slot], offset,input_owner(devices,INPUT_KEYBOARD)<0);
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

static void menu_row(Font font, const Game *game, MenuPage page, int row, bool selected, int x, int y, int width, int height) {
    DrawRectangle(x,y,width,height-3,selected ? surface : ink);
    DrawLine(x,y+height-3,x+width,y+height-3,rule);
    if (selected) DrawRectangle(x,y,5,height-3,accent);
    const char *label = ui_text(game_menu_page_label(game,page,row));
    if (page == MENU_SLOTS && row < GAME_SAVE_SLOTS) {
        const char *state = game->saves[row].valid ? TextFormat(ui_text("Stage %d"),game->saves[row].stage+1) :
            ui_text(game->save_errors[row] ? "Unreadable" : "Empty");
        label = TextFormat("%s - %s",TextFormat(ui_text("Slot %d"),row+1),state);
    } else if (page == MENU_STAGE_SELECT && row < 12) label = TextFormat(ui_text("Stage %d"),row+1);
    int label_width = page == MENU_OPTIONS && row < OPTION_COUNT ? width-232 : width-44;
    float label_size = fminf(28,28*label_width/fmaxf(1,MeasureTextEx(font,label,28,0).x));
    DrawTextEx(font,label,(Vector2){x+22,y+(height-label_size)/2-2},label_size,0,WHITE);
    if (game->menu.page == MENU_OPTIONS && (row >= OPTION_RENDER_SCALE && row < OPTION_COUNT)) {
        const char *setting = row == OPTION_RENDER_SCALE ?
            TextFormat("%d%%",render_scale_percent[game->menu.options[row]]) :
            row == OPTION_LANGUAGE ? "English" :
            row == OPTION_WINDOW_MODE ? (game->menu.options[row] ? "Fullscreen" : "Windowed") :
            row == OPTION_MSAA ? (const char *[]){"Off","2x","4x"}[game->menu.options[row]] :
            (game->menu.options[row] ? "On" : "Off");
        setting = ui_text(setting);
        float value_width = MeasureTextEx(font,setting,26,0).x;
        DrawTextEx(font,setting,(Vector2){x+width-value_width-22,y+(height-26)/2},26,0,accent);
    }
    if (game->menu.page == MENU_OPTIONS && row < OPTION_RENDER_SCALE) {
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
            button_prompt(font,x+172,526,0,GLYPH_START,"Join");
            if (input_owner(devices,INPUT_KEYBOARD)<0) key_prompt(font,x+336,526,"F2","Join");
            continue;
        }
        sprite(whole_texture(assets->portraits[pilot]),x+332,437,312,0,WHITE);
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
        else button_prompt(font,x+32,819,devices->device[slot],GLYPH_CONFIRM,"Select");
        button_prompt(font,x+455,819,devices->device[slot],GLYPH_BACK,game->menu.ready[slot] ? "Cancel" : "Back");
    }
}

static int shared_prompt_device(const PlayerDevices *devices) {
    if (input_owner(devices,devices->prompt_device) >= 0) return devices->prompt_device;
    return devices->device[0] >= 0 ? devices->device[0] : devices->device[1];
}

static void draw_menu_list(const Game *game,const Assets *assets,int frame,const PlayerDevices *devices,int offset,float clearance) {
    static const char *headings[MENU_CONTROLLERS+1] = {
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
    int row_height = stages ? 53 : page == MENU_OPTIONS ? 40 : 69;
    int y = title ? 606 : stages ? 180 : 437;
    bool dialog = page == MENU_DELETE || page == MENU_QUIT;
    MenuPage background = page == MENU_DELETE ? MENU_SLOTS : page == MENU_QUIT ? game->menu.parent : page;
    int cursor = dialog ? game->menu.dialog_cursor : game->menu.cursor;
    int count = game_menu_page_count(background);
    if (title) {
        float squad_height = width * (float)assets->title_squad.height / assets->title_squad.width;
        sprite(whole_texture(assets->title_squad),x+width/2,y-16-squad_height/2,width,0,WHITE);
        DrawRectangle(x,y-9,width,2,rule);
        sprite(whole_texture(assets->title_logo),960,151,776,0,WHITE);
    } else {
        DrawRectangle(x-32,y-114,width+64,192+count*row_height,ink);
        DrawRectangle(x-32,y-114,width+64,3,accent);
    }
    x += offset;
    const char *heading = ui_text(headings[background]);
    if (heading && title)
        DrawTextEx(font,heading,(Vector2){(CANVAS_WIDTH-MeasureTextEx(font,heading,34,0).x)/2+offset,295},34,0,WHITE);
    else if (heading) DrawTextEx(font,heading,(Vector2){x,y-78},44,0,WHITE);
    for (int row=0; row<count; ++row) menu_row(font,game,background,row,row==cursor,x,y+row*row_height,width,row_height);
    int device = shared_prompt_device(devices);
    if (dialog) {
        bool deleting = page == MENU_DELETE;
        bool exiting = !deleting && background == MENU_MAIN;
        const char *verb = deleting ? "Delete" : exiting ? "Quit" : "Return to title";
        int top = (CANVAS_HEIGHT-228)/2;
        DrawRectangle(0,0,CANVAS_WIDTH,CANVAS_HEIGHT,Fade(BLACK,0.58f));
        dialog_frame(top,danger);
        DrawTextEx(font,deleting ? TextFormat("Delete slot %d?",game->menu.delete_slot+1) :
            exiting ? "Quit Galaxy Defenders?" : "Return to title?",(Vector2){672,top+24},38,0,WHITE);
        DrawTextEx(font,deleting ? "This permanently deletes its saved progress." :
            exiting ? "Close the game and return to the desktop." : "Leave this session and return to the title screen.",(Vector2){672,top+89},25,0,dim);
        for (int i=0; i<2; ++i) {
            bool selected = game->menu.cursor == i;
            int left = 672+i*294;
            if (selected) {
                DrawRectangle(left,top+166,280,48,surface);
                DrawRectangle(left,top+166,4,48,i ? danger : accent);
            }
            if (!i || selected)
                button_prompt(font,left+16,top+173,device,i ? GLYPH_CONFIRM : GLYPH_BACK,i ? verb : "Cancel");
            else DrawTextEx(font,verb,(Vector2){left+16,top+174},25,0,dim);
        }
        return;
    }
    int footer = title ? 993 : y+row_height*count+30;
    DrawLine(x,footer-13,x+width,footer-13,rule);
    bool adjust = page == MENU_OPTIONS && game->menu.cursor < OPTION_COUNT;
    int advance = button_prompt(font,x,footer,device,adjust ? GLYPH_HORIZONTAL : GLYPH_CONFIRM,adjust ? "Adjust" : "Select")+28;
    if (page != MENU_MAIN && page != MENU_OVER) advance += button_prompt(font,x+advance,footer,device,GLYPH_BACK,"Back")+28;
    int row = game->menu.cursor;
    if (page == MENU_SLOTS && row < GAME_SAVE_SLOTS && (game->saves[row].valid || game->save_errors[row]))
        button_prompt(font,x+advance,footer,device,GLYPH_READY,"Delete");
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
        bool slide = from_title && game_menu_is_title(game) && page != MENU_DELETE && page != MENU_QUIT && previous_page != MENU_QUIT &&
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
        sprite(whole_texture(assets->portraits[pilot]),648,821,124,0,WHITE);
        DrawTextEx(assets->ui_font,pilot_names[pilot],(Vector2){735,774},24,0,accent);
        DrawTextEx(assets->ui_font,"Copy, Control. Launching.",(Vector2){735,812},34,0,WHITE);
        button_prompt(assets->ui_font,1180,923,shared_prompt_device(devices),GLYPH_CONFIRM,"Skip");
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
            Sprite texture = assets->medals[medal - 1];
            float scale = fmaxf(1, 2 - (counter - 299) * 0.125f);
            sprite(texture, 968, 540, texture.width * scale, 0, WHITE);
        }
        if (counter >= 360) {
            button_prompt(assets->ui_font,880,630,shared_prompt_device(devices),GLYPH_CONFIRM,"Select");
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
    for (int slot=0; slot<GAME_PLAYERS; ++slot) {
        if (game->players[slot].joined) devices->choosing[slot] = false;
        if (devices->choosing[slot] && (game_menu_is_title(game) || game->over))
            input_drop(game,devices,devices->device[slot]);
    }
    if (!accept_input || game->menu.page == MENU_CONTROLLERS) return;
    bool start = IsKeyPressed(KEY_ENTER);
    bool join = IsKeyPressed(KEY_F2);
    int keyboard_slot = input_owner(devices,INPUT_KEYBOARD);
    bool keyboard_input = game->players[0].joined && devices->device[0] < 0 &&
        (IsKeyPressed(KEY_X) || IsKeyPressed(KEY_Z) || IsKeyPressed(KEY_ESCAPE) || IsKeyPressed(KEY_UP) || IsKeyPressed(KEY_DOWN));
    if (start || join || keyboard_input) devices->prompt_device = INPUT_KEYBOARD;
    if (keyboard_slot < 0 && (start || join || keyboard_input)) {
        input_claim(game,devices,INPUT_KEYBOARD);
    } else if (keyboard_slot >= 0 && !devices->choosing[keyboard_slot]) {
        if (start && (game->menu.page == MENU_NONE || game->menu.page == MENU_SHOP)) game_menu_open(game,MENU_PAUSE);
        else if (start && game->menu.page == MENU_PAUSE) menu_input(game,assets,MENU_BACK,keyboard_slot);
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
        devices->prompt_device = device;
        if (slot < 0) {
            bool empty = !game->players[0].joined && !game->players[1].joined;
            if (input_claim(game, devices, device) >= 0) {
                if (empty && game->menu.page == MENU_PAUSE) menu_input(game, assets, MENU_BACK, 0);
            }
        } else if (pad->buttons & PAD_SELECT) input_drop(game, devices, device);
        else if (devices->choosing[slot]) continue;
        else if (!game->players[slot].joined) game_join(game, slot);
        else if (game->menu.page == MENU_NONE || game->menu.page == MENU_SHOP) game_menu_open(game, MENU_PAUSE);
        else if (game->menu.page == MENU_PAUSE) menu_input(game, assets, MENU_BACK, slot);
        handled_start = true;
        pad->pressed = 0; /* The join/pause press must not also select a menu row. */
    }
}

static uint32_t menu_buttons(int device, const PadInput pads[INPUT_PADS]) {
    if (device >= 0 && device < INPUT_PADS) return pads[device].pressed;
    if (device != INPUT_KEYBOARD) return 0;
    uint32_t buttons = 0;
    if (IsKeyPressed(KEY_Z) || IsKeyPressed(KEY_ESCAPE)) buttons |= PAD_BACK;
    if (IsKeyPressed(KEY_X)) buttons |= PAD_CONFIRM;
    if (IsKeyPressed(KEY_C)) buttons |= PAD_READY;
    if (IsKeyPressed(KEY_LEFT)) buttons |= PAD_LEFT;
    if (IsKeyPressed(KEY_RIGHT)) buttons |= PAD_RIGHT;
    if (IsKeyPressed(KEY_UP)) buttons |= PAD_UP;
    if (IsKeyPressed(KEY_DOWN)) buttons |= PAD_DOWN;
    return buttons;
}

static int device_rows(const PadInput pads[INPUT_PADS], int rows[INPUT_PADS+1]) {
    rows[0] = INPUT_KEYBOARD;
    int count = 1;
    for (int i=0; i<INPUT_PADS; ++i) if (pads[i].connected) rows[count++] = i;
    return count;
}

static void poll_assignments(Game *game, PlayerDevices *devices, const PadInput pads[INPUT_PADS]) {
    int rows[INPUT_PADS+1], count = device_rows(pads,rows), row = 0;
    uint32_t buttons = 0;
    for (int i=0; i<count; ++i) {
        uint32_t pressed = menu_buttons(rows[i],pads);
        if (pressed) devices->prompt_device = rows[i];
        buttons |= pressed;
        if (rows[i] == devices->selected_device) row = i;
    }
    for (int i=0; i<GAME_PLAYERS; ++i) if (devices->choosing[i]) {
        if (buttons & PAD_BACK) input_pick_pilot(game,devices,i,MENU_BACK);
        else if (buttons & PAD_CONFIRM) input_pick_pilot(game,devices,i,MENU_CONFIRM);
        else if (buttons & PAD_LEFT) input_pick_pilot(game,devices,i,MENU_LEFT);
        else if (buttons & PAD_RIGHT) input_pick_pilot(game,devices,i,MENU_RIGHT);
        return;
    }
    if (buttons & PAD_BACK) {
        game_menu_input(game,MENU_BACK,0);
        return;
    }
    int device = rows[row], owner = input_owner(devices,device);
    if ((buttons & PAD_UP) && row > 0) --row;
    else if ((buttons & PAD_DOWN) && row+1 < count) ++row;
    else if ((buttons & PAD_LEFT) && owner != 0) input_assign(game,devices,device,owner == 1 ? -1 : 0);
    else if ((buttons & PAD_RIGHT) && owner != 1) input_assign(game,devices,device,owner == 0 ? -1 : 1);
    devices->selected_device = rows[row];
}

static void draw_assignments(const Game *game, const Assets *assets, const PlayerDevices *devices, const PadInput pads[INPUT_PADS]) {
    int rows[INPUT_PADS+1], count = device_rows(pads,rows);
    Font font = assets->ui_font;
    int x = 368, y = 220, width = 1184;
    int bottom = y+200+count*60;
    DrawRectangle(0,0,CANVAS_WIDTH,CANVAS_HEIGHT,Fade(BLACK,.65f));
    DrawRectangle(x,y,width,bottom-y+70,ink);
    DrawRectangle(x,y,width,3,accent);
    DrawTextEx(assets->heading_font,"Controllers",(Vector2){x+30,y+20},48,0,WHITE);
    text_center(font,"Unassigned",x+592,y+111,28,dim);
    for (int slot=0; slot<GAME_PLAYERS; ++slot) {
        int left = x+40+slot*768;
        const Player *p = &game->players[slot];
        if (p->initialized) {
            sprite(whole_texture(assets->portraits[p->pilot]),left+38,y+114,78,0,p->joined ? WHITE : dim);
            DrawTextEx(font,TextFormat("P%d   %s",slot+1,pilot_names[p->pilot]),(Vector2){left+90,y+84},26,0,p->joined ? player_colors[slot] : dim);
            if (!p->joined) DrawTextEx(font,"Disconnected",(Vector2){left+90,y+119},21,0,dim);
        } else text_center(font,TextFormat("P%d",slot+1),left+168,y+111,28,dim);
    }
    for (int i=0; i<count; ++i) {
        int device = rows[i], owner = input_owner(devices,device);
        int column = owner == 0 ? 0 : owner == 1 ? 2 : 1;
        int left = x+24+column*384, top = y+160+i*60;
        const char *name = device == INPUT_KEYBOARD ? "Keyboard" : device >= 4 ?
            TextFormat("DualShock 4  #%d",device-3) : TextFormat("%s  #%d",GetGamepadName(device),device+1);
        DrawLine(x+24,top+54,x+width-24,top+54,rule);
        bool selected = device == devices->selected_device;
        DrawRectangle(left,top,368,51,selected ? surface : ink);
        if (selected) DrawRectangle(left,top,4,51,accent);
        float size = fminf(25,25*310/fmaxf(1,MeasureTextEx(font,name,25,0).x));
        text_center(font,name,left+184,top+10,size,selected ? WHITE : dim);
        if (selected && column > 0) DrawTextEx(font,"<",(Vector2){left+8,top+10},23,0,accent);
        if (selected && column < 2) DrawTextEx(font,">",(Vector2){left+344,top+10},23,0,accent);
    }
    int next = x+30+button_prompt(font,x+30,bottom,devices->prompt_device,GLYPH_VERTICAL,"Select device")+28;
    button_prompt(font,next,bottom,devices->prompt_device,GLYPH_HORIZONTAL,"Assign");
    if (game->players[0].joined || game->players[1].joined)
        button_prompt(font,x+width-220,bottom,devices->prompt_device,GLYPH_BACK,"Done");
    else DrawTextEx(font,"Assign a controller to continue",(Vector2){x+width-390,bottom},24,0,accent);
    for (int i=0; i<GAME_PLAYERS; ++i) if (devices->choosing[i]) {
        int top = (CANVAS_HEIGHT-228)/2, pilot = devices->pilot[i];
        DrawRectangle(0,0,CANVAS_WIDTH,CANVAS_HEIGHT,Fade(BLACK,.55f));
        dialog_frame(top,player_colors[i]);
        sprite(whole_texture(assets->portraits[pilot]),742,top+80,132,0,WHITE);
        DrawTextEx(font,TextFormat("P%d   Choose pilot",i+1),(Vector2){840,top+25},26,0,dim);
        DrawTextEx(font,TextFormat("<   %s   >",pilot_names[pilot]),(Vector2){840,top+75},36,0,WHITE);
        button_prompt(font,672,top+174,devices->device[i],GLYPH_CONFIRM,"Join");
        button_prompt(font,1030,top+174,devices->device[i],GLYPH_BACK,"Cancel");
        break;
    }
}

static void draw_join_picker(const Assets *assets, const PlayerDevices *devices, int slot) {
    int left = slot ? ARENA_X+GAME_WIDTH : 0, pilot = devices->pilot[slot];
    Font font = assets->ui_font;
    DrawRectangle(left,0,ARENA_X,GAME_HEIGHT,ink);
    DrawRectangle(left+28,32,58,36,player_colors[slot]);
    text_center(font,slot ? "P2" : "P1",left+57,33,27,ink);
    DrawTextEx(font,pilot_names[pilot],(Vector2){left+98,26},38,0,WHITE);
    DrawLine(left+28,88,left+ARENA_X-28,88,rule);
    sprite(whole_texture(assets->portraits[pilot]),left+110,204,164,0,WHITE);
    DrawTextEx(font,"<   Pilot   >",(Vector2){left+212,174},29,0,WHITE);
    int device = devices->device[slot];
    button_prompt(font,left+32,326,device,GLYPH_CONFIRM,"Join");
    button_prompt(font,left+196,326,device,GLYPH_BACK,"Cancel");
}

static void poll_menu(Game *game, Assets *assets, PlayerDevices *devices, const PadInput pads[INPUT_PADS], const bool picking[GAME_PLAYERS]) {
    if (game->menu.page == MENU_PAUSE && IsKeyPressed(KEY_P)) {
        menu_input(game, assets, MENU_BACK, 0); return;
    }
    for (int slot = 0; slot < GAME_PLAYERS; ++slot) {
        if (picking[slot]) continue;
        uint32_t buttons = menu_buttons(devices->device[slot],pads);
        const unsigned bits[] = {PAD_BACK,PAD_UP,PAD_DOWN,PAD_LEFT,PAD_RIGHT,PAD_CONFIRM,PAD_READY};
        const MenuAction actions[] = {MENU_BACK,MENU_UP,MENU_DOWN,MENU_LEFT,MENU_RIGHT,MENU_CONFIRM,MENU_READY};
        for (int i=0; i<7; ++i) if (buttons & bits[i]) {
            devices->prompt_device = devices->device[slot];
            MenuPage previous = game->menu.page;
            menu_input(game,assets,actions[i],slot);
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

static PlayerInput poll_keyboard(void) {
    return (PlayerInput){
        .x = IsKeyDown(KEY_RIGHT) - IsKeyDown(KEY_LEFT),
        .y = IsKeyDown(KEY_DOWN) - IsKeyDown(KEY_UP),
        .fire = IsKeyDown(KEY_X), .focus = IsKeyDown(KEY_C),
        .sidekick_left = IsKeyDown(KEY_Z), .sidekick_right = IsKeyDown(KEY_Z)
    };
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
    canvas_scissor(0, CANVAS_HEIGHT - panel_height, CANVAS_WIDTH, panel_height);
#else
    canvas_scissor(0, 0, CANVAS_WIDTH, panel_height);
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

#ifndef GALAXY_MEMORY
typedef struct {
    unsigned int framebuffer, color;
} MultisampleTarget;

static void unload_msaa(MultisampleTarget *target) {
    glDeleteFramebuffers(1,&target->framebuffer);
    glDeleteRenderbuffers(1,&target->color);
    *target = (MultisampleTarget){0};
}

static bool create_msaa(MultisampleTarget *target, int width, int height, int samples) {
    glGenFramebuffers(1,&target->framebuffer);
    glGenRenderbuffers(1,&target->color);
    glBindFramebuffer(GL_FRAMEBUFFER,target->framebuffer);
    glBindRenderbuffer(GL_RENDERBUFFER,target->color);
    glRenderbufferStorageMultisample(GL_RENDERBUFFER,samples,GL_RGBA8,width,height);
    glFramebufferRenderbuffer(GL_FRAMEBUFFER,GL_COLOR_ATTACHMENT0,GL_RENDERBUFFER,target->color);
    bool ready = glCheckFramebufferStatus(GL_FRAMEBUFFER) == GL_FRAMEBUFFER_COMPLETE;
    int actual = 0;
    glGetRenderbufferParameteriv(GL_RENDERBUFFER,GL_RENDERBUFFER_SAMPLES,&actual);
    glBindRenderbuffer(GL_RENDERBUFFER,0);
    glBindFramebuffer(GL_FRAMEBUFFER,0);
    if (!ready || actual < samples) {
        unload_msaa(target);
        return false;
    }
    glEnable(GL_MULTISAMPLE);
    TraceLog(LOG_INFO,"MSAA: %dx requested, %d samples allocated (%dx%d)",samples,actual,width,height);
    return true;
}
#endif

int app_run(Game *game, Console *console, int frames, const char *capture, bool mute, float volume, bool show_console) {
    Assets assets = {0};
    double save_retry_at = 0;
    double selection_retry_at = 0;
    char save_paths[GAME_SAVE_SLOTS][2048] = {{0}};
    char selection_path[2048] = {0}, settings_path[2048] = {0};
    int stored_options[OPTION_COUNT];
    double settings_retry_at = 0;
    int remembered_slot = -1;
    if (!frames) {
        const char *root = getenv("LOCALAPPDATA");
        char directory[1800];
        int length = snprintf(directory, sizeof(directory), "%s/GalaxyDefenders", root ? root : ".");
        if (length > 0 && length < (int)sizeof(directory) && (DirectoryExists(directory) || MakeDirectory(directory) == 0)) {
            snprintf(settings_path,sizeof(settings_path),"%s/settings.cfg",directory);
            if (FileExists(settings_path) && !game_options_read(game->menu.options,settings_path))
                console_log(console,"Settings file unreadable; using defaults.");
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
    SetConfigFlags(FLAG_WINDOW_RESIZABLE | FLAG_VSYNC_HINT | (frames ? FLAG_WINDOW_HIDDEN : 0));
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
    memcpy(stored_options, game->menu.options, sizeof(stored_options));
    if (!assets_load(&assets, console, mute, volume)) {
        assets_unload(&assets);
        if (IsRenderTextureValid(canvas)) UnloadRenderTexture(canvas);
        CloseWindow();
        return 1;
    }
#ifndef GALAXY_MEMORY
    Shader edge_aa = LoadShader(NULL,asset_path("shaders/edge_aa.fs"));
    int texel_location = GetShaderLocation(edge_aa,"texelSize");
    if (texel_location < 0) {
        console_log(console,"ERROR: could not load anti-aliasing shader");
        UnloadShader(edge_aa);
        assets_unload(&assets);
        CloseWindow();
        return 1;
    }
    int requested_width = 0, applied_msaa = -1;
    MultisampleTarget multisample = {0};
#endif
    bool console_open = show_console, console_full = false, command_failed = false, muted = mute;
    int rendered = 0, result = 0, menu_frame = 0;
    double accumulator = 0;
    FrameGraph frame_graph = {0};
    PlayerDevices devices;
    PadInput pads[INPUT_PADS] = {0};
    input_devices_init(&devices);
    if (!frames && !ds4_init()) console_log(console, "Native DS4 input unavailable; standard gamepads remain enabled.");
    console_log(console, "Ready. Type help; console commands and launch scripts share the same game.");
    bool link_cursor = false;
    while (!WindowShouldClose() && !console->quit && !game->menu.quit) {
        double frame_start = GetTime();
        if (!console_open && !console->music_album && IsKeyPressed(KEY_F8)) {
            int direction = IsKeyDown(KEY_LEFT_SHIFT) || IsKeyDown(KEY_RIGHT_SHIFT) ? -1 : 1;
            int *language = &game->menu.options[OPTION_LANGUAGE];
            *language = (*language + direction + GAME_LANGUAGE_COUNT) % GAME_LANGUAGE_COUNT;
        }
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
        int keyboard_owner = input_owner(&devices,INPUT_KEYBOARD);
        bool keyboard_picking = keyboard_owner >= 0 && devices.choosing[keyboard_owner];
        bool picking[GAME_PLAYERS] = {devices.choosing[0],devices.choosing[1]};
        if (game->menu.page != MENU_CONTROLLERS && !console->music_album && !console_open && !toggled && IsWindowFocused()) {
            for (int i=0; i<GAME_PLAYERS; ++i) if (picking[i]) {
                uint32_t b = menu_buttons(devices.device[i],pads);
                if (b & PAD_BACK) input_pick_pilot(game,&devices,i,MENU_BACK);
                else if (b & PAD_LEFT) input_pick_pilot(game,&devices,i,MENU_LEFT);
                else if (b & PAD_RIGHT) input_pick_pilot(game,&devices,i,MENU_RIGHT);
                else if (b & PAD_CONFIRM) input_pick_pilot(game,&devices,i,MENU_CONFIRM);
            }
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
            if (game->menu.page == MENU_CONTROLLERS) poll_assignments(game,&devices,pads);
            else poll_menu(game, &assets, &devices, pads, picking);
        } else if (!toggled) {
            if (IsKeyPressed(KEY_P) || (IsKeyPressed(KEY_ESCAPE) && !keyboard_picking)) game_menu_open(game, MENU_PAUSE);
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
                    device == INPUT_KEYBOARD ? poll_keyboard() : (PlayerInput){0};
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
                /* Present the fully black switch frame even after a stalled window frame. */
                if (game->transition_frames == 15) { accumulator = 0; break; }
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
        if (settings_path[0] && memcmp(stored_options,game->menu.options,sizeof(stored_options)) && GetTime() >= settings_retry_at) {
            if (game_options_write(game->menu.options,settings_path))
                memcpy(stored_options,game->menu.options,sizeof(stored_options));
            else {
                console_log(console,"Could not write settings; retrying");
                settings_retry_at = GetTime()+5;
            }
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
        if (IsWindowState(FLAG_BORDERLESS_WINDOWED_MODE) != (game->menu.options[OPTION_WINDOW_MODE] != 0))
            ToggleBorderlessWindowed();
        float screen_fraction = 0.875f + game->menu.options[OPTION_SCREEN_SIZE]*0.0125f;
        float scale = fminf(GetScreenWidth() / (float)CANVAS_WIDTH, GetScreenHeight() / (float)CANVAS_HEIGHT);
        scale *= screen_fraction;
        Rectangle destination = {(GetScreenWidth() - CANVAS_WIDTH * scale) / 2,
            (GetScreenHeight() - CANVAS_HEIGHT * scale) / 2, CANVAS_WIDTH * scale, CANVAS_HEIGHT * scale};
        float units = fminf(GetRenderWidth()/16.0f,GetRenderHeight()/9.0f)*screen_fraction;
        int blocks = (int)fmaxf(1,roundf(units*render_scale_percent[game->menu.options[OPTION_RENDER_SCALE]]/100.0f));
        if (requested_width != blocks*16) {
            requested_width = blocks*16;
            RenderTexture2D replacement = LoadRenderTexture(requested_width,blocks*9);
            if (IsRenderTextureValid(replacement)) {
                if (IsRenderTextureValid(canvas)) UnloadRenderTexture(canvas);
                canvas = replacement;
                applied_msaa = -1;
                SetTextureFilter(canvas.texture,TEXTURE_FILTER_BILINEAR);
                Vector2 texel = {1.0f/canvas.texture.width,1.0f/canvas.texture.height};
                SetShaderValue(edge_aa,texel_location,&texel,SHADER_UNIFORM_VEC2);
                canvas_zoom = (float)canvas.texture.width/CANVAS_WIDTH;
            } else {
                console_log(console,"Could not allocate %d x %d render target",requested_width,blocks*9);
                if (!IsRenderTextureValid(canvas)) { result = 1; break; }
            }
        }
        if (applied_msaa != game->menu.options[OPTION_MSAA]) {
            unload_msaa(&multisample);
            int level = game->menu.options[OPTION_MSAA];
            while (level && !create_msaa(&multisample,canvas.texture.width,canvas.texture.height,1 << level)) --level;
            if (level != game->menu.options[OPTION_MSAA]) {
                console_log(console,"MSAA allocation failed; using %d samples",1 << level);
                game->menu.options[OPTION_MSAA] = level;
            }
            applied_msaa = level;
        }
        RenderTexture2D draw_target = canvas;
        if (multisample.framebuffer) draw_target.id = multisample.framebuffer;
        BeginTextureMode(draw_target);
        BeginMode2D((Camera2D){.zoom=canvas_zoom});
#endif
        ui_language = game->menu.options[OPTION_LANGUAGE];
        bool cjk = ui_uses_cjk[ui_language];
        assets.ui_font = cjk ? assets.cjk_font : assets.western_body;
        assets.heading_font = cjk ? assets.cjk_font : assets.western_heading;
        if (!game_menu_is_title(game) && game->menu.page != MENU_SHOP && game->menu.page != MENU_FINISH &&
            game->menu.page != MENU_STAGE_SELECT)
            draw_game(game, &assets, keyboard_owner < 0);
        int animation_tick = frames ? menu_frame++ : (int)fmod(GetTime()*GAME_HZ, 16777216.0);
        if (game->menu.page == MENU_CONTROLLERS) draw_assignments(game,&assets,&devices,pads);
        else {
            draw_menu(game, &assets, animation_tick, &devices);
            for (int i=0; i<GAME_PLAYERS; ++i) if (devices.choosing[i]) draw_join_picker(&assets,&devices,i);
        }
        int link = 0;
        if (game->menu.page == MENU_MAIN) {
            Vector2 pointer = {-1,-1};
#ifndef GALAXY_MEMORY
            if (!frames && !console_open && !console->music_album && IsWindowFocused() && !game->transition_frames && scale > 0) {
                Vector2 mouse = GetMousePosition();
                pointer = (Vector2){(mouse.x-destination.x)/scale,(mouse.y-destination.y)/scale};
            }
#endif
            link = title_links(assets.ui_font,pointer);
        }
        if ((link != 0) != link_cursor) {
            link_cursor = link != 0;
            SetMouseCursor(link_cursor ? MOUSE_CURSOR_POINTING_HAND : MOUSE_CURSOR_DEFAULT);
        }
        if (link && IsMouseButtonPressed(MOUSE_BUTTON_LEFT))
            OpenURL(link == 1 ? "https://kalin.games/" : "https://x.com/kalin_t");
        if (game->active_slot >= 0 && game->active_slot < GAME_SAVE_SLOTS && game->save_errors[game->active_slot])
            text_center(assets.ui_font, "Save unavailable - progress has not been written", 960, 940, 24, RED);
        if (game->transition_frames > 0 && game->menu.page != MENU_CONTROLLERS) {
            float alpha = game->transition_frames > 15 ? (30 - game->transition_frames) / 15.0f : game->transition_frames / 15.0f;
            if (game->transition_frames > 15) alpha += (1-alpha)*game->fade_start;
            alpha = alpha*alpha*(3-2*alpha);
            DrawRectangle(0, 0, CANVAS_WIDTH, CANVAS_HEIGHT, Fade(BLACK, alpha));
        }
        if (console->music_album) draw_music_album(console, &assets);
        if (console_open) draw_console(console, assets.console_font, command_failed, console_full);
        if (console->perf) draw_frame_graph(&frame_graph, assets.console_font);
#ifndef GALAXY_MEMORY
        EndMode2D();
        EndTextureMode();
        if (multisample.framebuffer) {
            int w = canvas.texture.width, h = canvas.texture.height;
            glBindFramebuffer(GL_READ_FRAMEBUFFER,multisample.framebuffer);
            glBindFramebuffer(GL_DRAW_FRAMEBUFFER,canvas.id);
            glBlitFramebuffer(0,0,w,h,0,0,w,h,GL_COLOR_BUFFER_BIT,GL_NEAREST);
            glBindFramebuffer(GL_FRAMEBUFFER,0);
        }
        BeginDrawing();
        ClearBackground(BLACK);
        if (game->menu.options[OPTION_AA]) BeginShaderMode(edge_aa);
        DrawTexturePro(canvas.texture,(Rectangle){0,0,canvas.texture.width,-canvas.texture.height},destination,(Vector2){0},0,WHITE);
        if (game->menu.options[OPTION_AA]) EndShaderMode();
#endif
        if (frames && rendered+1 == frames && capture) {
            rlDrawRenderBatchActive();
            Image image = LoadImageFromScreen();
#ifdef GALAXY_MEMORY
            /* The software framebuffer is already top-down before raylib's OpenGL flip. */
            ImageFlipVertical(&image);
#endif
            if (!IsImageValid(image) || !ExportImage(image,capture)) result = 1;
            UnloadImage(image);
        }
        double work_end = GetTime();
        EndDrawing();
        frame_graph.work_ms[frame_graph.cursor] = (float)((work_end - frame_start) * 1000);
        frame_graph.frame_ms[frame_graph.cursor] = (float)((GetTime() - frame_start) * 1000);
        frame_graph.cursor = (frame_graph.cursor + 1) % FRAME_SAMPLES;
        if (frame_graph.count < FRAME_SAMPLES) ++frame_graph.count;
        if (frames && ++rendered >= frames) break;
    }
    console_execute(console, game, "state");
    assets_unload(&assets);
    if (IsRenderTextureValid(canvas)) UnloadRenderTexture(canvas);
#ifndef GALAXY_MEMORY
    unload_msaa(&multisample);
    UnloadShader(edge_aa);
#endif
    if (!frames) ds4_shutdown();
    CloseWindow();
    return result;
}
