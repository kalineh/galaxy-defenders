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
            Physics.AngularVelocity = 0.015f * world.Random.NextSign();
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 36.0f);
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Enemy/Splitter");
            HealthMax = 1.25f;
            Coins = 0;
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.Position;
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
            Visual = CVisual.MakeSpriteCached1(world.Game, left ? "Textures/Enemy/SplitterFragmentLeft" : "Textures/Enemy/SplitterFragmentRight");
            Physics.AngularVelocity = 0.1f * (left ? 1.0f : -1.0f);
            Physics.Position = parent.Physics.Position + Vector2.UnitX * 3.0f * (left ? -1.0f : 1.0f);
            Physics.Velocity = parent.Physics.Velocity;
            Physics.Velocity += Vector2.UnitX * (left ? -1.0f : 1.0f);
            Physics.Velocity += world.Random.NextVector2() * 0.5f;
        }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 24.0f);
            HealthMax = 1.0f;
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.Position;
        }
    }
}

