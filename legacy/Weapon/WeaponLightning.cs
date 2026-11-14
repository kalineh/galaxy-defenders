//
// WeaponLightning.cs
//

using System;
using Microsoft.Xna.Framework;

namespace Galaxy
{
    public class CWeaponLightning
        : CWeapon
    {
        public override void Initialize(CShip owner)
        {
            base.Initialize(owner);
        }

        protected override void Instantiate(CShip owner, Vector2 position, float rotation, float speed, float damage, float charge, object custom_data)
        {
            CLightning lightning = ProjectileCacheManager.Lightnings.GetProjectileInstance(Owner.GameControllerIndex);

            lightning.Initialize(owner.World, owner, damage);

            lightning.Physics.Rotation = rotation;
            lightning.Physics.Position = position;
            lightning.Physics.Velocity = Vector2.UnitX.Rotate(rotation) * speed;

            LightningCustomData custom = (LightningCustomData)custom_data;
            lightning.BouncesRemaining = custom.Bounces;
            lightning.IgnoreEntity = null;

            // TODO: what if this is on the entity delete list?!
            // TODO: it will be added (a second instance), then deleted at the end of the frame
            owner.World.EntityAdd(lightning);
        }
    }

    public class CWeaponLightningFocus
        : CWeaponLightning
    {
        
    }
}
