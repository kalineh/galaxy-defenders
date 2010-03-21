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

            // TODO: is this a good place to add score to money?
            // TODO: 2p
            SProfile profile = CSaveData.GetCurrentProfile();
            profile.Money += world.Score;

            // TODO: not this, but save current ship upgrades to profile
            CShip ship = world.GetNearestShip(Vector2.Zero);
            if (ship != null)
            {
                profile.WeaponPrimaryType = ship.PrimaryWeapon.Type;
                profile.WeaponPrimaryLevel = ship.PrimaryWeapon.Level;
                profile.WeaponSecondaryType = ship.SecondaryWeapon.Type;
                profile.WeaponSecondaryLevel = ship.SecondaryWeapon.Level;
                profile.WeaponSidekickLeftType = ship.SidekickLeft.Type;
                profile.WeaponSidekickLeftLevel = ship.SidekickLeft.Level;
                profile.WeaponSidekickRightType = ship.SidekickRight.Type;
                profile.WeaponSidekickRightLevel = ship.SidekickRight.Level;
                profile.WeaponSecondaryLevel = ship.SecondaryWeapon.Level;
            }

            CSaveData.SetCurrentProfileData(profile);
            CSaveData.Save();

            // TODO: lobby state
            world.Game.State = new CStateFadeTo(world.Game, world.Game.State, new CStateShop(world.Game));
        }

        public override bool IsExpired()
        {
            int enemies = World.GetEntities().Where(e => e.GetType().IsSubclassOf(typeof(CEnemy))).Count();
            // TODO: sleep
            return enemies <= 0;
        }
    }
}


