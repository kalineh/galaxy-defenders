#include "input.h"
#include <math.h>

void input_devices_init(PlayerDevices *devices) {
    for (int i = 0; i < GAME_PLAYERS; ++i) devices->device[i] = -1;
}

int input_owner(const PlayerDevices *devices, int device) {
    if (device < 0) return -1;
    for (int i = 0; i < GAME_PLAYERS; ++i) if (devices->device[i] == device) return i;
    return -1;
}

int input_claim(Game *game, PlayerDevices *devices, int device) {
    if (device < 0 || device > INPUT_KEYBOARD_P2) return -1;
    int owner = input_owner(devices, device);
    if (owner >= 0) return owner;
    for (int slot = 0; slot < GAME_PLAYERS; ++slot) {
        if (device == INPUT_KEYBOARD && slot != 0) continue;
        if (device == INPUT_KEYBOARD_P2 && slot != 1) continue;
        if (devices->device[slot] != -1) continue;
        if (!game->players[slot].joined && !game_join(game, slot)) return -1;
        devices->device[slot] = device;
        return slot;
    }
    return -1;
}

bool input_drop(Game *game, PlayerDevices *devices, int device) {
    int slot = input_owner(devices, device);
    if (slot < 0) return false;
    devices->device[slot] = -1;
    return game_leave(game, slot);
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
