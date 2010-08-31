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
        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Physics.PositionPhysics.Friction = 0.98f;
            Visual = CVisual.MakeSpriteCached1(world, "Textures/Enemy/Spike");
            Visual.TileX = 2;
            Visual.AnimationSpeed = 0.05f;
            HealthMax = 7.0f;
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 28.0f);
        }

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

        protected override void GenerateCorpse()
        {
        }
    }
}
