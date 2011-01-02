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

            StageEndCountdown += 1;
            if (StageEndCountdown == 60)
            {
                world.DestroyAllProjectiles();
                world.StartStageEnd();
                world.StageEnd = true;
            }

            if (StageEndCountdown > 60)
                world.ScrollSpeed = MathHelper.Lerp(world.ScrollSpeed, 32.0f, 0.035f);

        }

        public override bool IsExpired()
        {
            return false;
        }
    }
}
