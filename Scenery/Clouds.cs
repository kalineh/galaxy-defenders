//
// Clouds.cs
//

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CClouds
        : CScenery
    {
        private class Cloud
        {
            public Cloud(Vector2 position)
            {
                Position = position;
            }

            public Vector2 Position { get; set; }
        }

        public CVisual Visual { get; set; }
        public float Scale { get; set; }
        public Vector2 Speed { get; set; }
        private List<Cloud> Clouds { get; set; }
        private int Count { get; set; }

        public CClouds(CWorld world, Texture2D texture, float scale, float speed)
            : base(world)
        {
            Visual = new CVisual(world, texture, Color.White);
            Scale = scale;
            Speed = Vector2.UnitY * speed;
            Clouds = new List<Cloud>();
            UpdateCloudCount();
        }

        public void UpdateCloudCount()
        {
            Count = (int)(10.0f * 1.0f / World.GameCamera.Zoom);
            for (int i = 0; i < Count - Clouds.Count; ++i)
            {
                Cloud cloud = new Cloud(RandomScreenPosition());
                Clouds.Add(cloud);
            }
        }

        public override void Update()
        {
            //Speed *= new Vector2(0.9f, 1.0f);

            UpdateCloudCount();

            foreach (Cloud cloud in Clouds)
            {
                if (World.GameCamera.IsOffBottom(cloud.Position, 1.0f))
                {
                    cloud.Position = RandomRespawnPosition();
                }

                float speed_scale = 1.0f - Math.Abs(cloud.Position.X) * 0.001f;
                cloud.Position += Speed * speed_scale;
            }
        }

        public override void Draw(SpriteBatch sprite_batch)
        {
            foreach (Cloud cloud in Clouds)
            {
                Visual.Draw(sprite_batch, cloud.Position, 0.0f);
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

