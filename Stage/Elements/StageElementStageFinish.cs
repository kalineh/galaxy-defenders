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
        public override void Update(CWorld world)
        {
            // TODO: is this a good place to add score to money?
            // TODO: 2p
            SProfile profile = CSaveData.GetCurrentProfile();
            profile.Money += world.Score;
            // TODO: not this, but save somehow
            CShip ship = world.GetNearestShip(Vector2.Zero);
            if (ship != null)
            {
                profile.WeaponPrimaryType = ship.WeaponPrimaryType;
                profile.WeaponPrimaryLevel = ship.WeaponPrimaryLevel;
                profile.WeaponSecondaryType = ship.WeaponSecondaryType;
                profile.WeaponSecondaryLevel = ship.WeaponSecondaryLevel;
            }

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


