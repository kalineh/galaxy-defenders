//
// Isosceles.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CIsosceles
        : CEnemy
    {
        public CIsosceles(CWorld world, Vector2 position)
            : base(world)
        {
            Physics = new CPhysics();
            Physics.PositionPhysics.Position = position;
            Visual = CVisual.MakeSprite(world, "Textures/Enemy/Isosceles.cs");
            HealthMax = 1.0f;
        }
    }
}
