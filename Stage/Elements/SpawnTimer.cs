//
// SpawnTimer.cs
//

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Galaxy
{
    public abstract class CSpawnTimer
    {
        public abstract int Update(CWorld world);
    }

    public class CSpawnTimerRandom
        : CSpawnTimer
    {
        public float Frequency { get; set; }
        public float IncreaseRate { get; set; }

        public CSpawnTimerRandom()
        {
            Frequency = 0.01f;
            IncreaseRate = 0.0f;
        }

        public override int Update(CWorld world)
        {
            Frequency += IncreaseRate;
            if (world.Random.NextFloat() < Frequency)
                return 1;

            return 0;
        }
    }

    public class CSpawnTimerInterval
        : CSpawnTimer
    {
        public float Delay { get; set; }
        private float Cooldown { get; set; }

        public CSpawnTimerInterval()
        {
            Delay = 1.0f;
            Cooldown = 0.0f;
        }

        public override int Update(CWorld world)
        {
            Cooldown -= Time.ToSeconds(1);
            if (Cooldown <= 0.0f)
            {
                Cooldown = Delay;
                return 1;
            }

            return 0;
        }
    }
}


