//
// Laser.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CLaser
        : CEntity
    {
        public static CLaser Spawn(CWorld world, Vector2 position, float rotation, float speed, float damage)
        {
            CLaser laser = new CLaser(world, damage);

            laser.Physics.AnglePhysics.Rotation = rotation;
            laser.Physics.PositionPhysics.Position = position;
            laser.Physics.PositionPhysics.Velocity = Vector2.UnitX.Rotate(rotation) * speed;

            world.EntityAdd(laser);

            return laser;
        }

        public float Damage { get; private set; }

        public CLaser(CWorld world, float damage)
            : base(world, "Laser")
        {
            Physics = new CPhysics();
            Visual = new CVisual(world, CContent.LoadTexture2D(world.Game, "Textures/Weapons/Laser"), Color.White);
            Collision = new CollisionAABB(Vector2.Zero, new Vector2(1.0f, 0.5f));
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
            CollisionAABB box = Collision as CollisionAABB;
            box.Position = Physics.PositionPhysics.Position;
        }

        protected override void OnDie()
        {
            CEffect.LaserHit(World, Physics.PositionPhysics.Position, 1.0f);
            base.OnDie();
        }
    }
}
