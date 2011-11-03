//
// ChargeShot.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CChargeShot
        : CProjectile
    {
        public static CChargeShot Spawn(CShip owner, Vector2 position, float rotation, float speed, float damage, float charge)
        {
            CChargeShot charge_shot = ProjectileCacheManager.ChargeShots.GetProjectileInstance(owner.GameControllerIndex);
            charge_shot.Initialize(owner.World, owner, damage * charge);

            charge_shot.Physics.Rotation = rotation;
            charge_shot.Physics.Position = position;
            charge_shot.Physics.Velocity = Vector2.UnitX.Rotate(rotation) * speed;

            owner.World.EntityAdd(charge_shot);

            return charge_shot;
        }

        public override void Initialize(CWorld world, CShip owner, float damage)
        {
            base.Initialize(world);

            Owner = owner;
            Physics = new CPhysics();
            Visual = CVisual.MakeSpriteCachedForPlayer(world.Game, "Textures/Weapons/ChargeShot", owner.GameControllerIndex);
            Visual.Color = CShip.GetPlayerColor(owner.GameControllerIndex);
            Visual.Update();
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 12.0f * damage);
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
            World.ParticleEffects.Spawn(EParticleType.WeaponChargeHit, Physics.Position, Visual.Color, 0.5f + Damage * 0.25f, -Physics.Velocity.Normal());
            CAudio.PlaySound("WeaponHitLaser", 1.0f);
            base.OnDie();
        }

        public override void Draw(SpriteBatch sprite_batch)
        {
            if (Visual != null)
            {
                Visual.Draw(sprite_batch, Physics.Position, Physics.Rotation, 0.25f + Damage * 0.5f);
            }
        }
    }
}
