//
// Stars.cs
//

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Galaxy
{
    class CStars
    {
        private class Star
        {
            public Star(Vector2 position)
            {
                Position = position;
            }

            public Vector2 Position { get; set; }
        }

        public CWorld World { get; set; }
        public CVisual Visual { get; set; }
        public float Scale { get; set; }
        public Vector2 Speed { get; set; }
        private List<Star> Stars { get; set; }

        public CStars(CWorld world, Texture2D texture, float scale, float speed)
        {
            World = world;
            Visual = new CVisual(texture, Color.White);
            Scale = scale;
            Speed = Vector2.UnitY * speed;

            Stars = new List<Star>();
            for (int i = 0; i < 16; ++i)
            {
                Star star = new Star(RandomScreenPosition());
                Stars.Add(star);
            }
        }

        public void Update()
        {
            //Speed *= new Vector2(0.9f, 1.0f);

            foreach (Star star in Stars)
            {
                if (World.GameCamera.IsOffBottom(star.Position, 1.0f))
                {
                    star.Position = RandomRespawnPosition();
                }

                star.Position += Speed;
            }
        }

        public void Draw(SpriteBatch sprite_batch)
        {
            foreach (Star star in Stars)
            {
                Visual.Draw(sprite_batch, star.Position, 0.0f);
            }
        }

        private Vector2 RandomScreenPosition()
        {
            Vector2 tl = World.GameCamera.GetTopLeft();
            Vector2 br = World.GameCamera.GetBottomRight();
            Vector2 range = br - tl;
            return tl + new Vector2(World.Random.NextFloat() * range.X, World.Random.NextFloat() * range.Y);
        }

        private Vector2 RandomRespawnPosition()
        {
            Vector2 tl = World.GameCamera.GetTopLeft();
            Vector2 br = World.GameCamera.GetBottomRight();
            Vector2 range = br - tl;
            return tl + new Vector2(World.Random.NextFloat() * range.X, -100.0f);
        }
    }
}

