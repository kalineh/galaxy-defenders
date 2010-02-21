//
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

            SoundEffect sound = world.Game.Content.Load<SoundEffect>("SE/ExplosionSound");
            sound.Play(0.1f, 0.0f, 0.0f);

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

            SoundEffect sound = ship.World.Game.Content.Load<SoundEffect>("SE/ExplosionSound");
            sound.Play(0.125f, 0.0f, 0.0f);

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

            SoundEffect sound = ship.World.Game.Content.Load<SoundEffect>("SE/LaserHit");
            sound.Play(0.2f, 0.0f, 0.0f);

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

            // sfx? too urusai?
            SoundEffect sound = world.Game.Content.Load<SoundEffect>("SE/LaserHit");
            sound.Play(0.5f, 0.0f, 0.0f);

            world.EntityAdd(animation);
            return animation;
        }
    }
}

