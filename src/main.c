#include "game.h"
#include "console.h"
#include <errno.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#ifndef GALAXY_HEADLESS
int app_run(Game *game, Console *console, int frames, const char *capture, bool mute);
#endif

static Game game;
static Console console;

int main(int argc, char **argv) {
    bool headless = false, mute = false;
    int frames = 0;
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
                 "Use --command help for the shared command list.");
            return 0;
        } else if (!strcmp(argv[i], "--headless")) headless = true;
        else if (!strcmp(argv[i], "--mute")) mute = true;
        else if (!strcmp(argv[i], "--script") && i + 1 < argc && !script) script = argv[++i];
        else if (!strcmp(argv[i], "--command") && i + 1 < argc && !commands) commands = argv[++i];
        else if (!strcmp(argv[i], "--capture") && i + 1 < argc && !capture) capture = argv[++i];
        else if (!strcmp(argv[i], "--frames") && i + 1 < argc && !frames) {
            char *end;
            errno = 0;
            long value = strtol(argv[++i], &end, 10);
            if (errno || *end || value < 1 || value > 36000) { fputs("Invalid --frames value\n", stderr); return 2; }
            frames = (int)value;
        } else { fprintf(stderr, "Unknown or incomplete argument: %s\n", argv[i]); return 2; }
    }
    if ((headless && (frames || capture)) || (capture && !frames)) {
        fputs("--frames/--capture require a graphical run; capture also requires --frames\n", stderr);
        return 2;
    }
    game_reset(&game, 1);
    if (script && !console_script(&console, &game, script)) return 1;
    if (commands && !console.quit && !console_chain(&console, &game, commands)) return 1;
    if (headless || console.quit) {
        console_execute(&console, &game, "state");
        return 0;
    }
#ifndef GALAXY_HEADLESS
    return app_run(&game, &console, frames, capture, mute);
#else
    (void)mute;
    return 0;
#endif
}
