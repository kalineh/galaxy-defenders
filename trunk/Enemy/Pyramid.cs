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
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 56.0f);
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Enemy/Pyramid");
            HealthMax = 8.0f;
            Coins = 3;

            Physics.AngularVelocity = 0.005f * World.Random.NextSign();
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.Position;
        }

        protected override void OnDie()
        {
            Vector2 anti_camera = Vector2.Zero;
            if (Mover != null)
                anti_camera = World.ScrollSpeed * -Vector2.UnitY * 0.5f;

            anti_camera += Vector2.UnitX.Rotate(World.Random.NextAngle()) * 1.5f;

            World.ParticleEffects.Spawn(EParticleType.EnemyDeathExplosion, Physics.Position, CEnemy.EnemyOrangeColor, 1.5f, anti_camera);
            World.ParticleEffects.Spawn(EParticleType.EnemyDeathExplosion, Physics.Position, CEnemy.EnemyGrayColor, 1.5f, anti_camera);

            CEnemy a = new CIsosceles();
            CEnemy b = new CIsosceles();
            CEnemy c = new CIsosceles();

            a.Initialize(World);
            b.Initialize(World);
            c.Initialize(World);

            a.Physics.Position = Physics.Position + new Vector2(0.0f, -16.0f);
            b.Physics.Position = Physics.Position + new Vector2(-32.0f, 16.0f);
            c.Physics.Position = Physics.Position + new Vector2(0.0f, 16.0f);

            a.Physics.Velocity = new Vector2(0.0f, -0.2f);
            b.Physics.Velocity = new Vector2(-0.5f, 0.2f);
            c.Physics.Velocity = new Vector2(0.0f, 0.2f);

            a.Physics.Velocity += World.Random.NextVector2(World.Random.NextFloat() * 0.5f);
            b.Physics.Velocity += World.Random.NextVector2(World.Random.NextFloat() * 0.5f);
            c.Physics.Velocity += World.Random.NextVector2(World.Random.NextFloat() * 0.5f);

            a.Physics.Friction = 1.0f;
            b.Physics.Friction = 1.0f;
            c.Physics.Friction = 1.0f;

            a.Physics.AngularVelocity = 0.025f;
            b.Physics.AngularVelocity = 0.025f;
            c.Physics.AngularVelocity = -0.025f;

            a.HealthMax *= 1.5f;
            b.HealthMax *= 1.5f;
            c.HealthMax *= 1.5f;

            World.EntityAdd(a);
            World.EntityAdd(b);
            World.EntityAdd(c);

            base.OnDie();
        }
    }
}

