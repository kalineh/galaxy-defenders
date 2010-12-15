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
        public static Dictionary<string, CVisual> VisualCache1 { get; set; }
        public static Dictionary<string, CVisual> VisualCache2 { get; set; }

        static CVisual()
        {
            VisualCache1 = new Dictionary<string, CVisual>();
            VisualCache2 = new Dictionary<string, CVisual>();
        }

        public static CVisual MakeLabel(CGalaxy game, string text)
        {
            return MakeLabel(game, text, Color.Blue);
        }

        public static CVisual MakeLabel(CGalaxy game, Vector2 size, Color color)
        {
            return new CVisual(game, CContent.LoadTexture2D(game, "Textures/Top/Pixel"), color) { Scale = size };
        }

        public static CVisual MakeLabel(CGalaxy game, string text, Color color)
        {
            CVisual result = new CVisual(game, CContent.LoadTexture2D(game, "Textures/Top/Pixel"), color);
            Vector2 size = result.GetDebugFont().MeasureString(text);
            result.Scale = size + Vector2.One * 4.0f; ;
            result.DebugText = text;
            result.Depth = CLayers.Top - CLayers.SubLayerIncrement;
            return result;
        }

        public static CVisual MakeSpriteCachedForPlayer(CGalaxy game, string texture_name, PlayerIndex index)
        {
            Dictionary<string, CVisual> cache = index == 0 ? VisualCache1 : VisualCache2;

            CVisual result;
            if (!cache.TryGetValue(texture_name, out result))
            {
                result = MakeSpriteUncached(game, texture_name, Vector2.One, Color.White);
                cache[texture_name] = result;
            }

            return result;
        }

        public static CVisual MakeSpriteCached1(CGalaxy game, string texture_name)
        {
            CVisual result;
            if (!VisualCache1.TryGetValue(texture_name, out result))
            {
                result = MakeSpriteUncached(game, texture_name, Vector2.One, Color.White);
                VisualCache1[texture_name] = result;
            }

            return result;
        }

        public static CVisual MakeSpriteCached2(CGalaxy game, string texture_name)
        {
            CVisual result;
            if (!VisualCache2.TryGetValue(texture_name, out result))
            {
                result = MakeSpriteUncached(game, texture_name, Vector2.One, Color.White);
                VisualCache2[texture_name] = result;
            }

            return result;
        }

        public static CVisual MakeSpriteUncached(CGalaxy game, string texture_name)
        {
            return MakeSpriteUncached(game, texture_name, Vector2.One, Color.White);
        }

        public static CVisual MakeSpriteUncached(CGalaxy game, string texture_name, Vector2 size, Color color)
        {
            try
            {
                return new CVisual(game, CContent.LoadTexture2D(game, texture_name), color) { Scale = size };
            }
            catch
            {
                return null;
            }
        }

        public static CVisual MakeSpriteFromGame(CGalaxy game, string texture_name, Vector2 size, Color color)
        {
            return new CVisual(null, CContent.LoadTexture2D(game, texture_name), color) { Scale = size };
        }

        private static SpriteFont DebugFont { get; set; }

        public CGalaxy Game { get; set; }

        private bool _Dirty;
        private int _TileX;
        private int _TileY;
        private Texture2D _Texture;
        private Vector2 _Scale;
        private Color _Color;
        private float _Alpha;
        private float _Depth;
        private string _DebugText;
        private bool _DebugTextClampToScreen;
        private SpriteEffects _SpriteEffects;
        private Vector2? _NormalizedOrigin;

        public int TileX
        {
            get { return _TileX; }
            set { _TileX = value; SetDirty(true); }
        }

        public int TileY
        {
            get { return _TileY; }
            set { _TileY = value; SetDirty(true); }
        }

        public int Frame
        {
            get { return (int)FrameCounter; }
            set { FrameCounter = (float)value; }
        }

        private float FrameCounter { get; set; }
        public float AnimationSpeed { get; set; }

        public Texture2D Texture
        {
            get { return _Texture; }
            set { _Texture = value; SetDirty(true); }
        }

        public Vector2 Scale
        { 
            get { return _Scale; }
            set { _Scale = value; SetDirty(true); }
        }

        public Color Color
        { 
            get { return _Color; }
            set { _Color = value; SetDirty(true); }
        }
        
        public float Alpha
        { 
            get { return _Alpha; }
            // NOTE: special case so we can fade out objects without causing a whole recache
            set { _Alpha = value; _Color.A = (byte)(value * 255); _CacheColor.A = (byte)(value * 255); }
        }

        public float Depth
        { 
            get { return _Depth; }
            set { _Depth = value; SetDirty(true); }
        }

        public string DebugText
        { 
            get { return _DebugText; }
            set { _DebugText = value; SetDirty(true); }
        }

        public bool DebugTextClampToScreen
        { 
            get { return _DebugTextClampToScreen; }
            set { _DebugTextClampToScreen = value; SetDirty(true); }
        }

        public SpriteEffects SpriteEffects
        { 
            get { return _SpriteEffects; }
            set { _SpriteEffects = value; SetDirty(true); }
        }

        public Vector2? NormalizedOrigin
        {
            get { return _NormalizedOrigin; }
            set { _NormalizedOrigin = value; SetDirty(true); }
        }

        // cached values
        private float _CacheTextureWidthScaled;
        private float _CacheTextureHeightScaled;
        private Rectangle _CacheFrameSourceRect;
        public Vector2 _CacheOrigin;
        private Color _CacheColor;

        public CVisual(CGalaxy game, Texture2D texture, Color color)
        {
            Game = game;
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

            Recache();
        }

        public void SetDirty(bool dirty)
        {
            _Dirty = dirty;
        }

        public void Recache()
        {
            _CacheTextureWidthScaled = Texture.Width * Scale.X;
            _CacheTextureHeightScaled = Texture.Height * Scale.Y;
            _CacheFrameSourceRect = GetFrameSourceRect(Frame);
            _CacheOrigin = NormalizedOrigin != null
                ? (Vector2)NormalizedOrigin * new Vector2(_CacheFrameSourceRect.Width, _CacheFrameSourceRect.Height) 
                : new Vector2(_CacheFrameSourceRect.Width / 2.0f, _CacheFrameSourceRect.Height / 2.0f);
            _CacheColor = Color;

            //float tx = Texture.Width * Scale.X;
            //float ty = Texture.Height * Scale.Y;
            //Rectangle source = GetFrameSourceRect(Frame);
            //Vector2 origin = NormalizedOrigin != null
                //? (Vector2)NormalizedOrigin * new Vector2(source.Width, source.Height) 
                //: new Vector2(source.Width / 2.0f, source.Height / 2.0f);
            //Color color = Color;
            //color.A = (byte)(Alpha * 255.0f);
            //sprite_batch.Draw(Texture, position, source, color, rotation, origin, Scale, SpriteEffects, Depth);

            SetDirty(false);
        }

        public void Update()
        {
            if (_Dirty)
                Recache();

            UpdateAnimation();
        }

        public void UpdateColor()
        {
            _CacheColor = Color;
        }

        public void Draw(SpriteBatch sprite_batch, Vector2 position, float rotation)
        {
            sprite_batch.Draw(_Texture, position, _CacheFrameSourceRect, _CacheColor, rotation, _CacheOrigin, Scale, SpriteEffects.None, Depth);

#if DEBUG
            if (DebugText != null)
            {
                Vector2 size = GetDebugFont().MeasureString(DebugText);
                Vector2 center = position - new Vector2(_CacheFrameSourceRect.Center.X, _CacheFrameSourceRect.Center.Y) * 0.5f - size * 0.5f;

                if (DebugTextClampToScreen)
                {
                    Vector2 tl = Game.TryGetCameraTopLeft();
                    Vector2 br = Game.TryGetCameraBottomRight();
                    center = new Vector2(
                        MathHelper.Clamp(center.X, tl.X, br.X - size.X),
                        MathHelper.Clamp(center.Y, tl.Y, br.Y - size.Y * 2.0f)
                    );
                }

                Color text_color = Color.White;

                // TODO: dont waste time on stuff like this (also endianness bad)
                if (_CacheColor.R > 200 && _CacheColor.G > 200 && _CacheColor.B > 200)
                {
                    uint packed = _CacheColor.PackedValue;
                    uint invert = ~packed;
                    byte r = (byte)((packed & 0xFF000000) >> 24);
                    byte g = (byte)((packed & 0x00FF0000) >> 16);
                    byte b = (byte)((packed & 0x0000FF00) >>  8);
                    text_color = new Color(r, g, b);
                }

                sprite_batch.DrawString(GetDebugFont(), DebugText, center, text_color, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, CLayers.Top);
            }
#endif
        }

        public SpriteFont GetDebugFont()
        {
            DebugFont = DebugFont ?? Game.Content.Load<SpriteFont>("Fonts/DefaultFont");
            return DebugFont;
        }

        private void UpdateAnimation()
        {
            int pre_frame = Frame;

            FrameCounter += AnimationSpeed;

            int post_frame = Frame;
            if (pre_frame != post_frame)
            {
                _CacheFrameSourceRect = GetFrameSourceRect(post_frame);
            }
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
