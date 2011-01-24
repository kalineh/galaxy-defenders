//
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

        public override void Update(CWorld world)
        {
            bool enemies = EnemiesExist(world);
           
            if (enemies)
            {
                world.ScrollSpeed = MathHelper.Lerp(world.ScrollSpeed, 0.0f, 0.05f);
                return;
            }

            // no ships! cannot end
            if (world.ShipEntitiesCache.Count == 0)
                return;

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
                world.StageClearPanel.Start();
            }

            if (StageEndCountdown > 240)
            {
                world.ScrollSpeed = MathHelper.Lerp(world.ScrollSpeed, 32.0f, 0.035f);
            }

            if (StageEndCountdown == 1)
                StageEndCameraPosition = world.GameCamera.Position.ToVector2();


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
            }

            ExplosionSoundDelay -= 1;
        }

        public override bool IsExpired()
        {
            return false;
        }
    }
}
