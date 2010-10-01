//
// EnemyMissile.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CEnemyMissile
        : CEntity
    {
        public bool IsReflected { get; set; }
        public CShip Target { get; set; }
        public float Speed { get; set; }
        public int Health { get; set; }
        public CParticleGroupSpawner TrailEffect { get; set; }

        public static CEnemyMissile Spawn(CWorld world, Vector2 position, float rotation, float speed, float damage)
        {
            CEnemyMissile missile = new CEnemyMissile();

            missile.Initialize(world);
            missile.Physics.PositionPhysics.Position = position;
            missile.Physics.PositionPhysics.Velocity = Vector2.UnitX.Rotate(rotation) * speed;
            missile.Physics.AnglePhysics.Rotation = rotation;
            missile.Damage = damage;
            missile.Speed = speed;

            world.EntityAdd(missile);

            return missile;
        }

        public float Damage { get; private set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Visual = CVisual.MakeSpriteCached1(world, "Textures/Weapons/EnemyMissile");
            Collision = new CollisionCircle(Vector2.Zero, 10.0f);

            Health = 4;

            TrailEffect = CParticleGroupSpawner.MakeEnemyMissileTrail(Vector2.Zero, Vector2.UnitY, CEnemy.EnemyOrangeColor);
        }

        public override void Update()
        {
            base.Update();

            TrailEffect.Position = Physics.PositionPhysics.Position + GetEffectOffset();
            TrailEffect.Spawn(World.ParticleEffects);

            UpdateTargeting();
            UpdateTrajectory();

            if (!IsInScreen())
                Delete();
        }

        private Vector2 GetEffectOffset()
        {
            return Physics.AnglePhysics.GetDir() * -10.0f;
        }

        private void UpdateTargeting()
        {
            if (Target != null && Target.IsDead)
            {
                Target = null;
                return;
            }

            if (Target != null)
                return;

            CShip nearest = World.GetNearestShip(Physics.PositionPhysics.Position, 2000.0f);
            Target = nearest;
        }

        private void UpdateTrajectory()
        {
            if (Target == null)
                return;

            Vector2 ofs = Target.Physics.PositionPhysics.Position - Physics.PositionPhysics.Position;
            Vector2 fwd = Physics.AnglePhysics.GetDir();
            float dot = Vector2.Dot(ofs, fwd.Perp());
            Physics.AnglePhysics.AngularVelocity = Math.Sign(dot) * 0.01f;
            Physics.PositionPhysics.Velocity = Physics.AnglePhysics.GetDir() * Speed;
        }

        public override void UpdateCollision()
        {
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.PositionPhysics.Position;
        }

        public void OnCollide(CShip ship)
        {
            if (ship.IsReflectBullets > 0)
            {
                Die();
                return;
            }

            World.Stats.ShotDamageReceived += Damage;
            ship.TakeDamage(Damage);

            CEffect.MissileExplosion(World, Physics.PositionPhysics.Position, 2.5f, CEnemy.EnemyOrangeColor);
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
            {
                CAudio.PlaySound("WeaponHitMissile", 1.0f);
                Die();
            }
        }

        protected override void OnDie()
        {
            CEffect.MissileExplosion(World, Physics.PositionPhysics.Position, 2.5f, CEnemy.EnemyOrangeColor);
            base.OnDie();
        }
    }
}
