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
        public static Color EnemyOrangeColor { get; set; }
        public static Color EnemyGrayColor { get; set; }
        public static string[] EnemyDeathSoundStrings = new string[] { "EnemyDie1", "EnemyDie2", "EnemyDie3" };

        static CEnemy()
        {
            EnemyOrangeColor = new Color(252, 124, 85, 255);
            EnemyGrayColor = new Color(102, 102, 102, 255);
        }

        private float _HealthMax;
        public float HealthMax
        {
            get { return _HealthMax; }
            set
            {
                int difficulty = CSaveData.GetCurrentGameData(World.Game).Difficulty;
                _HealthMax = value * CDifficulty.HealthScale[difficulty];
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

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            BaseScore = 50;
            CanSeekerTarget = true;
        }

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
            circle.Position = Physics.Position;
            circle.Radius = Visual.GetScaledTextureSize().Length() * 0.2f;
        }

        public virtual void TakeDamage(float damage, CShip source)
        {
            if (World.Ships.Count > 1)
            {
                damage *= 0.5f + 0.5f / World.Ships.Count;
            }

            Health -= damage;
            if (Health <= 0.0f)
            {
                World.Stats.EnemyKills += 1;
                source.Score += CalculateScoreFromHealth();
                Die();
            }
        }

        public void OnCollide(CShip source)
        {
            World.Stats.CollisionDamageReceived += 2.5f;
            source.TakeCollideDamage(Physics.Position, Physics.Velocity, 2.5f);

            World.Stats.CollisionDamageDealt += 0.5f;
            TakeDamage(0.5f, source);
        }

        // TODO: need to handle IsSubClassOf in the collision system
        // TODO: replace with generic CWeapon collider
        public void OnCollide(CLaser laser)
        {
            World.Stats.ShotDamageDealt += laser.Damage;
            TakeDamage(laser.Damage, laser.Owner);
            laser.Die();
        }

        public void OnCollide(CMissile missile)
        {
            World.Stats.ShotDamageDealt += missile.Damage;
            TakeDamage(missile.Damage, missile.Owner);
            missile.Die();
        }

        public void OnCollide(CSeekBomb seek_bomb)
        {
            if (CanSeekerTarget == false)
                return;

            World.Stats.ShotDamageDealt += seek_bomb.Damage;
            TakeDamage(seek_bomb.Damage, seek_bomb.Owner);
            seek_bomb.Die();
        }

        public void OnCollide(CPlasma plasma)
        {
            World.Stats.ShotDamageDealt += plasma.Damage;
            TakeDamage(plasma.Damage, plasma.Owner);
            plasma.Die();
        }

        public void OnCollide(CMiniShot minishot)
        {
            World.Stats.ShotDamageDealt += minishot.Damage;
            TakeDamage(minishot.Damage, minishot.Owner);
            minishot.Die();
        }

        public void OnCollide(CDetonation detonation)
        {
            TakeDamage(5.0f, detonation.Owner);
        }

        public void OnCollide(CEnemyShot shot)
        {
            if (!shot.IsReflected)
                return;

            TakeDamage(shot.Damage, shot.WhoReflected);
            shot.Die();
        }

        public void OnCollide(CEnemyLaser laser)
        {
            if (!laser.IsReflected)
                return;

            TakeDamage(laser.Damage, laser.WhoReflected);
            laser.Die();
        }
        
        // NOTE: design-wise, no missile reflection? 

        public void OnCollide(CEnemyPellet pellet)
        {
            if (!pellet.IsReflected)
                return;

            TakeDamage(pellet.Damage, pellet.WhoReflected);
            pellet.Die();
        }

        private int CalculateScoreFromHealth()
        {
            int difficulty = CSaveData.GetCurrentGameData(World.Game).Difficulty;
            float s = (float)BaseScore * HealthMax;
            float d = s * CDifficulty.MoneyScale[difficulty];
            int score = (int)d;
            return score - score % 10;
        }
        
        protected override void OnDie()
        {
            string death_sound = EnemyDeathSoundStrings[World.Random.Next() % 3];
            CAudio.PlaySound(death_sound);

            World.ParticleEffects.Spawn(EParticleType.EnemyDeathExplosion, Physics.Position, CEnemy.EnemyOrangeColor, null, World.ScrollSpeed * -Vector2.UnitY * 0.5f);
            World.ParticleEffects.Spawn(EParticleType.EnemyDeathExplosion, Physics.Position, CEnemy.EnemyGrayColor, null, World.ScrollSpeed * -Vector2.UnitY * 0.5f);

            int big_coins = Coins / 10;
            for (int i = 0; i < big_coins; i++)
            {
                CBigBonus bonus = new CBigBonus();
                bonus.Initialize(World);
                bonus.Physics.Position = Physics.Position;
                World.EntityAdd(bonus);
            }

            int small_coins = Coins - big_coins * 10;
            for (int i = 0; i < small_coins; i++)
            {
                CBonus bonus = new CBonus();
                bonus.Initialize(World);
                bonus.Physics.Position = Physics.Position;
                World.EntityAdd(bonus);
            }

            if (Powerup)
            {
                CPowerup powerup = new CPowerup();
                powerup.Initialize(World);
                powerup.Physics.Position = Physics.Position;
                World.EntityAdd(powerup);
            }

            GenerateCorpse();

            base.OnDie();
        }

        protected virtual bool DoesGenerateCorpse()
        {
            return false;
        }

        protected virtual void GenerateCorpse()
        {
            if (!DoesGenerateCorpse())
            {
                return;
            }

            if (Mover == null)
            {
                // TODO: cache
                CDecoration corpse = new CDecoration();
                corpse.Initialize(World);
                corpse.Physics.Position = Physics.Position;
                corpse.Visual = CVisual.MakeSpriteCached1(World.Game, Visual.Texture.Name + "Dead");
                World.EntityAdd(corpse);
            }
        }
    }
}

