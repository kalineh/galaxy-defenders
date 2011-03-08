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
        public CShip WhoReflected { get; set; }

        public static CEnemyPellet Spawn(CWorld world, Vector2 position, float rotation, float speed, float damage)
        {
            CEnemyPellet pellet = new CEnemyPellet();

            pellet.Initialize(world);
            pellet.Physics.Position = position;
            pellet.Physics.Velocity = Vector2.UnitX.Rotate(rotation) * speed;
            pellet.Physics.Rotation = rotation;
            pellet.Physics.AngularVelocity = 0.45f * world.Random.NextSign();
            pellet.Damage = damage;

            world.EntityAdd(pellet);

            return pellet;
        }

        public float Damage { get; private set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Weapons/EnemyPellet");
            Collision = new CollisionCircle(Vector2.Zero, 14.0f);

            Health = 3;
        }

        public override void UpdateCollision()
        {
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

            World.ParticleEffects.Spawn(EParticleType.EnemyShotHit, Physics.Position);
            World.Stats.ShotDamageReceived += Damage;
            ship.TakeDamage(Physics.Position, Physics.Velocity, Damage);
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

        public void OnCollide(CFlame flame)
        {
            flame.Die();
            Hit();
        }

        public void OnCollide(CLightning lightning)
        {
            lightning.Die();
            Hit();
        }

        private void Hit()
        {
            Health -= 1;
            if (Health <= 0)
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
