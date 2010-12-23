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
                Visual = CVisual.MakeSpriteCachedForPlayer(world.Game, "Textures/Weapons/SmallPlasma", owner.GameControllerIndex);
                Visual.Color = CShip.GetPlayerColor(owner.GameControllerIndex);
                Visual.Update();
                Collision = CCollision.GetCacheAABB(this, Vector2.Zero, new Vector2(1.0f, 0.5f));
            }
            else
            {
                Physics = new CPhysics();
                Visual = CVisual.MakeSpriteCachedForPlayer(world.Game, "Textures/Weapons/BigPlasma", owner.GameControllerIndex);
                Visual.Color = CShip.GetPlayerColor(owner.GameControllerIndex);
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
            box.Position = Physics.Position;
        }

        protected override void OnDie()
        {
            World.ParticleEffects.Spawn(EParticleType.WeaponPlasmaHit, Physics.Position, Visual.Color, null, -Physics.Velocity.Normal());
            base.OnDie();
        }
    }
}
