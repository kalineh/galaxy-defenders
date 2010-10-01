//
// MissilePod.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CMissilePod
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
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 28.0f);
            Visual = CVisual.MakeSpriteCached1(world, "Textures/Enemy/MissilePod");
            Visual.Depth = CLayers.Enemy + CLayers.SubLayerIncrement * -1.0f;
            HealthMax = 5.0f;

            FireDelay = 2.0f;
            FireCooldown = (int)(Time.ToFrames(FireDelay) * world.Random.NextFloat());
            FireDamage = 7.5f;
            FireSpeed = 2.5f;
            Health = 5.0f;
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
            Vector2 position = Physics.PositionPhysics.Position;
            Vector2 dir = GetDirToShip();
            float rotation = dir.ToAngle();

            rotation += World.Random.NextAngle() * 0.015f;

            CEnemyMissile missile = CEnemyMissile.Spawn(World, position, rotation, FireSpeed, FireDamage);
            CAudio.PlaySound("EnemyShoot");
            FireCooldown = Time.ToFrames(FireDelay);
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.PositionPhysics.Position;
        }

        protected override bool DoesGenerateCorpse()
        {
            return true;
        }
    }
}

