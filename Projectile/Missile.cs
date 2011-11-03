//
// Missile.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CMissile
        : CProjectile
    {
        public Vector2 FireVector { get; set; }
        public float Speed { get; set; }

        public static CMissile Spawn(CShip owner, Vector2 position, float rotation, float speed, float damage)
        {
            CMissile missile = ProjectileCacheManager.Missiles.GetProjectileInstance(owner.GameControllerIndex);
            missile.Initialize(owner.World, owner, damage);

            missile.Speed = speed;

            missile.Physics.Rotation = rotation - MathHelper.Pi;
            missile.FireVector = Vector2.UnitX.Rotate(rotation);

            missile.Physics.Position = position;
            missile.Physics.Velocity = missile.FireVector * 6.0f;

            owner.World.EntityAdd(missile);

            return missile;
        }

        public override void Initialize(CWorld world, CShip owner, float damage)
        {
            base.Initialize(world, owner, damage);

            Owner = owner;
            Physics = new CPhysics();
            Visual = CVisual.MakeSpriteCachedForPlayer(world.Game, "Textures/Weapons/Missile", owner.GameControllerIndex);
            Visual.Color = CShip.GetPlayerColor(owner.GameControllerIndex);
            Visual.Update();
            Collision = CCollision.GetCacheAABB(this, Vector2.Zero, new Vector2(1.0f, 0.5f));
        }

        public override void Update()
        {
            Physics.Velocity = Vector2.Lerp(Physics.Velocity, -FireVector * Speed, 0.05f);
            Vector2 dir = Physics.Velocity.Normal();
            if (dir.Y < 0.0f)
                World.ParticleEffects.Spawn(EParticleType.WeaponMissileTrail, Physics.Position + dir * -8.0f, Visual.Color, null, -dir);

            base.Update();
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionAABB box = Collision as CollisionAABB;
            box.Position = Physics.Position;
        }

        protected override void OnDie()
        {
            World.ParticleEffects.Spawn(EParticleType.WeaponMissileHit, Physics.Position, Visual.Color, null, null);
            base.OnDie();
        }
    }
}
