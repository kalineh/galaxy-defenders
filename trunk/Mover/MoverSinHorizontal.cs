//
// MoverSinHorizontal.cs
//

using System;
using Microsoft.Xna.Framework;

namespace Galaxy
{
    public class CMoverSinHorizontal
        : CMover
    {
        public float Frequency { get; set; }
        public float Amplitude { get; set; }
        public float Right { get; set; }
        public int? StartFrame { get; set; }

        public override void Move(CEntity entity)
        {
            if (StartFrame == null)
                StartFrame = (int)entity.World.Game.GameFrame;

            if (Paused)
            {
                entity.Physics.Velocity = Vector2.Zero;
                return;
            }

            int frame = entity.World.Game.GameFrame - (int)StartFrame;
            float t = frame * Frequency;
            float x = Right * SpeedMultiplier;
            float y = (float)Math.Cos(t) * Amplitude;

            entity.Physics.Velocity = new Vector2(x, y);
        }
    }
}

