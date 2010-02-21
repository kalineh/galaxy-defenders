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
            Visual = CVisual.MakeSprite(world, "Textures/Enemy/Spike.cs");
            HealthMax = 5.0f;
        }
    }
}
