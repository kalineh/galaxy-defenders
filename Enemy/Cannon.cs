//
// Cannon.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CCannon
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
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Enemy/Cannon");
            Visual.Depth = CLayers.Enemy + CLayers.SubLayerIncrement * -1.0f;
            HealthMax = 6.0f;

            FireDelay = 1.25f;
            FireCooldown = (int)(Time.ToFrames(FireDelay) * world.Random.NextFloat());
            FireDamage = 5.0f;
            FireSpeed = 10.0f;

            Coins = 8;
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
            Vector2 dir = Vector2.UnitY;
            float rotation = MathHelper.PiOver2;

            CEnemyCannonShot shot = CEnemyCannonShot.Spawn(World, position, rotation, FireSpeed, FireDamage);
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

