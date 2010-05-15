﻿//
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

            world.EntityAdd(animation);

            CParticleGroupSpawner light_spawner = CParticleGroupSpawner.MakeBigExplosion(position, new Color(177, 167, 167));
            light_spawner.Spawn(world.ParticleEffects);

            return animation;
        }

        public static CEntity MissileExplosion(CWorld world, Vector2 position, float scale, Color color)
        {
            COneShotAnimation animation = new COneShotAnimation(world,
                new COneShotAnimation.Settings()
                {
                    Position = position,
                    Rotation = world.Random.NextAngle(),
                    TextureName = "Textures/Effects/MissileExplosion",
                    TileX = 4,
                    TileY = 4,
                    AnimationSpeed = 1.0f,
                    Scale = scale,
                    Color = color,
                }
            );

            world.Sound.Play("WeaponHitMissile", 1.0f);
            world.EntityAdd(animation);

            CParticleGroupSpawner light_spawner = CParticleGroupSpawner.MakeBigExplosion(position, color);
            light_spawner.Spawn(world.ParticleEffects);

            return animation;
        }

        public static void BuildingExplosion(CWorld world, Vector2 position, float power)
        {
            world.Sound.Play("BuildingExplode", 1.0f);

            CParticleGroupSpawner light_spawner = CParticleGroupSpawner.MakeBuildingExplosion(position, new Color(177, 167, 167), power);
            light_spawner.Spawn(world.ParticleEffects);
            CParticleGroupSpawner dark_spawner = CParticleGroupSpawner.MakeBuildingExplosion(position, new Color(102, 102, 102), power);
            dark_spawner.Spawn(world.ParticleEffects);
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

            string death_sound = "EnemyDie" + (world.Random.Next() % 3 + 1);
            world.Sound.Play(death_sound);
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

            ship.World.Sound.Play("PlayerArmorHit", 1.0f);
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

            ship.World.Sound.Play("PlayerShieldHit", 1.0f);
            ship.World.EntityAdd(animation);

            return animation;
        }

        public static void LaserHit(CWorld world, Vector2 position, Vector2 direction, float scale, Color color)
        {
            world.Sound.Play("WeaponHitLaser", 1.0f);
            CParticleGroupSpawner spawner = CParticleGroupSpawner.MakeSmallDirectionalExplosion(position, direction, color);
            spawner.Spawn(world.ParticleEffects);
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
