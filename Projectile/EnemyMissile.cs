﻿//
// EnemyMissile.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CEnemyMissile
        : CEntity
        , ICacheableProjectile
    {
        public bool IsReflected { get; set; }
        public CShip WhoReflected { get; set; }
        public CShip Target { get; set; }
        public float Speed { get; set; }
        public int Health { get; set; }

        public static CEnemyMissile Spawn(CWorld world, Vector2 position, float rotation, float speed, float damage)
        {
            CEnemyMissile missile = ProjectileCacheManager.EnemyMissiles.GetProjectileInstance(GameControllerIndex.One);

            missile.Initialize(world);
            missile.Physics.Position = position;
            missile.Physics.Velocity = Vector2.UnitX.Rotate(rotation) * speed;
            missile.Physics.Rotation = rotation;
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
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Weapons/EnemyMissile");
            Collision = new CollisionCircle(Vector2.Zero, 10.0f);

            Health = 4;

            IsReflected = false;
            WhoReflected = null;
            Target = null;
        }

        public override void Update()
        {
            base.Update();

            World.ParticleEffects.Spawn(EParticleType.EnemyMissileTrail, Physics.Position + GetEffectOffset());

            UpdateTargeting();
            UpdateTrajectory();

            if (!IsInScreen())
                Delete();
        }

        private Vector2 GetEffectOffset()
        {
            return Physics.GetDir() * -10.0f;
        }

        private void UpdateTargeting()
        {
            if (Target != null && Target.IsDead)
            {
                Target = null;
                Physics.AngularVelocity = 0.0f;
                return;
            }

            if (Target != null)
                return;

            CShip nearest = World.GetNearestShip(Physics.Position, 2000.0f);
            Target = nearest;
        }

        private void UpdateTrajectory()
        {
            if (Target == null)
                return;

            Vector2 ofs = Target.Physics.Position - Physics.Position;
            Vector2 fwd = Physics.GetDir();
            float dot = Vector2.Dot(ofs, fwd.Perp());
            Physics.AngularVelocity = Math.Sign(dot) * 0.0035f;
            Physics.Velocity = Physics.GetDir() * Speed;

            Speed += 0.075f;
        }

        public override void UpdateCollision()
        {
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.Position;
        }

        public void OnCollide(CShip ship)
        {
            if (ship.IsIgnoreBullets > 0)
                return;

            if (ship.IsReflectBullets > 0)
            {
                Die();
                return;
            }

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

        public void OnCollide(CFlame flame)
        {
            flame.Die();
            Hit();
        }

        public void OnCollide(CDetonation detonation)
        {
            Die();
        }

        public void OnCollide(CBomblet bomblet)
        {
            bomblet.Die();
            Die();
        }

        private void Hit()
        {
            // NOTE: unkillable more fun, probably
            //Health -= 1;
            //if (Health <= 0)
            //{
                //CAudio.PlaySound("WeaponHitMissile", 1.0f);
                //Die();
            //}
        }

        protected override void OnDie()
        {
            World.ParticleEffects.Spawn(EParticleType.WeaponMissileHit, Physics.Position, CEnemy.EnemyOrangeColor, null, null);
            base.OnDie();
        }
    }
}
