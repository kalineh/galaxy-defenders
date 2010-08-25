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
           
            // TODO: not hack into the stage definition!
            if (enemies)
            {
                world.Game.StageDefinition.ScrollSpeed = MathHelper.Lerp(world.Game.StageDefinition.ScrollSpeed, 0.0f, 0.05f);
                return;
            }

            world.Game.StageDefinition.ScrollSpeed = MathHelper.Lerp(world.Game.StageDefinition.ScrollSpeed, 32.0f, 0.015f);
        }

        public override bool IsExpired()
        {
            return false;
        }
    }
}
