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

        public override void Move(CEntity entity)
        {
            float t = entity.World.Game.GameFrame * Frequency;
            float x = (float)Math.Cos(t) * Amplitude;
            float y = Down;

            entity.Physics.PositionPhysics.Velocity = new Vector2(x, y);
        }
    }
}

