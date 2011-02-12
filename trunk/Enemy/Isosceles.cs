//
// Isosceles.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CIsosceles
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
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Enemy/Isosceles");
            HealthMax = 0.75f;

            FireDelay = 1.5f;
            FireCooldown = Time.ToFrames(FireDelay) / 3;
            FireDamage = 1.0f;
            FireSpeed = CEnemy.TurretShotSpeed;
            Coins = 1;
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
            Vector2 dir = GetDirToShip();
            float rotation = Vector2.UnitY.ToAngle();

            // TODO: do we want this enemy to shoot? (CBeamer is better perhaps)
            //CEnemyLaser shot = CEnemyLaser.Spawn(World, position, rotation, FireSpeed, FireDamage);
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
