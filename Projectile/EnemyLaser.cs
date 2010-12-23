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
        public CShip WhoReflected { get; set; }

        public static CEnemyLaser Spawn(CWorld world, Vector2 position, float rotation, float speed, float damage)
        {
            CEnemyLaser laser = new CEnemyLaser();

            laser.Initialize(world);
            laser.Physics.Position = position;
            laser.Physics.Velocity = Vector2.UnitX.Rotate(rotation) * speed;
            laser.Physics.Rotation = rotation;
            laser.Damage = damage;

            world.EntityAdd(laser);

            return laser;
        }

        public float Damage { get; private set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Weapons/EnemyLaser");
            Collision = new CollisionAABB(Vector2.Zero, new Vector2(1.0f, 0.5f));
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
            CollisionAABB box = Collision as CollisionAABB;
            box.Position = Physics.Position;
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

            World.Stats.ShotDamageReceived += Damage;
            ship.TakeDamage(Physics.Position, Physics.Velocity, Damage);
            Die();
        }

        private void Reflect(CShip ship)
        {
            Vector2 from_ship = Physics.Position - ship.Physics.Position;
            Vector2 reflect = from_ship.Normal();
            Vector2 new_velocity = reflect * Physics.Velocity.Length();
            Physics.Velocity = new_velocity;
            IsReflected = true;
            Physics.Rotation = new_velocity.ToAngle();
        }
    }
}
