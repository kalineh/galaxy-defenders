//
// StageElementStageFinish.cs
//

using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Galaxy
{
    public class CStageElementStageFinish
        : CStageElement
    {
        public CWorld World { get; set; }

        public override void Update(CWorld world)
        {
            World = world;

            // TODO: not repeat this check!
            if (IsExpired() == false)
                return;

            World.StageEnd = true;
        }

        public override bool IsExpired()
        {
            foreach (CEntity entity in World.GetEntities())
            {
                // ignore
                if (entity.GetType() == typeof(CFence))
                    continue;

                if (entity.GetType().IsSubclassOf(typeof(CEnemy)))
                    return false;
            }

            return true;
        }
    }
}


