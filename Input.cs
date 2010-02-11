//
// Input.cs
//

using Microsoft.Xna.Framework.Input;
using System.Runtime.InteropServices;

namespace Galaxy
{
    public class CInput
    {
        private CGalaxy Game { get; set; }
        private Keys[] KeysCurrent { get; set; }
        private Keys[] KeysPrevious { get; set; }
        public Keys[] KeysOverride { get; set; }

        public CInput(CGalaxy game)
        {
            Game = game;
            KeysCurrent = Keyboard.GetState().GetPressedKeys();
            KeysPrevious = Keyboard.GetState().GetPressedKeys();
            KeysOverride = new Keys[0];
        }

        public void Update()
        {
            KeysPrevious = KeysCurrent;
            KeysCurrent = Keyboard.GetState().GetPressedKeys();
        }

        public bool IsKeyDown(Keys query)
        {
            foreach (Keys key in KeysCurrent)
            {
                if (key == query)
                {
                    return true;
                }
            }
            foreach (Keys key in KeysOverride)
            {
                if (key == query)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsKeyPressed(Keys query)
        {
            bool was = false;
            bool _is = false;
            foreach (Keys key in KeysPrevious)
            {
                if (key == query)
                {
                    was = true;
                    break;
                }
            }
            foreach (Keys key in KeysCurrent)
            {
                if (key == query)
                {
                    _is = true;
                    break;
                }
            }
            foreach (Keys key in KeysOverride)
            {
                if (key == query)
                {
                    _is = true;
                    break;
                }
            }
            return !was && _is;
        }

        /// <summary>
        /// Raw input system to work around various input not being consistent through xna+winforms.
        /// </summary>
        [DllImport("user32.dll")]
        static extern int GetAsyncKeyState(int nVirtKey);

        public static bool IsRawKeyDown(Keys key)
        {
            int keycode = (int)key;
            int keystate = GetAsyncKeyState(keycode);
            int is_down = keystate & 0x8000;
            return is_down != 0;
        }

        public static bool IsRawKeyPressed(Keys key)
        {
            int keycode = (int)key;
            int keystate = GetAsyncKeyState(keycode);
            int is_down = keystate & 0x8000;
            int is_pressed = keystate & 0x0001;
            return is_down != 0 && is_pressed != 0;
        }
    }
}

