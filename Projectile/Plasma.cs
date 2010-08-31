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
        public override void Initialize(CWorld world, PlayerIndex index, float damage)
        {
            base.Initialize(world, index, damage);

            if (damage < 1.0f)
            {
                Physics = new CPhysics();
                Visual = new CVisual(world, CContent.LoadTexture2D(world.Game, "Textures/Weapons/SmallPlasma"), CShip.GetPlayerColor(index));
                Visual.Color = CShip.GetPlayerColor(index);
                Visual.Update();
                Collision = CCollision.GetCacheAABB(this, Vector2.Zero, new Vector2(1.0f, 0.5f));
            }
            else
            {
                Physics = new CPhysics();
                Visual = new CVisual(world, CContent.LoadTexture2D(world.Game, "Textures/Weapons/BigPlasma"), CShip.GetPlayerColor(index));
                Visual.Color = CShip.GetPlayerColor(index);
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
