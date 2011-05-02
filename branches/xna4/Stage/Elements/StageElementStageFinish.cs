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
        private CWorld World { get; set; }

        public override void Initialize(CWorld world)
        {
            // nothing needed
        }

        public override void Update(CWorld world)
        {
            World = world;

            // TODO: not repeat this check!
            if (IsExpired() == false)
                return;

            if (World.IsSecretWorld)
                World.StartSecretStageFinish();
            // NOTE: doing this in camerastop now for regular stage
            // TODO: cleanup mess
            //else
                //World.StartStageEnd();

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


