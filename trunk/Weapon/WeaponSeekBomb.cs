//
// WeaponSeekBomb.cs
//

using Microsoft.Xna.Framework;

namespace Galaxy
{
    public class CWeaponSeekBomb
        : CWeapon
    {
        public override void Initialize(CShip owner)
        {
            base.Initialize(owner);
        }

        protected override void Instantiate(CShip owner, Vector2 position, float rotation, float speed, float damage, float charge)
        {
            CSeekBomb seek_bomb = CSeekBomb.Spawn(owner, position, rotation, speed, damage);
        }
    };

}
