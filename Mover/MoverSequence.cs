//
// MoverSequence.cs
//

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
        public bool AlwaysMaxSpeed { get; set; }
        public float SpeedMultiplier { get; set; }

        public CMoverSequence()
        {
            VelocityLerpRate = 1.0f;
            AlwaysMaxSpeed = true;
            SpeedMultiplier = 1.0f;
        }

        public override void Move(CEntity entity)
        {
            Vector2 velocity = GetVelocity(entity);

            if (entity.AliveTime == 0.0f)
            {
                entity.Physics.PositionPhysics.Velocity = velocity;
                return;
            }

            if (AlwaysMaxSpeed)
            {
                float speed = velocity.Length();
                Vector2 target = Vector2.Lerp(entity.Physics.PositionPhysics.Velocity, velocity, VelocityLerpRate);
                entity.Physics.PositionPhysics.Velocity = target.Normal() * speed * SpeedMultiplier;
            }
            else
            {
                entity.Physics.PositionPhysics.Velocity = Vector2.Lerp(entity.Physics.PositionPhysics.Velocity, velocity, VelocityLerpRate);

            }
        }

        private Vector2 GetVelocity(CEntity entity)
        {
            Vector2 velocity = Velocity[Velocity.Count - 1];
            float remaining = entity.AliveTime;

            for (int i = 0; i < Duration.Count; ++i)
            {
                float duration = Duration[i];

                if (remaining <= duration * SpeedMultiplier)
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

