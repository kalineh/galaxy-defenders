//
// DoubleTurret.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CDoubleTurret
        : CEnemy
    {
        public float FireDelay { get; private set; }
        private int FireCooldown { get; set; }
        public float FireDamage { get; private set; }
        public float FireSpeed { get; private set; }
        private int FireCycle { get; set; }
        private float LastFireAngle { get; set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 34.0f);
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Enemy/DoubleTurret");
            Visual.Depth = CLayers.Enemy + CLayers.SubLayerIncrement * -1.0f;
            HealthMax = 2.0f;

            FireDelay = 0.75f;
            FireCooldown = (int)(Time.ToFrames(FireDelay) * world.Random.NextFloat());
            FireDamage = 3.0f;
            FireSpeed = CEnemy.TurretShotSpeed;

            Coins = 4;
        }

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
            Vector2 position = Physics.Position;

            if (FireCycle == 0)
            {
                Vector2 dir = GetDirToShip();
                float rotation = dir.ToAngle();
                rotation += World.Random.NextAngle() * 0.01f * World.Random.NextSign();
                LastFireAngle = rotation;
            }

            CEnemyShot shot = CEnemyShot.Spawn(World, position, LastFireAngle, FireSpeed, FireDamage);
            CAudio.PlaySound("EnemyShoot");

            FireCycle += 1;

            if (FireCycle == 1)
            {
                FireCooldown = 6;
            }
            else
            {
                FireCooldown = (int)(Time.ToFrames(FireDelay) * CDifficulty.ReloadSpeedScale[World.CachedDifficulty]);
                FireCycle = 0;
            }
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

