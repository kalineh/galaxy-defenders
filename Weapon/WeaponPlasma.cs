//
// WeaponPlasma.cs
//

using Microsoft.Xna.Framework;

namespace Galaxy
{
    public class CWeaponPlasma
        : CWeapon
    {
        public CWeaponPlasma(CEntity owner)
            : base(owner)
        {
        }

        protected override void Instantiate(CWorld world, Vector2 position, float rotation, float speed, float damage)
        {
            if (damage >= 1.0f)
            {
                CBigPlasma.Spawn(world, position, rotation, Speed, Damage);
            }
            else
            {
                CSmallPlasma.Spawn(world, position, rotation, Speed, Damage);
            }
        }
    };
}
