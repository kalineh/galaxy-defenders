#ifndef GALAXY_CONSOLE_H
#define GALAXY_CONSOLE_H

#include "game.h"
#define CONSOLE_LINE 512
#define CONSOLE_HISTORY 256
#define CONSOLE_COMMAND_HISTORY 32
#define CONSOLE_STEP_LIMIT 72000

typedef struct {
    char lines[CONSOLE_HISTORY][CONSOLE_LINE];
    char input[CONSOLE_LINE], draft[CONSOLE_LINE], completion[CONSOLE_LINE];
    char commands[CONSOLE_COMMAND_HISTORY][CONSOLE_LINE];
    int count, steps, cursor, command_count, history_offset, scroll;
    bool quit, perf;
    int music_track;
    unsigned music_revision;
    float music_progress;
} Console;

void console_log(Console *console, const char *format, ...);
bool console_execute(Console *console, Game *game, const char *line);
bool console_chain(Console *console, Game *game, const char *commands);
bool console_script(Console *console, Game *game, const char *path);
void console_insert(Console *console, const char *text);
void console_erase(Console *console, bool backward);
void console_history(Console *console, int direction);
int console_complete(Console *console);
bool console_submit(Console *console, Game *game);

#endif
