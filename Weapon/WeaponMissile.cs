//
// WeaponMissile.cs
//

using Microsoft.Xna.Framework;

namespace Galaxy
{
    public class CWeaponMissile
        : CWeapon
    {
        public CWeaponMissile(CShip owner)
            : base(owner)
        {
        }

#if XBOX360
        public CWeaponMissile()
        {
        }

        public new void Init360(CShip owner)
        {
            base.Init360(owner);
        }
#endif

        protected override void Instantiate(CWorld world, Vector2 position, float rotation, float speed, float damage)
        {
            CMissile missile = CMissile.Spawn(world, position, rotation, Speed, Damage, Owner.PlayerIndex);
        }
    };

}
