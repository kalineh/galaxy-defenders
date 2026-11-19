#include "input.h"
#include <math.h>

void input_devices_init(PlayerDevices *devices) {
    *devices = (PlayerDevices){.selected_device = INPUT_KEYBOARD, .prompt_device = INPUT_KEYBOARD,
        .player_prompt = {INPUT_KEYBOARD, INPUT_KEYBOARD}, .flight_device = {INPUT_KEYBOARD, INPUT_KEYBOARD},
        .has_device = {true, false}};
    for (int i = 0; i < INPUT_PADS; ++i) devices->owner[i] = -1;
    devices->owner[INPUT_KEYBOARD] = 0;
}

int input_owner(const PlayerDevices *devices, int device) {
    if (device < 0 || device >= INPUT_DEVICES || (device < INPUT_PADS && !devices->pad_number[device])) return -1;
    return devices->owner[device];
}

static int other_pilot(const Game *game, const PlayerDevices *devices, int slot) {
    int other = 1-slot;
    if (devices->choosing[other]) return devices->pilot[other];
    return game->players[other].initialized ? game->players[other].pilot : -1;
}

static int first_device(const PlayerDevices *devices, int slot) {
    if (input_owner(devices, INPUT_KEYBOARD) == slot) return INPUT_KEYBOARD;
    for (int i = 0; i < INPUT_PADS; ++i) if (input_owner(devices, i) == slot) return i;
    return -1;
}

static void sync_players(Game *game, PlayerDevices *devices) {
    MenuPage page = game->menu.page == MENU_CONTROLLERS ? game->menu.controllers_origin : game->menu.page;
    bool setup = page == MENU_MAIN || page == MENU_SLOTS || page == MENU_DELETE || page == MENU_DIFFICULTY ||
        page == MENU_PILOT || ((page == MENU_OPTIONS || page == MENU_QUIT) && game->menu.parent == MENU_MAIN);
    bool lost_last[GAME_PLAYERS] = {false};
    for (int slot = 0; slot < GAME_PLAYERS; ++slot) {
        int device = first_device(devices, slot);
        bool previously_assigned = devices->has_device[slot];
        devices->has_device[slot] = device >= 0;
        lost_last[slot] = previously_assigned && device < 0;
        if (device < 0) {
            devices->choosing[slot] = false;
            if (lost_last[slot] && game->ai.mode[slot]) game->ai.joined[slot] = true;
            continue;
        }
        if (game->ai.mode[slot]) game->ai.joined[slot] = false;
        if (input_owner(devices,devices->flight_device[slot]) != slot) devices->flight_device[slot] = device;
        int prompt = devices->player_prompt[slot];
        int owner = input_owner(devices,prompt);
        if ((prompt < INPUT_PADS && !devices->pad_number[prompt]) || (owner >= 0 && owner != slot))
            devices->player_prompt[slot] = device;
        bool fresh = !game->players[slot].initialized;
        bool choose = fresh && !game->over && (page == MENU_NONE || page == MENU_PAUSE || page == MENU_SHOP);
        if (choose && !devices->choosing[slot]) {
            devices->pilot[slot] = slot == other_pilot(game, devices, slot) ? (slot+1)%3 : slot;
            if (game->menu.page == MENU_PAUSE) game_menu_open(game, MENU_CONTROLLERS);
        }
        devices->choosing[slot] = choose;
        if (!game->players[slot].joined && (fresh ? setup : !previously_assigned))
            game_join(game, slot);
    }
    /* Join destinations first so moving the last device doesn't briefly empty the party. */
    for (int slot = 0; slot < GAME_PLAYERS; ++slot)
        if (lost_last[slot] && !game->ai.mode[slot]) game_leave(game, slot);
}

void input_update_devices(Game *game, PlayerDevices *devices, const PadInput inputs[INPUT_DEVICES]) {
    bool used[INPUT_PADS+1] = {false};
    for (int i = 0; i < INPUT_PADS; ++i) {
        if (!inputs[i].connected) devices->pad_number[i] = 0;
        used[devices->pad_number[i]] = true;
    }
    for (int i = 0; i < INPUT_PADS; ++i) {
        if (!inputs[i].connected || devices->pad_number[i]) continue;
        int number = 1;
        while (used[number]) ++number;
        used[number] = true;
        devices->pad_number[i] = number;
        if (!devices->manual[i]) devices->owner[i] = number <= GAME_PLAYERS ? number-1 : -1;
    }
    if (devices->prompt_device < INPUT_PADS && !inputs[devices->prompt_device].connected)
        devices->prompt_device = INPUT_KEYBOARD;
    sync_players(game, devices);
}

bool input_assign(Game *game, PlayerDevices *devices, int device, int slot) {
    if (device < 0 || device >= INPUT_DEVICES || slot < -1 || slot >= GAME_PLAYERS) return false;
    if (device < INPUT_PADS && !devices->pad_number[device]) return false;
    devices->owner[device] = slot;
    devices->manual[device] = true;
    sync_players(game, devices);
    return true;
}

void input_pick_pilot(Game *game, PlayerDevices *devices, int slot, MenuAction action) {
    if (!devices->choosing[slot]) return;
    int other = other_pilot(game,devices,slot);
    int pilot = devices->pilot[slot];
    if (action == MENU_BACK) {
        for (int i = 0; i < INPUT_DEVICES; ++i)
            if (input_owner(devices, i) == slot) input_drop(game, devices, i);
        return;
    }
    int step = action == MENU_LEFT ? -1 : action == MENU_RIGHT ? 1 : 0;
    pilot = (pilot + step + 3)%3;
    if (pilot == other) pilot = (pilot + (step < 0 ? 2 : 1))%3;
    devices->pilot[slot] = pilot;
    bool fresh = !game->players[slot].initialized;
    if (action == MENU_CONFIRM && game_join(game,slot)) {
        game->players[slot].pilot = game->menu.pilot[slot] = pilot;
        if (fresh) {
            game_starting_loadout(game,slot);
            MenuPage page = game->menu.page;
            if (page == MENU_CONTROLLERS) page = game->menu.controllers_origin;
            if (page == MENU_SHOP || (page == MENU_PAUSE && game->menu.pause_origin == MENU_SHOP))
                game_checkpoint(game,true);
        }
        devices->choosing[slot] = false;
    }
}

int input_claim(Game *game, PlayerDevices *devices, int device) {
    int owner = input_owner(devices,device);
    if (owner >= 0) return owner;
    for (int slot = 0; slot < GAME_PLAYERS; ++slot)
        if (first_device(devices,slot) < 0 && input_assign(game,devices,device,slot)) return slot;
    return -1;
}

bool input_drop(Game *game, PlayerDevices *devices, int device) {
    return input_owner(devices,device) >= 0 && input_assign(game,devices,device,-1);
}

PlayerInput input_player(const PadInput *pad) {
    PlayerInput result = {0};
    if (!pad->connected) return result;
    float length = sqrtf(pad->x * pad->x + pad->y * pad->y);
    if (length > .18f) {
        float scale = fminf(1, (length - .18f) / .82f) / length;
        result.x = pad->x * scale; result.y = pad->y * scale;
    }
    result.x += !!(pad->buttons & PAD_RIGHT) - !!(pad->buttons & PAD_LEFT);
    result.y += !!(pad->buttons & PAD_DOWN) - !!(pad->buttons & PAD_UP);
    length = sqrtf(result.x * result.x + result.y * result.y);
    if (length > 1) { result.x /= length; result.y /= length; }
    result.fire = (pad->buttons & PAD_CONFIRM) || pad->trigger > INPUT_FIRE_THRESHOLD;
    result.focus = (pad->buttons & (PAD_FOCUS | PAD_BACK)) != 0;
    result.sidekick_left = result.sidekick_right = (pad->buttons & PAD_SIDEKICK) != 0;
    return result;
}

PlayerInput input_slot_player(const PlayerDevices *devices, const PadInput inputs[INPUT_DEVICES], int slot) {
    PlayerInput result = {0};
    float left = 0, right = 0, up = 0, down = 0;
    for (int i = 0; i < INPUT_DEVICES; ++i) {
        if (input_owner(devices,i) != slot) continue;
        PlayerInput p = input_player(&inputs[i]);
        left = fminf(left,p.x); right = fmaxf(right,p.x);
        up = fminf(up,p.y); down = fmaxf(down,p.y);
        result.fire |= p.fire;
        result.focus |= p.focus;
        result.sidekick_left |= p.sidekick_left;
        result.sidekick_right |= p.sidekick_right;
    }
    result.x = left+right; result.y = up+down;
    float length = hypotf(result.x,result.y);
    if (length > 1) { result.x /= length; result.y /= length; }
    return result;
}

MenuButtons input_menu_buttons(PlayerDevices *devices, const PadInput inputs[INPUT_DEVICES], int lead) {
    MenuButtons buttons = {0};
    for (int i = 0; i < INPUT_DEVICES; ++i) {
        if (!inputs[i].connected || !inputs[i].pressed) continue;
        int slot = input_owner(devices,i);
        if (slot >= 0) devices->flight_device[slot] = i;
        uint32_t menu_pressed = inputs[i].pressed & ~PAD_FIRE_TRIGGER;
        if (!menu_pressed) continue;
        if (slot < 0) slot = lead;
        buttons.shared |= menu_pressed;
        buttons.player[slot] |= menu_pressed;
        devices->prompt_device = devices->player_prompt[slot] = i;
    }
    return buttons;
}

/* Report offsets and supported transports follow Solcraft's native DS4 reader.
   No dependency on the sibling repository or controller emulation software. */
bool input_ds4_report(PadInput *pad, const unsigned char *report, size_t size) {
    if (!pad || !report || size == 0) return false;
    size_t offset;
    if (report[0] == 1 && size >= 10) offset = 1;
    else if (report[0] == 0x11 && size >= 12) offset = 3;
    else return false;
    unsigned face = report[offset + 4], shoulders = report[offset + 5], hat = face & 15;
    uint32_t buttons = 0;
    if (hat == 0 || hat == 1 || hat == 7) buttons |= PAD_UP;
    if (hat == 1 || hat == 2 || hat == 3) buttons |= PAD_RIGHT;
    if (hat == 3 || hat == 4 || hat == 5) buttons |= PAD_DOWN;
    if (hat == 5 || hat == 6 || hat == 7) buttons |= PAD_LEFT;
    if (face & 0x10) buttons |= PAD_READY;
    if (face & 0x20) buttons |= PAD_CONFIRM;
    if (face & 0x40) buttons |= PAD_BACK;
    if (face & 0x80) buttons |= PAD_RETRY;
    if (shoulders & 1) buttons |= PAD_FOCUS;
    if (shoulders & 2) buttons |= PAD_SIDEKICK;
    if (shoulders & 0x10) buttons |= PAD_SELECT;
    if (shoulders & 0x20) buttons |= PAD_START;
    pad->pressed |= buttons & ~pad->buttons;
    pad->buttons = buttons;
    pad->x = report[offset] / 127.5f - 1;
    pad->y = report[offset + 1] / 127.5f - 1;
    pad->trigger = report[offset + 8] / 255.0f;
    pad->connected = true;
    return true;
}
