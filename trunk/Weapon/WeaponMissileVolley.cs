//
// WeaponMissileVolley.cs
//

using Microsoft.Xna.Framework;

namespace Galaxy
{
    public class CWeaponMissileVolley
        : CWeapon
    {
        public override void Initialize(CShip owner)
        {
            base.Initialize(owner);
        }

        protected override void Instantiate(CShip owner, Vector2 position, float rotation, float speed, float damage, float charge, object custom_data)
        {
            CDrunkMissile missile = CDrunkMissile.Spawn(owner, position, rotation, speed, damage);
        }
    };

}
