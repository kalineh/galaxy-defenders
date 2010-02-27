//
// Spike.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CSpike
        : CEnemy
    {
        public CSpike(CWorld world, Vector2 position)
            : base(world)
        {
            Physics = new CPhysics();
            Physics.PositionPhysics.Position = position;
            Visual = CVisual.MakeSprite(world, "Textures/Enemy/Spike");
            HealthMax = 22.0f;
            IgnoreCameraScroll = true;
            Collision = new CollisionCircle(Vector2.Zero, 16.0f);
        }

        public override void UpdateAI()
        {
            CShip ship = World.GetNearestShip(Physics.PositionPhysics.Position);
            if (ship != null)
            {
                Vector2 offset = ship.Physics.PositionPhysics.Position - Physics.PositionPhysics.Position;
                Vector2 force = offset.Normal() * 2.0f;
                Physics.PositionPhysics.Position += force;
            }
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.PositionPhysics.Position;
        }
    }
}
