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
            // TODO: is this a good place to add score to money?
            SProfile profile = CSaveData.GetCurrentProfile();
            profile.Money += world.Score;
            CSaveData.SetCurrentProfileData(profile);
            CSaveData.Save();

            // TODO: lobby state
            world.Game.State = new CStateFadeTo(world.Game, world.Game.State, new CStateShop(world.Game));
        }

        public override bool IsExpired()
        {
            return true;
        }
    }
}


