//
// Input.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Runtime.InteropServices;

namespace Galaxy
{
    public enum InputSourceIndex
    {
        ControllerOne, // must be same value as PlayerIndex.One
        ControllerTwo, // must be same value as PlayerIndex.Two
        Keyboard,
    };

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
            TriggerLeft = state.Triggers.Left;
            TriggerRight = state.Triggers.Right;
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
        public float TriggerLeft;
        public float TriggerRight;
    }

    public class CInput
    {
        public const float DeadZone = 0.025f;

        private CGalaxy Game { get; set; }
        private bool[] ConnectedPlayerIndex { get; set; }
        private InputSourceIndex[] GameControllerIndexToInputSourceIndex { get; set; }
        private GamePadState[] PreviousFrameGamePadState { get; set; }
        private GamePadButtonsMutable[] CurrentFrameGamePadButtonsState { get; set; }
        private GamePadButtonsMutable[] PreviousFrameGamePadButtonsState { get; set; }

        public CInput(CGalaxy game)
        {
            Game = game;
            ConnectedPlayerIndex = new bool[4] { false, false, false, false };
            GameControllerIndexToInputSourceIndex = new InputSourceIndex[2] { (InputSourceIndex)(-1), (InputSourceIndex)(-1) };
            CurrentFrameGamePadButtonsState = new GamePadButtonsMutable[2];
            CurrentFrameGamePadButtonsState[(int)GameControllerIndex.One] = new GamePadButtonsMutable(GamePad.GetState(PlayerIndex.One));
            CurrentFrameGamePadButtonsState[(int)GameControllerIndex.Two] = new GamePadButtonsMutable(GamePad.GetState(PlayerIndex.Two));
            PreviousFrameGamePadState = new GamePadState[2];
            PreviousFrameGamePadState[(int)GameControllerIndex.One] = GamePad.GetState(PlayerIndex.One);
            PreviousFrameGamePadState[(int)GameControllerIndex.Two] = GamePad.GetState(PlayerIndex.Two);
            PreviousFrameGamePadButtonsState = new GamePadButtonsMutable[2];
            PreviousFrameGamePadButtonsState[(int)GameControllerIndex.One] = new GamePadButtonsMutable(GamePad.GetState(PlayerIndex.One));
            PreviousFrameGamePadButtonsState[(int)GameControllerIndex.Two] = new GamePadButtonsMutable(GamePad.GetState(PlayerIndex.Two));
        }

        public void ResetGameControllers()
        {
            ConnectedPlayerIndex = new bool[4] { false, false, false, false };
            GameControllerIndexToInputSourceIndex = new InputSourceIndex[2] { (InputSourceIndex)(-1), (InputSourceIndex)(-1) };
        }

        public bool PollGameControllerConnected(GameControllerIndex game_controller_index)
        {
            // already connected
            if ((int)GameControllerIndexToInputSourceIndex[(int)game_controller_index] != (-1))
            {
                return true;     
            }

            // check all controllers
            if (!ConnectedPlayerIndex[(int)game_controller_index])
            {
                // only valid if no other keyboards are connected
                if (IsKeyDown(Keys.Enter) && GameControllerIndexToInputSourceIndex[0] != InputSourceIndex.Keyboard && GameControllerIndexToInputSourceIndex[1] != InputSourceIndex.Keyboard)
                {
                    SetGameControllerInputSourceIndex(game_controller_index, InputSourceIndex.Keyboard);
                    return true;
                }
                if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.Start) && GameControllerIndexToInputSourceIndex[0] != InputSourceIndex.ControllerOne && GameControllerIndexToInputSourceIndex[1] != InputSourceIndex.ControllerOne)
                {
                    SetGameControllerInputSourceIndex(game_controller_index, InputSourceIndex.ControllerOne);
                    return true;
                }
                // is this correct? 
                if (GamePad.GetState(PlayerIndex.Two).IsButtonDown(Buttons.Start) && GameControllerIndexToInputSourceIndex[0] != InputSourceIndex.ControllerTwo && GameControllerIndexToInputSourceIndex[1] != InputSourceIndex.ControllerTwo)
                {
                    SetGameControllerInputSourceIndex(game_controller_index, InputSourceIndex.ControllerTwo);
                    return true;
                }
            }

            return false;
        }

        public int CountConnectedControllers()
        {
            return ((int)GameControllerIndexToInputSourceIndex[0] == -1 ? 0 : 1) +
                   ((int)GameControllerIndexToInputSourceIndex[1] == -1 ? 0 : 1);
        }

        public void SetGameControllerInputSourceIndex(GameControllerIndex game_controller_index, InputSourceIndex input_source_index)
        {
            ConnectedPlayerIndex[(int)game_controller_index] = true;
            GameControllerIndexToInputSourceIndex[(int)game_controller_index] = input_source_index;
        }

        public GamePadButtonsMutable GetCurrentFrameGamePadButtonsState(GameControllerIndex game_controller_index)
        {
            return CurrentFrameGamePadButtonsState[(int)game_controller_index];
        }

        public void SetCurrentFrameGamePadButtonsState(GameControllerIndex game_controller_index, GamePadButtonsMutable buttons)
        {
            CurrentFrameGamePadButtonsState[(int)game_controller_index] = buttons;
        }

        public GamePadState GetPreviousFrameGamePadState(GameControllerIndex game_controller_index)
        {
            return PreviousFrameGamePadState[(int)game_controller_index];
        }

        public GamePadButtonsMutable GetGamePadButtonsStateFromInputSource(InputSourceIndex input_source_index)
        {
            switch (input_source_index)
            {
                case InputSourceIndex.Keyboard:
                    {
                        GamePadButtonsMutable buttons = new GamePadButtonsMutable();
                        buttons.Y = ButtonState.Released;
                        buttons.X = Game.Input.IsKeyDown(Keys.V) ? ButtonState.Pressed : ButtonState.Released;
                        buttons.A = Game.Input.IsKeyDown(Keys.Enter) ? ButtonState.Pressed : ButtonState.Released;
                        buttons.B = Game.Input.IsKeyDown(Keys.Escape) ? ButtonState.Pressed : ButtonState.Released;
                        buttons.LeftShoulder = Game.Input.IsKeyDown(Keys.X) ? ButtonState.Pressed : ButtonState.Released;
                        buttons.RightShoulder = Game.Input.IsKeyDown(Keys.C) ? ButtonState.Pressed : ButtonState.Released;
                        buttons.Back = Game.Input.IsKeyDown(Keys.Back) ? ButtonState.Pressed : ButtonState.Released;
                        buttons.Start = Game.Input.IsKeyDown(Keys.Tab) ? ButtonState.Pressed : ButtonState.Released;
                        buttons.DPadUp = Game.Input.IsKeyDown(Keys.Up) ? ButtonState.Pressed : ButtonState.Released;
                        buttons.DPadDown = Game.Input.IsKeyDown(Keys.Down) ? ButtonState.Pressed : ButtonState.Released;
                        buttons.DPadLeft = Game.Input.IsKeyDown(Keys.Left) ? ButtonState.Pressed : ButtonState.Released;
                        buttons.DPadRight = Game.Input.IsKeyDown(Keys.Right) ? ButtonState.Pressed : ButtonState.Released;
                        buttons.TriggerLeft = 0.0f;
                        buttons.TriggerRight = 0.0f;
                        return buttons;
                    }

                case InputSourceIndex.ControllerOne:
                    {
                        return new GamePadButtonsMutable(GamePad.GetState(PlayerIndex.One));
                    }

                case InputSourceIndex.ControllerTwo:
                    {
                        return new GamePadButtonsMutable(GamePad.GetState(PlayerIndex.Two));
                    }
            }

            return new GamePadButtonsMutable();
        }

        public void Update()
        {
            PreviousFrameGamePadButtonsState[(int)GameControllerIndex.One] = CurrentFrameGamePadButtonsState[(int)GameControllerIndex.One];
            PreviousFrameGamePadButtonsState[(int)GameControllerIndex.Two] = CurrentFrameGamePadButtonsState[(int)GameControllerIndex.Two];

            InputSourceIndex player_one_input = GameControllerIndexToInputSourceIndex[0];
            InputSourceIndex player_two_input = GameControllerIndexToInputSourceIndex[1];

            CurrentFrameGamePadButtonsState[0] = GetGamePadButtonsStateFromInputSource(player_one_input);
            CurrentFrameGamePadButtonsState[1] = GetGamePadButtonsStateFromInputSource(player_two_input);
        }

        public bool IsKeyboardController(GameControllerIndex game_controller_index)
        {
            return GameControllerIndexToInputSourceIndex[(int)game_controller_index] == InputSourceIndex.Keyboard;
        }

        public bool IsKeyDown(Keys query)
        {
            return IsRawKeyDown(query);
        }

        public bool IsKeyDownGame(GameControllerIndex game_controller_index, Keys query)
        {
            // deprecated
            return false;
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
            return IsPadL2DownImpl(CurrentFrameGamePadButtonsState[(int)game_controller_index]);
        }

        public bool IsR2Down(GameControllerIndex game_controller_index)
        {
            return IsR2DownImpl(CurrentFrameGamePadButtonsState[(int)game_controller_index]);
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

        private bool IsPadL2DownImpl(GamePadButtonsMutable state)
        {
            bool down = state.TriggerLeft > DeadZone;
            return down;
        }

        private bool IsR2DownImpl(GamePadButtonsMutable state)
        {
            bool down = state.TriggerRight > DeadZone;
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

