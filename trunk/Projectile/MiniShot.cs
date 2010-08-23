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
        public static CMiniShot Spawn(CWorld world, Vector2 position, float rotation, float speed, float damage, PlayerIndex index)
        {
            CMiniShot mini_shot = new CMiniShot();
            mini_shot.Initialize(world, index, damage);

            mini_shot.Physics.AnglePhysics.Rotation = rotation;
            mini_shot.Physics.PositionPhysics.Position = position;
            mini_shot.Physics.PositionPhysics.Velocity = Vector2.UnitX.Rotate(rotation) * speed;

            world.EntityAdd(mini_shot);

            return mini_shot;
        }

        public override void Initialize(CWorld world, PlayerIndex index, float damage)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Visual = CVisual.MakeSpriteCachedForPlayer(world, "Textures/Weapons/MiniShot", index);
            Visual.Color = CShip.GetPlayerColor(index);
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
            circle.Position = Physics.PositionPhysics.Position;
        }

        protected override void OnDie()
        {
            CAudio.PlaySound("WeaponHitMiniShot", 1.0f);
            base.OnDie();
        }
    }
}
