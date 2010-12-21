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

        public bool IsTeleporting { get; private set; }
        public int TeleportCountdown { get; set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Physics.AngularVelocity = 0.005f;
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 28.0f);
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Enemy/Teleporter");
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
            UpdateTeleport();
        }

        private void UpdateFire()
        {
            if (IsTeleporting)
                return;

            FireCooldown -= 1;
            if (FireCooldown <= 0)
            {
                Fire();
                IsTeleporting = true;
                TeleportCountdown = 90;
            }
        }

        private void UpdateTeleport()
        {
            if (!IsTeleporting)
                return;

            TeleportCountdown = Math.Max(0, TeleportCountdown - 1); 
            Physics.AngularVelocity += 0.005f;
            if (TeleportCountdown == 0)
            {
                Teleport();
                Fire();
            }
        }

        private void Teleport()
        {
            World.ParticleEffects.Spawn(EParticleType.EnemyTeleporterAppear, Physics.Position);

            Vector2 to_center = World.GameCamera.GetCenter().ToVector2() - Physics.Position;
            Vector2 direction = to_center.Rotate(World.Random.NextFloat() * 0.2f * World.Random.NextSign());
            Vector2 to_target = direction.Normal() * (200.0f + World.Random.NextFloat() * 100.0f);
            Physics.Position += to_target;
            Physics.Position += Vector2.UnitY * -300.0f;
            Physics.AngularVelocity = 0.005f;
            IsTeleporting = false;

            // TODO: a not crap effect
            //CEffect.TeleportIn(World, Physics.Position);
        }

        private void Fire()
        {
            Vector2 position = Physics.Position;
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
            circle.Position = Physics.Position;
        }
    }
}

