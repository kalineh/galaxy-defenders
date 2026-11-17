#include "input.h"
#include <string.h>
#if defined(_WIN32) && !defined(GALAXY_MEMORY)
#define WIN32_LEAN_AND_MEAN
#include <windows.h>

static HWND message_window;
static struct { HANDLE handle; PadInput input; ULONGLONG last_report; } devices[2];
static const wchar_t *window_class = L"GalaxyDefendersDS4";

static int find_device(HANDLE handle) {
    for (int i = 0; i < 2; ++i) if (devices[i].handle == handle) return i;
    RID_DEVICE_INFO info = {.cbSize = sizeof(info)};
    UINT size = sizeof(info);
    if (GetRawInputDeviceInfoW(handle, RIDI_DEVICEINFO, &info, &size) == (UINT)-1 ||
        info.dwType != RIM_TYPEHID || info.hid.dwVendorId != 0x054c ||
        (info.hid.dwProductId != 0x05c4 && info.hid.dwProductId != 0x09cc && info.hid.dwProductId != 0x0ba0)) return -1;
    for (int i = 0; i < 2; ++i) if (!devices[i].handle) { devices[i].handle = handle; return i; }
    return -1;
}

static LRESULT CALLBACK input_window(HWND window, UINT message, WPARAM wparam, LPARAM lparam) {
    if (message == WM_INPUT_DEVICE_CHANGE && wparam == GIDC_REMOVAL) {
        for (int i = 0; i < 2; ++i) if (devices[i].handle == (HANDLE)lparam) memset(&devices[i], 0, sizeof(devices[i]));
    } else if (message == WM_INPUT) {
        /* Bluetooth collections can report 547-byte packets. Keep reads bounded and aligned. */
        union { RAWINPUT aligned; unsigned char bytes[8192]; } buffer;
        UINT size = sizeof(buffer);
        UINT count = GetRawInputData((HRAWINPUT)lparam, RID_INPUT, &buffer, &size, sizeof(RAWINPUTHEADER));
        if (count != (UINT)-1 && count >= offsetof(RAWINPUT, data.hid.bRawData)) {
            RAWINPUT *raw = &buffer.aligned;
            if (raw->header.dwType == RIM_TYPEHID) {
                int slot = find_device(raw->header.hDevice);
                RAWHID *hid = &raw->data.hid;
                size_t available = count - offsetof(RAWINPUT, data.hid.bRawData);
                if (slot >= 0 && hid->dwSizeHid && hid->dwCount <= available / hid->dwSizeHid) {
                    for (DWORD i = 0; i < hid->dwCount; ++i)
                        if (input_ds4_report(&devices[slot].input, hid->bRawData + i * hid->dwSizeHid, hid->dwSizeHid))
                            devices[slot].last_report = GetTickCount64();
                }
            }
        }
    }
    return DefWindowProcW(window, message, wparam, lparam);
}

bool ds4_init(void) {
    WNDCLASSW wc = {.lpfnWndProc = input_window, .hInstance = GetModuleHandleW(NULL), .lpszClassName = window_class};
    if (!RegisterClassW(&wc) && GetLastError() != ERROR_CLASS_ALREADY_EXISTS) return false;
    message_window = CreateWindowExW(0, window_class, L"", 0, 0, 0, 0, 0, HWND_MESSAGE, NULL, wc.hInstance, NULL);
    if (!message_window) return false;
    RAWINPUTDEVICE registrations[] = {
        {1, 4, RIDEV_INPUTSINK | RIDEV_DEVNOTIFY, message_window},
        {1, 5, RIDEV_INPUTSINK | RIDEV_DEVNOTIFY, message_window}
    };
    if (!RegisterRawInputDevices(registrations, 2, sizeof(RAWINPUTDEVICE))) { ds4_shutdown(); return false; }
    return true;
}

void ds4_update(PadInput pads[2]) {
    if (message_window) {
        MSG message;
        while (PeekMessageW(&message, message_window, 0, 0, PM_REMOVE)) DispatchMessageW(&message);
    }
    for (int i = 0; i < 2; ++i) {
        if (devices[i].input.connected && GetTickCount64() - devices[i].last_report > 500)
            devices[i].input = (PadInput){0};
        pads[i] = devices[i].input;
        devices[i].input.pressed = 0;
    }
}

void ds4_shutdown(void) {
    if (message_window) {
        RAWINPUTDEVICE registrations[] = {{1, 4, RIDEV_REMOVE, NULL}, {1, 5, RIDEV_REMOVE, NULL}};
        RegisterRawInputDevices(registrations, 2, sizeof(RAWINPUTDEVICE));
        DestroyWindow(message_window);
        message_window = NULL;
    }
    UnregisterClassW(window_class, GetModuleHandleW(NULL));
    memset(devices, 0, sizeof(devices));
}
#else
bool ds4_init(void) { return false; }
void ds4_update(PadInput pads[2]) { memset(pads, 0, sizeof(PadInput) * 2); }
void ds4_shutdown(void) {}
#endif
