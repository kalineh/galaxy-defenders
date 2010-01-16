//
// PewPew.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Galaxy
{
    public class CPewPew
        : CEnemy
    {
        public float FireDelay { get; private set; }
        private int FireCooldown { get; set; }
        public float FireDamage { get; private set; }
        public float FireSpeed { get; private set; }

        public CPewPew(CWorld world, Vector2 position)
            : base(world, "PewPew")
        {
            Physics = new CPhysics();
            Physics.PositionPhysics.Position = position;
            Collision = new CollisionCircle(Vector2.Zero, 32.0f);
            Visual = new CVisual(world.Game.Content.Load<Texture2D>("PewPew"), Color.White);

            FireDelay = 1.0f;
            FireCooldown = Time.ToFrames(FireDelay);
            FireDamage = 1.0f;
            FireSpeed = 4.0f;
        }

        public override void UpdateAI()
        {
            float t = World.Game.GameFrame * 0.05f;
            float x = (float)Math.Cos(t) * 1.0f;
            float y = 0.2f;
            Physics.PositionPhysics.Velocity = new Vector2(x, y);

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
            Vector2 dir = Physics.AnglePhysics.GetDir();
            Vector2 fire_offset = dir * 8.0f + dir.Perp() * 16.0f;
            Vector2 fire_position = position + fire_offset;

            CEnemyLaser laser = CEnemyLaser.Spawn(World, fire_position, rotation, FireSpeed, FireDamage);
            FireCooldown = Time.ToFrames(FireDelay);
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
        }
    }
}

