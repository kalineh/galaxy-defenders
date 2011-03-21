//
// WeaponBeam.cs
//

using System;
using Microsoft.Xna.Framework;

namespace Galaxy
{
    public class CWeaponBeam
        : CWeapon
    {
        public CProjectileCache<CBeam> Cache { get; set; }

        public override void Initialize(CShip owner)
        {
            base.Initialize(owner);

            Cache = new CProjectileCache<CBeam>(owner.World);
        }

        protected override void Instantiate(CShip owner, Vector2 position, float rotation, float speed, float damage, float charge, object custom_data)
        {
            CBeam beam = Cache.GetProjectileInstance(Owner.GameControllerIndex);

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
}
