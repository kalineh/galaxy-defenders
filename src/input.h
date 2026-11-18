#ifndef GALAXY_INPUT_H
#define GALAXY_INPUT_H
#include "game.h"
#include <stddef.h>

#define INPUT_PADS 6
#define INPUT_KEYBOARD INPUT_PADS
#define INPUT_DEVICES (INPUT_PADS + 1)
#define INPUT_FIRE_THRESHOLD .2f
enum { PAD_UP=1u<<0, PAD_RIGHT=1u<<1, PAD_DOWN=1u<<2, PAD_LEFT=1u<<3,
    PAD_CONFIRM=1u<<4, PAD_BACK=1u<<5, PAD_RETRY=1u<<6, PAD_FOCUS=1u<<7,
    PAD_SIDEKICK=1u<<8, PAD_START=1u<<9, PAD_SELECT=1u<<10, PAD_READY=1u<<11, PAD_FIRE_TRIGGER=1u<<12 };
typedef struct {
    bool connected;
    uint32_t buttons, pressed;
    float x, y, trigger;
} PadInput;
typedef struct {
    int owner[INPUT_DEVICES], pad_number[INPUT_PADS];
    bool manual[INPUT_DEVICES];
    int pilot[GAME_PLAYERS], player_prompt[GAME_PLAYERS];
    int flight_device[GAME_PLAYERS];
    bool choosing[GAME_PLAYERS], has_device[GAME_PLAYERS];
    int selected_device, prompt_device;
} PlayerDevices;
typedef struct { uint32_t shared, player[GAME_PLAYERS]; } MenuButtons;

void input_devices_init(PlayerDevices *devices);
int input_owner(const PlayerDevices *devices, int device);
void input_update_devices(Game *game, PlayerDevices *devices, const PadInput inputs[INPUT_DEVICES]);
bool input_assign(Game *game, PlayerDevices *devices, int device, int slot);
void input_pick_pilot(Game *game, PlayerDevices *devices, int slot, MenuAction action);
int input_claim(Game *game, PlayerDevices *devices, int device);
bool input_drop(Game *game, PlayerDevices *devices, int device);
PlayerInput input_player(const PadInput *pad);
PlayerInput input_slot_player(const PlayerDevices *devices, const PadInput inputs[INPUT_DEVICES], int slot);
MenuButtons input_menu_buttons(PlayerDevices *devices, const PadInput inputs[INPUT_DEVICES], int lead);
bool input_ds4_report(PadInput *pad, const unsigned char *report, size_t size);
bool ds4_init(void);
void ds4_update(PadInput pads[2]);
void ds4_shutdown(void);
#endif
