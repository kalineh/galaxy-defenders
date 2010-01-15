//
// EnemySpawner.cs
//

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace galaxy
{
    public class CStageEnemySpawner
        : CStageElement
    {
        public System.Type Type { get; set; }
        public float Frequency { get; set; }
        public float IncreaseRate { get; set; }
        public int SpawnCount { get; set; }

        public override void Update(CWorld world)
        {
            Frequency += IncreaseRate;

            if (SpawnCount > 0 && world.Random.NextFloat() < Frequency)
            {
                Vector2 spawn_position = GetRandomSpawnPosition(world);
                CEnemy enemy = Activator.CreateInstance(Type, new object[] { world, spawn_position }) as CEnemy;
                world.EntityAdd(enemy);
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
            Vector2 spawn_base = new Vector2(viewport.X, viewport.Y - 50.0f);
            Vector2 spawn_random = new Vector2(world.Random.NextFloat() * viewport.Width, world.Random.NextFloat() * -50.0f);
            return spawn_base + spawn_random;
        }
    };
}


