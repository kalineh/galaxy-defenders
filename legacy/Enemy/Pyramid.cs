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
        private CVisual TurretVisual { get; set; }
        private float TurretRotation { get; set; }
        private int ShootSequence { get; set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 56.0f);
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Enemy/Pyramid");
            HealthMax = 6.0f;
            Coins = 3;

            // boring mode
            Physics.AngularVelocity = 0.005f * World.Random.NextSign();

            // rad mode
            TurretVisual = CVisual.MakeSpriteUncached(world.Game, "Textures/Enemy/PyramidTurret");
            TurretVisual.Depth = CLayers.Enemy + CLayers.SubLayerIncrement * 1.0f;
            TurretVisual.Recache();
        }

        public override void Update()
        {
            base.Update();

            ShootSequence += 1;

            if (ShootSequence < 20)
            {
                Vector2 dir = GetDirToShipWithLead(5.0f);
                float target = dir.ToAngle() - MathHelper.PiOver2;
                TurretRotation = Interpolation.MoveToAngle(TurretRotation, target, 0.03f);
            }
            else if (ShootSequence < 40)
            {
                if (ShootSequence % 4 == 0)
                {
                    float adjusted = TurretRotation + MathHelper.PiOver2;
                    Vector2 turret_offset = Vector2.UnitX.Rotate(adjusted) * 34.0f;
                    CProjectile shot = CEnemyMiniShot.Spawn(World, Physics.Position + turret_offset, adjusted, 9.0f, 1.0f);
                    CAudio.PlaySound("EnemyCannonShoot");
                    shot.Physics.Velocity += World.ScrollSpeed * -Vector2.UnitY;
                }
            }
            else if (ShootSequence < 60)
            {
            }
            else
            {
                ShootSequence = 0;    
            }
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.Position;
        }

        public override void Draw(SpriteBatch sprite_batch)
        {
            base.Draw(sprite_batch);
            TurretVisual.Draw(sprite_batch, Physics.Position, TurretRotation);
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

            a.Physics.Position = Physics.Position + new Vector2(0.0f, -32.0f);
            b.Physics.Position = Physics.Position + new Vector2(-48.0f, 32.0f);
            c.Physics.Position = Physics.Position + new Vector2(0.0f, 32.0f);

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

