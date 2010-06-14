//
// DownTurret.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CDownTurret
        : CEnemy
    {
        public float FireDelay { get; private set; }
        private int FireCooldown { get; set; }
        public float FireDamage { get; private set; }
        public float FireSpeed { get; private set; }

        public CDownTurret(CWorld world, Vector2 position)
            : base(world)
        {
            Physics = new CPhysics();
            Physics.PositionPhysics.Position = position;
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 28.0f);
            Visual = CVisual.MakeSpriteCached1(world, "Textures/Enemy/DownTurret");
            Visual.Depth = CLayers.Enemy + CLayers.SubLayerIncrement * -1.0f;
            HealthMax = 2.5f;

            FireDelay = 1.5f;
            FireCooldown = (int)(Time.ToFrames(FireDelay) * world.Random.NextFloat());
            FireDamage = 2.0f;
            FireSpeed = 12.0f;
        }

#if XBOX360
        public CDownTurret()
        {
        }

        public void Init360(CWorld world, Vector2 position)
        {
            base.Init360(world);

            Physics = new CPhysics();
            Physics.PositionPhysics.Position = position;
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 28.0f);
            Visual = CVisual.MakeSpriteCached1(world, "Textures/Enemy/DownTurret");
            Visual.Depth = CLayers.Enemy + CLayers.SubLayerIncrement * -1.0f;
            HealthMax = 2.5f;

            FireDelay = 1.5f;
            FireCooldown = (int)(Time.ToFrames(FireDelay) * world.Random.NextFloat());
            FireDamage = 2.0f;
            FireSpeed = 12.0f;
        }
#endif

        public override void UpdateAI()
        {
            UpdateFire();
        }

        private void UpdateFire()
        {
            FireCooldown -= 1;
            if (FireCooldown <= 0)
            {
                Fire();
            }
        }

        private void Fire()
        {
            Vector2 position = Physics.PositionPhysics.Position;
            Vector2 dir = Vector2.UnitY;
            float rotation = MathHelper.PiOver2;

            CEnemyShot shot = CEnemyShot.Spawn(World, position, rotation, FireSpeed, FireDamage);
            World.Sound.Play("EnemyShoot");
            FireCooldown = Time.ToFrames(FireDelay);
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.PositionPhysics.Position;
        }
    }
}

