//
// WeaponVulcan.cs
//

using Microsoft.Xna.Framework;

namespace Galaxy
{
    public class CWeaponVulcan
        : CWeapon
    {
        public CEnemy LastTarget { get; set; }

        public override void Initialize(CShip owner)
        {
            base.Initialize(owner);
        }

        protected override void Instantiate(CShip owner, Vector2 position, float rotation, float speed, float damage, float charge, object custom_data)
        {
            CVulcan vulcan = ProjectileCacheManager.Vulcans.GetProjectileInstance(Owner.GameControllerIndex);

            vulcan.Initialize(owner.World, owner, damage);

            // TODO: is this an ok place to do a search?
            if (LastTarget == null || LastTarget.IsDead)
            {
                LastTarget = owner.World.GetNearestEnemySeekable(position, true);
            }

            if (LastTarget != null && LastTarget.IsDead == false)
            {
                Vector2 target = LastTarget.Physics.Position + LastTarget.Physics.Velocity * 25.0f;
                Vector2 offset = target - position;
                rotation = offset.ToAngle();

                // NOTE: will break seeker bombs, and never resets
                LastTarget.IsSeekerTarget = true;
            }

            vulcan.Physics.Rotation = rotation;
            vulcan.Physics.Position = position;
            vulcan.Physics.Velocity = Vector2.UnitX.Rotate(rotation) * speed;

            VulcanCustomData custom = (VulcanCustomData)custom_data;
            vulcan.Physics.Velocity = vulcan.Physics.Velocity.Rotate(owner.World.Random.NextFloat() * custom.SprayAngle * owner.World.Random.NextSign());

            owner.World.EntityAdd(vulcan);
        }
    };
}
