//
// SinBall.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace galaxy
{
    public class CSinBall
        : CEnemy
    {
        public CSinBall(CWorld world, Vector2 position)
            : base(world, "SinBall")
        {
            Physics = new CPhysics();
            Physics.PositionPhysics.Position = position;
            Collision = new CollisionCircle(Vector2.Zero, 16.0f);
            Visual = new CVisual(world.Game.Content.Load<Texture2D>("SinBall"), Color.White);
            Health = 5.0f;
        }

        public override void UpdateAI()
        {
            float t = World.Game.GameFrame * 0.05f;
            float x = (float)Math.Cos(t) * 2.0f;
            float y = 1.0f;
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
        }
    }
}

