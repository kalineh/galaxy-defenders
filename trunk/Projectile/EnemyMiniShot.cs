//
// EnemyMiniShot.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CEnemyMiniShot
        : CProjectile
    {
        public bool IsReflected { get; set; }
        public CShip WhoReflected { get; set; }

        public static CEnemyMiniShot Spawn(CWorld world, Vector2 position, float rotation, float speed, float damage)
        {
            CEnemyMiniShot shot = new CEnemyMiniShot();
            shot.Initialize(world, null, damage);

            shot.Physics.Rotation = rotation;
            shot.Physics.Position = position;
            shot.Physics.Velocity = Vector2.UnitX.Rotate(rotation) * speed;

            world.EntityAdd(shot);

            //CAudio.PlaySound("Shot", 1.0f);

            return shot;
        }

        public override void Initialize(CWorld world, CShip owner, float damage)
        {
            base.Initialize(world, owner, damage);

            Physics = new CPhysics();
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Weapons/EnemyMiniShot");
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 1.0f);
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.Position;
        }

        public void OnCollide(CShip ship)
        {
            if (IsReflected)
                return;

            if (ship.IsIgnoreBullets > 0)
                return;

            if (ship.IsReflectBullets > 0)
            {
                Reflect(ship);
                return;
            }

            if (ship.IsAbsorbBullets > 0)
            {
                ship.AbsorbBullet(this);
                Die();
                return;
            }

            World.Stats.ShotDamageReceived += Damage;
            ship.TakeDamage(Physics.Position, Physics.Velocity, Damage);
            Die();
        }

        protected override void OnDie()
        {
            World.ParticleEffects.Spawn(EParticleType.EnemyShotHit, Physics.Position);
            base.OnDie();
        }

        private void Reflect(CShip ship)
        {
            Vector2 from_ship = Physics.Position - ship.Physics.Position;
            Vector2 reflect = from_ship.Normal();
            Vector2 new_velocity = reflect * Physics.Velocity.Length();
            Physics.Velocity = new_velocity;
            IsReflected = true;
            WhoReflected = ship;
        }
    }
}
