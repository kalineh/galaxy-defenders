//
// Teleporter.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CTeleporter
        : CEnemy
    {
        public float FireDelay { get; private set; }
        private int FireCooldown { get; set; }
        public float FireDamage { get; private set; }
        public float FireSpeed { get; private set; }

        public int TeleportCountdown { get; set; }
        public int InTeleportCountdown { get; set; }

        private int FireWaves { get; set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Physics.AngularVelocity = 0.005f;
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 34.0f);
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Enemy/Teleporter");
            Visual.Depth = CLayers.Enemy + CLayers.SubLayerIncrement * -2.0f;
            Visual.Recache();
            HealthMax = 4.0f;

            FireDelay = 0.1f;
            FireCooldown = (int)(Time.ToFrames(FireDelay) * world.Random.NextFloat());
            FireDamage = 6.0f;
            FireSpeed = 4.0f;

            FireWaves = -1;
        }

        public override void UpdateAI()
        {
            UpdateFire();
            UpdateTeleport();
        }

        public override void Draw(SpriteBatch sprite_batch)
        {
            if (InTeleportCountdown > 0)
                return;

            base.Draw(sprite_batch);
        }

        private void UpdateFire()
        {
            if (TeleportCountdown > 0)
                return;

            if (InTeleportCountdown > 0)
                return;

            if (FireWaves < 0)
                return;

            FireCooldown -= 1;
            if (FireCooldown <= 0)
            {
                Fire();
            }
        }

        private void UpdateTeleport()
        {
            if (TeleportCountdown > 0)
            {
                TeleportCountdown -= 1;
                Physics.AngularVelocity += 0.010f;
                if (TeleportCountdown == 0)
                {
                    Teleport();
                }

                return;
            }

            if (InTeleportCountdown > 0)
            {
                InTeleportCountdown -= 1;
                if (InTeleportCountdown == 0)
                    Collision.Enabled = true;

                if (InTeleportCountdown == 15)
                    World.ParticleEffects.Spawn(EParticleType.EnemyTeleporterAppear, Physics.Position);
            }
        }

        private void Teleport()
        {
            World.ParticleEffects.Spawn(EParticleType.EnemyTeleporterVanish, Physics.Position);

            Physics.Position = new Vector2(
                World.GameCamera.GetCenter().X + World.Random.NextFloat() * 300.0f * World.Random.NextSign(),
                World.GameCamera.GetTopLeft().Y - 150.0f + World.Random.NextFloat() * 100.0f
            );
            Physics.Velocity = GetPerpToShip() * 2.0f + GetDirToCenter() * 2.0f;
            Physics.Friction = 0.99f;
            Physics.AngularVelocity = 0.005f;
            InTeleportCountdown = 90;
            FireWaves = -1;
            FireCooldown = 0;
            Collision.Enabled = false;

            // TODO: a not crap effect
            //CEffect.TeleportIn(World, Physics.Position);
        }

        private Vector2 GetPerpToShip()
        {
            Vector2 dir = GetDirToShip();
            return dir.Perp() * World.Random.NextSign();
        }

        private Vector2 GetDirToCenter()
        {
            Vector2 ofs = World.GameCamera.GetCenter().ToVector2() - Physics.Position;
            return ofs.Normal();
        }

        private void Fire()
        {
            Vector2 position = Physics.Position;
            Vector2 dir = GetDirToShip();
            float rotation = dir.ToAngle();

            rotation += FireWaves * MathHelper.PiOver4;

            CEnemyMissile.Spawn(World, position, MathHelper.WrapAngle(rotation + MathHelper.PiOver2 * 0.0f), FireSpeed, FireDamage);
            CEnemyMissile.Spawn(World, position, MathHelper.WrapAngle(rotation + MathHelper.PiOver2 * 1.0f), FireSpeed, FireDamage);
            CEnemyMissile.Spawn(World, position, MathHelper.WrapAngle(rotation + MathHelper.PiOver2 * 2.0f), FireSpeed, FireDamage);
            CEnemyMissile.Spawn(World, position, MathHelper.WrapAngle(rotation + MathHelper.PiOver2 * 3.0f), FireSpeed, FireDamage);

            //CEnemyPellet pellet = CEnemyPellet.Spawn(World, position, rotation, FireSpeed, FireDamage);
            CAudio.PlaySound("EnemyShoot");
            FireCooldown = (int)(Time.ToFrames(FireDelay) * CDifficulty.ReloadSpeedScale[World.CachedDifficulty]);

            FireWaves += 1;
            if (FireWaves > 1)
            {
                TeleportCountdown = 30;
                FireWaves = -1;
            }
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.Position;
        }

        public override void TakeDamage(float damage, CShip source)
        {
            base.TakeDamage(damage, source);

            if (FireWaves < 0)
                FireWaves = 0;
        }
    }
}

