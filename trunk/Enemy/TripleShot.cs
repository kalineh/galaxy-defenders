//
// TripleShot.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CTripleShot
        : CEnemy
    {
        public float FireDelay { get; private set; }
        private int FireCooldown { get; set; }
        public float FireDamage { get; private set; }
        public float FireSpeed { get; private set; }
        public float PauseCounter { get; private set; }
        public float TripleShotDelay { get; set; }
        public int TripleShotCounter { get; set; }
        private CMover OriginalMover { get; set; }
        private CMover IgnoreCameraMover { get; set; }
        private Vector2 PreviousVelocity { get; set; }
        private float LastFireAngle { get; set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 34.0f);
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Enemy/TripleShot");
            HealthMax = 2.0f;

            FireDelay = 1.5f;
            FireCooldown = (int)(Time.ToFrames(FireDelay) * world.Random.NextFloat());
            FireDamage = 3.0f;
            FireSpeed = CEnemy.TurretShotSpeed;
            TripleShotDelay = 0.15f;

            //IgnoreCameraMover = new CMoverIgnoreCamera();
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
            // TODO: fibers :(
            // TODO: make sure the last shot isnt fired so we get a nice delay after shot before moving again
            if (TripleShotCounter < 3)
            {
                if (TripleShotCounter == 0)
                {
                    Vector2 dir = GetDirToShipWithLead(10.0f);
                    float rotation = dir.ToAngle();
                    LastFireAngle = rotation;
                }

                Vector2 position = Physics.Position;
                float random = World.Random.NextAngle() * 0.015f;

                CEnemyShot shot = CEnemyShot.Spawn(World, position, LastFireAngle + random, FireSpeed, FireDamage);
                CAudio.PlaySound("EnemyShoot");

                OriginalMover = OriginalMover ?? Mover;
                PreviousVelocity = Physics.Velocity;

                if (Mover != null)
                {
                    IgnoreCameraMover = IgnoreCameraMover ?? new CMoverIgnoreCamera();
                }

                Mover = IgnoreCameraMover;
                FireCooldown = Time.ToFrames(TripleShotDelay);
                TripleShotCounter += 1;
            }
            else
            {
                FireCooldown = Time.ToFrames(FireDelay + World.Random.NextFloat());
                Mover = OriginalMover;
                OriginalMover = null;
                Physics.Velocity = PreviousVelocity;
                TripleShotCounter = 0;
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

