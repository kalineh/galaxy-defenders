//
// EnemySpawner.cs
//

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace galaxy
{
    public class CStageEnemySpawner
        : CStageElement
    {
        public delegate CEnemy Spawner(Vector2 position);

        public override void Update(CWorld world)
        {
        }

        public override bool IsExpired()
        {
         	throw new NotImplementedException();
        }
    };
}


