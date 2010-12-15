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
        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Collision = CCollision.GetCacheAABB(this, Vector2.Zero, Vector2.Zero);
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Enemy/FenceBeam");
            Visual.TileY = 4;
            Visual.AnimationSpeed = 0.25f;
            Visual.Depth = CLayers.Enemy + CLayers.SubLayerIncrement * -2.0f;
        }

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
            World.Stats.CollisionDamageReceived += 2.0f;
            Vector2 collision_point = Physics.PositionPhysics.Position;
            collision_point.X = ship.Physics.PositionPhysics.Position.X;
            ship.TakeCollideDamage(collision_point, 2.0f);
        }
    }
}

