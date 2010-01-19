//
// Bonus.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace Galaxy
{
    public class CFrameRateDisplay
    {
        public CGalaxy Game { get; set; }
        public int FrameRate { get; set; }
        public int FrameCounter { get; set; }
        public TimeSpan ElapsedTime { get; set; }

        public CFrameRateDisplay(CGalaxy game)
        {
            Game = game;
            FrameRate = 60;
            FrameCounter = 0;
            ElapsedTime = TimeSpan.Zero;
        }

        public void Update(GameTime game_time)
        {
            ElapsedTime += game_time.ElapsedGameTime;

            if (ElapsedTime > TimeSpan.FromSeconds(1.0))
            {
                ElapsedTime -= TimeSpan.FromSeconds(1.0);
                FrameRate = FrameCounter;
                FrameCounter = 0;
            }
        }

        public void Draw(SpriteBatch sprite_batch)
        {
            FrameCounter += 1;

            string fps = string.Format("fps: {0}", FrameRate);

            sprite_batch.Begin();

            Vector2 position = new Vector2(Game.GraphicsDevice.Viewport.TitleSafeArea.Right - Game.DefaultFont.MeasureString(fps).X * 1.2f, 15.0f);
            sprite_batch.DrawString(Game.DefaultFont, fps, position, Color.White);
            
            sprite_batch.End();
        }
    }
}
