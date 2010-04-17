//
// WeaponMiniShot.cs
//

using Microsoft.Xna.Framework;

namespace Galaxy
{
    public class CWeaponMiniShot
        : CWeapon
    {
        public CWeaponMiniShot(CShip owner)
            : base(owner)
        {
        }

#if XBOX360
        public CWeaponMiniShot()
        {
        }

        public new void Init360(CShip owner)
        {
            base.Init360(owner);
        }
#endif

        protected override void Instantiate(CWorld world, Vector2 position, float rotation, float speed, float damage)
        {
            CMiniShot minishot = CMiniShot.Spawn(world, position, rotation, Speed, Damage, Owner.PlayerIndex);
        }
    };
}
