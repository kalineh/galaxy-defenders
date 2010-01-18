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
            world.Game.State = new CStateFadeTo(world.Game, world.Game.State, new CStateMainMenu(world.Game));
        }

        public override bool IsExpired()
        {
            return false;
        }
    }
}


