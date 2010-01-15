//
// EnemyLaser.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace galaxy
{
    public class CEnemyLaser
        : CEntity
    {
        public static CEnemyLaser Spawn(CWorld world, Vector2 position, float rotation, float speed, float damage)
        {
            CEnemyLaser laser = new CEnemyLaser(world, damage);

            laser.Physics.AnglePhysics.Rotation = rotation;
            laser.Physics.PositionPhysics.Position = position;
            laser.Physics.PositionPhysics.Velocity = Vector2.UnitX.Rotate(rotation) * speed;

            world.EntityAdd(laser);

            return laser;
        }

        public float Damage { get; private set; }

        public CEnemyLaser(CWorld world, float damage)
            : base(world, "EnemyLaser")
        {
            Physics = new CPhysics();
            Visual = new CVisual(world.Game.Content.Load<Texture2D>("EnemyLaser"), Color.White);
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

        public void OnCollide(CShip ship)
        {
            ship.Die();
        }
    }
}
