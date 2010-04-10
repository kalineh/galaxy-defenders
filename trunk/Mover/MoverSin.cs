//
// MoverSin.cs
//

using System;
using Microsoft.Xna.Framework;

namespace Galaxy
{
    public class CMoverSin
        : CMover
    {
        public float Frequency { get; set; }
        public float Amplitude { get; set; }
        public float Down { get; set; }
        public float SpeedMultiplier { get; set; }

        public CMoverSin()
        {
            SpeedMultiplier = 1.0f;
        }

        public override void Move(CEntity entity)
        {
            if (Paused)
            {
                entity.Physics.PositionPhysics.Velocity = Vector2.Zero;
                return;
            }

            float t = entity.World.Game.GameFrame * Frequency;
            float x = (float)Math.Cos(t) * Amplitude;
            float y = Down * SpeedMultiplier;

            entity.Physics.PositionPhysics.Velocity = new Vector2(x, y);
        }
    }
}

