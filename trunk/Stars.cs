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
        private CollisionAABB ScreenAABB { get; set; }

        public CStars(CWorld world, Texture2D texture, float scale, float speed)
        {
            World = world;
            Visual = new CVisual(texture, Color.White);
            Scale = scale;
            Speed = Vector2.UnitY * speed;

            Viewport viewport = world.Game.GraphicsDevice.Viewport;
            CollisionAABB box = new CollisionAABB(
                new Vector2(viewport.X, viewport.Y),
                new Vector2(viewport.Width, viewport.Height)
            );
            ScreenAABB = box;

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
                if (!ScreenAABB.Contains(star.Position))
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
            return new Vector2(
                World.Random.NextFloat() * ScreenAABB.Size.X,
                World.Random.NextFloat() * ScreenAABB.Size.Y
            );
        }

        private Vector2 RandomRespawnPosition()
        {
            return new Vector2(
                World.Random.NextFloat() * ScreenAABB.Size.X,
                World.Random.NextFloat() * -100.0f
            );
        }
    }
}

