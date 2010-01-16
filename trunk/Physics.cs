//
// Physics.cs
//

using System;
using Microsoft.Xna.Framework;

// TODO: infinitismal float handling
// TODO: mass
// TODO: max speed

namespace Galaxy
{
    public class CSimplePositionPhysics
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }

        public CSimplePositionPhysics()
        {
            Position = new Vector2(0.0f, 0.0f);
            Velocity = new Vector2(0.0f, 0.0f);
        }

        public virtual void Solve()
        {
            Position += Velocity;
        }
    }

    public class CNewtonPositionPhysics
        : CSimplePositionPhysics
    {
        public float Friction { get; set; }

        public CNewtonPositionPhysics()
        {
            Friction = 1.0f;
        }

        public override void Solve()
        {
            Position += Velocity;
            Velocity *= Friction;
        }
    }

    public class CSimpleAnglePhysics 
    {
        public float Rotation { get; set; }
        public float AngularVelocity { get; set; }

        public CSimpleAnglePhysics()
        {
            Rotation = 0.0f;
            AngularVelocity = 0.0f;
        }

        public virtual void Solve()
        {
            Rotation += AngularVelocity;
            Wrap();
        }

        public Vector2 GetDir()
        {
            return Vector2.UnitX.Rotate(Rotation);
        }

        protected void Wrap()
        {
            Rotation = MathHelper.WrapAngle(Rotation);
        }
    }

    public class CNewtonAnglePhysics
        : CSimpleAnglePhysics
    {
        public float AngularFriction { get; set; }

        public CNewtonAnglePhysics()
        {
            AngularFriction = 1.0f;
        }

        public override void Solve()
        {
            Rotation += AngularVelocity;
            AngularVelocity *= AngularFriction;
            Wrap();
        }
    }

    public class CPhysics
    {
        public CNewtonPositionPhysics PositionPhysics { get; set; }
        public CNewtonAnglePhysics AnglePhysics { get; set; }

        public CPhysics()
        {
            PositionPhysics = new CNewtonPositionPhysics();
            AnglePhysics = new CNewtonAnglePhysics();
        }
    };
}
