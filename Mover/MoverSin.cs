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
        public int? StartFrame { get; set; }

        public CMoverSin()
        {
            SpeedMultiplier = 1.0f;
        }

        public override void Move(CEntity entity)
        {
            if (StartFrame == null)
                StartFrame = (int)entity.World.Game.GameFrame;

            if (Paused)
            {
                entity.Physics.PositionPhysics.Velocity = Vector2.Zero;
                return;
            }

            int frame = entity.World.Game.GameFrame - (int)StartFrame;
            float t = frame * Frequency;
            float x = (float)Math.Cos(t) * Amplitude;
            float y = Down * SpeedMultiplier;

            entity.Physics.PositionPhysics.Velocity = new Vector2(x, y);
        }
    }
}

