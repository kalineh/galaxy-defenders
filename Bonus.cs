//
// Bonus.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CBonus
        : CEntity
    {
        public CBonus(CWorld world, Vector2 position)
            : base(world, "Bonus")
        {
            Physics = new CPhysics();
            Physics.PositionPhysics.Position = position;
            Physics.PositionPhysics.Velocity = Vector2.UnitY * 0.2f;
            Collision = new CollisionCircle(Vector2.Zero, 16.0f);
            Visual = new CVisual(world.Game.Content.Load<Texture2D>("Bonus"), Color.White);
        }

        public override void Update()
        {
            base.Update();

            if (!IsInScreen() && Physics.PositionPhysics.Position.Y > 0.0f)
                Delete();
        }

        public void OnCollide(CShip ship)
        {
            World.Score += 1000;
            Die();
        }
    }
}
