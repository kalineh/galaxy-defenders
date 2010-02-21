//
// Powerup.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CPowerup
        : CBonus
    {
        public CPowerup(CWorld world, Vector2 position)
            : base(world, position)
        {
            Visual = CVisual.MakeSprite(world, "Textures/Entity/Powerup");
        }

        public new void OnCollide(CShip ship)
        {
            ship.UpgradePrimaryWeapon();
            Die();
        }
    }
}
