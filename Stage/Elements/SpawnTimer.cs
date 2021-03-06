﻿//
// SpawnTimer.cs
//


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
        public float Interval { get; set; }
        private float Cooldown { get; set; }

        public CSpawnTimerInterval()
        {
            Interval = 1.0f;
            Cooldown = 0.0f;
        }

        public override int Update(CWorld world)
        {
            Cooldown -= Time.SingleFrame;
            if (Cooldown <= 0.0f)
            {
                Cooldown = Interval;
                return 1;
            }

            return 0;
        }
    }
}


