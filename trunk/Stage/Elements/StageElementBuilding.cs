//
// StageElementBuilding.cs
//

using Microsoft.Xna.Framework;
using System.ComponentModel;
using System;
using System.Linq;

namespace Galaxy
{
    public class CStageElementBuilding
        : CStageElement
    {
        public int Coins { get; set; }
        public bool Powerup { get; set; }
        public string TextureName { get; set; }

        public override void Update(CWorld world)
        {
            CBuilding building = new CBuilding(world, Position) {
                Coins = Coins,
                Powerup = Powerup,
                TextureName = TextureName,
            };

            //building.UpdateTexture();

            world.EntityAdd(building);
        }
    }
}
