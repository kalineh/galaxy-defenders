//
// Shot.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CEnemyShot
        : CProjectile
    {
        public bool IsReflected { get; set; }

        public static CEnemyShot Spawn(CWorld world, Vector2 position, float rotation, float speed, float damage)
        {
            CEnemyShot shot = new CEnemyShot();
            shot.Initialize(world, PlayerIndex.One, damage);

            shot.Physics.AnglePhysics.Rotation = rotation;
            shot.Physics.PositionPhysics.Position = position;
            shot.Physics.PositionPhysics.Velocity = Vector2.UnitX.Rotate(rotation) * speed;

            world.EntityAdd(shot);

            //CAudio.PlaySound("Shot", 1.0f);

            return shot;
        }

        public override void Initialize(CWorld world, PlayerIndex index, float damage)
        {
            base.Initialize(world, index, damage);

            Physics = new CPhysics();
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Weapons/EnemyShot");
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 2.0f);
        }

        public override void Update()
        {
            base.Update();

            if (!IsInScreen())
                Delete();
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle box = Collision as CollisionCircle;
            box.Position = Physics.PositionPhysics.Position;
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
            ship.TakeDamage(Damage);
            Die();
        }

        private void Reflect(CShip ship)
        {
            Vector2 from_ship = Physics.PositionPhysics.Position - ship.Physics.PositionPhysics.Position;
            Vector2 reflect = from_ship.Normal();
            Vector2 new_velocity = reflect * Physics.PositionPhysics.Velocity.Length();
            Physics.PositionPhysics.Velocity = new_velocity;
            IsReflected = true;
        }
    }
}
