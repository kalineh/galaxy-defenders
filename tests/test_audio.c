/* Exercise the real audio router without creating a window. */
#include "../src/app.c"
#include <time.h>

int main(void) {
    static Game game;
    Assets assets = {0};
    InitAudioDevice();
    if (!IsAudioDeviceReady()) return 1;
    SetMasterVolume(0.01f);
    bool sounds_valid = true;
    for (int i = 0; i < WEAPON_COUNT; ++i) {
        if (!game_weapons[i].sound || !game_weapons[i].sound[0]) continue;
        char path[160];
        snprintf(path, sizeof(path), "assets/audio/%s.wav", game_weapons[i].sound);
        Sound sound = LoadSound(path);
        sounds_valid = IsSoundValid(sound) && sounds_valid;
        if (IsSoundValid(sound)) UnloadSound(sound);
    }
    assets.audio = true;
    assets.music = LoadMusicStream("assets/audio/The_Voyage.wav");
    assets.title_music = LoadMusicStream("assets/audio/Konami's_Moon_Base.wav");
    assets.pause_music = LoadMusicStream("assets/audio/1-X-0.wav");
    assets.music_name = "The_Voyage";
    assets.music_mode = assets.music_revision = -1;
    assets.music_event = -2;
    game_reset(&game, 1);
    game_menu_open(&game, MENU_MAIN);
    update_audio(&game, &assets);
    /* New Game resets tick/revision to zero, as on the title screen. */
    game_reset(&game, 1);
    update_audio(&game, &assets);
    bool started = IsMusicStreamPlaying(assets.music);
    clock_t until = clock() + CLOCKS_PER_SEC / 5;
    while (clock() < until) update_audio(&game, &assets);
    float position = GetMusicTimePlayed(assets.music);
    game_menu_open(&game, MENU_PAUSE);
    update_audio(&game, &assets);
    bool paused = !IsMusicStreamPlaying(assets.music);
    game_menu_open(&game, MENU_NONE);
    update_audio(&game, &assets);
    bool resumed = IsMusicStreamPlaying(assets.music) && GetMusicTimePlayed(assets.music) >= position;
    UnloadMusicStream(assets.music);
    UnloadMusicStream(assets.title_music);
    UnloadMusicStream(assets.pause_music);
    CloseAudioDevice();
    printf("Audio routing: started=%d advanced=%d paused=%d resumed=%d\n", started, position > 0, paused, resumed);
    printf("Weapon sound assets: %s\n", sounds_valid ? "PASS" : "FAIL");
    return !(started && position > 0 && paused && resumed && sounds_valid);
}
