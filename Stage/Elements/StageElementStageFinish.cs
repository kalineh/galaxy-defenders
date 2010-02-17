//
// StageElementStageFinish.cs
//

using System.Collections.Generic;
using System.Linq;

namespace Galaxy
{
    public class CStageElementStageFinish
        : CStageElement
    {
        public override void Update(CWorld world)
        {
            world.Game.State = new CStateFadeTo(world.Game, world.Game.State, new CStateMainMenu(world.Game));
        }

        public override bool IsExpired()
        {
            return true;
        }
    }
}


