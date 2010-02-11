//
// Input.cs
//

using Microsoft.Xna.Framework.Input;

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
    }
}

