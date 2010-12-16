//
// Physics.cs
//

using Microsoft.Xna.Framework;
using System;

// TODO: infinitismal float handling
// TODO: mass
// TODO: max speed

// TODO-OPT: structify me
// TODO-OPT: remove layers
// TODO-OPT: replace vector math with memberwise calculations

namespace Galaxy
{
    public class CPhysics
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public float Friction;
        public float Rotation;
        public float AngularVelocity;
        public float AngularFriction;

        public CPhysics()
        {
            Position = new Vector2();
            Velocity = new Vector2();
            Friction = 1.0f;
            Rotation = 0.0f;
            AngularVelocity = 0.0f;
            AngularFriction = 1.0f;
        }

        public void Solve()
        {
            // TODO: optimize
            Position += Velocity;
            Velocity *= Friction;
            Rotation = Rotation + AngularVelocity;
            Rotation = MathHelper.WrapAngle(Rotation);
            AngularVelocity *= AngularFriction;
        }

        public Vector2 GetDir()
        {
            return Vector2.UnitX.Rotate(Rotation);
        }
    };
}
