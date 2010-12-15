//
// Laser.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CLaser
        : CProjectile
    {
        public override void Initialize(CWorld world, PlayerIndex index, float damage)
        {
            base.Initialize(world, index, damage);

            if (damage < 0.4f)
            {
                Visual = CVisual.MakeSpriteCachedForPlayer(world.Game, "Textures/Weapons/Laser", index);
                Visual.Color = CShip.GetPlayerColor(index);
                Visual.UpdateColor();
                Collision = CCollision.GetCacheAABB(this, Vector2.Zero, new Vector2(1.0f, 0.5f));
            }
            else
            {
                Visual = CVisual.MakeSpriteCachedForPlayer(world.Game, "Textures/Weapons/BigLaser", index);
                Visual.Color = CShip.GetPlayerColor(index);
                Visual.UpdateColor();
                Collision = CCollision.GetCacheAABB(this, Vector2.Zero, new Vector2(1.0f, 0.5f));
            }
        }

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
