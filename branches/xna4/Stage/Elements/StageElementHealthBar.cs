//
// StageElementHealthBar.cs
//

using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System;

namespace Galaxy
{
    // TODO: rename appropriately for stage-end-boss-camera-stop
    public class CStageElementHealthBar
        : CStageElement
    {
        private CHealthBar HealthBar { get; set; }

        public override void Initialize(CWorld world)
        {
            // not needed
            HealthBar = new CHealthBar();
            HealthBar.Initialize(world);
            world.EntityAdd(HealthBar);
        }

        public override void Update(CWorld world)
        {
        }

        public override bool IsExpired()
        {
            return true;
        }
    }
}
