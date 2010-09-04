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

            world.ScrollSpeed = MathHelper.Lerp(world.ScrollSpeed, 32.0f, 0.015f);
        }

        public override bool IsExpired()
        {
            return false;
        }
    }
}
