//
// WeaponChargeShot.cs
//

using Microsoft.Xna.Framework;

namespace Galaxy
{
    public class CWeaponChargeShot
        : CWeapon
    {
        public override void Initialize(CShip owner)
        {
            base.Initialize(owner);
        }

        protected override void Instantiate(CShip owner, Vector2 position, float rotation, float speed, float damage, float charge, object custom_data)
        {
            CChargeShot chargeshot = CChargeShot.Spawn(owner, position, rotation, speed, damage, charge);
        }
    };
}
