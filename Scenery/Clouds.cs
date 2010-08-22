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
            public Cloud(Vector2 position, Vector2 velocity, Vector2 lerp_velocity)
            {
                Position = position;
                Velocity = velocity;
                LerpVelocity = lerp_velocity;
            }

            public Vector2 Position { get; set; }
            public Vector2 Velocity { get; set; }
            public Vector2 LerpVelocity { get; set; }
        }

        public CVisual Visual { get; set; }
        public float Scale { get; set; }
        public Vector2 Velocity { get; set; }
        public float RandomLerp { get; set; }
        private List<Cloud> Clouds { get; set; }
        private int Count { get; set; }
        private int BaseCount { get; set; }

        public CClouds(CWorld world, Texture2D texture, float scale, Vector2 velocity)
            : this(world, texture, scale, velocity, 0.0f, 10)
        {
        }

        public CClouds(CWorld world, Texture2D texture, float scale, Vector2 velocity, float random_lerp, int base_count)
            : base(world)
        {
            Visual = new CVisual(world, texture, Color.White);
            Scale = scale;
            Velocity = velocity;
            RandomLerp = random_lerp;
            Clouds = new List<Cloud>();
            BaseCount = base_count;
            UpdateCloudCount();
        }

        public void UpdateCloudCount()
        {
            Count = (int)((float)BaseCount * 1.0f / World.GameCamera.Zoom);
            for (int i = 0; i < Count - Clouds.Count; ++i)
            {
                Cloud cloud = new Cloud(
                    RandomScreenPosition(),
                    Velocity,
                    RandomLerpVelocity()
                );
                Clouds.Add(cloud);
            }
        }

        public override void Update()
        {
            UpdateCloudCount();

            foreach (Cloud cloud in Clouds)
            {
                if (World.GameCamera.IsOffBottom(cloud.Position, 1.0f))
                {
                    cloud.Position = RandomRespawnPosition();
                    cloud.Velocity = Velocity;
                    cloud.LerpVelocity = RandomLerpVelocity();
                }

                float speed_scale = 1.0f - Math.Abs(cloud.Position.X) * 0.001f;
                cloud.Position += cloud.Velocity * speed_scale;
                cloud.Velocity = cloud.Velocity * 0.999f + cloud.LerpVelocity * 0.001f;
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

        private Vector2 RandomLerpVelocity()
        {
            return Velocity.Rotate((World.Random.NextAngle() - MathHelper.PiOver2) * RandomLerp);
        }
    }
}

