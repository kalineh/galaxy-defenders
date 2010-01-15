//
// AsteroidSpawner.cs
//

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace galaxy
{
    public class CStageAsteroidSpawner
        : CStageElement
    {
        public float Frequency { get; set; }
        public float IncreaseRate { get; set; }
        public int SpawnCount { get; set; }

        public override void Update(CWorld world)
        {
            Frequency += IncreaseRate;

            if (SpawnCount > 0 && world.Random.NextFloat() < Frequency)
            {
                Vector2 spawn_position = GetRandomSpawnPosition(world);
                CAsteroid asteroid = new CAsteroid(world, spawn_position);

                asteroid.Physics.PositionPhysics.Velocity = GetRandomSpawnVelocity(world);
                asteroid.Physics.AnglePhysics.AngularVelocity = GetRandomSpawnAngularVelocity(world);

                float bigness = GetRandomSpawnBigness(world);
                asteroid.Visual.Scale = new Vector2(bigness);
                asteroid.HealthMax = 1.0f * bigness;

                world.EntityAdd(asteroid);
                SpawnCount -= 1;
            }
        }

        public override bool IsExpired()
        {
            return SpawnCount <= 0;
        }

        private Vector2 GetRandomSpawnPosition(CWorld world)
        {
            Viewport viewport = world.Game.GraphicsDevice.Viewport;
            Vector2 spawn_base = new Vector2(viewport.X, viewport.Y - 100.0f);
            Vector2 spawn_random = new Vector2(world.Random.NextFloat() * viewport.Width, world.Random.NextFloat() * -100.0f);
            return spawn_base + spawn_random;
        }

        private Vector2 GetRandomSpawnVelocity(CWorld world)
        {
            float x_base = 0.1f;
            float x_random = 0.3f * world.Random.NextFloat();
            float x_sign = world.Random.RandomSign();
            float x = x_base + x_random * x_sign;

            float y_base = 1.0f;
            float y_random = 2.0f * world.Random.NextFloat();
            float y = y_base + y_random;

            return new Vector2(x, y);
        }
        
        private float GetRandomSpawnAngularVelocity(CWorld world)
        {
            float angular_velocity_base = 0.05f;
            float angular_velocity_random = 0.1f * world.Random.NextFloat() * world.Random.RandomSign();

            return angular_velocity_base + angular_velocity_random;
        }
        
        private float GetRandomSpawnBigness(CWorld world)
        {
            float bigness_base = 1.0f;
            float bigness_random = 0.15f * world.Random.NextFloat();

            return bigness_base + bigness_random * world.Random.RandomSign();
        }
    }
}
