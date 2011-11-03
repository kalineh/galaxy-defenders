//
// WeaponLaserShot.cs
//

using System;
using Microsoft.Xna.Framework;

namespace Galaxy
{
    public class CWeaponLaserShot
        : CWeapon
    {
        public override void Initialize(CShip owner)
        {
            base.Initialize(owner);
        }

        protected override void Instantiate(CShip owner, Vector2 position, float rotation, float speed, float damage, float charge, object custom_data)
        {
            CLaser laser = ProjectileCacheManager.Lasers.GetProjectileInstance(Owner.GameControllerIndex);

            laser.Initialize(owner.World, owner, damage);

            laser.Physics.Rotation = rotation;
            laser.Physics.Position = position;
            laser.Physics.Velocity = Vector2.UnitX.Rotate(rotation) * speed;

            owner.World.EntityAdd(laser);
        }
    }
}
