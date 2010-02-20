//
// WeaponLaser.cs
//

using Microsoft.Xna.Framework;

namespace Galaxy
{
    public class CWeaponLaser
        : CWeapon
    {
        public CWeaponLaser(CEntity owner)
            : base(owner, "Laser")
        {
        }

        protected override void Instantiate(CWorld world, Vector2 position, float rotation, float speed, float damage)
        {
            CLaser laser = CLaser.Spawn(world, position, rotation, Speed, Damage);
        }
    };
}
