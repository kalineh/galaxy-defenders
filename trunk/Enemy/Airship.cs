//
// Airship.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Galaxy
{
    public class CAirship
        : CEnemy
    {
        private Vector2 CollisionOffset { get; set; }
        public float FireDelay { get; private set; }
        private int FireCooldown { get; set; }
        public float FireDamage { get; private set; }
        public float FireSpeed { get; private set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Collision = CCollision.GetCacheAABB(this, Vector2.Zero, new Vector2(64.0f, 96.0f));
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Enemy/Airship");
            HealthMax = 13.0f;

            CollisionOffset = new Vector2(-32.0f, -48.0f);

            FireDelay = 0.5f;
            FireCooldown = Time.ToFrames(FireDelay) / 2;
            FireDamage = 3.0f;
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
            Vector2 position = Physics.Position;
            float rotation = Vector2.UnitY.ToAngle();
            Vector2 dir = Physics.GetDir();
            Vector2 left_fire_offset = dir * -41.0f;
            Vector2 right_fire_offset = dir * 38.0f;
            Vector2 left_fire_position = position + left_fire_offset;
            Vector2 right_fire_position = position + right_fire_offset;

            CEnemyLaser.Spawn(World, left_fire_position, rotation, FireSpeed, FireDamage);
            CEnemyLaser.Spawn(World, right_fire_position, rotation, FireSpeed, FireDamage);

            FireCooldown = Time.ToFrames(FireDelay);
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionAABB aabb = Collision as CollisionAABB;
            aabb.Position = Physics.Position + CollisionOffset;
        }

        protected override void OnDie()
        {
            // TODO: new explosion effect
            CEffect.EnemyExplosion(World, Physics.Position, 3.0f);

            base.OnDie();
        }
    }
}

