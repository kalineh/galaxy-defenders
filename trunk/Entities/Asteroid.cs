//
// Asteroid.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CAsteroid
        : CEntity
    {
        private float _HealthMax;
        public float HealthMax
        {
            get { return _HealthMax; }
            set { _HealthMax = value; Health = value; }
        }

        public float Health { get; private set; }
        public CVisual Cracks { get; set; }

        public CAsteroid(CWorld world, Vector2 position)
            : base(world, "Asteroid")
        {
            Physics = new CPhysics();
            Physics.PositionPhysics.Position = position;
            Collision = new CollisionCircle(Vector2.Zero, 16.0f);
            Visual = new CVisual(world.Game.Content.Load<Texture2D>("Asteroid"), Color.White);
            Cracks = new CVisual(world.Game.Content.Load<Texture2D>("Cracks"), Color.White);
            Cracks.Alpha = 0.0f;
        }

        public override void Update()
        {
            base.Update();

            Cracks.Update();

            if (IsOffScreenBottom())
                Delete();
        }

        public override void Draw(SpriteBatch sprite_batch)
        {
            base.Draw(sprite_batch);
            Cracks.Draw(sprite_batch, Physics.PositionPhysics.Position, Physics.AnglePhysics.Rotation);
        }

        public void OnCollide(CShip ship)
        {
            ship.TakeCollideDamage(Physics.PositionPhysics.Position, 1.0f);
            Health -= 1.0f;
        }

        public void OnCollide(CLaser laser)
        {
            Physics.PositionPhysics.Velocity += laser.Physics.AnglePhysics.GetDir() * laser.Damage;
            TakeDamage(laser.Damage);
        }

        public void OnCollide(CMissile missile)
        {
            Physics.PositionPhysics.Velocity += missile.Physics.AnglePhysics.GetDir() * missile.Damage;
            TakeDamage(missile.Damage);
        }

        protected override void OnDie()
        {
            CExplosion.Spawn(World, Physics.PositionPhysics.Position, 1.0f);
            World.Score += 100;
            base.OnDie();
        }

        private void TakeDamage(float damage)
        {
            Health -= damage;
            Cracks.Alpha = 1.0f - Health / HealthMax;
            if (Health < 0.0f)
            {
                Die();
            }
        }

    }
}
