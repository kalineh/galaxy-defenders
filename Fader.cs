//
// Fader.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CFader
    {
        public CGalaxy Game { get; private set; }
        public Texture2D Texture { get; private set; }
        public float TransitionTime { get; private set; }
        private float CurrentTime { get; set; }
        private float CurrentAlpha { get; set; }

        public CFader(CGalaxy game)
        {
            Game = game;
            Texture = CContent.LoadTexture2D(Game, "Textures/Top/Pixel");
            TransitionTime = 0.75f;
            CurrentTime = 0.0f;
            CurrentAlpha = -1.0f;
        }

        public void Update()
        {
            CurrentAlpha = MathHelper.Lerp(-1.0f, 1.0f, CurrentTime / TransitionTime);
            CurrentAlpha = Math.Min(CurrentAlpha, 1.0f);

            CurrentTime += Time.SingleFrame;
            CurrentTime = Math.Min(CurrentTime, TransitionTime);
        }

        public void Draw(SpriteBatch sprite_batch)
        {
            Color color = Color.Black;
            int alpha = byte.MaxValue - (byte)(Math.Abs(CurrentAlpha) * 255.0f);
            color.A = (byte)alpha;
            Rectangle destination = Game.GraphicsDevice.Viewport.TitleSafeArea;
            sprite_batch.Draw(Texture, destination, color);
        }

        public bool IsFadeIn()
        {
            return CurrentAlpha < 0.0f;
        }

        public bool IsFadeOut()
        {
            return CurrentAlpha >= 0.0f;
        }

        public bool IsComplete()
        {
            return CurrentAlpha >= 1.0f;
        }
    }
}

