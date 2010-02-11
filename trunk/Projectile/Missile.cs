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
        public float Speed { get; set; }

        public static CMissile Spawn(CWorld world, Vector2 position, float rotation, float speed, float damage)
        {
            CMissile missile = new CMissile(world, damage);

            missile.Speed = speed;

            missile.Physics.AnglePhysics.Rotation = MathHelper.ToRadians(-90.0f);
            missile.Physics.PositionPhysics.Position = position;
            missile.Physics.PositionPhysics.Velocity = Vector2.UnitX.Rotate(rotation) * 6.0f;

            world.EntityAdd(missile);

            return missile;
        }

        public float Damage { get; private set; }

        public CMissile(CWorld world, float damage)
            : base(world, "Missile")
        {
            Physics = new CPhysics();
            Visual = new CVisual(CContent.LoadTexture2D(world.Game, "Textures/Weapons/Missile"), Color.White);
            Collision = new CollisionAABB(Vector2.Zero, new Vector2(1.0f, 0.5f));
            Damage = damage;
        }

        public override void Update()
        {
            Physics.PositionPhysics.Velocity = Vector2.Lerp(Physics.PositionPhysics.Velocity, -Vector2.UnitY * Speed, 0.05f);

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

        public void OnCollide(CAsteroid asteroid)
        {
            Die();
        }

        protected override void OnDie()
        {
            CExplosion.Spawn(World, Physics.PositionPhysics.Position, 2.5f);
            base.OnDie();
        }
    }
}
