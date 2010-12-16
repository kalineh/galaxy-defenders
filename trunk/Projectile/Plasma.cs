//
// Plasma.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CPlasma
        : CProjectile
    {
        public override void Initialize(CWorld world, CShip owner, float damage)
        {
            base.Initialize(world, owner, damage);

            if (damage < 1.0f)
            {
                Physics = new CPhysics();
                Visual = CVisual.MakeSpriteCachedForPlayer(world.Game, "Textures/Weapons/SmallPlasma", owner.PlayerIndex);
                Visual.Color = CShip.GetPlayerColor(owner.PlayerIndex);
                Visual.Update();
                Collision = CCollision.GetCacheAABB(this, Vector2.Zero, new Vector2(1.0f, 0.5f));
            }
            else
            {
                Physics = new CPhysics();
                Visual = CVisual.MakeSpriteCachedForPlayer(world.Game, "Textures/Weapons/BigPlasma", owner.PlayerIndex);
                Visual.Color = CShip.GetPlayerColor(owner.PlayerIndex);
                Visual.Update();
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
            // TODO: plasma hit effect
            CEffect.LaserHit(World, Physics.PositionPhysics.Position, Physics.PositionPhysics.Velocity.Normal(), 1.0f, Visual.Color);
            //CAudio.PlaySound("WeaponHitPlasma", 1.0f);
            base.OnDie();
        }
    }
}
