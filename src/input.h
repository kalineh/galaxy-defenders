#ifndef GALAXY_INPUT_H
#define GALAXY_INPUT_H
#include "game.h"
#include <stddef.h>

#define INPUT_PADS 6
#define INPUT_KEYBOARD 6
#define INPUT_KEYBOARD_P2 7
enum { PAD_UP=1u<<0, PAD_RIGHT=1u<<1, PAD_DOWN=1u<<2, PAD_LEFT=1u<<3,
    PAD_CONFIRM=1u<<4, PAD_BACK=1u<<5, PAD_RETRY=1u<<6, PAD_FOCUS=1u<<7,
    PAD_SIDEKICK=1u<<8, PAD_START=1u<<9, PAD_SELECT=1u<<10 };
typedef struct {
    bool connected;
    uint32_t buttons, pressed;
    float x, y, trigger;
} PadInput;
typedef struct { int device[GAME_PLAYERS]; } PlayerDevices;

void input_devices_init(PlayerDevices *devices);
int input_owner(const PlayerDevices *devices, int device);
int input_claim(Game *game, PlayerDevices *devices, int device);
bool input_drop(Game *game, PlayerDevices *devices, int device);
PlayerInput input_player(const PadInput *pad);
bool input_ds4_report(PadInput *pad, const unsigned char *report, size_t size);
bool ds4_init(void);
void ds4_update(PadInput pads[2]);
void ds4_shutdown(void);
#endif
