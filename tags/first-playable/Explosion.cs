//
// Explosion.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using galaxy;

namespace galaxy
{
    class CExplosion
        : CEntity
    {
        public static CExplosion Spawn(CWorld world, Vector2 position)
        {
            Texture2D texture = world.Game.Content.Load<Texture2D>("Explosion");
            CExplosion explosion = new CExplosion(world, "Explosion", texture, 1.0f);

            explosion.Physics.PositionPhysics.Position = position;
            explosion.Physics.AnglePhysics.Rotation = world.Random.NextAngle();

            world.EntityAdd(explosion);

            return explosion;
        }

        public CExplosion(CWorld world, String name, Texture2D texture, float scale)
            : base(world, name, texture)
        {
            Visual.TileX = 4;
            Visual.TileY = 4;
            Visual.AnimationSpeed = 0.5f;
            Visual.Scale = new Vector2(scale);
            Collision.Enabled = false;
        }

        public override void Update()
        {
            base.Update();

            if (Visual.Frame >= Visual.TileX * Visual.TileY)
                Die();

            if (!IsInScreen())
                Die();
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.PositionPhysics.Position;
            circle.Radius = Visual.GetScaledTextureSize().Length() * 0.1f;
        }

        public void OnCollide(CAsteroid asteroid)
        {
            asteroid.Die();
        }
    }
}

