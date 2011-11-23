//
// StateGameFinish.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CStateGameFinish
        : CState
    {
        public CGalaxy Game { get; private set; }
        public CWorld EmptyWorld { get; set; }
        public int FrameCount { get; set; }
        public string[] Text { get; set; }
        
        public CStateGameFinish(CGalaxy game)
        {
            Game = game;

            EmptyWorld = new CWorld(game, null);

            EmptyWorld.BackgroundScenery = new CSceneryChain(EmptyWorld,
                new CBackground(EmptyWorld, new Color(0, 0, 0)),
                new CStars(EmptyWorld, CContent.LoadTexture2D(Game, "Textures/Background/Star"), 0.4f, 3.5f),
                new CStars(EmptyWorld, CContent.LoadTexture2D(Game, "Textures/Background/Star"), 0.6f, 2.1f)
            );
            EmptyWorld.ForegroundScenery = CSceneryPresets.Empty(EmptyWorld);
            EmptyWorld.GameCamera.Position = new Vector3(0.0f, 0.0f, 0.0f);

            Text = new string[] {
                "",
                "",
                "AS THE EVIL KURGLON EMPIRE",
                "RECEIVED DEFEAT",
                "",
                "",
                "OUR VALLIANT HERO COULD",
                "ENJOY TRIUMPH FROM THE",
                "PLANET POPULATION",
                "",
                "",
                "THE TASTE OF FREEDOM AND PEACE",
                "COULD IT LAST FOREVER?",
                "",
                "",
                "THE TALE OF YOUR TREMENDOUS",
                "VICTORY OVER THE KURGLON EMPIRE",
                "SHALL BE PASSED DOWN FROM",
                "GENERATION TO GENERATION",
                "",
                "",
                "AS THE HERO WHO BROUGHT PEACE",
                "PEACE TO FANTORA-K",
                "",
                "",
                "EVEN THOUGH THE WAR HAS BEEN OVER",
                "THE SCAR OF DECIMATED POPULATION",
                "IS REMEMBERED",
                "",
                "",
                "ONLY CONTINUED PEACE WILL",
                "ADVENGE IT",
                "",
                "",
                "UNTIL A TRUE HERO CAN PERFORM.....",
            };
        }

        public override void OnEnter()
        {
            base.OnEnter();
            CAudio.PlayMusic("fading-memories");
        }

        public override void OnExit()
        {
            base.OnExit();
            EmptyWorld.Stop();
        }

        public override void Update()
        {
            EmptyWorld.GameCamera.Update();
            EmptyWorld.UpdateEntitiesSingleThreadCollision();
            EmptyWorld.ProcessEntityAdd();
            EmptyWorld.ProcessEntityDelete();
            EmptyWorld.BackgroundScenery.Update();
            EmptyWorld.ForegroundScenery.Update();
            EmptyWorld.ParticleEffects.Update();

#if DEBUG
            if (Game.Input.IsPadStartPressedAny() || Game.Input.IsKeyPressed(Keys.Escape))
            {
                Game.State = new CStateFadeTo(Game, Game.State, new CStateMainMenu(Game));
            }
#endif
            if (FrameCount > 3200)
            {
                if (Game.Input.IsPadStartPressedAny() || Game.Input.IsKeyPressed(Keys.Escape))
                {
                    Game.State = new CStateFadeTo(Game, Game.State, new CStateMainMenu(Game));
                }
            }
            FrameCount++;
        }

        public override void Draw()
        {
            EmptyWorld.DrawBackground(EmptyWorld.GameCamera);
            EmptyWorld.DrawEntities(EmptyWorld.GameCamera);

            Game.DefaultSpriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Game.RenderScaleMatrix);

            // draw text
            const float speed = 0.70f;
            const float spacing = 50.0f;
            Vector2 base_position = new Vector2(Game.Resolution.X * 0.5f, 1080.0f - FrameCount * speed);
            for (int i = 0; i < Text.Length; ++i)
            {
                Game.DefaultSpriteBatch.DrawStringAlignCenter(
                    Game.GameLargeFont,
                    base_position + Vector2.UnitY * i * spacing,
                    Text[i],
                    Color.White
                );
            }

            Game.DefaultSpriteBatch.End();

            Game.DefaultSpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, EmptyWorld.GameCamera.WorldMatrix);
            EmptyWorld.ParticleEffects.Draw(Game.DefaultSpriteBatch);
            Game.DefaultSpriteBatch.End();
        }

        public override void PostHudDraw()
        {
            Game.DefaultSpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Game.RenderScaleMatrix);

            // draw

            Game.DefaultSpriteBatch.End();
        }
    }
}
