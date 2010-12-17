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
        public List<CHud> Huds { get; set; }
        public List<HudPilotSelect> HudsProfileSelect { get; set; }

        public CHudManager(CGalaxy game)
        {
            Game = game;
            Huds = new List<CHud>() {
                new CHud(Game, new Vector2(0.0f, Game.GraphicsDevice.Viewport.Height - 60.0f), GameControllerIndex.One),
                new CHud(Game, new Vector2(Game.GraphicsDevice.Viewport.Width - 480.0f, Game.GraphicsDevice.Viewport.Height - 60.0f), GameControllerIndex.Two),
            };
            HudsProfileSelect = new List<HudPilotSelect>() {
                new HudPilotSelect(Game, new Vector2(0.0f, Game.GraphicsDevice.Viewport.Height - 60.0f), GameControllerIndex.One),
                new HudPilotSelect(Game, new Vector2(Game.GraphicsDevice.Viewport.Width - 480.0f, Game.GraphicsDevice.Viewport.Height - 60.0f), GameControllerIndex.Two),
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

        public void ActivatePressStart()
        {
            foreach (HudPilotSelect hud in HudsProfileSelect)
                hud.PressStart();
        }

        public void LockHuds()
        {
            foreach (HudPilotSelect hud in HudsProfileSelect)
                hud.Deactivate();

            int players = Game.PlayersInGame;
            for (int i = 0; i < players; ++i)
                HudsProfileSelect[i].Lock();
        }

        public void ActivatePilotSelect()
        {
            foreach (HudPilotSelect hud in HudsProfileSelect)
                hud.Deactivate();

            int players = Game.PlayersInGame;
            for (int i = 0; i < players; ++i)
                HudsProfileSelect[i].Activate();
        }

        public void DeactivatePilotSelect()
        {
            foreach (HudPilotSelect hud in HudsProfileSelect)
                hud.Deactivate();
        }

        public bool IsPilotSelectComplete(int index)
        {
            if (HudsProfileSelect[index].State == HudPilotSelect.EState.Locked)
                return true;
            return false;
        }

        public bool IsPilotSelectCompleteAll()
        {
            int players = Game.PlayersInGame;
            for (int i = 0; i < players; ++i)
                if (!IsPilotSelectComplete(i))
                    return false;

            return true;
        }

        private void UpdateHuds()
        {
            foreach (CHud hud in Huds)
                hud.Update();
        }

        private void UpdateHudsProfileSelect()
        {
            foreach (HudPilotSelect hud in HudsProfileSelect)
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
                foreach (HudPilotSelect hud in HudsProfileSelect)
                    hud.DrawEditor(Game.DefaultSpriteBatch);
                Game.DefaultSpriteBatch.End();
                return;
            }

            Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.FrontToBack, SaveStateMode.None, Matrix.Identity);

            foreach (HudPilotSelect hud in HudsProfileSelect)
                hud.Draw(Game.DefaultSpriteBatch);

            Game.DefaultSpriteBatch.End();
        }

    }
}

