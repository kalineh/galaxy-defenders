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

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 28.0f);
            Visual = CVisual.MakeSpriteCached1(world, "Textures/Enemy/TripleShot");
            HealthMax = 4.0f;

            FireDelay = 2.5f;
            FireCooldown = (int)(Time.ToFrames(FireDelay) * world.Random.NextFloat());
            FireDamage = 2.0f;
            FireSpeed = 10.0f;
            TripleShotDelay = 0.15f;
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
                Vector2 position = Physics.PositionPhysics.Position;
                Vector2 dir = GetDirToShip();
                float rotation = dir.ToAngle();

                rotation += World.Random.NextAngle() * 0.015f;

                CEnemyShot shot = CEnemyShot.Spawn(World, position, rotation, FireSpeed, FireDamage);
                CAudio.PlaySound("EnemyShoot");

                OriginalMover = OriginalMover ?? Mover;

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
                FireCooldown = Time.ToFrames(FireDelay);
                Mover = OriginalMover;
                OriginalMover = null;
                TripleShotCounter = 0;
            }
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.PositionPhysics.Position;
        }
    }
}

