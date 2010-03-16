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
            Visual = CVisual.MakeSprite(world, "Textures/Top/Pixel", World.GameCamera.ScreenSize, new Color(133, 145, 181));
        }

        public override void Update()
        {
        }

        public override void Draw(SpriteBatch sprite_batch)
        {
            Visual.Draw(sprite_batch, World.GameCamera.GetCenter().ToVector2(), 0.0f);
        }
    }
}
