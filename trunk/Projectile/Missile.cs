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
        public CParticleGroupSpawner TrailEffect { get; set; }

        public static CMissile Spawn(CWorld world, Vector2 position, float rotation, float speed, float damage, PlayerIndex index)
        {
            CMissile missile = new CMissile();
            missile.Initialize(world, index, damage);

            missile.Speed = speed;

            missile.Physics.AnglePhysics.Rotation = rotation - MathHelper.Pi;
            missile.FireVector = Vector2.UnitX.Rotate(rotation);

            missile.Physics.PositionPhysics.Position = position;
            missile.Physics.PositionPhysics.Velocity = missile.FireVector * 6.0f;

            world.EntityAdd(missile);

            return missile;
        }

        public override void Initialize(CWorld world, PlayerIndex index, float damage)
        {
            base.Initialize(world, index, damage);

            Physics = new CPhysics();
            Visual = CVisual.MakeSpriteCachedForPlayer(world, "Textures/Weapons/Missile", index);
            Visual.Color = CShip.GetPlayerColor(index);
            Visual.Update();
            Collision = CCollision.GetCacheAABB(this, Vector2.Zero, new Vector2(1.0f, 0.5f));

            TrailEffect = CParticleGroupSpawner.MakeMissileTrail(Vector2.Zero, Vector2.UnitY, CShip.GetPlayerColor(index));
        }

        public override void Update()
        {
            Physics.PositionPhysics.Velocity = Vector2.Lerp(Physics.PositionPhysics.Velocity, -FireVector * Speed, 0.05f);

            //CEffect.MissileTrail(World, Physics.PositionPhysics.Position, 0.3f, Visual.Color);
            TrailEffect.Position = Physics.PositionPhysics.Position;
            TrailEffect.Spawn(World.ParticleEffects);

            base.Update();

            if (!IsInScreen())
                Delete();
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionAABB box = Collision as CollisionAABB;
            box.Position = Physics.PositionPhysics.Position;
        }

        protected override void OnDie()
        {
            CEffect.MissileExplosion(World, Physics.PositionPhysics.Position, 2.5f, Visual.Color);
            base.OnDie();
        }
    }
}
