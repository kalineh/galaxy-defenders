﻿//
// Effect.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Galaxy
{
    class CEffect
    {
        public static CEntity Explosion(CWorld world, Vector2 position, float scale)
        {
            COneShotAnimation animation = new COneShotAnimation(world,
                new COneShotAnimation.Settings()
                {
                    Position = position,
                    Rotation = world.Random.NextAngle(),
                    TextureName = "Textures/Effects/Explosion",
                    TileX = 4,
                    TileY = 4,
                    AnimationSpeed = 1.0f,
                    Scale = scale,
                }
            );

            world.Sound.Play("ExplosionSound", 0.1f);
            world.EntityAdd(animation);
            return animation;
        }

        public static CEntity PlayerTakeDamage(CShip ship, Vector2 position, float scale)
        {
            COneShotAnimation animation = new COneShotAnimation(ship.World,
                new COneShotAnimation.Settings()
                {
                    Position = position,
                    Rotation = ship.World.Random.NextAngle(),
                    TextureName = "Textures/Effects/Explosion",
                    TileX = 4,
                    TileY = 4,
                    AnimationSpeed = 1.0f,
                    Scale = scale,
                }
            );

            animation.AttachToEntity = ship;

            ship.World.Sound.Play("ExplosionSound", 0.125f);
            ship.World.EntityAdd(animation);
            return animation;
        }

        public static CEntity PlayerTakeShieldDamage(CShip ship, Vector2 position, float scale)
        {
            COneShotAnimation animation = new COneShotAnimation(ship.World,
                new COneShotAnimation.Settings()
                {
                    Position = position,
                    Rotation = ship.World.Random.NextAngle(),
                    TextureName = "Textures/Effects/LaserHit",
                    TileX = 4,
                    TileY = 4,
                    AnimationSpeed = 1.0f,
                    Scale = scale,
                }
            );

            animation.AttachToEntity = ship;

            ship.World.Sound.Play("ExplosionSound", 0.2f);
            ship.World.EntityAdd(animation);
            return animation;
        }

        public static CEntity LaserHit(CWorld world, Vector2 position, float scale)
        {
            COneShotAnimation animation = new COneShotAnimation(world,
                new COneShotAnimation.Settings()
                {
                    Position = position,
                    Rotation = world.Random.NextAngle(),
                    TextureName = "Textures/Effects/LaserHit",
                    TileX = 4,
                    TileY = 4,
                    AnimationSpeed = 1.0f,
                    Scale = scale,
                }
            );

            world.Sound.Play("LaserHit", 0.4f);
            world.EntityAdd(animation);
            return animation;
        }

        public static CEntity MissileTrail(CWorld world, Vector2 position, float scale)
        {
            COneShotAnimation animation = new COneShotAnimation(world,
                new COneShotAnimation.Settings()
                {
                    Position = position,
                    Rotation = world.Random.NextAngle(),
                    TextureName = "Textures/Effects/LaserHit",
                    TileX = 4,
                    TileY = 4,
                    AnimationSpeed = 1.0f,
                    Scale = scale,
                }
            );

            world.EntityAdd(animation);
            return animation;
        }
    }
}
