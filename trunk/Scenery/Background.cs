//
// Background.cs
//

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CBackground
        : CScenery
    {
        private CVisual Visual { get; set; }

        public CBackground(CWorld world, Color color)
            : base(world)
        {
            Visual = CVisual.MakeSprite(world, "Textures/Top/Pixel", World.GameCamera.ScreenSize, color);
        }

        public override void Update()
        {
        }

        public override void Draw(SpriteBatch sprite_batch)
        {
            Visual.Scale = World.GameCamera.ScreenSize * 1.0f / World.GameCamera.Zoom;
            Visual.Draw(sprite_batch, World.GameCamera.GetCenter().ToVector2(), 0.0f);
        }
    }

    public class CGradientBackground
        : CScenery
    {
        public Color ColorTL { get; set; }
        public Color ColorTR { get; set; }
        public Color ColorBR { get; set; }
        public Color ColorBL { get; set; }
        private PrimitivesSample.PrimitiveBatch PrimitiveBatch;

        public CGradientBackground(CWorld world, Color tl, Color tr, Color br, Color bl)
            : base(world)
        {
            ColorTL = tl;
            ColorTR = tr;
            ColorBR = br;
            ColorBL = bl;
        }
        public override void Update()
        {
            // TODO: color lerp
        }

        public override void Draw(SpriteBatch sprite_batch)
        {
            if (PrimitiveBatch == null)
                PrimitiveBatch = new PrimitivesSample.PrimitiveBatch(World.Game.GraphicsDevice);

            Vector2 position = World.GameCamera.GetCenter().ToVector2();
            Vector2 screen_size = World.GameCamera.ScreenSize * 1.0f / World.GameCamera.Zoom;
            Vector2 half = screen_size * 0.5f;
            Matrix transform = World.GameCamera.WorldMatrix;

            PrimitiveBatch.Begin(PrimitiveType.TriangleList);

            Vector2 half_x = Vector2.UnitX * screen_size.X;
            Vector2 half_y = Vector2.UnitY * screen_size.Y;

            PrimitiveBatch.AddVertex(Vector2.Transform(position - half_x + half_y, transform), ColorBL);
            PrimitiveBatch.AddVertex(Vector2.Transform(position - half_x - half_y, transform), ColorTL);
            PrimitiveBatch.AddVertex(Vector2.Transform(position + half_x + half_y, transform), ColorBR);

            PrimitiveBatch.AddVertex(Vector2.Transform(position - half_x - half_y, transform), ColorTL);
            PrimitiveBatch.AddVertex(Vector2.Transform(position + half_x - half_y, transform), ColorTR);
            PrimitiveBatch.AddVertex(Vector2.Transform(position + half_x + half_y, transform), ColorBR);

            PrimitiveBatch.End();
        }
    }

    public class CBlendingGradientBackground
        : CGradientBackground
    {
        public List<Color[]> BlendColors { get; set; }
        private float BlendTime { get; set; }
        private float BlendCounter { get; set; }
        private int BlendCycle { get; set; }

        public CBlendingGradientBackground(CWorld world, float blend_time, List<Color[]> blend_colors)
            : base(world, blend_colors[0][0], blend_colors[0][1], blend_colors[0][2], blend_colors[0][3])
        {
            BlendColors = blend_colors; 
            BlendTime = blend_time;
            BlendCounter = Time.ToFrames(blend_time);
            BlendCycle = 0;
        }

        public override void Update()
        {
            int previous = Math.Abs(BlendCycle - 1) % BlendColors.Count;
            float total = (float)Time.ToFrames(BlendTime);
            float current = (float)BlendCounter;
            float t = (total - current) / total;

            ColorTL = ColorExtensions.Smoothstep(BlendColors[previous][0], BlendColors[BlendCycle][0], t);
            ColorTR = ColorExtensions.Smoothstep(BlendColors[previous][1], BlendColors[BlendCycle][1], t);
            ColorBR = ColorExtensions.Smoothstep(BlendColors[previous][2], BlendColors[BlendCycle][2], t);
            ColorBL = ColorExtensions.Smoothstep(BlendColors[previous][3], BlendColors[BlendCycle][3], t);

            BlendCounter -= 1;
            if (BlendCounter <= 0)
            {
                BlendCycle += 1;
                BlendCycle %= BlendColors.Count;
                BlendCounter = Time.ToFrames(BlendTime);
            }
        }
    }
}
