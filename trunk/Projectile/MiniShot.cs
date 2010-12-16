//
// MiniShot.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CMiniShot
        : CProjectile
    {
        public static CMiniShot Spawn(CShip owner, Vector2 position, float rotation, float speed, float damage)
        {
            CMiniShot mini_shot = new CMiniShot();
            mini_shot.Initialize(owner.World, owner, damage);

            mini_shot.Physics.Rotation = rotation;
            mini_shot.Physics.Position = position;
            mini_shot.Physics.Velocity = Vector2.UnitX.Rotate(rotation) * speed;

            owner.World.EntityAdd(mini_shot);

            return mini_shot;
        }

        public override void Initialize(CWorld world, CShip owner, float damage)
        {
            base.Initialize(world);

            Owner = owner;
            Physics = new CPhysics();
            Visual = CVisual.MakeSpriteCachedForPlayer(world.Game, "Textures/Weapons/MiniShot", owner.PlayerIndex);
            Visual.Color = CShip.GetPlayerColor(owner.PlayerIndex);
            Visual.Update();
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 1.0f);
            Damage = damage;
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
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.Position;
        }

        protected override void OnDie()
        {
            CAudio.PlaySound("WeaponHitMiniShot", 1.0f);
            base.OnDie();
        }
    }
}
