//
// Asteroid.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace galaxy
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
        private CVisual Cracks { get; set; }

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

            if (!IsInScreen() && Physics.PositionPhysics.Position.Y > 0.0f)
                Delete();
        }

        public override void Draw(SpriteBatch sprite_batch)
        {
            base.Draw(sprite_batch);
            Cracks.Draw(sprite_batch, Physics.PositionPhysics.Position, Physics.AnglePhysics.Rotation);
        }

        public void OnCollide(CShip ship)
        {
            ship.Die();
        }

        public void OnCollide(CLaser laser)
        {
            Health -= laser.Damage;
            Physics.PositionPhysics.Velocity += laser.Physics.AnglePhysics.GetDir() * laser.Damage;
            Cracks.Alpha = 1.0f - Health / HealthMax;
            if (Health < 0.0f)
            {
                Die();
            }
        }

        protected override void OnDie()
        {
            CExplosion.Spawn(World, Physics.PositionPhysics.Position);
            World.Score += 100;
            base.OnDie();
        }

    }
}
