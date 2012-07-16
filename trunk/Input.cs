//
// Input.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Runtime.InteropServices;

namespace Galaxy
{
    // workaround because xna class is not mutable, blwarhg
    public struct GamePadButtonsMutable
    {
        public GamePadButtonsMutable(GamePadState state)
        {
            A = state.Buttons.A;
            B = state.Buttons.B;
            Back = state.Buttons.Back;
            BigButton = state.Buttons.BigButton;
            LeftShoulder = state.Buttons.LeftShoulder;
            LeftStick = state.Buttons.LeftStick;
            RightShoulder = state.Buttons.RightShoulder;
            RightStick = state.Buttons.RightStick;
            Start = state.Buttons.Start;
            X = state.Buttons.X;
            Y = state.Buttons.Y;
            DPadUp = state.DPad.Up;
            DPadDown = state.DPad.Down;
            DPadLeft = state.DPad.Left;
            DPadRight = state.DPad.Right;
            ThumbstickLeft = state.ThumbSticks.Left;
            ThumbstickRight = state.ThumbSticks.Right;
        }

        public ButtonState A;
        public ButtonState B;
        public ButtonState Back;
        public ButtonState BigButton;
        public ButtonState LeftShoulder;
        public ButtonState LeftStick;
        public ButtonState RightShoulder;
        public ButtonState RightStick;
        public ButtonState Start;
        public ButtonState X;
        public ButtonState Y;
        public ButtonState DPadUp;
        public ButtonState DPadDown;
        public ButtonState DPadLeft;
        public ButtonState DPadRight;
        public Vector2 ThumbstickLeft;
        public Vector2 ThumbstickRight;
    }

    public class CInput
    {
        public const float DeadZone = 0.025f;

        private CGalaxy Game { get; set; }
        private bool[] ConnectedPlayerIndex { get; set; }
        private PlayerIndex[] GameControllerIndexToPlayerIndex { get; set; }
        private GamePadState[] CurrentFrameGamePadState { get; set; }
        private GamePadState[] PreviousFrameGamePadState { get; set; }
        private GamePadButtonsMutable[] CurrentFrameGamePadButtonsState { get; set; }
        private GamePadButtonsMutable[] PreviousFrameGamePadButtonsState { get; set; }
        private GameControllerIndex KeyboardControllerIndex;

        public CInput(CGalaxy game)
        {
            Game = game;
            ConnectedPlayerIndex = new bool[4] { false, false, false, false };
            GameControllerIndexToPlayerIndex = new PlayerIndex[2] { (PlayerIndex)(-1), (PlayerIndex)(-1) };
            CurrentFrameGamePadState = new GamePadState[2];
            CurrentFrameGamePadState[(int)GameControllerIndex.One] = GamePad.GetState(PlayerIndex.One);
            CurrentFrameGamePadState[(int)GameControllerIndex.Two] = GamePad.GetState(PlayerIndex.Two);
            CurrentFrameGamePadButtonsState = new GamePadButtonsMutable[2];
            CurrentFrameGamePadButtonsState[(int)GameControllerIndex.One] = new GamePadButtonsMutable(GamePad.GetState(PlayerIndex.One));
            CurrentFrameGamePadButtonsState[(int)GameControllerIndex.Two] = new GamePadButtonsMutable(GamePad.GetState(PlayerIndex.Two));
            PreviousFrameGamePadState = new GamePadState[2];
            PreviousFrameGamePadState[(int)GameControllerIndex.One] = GamePad.GetState(PlayerIndex.One);
            PreviousFrameGamePadState[(int)GameControllerIndex.Two] = GamePad.GetState(PlayerIndex.Two);
            PreviousFrameGamePadButtonsState = new GamePadButtonsMutable[2];
            PreviousFrameGamePadButtonsState[(int)GameControllerIndex.One] = new GamePadButtonsMutable(GamePad.GetState(PlayerIndex.One));
            PreviousFrameGamePadButtonsState[(int)GameControllerIndex.Two] = new GamePadButtonsMutable(GamePad.GetState(PlayerIndex.Two));
            KeyboardControllerIndex = (GameControllerIndex)(-1);
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
                if (IsKeyDown(Keys.Enter) && (int)KeyboardControllerIndex == -1)
                {
                    KeyboardControllerIndex = game_controller_index;
                    SetGameControllerIndex(game_controller_index, PlayerIndex.One);
                    return true;
                }

                //if (GamePad.GetState(game_controller_index).IsButtonDown(Buttons.Start))
                //{
                    //SetGameControllerIndex(game_controller_index, PlayerIndex.One);
                    //return true;
                //}
            }
            if (!ConnectedPlayerIndex[(int)PlayerIndex.Two])
            {
                if (IsKeyDown(Keys.Enter) && (int)KeyboardControllerIndex == -1)
                {
                    KeyboardControllerIndex = game_controller_index;
                    SetGameControllerIndex(game_controller_index, PlayerIndex.Two);
                    return true;
                }
                if (GamePad.GetState(PlayerIndex.Two).IsButtonDown(Buttons.Start))
                {
                    SetGameControllerIndex(GameControllerIndex.Two, PlayerIndex.Two);
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

        public GamePadButtonsMutable GetCurrentFrameGamePadButtonsState(GameControllerIndex game_controller_index)
        {
            return CurrentFrameGamePadButtonsState[(int)game_controller_index];
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
            PreviousFrameGamePadButtonsState[(int)GameControllerIndex.One] = CurrentFrameGamePadButtonsState[(int)GameControllerIndex.One];
            PreviousFrameGamePadButtonsState[(int)GameControllerIndex.Two] = CurrentFrameGamePadButtonsState[(int)GameControllerIndex.Two];

            PlayerIndex player_one = GameControllerIndexToPlayerIndex[0];
            PlayerIndex player_two = GameControllerIndexToPlayerIndex[1];

            if ((int)player_one != -1)
            {
                CurrentFrameGamePadState[(int)GameControllerIndex.One] = GamePad.GetState(player_one);
                CurrentFrameGamePadButtonsState[(int)GameControllerIndex.One] = new GamePadButtonsMutable(CurrentFrameGamePadState[(int)GameControllerIndex.One]);
            }

            if ((int)player_two != -1)
            {
                CurrentFrameGamePadState[(int)GameControllerIndex.Two] = GamePad.GetState(player_two);
                CurrentFrameGamePadButtonsState[(int)GameControllerIndex.Two] = new GamePadButtonsMutable(CurrentFrameGamePadState[(int)GameControllerIndex.Two]);
            }

            if ((int)KeyboardControllerIndex != -1)
            {
                int index = (int)KeyboardControllerIndex;
                CurrentFrameGamePadButtonsState[index].X = Game.Input.IsKeyDown(Keys.V) ? ButtonState.Pressed : ButtonState.Released;
                CurrentFrameGamePadButtonsState[index].A = Game.Input.IsKeyDown(Keys.Enter) ? ButtonState.Pressed : ButtonState.Released;
                CurrentFrameGamePadButtonsState[index].B = Game.Input.IsKeyDown(Keys.Escape) ? ButtonState.Pressed : ButtonState.Released;
                CurrentFrameGamePadButtonsState[index].LeftShoulder = Game.Input.IsKeyDown(Keys.X) ? ButtonState.Pressed : ButtonState.Released;
                CurrentFrameGamePadButtonsState[index].RightShoulder = Game.Input.IsKeyDown(Keys.C) ? ButtonState.Pressed : ButtonState.Released;
                CurrentFrameGamePadButtonsState[index].Back = Game.Input.IsKeyDown(Keys.Back) ? ButtonState.Pressed : ButtonState.Released;
                CurrentFrameGamePadButtonsState[index].Start = Game.Input.IsKeyDown(Keys.Tab) ? ButtonState.Pressed : ButtonState.Released;
                CurrentFrameGamePadButtonsState[index].DPadUp = Game.Input.IsKeyDown(Keys.Up) ? ButtonState.Pressed : ButtonState.Released;
                CurrentFrameGamePadButtonsState[index].DPadDown = Game.Input.IsKeyDown(Keys.Down) ? ButtonState.Pressed : ButtonState.Released;
                CurrentFrameGamePadButtonsState[index].DPadLeft = Game.Input.IsKeyDown(Keys.Left) ? ButtonState.Pressed : ButtonState.Released;
                CurrentFrameGamePadButtonsState[index].DPadRight = Game.Input.IsKeyDown(Keys.Right) ? ButtonState.Pressed : ButtonState.Released;
            }
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
            return false;
            return IsKeyboardController(game_controller_index) && IsRawKeyDown(query);
        }

        public bool IsKeyPressed(Keys query)
        {
            return IsRawKeyPressed(query);
        }

        public bool IsKeyPressedGame(GameControllerIndex game_controller_index, Keys query)
        {
            return false;
            return IsKeyboardController(game_controller_index) && IsRawKeyPressed(query);
        }

        public bool IsPadLeftDown(GameControllerIndex game_controller_index)
        {
            return IsPadLeftDownImpl(CurrentFrameGamePadButtonsState[(int)game_controller_index]);
        }

        public bool IsPadRightDown(GameControllerIndex game_controller_index)
        {
            return IsPadRightDownImpl(CurrentFrameGamePadButtonsState[(int)game_controller_index]);
        }

        public bool IsPadUpDown(GameControllerIndex game_controller_index)
        {
            return IsPadUpDownImpl(CurrentFrameGamePadButtonsState[(int)game_controller_index]);
        }

        public bool IsPadDownDown(GameControllerIndex game_controller_index)
        {
            return IsPadDownDownImpl(CurrentFrameGamePadButtonsState[(int)game_controller_index]);
        }

        public bool IsPadLeftPressed(GameControllerIndex game_controller_index)
        {
            bool is_down = IsPadLeftDownImpl(CurrentFrameGamePadButtonsState[(int)game_controller_index]);
            bool was_down = IsPadLeftDownImpl(PreviousFrameGamePadButtonsState[(int)game_controller_index]);
            return is_down && !was_down;
        }

        public bool IsPadRightPressed(GameControllerIndex game_controller_index)
        {
            bool is_down = IsPadRightDownImpl(CurrentFrameGamePadButtonsState[(int)game_controller_index]);
            bool was_down = IsPadRightDownImpl(PreviousFrameGamePadButtonsState[(int)game_controller_index]);
            return is_down && !was_down;
        }

        public bool IsPadUpPressed(GameControllerIndex game_controller_index)
        {
            bool is_down = IsPadUpDownImpl(CurrentFrameGamePadButtonsState[(int)game_controller_index]);
            bool was_down = IsPadUpDownImpl(PreviousFrameGamePadButtonsState[(int)game_controller_index]);
            return is_down && !was_down;
        }

        public bool IsPadDownPressed(GameControllerIndex game_controller_index)
        {
            bool is_down = IsPadDownDownImpl(CurrentFrameGamePadButtonsState[(int)game_controller_index]);
            bool was_down = IsPadDownDownImpl(PreviousFrameGamePadButtonsState[(int)game_controller_index]);
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
            bool is_down = CurrentFrameGamePadButtonsState[(int)game_controller_index].A == ButtonState.Pressed;
            bool was_down = PreviousFrameGamePadButtonsState[(int)game_controller_index].A == ButtonState.Pressed;
            return is_down && !was_down;
        }

        public bool IsPadConfirmPressedAny()
        {
            return IsPadConfirmPressed(GameControllerIndex.One) ||
                   IsPadConfirmPressed(GameControllerIndex.Two);
        }

        public bool IsPadCancelPressed(GameControllerIndex game_controller_index)
        {
            bool is_down = CurrentFrameGamePadButtonsState[(int)game_controller_index].B == ButtonState.Pressed;
            bool was_down = PreviousFrameGamePadButtonsState[(int)game_controller_index].B == ButtonState.Pressed;
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
            bool is_down = IsPadStartDownImpl(CurrentFrameGamePadButtonsState[(int)game_controller_index]);
            bool was_down = IsPadStartDownImpl(PreviousFrameGamePadButtonsState[(int)game_controller_index]);
            return is_down && !was_down;
        }

        public bool IsPadBackPressed(GameControllerIndex game_controller_index)
        {
            bool is_down = IsPadBackDownImpl(CurrentFrameGamePadButtonsState[(int)game_controller_index]);
            bool was_down = IsPadBackDownImpl(PreviousFrameGamePadButtonsState[(int)game_controller_index]);
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

        private bool IsPadStartDownImpl(GamePadButtonsMutable state)
        {
            bool down = state.Start == ButtonState.Pressed;
            return down;
        }

        private bool IsPadBackDownImpl(GamePadButtonsMutable state)
        {
            bool down = state.Back == ButtonState.Pressed;
            return down;
        }

        public bool IsL1Pressed(GameControllerIndex game_controller_index)
        {
            bool is_down = IsL1DownImpl(CurrentFrameGamePadButtonsState[(int)game_controller_index]);
            bool was_down = IsL1DownImpl(PreviousFrameGamePadButtonsState[(int)game_controller_index]);
            return is_down && !was_down;
        }

        public bool IsR1Pressed(GameControllerIndex game_controller_index)
        {
            bool is_down = IsR1DownImpl(CurrentFrameGamePadButtonsState[(int)game_controller_index]);
            bool was_down = IsR1DownImpl(PreviousFrameGamePadButtonsState[(int)game_controller_index]);
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

        private bool IsPadLeftDownImpl(GamePadButtonsMutable state)
        {
            bool dpad = state.DPadLeft == ButtonState.Pressed;
            bool stick = state.ThumbstickLeft.X < -DeadZone;
            return dpad || stick;
        }

        private bool IsPadRightDownImpl(GamePadButtonsMutable state)
        {
            bool dpad = state.DPadRight == ButtonState.Pressed;
            bool stick = state.ThumbstickLeft.X > DeadZone;
            return dpad || stick;
        }

        private bool IsPadUpDownImpl(GamePadButtonsMutable state)
        {
            bool dpad = state.DPadUp == ButtonState.Pressed;
            bool stick = state.ThumbstickLeft.Y > DeadZone;
            return dpad || stick;
        }

        private bool IsPadDownDownImpl(GamePadButtonsMutable state)
        {
            bool dpad = state.DPadDown == ButtonState.Pressed;
            bool stick = state.ThumbstickLeft.Y < -DeadZone;
            return dpad || stick;
        }

        private bool IsL1DownImpl(GamePadButtonsMutable state)
        {
            bool down = state.LeftShoulder == ButtonState.Pressed;
            return down;
        }

        private bool IsR1DownImpl(GamePadButtonsMutable state)
        {
            bool down = state.RightShoulder == ButtonState.Pressed;
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

