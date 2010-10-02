//
// Splitter.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Galaxy
{
    public class CSplitter
        : CEnemy
    {
        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Physics.AnglePhysics.AngularVelocity = 0.015f * world.Random.NextSign();
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 32.0f);
            Visual = CVisual.MakeSpriteCached1(world, "Textures/Enemy/Splitter");
            HealthMax = 2.5f;
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.PositionPhysics.Position;
        }

        protected override void OnDie()
        {
            CSplitterFragment left = new CSplitterFragment();
            CSplitterFragment right = new CSplitterFragment();
            left.CustomInitialize(World, true, this);
            right.CustomInitialize(World, false, this);
            World.EntityAdd(left);
            World.EntityAdd(right);
            base.OnDie();
        }
    }

    public class CSplitterFragment
        : CEnemy
    {
        public void CustomInitialize(CWorld world, bool left, CSplitter parent)
        {
            Initialize(world); 
            Visual = CVisual.MakeSpriteCached1(world, left ? "Textures/Enemy/SplitterFragmentLeft" : "Textures/Enemy/SplitterFragmentRight");
            Physics.AnglePhysics.AngularVelocity = 0.1f * (left ? 1.0f : -1.0f);
            Physics.PositionPhysics.Position = parent.Physics.PositionPhysics.Position + Vector2.UnitX * 4.0f * (left ? -1.0f : 1.0f);
            Physics.PositionPhysics.Velocity = parent.Physics.PositionPhysics.Velocity;
            Physics.PositionPhysics.Velocity += Vector2.UnitY * 2.0f + Vector2.UnitX * (left ? -1.0f : 1.0f);
        }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 24.0f);
            HealthMax = 2.5f;
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.PositionPhysics.Position;
        }
    }
}

