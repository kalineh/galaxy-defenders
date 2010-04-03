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
        public int Coins { get; set; }
        public bool Powerup { get; set; }

        public override void Update(CWorld world)
        {
            CEnemy enemy = Spawn(world) as CEnemy;
            enemy.Coins = Coins;
            enemy.Powerup = Powerup;
        }
    }
}


