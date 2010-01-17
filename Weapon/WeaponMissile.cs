//
// WeaponMissile.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Galaxy
{
    public class CWeaponMissile
        : CWeapon
    {
        public CWeaponMissile(CEntity owner)
            : base(owner)
        {
        }

        protected override void Instantiate(CWorld world, Vector2 position, float rotation, float speed, float damage)
        {
            CMissile missile = CMissile.Spawn(world, position, rotation, Speed, Damage);
        }
    };

}
