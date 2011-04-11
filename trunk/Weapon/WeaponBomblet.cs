//
// WeaponBomblet.cs
//

using Microsoft.Xna.Framework;

namespace Galaxy
{
    public class CWeaponBomblet
        : CWeapon
    {
        public override void Initialize(CShip owner)
        {
            base.Initialize(owner);
        }

        protected override void Instantiate(CShip owner, Vector2 position, float rotation, float speed, float damage, float charge, object custom_data)
        {
            CBomblet bomblet = CBomblet.Spawn(owner, position, rotation, speed, damage);
        }
    };

}
