//
// StageElementSpawnerEnemy.cs
//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Xna.Framework;

namespace Galaxy
{
    public class CStageElementSpawnerEnemy
        : CStageElementSpawnerEntity
    {
        public bool NoDropCoins { get; set; }
        public bool Powerup { get; set; }

        public override void Update(CWorld world)
        {
            base.Update(world);
            CEnemy enemy = Preloaded as CEnemy;
            if (NoDropCoins)
                enemy.Coins = 0;
            enemy.Powerup = Powerup;
        }
    }
}


