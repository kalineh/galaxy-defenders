//
// EnemyCannonShot.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CEnemyCannonShot
        : CProjectile
    {
        public bool IsReflected { get; set; }
        public CShip WhoReflected { get; set; }
        public int CanCollideWait { get; set; }

        public static CEnemyCannonShot Spawn(CWorld world, Vector2 position, float rotation, float speed, float damage)
        {
            CEnemyCannonShot shot = new CEnemyCannonShot();
            shot.Initialize(world, null, damage);

            shot.Physics.AnglePhysics.Rotation = rotation;
            shot.Physics.PositionPhysics.Position = position;
            shot.Physics.PositionPhysics.Velocity = Vector2.UnitX.Rotate(rotation) * speed;

            world.EntityAdd(shot);

            //CAudio.PlaySound("Shot", 1.0f);

            return shot;
        }

        public override void Initialize(CWorld world, CShip owner, float damage)
        {
            base.Initialize(world, owner, damage);

            Physics = new CPhysics();
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Weapons/EnemyCannonShot");
            Collision = CCollision.GetCacheAABB(this, Vector2.Zero, new Vector2(11.0f, 42.0f));
        }

        public override void Update()
        {
            base.Update();

            CanCollideWait = Math.Max(0, CanCollideWait - 1);

            if (!IsInScreen())
                Delete();
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionAABB box = Collision as CollisionAABB;
            box.Position = Physics.PositionPhysics.Position - new Vector2(6.0f, 21.0f);
        }

        public void OnCollide(CShip ship)
        {
            if (IsReflected)
                return;

            if (ship.IsIgnoreBullets > 0)
                return;

            if (ship.IsReflectBullets > 0)
                return;

            if (ship.IsAbsorbBullets > 0)
                return;

            if (CanCollideWait > 0)
                return;

            World.Stats.ShotDamageReceived += Damage;
            ship.TakeDamage(Damage);

            Vector2 ofs = ship.Physics.PositionPhysics.Position - Physics.PositionPhysics.Position;
            Vector2 dir = ofs.Normal();
            Vector2 force = dir * 5.0f;

            ship.Physics.PositionPhysics.Velocity += force;

            CanCollideWait = 3;
        }
    }
}
