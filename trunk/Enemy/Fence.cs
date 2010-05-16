//
// Fence.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Galaxy
{
    public class CFence
        : CEnemy
    {
        public CFence Child { get; set; }
        public CFence Parent { get; set; }
        public bool HasChecked { get; set; }
        public CFenceBeam FenceBeam { get; set; }
        public Vector2 FenceOffset { get; set; }
        
        public CFence(CWorld world, Vector2 position)
            : base(world)
        {
            Physics = new CPhysics();
            Physics.PositionPhysics.Position = position;
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 32.0f);
            Visual = new CVisual(world, CContent.LoadTexture2D(world.Game, "Textures/Enemy/Fence"), Color.White);
            HealthMax = 10.0f;
            CanSeekerTarget = false;
        }

#if XBOX360
        public CFence()
        {
        }

        public void Init360(CWorld world, Vector2 position)
        {
            base.Init360(world);
            
            Physics = new CPhysics();
            Physics.PositionPhysics.Position = position;
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 32.0f);
            Visual = new CVisual(world, CContent.LoadTexture2D(world.Game, "Textures/Enemy/Fence"), Color.White);
            HealthMax = 10.0f;
            CanSeekerTarget = false;
        }
#endif

        public void FindNearestFence()
        {
            IEnumerable<CEntity> entities = World.GetEntities();
            foreach (CEntity entity in entities)
            {
                CFence fence = entity as CFence;
                if (fence == null)
                    continue;

                if (fence == this)
                    continue;

                Vector2 offset = fence.Physics.PositionPhysics.Position - Physics.PositionPhysics.Position;
                
                if (Math.Abs(offset.Y) > 16.0f)
                    continue;

                if (Math.Abs(offset.X) > 256.0f)
                    continue;

                if (fence.Child != null)
                    continue;

                Child = fence;
                Child.Parent = this;

                FenceOffset = offset * 0.5f;
                FenceBeam = new CFenceBeam(World, Physics.PositionPhysics.Position + FenceOffset);
                FenceBeam.UpdateAttachment(this);
                World.EntityAdd(FenceBeam);
                break;
            }

            HasChecked = true;
        }

        public override void Update()
        {
            base.Update();

            if (!HasChecked)
            {
                FindNearestFence();
            }

            if (FenceBeam != null)
                FenceBeam.UpdateAttachment(this);
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.PositionPhysics.Position;
        }

        public override void Draw(SpriteBatch sprite_batch)
        {
            base.Draw(sprite_batch);
        }

        protected override void OnDie()
        {
            if (FenceBeam != null)
            {
                FenceBeam.Die();
            }

            if (Parent != null && Parent.FenceBeam != null)
            {
                Parent.FenceBeam.Die();
                Parent.FenceBeam = null;
            }

            base.OnDie();
        }
    }
}

