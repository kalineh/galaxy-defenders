//
// Input.cs
//

using System;
using Microsoft.Xna.Framework.Input;

namespace Galaxy
{
    public class CInput
    {
        private CGalaxy Game { get; set; }
        private Keys[] KeysCurrent { get; set; }
        private Keys[] KeysPrevious { get; set; }

        public CInput(CGalaxy game)
        {
            Game = game;
            KeysCurrent = Keyboard.GetState().GetPressedKeys();
            KeysPrevious = Keyboard.GetState().GetPressedKeys();
        }

        public void Update()
        {
            KeysPrevious = KeysCurrent;
            KeysCurrent = Keyboard.GetState().GetPressedKeys();
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
            return !was && _is;
        }
    }
}

