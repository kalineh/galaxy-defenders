//
// WeaponLaser.cs
//

using System;
using Microsoft.Xna.Framework;

namespace Galaxy
{
    public class CWeaponLaser
        : CWeapon
    {
        public CProjectileCache<CLaser> Cache { get; set; }

        public override void Initialize(CShip owner)
        {
            base.Initialize(owner);

            Cache = new CProjectileCache<CLaser>(owner.World);
        }

        protected override void Instantiate(CShip owner, Vector2 position, float rotation, float speed, float damage)
        {
            CLaser laser = Cache.GetProjectileInstance(Owner.PlayerIndex);

            laser.Initialize(owner.World, owner, damage);

            laser.Physics.Rotation = rotation;
            laser.Physics.Position = position;
            laser.Physics.Velocity = Vector2.UnitX.Rotate(rotation) * speed;

            // TODO: what if this is on the entity delete list?!
            // TODO: it will be added (a second instance), then deleted at the end of the frame
            owner.World.EntityAdd(laser);
        }
    }

    public class CWeaponFrontLaser
        : CWeaponLaser
    {
    }

    public class CWeaponSpreadLaser
        : CWeaponLaser
    {
    }
}
