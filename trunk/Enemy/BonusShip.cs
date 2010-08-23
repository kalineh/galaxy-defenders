//
// BonusShip.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CBonusShip
        : CEnemy
    {
        public override void Initialize(CWorld world)
        {
            base.Initialize(world);
            
            Physics = new CPhysics();
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 32.0f);
            Visual = new CVisual(world, CContent.LoadTexture2D(world.Game, "Textures/Enemy/BonusShip"), Color.White);
            HealthMax = 3.0f;
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.PositionPhysics.Position;
        }
    }
}

