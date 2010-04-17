//
// WeaponSeekBomb.cs
//

using Microsoft.Xna.Framework;

namespace Galaxy
{
    public class CWeaponSeekBomb
        : CWeapon
    {
        public CWeaponSeekBomb(CShip owner)
            : base(owner)
        {
        }

#if XBOX360
        public CWeaponSeekBomb()
        {
        }

        public new void Init360(CShip owner)
        {
            base.Init360(owner);
        }
#endif

        protected override void Instantiate(CWorld world, Vector2 position, float rotation, float speed, float damage)
        {
            CSeekBomb seek_bomb = CSeekBomb.Spawn(world, position, rotation, Speed, Damage, Owner.PlayerIndex);
        }
    };

}
