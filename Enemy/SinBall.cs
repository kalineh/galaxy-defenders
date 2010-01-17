//
// SinBall.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Galaxy
{
    public class CSinBall
        : CEnemy
    {
        public CSinBall(CWorld world, Vector2 position)
            : base(world, "SinBall")
        {
            Physics = new CPhysics();
            Physics.PositionPhysics.Position = position;
            Collision = new CollisionCircle(Vector2.Zero, 32.0f);
            Visual = new CVisual(world.Game.Content.Load<Texture2D>("SinBall"), Color.White);
            Health = 5.0f;
            Mover = new CMoverSin() { Frequency = 0.05f, Amplitude = 4.0f, Down = 0.5f };
        }

        public override void UpdateAI()
        {
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

