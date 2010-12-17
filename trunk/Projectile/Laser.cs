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
        public override void Initialize(CWorld world, CShip owner, float damage)
        {
            base.Initialize(world, owner, damage);

            if (damage < 0.4f)
            {
                Visual = CVisual.MakeSpriteCachedForPlayer(world.Game, "Textures/Weapons/Laser", owner.GameControllerIndex);
                Visual.Color = CShip.GetPlayerColor(owner.GameControllerIndex);
                Visual.UpdateColor();
                Collision = CCollision.GetCacheAABB(this, Vector2.Zero, new Vector2(1.0f, 0.5f));
            }
            else
            {
                Visual = CVisual.MakeSpriteCachedForPlayer(world.Game, "Textures/Weapons/BigLaser", owner.GameControllerIndex);
                Visual.Color = CShip.GetPlayerColor(owner.GameControllerIndex);
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
            box.Position = Physics.Position;
        }

        protected override void OnDie()
        {
            CEffect.LaserHit(World, Physics.Position, Physics.Velocity.Normal(), 1.0f, Visual.Color);
            base.OnDie();
        }
    }
}
