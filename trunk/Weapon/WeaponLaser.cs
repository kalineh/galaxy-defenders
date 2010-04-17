//
// WeaponLaser.cs
//

using Microsoft.Xna.Framework;

namespace Galaxy
{
    public class CWeaponFrontLaser
        : CWeapon
    {
        public CWeaponFrontLaser(CShip owner)
            : base(owner)
        {
        }

#if XBOX360
        public CWeaponFrontLaser()
        {
        }

        public new void Init360(CShip owner)
        {
            base.Init360(owner);
        }
#endif

        protected override void Instantiate(CWorld world, Vector2 position, float rotation, float speed, float damage)
        {
            CLaser laser = CLaser.Spawn(world, position, rotation, Speed, Damage, Owner.PlayerIndex);
        }
    };

    public class CWeaponSpreadLaser
        : CWeapon
    {
        public CWeaponSpreadLaser(CShip owner)
            : base(owner)
        {
        }

#if XBOX360
        public CWeaponSpreadLaser()
        {
        }

        public new void Init360(CShip owner)
        {
            base.Init360(owner);
        }
#endif

        protected override void Instantiate(CWorld world, Vector2 position, float rotation, float speed, float damage)
        {
            CLaser laser = CLaser.Spawn(world, position, rotation, Speed, Damage, Owner.PlayerIndex);
        }
    };
}
