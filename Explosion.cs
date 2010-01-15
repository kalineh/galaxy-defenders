//
// Explosion.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace galaxy
{
    class CExplosion
        : CEntity
    {
        public static CExplosion Spawn(CWorld world, Vector2 position)
        {
            CExplosion explosion = new CExplosion(world, 1.0f);

            explosion.Physics.PositionPhysics.Position = position;
            explosion.Physics.AnglePhysics.Rotation = world.Random.NextAngle();

            world.EntityAdd(explosion);

            return explosion;
        }

        public CExplosion(CWorld world, float scale)
            : base(world, "Explosion")
        {
            Texture2D texture = world.Game.Content.Load<Texture2D>("Explosion");
            Physics = new CPhysics();
            Visual = new CVisual(texture, Color.White);
            Visual.TileX = 4;
            Visual.TileY = 4;
            Visual.AnimationSpeed = 0.5f;
            Visual.Scale = new Vector2(scale);
        }

        public override void Update()
        {
            base.Update();

            if (Visual.Frame >= Visual.TileX * Visual.TileY)
                Die();
        }
    }
}

