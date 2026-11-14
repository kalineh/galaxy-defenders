#ifndef GALAXY_CONSOLE_H
#define GALAXY_CONSOLE_H

#include "game.h"
#define CONSOLE_LINE 512
#define CONSOLE_HISTORY 12
#define CONSOLE_STEP_LIMIT 72000

typedef struct {
    char lines[CONSOLE_HISTORY][180];
    int count, steps;
    bool quit;
} Console;

void console_log(Console *console, const char *format, ...);
bool console_execute(Console *console, Game *game, const char *line);
bool console_chain(Console *console, Game *game, const char *commands);
bool console_script(Console *console, Game *game, const char *path);

#endif
