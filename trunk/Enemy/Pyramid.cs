//
// Pyramid.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Galaxy
{
    public class CPyramid
        : CEnemy
    {
        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 48.0f);
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Enemy/Pyramid");
            HealthMax = 13.0f;

            Physics.AnglePhysics.AngularVelocity = 0.005f * World.Random.NextSign();
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.PositionPhysics.Position;
        }

        protected override void OnDie()
        {
            // TODO: new explosion effect
            CEffect.EnemyExplosion(World, Physics.PositionPhysics.Position, 3.0f);

            CEnemy a = new CIsosceles();
            CEnemy b = new CIsosceles();
            CEnemy c = new CIsosceles();
            CEnemy d = new CIsosceles();

            a.Initialize(World);
            b.Initialize(World);
            c.Initialize(World);
            d.Initialize(World);

            a.Physics.PositionPhysics.Position = Physics.PositionPhysics.Position + new Vector2(0.0f, -16.0f);
            b.Physics.PositionPhysics.Position = Physics.PositionPhysics.Position + new Vector2(-32.0f, 16.0f);
            c.Physics.PositionPhysics.Position = Physics.PositionPhysics.Position + new Vector2(0.0f, 16.0f);
            d.Physics.PositionPhysics.Position = Physics.PositionPhysics.Position + new Vector2(32.0f, 16.0f);

            a.Physics.PositionPhysics.Velocity = new Vector2(0.0f, -0.2f);
            b.Physics.PositionPhysics.Velocity = new Vector2(-0.5f, 0.2f);
            c.Physics.PositionPhysics.Velocity = new Vector2(0.0f, 0.2f);
            d.Physics.PositionPhysics.Velocity = new Vector2(0.5f, 0.2f);

            a.Physics.PositionPhysics.Velocity += World.Random.NextVector2(World.Random.NextFloat() * 0.5f);
            b.Physics.PositionPhysics.Velocity += World.Random.NextVector2(World.Random.NextFloat() * 0.5f);
            c.Physics.PositionPhysics.Velocity += World.Random.NextVector2(World.Random.NextFloat() * 0.5f);
            d.Physics.PositionPhysics.Velocity += World.Random.NextVector2(World.Random.NextFloat() * 0.5f);

            a.Physics.PositionPhysics.Friction = 1.0f;
            b.Physics.PositionPhysics.Friction = 1.0f;
            c.Physics.PositionPhysics.Friction = 1.0f;
            d.Physics.PositionPhysics.Friction = 1.0f;

            a.Physics.AnglePhysics.AngularVelocity = 0.025f;
            b.Physics.AnglePhysics.AngularVelocity = 0.025f;
            c.Physics.AnglePhysics.AngularVelocity = -0.025f;
            d.Physics.AnglePhysics.AngularVelocity = -0.025f;

            a.HealthMax *= 1.5f;
            b.HealthMax *= 1.5f;
            c.HealthMax *= 1.5f;
            d.HealthMax *= 1.5f;

            World.EntityAdd(a);
            World.EntityAdd(b);
            World.EntityAdd(c);
            World.EntityAdd(d);

            base.OnDie();
        }
    }
}

