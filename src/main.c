#include "game.h"
#include "console.h"
#include <errno.h>
#include <math.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#ifndef GALAXY_HEADLESS
int app_run(Game *game, Console *console, int frames, const char *capture, bool mute, float volume, bool show_console);
#endif

static Game game;
static Console console;

int main(int argc, char **argv) {
    bool headless = false, mute = false, show_console = false;
    int frames = 0;
    float volume = -1;
    const char *script = NULL, *commands = NULL, *capture = NULL;
#ifdef GALAXY_HEADLESS
    headless = true;
#endif
    for (int i = 1; i < argc; ++i) {
        if (!strcmp(argv[i], "--help")) {
            puts("Galaxy Defenders native combat slice\n"
                 "  --headless           no window/audio; exits after commands\n"
                 "  --script PATH        bounded command file\n"
                 "  --command COMMANDS   semicolon-separated console commands\n"
                 "  --frames N           graphical smoke: exit after N frames\n"
                 "  --capture PATH       save the final graphical frame (requires --frames)\n"
                 "  --mute               disable audio device\n"
                 "  --volume 0..1        master volume (default .3; --frames default .01)\n"
                 "  --console            open the graphical debug console at launch\n"
                 "Use --command help for the shared command list.");
            return 0;
        } else if (!strcmp(argv[i], "--headless")) headless = true;
        else if (!strcmp(argv[i], "--mute")) mute = true;
        else if (!strcmp(argv[i], "--console")) show_console = true;
        else if (!strcmp(argv[i], "--script") && i + 1 < argc && !script) script = argv[++i];
        else if (!strcmp(argv[i], "--command") && i + 1 < argc && !commands) commands = argv[++i];
        else if (!strcmp(argv[i], "--capture") && i + 1 < argc && !capture) capture = argv[++i];
        else if (!strcmp(argv[i], "--volume") && i + 1 < argc && volume < 0) {
            char *end;
            errno = 0;
            const char *text = argv[++i];
            volume = strtof(text, &end);
            if (errno || end == text || *end || !isfinite(volume) || volume < 0 || volume > 1) {
                fputs("Invalid --volume value (expected 0..1)\n", stderr);
                return 2;
            }
        }
        else if (!strcmp(argv[i], "--frames") && i + 1 < argc && !frames) {
            char *end;
            errno = 0;
            long value = strtol(argv[++i], &end, 10);
            if (errno || *end || value < 1 || value > 36000) { fputs("Invalid --frames value\n", stderr); return 2; }
            frames = (int)value;
        } else { fprintf(stderr, "Unknown or incomplete argument: %s\n", argv[i]); return 2; }
    }
    if ((headless && (frames || capture || show_console)) || (capture && !frames)) {
        fputs("--frames/--capture require a graphical run; capture also requires --frames\n", stderr);
        return 2;
    }
#ifdef GALAXY_MEMORY
    mute = true; /* Software captures never open an audio device. */
    if (!headless && !frames) {
        fputs("Software render checks require --frames N (no interactive window)\n", stderr);
        return 2;
    }
#endif
    game_reset(&game, 1);
    if (!headless && !script && !commands) game_menu_open(&game, MENU_MAIN);
    if (script && !console_script(&console, &game, script)) return 1;
    if (commands && !console.quit && !console_chain(&console, &game, commands)) return 1;
    if (headless || console.quit) {
        console_execute(&console, &game, "state");
        return 0;
    }
#ifndef GALAXY_HEADLESS
    if (volume < 0) volume = frames ? 0.01f : 0.3f;
    return app_run(&game, &console, frames, capture, mute, volume, show_console);
#else
    (void)mute;
    return 0;
#endif
}
