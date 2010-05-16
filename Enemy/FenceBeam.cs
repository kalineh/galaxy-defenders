//
// FenceBeam.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Galaxy
{
    public class CFenceBeam
        : CEntity
    {
        public CFenceBeam(CWorld world, Vector2 position)
            : base(world)
        {
            Physics = new CPhysics();
            Physics.PositionPhysics.Position = position;
            Collision = CCollision.GetCacheAABB(this, position, Vector2.Zero);
            Visual = new CVisual(World, CContent.LoadTexture2D(World.Game, "Textures/Enemy/FenceBeam"), Color.White);
            Visual.TileY = 4;
            Visual.AnimationSpeed = 0.25f;
            Visual.Depth = CLayers.Enemy + CLayers.SubLayerIncrement * -1.0f;
        }

#if XBOX360

        public CFenceBeam()
    	{
    	}

        public void Init360(CWorld world, Vector2 position)
        {
            base.Init360(world);

            Physics = new CPhysics();
            Physics.PositionPhysics.Position = position;
            Collision = CCollision.GetCacheAABB(this, position, Vector2.Zero);
            Visual = new CVisual(World, CContent.LoadTexture2D(World.Game, "Textures/Enemy/FenceBeam"), Color.White);
            Visual.TileY = 4;
            Visual.AnimationSpeed = 0.25f;
            Visual.Depth = CLayers.Enemy + CLayers.SubLayerIncrement * -1.0f;
        }
#endif

        public void UpdateAttachment(CFence owner)
        {
            Physics.PositionPhysics.Position = owner.Physics.PositionPhysics.Position + owner.FenceOffset;

            CollisionAABB aabb = Collision as CollisionAABB;
            aabb.Position = Physics.PositionPhysics.Position - owner.FenceOffset;
            aabb.Size = new Vector2(owner.FenceOffset.X * 2.0f, 8.0f); 
        }

        public override void UpdateCollision()
        {
            // done in UpdateAttachment()
        }

        public void OnCollide(CShip ship)
        {
            Vector2 collision_point = Physics.PositionPhysics.Position;
            collision_point.X = ship.Physics.PositionPhysics.Position.X;
            ship.TakeCollideDamage(collision_point, 1.0f);
        }
    }
}

