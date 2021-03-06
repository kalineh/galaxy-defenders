﻿//
// StageElementCameraStop.cs
//

using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System;

namespace Galaxy
{
    // TODO: rename appropriately for stage-end-boss-camera-stop
    public class CStageElementCameraStop
        : CStageElement
    {
        private int StageEndCountdown { get; set; }
        private Vector2 StageEndCameraPosition { get; set; }
        private int ExplosionSoundDelay { get; set; }

        private bool EnemiesExist(CWorld world)
        {
            foreach (CEntity entity in world.GetEntities())
            {
                // ignore
                if (entity.GetType() == typeof(CFence))
                    continue;

                if (entity.GetType().IsSubclassOf(typeof(CEnemy)))
                    return true;
            }

            return false;
        }

        public override void Initialize(CWorld world)
        {
            // not needed
        }

        public override void Update(CWorld world)
        {
            if (!world.IsSecretWorld)
            {
                bool enemies = EnemiesExist(world);

                if (enemies)
                {
                    world.ScrollSpeed = MathHelper.Lerp(world.ScrollSpeed, 0.0f, 0.05f);
                    return;
                }
            }

            // HACK: this is not the right place for this
            if (world.IsSecretWorld)
            {
                // HACK: danger, bad hackings
                world.StageEnd = true;

                foreach (CShip ship in world.ShipEntitiesCache)
                {
                    ship.Physics.Velocity += Vector2.UnitY * -0.1f * StageEndCountdown;
                    world.ParticleEffects.Spawn(EParticleType.PlayerStageEndShipTrail, ship.Physics.Position, ship.Visual.Color, null, null);
                }
            }

            // no ships! cannot end
            if (world.ShipEntitiesCache.Count == 0)
                return;

            // skip ahead to finish phase for secret stage
            if (world.IsSecretWorld)
                StageEndCountdown = Math.Min(StageEndCountdown, 190);

            StageEndCountdown += 1;
            if (StageEndCountdown == 220)
            {
                world.DestroyAllProjectiles();
                world.StartStageEnd();

                foreach (CShip ship in world.ShipEntitiesCache)
                {
                    ship.IsInvincible += 1;    
                }

                world.StageEnd = true;
                world.StageEndCounter = Math.Min(world.StageEndCounter, 120);
                world.StageClearPanel.Start();
            }

            if (StageEndCountdown > 240)
            {
                world.ScrollSpeed = MathHelper.Lerp(world.ScrollSpeed, 32.0f, 0.035f);
            }

            if (StageEndCountdown == 1)
                StageEndCameraPosition = world.GameCamera.Position.ToVector2();

            if (world.IsSecretWorld)
                return;

            if (StageEndCountdown < 240)
            {
                BossExplosionEffects(world);
                BossExplosionSounds(world);
            }
        }

        public void BossExplosionEffects(CWorld world)
        {
            EParticleType type = EParticleType.EnemyDeathExplosion;
            Vector2 position = new Vector2(
                world.Random.NextFloat() * world.Game.Resolution.X / 2.0f * world.Random.NextSign(),
                world.Random.NextFloat() * world.Game.Resolution.Y / 2.0f * world.Random.NextSign()
            );
            world.ParticleEffects.Spawn(type, StageEndCameraPosition + position, null, 1.0f, null);
        }

        public void BossExplosionSounds(CWorld world)
        {
            if (ExplosionSoundDelay == 0)
            {
                if (world.Random.NextFloat() < 0.5f)
                {
                    string death_sound = CEnemy.EnemyDeathSoundStrings[world.Random.Next() % 3];
                    CAudio.PlaySound(death_sound);
                }
                else
                {
                    CAudio.PlaySound("BuildingExplode");
                }

                ExplosionSoundDelay = 6 + (int)(world.Random.NextFloat() * 8.0f);

                world.GameCamera.Shake(0.5f, 2.0f);
            }

            ExplosionSoundDelay -= 1;
        }

        public override bool IsExpired()
        {
            return false;
        }
    }
}
