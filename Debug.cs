//
// Debug.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CDebug
    {
        public CGalaxy Game { get; private set; }
        public SpriteFont Font { get; private set; }

        public CDebug(CGalaxy game)
        {
            Game = game;
        }

        public void DrawString(Vector2 position, string text)
        {
            Game.SpriteBatch.DrawString(Game.DefaultFont, text, position, Color.White); 
        }

        public void DrawDisk(Vector2 position, float radius)
        {
            Vector2 center = position - new Vector2(radius);
            Vector2 scale = new Vector2(radius * 2.0f);
            Game.SpriteBatch.Draw(Game.PixelTexture, center, null, Color.White, 0.0f, Vector2.Zero, scale, SpriteEffects.None, 0.0f);
        }

        public void DrawVector(Vector2 position, Vector2 vector, float thickness)
        {
            // TODO: arrow part
            float angle = vector.ToAngle();
            Vector2 draw_vector = new Vector2(vector.Length(), thickness);
            Game.SpriteBatch.Draw(Game.PixelTexture, position, null, Color.White, angle, Vector2.Zero, draw_vector, SpriteEffects.None, 0.0f);
        }
    }
}
