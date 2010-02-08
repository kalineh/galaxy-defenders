//
// MoverFixedVelocity.cs
//

using System;
using Microsoft.Xna.Framework;

namespace Galaxy
{
    public class CMoverFixedVelocity
        : CMover
    {
        public float SpeedMultiplier { get; set; }
        public Vector2 Velocity { get; set; }

        public override void Move(CEntity entity)
        {
            entity.Physics.PositionPhysics.Velocity = Velocity * SpeedMultiplier;
        }
    }
}

