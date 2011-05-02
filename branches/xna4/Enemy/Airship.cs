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
            Collision = CCollision.GetCacheAABB(this, Vector2.Zero, new Vector2(72.0f, 104.0f));
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Enemy/Airship");
            HealthMax = 7.0f;

            CollisionOffset = new Vector2(-36.0f, -52.0f);

            FireDelay = 0.5f;
            FireCooldown = Time.ToFrames(FireDelay) / 2;
            FireDamage = 3.0f;
            FireSpeed = 8.0f;
            Coins = 6;
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
            Vector2 left_fire_offset = dir * -50.0f;
            Vector2 right_fire_offset = dir * 47.0f;
            Vector2 left_fire_position = position + left_fire_offset;
            Vector2 right_fire_position = position + right_fire_offset;

            CEnemyLaser.Spawn(World, left_fire_position, rotation, FireSpeed, FireDamage);
            CEnemyLaser.Spawn(World, right_fire_position, rotation, FireSpeed, FireDamage);

            FireCooldown = (int)(Time.ToFrames(FireDelay) * CDifficulty.ReloadSpeedScale[World.CachedDifficulty]);
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionAABB aabb = Collision as CollisionAABB;
            aabb.Position = Physics.Position + CollisionOffset;
        }

        protected override void OnDie()
        {
            Vector2 anti_camera = Vector2.Zero;
            if (Mover != null)
                anti_camera = World.ScrollSpeed * -Vector2.UnitY * 0.5f;

            anti_camera += Vector2.UnitX.Rotate(World.Random.NextAngle()) * 1.5f;

            World.ParticleEffects.Spawn(EParticleType.EnemyDeathExplosion, Physics.Position, CEnemy.EnemyOrangeColor, 1.5f, anti_camera);
            World.ParticleEffects.Spawn(EParticleType.EnemyDeathExplosion, Physics.Position, CEnemy.EnemyGrayColor, 1.5f, anti_camera);

            base.OnDie();
        }
    }
}

