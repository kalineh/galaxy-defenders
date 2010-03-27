//
// Input.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Runtime.InteropServices;

namespace Galaxy
{
    public class CInput
    {
        private CGalaxy Game { get; set; }
        private GamePadState PreviousFrameGamePadState { get; set; }

        public CInput(CGalaxy game)
        {
            Game = game;
            PreviousFrameGamePadState = GamePad.GetState(PlayerIndex.One);
        }

        public void Update()
        {
            PreviousFrameGamePadState = GamePad.GetState(PlayerIndex.One);
        }

        public bool IsKeyDown(Keys query)
        {
            return IsRawKeyDown(query);
        }

        public bool IsKeyPressed(Keys query)
        {
            return IsRawKeyPressed(query);
        }

        public bool IsPadLeftDown()
        {
            return IsPadLeftDownImpl(GamePad.GetState(PlayerIndex.One));
        }

        public bool IsPadRightDown()
        {
            return IsPadRightDownImpl(GamePad.GetState(PlayerIndex.One));
        }

        public bool IsPadUpDown()
        {
            return IsPadUpDownImpl(GamePad.GetState(PlayerIndex.One));
        }

        public bool IsPadDownDown()
        {
            return IsPadDownDownImpl(GamePad.GetState(PlayerIndex.One));
        }

        public bool IsPadLeftPressed()
        {
            bool is_down = IsPadLeftDownImpl(GamePad.GetState(PlayerIndex.One));
            bool was_down = IsPadLeftDownImpl(PreviousFrameGamePadState);
            return is_down && !was_down;
        }

        public bool IsPadRightPressed()
        {
            bool is_down = IsPadRightDownImpl(GamePad.GetState(PlayerIndex.One));
            bool was_down = IsPadRightDownImpl(PreviousFrameGamePadState);
            return is_down && !was_down;
        }

        public bool IsPadUpPressed()
        {
            bool is_down = IsPadUpDownImpl(GamePad.GetState(PlayerIndex.One));
            bool was_down = IsPadUpDownImpl(PreviousFrameGamePadState);
            return is_down && !was_down;
        }

        public bool IsPadDownPressed()
        {
            bool is_down = IsPadDownDownImpl(GamePad.GetState(PlayerIndex.One));
            bool was_down = IsPadDownDownImpl(PreviousFrameGamePadState);
            return is_down && !was_down;
        }

        public bool IsPadConfirmPressed()
        {
            bool is_down = GamePad.GetState(PlayerIndex.One).Buttons.B == ButtonState.Pressed;
            bool was_down = PreviousFrameGamePadState.Buttons.B == ButtonState.Pressed;
            return is_down && !was_down;
        }

        public bool IsPadCancelPressed()
        {
            bool is_down = GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed;
            bool was_down = PreviousFrameGamePadState.Buttons.A == ButtonState.Pressed;
            return is_down && !was_down;
        }

        private bool IsPadLeftDownImpl(GamePadState state)
        {
            bool dpad = state.DPad.Left == ButtonState.Pressed;
            bool stick = state.ThumbSticks.Left.X < 0.0f;
            return dpad || stick;
        }

        private bool IsPadRightDownImpl(GamePadState state)
        {
            bool dpad = state.DPad.Right == ButtonState.Pressed;
            bool stick = state.ThumbSticks.Left.X > 0.0f;
            return dpad || stick;
        }

        private bool IsPadUpDownImpl(GamePadState state)
        {
            bool dpad = state.DPad.Up == ButtonState.Pressed;
            bool stick = state.ThumbSticks.Left.Y > 0.0f;
            return dpad || stick;
        }

        private bool IsPadDownDownImpl(GamePadState state)
        {
            bool dpad = state.DPad.Down == ButtonState.Pressed;
            bool stick = state.ThumbSticks.Left.Y < 0.0f;
            return dpad || stick;
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

