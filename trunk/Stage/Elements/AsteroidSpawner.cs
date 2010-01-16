//
// Asteroidpawner.cs
//

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Galaxy
{
    public class CSpawnerCustomAsteroid
        : CSpawnerCustomElement
    {
        public override void Customize(CEntity entity)
        {
            CAsteroid asteroid = entity as CAsteroid;

            asteroid.Physics.PositionPhysics.Velocity = GetRandomSpawnVelocity(entity.World);
            asteroid.Physics.AnglePhysics.AngularVelocity = GetRandomSpawnAngularVelocity(entity.World);

            float bigness = GetRandomSpawnBigness(entity.World);
            asteroid.Visual.Scale = new Vector2(bigness);
            asteroid.HealthMax = 1.0f * bigness;
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
