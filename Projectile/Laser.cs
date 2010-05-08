//
// Laser.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CLaser
        : CEntity
    {
        public static CLaser Spawn(CWorld world, Vector2 position, float rotation, float speed, float damage, PlayerIndex index)
        {
            CLaser laser = new CLaser(world, damage, index);

            laser.Physics.AnglePhysics.Rotation = rotation;
            laser.Physics.PositionPhysics.Position = position;
            laser.Physics.PositionPhysics.Velocity = Vector2.UnitX.Rotate(rotation) * speed;

            world.EntityAdd(laser);

            return laser;
        }

        public float Damage { get; private set; }

        public CLaser(CWorld world, float damage, PlayerIndex index)
            : base(world)
        {
            Physics = new CPhysics();

            Visual = CVisual.MakeSpriteCachedForPlayer(world, "Textures/Weapons/Laser", index);
            Visual.Color = CShip.GetPlayerColor(index);
            Visual.UpdateColor();
            Collision = CCollision.GetCacheAABB(this, Vector2.Zero, new Vector2(1.0f, 0.5f));
            Damage = damage;
            IgnoreCameraScroll = true;
        }

#if XBOX360
        public CLaser()
        {
        }

        public void Init360(CWorld world, float damage, PlayerIndex index)
        {
            base.Init360(world);

            Physics = new CPhysics();
            Visual = new CVisual(world, CContent.LoadTexture2D(world.Game, "Textures/Weapons/Laser"), CShip.GetPlayerColor(index));
            Visual.Color = CShip.GetPlayerColor(index);
            Visual.UpdateColor();
            Collision = CCollision.GetCacheAABB(this, Vector2.Zero, new Vector2(1.0f, 0.5f));
            Damage = damage;
            IgnoreCameraScroll = true;
        }
#endif

        public override void Update()
        {
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
            CEffect.LaserHit(World, Physics.PositionPhysics.Position, Physics.PositionPhysics.Velocity.Normal(), 1.0f, Visual.Color);
            base.OnDie();
        }
    }

    public class CBigLaser
        : CEntity
    {
        public static CBigLaser Spawn(CWorld world, Vector2 position, float rotation, float speed, float damage, PlayerIndex index)
        {
            CBigLaser laser = new CBigLaser(world, damage, index);

            laser.Physics.AnglePhysics.Rotation = rotation;
            laser.Physics.PositionPhysics.Position = position;
            laser.Physics.PositionPhysics.Velocity = Vector2.UnitX.Rotate(rotation) * speed;

            world.EntityAdd(laser);

            return laser;
        }

        public float Damage { get; private set; }

        public CBigLaser(CWorld world, float damage, PlayerIndex index)
            : base(world)
        {
            Physics = new CPhysics();

            Visual = CVisual.MakeSpriteCachedForPlayer(world, "Textures/Weapons/BigLaser", index);
            Visual.Color = CShip.GetPlayerColor(index);
            Visual.UpdateColor();
            Collision = CCollision.GetCacheAABB(this, Vector2.Zero, new Vector2(1.0f, 0.5f));
            Damage = damage;
            IgnoreCameraScroll = true;
        }

#if XBOX360
        public CBigLaser()
        {
        }

        public void Init360(CWorld world, float damage, PlayerIndex index)
        {
            base.Init360(world);

            Physics = new CPhysics();
            Visual = new CVisual(world, CContent.LoadTexture2D(world.Game, "Textures/Weapons/BigLaser"), CShip.GetPlayerColor(index));
            Visual.Color = CShip.GetPlayerColor(index);
            Visual.UpdateColor();
            Collision = CCollision.GetCacheAABB(this, Vector2.Zero, new Vector2(1.0f, 0.5f));
            Damage = damage;
            IgnoreCameraScroll = true;
        }
#endif

        public override void Update()
        {
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
            CEffect.LaserHit(World, Physics.PositionPhysics.Position, Physics.PositionPhysics.Velocity.Normal(), 1.0f, Visual.Color);
            base.OnDie();
        }
    }
}
