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

        public static CMissile Spawn(CShip owner, Vector2 position, float rotation, float speed, float damage)
        {
            CMissile missile = new CMissile();
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
            Visual = CVisual.MakeSpriteCachedForPlayer(world.Game, "Textures/Weapons/Missile", owner.PlayerIndex);
            Visual.Color = CShip.GetPlayerColor(owner.PlayerIndex);
            Visual.Update();
            Collision = CCollision.GetCacheAABB(this, Vector2.Zero, new Vector2(1.0f, 0.5f));

            TrailEffect = CParticleGroupSpawner.MakeMissileTrail(Vector2.Zero, Vector2.UnitY, CShip.GetPlayerColor(owner.PlayerIndex));
        }

        public override void Update()
        {
            Physics.Velocity = Vector2.Lerp(Physics.Velocity, -FireVector * Speed, 0.05f);

            TrailEffect.Position = Physics.Position;
            TrailEffect.Spawn(World.ParticleEffects);

            base.Update();

            if (!IsInScreen())
                Delete();
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionAABB box = Collision as CollisionAABB;
            box.Position = Physics.Position;
        }

        protected override void OnDie()
        {
            CEffect.MissileExplosion(World, Physics.Position, 2.5f, Visual.Color);
            base.OnDie();
        }
    }
}
