//
// Laser.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using galaxy;

namespace galaxy
{
    class CLaser
        : CEntity
    {
        public static CLaser Spawn(CWorld world, Vector2 position, float rotation, float speed, float damage)
        {
            Texture2D texture = world.Game.Content.Load<Texture2D>("Laser");
            CLaser laser = new CLaser(world, "Laser", texture, damage);

            laser.Physics.AnglePhysics.Rotation = rotation;
            laser.Physics.PositionPhysics.Position = position;
            laser.Physics.PositionPhysics.Velocity = Vector2.UnitX.Rotate(rotation) * speed;

            world.EntityAdd(laser);

            return laser;
        }

        public float Damage { get; private set; }

        public CLaser(CWorld world, String name, Texture2D texture, float damage)
            : base(world, name, texture)
        {
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
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.PositionPhysics.Position;
            circle.Radius = Visual.GetScaledTextureSize().Length() * 0.2f;
        }

        public void OnCollide(CAsteroid asteroid)
        {
            Die();
        }
    }
}
