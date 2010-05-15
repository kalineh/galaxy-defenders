﻿//
// Plasma.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CSmallPlasma
        : CEntity
    {
        public static CSmallPlasma Spawn(CWorld world, Vector2 position, float rotation, float speed, float damage, PlayerIndex index)
        {
            CSmallPlasma plasma = new CSmallPlasma(world, damage, index);

            plasma.Physics.AnglePhysics.Rotation = rotation;
            plasma.Physics.PositionPhysics.Position = position;
            plasma.Physics.PositionPhysics.Velocity = Vector2.UnitX.Rotate(rotation) * speed;

            world.EntityAdd(plasma);

            return plasma;
        }

        public float Damage { get; private set; }

        public CSmallPlasma(CWorld world, float damage, PlayerIndex index)
            : base(world)
        {
            Physics = new CPhysics();
            Visual = new CVisual(world, CContent.LoadTexture2D(world.Game, "Textures/Weapons/SmallPlasma"), CShip.GetPlayerColor(index));
            Visual.Color = CShip.GetPlayerColor(index);
            Visual.Update();
            Collision = CCollision.GetCacheAABB(this, Vector2.Zero, new Vector2(1.0f, 0.5f));
            Damage = damage;
            IgnoreCameraScroll = true;
        }

#if XBOX360
        public CSmallPlasma()
        {
        }

        public void Init360(CWorld world, float damage, PlayerIndex index)
        {
            base.Init360(world);

            Physics = new CPhysics();
            Visual = new CVisual(world, CContent.LoadTexture2D(world.Game, "Textures/Weapons/SmallPlasma"), CShip.GetPlayerColor(index));
            Visual.Color = CShip.GetPlayerColor(index);
            Visual.Update();
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
            // TODO: plasma hit effect
            CEffect.LaserHit(World, Physics.PositionPhysics.Position, Physics.PositionPhysics.Velocity.Normal(), 1.0f, Visual.Color);
            //World.Sound.Play("WeaponHitPlasma", 1.0f);
            base.OnDie();
        }
    }

    public class CBigPlasma
        : CEntity
    {
        public static CBigPlasma Spawn(CWorld world, Vector2 position, float rotation, float speed, float damage, PlayerIndex index)
        {
            CBigPlasma plasma = new CBigPlasma(world, damage, index);

            plasma.Physics.AnglePhysics.Rotation = rotation;
            plasma.Physics.PositionPhysics.Position = position;
            plasma.Physics.PositionPhysics.Velocity = Vector2.UnitX.Rotate(rotation) * speed;

            world.EntityAdd(plasma);

            return plasma;
        }

        public float Damage { get; private set; }

        public CBigPlasma(CWorld world, float damage, PlayerIndex index)
            : base(world)
        {
            Physics = new CPhysics();
            Visual = new CVisual(world, CContent.LoadTexture2D(world.Game, "Textures/Weapons/BigPlasma"), CShip.GetPlayerColor(index));
            Visual.Color = CShip.GetPlayerColor(index);
            Visual.Update();
            Collision = CCollision.GetCacheAABB(this, Vector2.Zero, new Vector2(1.0f, 0.5f));
            Damage = damage;
            IgnoreCameraScroll = true;
        }

#if XBOX360
        public CBigPlasma()
        {
        }

        public void Init360(CWorld world, float damage, PlayerIndex index)
        {
            base.Init360(world);

            Physics = new CPhysics();
            Visual = new CVisual(world, CContent.LoadTexture2D(world.Game, "Textures/Weapons/BigPlasma"), CShip.GetPlayerColor(index));
            Visual.Color = CShip.GetPlayerColor(index);
            Visual.Update();
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
            // TODO: plasma hit effect
            CEffect.LaserHit(World, Physics.PositionPhysics.Position, Physics.PositionPhysics.Velocity.Normal(), 1.0f, Visual.Color);
            base.OnDie();
        }
    }
}