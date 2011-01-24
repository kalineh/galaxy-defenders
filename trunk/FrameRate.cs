//
// FrameRate.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CFrameRateDisplay
    {
        public CGalaxy Game { get; set; }
        public int FrameRate { get; set; }
        public int FrameCounter { get; set; }
        public TimeSpan ElapsedTime { get; set; }
        private string CachedFrameRateString { get; set; }
        private Vector2 DrawPosition { get; set; }

        public CFrameRateDisplay(CGalaxy game)
        {
            Game = game;
            FrameRate = 60;
            FrameCounter = 0;
            CachedFrameRateString = "fps: 60";
            ElapsedTime = TimeSpan.Zero;
            DrawPosition = new Vector2(Game.GraphicsDevice.Viewport.TitleSafeArea.Right - Game.GameRegularFont.MeasureString(CachedFrameRateString).X * 1.2f, 15.0f);

        }

        public void Update(GameTime game_time)
        {
            ElapsedTime += game_time.ElapsedGameTime;

            if (ElapsedTime > TimeSpan.FromSeconds(1.0))
            {
                ElapsedTime -= TimeSpan.FromSeconds(1.0);
                if (FrameRate != FrameCounter)
                {
                    FrameRate= FrameCounter;
                    CachedFrameRateString = string.Format("fps: {0}", FrameRate);
                }
                FrameCounter = 0;
            }
        }

        public void Draw(SpriteBatch sprite_batch)
        {
            FrameCounter += 1;

            sprite_batch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None, Game.RenderScaleMatrix);

            Vector2 position = new Vector2(Game.GraphicsDevice.Viewport.TitleSafeArea.Right - Game.GameRegularFont.MeasureString(CachedFrameRateString).X * 1.2f, 15.0f);
            sprite_batch.DrawString(Game.GameRegularFont, CachedFrameRateString, position, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
            
            sprite_batch.End();
        }
    }
}
