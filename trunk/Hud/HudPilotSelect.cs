//
// HudPilotSelect.cs
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Galaxy
{
    public class HudPilotSelect
    {
        public GameControllerIndex GameControllerIndex { get; set; }
        public CGalaxy Game { get; set; }
        public string NameText { get; set; }
        public string MoneyText { get; set; }
        public int Cursor { get; set; }

        private CVisual LeftPanelVisual { get; set; }
        private CVisual RightPanelVisual { get; set; }
        private CVisual LeftPanelFramesVisual { get; set; }
        private CVisual RightPanelFramesVisual { get; set; }
        private CVisual EnergyVisual { get; set; }
        private CVisual ShieldVisual { get; set; }
        private CVisual ArmorVisual { get; set; }
        private CVisual MoneyIconVisual { get; set; }
        private CVisual EnergyIconVisual { get; set; }
        private CVisual ShieldIconVisual { get; set; }
        private CVisual ArmorIconVisual { get; set; }
        private List<CVisual> PortraitIconVisuals { get; set; }

        private Vector2 BasePosition { get; set; }
        private Vector2 LeftPanelPosition { get; set; }
        private Vector2 RightPanelPosition { get; set; }
        public Vector2 NameTextPosition { get; set; }
        public Vector2 MoneyTextPosition { get; set; }
        private Vector2 MoneyIconPosition { get; set; }
        private Vector2 TextDisplayPosition { get; set; }
        private Vector2 PortraitIconPosition { get; set; }

        private Keys StartKey { get; set; }

        public enum EState
        {
            PressStart, // display press start
            Inactive,   // no player
            Active,     // selecting profile
            Locked,     // profile selected
        }
        public EState State { get; set; }

        public HudPilotSelect(CGalaxy game, Vector2 base_position, GameControllerIndex game_controller_index)
        {
            GameControllerIndex = game_controller_index;
            Game = game;
            BasePosition = base_position;

            NameText = "Profile";
            MoneyText = null;

            Cursor = (int)GameControllerIndex;

            LeftPanelVisual = CVisual.MakeSpriteUncached(Game, "Textures/UI/PanelBackground");
            RightPanelVisual = CVisual.MakeSpriteUncached(Game, "Textures/UI/PanelBackground");
            LeftPanelFramesVisual = CVisual.MakeSpriteUncached(Game, "Textures/UI/PanelFrames");
            RightPanelFramesVisual = CVisual.MakeSpriteUncached(Game, "Textures/UI/PanelFrames");
            PortraitIconVisuals = new List<CVisual>() {
                CVisual.MakeSpriteUncached(Game, "Textures/UI/KazukiPortrait"),
                CVisual.MakeSpriteUncached(Game, "Textures/UI/RabbitPortrait"),
                CVisual.MakeSpriteUncached(Game, "Textures/UI/GunthorPortrait"),
                //CVisual.MakeSpriteUncached(Game, "Textures/UI/MysteryPortrait"),
            };

            LeftPanelPosition = new Vector2(0.0f, 0.0f);
            RightPanelPosition = new Vector2(Game.GraphicsDevice.Viewport.Width, 0.0f);

            NameTextPosition = BasePosition + new Vector2(80.0f, -964.0f);
            MoneyTextPosition = BasePosition + new Vector2(100.0f, -888.0f);
            TextDisplayPosition = BasePosition + new Vector2(238.0f, -720.0f);
            PortraitIconPosition = BasePosition + new Vector2(238.0f, -608.0f);

            LeftPanelVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 0.0f;
            RightPanelVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 0.0f;
            LeftPanelFramesVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 3.0f;
            RightPanelFramesVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 3.0f;

            foreach (CVisual portrait in PortraitIconVisuals)
            {
                portrait.Depth = CLayers.UI + CLayers.SubLayerIncrement * 2.0f;
                portrait.Update();
            }

            LeftPanelVisual.NormalizedOrigin = new Vector2(0.0f, 0.0f);
            RightPanelVisual.NormalizedOrigin = new Vector2(1.0f, 0.0f);
            LeftPanelFramesVisual.NormalizedOrigin = new Vector2(0.0f, 0.0f);
            RightPanelFramesVisual.NormalizedOrigin = new Vector2(1.0f, 0.0f);

            // TODO: force a recache, need to handle non-entity visuals better
            LeftPanelVisual.Update();
            RightPanelVisual.Update();
            LeftPanelFramesVisual.Update();
            RightPanelFramesVisual.Update();
        }

        public void Update()
        {
            switch (State)
            {
                case EState.PressStart:
                    if (Game.Input.PollGameControllerConnected(GameControllerIndex))
                    {
                        State = EState.Inactive;
                    }
                    break;

                case EState.Inactive:
                    break;

                case EState.Active:
                    // TODO: up/down control for profile select
                    if (Game.Input.IsPadConfirmPressed(GameControllerIndex) || Game.Input.IsKeyPressed(Keys.Enter))
                    {
                        Lock();
                    }

                    PortraitIconVisuals[Cursor].Scale = Vector2.One;

                    int other_player = GameControllerIndex == GameControllerIndex.One ? 1 : 0;
                    int other_cursor = Game.HudManager.HudsProfileSelect[other_player].Cursor;
                    if (Game.Input.IsPadUpPressed(GameControllerIndex) || Game.Input.IsKeyPressed(Keys.Up))
                    {
                        int next_valid = Cursor;

                        for (int i = Cursor - 1; i >= 0; --i)
                        {
                            if (i == other_cursor)
                                continue;

                            next_valid = i;
                            break;
                        }

                        Cursor = next_valid;
                    }

                    if (Game.Input.IsPadDownPressed(GameControllerIndex) || Game.Input.IsKeyPressed(Keys.Down))
                    {
                        int next_valid = Cursor;

                        for (int i = Cursor + 1; i < PortraitIconVisuals.Count; ++i)
                        {
                            if (i == other_cursor)
                                continue;

                            next_valid = i;
                            break;
                        }

                        Cursor = next_valid;
                    }

                    PortraitIconVisuals[Cursor].Scale = Vector2.One + Vector2.One * 0.025f * (float)Math.Sin(Game.GameFrame * 0.1f);
                    break;

                case EState.Locked:
                    // NOTE: no more disabling mid-game
                    //if (Game.Input.IsPadStartPressed(GameControllerIndex) || Game.Input.IsKeyPressed(StartKey))
                    //{
                        //Game.HudManager.ToggleProfileActive(GameControllerIndex);
                        //State = EState.Inactive;
                    //}
                    break;
            }
        }

        public void Draw(SpriteBatch sprite_batch)
        {
            switch (State)
            {
                case EState.Inactive:
                    break;

                case EState.Active:
                    Vector2 offset = new Vector2(0.0f, 210.0f);
                    Vector2 position = PortraitIconPosition;
                    foreach (CVisual portrait in PortraitIconVisuals)
                    {
                        portrait.Draw(sprite_batch, position, 0.0f);
                        position += offset;
                    }

                    break;

                case EState.Locked:
                    break;
            }
        }

        public void DrawEditor(SpriteBatch sprite_batch)
        {
            sprite_batch.DrawString(Game.DefaultFont, String.Format("todo"), new Vector2(8.0f, 1020.0f), Color.White);
        }

        public void PressStart()
        {
            State = EState.PressStart;
        }

        public void Deactivate()
        {
            State = EState.Inactive;    
        }

        public void Activate()
        {
            State = EState.Active;
        }

        public void Lock()
        {
            State = EState.Locked;    

            // TODO: correct mapping somewhere
            string pilot = "";
            switch (Cursor)
            {
                case 0: pilot = "Kazuki"; break;
                case 1: pilot = "Rabbit"; break;
                case 2: pilot = "Gunthor"; break;
                //case 3: pilot = "Mystery"; break;
                default:
                    break;
            }

            CSaveData.GetCurrentProfile().Game.Pilots[(int)GameControllerIndex].Pilot = pilot;
            Game.HudManager.Huds[(int)GameControllerIndex].UpdatePilot();
        }
    }
}
