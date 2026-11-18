#include "input.h"
#include <math.h>

void input_devices_init(PlayerDevices *devices) {
    *devices = (PlayerDevices){.device = {-1,-1}, .selected_device = INPUT_KEYBOARD, .prompt_device = INPUT_KEYBOARD};
}

int input_owner(const PlayerDevices *devices, int device) {
    if (device < 0) return -1;
    for (int i = 0; i < GAME_PLAYERS; ++i) if (devices->device[i] == device) return i;
    return -1;
}

static int other_pilot(const Game *game, const PlayerDevices *devices, int slot) {
    int other = 1-slot;
    if (devices->choosing[other]) return devices->pilot[other];
    return game->players[other].initialized ? game->players[other].pilot : -1;
}

bool input_assign(Game *game, PlayerDevices *devices, int device, int slot) {
    if (device < 0 || device > INPUT_KEYBOARD || slot < -1 || slot >= GAME_PLAYERS) return false;
    int old = input_owner(devices,device);
    if (old == slot) return true;
    bool choosing = false;
    if (slot >= 0 && !game->players[slot].joined) {
        MenuPage page = game->menu.page;
        if (page == MENU_CONTROLLERS) page = game->menu.controllers_origin;
        choosing = !game->players[slot].initialized && (page == MENU_NONE || page == MENU_SHOP || page == MENU_PAUSE);
        if (choosing && game->over) return false;
        if (!choosing && !game_join(game,slot)) return false;
    }
    if (old >= 0) {
        devices->device[old] = -1;
        devices->choosing[old] = false;
        if (game->players[old].joined) game_leave(game,old);
    }
    if (slot >= 0) {
        devices->device[slot] = device;
        devices->choosing[slot] = choosing;
        if (choosing && game->menu.page == MENU_PAUSE) game_menu_open(game,MENU_CONTROLLERS);
        devices->pilot[slot] = game->players[slot].initialized ? game->players[slot].pilot : slot;
        if (devices->pilot[slot] == other_pilot(game,devices,slot))
            devices->pilot[slot] = (devices->pilot[slot]+1)%3;
        game->players[slot].input = (PlayerInput){0};
    }
    return true;
}

void input_pick_pilot(Game *game, PlayerDevices *devices, int slot, MenuAction action) {
    if (!devices->choosing[slot]) return;
    int other = other_pilot(game,devices,slot);
    int pilot = devices->pilot[slot];
    if (action == MENU_BACK) { input_drop(game,devices,devices->device[slot]); return; }
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
        if (devices->device[slot] < 0 && input_assign(game,devices,device,slot)) return slot;
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
    result.fire = (pad->buttons & PAD_CONFIRM) || pad->trigger > .2f;
    result.focus = (pad->buttons & (PAD_FOCUS | PAD_BACK)) != 0;
    result.sidekick_left = result.sidekick_right = (pad->buttons & PAD_SIDEKICK) != 0;
    return result;
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
