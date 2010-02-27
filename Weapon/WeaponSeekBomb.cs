﻿//
// WeaponSeekBomb.cs
//

using Microsoft.Xna.Framework;

namespace Galaxy
{
    public class CWeaponSeekBomb
        : CWeapon
    {
        public CWeaponSeekBomb(CEntity owner)
            : base(owner)
        {
        }

        protected override void Instantiate(CWorld world, Vector2 position, float rotation, float speed, float damage)
        {
            CSeekBomb seek_bomb = CSeekBomb.Spawn(world, position, rotation, Speed, Damage);
        }
    };

}