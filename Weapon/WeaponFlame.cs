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
        public float FireRotation { get; set; }

        public override void Initialize(CShip owner)
        {
            base.Initialize(owner);
        }

        protected override void Instantiate(CShip owner, Vector2 position, float rotation, float speed, float damage, float charge, object custom_data)
        {
            FlameCustomData custom = (FlameCustomData)custom_data;

            CFlame flame = ProjectileCacheManager.Flames.GetProjectileInstance(Owner.GameControllerIndex);

            flame.Initialize(owner.World, owner, damage);

            rotation = MathHelper.WrapAngle(FireRotation + custom.StartRotation);
            FireRotation = (float)owner.AliveTime * custom.FireRotationSpeed;

            flame.Physics.Rotation = rotation;
            flame.Physics.Position = position;
            flame.Physics.Velocity = Vector2.UnitX.Rotate(rotation) * speed;

            flame.Physics.Rotation = owner.World.Random.NextAngle();

            flame.Physics.Friction = custom.Friction;
            flame.Physics.Velocity = flame.Physics.Velocity.Rotate(owner.World.Random.NextFloat() * custom.SprayAngle * owner.World.Random.NextSign());
            flame.Physics.AngularVelocity = owner.World.Random.NextAngle() * 0.05f;
            flame.Lifetime = custom.Lifetime;

            flame.Physics.Velocity += Vector2.UnitY * -owner.World.ScrollSpeed * custom.AntiCameraSpeed;

            flame.ScaleOverride = custom.ScaleOverride;

            owner.Physics.Velocity *= 1.15f;

            // TODO: what if this is on the entity delete list?!
            // TODO: it will be added (a second instance), then deleted at the end of the frame
            owner.World.EntityAdd(flame);
        }
    }

    public class CWeaponFlameFocus
        : CWeaponFlame
    {
        
    }
}
