//
// Enemy.cs
//

using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Galaxy
{
    public class CEnemy
        : CEntity
    {
        public float Health { get; set; }
        public int BonusDrop { get; set; }

        public CEnemy(CWorld world, String name)
            : base(world, name)
        {
            Physics = new CPhysics();
            BonusDrop = 3;
        }

        public virtual void UpdateAI()
        {
            float t = World.Game.GameFrame * 0.05f;
            float x = (float)Math.Cos(t) * 4.0f;
            float y = 2.0f;
            Physics.PositionPhysics.Velocity = new Vector2(x, y);
        }

        public override void Update()
        {
            UpdateAI();

            base.Update();

            if (IsOffScreenBottom())
                Die();
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.PositionPhysics.Position;
            circle.Radius = Visual.GetScaledTextureSize().Length() * 0.2f;
        }

        public void TakeDamage(float damage)
        {
            Health -= damage;
            if (Health <= 0.0f)
            {
                foreach (int i in Enumerable.Range(0, BonusDrop))
                {
                    World.EntityAdd(new CBonus(World, Physics.PositionPhysics.Position));
                }

                Die();
            }
        }

        public void OnCollide(CShip ship)
        {
            ship.Die();
        }

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
        
        protected override void OnDie()
        {
            CExplosion.Spawn(World, Physics.PositionPhysics.Position, 1.0f);
            base.OnDie();
        }
    }
}

