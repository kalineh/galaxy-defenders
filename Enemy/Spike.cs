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
            Physics.PositionPhysics.Friction = 0.98f;
            Visual = CVisual.MakeSpriteCached1(world, "Textures/Enemy/Spike");
            Visual.TileX = 2;
            Visual.AnimationSpeed = 0.05f;
            HealthMax = 11.0f;
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 28.0f);
        }

#if XBOX360
        public CSpike()
        {
        }

        public void Init360(CWorld world, Vector2 position)
        {
            base.Init360(world);

            Physics = new CPhysics();
            Physics.PositionPhysics.Position = position;
            Physics.PositionPhysics.Friction = 0.98f;
            Visual = CVisual.MakeSpriteCached1(world, "Textures/Enemy/Spike");
            Visual.TileX = 2;
            Visual.AnimationSpeed = 0.05f;
            HealthMax = 11.0f;
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 28.0f);
        }
#endif

        public override void UpdateAI()
        {
            Mover = null;
            CShip ship = World.GetNearestShip(Physics.PositionPhysics.Position);
            if (ship == null)
                return;

            Vector2 offset = ship.Physics.PositionPhysics.Position - Physics.PositionPhysics.Position;
            float length = offset.Length();
            const float limit = 750.0f;
            if (offset.Length() < limit)
            {
                float t = 1.0f - length / limit;
                Vector2 force = offset.Normal() * t * t * 0.05f;
                Physics.PositionPhysics.Velocity += force;
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
