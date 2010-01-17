//
// MoverSequence.cs
//

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Galaxy
{
    public class CMoverSequence
        : CMover
    {
        public List<Vector2> Velocity { get; set; }
        public List<float> Duration { get; set; }
        public float VelocityLerpRate { get; set; }

        public CMoverSequence()
        {
            VelocityLerpRate = 1.0f;
        }

        public override void Move(CEntity entity)
        {
            Vector2 velocity = GetVelocity(entity);
            entity.Physics.PositionPhysics.Velocity = Vector2.Lerp(entity.Physics.PositionPhysics.Velocity, velocity, VelocityLerpRate);
        }

        private Vector2 GetVelocity(CEntity entity)
        {
            Vector2 velocity = Velocity[Velocity.Count - 1];
            float remaining = entity.AliveTime;

            for (int i = 0; i < Duration.Count; ++i)
            {
                float duration = Duration[i];

                if (remaining <= duration)
                {
                    velocity = Velocity[i];
                    break;
                }

                remaining -= duration;
            }

            return velocity;
        }
    }
}

