//
// MiniShot.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CMiniShot
        : CEntity
    {
        public static CMiniShot Spawn(CWorld world, Vector2 position, float rotation, float speed, float damage, PlayerIndex index)
        {
            CMiniShot laser = new CMiniShot(world, damage, index);

            laser.Physics.AnglePhysics.Rotation = rotation;
            laser.Physics.PositionPhysics.Position = position;
            laser.Physics.PositionPhysics.Velocity = Vector2.UnitX.Rotate(rotation) * speed;

            world.EntityAdd(laser);

            return laser;
        }

        public float Damage { get; private set; }

        public CMiniShot(CWorld world, float damage, PlayerIndex index)
            : base(world)
        {
            Physics = new CPhysics();
            Visual = CVisual.MakeSpriteCachedForPlayer(world, "Textures/Weapons/MiniShot", index);
            Visual.Color = CShip.GetPlayerColor(index);
            Visual.Update();
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 1.0f);
            Damage = damage;
            IgnoreCameraScroll = true;
        }

#if XBOX360
        public CMiniShot()
        {
        }

        public void Init360(CWorld world, float damage, PlayerIndex index)
        {
            base.Init360(world);
            Physics = new CPhysics();
            Visual = new CVisual(world, CContent.LoadTexture2D(world.Game, "Textures/Weapons/MiniShot"), CShip.GetPlayerColor(index));
            Visual.Color = CShip.GetPlayerColor(index);
            Visual.Update();
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 1.0f);
            Damage = damage;
            IgnoreCameraScroll = true;
        }
#endif

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
    }
}
