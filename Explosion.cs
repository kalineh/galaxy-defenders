//
// Explosion.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Galaxy
{
    class CExplosion
    {
        public static CEntity Spawn(CWorld world, Vector2 position, float scale)
        {
            COneShotAnimation animation = new COneShotAnimation(world,
                new COneShotAnimation.Settings()
                {
                    Position = position,
                    Rotation = world.Random.NextAngle(),
                    TextureName = "Explosion",
                    TileX = 4,
                    TileY = 4,
                    AnimationSpeed = 0.5f,
                    Scale = scale,
                }
            );

            world.EntityAdd(animation);
            return animation;
        }
    }
}

