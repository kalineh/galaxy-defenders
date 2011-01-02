//
// SpriteBatchExtensions.cs
// 

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public static class SpriteBatchExtensions
    {
        public static Vector2 DrawStringPixelOffset = new Vector2(0.5f, 0.5f);
        public static float TextLabelDepth = 1.0f;

        public static void DrawStringAlignLeft(this SpriteBatch self, SpriteFont font, Vector2 position, string text, Color color)
        {
            Vector2 adjusted = StringMeasurement.AlignLeft(font, position, text);
            self.DrawString(font, text, adjusted + DrawStringPixelOffset, color, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, TextLabelDepth);
        }

        public static void DrawStringAlignRight(this SpriteBatch self, SpriteFont font, Vector2 position, string text, Color color)
        {
            Vector2 adjusted = StringMeasurement.AlignRight(font, position, text);
            self.DrawString(font, text, adjusted + DrawStringPixelOffset, color, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, TextLabelDepth);
        }

        public static void DrawStringAlignCenter(this SpriteBatch self, SpriteFont font, Vector2 position, string text, Color color)
        {
            Vector2 adjusted = StringMeasurement.AlignCenter(font, position, text);
            self.DrawString(font, text, adjusted + DrawStringPixelOffset, color, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, TextLabelDepth);
        }

        public static void DrawStringAlignCenter(this SpriteBatch self, SpriteFont font, Vector2 position, string text, Color color, float scale)
        {
            Vector2 adjusted = StringMeasurement.AlignCenter(font, position, text, scale);
            self.DrawString(font, text, adjusted + DrawStringPixelOffset, color, 0.0f, Vector2.Zero, scale, SpriteEffects.None, TextLabelDepth);
        }
    }

    public static class StringMeasurement
    {
        public static Vector2 AlignLeft(SpriteFont font, Vector2 position, string text)
        {
            Vector2 measured = font.MeasureString(text);
            return new Vector2(
                position.X,
                position.Y - measured.Y * 0.5f
            );
        }

        public static Vector2 AlignRight(SpriteFont font, Vector2 position, string text)
        {
            Vector2 measured = font.MeasureString(text);
            return new Vector2(
                position.X - measured.X,
                position.Y - measured.Y * 0.5f
            );
        }

        public static Vector2 AlignCenter(SpriteFont font, Vector2 position, string text)
        {
            return AlignCenter(font, position, text, 1.0f);
        }

        public static Vector2 AlignCenter(SpriteFont font, Vector2 position, string text, float scale)
        {
            Vector2 measured = font.MeasureString(text);
            return new Vector2(
                position.X - measured.X * 0.5f * scale,
                position.Y - measured.Y * 0.5f * scale
            );
        }
    }
}