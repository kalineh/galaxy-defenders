//
// WeaponPlasma.cs
//

using Microsoft.Xna.Framework;

namespace Galaxy
{
    public class CWeaponPlasma
        : CWeapon
    {
        public CWeaponPlasma(CShip owner)
            : base(owner)
        {
        }

#if XBOX360
        public CWeaponPlasma()
        {
        }

        public new void Init360(CShip owner)
        {
            base.Init360(owner);
        }
#endif

        protected override void Instantiate(CWorld world, Vector2 position, float rotation, float speed, float damage)
        {
            if (damage >= 1.0f)
            {
                CBigPlasma.Spawn(world, position, rotation, Speed, Damage, Owner.PlayerIndex);
            }
            else
            {
                CSmallPlasma.Spawn(world, position, rotation, Speed, Damage, Owner.PlayerIndex);
            }
        }
    };
}
