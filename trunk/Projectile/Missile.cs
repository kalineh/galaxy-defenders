//
// Missile.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Galaxy
{
    public class CMissile
        : CEntity
    {
        public static CMissile Spawn(CWorld world, Vector2 position, float rotation, float speed, float damage)
        {
            CMissile missile = new CMissile(world, damage);

            missile.Physics.AnglePhysics.Rotation = rotation;
            missile.Physics.PositionPhysics.Position = position;
            missile.Physics.PositionPhysics.Velocity = Vector2.UnitX.Rotate(rotation) * speed;

            world.EntityAdd(missile);

            return missile;
        }

        public float Damage { get; private set; }

        public CMissile(CWorld world, float damage)
            : base(world, "Missile")
        {
            Physics = new CPhysics();
            Visual = new CVisual(world.Game.Content.Load<Texture2D>("Missile"), Color.White);
            Collision = new CollisionAABB(Vector2.Zero, new Vector2(1.0f, 0.5f));
            Damage = damage;
        }

        public override void Update()
        {
            base.Update();

            if (!IsInScreen())
                Die();
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
