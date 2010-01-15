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
                CAsteroid.Spawn(world);
                SpawnCount -= 1;
            }
        }

        public override bool IsExpired()
        {
            return SpawnCount <= 0;
        }
    }
}
