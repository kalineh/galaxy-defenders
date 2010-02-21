//
// Missile.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CMissile
        : CEntity
    {
        public Vector2 FireVector { get; set; }
        public float Speed { get; set; }

        public static CMissile Spawn(CWorld world, Vector2 position, float rotation, float speed, float damage)
        {
            CMissile missile = new CMissile(world, damage);

            missile.Speed = speed;

            missile.Physics.AnglePhysics.Rotation = rotation - MathHelper.Pi;
            missile.FireVector = Vector2.UnitX.Rotate(rotation);

            missile.Physics.PositionPhysics.Position = position;
            missile.Physics.PositionPhysics.Velocity = missile.FireVector * 6.0f;

            world.EntityAdd(missile);

            return missile;
        }

        public float Damage { get; private set; }

        public CMissile(CWorld world, float damage)
            : base(world)
        {
            Physics = new CPhysics();
            Visual = new CVisual(world, CContent.LoadTexture2D(world.Game, "Textures/Weapons/Missile"), Color.White);
            Collision = new CollisionAABB(Vector2.Zero, new Vector2(1.0f, 0.5f));
            Damage = damage;
            IgnoreCameraScroll = true;
        }

        public override void Update()
        {
            Physics.PositionPhysics.Velocity = Vector2.Lerp(Physics.PositionPhysics.Velocity, -FireVector * Speed, 0.05f);

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
            CEffect.Explosion(World, Physics.PositionPhysics.Position, 2.5f);
            base.OnDie();
        }
    }
}
