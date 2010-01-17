//
// Turret.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Galaxy
{
    public class CTurret
        : CEnemy
    {
        public float FireDelay { get; private set; }
        private int FireCooldown { get; set; }
        public float FireDamage { get; private set; }
        public float FireSpeed { get; private set; }

        public CTurret(CWorld world, Vector2 position)
            : base(world, "Turret")
        {
            Physics = new CPhysics();
            Physics.PositionPhysics.Position = position;
            Collision = new CollisionCircle(Vector2.Zero, 28.0f);
            Visual = new CVisual(world.Game.Content.Load<Texture2D>("Turret"), Color.White);
            Health = 3.0f;

            FireDelay = 4.0f;
            FireCooldown = Time.ToFrames(FireDelay);
            FireDamage = 1.0f;
            FireSpeed = 3.0f;
        }

        public override void UpdateAI()
        {
            UpdateFire();
        }

        private void UpdateFire()
        {
            FireCooldown -= 1;
            if (FireCooldown <= 0)
            {
                Fire();
            }
        }

        private void Fire()
        {
            // TODO: this math is weird and broken
            // TODO: replace with matrices
            Vector2 position = Physics.PositionPhysics.Position;
            float rotation = Vector2.UnitY.ToAngle();

            CShip ship = World.GetNearestShip(position);
            if (ship != null)
            {
                Vector2 offset = ship.Physics.PositionPhysics.Position - position;
                rotation = offset.ToAngle();
            }

            Vector2 dir = Physics.AnglePhysics.GetDir();
            Vector2 fire_offset = dir * 2.0f + dir.Perp() * 16.0f;
            Vector2 fire_position = position + fire_offset;

            CEnemyShot shot = CEnemyShot.Spawn(World, fire_position, rotation, FireSpeed, FireDamage);
            FireCooldown = Time.ToFrames(FireDelay);
        }

        public override void Update()
        {
            UpdateAI();

            base.Update();
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.PositionPhysics.Position;
        }
    }
}

