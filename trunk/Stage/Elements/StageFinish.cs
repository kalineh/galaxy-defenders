//
// StageFinish.cs
//

using System;

namespace Galaxy
{
    public class CStageFinish
        : CStageElement
    {
        public override void Update(CWorld world)
        {
            world.Stop();
        }

        public override bool IsExpired()
        {
            return false;
        }
    }
}


