//
// WeaponMiniShot.cs
//

using Microsoft.Xna.Framework;

namespace Galaxy
{
    public class CWeaponMiniShot
        : CWeapon
    {
        public CWeaponMiniShot(CEntity owner)
            : base(owner)
        {
        }

        protected override void Instantiate(CWorld world, Vector2 position, float rotation, float speed, float damage)
        {
            CMiniShot minishot = CMiniShot.Spawn(world, position, rotation, Speed, Damage);
        }
    };
}
