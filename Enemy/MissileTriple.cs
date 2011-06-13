//
// MissileTriple.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CMissileTriple
        : CEnemy
    {
        public float FireDelay { get; private set; }
        private int FireCooldown { get; set; }
        public float FireDamage { get; private set; }
        public float FireSpeed { get; private set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 34.0f);
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Enemy/MissileTriple");
            Visual.Depth = CLayers.Enemy + CLayers.SubLayerIncrement * -1.0f;
            HealthMax = 3.0f;

            FireDelay = 1.85f;
            FireCooldown = (int)(Time.ToFrames(FireDelay) * world.Random.NextFloat());
            FireDamage = 6.0f;
            FireSpeed = 2.5f;
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
            //Vector2 dir = GetDirToShip();
            float rotation = MathHelper.PiOver2;

            CEnemyMissile.Spawn(World, position, rotation, FireSpeed, FireDamage);
            CEnemyMissile.Spawn(World, position, rotation + MathHelper.PiOver4, FireSpeed, FireDamage);
            CEnemyMissile.Spawn(World, position, rotation - MathHelper.PiOver4, FireSpeed, FireDamage);
            CAudio.PlaySound("EnemyShoot");
            FireCooldown = (int)(Time.ToFrames(FireDelay) * CDifficulty.ReloadSpeedScale[World.CachedDifficulty]);
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

