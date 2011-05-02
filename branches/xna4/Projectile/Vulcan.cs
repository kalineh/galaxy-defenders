//
// Vulcan.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public struct VulcanCustomData
    {
        public float SprayAngle;
    };

    public class CVulcan
        : CProjectile
    {
        public static CVulcan Spawn(CShip owner, Vector2 position, float rotation, float speed, float damage)
        {
            CVulcan vulcan = new CVulcan();
            vulcan.Initialize(owner.World, owner, damage);

            vulcan.Physics.Rotation = rotation;
            vulcan.Physics.Position = position;
            vulcan.Physics.Velocity = Vector2.UnitX.Rotate(rotation) * speed;

            owner.World.EntityAdd(vulcan);

            return vulcan;
        }

        public override void Initialize(CWorld world, CShip owner, float damage)
        {
            base.Initialize(world);

            Owner = owner;
            Physics = new CPhysics();
            Visual = CVisual.MakeSpriteCachedForPlayer(world.Game, "Textures/Weapons/Vulcan", owner.GameControllerIndex);
            Visual.Color = CShip.GetPlayerColor(owner.GameControllerIndex);
            Visual.Update();
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 1.0f);
            Damage = damage;
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.Position;
        }

        protected override void OnDie()
        {
            CAudio.PlaySound("WeaponHitMiniShot", 1.0f);
            World.ParticleEffects.Spawn(EParticleType.WeaponVulcanHit, Physics.Position, Visual.Color, null, -Physics.Velocity.Normal());
            base.OnDie();
        }
    }
}
