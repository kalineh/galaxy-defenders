//
// MissileStorm.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CMissileStorm
        : CEnemy
    {
        public float FireDelay { get; private set; }
        private int FireCooldown { get; set; }
        public float FireDamage { get; private set; }
        public float FireSpeed { get; private set; }
        public int FireSequenceCountdown { get; set; }
        public float LastFireRotation { get; set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 48.0f);
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Enemy/MissileStorm");
            Visual.Depth = CLayers.Enemy + CLayers.SubLayerIncrement * -1.0f;
            HealthMax = 5.0f;

            FireDelay = 0.05f;
            FireCooldown = (int)(Time.ToFrames(FireDelay) * world.Random.NextFloat());
            FireDamage = 7.5f;
            FireSpeed = 2.5f;
            LastFireRotation = MathHelper.PiOver2;
        }

        public override void UpdateAI()
        {
            UpdateFire();
        }

        private void UpdateFire()
        {
            if (FireSequenceCountdown < -30)
            {
                FireSequenceCountdown = 180;
                LastFireRotation = World.Random.NextAngle();
            }

            FireSequenceCountdown -= 1;
            if (FireSequenceCountdown > 0)
                return;

            FireCooldown -= 1;
            if (FireCooldown <= 0)
                Fire();
        }

        private void Fire()
        {
            Vector2 position = Physics.Position;
            Vector2 dir = GetDirToShip();
            float rotation = LastFireRotation;

            LastFireRotation += MathHelper.Pi / 8.0f;

            CEnemyMissile.Spawn(World, position, rotation, FireSpeed, FireDamage);
            CAudio.PlaySound("EnemyShoot");
            FireCooldown = Time.ToFrames(FireDelay);
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.Position;
        }

        protected override bool DoesGenerateCorpse()
        {
            return true;
        }
    }
}

