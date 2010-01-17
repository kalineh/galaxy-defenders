//
// Explosion.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

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

            SoundEffect sound = world.Game.Content.Load<SoundEffect>("ExplosionSound");
            sound.Play(0.2f, 0.0f, 0.0f);

            world.EntityAdd(animation);
            return animation;
        }
    }
}

