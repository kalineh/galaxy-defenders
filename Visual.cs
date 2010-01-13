//
// Visual.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace galaxy
{
    public class CVisual
    {
        public int TileX { get; set; }
        public int TileY { get; set; }
        public int Frame { get { return (int)FrameCounter; } set { FrameCounter = (float)value; } }
        private float FrameCounter { get; set; }
        public float AnimationSpeed { get; set; }
        public Texture2D Texture { get; set; }
        public Vector2 Scale { get; set; }
        public Color Color { get; set; }
        public float Alpha { get; set; }
        public float Depth { get; set; }

        public CVisual(Texture2D texture, Color color)
        {
            TileX = 1;
            TileY = 1;
            FrameCounter = 0.0f;
            AnimationSpeed = 1.0f;
            Scale = Vector2.One;
            Texture = texture;
            Color = color;
            Alpha = 1.0f;
            Depth = 1.0f;
        }

        public void Update()
        {
            UpdateAnimation();
        }

        public void Draw(SpriteBatch sprite_batch, Vector2 position, float rotation)
        {
            //Draw(Texture2D texture, Rectangle destinationRectangle, Color color);
            //Draw(Texture2D texture, Vector2 position, Color color);
            //Draw(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color);
            //Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color);
            //Draw(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, SpriteEffects effects, float layerDepth);
            //Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth);
            //Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth);

            float tx = Texture.Width * Scale.X;
            float ty = Texture.Height * Scale.Y;
            Rectangle source = GetFrameSourceRect(Frame);
            Vector2 origin = new Vector2(source.Width / 2.0f, source.Height / 2.0f);
            Color color = Color;
            color.A = (byte)(Alpha * 255.0f);
            sprite_batch.Draw(Texture, position, source, color, rotation, origin, Scale, SpriteEffects.None, Depth);
        }

        private void UpdateAnimation()
        {
            FrameCounter += AnimationSpeed;
        }

        private Rectangle GetFrameSourceRect(int frame)
        {
            int total_frames = TileY * TileX;
            int frame_wrapped = frame % total_frames;
            int x = frame_wrapped % TileX;
            int y = (int)((float)frame_wrapped / (float)TileX);

            return new Rectangle(
                (int)(x * Texture.Width / TileX),
                (int)(y * Texture.Height / TileY),
                (int)(Texture.Width / TileX),
                (int)(Texture.Height / TileY)
            );
        }

        public Vector2 GetCentered(Vector2 position)
        {
            float tx = Texture.Width * Scale.X;
            float ty = Texture.Height * Scale.Y;
            return new Vector2(position.X, position.Y) - GetScaledTextureSize() / 2.0f;
        }

        public Vector2 GetScaledTextureSize()
        {
            float tx = Texture.Width * Scale.X;
            float ty = Texture.Height * Scale.Y;
            return new Vector2(tx, ty);

        }
    }
}
