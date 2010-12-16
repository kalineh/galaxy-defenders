//
// HudProfileSelect.cs
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
    public class CHudProfileSelect
    {
        public PlayerIndex PlayerIndex { get; set; }
        public CGalaxy Game { get; set; }
        public string NameText { get; set; }
        public string MoneyText { get; set; }

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
        private CVisual PortraitIconVisual { get; set; }

        private Vector2 BasePosition { get; set; }
        private Vector2 LeftPanelPosition { get; set; }
        private Vector2 RightPanelPosition { get; set; }
        public Vector2 NameTextPosition { get; set; }
        public Vector2 MoneyTextPosition { get; set; }
        private Vector2 MoneyIconPosition { get; set; }
        private Vector2 PortraitIconPosition { get; set; }

        private Keys StartKey { get; set; }

        private enum EState
        {
            Disabled, // main menu, not used yet
            Inactive, // no player
            Active,   // selecting profile
            Locked,   // profile selected
        }
        private EState State { get; set; }

        public CHudProfileSelect(CGalaxy game, Vector2 base_position, PlayerIndex player_index)
        {
            PlayerIndex = player_index;
            Game = game;
            BasePosition = base_position;

            StartKey = player_index == PlayerIndex.One ? Keys.F1 : Keys.F2;

            NameText = "Profile";
            MoneyText = null;

            LeftPanelVisual = CVisual.MakeSpriteUncached(Game, "Textures/UI/PanelBackground");
            RightPanelVisual = CVisual.MakeSpriteUncached(Game, "Textures/UI/PanelBackground");
            LeftPanelFramesVisual = CVisual.MakeSpriteUncached(Game, "Textures/UI/PanelFrames");
            RightPanelFramesVisual = CVisual.MakeSpriteUncached(Game, "Textures/UI/PanelFrames");
            PortraitIconVisual = CVisual.MakeSpriteUncached(Game, "Textures/Top/Pixel");

            LeftPanelPosition = new Vector2(0.0f, 0.0f);
            RightPanelPosition = new Vector2(Game.GraphicsDevice.Viewport.Width, 0.0f);

            NameTextPosition = BasePosition + new Vector2(80.0f, -964.0f);
            MoneyTextPosition = BasePosition + new Vector2(100.0f, -888.0f);
            PortraitIconPosition = BasePosition + new Vector2(238.0f, -608.0f);

            LeftPanelVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 0.0f;
            RightPanelVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 0.0f;
            LeftPanelFramesVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 3.0f;
            RightPanelFramesVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 3.0f;
            PortraitIconVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 2.0f;

            LeftPanelVisual.NormalizedOrigin = new Vector2(0.0f, 0.0f);
            RightPanelVisual.NormalizedOrigin = new Vector2(1.0f, 0.0f);
            LeftPanelFramesVisual.NormalizedOrigin = new Vector2(0.0f, 0.0f);
            RightPanelFramesVisual.NormalizedOrigin = new Vector2(1.0f, 0.0f);

            // TODO: force a recache, need to handle non-entity visuals better
            LeftPanelVisual.Update();
            RightPanelVisual.Update();
            LeftPanelFramesVisual.Update();
            RightPanelFramesVisual.Update();
            PortraitIconVisual.Update();
        }

        public void Update()
        {
            switch (State)
            {
                case EState.Disabled:
                    break;

                case EState.Inactive:
                    if (Game.Input.IsPadStartPressed(PlayerIndex) || Game.Input.IsKeyPressed(StartKey))
                    {
                        State = EState.Active;
                    }
                    break;

                case EState.Active:
                    // TODO: up/down control for profile select
                    if (Game.Input.IsPadConfirmPressed(PlayerIndex) || Game.Input.IsKeyPressed(Keys.Enter))
                    {
                        Game.HudManager.ToggleProfileActive(PlayerIndex);
                        State = EState.Locked;
                    }
                    break;

                case EState.Locked:
                    // NOTE: no more disabling mid-game
                    //if (Game.Input.IsPadStartPressed(PlayerIndex) || Game.Input.IsKeyPressed(StartKey))
                    //{
                        //Game.HudManager.ToggleProfileActive(PlayerIndex);
                        //State = EState.Inactive;
                    //}
                    break;
            }
        }

        public void Draw(SpriteBatch sprite_batch)
        {
            switch (State)
            {
                case EState.Disabled:
                    break;

                case EState.Inactive:
                    float t = (float)Math.Sin(Game.GameFrame * 0.15f);
                    float c = 0.7f + t * 0.3f;
                    Color color = new Color(c, c, c);
                    sprite_batch.DrawString(Game.DefaultFont, "Press Start", CMenu.CenteredText(Game, NameTextPosition, new Vector2(256.0f, 750.0f), "Press Start"), color, 0.0f, Vector2.Zero, 1.25f, SpriteEffects.None, CLayers.UI + CLayers.SubLayerIncrement * 2.0f);
                    break;

                case EState.Active:
                    // TODO: selection
                    PortraitIconVisual.Draw(sprite_batch, PortraitIconPosition, 0.0f);
                    // TODO: score display
                    break;

                case EState.Locked:
                    break;
            }
        }

        public void DrawEditor(SpriteBatch sprite_batch)
        {
            sprite_batch.DrawString(Game.DefaultFont, String.Format("todo"), new Vector2(8.0f, 1020.0f), Color.White);
        }
    }
}
