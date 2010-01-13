//
// Asteroid.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace galaxy
{
    class CAsteroid
        : CEntity
    {
        struct Settings
        {
            public float SpeedBase;
            public float SpeedRandom;
            public float SpeedHorizontalScale;
            public float SpawnHeightRange;
            public float AngleSpeedBase;
            public float AngleSpeedRandom;
            public float ScaleBase;
            public float ScaleRandom;
            public float HealthBase;
        };
        static Settings SSettings;

        static CAsteroid()
        {
            SSettings.SpeedBase = 3.0f;
            SSettings.SpeedRandom = 3.0f;
            SSettings.SpeedHorizontalScale = 0.15f;
            SSettings.SpawnHeightRange = 300.0f;
            SSettings.AngleSpeedBase = 0.01f;
            SSettings.AngleSpeedRandom = 0.03f;
            SSettings.ScaleBase = 0.9f;
            SSettings.ScaleRandom = 0.2f;
            SSettings.HealthBase = 2.0f;
        }

        public static CAsteroid Spawn(CWorld world)
        {
            Texture2D texture = world.Game.Content.Load<Texture2D>("Asteroid");
            CAsteroid asteroid = new CAsteroid(world, "Asteroid", texture);

            asteroid.Physics.PositionPhysics.Position = new Vector2(
                world.Random.NextFloat() * world.Game.Window.ClientBounds.Width,
                world.Random.NextFloat() * SSettings.SpawnHeightRange * -1.0f
            );

            asteroid.Physics.PositionPhysics.Velocity = new Vector2(
                (SSettings.SpeedBase + SSettings.SpeedRandom * world.Random.NextFloat()) * world.Random.RandomSign() * SSettings.SpeedHorizontalScale,
                SSettings.SpeedBase + SSettings.SpeedRandom * world.Random.NextFloat()
            );

            asteroid.Physics.AnglePhysics.AngularVelocity = SSettings.AngleSpeedBase + SSettings.AngleSpeedRandom * world.Random.NextFloat() * world.Random.RandomSign();

            float bigness = SSettings.ScaleBase + SSettings.ScaleRandom * world.Random.NextFloat();
            asteroid.Visual.Scale = new Vector2(bigness);
            asteroid.Health = SSettings.HealthBase * bigness;

            world.EntityAdd(asteroid);

            return asteroid;
        }

        public float Health { get; private set; }
        private CVisual Cracks { get; set; }

        public CAsteroid(CWorld world, String name, Texture2D texture)
            : base(world, name, texture)
        {
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
            Cracks.Alpha = 1.0f - Health / SSettings.HealthBase;
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
