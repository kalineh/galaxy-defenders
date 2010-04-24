//
// Enemy.cs
//

using System;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Galaxy
{
    public class CEnemy
        : CEntity
    {
        private float _HealthMax;
        public float HealthMax
        {
            get { return _HealthMax; }
            set { _HealthMax = value; Health = value; }
        }

        public float Health { get; private set; }
        public int Coins { get; set; }
        public bool Powerup { get; set; }
        private int BaseScore { get; set; }

        public CEnemy(CWorld world)
            : base(world)
        {
            Physics = new CPhysics();
            BaseScore = 10;
        }

#if XBOX360
        public CEnemy()
        {
        }

        public new void Init360(CWorld world)
        {
            base.Init360(world);

            Physics = new CPhysics();
            BaseScore = 10;
        }
#endif

        public virtual void UpdateAI()
        {
            float t = World.Game.GameFrame * 0.05f;
            float x = (float)Math.Cos(t) * 4.0f;
            float y = 2.0f;
            Physics.PositionPhysics.Velocity = new Vector2(x, y);
        }

        public override void Update()
        {
            // TODO: hack till we get proper generation of StageElements based on camera
            if (World.GameCamera.IsAboveActiveRegion(Physics.PositionPhysics.Position))
            {
                // TODO: we need to do this or all collision bounds will be at 0,0
                UpdateCollision();
                return;
            }

            UpdateAI();

            base.Update();

            //if (World.Game.State.GetType() == typeof(CStateGame))
            //{
                //Physics.PositionPhysics.Position += World.Game.StageDefinition.ScrollSpeed * -Vector2.UnitY;
            //}

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
        
        public void OnCollide(CMissile missile)
        {
            TakeDamage(missile.Damage);
            missile.Die();
        }

        public void OnCollide(CSeekBomb seek_bomb)
        {
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

        private int CalculateScoreFromHealth()
        {
            int base_ = BaseScore * (int)HealthMax;
            return base_ - base_ % 10;
        }
        
        protected override void OnDie()
        {
            // TODO: texture offset is not centered nicely? (enemy textures just offset maybe?
            CEffect.EnemyExplosion(World, Physics.PositionPhysics.Position, 1.5f);

            World.Score += CalculateScoreFromHealth();

            for (int i = 0; i < Coins; i++)
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

