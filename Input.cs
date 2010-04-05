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
        private GamePadState[] PreviousFrameGamePadState { get; set; }

        public CInput(CGalaxy game)
        {
            Game = game;
            PreviousFrameGamePadState = new GamePadState[2];
            PreviousFrameGamePadState[(int)PlayerIndex.One] = GamePad.GetState(PlayerIndex.One);
            PreviousFrameGamePadState[(int)PlayerIndex.Two] = GamePad.GetState(PlayerIndex.Two);
        }

        public void Update()
        {
            PreviousFrameGamePadState[(int)PlayerIndex.One] = GamePad.GetState(PlayerIndex.One);
            PreviousFrameGamePadState[(int)PlayerIndex.Two] = GamePad.GetState(PlayerIndex.Two);
        }

        public bool IsKeyDown(Keys query)
        {
            return IsRawKeyDown(query);
        }

        public bool IsKeyPressed(Keys query)
        {
            return IsRawKeyPressed(query);
        }

        public bool IsPadLeftDown(PlayerIndex player_index)
        {
            return IsPadLeftDownImpl(GamePad.GetState(player_index));
        }

        public bool IsPadRightDown(PlayerIndex player_index)
        {
            return IsPadRightDownImpl(GamePad.GetState(player_index));
        }

        public bool IsPadUpDown(PlayerIndex player_index)
        {
            return IsPadUpDownImpl(GamePad.GetState(player_index));
        }

        public bool IsPadDownDown(PlayerIndex player_index)
        {
            return IsPadDownDownImpl(GamePad.GetState(player_index));
        }

        public bool IsPadLeftPressed(PlayerIndex player_index)
        {
            bool is_down = IsPadLeftDownImpl(GamePad.GetState(player_index));
            bool was_down = IsPadLeftDownImpl(PreviousFrameGamePadState[(int)player_index]);
            return is_down && !was_down;
        }

        public bool IsPadRightPressed(PlayerIndex player_index)
        {
            bool is_down = IsPadRightDownImpl(GamePad.GetState(player_index));
            bool was_down = IsPadRightDownImpl(PreviousFrameGamePadState[(int)player_index]);
            return is_down && !was_down;
        }

        public bool IsPadUpPressed(PlayerIndex player_index)
        {
            bool is_down = IsPadUpDownImpl(GamePad.GetState(player_index));
            bool was_down = IsPadUpDownImpl(PreviousFrameGamePadState[(int)player_index]);
            return is_down && !was_down;
        }

        public bool IsPadDownPressed(PlayerIndex player_index)
        {
            bool is_down = IsPadDownDownImpl(GamePad.GetState(player_index));
            bool was_down = IsPadDownDownImpl(PreviousFrameGamePadState[(int)player_index]);
            return is_down && !was_down;
        }

        public bool IsPadConfirmPressed(PlayerIndex player_index)
        {
            bool is_down = GamePad.GetState(player_index).Buttons.B == ButtonState.Pressed;
            bool was_down = PreviousFrameGamePadState[(int)player_index].Buttons.B == ButtonState.Pressed;
            return is_down && !was_down;
        }

        public bool IsPadCancelPressed(PlayerIndex player_index)
        {
            bool is_down = GamePad.GetState(player_index).Buttons.A == ButtonState.Pressed;
            bool was_down = PreviousFrameGamePadState[(int)player_index].Buttons.A == ButtonState.Pressed;
            return is_down && !was_down;
        }

        public bool IsPadStartPressed(PlayerIndex player_index)
        {
            bool is_down = IsPadStartDownImpl(GamePad.GetState(player_index));
            bool was_down = IsPadStartDownImpl(PreviousFrameGamePadState[(int)player_index]);
            return is_down && !was_down;
        }

        public bool IsPadBackPressed(PlayerIndex player_index)
        {
            bool is_down = IsPadBackDownImpl(GamePad.GetState(player_index));
            bool was_down = IsPadBackDownImpl(PreviousFrameGamePadState[(int)player_index]);
            return is_down && !was_down;
        }

        private bool IsPadStartDownImpl(GamePadState state)
        {
            bool down = state.Buttons.Start == ButtonState.Pressed;
            return down;
        }

        private bool IsPadBackDownImpl(GamePadState state)
        {
            bool down = state.Buttons.Back == ButtonState.Pressed;
            return down;
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
#if XBOX360
            return false;
#endif
            int keycode = (int)key;
            int keystate = GetAsyncKeyState(keycode);
            int is_down = keystate & 0x8000;
            return is_down != 0;
        }

        public static bool IsRawKeyPressed(Keys key)
        {
#if XBOX360
            return false;
#endif

            int keycode = (int)key;
            int keystate = GetAsyncKeyState(keycode);
            int is_down = keystate & 0x8000;
            int is_pressed = keystate & 0x0001;
            return is_down != 0 && is_pressed != 0;
        }
    }
}

