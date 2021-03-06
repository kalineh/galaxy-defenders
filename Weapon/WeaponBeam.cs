﻿//
// WeaponBeam.cs
//

using System;
using Microsoft.Xna.Framework;

namespace Galaxy
{
    public class CWeaponBeam
        : CWeapon
    {
        public override void Initialize(CShip owner)
        {
            base.Initialize(owner);
        }

        protected override void Instantiate(CShip owner, Vector2 position, float rotation, float speed, float damage, float charge, object custom_data)
        {
            CBeam beam = ProjectileCacheManager.Beams.GetProjectileInstance(Owner.GameControllerIndex);

            beam.Initialize(owner.World, owner, damage);

            beam.Physics.Rotation = rotation;
            beam.Physics.Position = position;
            beam.OwnerOffset = position - owner.Physics.Position;

            BeamCustomData custom = (BeamCustomData)custom_data;
            beam.Width = custom.Width;

            beam.OwnerWeapon = this;

            // TODO: what if this is on the entity delete list?!
            // TODO: it will be added (a second instance), then deleted at the end of the frame
            owner.World.EntityAdd(beam);
        }
    }

    public class CWeaponBeamFocus
        : CWeapon
    {
        public override void Initialize(CShip owner)
        {
            base.Initialize(owner);
        }

        protected override void Instantiate(CShip owner, Vector2 position, float rotation, float speed, float damage, float charge, object custom_data)
        {
            CBeamFocus beam = ProjectileCacheManager.BeamFocuses.GetProjectileInstance(Owner.GameControllerIndex);

            beam.Initialize(owner.World, owner, damage);

            beam.Physics.Rotation = rotation;
            beam.Physics.Position = position;
            beam.OwnerOffset = position - owner.Physics.Position;

            BeamFocusCustomData custom = (BeamFocusCustomData)custom_data;
            beam.TargetRadius = custom.Radius;

            beam.OwnerWeapon = this;

            // TODO: what if this is on the entity delete list?!
            // TODO: it will be added (a second instance), then deleted at the end of the frame
            owner.World.EntityAdd(beam);
        }
    }
}
