//
// EnemyPellet.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CEnemyPellet
        : CEntity
    {
        private int Health { get; set; }
        public bool IsReflected { get; set; }

        public static CEnemyPellet Spawn(CWorld world, Vector2 position, float rotation, float speed, float damage)
        {
            CEnemyPellet pellet = new CEnemyPellet();

            pellet.Initialize(world);
            pellet.Physics.PositionPhysics.Position = position;
            pellet.Physics.PositionPhysics.Velocity = Vector2.UnitX.Rotate(rotation) * speed;
            pellet.Physics.AnglePhysics.Rotation = rotation;
            pellet.Physics.AnglePhysics.AngularVelocity = 0.45f * world.Random.RandomSign();
            pellet.Damage = damage;

            world.EntityAdd(pellet);

            return pellet;
        }

        public float Damage { get; private set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Visual = CVisual.MakeSpriteCached1(world, "Textures/Weapons/EnemyPellet");
            Collision = new CollisionCircle(Vector2.Zero, 14.0f);

            Health = 3;
        }

        public override void Update()
        {
            base.Update();

            if (!IsInScreen())
                Delete();
        }

        public override void UpdateCollision()
        {
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.PositionPhysics.Position;
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
            ship.TakeDamage(Damage);
            Die();
        }

        public void OnCollide(CLaser laser)
        {
            laser.Die();
            Hit();
        }

        public void OnCollide(CMissile missile)
        {
            missile.Die();
            Hit();
        }

        public void OnCollide(CSeekBomb seek_bomb)
        {
            seek_bomb.Die();
            Hit();
        }

        public void OnCollide(CPlasma plasma)
        {
            plasma.Die();
            Hit();
        }

        public void OnCollide(CMiniShot minishot)
        {
            minishot.Die();
            Hit();
        }

        public void OnCollide(CDetonation detonation)
        {
            Die();
        }

        private void Hit()
        {
            Health -= 1;
            if (Health <= 0)
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
