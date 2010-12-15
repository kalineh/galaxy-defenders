//
// HudManager.cs
//

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CHudManager
    {
        public CGalaxy Game { get; private set; }

        public List<bool> Players { get; set; }
        public List<CHud> Huds { get; set; }
        public List<CHudProfileSelect> HudsProfileSelect { get; set; }

        public CHudManager(CGalaxy game)
        {
            Game = game;
            Players = new List<bool>() { false, false };
            Huds = new List<CHud>() {
                new CHud(Game, new Vector2(0.0f, Game.GraphicsDevice.Viewport.Height - 60.0f), PlayerIndex.One),
                new CHud(Game, new Vector2(Game.GraphicsDevice.Viewport.Width - 480.0f, Game.GraphicsDevice.Viewport.Height - 60.0f), PlayerIndex.Two),
            };
            HudsProfileSelect = new List<CHudProfileSelect>() {
                new CHudProfileSelect(Game, new Vector2(0.0f, Game.GraphicsDevice.Viewport.Height - 60.0f), PlayerIndex.One),
                new CHudProfileSelect(Game, new Vector2(Game.GraphicsDevice.Viewport.Width - 480.0f, Game.GraphicsDevice.Viewport.Height - 60.0f), PlayerIndex.Two),
            };
        }

        public void Update()
        {
            UpdateHuds();
            UpdateHudsProfileSelect();
        }

        public void Draw()
        {
            Game.GraphicsDevice.RenderState.ScissorTestEnable = false;
            DrawHuds();
            DrawHudsProfileSelect();
            Game.GraphicsDevice.RenderState.ScissorTestEnable = true;
        }

        public void ToggleProfileActive(PlayerIndex index)
        {
            Players[(int)index] = !Players[(int)index];
        }

        public int GetActivePlayerCount()
        {
            int count = 0;
            foreach (bool active in Players)
            {
                count += active ? 1 : 0; 
            }
            return count;
        }

        private void UpdateHuds()
        {
            foreach (CHud hud in Huds)
                hud.Update();
        }

        private void UpdateHudsProfileSelect()
        {
            foreach (CHudProfileSelect hud in HudsProfileSelect)
                hud.Update();
        }

        private void DrawHuds()
        {
            // NOTE: no side panels in editor mode!
            if (Game.EditorMode)
            {
                Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.FrontToBack, SaveStateMode.None, Matrix.Identity);
                foreach (CHud hud in Huds)
                    hud.DrawEditor(Game.DefaultSpriteBatch);
                Game.DefaultSpriteBatch.End();
                return;
            }

            Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.FrontToBack, SaveStateMode.None, Matrix.Identity);

            foreach (CHud hud in Huds)
                hud.Draw(Game.DefaultSpriteBatch);

            Game.DefaultSpriteBatch.End();
        }

        private void DrawHudsProfileSelect()
        {
            // NOTE: no side panels in editor mode!
            if (Game.EditorMode)
            {
                Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.FrontToBack, SaveStateMode.None, Matrix.Identity);
                foreach (CHudProfileSelect hud in HudsProfileSelect)
                    hud.DrawEditor(Game.DefaultSpriteBatch);
                Game.DefaultSpriteBatch.End();
                return;
            }

            Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.FrontToBack, SaveStateMode.None, Matrix.Identity);

            foreach (CHudProfileSelect hud in HudsProfileSelect)
                hud.Draw(Game.DefaultSpriteBatch);

            Game.DefaultSpriteBatch.End();
        }

    }
}

