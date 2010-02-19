//
// SinBall.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
            Visual = new CVisual(world, CContent.LoadTexture2D(world.Game, "Textures/Enemy/SinBall"), Color.White);
            Health = 1.0f;
            Mover = new CMoverSin() { Frequency = 0.05f, Amplitude = 4.0f, Down = 0.5f };
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

