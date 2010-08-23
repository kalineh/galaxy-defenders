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
            CBuilding building = new CBuilding();

            building.Initialize(world);
            building.Physics.PositionPhysics.Position = Position;
            building.Coins = Coins;
            building.Powerup = Powerup;
            building.TextureName = TextureName;

            //building.UpdateTexture();

            world.EntityAdd(building);
        }
    }
}
