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
        public override void Update(CWorld world)
        {
            int enemies = world.GetEntities().Where(e => e.GetType().IsSubclassOf(typeof(CEnemy))).Count();
           
            // TODO: not hack into the stage definition!
            if (enemies > 0)
            {
                world.Game.StageDefinition.ScrollSpeed = MathHelper.Lerp(world.Game.StageDefinition.ScrollSpeed, 0.0f, 0.05f);
                return;
            }

            world.Game.StageDefinition.ScrollSpeed = MathHelper.Lerp(world.Game.StageDefinition.ScrollSpeed, 100.0f, 0.005f);
        }

        public override bool IsExpired()
        {
            return false;
        }
    }
}
