//
// Visual.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace Galaxy
{
    public class CVisual
    {
        public static Dictionary<string, CVisual> VisualCache { get; set; }

        static CVisual()
        {
            VisualCache = new Dictionary<string, CVisual>();
        }

        public static CVisual MakeLabel(CWorld world, string text)
        {
            return MakeLabel(world, text, Color.Blue);
        }

        public static CVisual MakeLabel(CWorld world, Vector2 size, Color color)
        {
            return new CVisual(world, CContent.LoadTexture2D(world.Game, "Textures/Top/Pixel"), color) { Scale = size };
        }

        public static CVisual MakeLabel(CWorld world, string text, Color color)
        {
            CVisual result = new CVisual(world, CContent.LoadTexture2D(world.Game, "Textures/Top/Pixel"), color);
            Vector2 size = result.GetDebugFont().MeasureString(text);
            result.Scale = size + Vector2.One * 4.0f; ;
            result.DebugText = text;
            return result;
        }

        public static CVisual MakeSpriteCached(CWorld world, string texture_name)
        {
            CVisual result;
            if (!VisualCache.TryGetValue(texture_name, out result))

            {
                result = MakeSpriteUncached(world, texture_name, Vector2.One, Color.White);
                VisualCache[texture_name] = result;
            }

            return result;
        }

        public static CVisual MakeSpriteUncached(CWorld world, string texture_name)
        {
            return MakeSpriteUncached(world, texture_name, Vector2.One, Color.White);
        }

        public static CVisual MakeSpriteUncached(CWorld world, string texture_name, Vector2 size, Color color)
        {
            try
            {
                return new CVisual(world, CContent.LoadTexture2D(world.Game, texture_name), color) { Scale = size };
            }
            catch
            {
                return null;
            }
        }

        private static SpriteFont DebugFont { get; set; }

        public CWorld World { get; set; }
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
        public string DebugText { get; set; }
        public SpriteEffects SpriteEffects { get; set; }
        public Vector2? NormalizedOrigin { get; set; }

        public CVisual(CWorld world, Texture2D texture, Color color)
        {
            World = world;
            TileX = 1;
            TileY = 1;
            FrameCounter = 0.0f;
            AnimationSpeed = 1.0f;
            Scale = Vector2.One;
            Texture = texture;
            Color = color;
            Alpha = 1.0f;
            Depth = CLayers.CalculateDepth(Texture);
            SpriteEffects = SpriteEffects.None;
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
            Vector2 origin = NormalizedOrigin != null
                ? (Vector2)NormalizedOrigin * new Vector2(source.Width, source.Height) 
                : new Vector2(source.Width / 2.0f, source.Height / 2.0f);
            Color color = Color;
            color.A = (byte)(Alpha * 255.0f);
            sprite_batch.Draw(Texture, position, source, color, rotation, origin, Scale, SpriteEffects, Depth);

            if (DebugText != null)
            {
                Vector2 size = GetDebugFont().MeasureString(DebugText);
                Vector2 center = position - new Vector2(source.Center.X, source.Center.Y) * 0.5f - size * 0.5f;
                Color text_color = Color.White;

                // TODO: dont waste time on stuff like this (also endianness bad)
                if (color.R > 200 && color.G > 200 && color.B > 200)
                {
                    uint packed = color.PackedValue;
                    uint invert = ~packed;
                    byte r = (byte)((packed & 0xFF000000) >> 24);
                    byte g = (byte)((packed & 0x00FF0000) >> 16);
                    byte b = (byte)((packed & 0x0000FF00) >>  8);
                    text_color = new Color(r, g, b);
                }

                sprite_batch.DrawString(GetDebugFont(), DebugText, center, text_color, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
            }
        }

        public SpriteFont GetDebugFont()
        {
            DebugFont = DebugFont ?? World.Game.Content.Load<SpriteFont>("Fonts/DefaultFont");
            return DebugFont;
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
