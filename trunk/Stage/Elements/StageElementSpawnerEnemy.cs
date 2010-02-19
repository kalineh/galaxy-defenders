//
// StageElementSpawnerEnemy.cs
//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using Microsoft.Xna.Framework;

namespace Galaxy
{
    public class CStageElementSpawnerEnemy
        : CStageElementSpawnerEntity
    {
        public int Coins { get; set; }
        public bool Powerup { get; set; }
    }
}


