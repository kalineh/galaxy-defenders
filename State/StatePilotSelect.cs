﻿//
// StatePilotSelect.cs
//

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Galaxy
{
    public class CStatePilotSelect
        : CState
    {
        public CGalaxy Game { get; set; }
        public Texture2D TitleTexture { get; set; }
        private CWorld EmptyWorld { get; set; }
        public CMenu Menu { get; set; }
        public CSampleShipManager SampleShipManager { get; set; }

        public CStatePilotSelect(CGalaxy game)
        {
            Game = game;
            TitleTexture = CContent.LoadTexture2D(Game, "Textures/UI/Title");
            EmptyWorld = new CWorld(game, null);
            SampleShipManager = new CSampleShipManager(EmptyWorld);

            EmptyWorld.BackgroundScenery = CSceneryPresets.BlueSky(EmptyWorld);
            EmptyWorld.ForegroundScenery = CSceneryPresets.Empty(EmptyWorld);

            if (!Game.EditorMode)
                CAudio.PlayMusic("Konami's_Moon_Base");
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Game.HudManager.ActivatePilotSelect();
        }

        public override void Update()
        {
            if (Game.HudManager.IsPilotSelectCompleteAll())
            {
                StartGame(null);
                //Game.State = new CStateFadeTo(Game, this, new CStateShop(Game));
                return;
            }
            else
            {
                if (Game.Input.IsPadBackPressedAny() || Game.Input.IsKeyPressed(Keys.Escape))
                {
                    Back(null);
                    return;
                }
            }

            SampleShipManager.Update();

            EmptyWorld.GameCamera.Update();
            EmptyWorld.UpdateEntitiesSingleThreadCollision();
            EmptyWorld.BackgroundScenery.Update();
            EmptyWorld.ForegroundScenery.Update();
            EmptyWorld.GameCamera.Position = new Vector3(0.0f, 0.0f, 0.0f);

            EmptyWorld.ParticleEffects.Update();
        }

        public override void Draw()
        {
            EmptyWorld.DrawBackground(EmptyWorld.GameCamera);

            SampleShipManager.Draw();

            EmptyWorld.DrawEntities(EmptyWorld.GameCamera);

            Game.DefaultSpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Game.RenderScaleMatrix);
            Game.DefaultSpriteBatch.Draw(TitleTexture, new Vector2(Game.Resolution.X / 2.0f - 256.0f, 120.0f), Color.White);
            Game.DefaultSpriteBatch.End();

            Game.DefaultSpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, EmptyWorld.GameCamera.WorldMatrix);
            EmptyWorld.ParticleEffects.Draw(Game.DefaultSpriteBatch);
            Game.DefaultSpriteBatch.End();
        }

        private void StartGame(object tag)
        {
            //Game.State = new CStateFadeTo(Game, this, new CStateShop(Game));

            // NOTE: now we just jump into stage 1, let the player play before giving them shop
            CStageDefinition definition = CStageDefinition.GetStageDefinitionByName("Stage1");
            Game.State = new CStateFadeTo(Game, this, new CStateGame(Game, definition));
        }

        private void Back(object tag)
        {
            Game.HudManager.DeactivatePilotSelect();
            Game.State = new CStateFadeTo(Game, this, new CStateMainMenu(Game));
        }
    }
}
