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

        public CInput(CGalaxy game)
        {
            Game = game;
        }

        public void Update()
        {
        }

        public bool IsKeyDown(Keys query)
        {
            return IsRawKeyDown(query);
        }

        public bool IsKeyPressed(Keys query)
        {
            return IsRawKeyPressed(query);
        }

        //public  

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

