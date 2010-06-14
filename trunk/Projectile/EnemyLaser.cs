//
// EnemyLaser.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CEnemyLaser
        : CEntity
    {
        public bool IsReflected { get; set; }

        public static CEnemyLaser Spawn(CWorld world, Vector2 position, float rotation, float speed, float damage)
        {
            CEnemyLaser laser = new CEnemyLaser(world, damage);

            laser.Physics.AnglePhysics.Rotation = rotation;
            laser.Physics.PositionPhysics.Position = position;
            laser.Physics.PositionPhysics.Velocity = Vector2.UnitX.Rotate(rotation) * speed;

            world.EntityAdd(laser);

            return laser;
        }

        public float Damage { get; private set; }

        public CEnemyLaser(CWorld world, float damage)
            : base(world)
        {
            Physics = new CPhysics();
            Visual = new CVisual(world, CContent.LoadTexture2D(world.Game, "Textures/Weapons/EnemyLaser"), Color.White);
            Collision = new CollisionAABB(Vector2.Zero, new Vector2(1.0f, 0.5f));
            Damage = damage;
        }

#if XBOX360
        public CEnemyLaser()
        {
        }

        public void Init360(CWorld world, float damage)
        {
            base.Init360(world);

            Physics = new CPhysics();
            Visual = new CVisual(world, CContent.LoadTexture2D(world.Game, "Textures/Weapons/EnemyLaser"), Color.White);
            Collision = new CollisionAABB(Vector2.Zero, new Vector2(1.0f, 0.5f));
            Damage = damage;
        }
#endif

        public override void Update()
        {
            base.Update();

            if (!IsInScreen())
                Delete();
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionAABB box = Collision as CollisionAABB;
            box.Position = Physics.PositionPhysics.Position;
        }

        public void OnCollide(CShip ship)
        {
            if (IsReflected)
                return;

            if (ship.IsReflectBullets > 0)
            {
                Reflect(ship);
                return;
            }

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
            Physics.AnglePhysics.Rotation = new_velocity.ToAngle();
        }
    }
}
