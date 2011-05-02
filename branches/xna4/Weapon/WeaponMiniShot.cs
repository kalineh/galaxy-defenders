//
// WeaponMiniShot.cs
//

using Microsoft.Xna.Framework;

namespace Galaxy
{
    public class CWeaponMiniShot
        : CWeapon
    {
        public override void Initialize(CShip owner)
        {
            base.Initialize(owner);
        }

        protected override void Instantiate(CShip owner, Vector2 position, float rotation, float speed, float damage, float charge, object custom_data)
        {
            CMiniShot minishot = CMiniShot.Spawn(owner, position, rotation, speed, damage);
        }
    };
}
