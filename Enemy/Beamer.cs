//
// Beamer.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CBeamer
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
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 32.0f);
            Visual = new CVisual(world, CContent.LoadTexture2D(world.Game, "Textures/Enemy/Beamer"), Color.White);
            HealthMax = 4.0f;

            FireDelay = 1.5f;
            FireCooldown = Time.ToFrames(FireDelay) / 2;
            FireDamage = 2.0f;
            FireSpeed = 14.0f;
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
            // TODO: this math is weird and broken
            // TODO: replace with matrices
            Vector2 position = Physics.PositionPhysics.Position;
            float rotation = Vector2.UnitY.ToAngle();
            Vector2 dir = Physics.AnglePhysics.GetDir();
            Vector2 fire_offset = dir * 2.0f + dir.Perp() * 16.0f;
            Vector2 fire_position = position + fire_offset;

            CEnemyLaser laser = CEnemyLaser.Spawn(World, fire_position, rotation, FireSpeed, FireDamage);
            FireCooldown = Time.ToFrames(FireDelay);
        }

        public override void Update()
        {
            UpdateAI();

            base.Update();
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

