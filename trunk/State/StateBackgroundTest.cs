//
// StateBackgroundTest.cs
//

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Galaxy
{
    public class CStateBackgroundTest
        : CState
    {
        public CGalaxy Game { get; set; }
        private CWorld EmptyWorld { get; set; }
        private List<CScenery> BackgroundSceneries { get; set; }
        private int BackgroundIndex { get; set; }

        public CStateBackgroundTest(CGalaxy game)
        {
            Game = game;
            EmptyWorld = new CWorld(game, null);

            BackgroundSceneries = new List<CScenery>();
            BackgroundSceneries.Add(new CFunkyClouds(EmptyWorld));
            BackgroundSceneries.Add(new CBackground(EmptyWorld, Color.Black));

            //EmptyWorld.BackgroundScenery = CSceneryPresets.BlueSky(EmptyWorld);
            EmptyWorld.BackgroundScenery = new CSceneryChain(EmptyWorld, new CFunkyClouds(EmptyWorld));
            EmptyWorld.ForegroundScenery = CSceneryPresets.Empty(EmptyWorld);

            RefreshScenery();

            CAudio.PlayMusic("Eye_of_the_Predator");
            //CAudio.PlayMusic("Konami's_Moon_Base");
        }

        public override void Update()
        {
            Game.HudManager.Hidden = true;

            EmptyWorld.GameCamera.Update();
            EmptyWorld.UpdateEntitiesSingleThreadCollision();
            EmptyWorld.BackgroundScenery.Update();
            EmptyWorld.ForegroundScenery.Update();
            EmptyWorld.GameCamera.Position = new Vector3(0.0f, 0.0f, 0.0f);

            EmptyWorld.ParticleEffects.Update();

            if (CInput.IsRawKeyPressed(Keys.Left))
                BackgroundIndex--;
            if (CInput.IsRawKeyPressed(Keys.Right))
                BackgroundIndex++;
        }

        public override void Draw()
        {
            EmptyWorld.UpdateScissorRectangle();
            EmptyWorld.DrawBackground(EmptyWorld.GameCamera);

            EmptyWorld.DrawEntities(EmptyWorld.GameCamera);

            Game.DefaultSpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, EmptyWorld.GameCamera.WorldMatrix);
            EmptyWorld.ParticleEffects.Draw(Game.DefaultSpriteBatch);
            Game.DefaultSpriteBatch.End();

            Game.DefaultSpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Game.RenderScaleMatrix);

            //Game.DefaultSpriteBatch.DrawString(Game.GameRegularFont, "BACKGROUND TEST", new Vector2(Game.Resolution.X / 2.0f - 20.0f, 650.0f), Color.White, 0.0f, new Vector2(60.0f, 10.0f), 1.5f + (float)Math.Sin(Game.GameFrame * 0.05f) * 0.05f, SpriteEffects.None, 0.0f);

            Game.DefaultSpriteBatch.End();
        }

        public void RefreshScenery()
        {
            EmptyWorld.BackgroundScenery = new CSceneryChain(EmptyWorld, BackgroundSceneries[BackgroundIndex % BackgroundSceneries.Count]);
        }
    }
}
