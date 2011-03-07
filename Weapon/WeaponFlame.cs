//
// WeaponFlame.cs
//

using System;
using Microsoft.Xna.Framework;

namespace Galaxy
{
    public class CWeaponFlame
        : CWeapon
    {
        public CProjectileCache<CFlame> Cache { get; set; }

        public override void Initialize(CShip owner)
        {
            base.Initialize(owner);

            Cache = new CProjectileCache<CFlame>(owner.World);

            RandomReloadTime = 0.1f;
        }

        protected override void Instantiate(CShip owner, Vector2 position, float rotation, float speed, float damage, float charge, object custom_data)
        {
            CFlame flame = Cache.GetProjectileInstance(Owner.GameControllerIndex);

            flame.Initialize(owner.World, owner, damage);

            flame.Physics.Rotation = rotation;
            flame.Physics.Position = position;
            flame.Physics.Velocity = Vector2.UnitX.Rotate(rotation) * speed;

            flame.Physics.Rotation = owner.World.Random.NextAngle();
            flame.Physics.Velocity += new Vector2(0.0f, owner.Physics.Velocity.Y * 0.5f);

            FlameCustomData custom = (FlameCustomData)custom_data;
            flame.Physics.Friction = custom.Friction;
            flame.Physics.Velocity = flame.Physics.Velocity.Rotate(owner.World.Random.NextFloat() * custom.SprayAngle * owner.World.Random.NextSign());
            flame.Physics.AngularVelocity = owner.World.Random.NextAngle() * 0.05f;
            flame.Lifetime = custom.Lifetime;

            // TODO: what if this is on the entity delete list?!
            // TODO: it will be added (a second instance), then deleted at the end of the frame
            owner.World.EntityAdd(flame);
        }
    }

    public class CWeaponFrontFlame
        : CWeaponFlame
    {
    }

    public class CWeaponSpreadFlame
        : CWeaponFlame
    {
    }
}
