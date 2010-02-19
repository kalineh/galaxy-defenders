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
        public float Health { get; set; }
        public int Coins { get; set; }
        public bool Powerup { get; set; }

        public CEnemy(CWorld world, String name)
            : base(world, name)
        {
            Physics = new CPhysics();
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
            Health -= damage;
            if (Health <= 0.0f)
            {
                Die();
            }
        }

        public void OnCollide(CShip ship)
        {
            ship.TakeCollideDamage(Physics.PositionPhysics.Position, 1.0f);
            TakeDamage(1.0f);
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

            foreach (int i in Enumerable.Range(0, Coins))
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

