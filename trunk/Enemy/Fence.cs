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
        
        public override void Initialize(CWorld world)
        {
            base.Initialize(world);
            
            Physics = new CPhysics();
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 24.0f);
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Enemy/Fence");
            Visual.Depth = CLayers.Enemy + CLayers.SubLayerIncrement * -1.0f;
            HealthMax = 8.0f;
            CanSeekerTarget = false;
            Coins = 0;
        }

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

                Vector2 offset = fence.Physics.Position - Physics.Position;
                
                if (Math.Abs(offset.Y) > 16.0f)
                    continue;

                if (Math.Abs(offset.X) > 256.0f)
                    continue;

                if (fence.Child != null)
                    continue;

                Child = fence;
                Child.Parent = this;

                FenceOffset = offset * 0.5f;
                FenceBeam = new CFenceBeam();
                FenceBeam.Initialize(World);
                FenceBeam.Physics.Position = Physics.Position + FenceOffset;
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

        public override void Delete()
        {
            if (FenceBeam != null)
                World.EntityDelete(FenceBeam);
            base.Delete();
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.Position;
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

        protected override bool DoesGenerateCorpse()
        {
            return true;
        }
    }
}

