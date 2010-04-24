//
// Effect.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

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
                    Color = Color.White,
                }
            );

            world.Sound.Play("ExplosionSound", 0.1f);
            world.EntityAdd(animation);

            CParticleGroupSpawner light_spawner = CParticleGroupSpawner.MakeBigExplosion(position, new Color(177, 167, 167));
            light_spawner.Spawn(world.ParticleEffects);

            return animation;
        }

        public static CEntity EnemyExplosion(CWorld world, Vector2 position, float scale)
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
                    Color = Color.White,
                }
            );

            world.Sound.Play("ExplosionSound", 0.1f);
            world.EntityAdd(animation);

            CParticleGroupSpawner light_spawner = CParticleGroupSpawner.MakeBigExplosion(position, new Color(252, 124, 85));
            light_spawner.Spawn(world.ParticleEffects);
            CParticleGroupSpawner dark_spawner = CParticleGroupSpawner.MakeBigExplosion(position, new Color(102, 102, 102));
            dark_spawner.Spawn(world.ParticleEffects);

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
                    Color = Color.White,
                }
            );

            animation.AttachToEntity = ship;

            ship.World.Sound.Play("ExplosionSound", 0.1f);
            ship.World.EntityAdd(animation);

            CParticleGroupSpawner spawner = CParticleGroupSpawner.MakeSmallExplosion(position, new Color(252, 124, 85));
            spawner.Spawn(ship.World.ParticleEffects);

            return animation;
        }

        public static CEntity PlayerTakeShieldDamage(CShip ship, Vector2 position, float scale, Color color)
        {
            COneShotAnimation animation = new COneShotAnimation(ship.World,
                new COneShotAnimation.Settings()
                {
                    Position = position,
                    Rotation = ship.World.Random.NextAngle(),
                    TextureName = "Textures/Effects/PlayerShieldHit",
                    TileX = 4,
                    TileY = 4,
                    AnimationSpeed = 1.0f,
                    Scale = scale,
                    Color = color,
                }
            );

            animation.AttachToEntity = ship;

            ship.World.Sound.Play("PlayerShieldHit", 0.25f);
            ship.World.EntityAdd(animation);

            return animation;
        }

        public static CEntity LaserHit(CWorld world, Vector2 position, float scale, Color color)
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
                    Color = color,
                }
            );

            world.Sound.Play("LaserHit", 0.4f);
            world.EntityAdd(animation);

            CParticleGroupSpawner spawner = CParticleGroupSpawner.MakeSmallExplosion(position, color);
            spawner.Spawn(world.ParticleEffects);

            return animation;
        }

        public static CEntity MissileTrail(CWorld world, Vector2 position, float scale, Color color)
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
                    Color = color,
                }
            );

            world.EntityAdd(animation);
            return animation;
        }
    }
}

