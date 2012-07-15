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
        public const float DeadZone = 0.025f;

        private CGalaxy Game { get; set; }
        private bool[] ConnectedPlayerIndex { get; set; }
        private PlayerIndex[] GameControllerIndexToPlayerIndex { get; set; }
        private GamePadState[] CurrentFrameGamePadState { get; set; }
        private GamePadState[] PreviousFrameGamePadState { get; set; }
        private GameControllerIndex KeyboardControllerIndex;

        public CInput(CGalaxy game)
        {
            Game = game;
            ConnectedPlayerIndex = new bool[4] { false, false, false, false };
            GameControllerIndexToPlayerIndex = new PlayerIndex[2] { (PlayerIndex)(-1), (PlayerIndex)(-1) };
            CurrentFrameGamePadState = new GamePadState[2];
            CurrentFrameGamePadState[(int)GameControllerIndex.One] = GamePad.GetState(PlayerIndex.One);
            CurrentFrameGamePadState[(int)GameControllerIndex.Two] = GamePad.GetState(PlayerIndex.Two);
            PreviousFrameGamePadState = new GamePadState[2];
            PreviousFrameGamePadState[(int)GameControllerIndex.One] = GamePad.GetState(PlayerIndex.One);
            PreviousFrameGamePadState[(int)GameControllerIndex.Two] = GamePad.GetState(PlayerIndex.Two);
        }

        public void ResetGameControllers()
        {
            ConnectedPlayerIndex = new bool[4] { false, false, false, false };
            GameControllerIndexToPlayerIndex = new PlayerIndex[2] { (PlayerIndex)(-1), (PlayerIndex)(-1) };
        }

        public bool PollGameControllerConnected(GameControllerIndex game_controller_index)
        {
            // already connected
            if ((int)GameControllerIndexToPlayerIndex[(int)game_controller_index] != (-1))
            {
                return true;     
            }

            // check all controllers
            if (!ConnectedPlayerIndex[(int)PlayerIndex.One])
            {
                if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.Start))// || IsKeyDown(Keys.F1))
                {
                    SetGameControllerIndex(game_controller_index, PlayerIndex.One);
                    return true;
                }
            }
            if (!ConnectedPlayerIndex[(int)PlayerIndex.Two])
            {
                if (GamePad.GetState(PlayerIndex.Two).IsButtonDown(Buttons.Start))// || IsKeyDown(Keys.F2))
                {
                    SetGameControllerIndex(game_controller_index, PlayerIndex.Two);
                    return true;
                }
            }
            if (!ConnectedPlayerIndex[(int)PlayerIndex.Three])
            {
                if (GamePad.GetState(PlayerIndex.Three).IsButtonDown(Buttons.Start))// || IsKeyDown(Keys.F3))
                {
                    SetGameControllerIndex(game_controller_index, PlayerIndex.Three);
                    return true;
                }
            }
            if (!ConnectedPlayerIndex[(int)PlayerIndex.Four])
            {
                if (GamePad.GetState(PlayerIndex.Four).IsButtonDown(Buttons.Start))// || IsKeyDown(Keys.F4))
                {
                    SetGameControllerIndex(game_controller_index, PlayerIndex.Four);
                    return true;
                }
            }

            return false;
        }

        public int CountConnectedControllers()
        {
            return ((int)GameControllerIndexToPlayerIndex[0] == -1 ? 0 : 1) +
                   ((int)GameControllerIndexToPlayerIndex[1] == -1 ? 0 : 1);
        }

        public void SetGameControllerIndex(GameControllerIndex game_controller_index, PlayerIndex player_index)
        {
            ConnectedPlayerIndex[(int)player_index] = true;
            GameControllerIndexToPlayerIndex[(int)game_controller_index] = player_index;
        }

        public void SetKeyboardControllerIndex(GameControllerIndex game_controller_index, PlayerIndex player_index)
        {
            ConnectedPlayerIndex[(int)player_index] = true;
            KeyboardControllerIndex = game_controller_index;
            GameControllerIndexToPlayerIndex[(int)game_controller_index] = player_index;
        }

        public GamePadState GetCurrentFrameGamePadState(GameControllerIndex game_controller_index)
        {
            return CurrentFrameGamePadState[(int)game_controller_index];
        }

        public GamePadState GetPreviousFrameGamePadState(GameControllerIndex game_controller_index)
        {
            return PreviousFrameGamePadState[(int)game_controller_index];
        }

        public void SetCurrentFrameGamePadState(GameControllerIndex game_controller_index, GamePadState state)
        {
            CurrentFrameGamePadState[(int)game_controller_index] = state;
        }

        public void Update()
        {
            PreviousFrameGamePadState[(int)GameControllerIndex.One] = CurrentFrameGamePadState[(int)GameControllerIndex.One];
            PreviousFrameGamePadState[(int)GameControllerIndex.Two] = CurrentFrameGamePadState[(int)GameControllerIndex.Two];

            PlayerIndex player_one = GameControllerIndexToPlayerIndex[0];
            PlayerIndex player_two = GameControllerIndexToPlayerIndex[1];

            if ((int)player_one != -1)
                CurrentFrameGamePadState[(int)GameControllerIndex.One] = GamePad.GetState(player_one);

            if ((int)player_two != -1)
                CurrentFrameGamePadState[(int)GameControllerIndex.Two] = GamePad.GetState(player_two);
        }

        public bool IsKeyboardController(GameControllerIndex game_controller_index)
        {
            return KeyboardControllerIndex == game_controller_index;
        }

        public bool IsKeyDown(Keys query)
        {
            return IsRawKeyDown(query);
        }

        public bool IsKeyDownGame(GameControllerIndex game_controller_index, Keys query)
        {
            return IsKeyboardController(game_controller_index) && IsRawKeyDown(query);
        }

        public bool IsKeyPressed(Keys query)
        {
            return IsRawKeyPressed(query);
        }

        public bool IsKeyPressedGame(GameControllerIndex game_controller_index, Keys query)
        {
            return IsKeyboardController(game_controller_index) && IsRawKeyPressed(query);
        }

        public bool IsPadLeftDown(GameControllerIndex game_controller_index)
        {
            return IsPadLeftDownImpl(CurrentFrameGamePadState[(int)game_controller_index]);
        }

        public bool IsPadRightDown(GameControllerIndex game_controller_index)
        {
            return IsPadRightDownImpl(CurrentFrameGamePadState[(int)game_controller_index]);
        }

        public bool IsPadUpDown(GameControllerIndex game_controller_index)
        {
            return IsPadUpDownImpl(CurrentFrameGamePadState[(int)game_controller_index]);
        }

        public bool IsPadDownDown(GameControllerIndex game_controller_index)
        {
            return IsPadDownDownImpl(CurrentFrameGamePadState[(int)game_controller_index]);
        }

        public bool IsPadLeftPressed(GameControllerIndex game_controller_index)
        {
            bool is_down = IsPadLeftDownImpl(CurrentFrameGamePadState[(int)game_controller_index]);
            bool was_down = IsPadLeftDownImpl(PreviousFrameGamePadState[(int)game_controller_index]);
            return is_down && !was_down;
        }

        public bool IsPadRightPressed(GameControllerIndex game_controller_index)
        {
            bool is_down = IsPadRightDownImpl(CurrentFrameGamePadState[(int)game_controller_index]);
            bool was_down = IsPadRightDownImpl(PreviousFrameGamePadState[(int)game_controller_index]);
            return is_down && !was_down;
        }

        public bool IsPadUpPressed(GameControllerIndex game_controller_index)
        {
            bool is_down = IsPadUpDownImpl(CurrentFrameGamePadState[(int)game_controller_index]);
            bool was_down = IsPadUpDownImpl(PreviousFrameGamePadState[(int)game_controller_index]);
            return is_down && !was_down;
        }

        public bool IsPadDownPressed(GameControllerIndex game_controller_index)
        {
            bool is_down = IsPadDownDownImpl(CurrentFrameGamePadState[(int)game_controller_index]);
            bool was_down = IsPadDownDownImpl(PreviousFrameGamePadState[(int)game_controller_index]);
            return is_down && !was_down;
        }

        public bool IsPadLeftPressedAny()
        {
            return IsPadLeftPressed(GameControllerIndex.One) ||
                   IsPadLeftPressed(GameControllerIndex.Two);
        }

        public bool IsPadRightPressedAny()
        {
            return IsPadRightPressed(GameControllerIndex.One) ||
                   IsPadRightPressed(GameControllerIndex.Two);
        }

        public bool IsPadUpPressedAny()
        {
            return IsPadUpPressed(GameControllerIndex.One) ||
                   IsPadUpPressed(GameControllerIndex.Two);
        }

        public bool IsPadDownPressedAny()
        {
            return IsPadDownPressed(GameControllerIndex.One) ||
                   IsPadDownPressed(GameControllerIndex.Two);
        }

        public bool IsPadConfirmPressed(GameControllerIndex game_controller_index)
        {
            bool is_down = CurrentFrameGamePadState[(int)game_controller_index].Buttons.A == ButtonState.Pressed;
            bool was_down = PreviousFrameGamePadState[(int)game_controller_index].Buttons.A == ButtonState.Pressed;
            return is_down && !was_down;
        }

        public bool IsPadConfirmPressedAny()
        {
            return IsPadConfirmPressed(GameControllerIndex.One) ||
                   IsPadConfirmPressed(GameControllerIndex.Two);
        }

        public bool IsPadCancelPressed(GameControllerIndex game_controller_index)
        {
            bool is_down = CurrentFrameGamePadState[(int)game_controller_index].Buttons.B == ButtonState.Pressed;
            bool was_down = PreviousFrameGamePadState[(int)game_controller_index].Buttons.B == ButtonState.Pressed;
            return is_down && !was_down;
        }

        public bool IsPadCancelPressedAny()
        {
            return IsPadCancelPressed(GameControllerIndex.One) ||
                   IsPadCancelPressed(GameControllerIndex.Two);
        }

        public bool IsPadCancelPressedAnyFilter(GameControllerIndex? filter)
        {
            if (filter == null)
                return IsPadCancelPressedAny();

            if (filter == GameControllerIndex.One)
                return IsPadCancelPressed(GameControllerIndex.Two);
            else
                return IsPadCancelPressed(GameControllerIndex.One);
        }

        public bool IsPadStartPressed(GameControllerIndex game_controller_index)
        {
            bool is_down = IsPadStartDownImpl(CurrentFrameGamePadState[(int)game_controller_index]);
            bool was_down = IsPadStartDownImpl(PreviousFrameGamePadState[(int)game_controller_index]);
            return is_down && !was_down;
        }

        public bool IsPadBackPressed(GameControllerIndex game_controller_index)
        {
            bool is_down = IsPadBackDownImpl(CurrentFrameGamePadState[(int)game_controller_index]);
            bool was_down = IsPadBackDownImpl(PreviousFrameGamePadState[(int)game_controller_index]);
            return is_down && !was_down;
        }

        public bool IsPadStartPressedAny()
        {
            return IsPadStartPressed(GameControllerIndex.One) ||
                   IsPadStartPressed(GameControllerIndex.Two);
        }

        public bool IsPadBackPressedAny()
        {
            return IsPadBackPressed(GameControllerIndex.One) ||
                   IsPadBackPressed(GameControllerIndex.Two);
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

        public bool IsL1Pressed(GameControllerIndex game_controller_index)
        {
            bool is_down = IsL1DownImpl(CurrentFrameGamePadState[(int)game_controller_index]);
            bool was_down = IsL1DownImpl(PreviousFrameGamePadState[(int)game_controller_index]);
            return is_down && !was_down;
        }

        public bool IsR1Pressed(GameControllerIndex game_controller_index)
        {
            bool is_down = IsR1DownImpl(CurrentFrameGamePadState[(int)game_controller_index]);
            bool was_down = IsR1DownImpl(PreviousFrameGamePadState[(int)game_controller_index]);
            return is_down && !was_down;
        }

        public bool IsL1PressedAny()
        {
            return IsL1Pressed(GameControllerIndex.One) ||
                   IsL1Pressed(GameControllerIndex.Two);
        }

        public bool IsR1PressedAny()
        {
            return IsR1Pressed(GameControllerIndex.One) ||
                   IsR1Pressed(GameControllerIndex.Two);
        }

        public bool IsL2Down(GameControllerIndex game_controller_index)
        {
            return IsPadL2DownImpl(CurrentFrameGamePadState[(int)game_controller_index]);
        }

        public bool IsR2Down(GameControllerIndex game_controller_index)
        {
            return IsR2DownImpl(CurrentFrameGamePadState[(int)game_controller_index]);
        }

        private bool IsPadLeftDownImpl(GamePadState state)
        {
            bool dpad = state.DPad.Left == ButtonState.Pressed;
            bool stick = state.ThumbSticks.Left.X < -DeadZone;
            return dpad || stick;
        }

        private bool IsPadRightDownImpl(GamePadState state)
        {
            bool dpad = state.DPad.Right == ButtonState.Pressed;
            bool stick = state.ThumbSticks.Left.X > DeadZone;
            return dpad || stick;
        }

        private bool IsPadUpDownImpl(GamePadState state)
        {
            bool dpad = state.DPad.Up == ButtonState.Pressed;
            bool stick = state.ThumbSticks.Left.Y > DeadZone;
            return dpad || stick;
        }

        private bool IsPadDownDownImpl(GamePadState state)
        {
            bool dpad = state.DPad.Down == ButtonState.Pressed;
            bool stick = state.ThumbSticks.Left.Y < -DeadZone;
            return dpad || stick;
        }

        private bool IsL1DownImpl(GamePadState state)
        {
            bool down = state.Buttons.LeftShoulder == ButtonState.Pressed;
            return down;
        }

        private bool IsR1DownImpl(GamePadState state)
        {
            bool down = state.Buttons.RightShoulder == ButtonState.Pressed;
            return down;
        }

        private bool IsPadL2DownImpl(GamePadState state)
        {
            bool down = state.Triggers.Left > DeadZone;
            return down;
        }

        private bool IsR2DownImpl(GamePadState state)
        {
            bool down = state.Triggers.Right > DeadZone;
            return down;
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
#else
            if (!IsFocused())
                return false;

            int keycode = (int)key;
            int keystate = GetAsyncKeyState(keycode);
            int is_down = keystate & 0x8000;
            return is_down != 0;
#endif
        }

        public static bool IsRawKeyPressed(Keys key)
        {
#if XBOX360
            return false;
#else
            if (!IsFocused())
                return false;

            int keycode = (int)key;
            int keystate = GetAsyncKeyState(keycode);
            int is_down = keystate & 0x8000;
            int is_pressed = keystate & 0x0001;
            return is_down != 0 && is_pressed != 0;
#endif
        }

        private static bool IsFocused()
        {
            return CGalaxy.ApplicationFocusedFlag;
        }
    }
}

