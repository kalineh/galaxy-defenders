//
// Enemy.cs
//

using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CEnemy
        : CEntity
    {
        private float _HealthMax;
        public float HealthMax
        {
            get { return _HealthMax; }
            set
            { 
                _HealthMax = value * CDifficulty.HealthScale[CSaveData.GetCurrentProfile().Difficulty];
                Health = _HealthMax;
            }
        }

        public float Health { get; set; }
        public int Coins { get; set; }
        public bool Powerup { get; set; }
        private int BaseScore { get; set; }

        // TODO: not good in enemy class?
        public bool CanSeekerTarget { get; set; }
        public bool IsSeekerTarget { get; set; }

        public CEnemy(CWorld world)
            : base(world)
        {
            Physics = new CPhysics();
            BaseScore = 50;
            CanSeekerTarget = true;
        }

#if XBOX360
        public CEnemy()
        {
        }

        public new void Init360(CWorld world)
        {
            base.Init360(world);

            Physics = new CPhysics();
            BaseScore = 50;
            CanSeekerTarget = true;
        }
#endif

        public virtual void UpdateAI()
        {
        }

        public override void Update()
        {
            UpdateAI();

            base.Update();

            if (IsInDieRegion())
                Delete();
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.PositionPhysics.Position;
            circle.Radius = Visual.GetScaledTextureSize().Length() * 0.2f;
        }

        public virtual void TakeDamage(float damage)
        {
            if (World.Players.Count > 1)
            {
                damage *= 0.5f + 0.5f / (float)World.Players.Count;
            }

            Health -= damage;
            if (Health <= 0.0f)
            {
                Die();
            }
        }

        public void OnCollide(CShip ship)
        {
            ship.TakeCollideDamage(Physics.PositionPhysics.Position, 1.5f);
            TakeDamage(0.5f);
        }

        // TODO: need to handle IsSubClassOf in the collision system
        // TODO: replace with generic CWeapon collider
        public void OnCollide(CLaser laser)
        {
            TakeDamage(laser.Damage);
            laser.Die();
        }

        public void OnCollide(CBigLaser laser)
        {
            TakeDamage(laser.Damage);
            laser.Die();
        }
        
        public void OnCollide(CMissile missile)
        {
            TakeDamage(missile.Damage);
            missile.Die();
        }

        public void OnCollide(CSeekBomb seek_bomb)
        {
            if (CanSeekerTarget == false)
                return;

            TakeDamage(seek_bomb.Damage);
            seek_bomb.Die();
        }

        public void OnCollide(CSmallPlasma plasma)
        {
            TakeDamage(plasma.Damage);
            plasma.Die();
        }

        public void OnCollide(CBigPlasma plasma)
        {
            TakeDamage(plasma.Damage);
            plasma.Die();
        }

        public void OnCollide(CMiniShot minishot)
        {
            TakeDamage(minishot.Damage);
            minishot.Die();
        }

        public void OnCollide(CDetonation detonation)
        {
            TakeDamage(5.0f);
        }

        public void OnCollide(CEnemyShot shot)
        {
            if (!shot.IsReflected)
                return;

            TakeDamage(shot.Damage);
            shot.Die();
        }

        public void OnCollide(CEnemyLaser laser)
        {
            if (!laser.IsReflected)
                return;

            TakeDamage(laser.Damage);
            laser.Die();
        }

        private int CalculateScoreFromHealth()
        {
            float s = (float)BaseScore * HealthMax;
            float d = s * CDifficulty.MoneyScale[CSaveData.GetCurrentProfile().Difficulty];
            int score = (int)d;
            return score - score % 10;
        }
        
        protected override void OnDie()
        {
            // TODO: texture offset is not centered nicely? (enemy textures just offset maybe?
            CEffect.EnemyExplosion(World, Physics.PositionPhysics.Position, 1.5f);

            World.Score += CalculateScoreFromHealth();

            int big_coins = Coins / 10;
            for (int i = 0; i < big_coins; i++)
            {
                World.EntityAdd(new CBigBonus(World, Physics.PositionPhysics.Position));
            }

            int small_coins = Coins - big_coins * 10;
            for (int i = 0; i < small_coins; i++)
            {
                World.EntityAdd(new CBonus(World, Physics.PositionPhysics.Position));
            }

            if (Powerup)
            {
                World.EntityAdd(new CPowerup(World, Physics.PositionPhysics.Position));
            }

            base.OnDie();
        }
    }
}

