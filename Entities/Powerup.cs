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
        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Visual = CVisual.MakeSpriteCached1(world, "Textures/Entity/Powerup");
        }

        public new void OnCollide(CShip ship)
        {
            ship.WeaponPrimary = ship.UpgradeWeapon(ship.PrimaryWeapon);
            Die();
        }
    }
}
