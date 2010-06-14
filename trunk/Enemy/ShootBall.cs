//
// ShootBall.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CShootBall
        : CEnemy
    {
        public float FireDelay { get; private set; }
        private int FireCooldown { get; set; }
        public float FireDamage { get; private set; }
        public float FireSpeed { get; private set; }

        public CShootBall(CWorld world, Vector2 position)
            : base(world)
        {
            Physics = new CPhysics();
            Physics.PositionPhysics.Position = position;
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 32.0f);
            Visual = new CVisual(world, CContent.LoadTexture2D(world.Game, "Textures/Enemy/ShootBall"), Color.White);
            HealthMax = 1.0f;

            FireDelay = 1.5f;
            FireCooldown = (int)(Time.ToFrames(FireDelay) * world.Random.NextFloat());
            FireDamage = 2.0f;
            FireSpeed = 12.0f;
        }

#if XBOX360
        public CShootBall()
        {
        }

        public void Init360(CWorld world, Vector2 position)
        {
            base.Init360(world);

            Physics = new CPhysics();
            Physics.PositionPhysics.Position = position;
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 32.0f);
            Visual = new CVisual(world, CContent.LoadTexture2D(world.Game, "Textures/Enemy/ShootBall"), Color.White);
            HealthMax = 1.0f;

            FireDelay = 1.5f;
            FireCooldown = (int)(Time.ToFrames(FireDelay) * world.Random.NextFloat());
            FireDamage = 2.0f;
            FireSpeed = 12.0f;
        }
#endif

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

            CEnemyShot shot = CEnemyShot.Spawn(World, position, rotation, FireSpeed, FireDamage);
            World.Sound.Play("EnemyShoot");
            FireCooldown = Time.ToFrames(FireDelay);
        }


        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.PositionPhysics.Position;
        }
    }
}

