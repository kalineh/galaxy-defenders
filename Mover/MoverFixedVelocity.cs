﻿//
// MoverFixedVelocity.cs
//

using Microsoft.Xna.Framework;

namespace Galaxy
{
    public class CMoverFixedVelocity
        : CMover
    {
        public Vector2 Velocity { get; set; }

        public override void Move(CEntity entity)
        {
            if (Paused)
            {
                entity.Physics.Velocity = Vector2.Zero;
                return;
            }

            entity.Physics.Velocity = Velocity * SpeedMultiplier;
        }
    }
}

