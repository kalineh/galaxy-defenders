﻿//
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
        private CBuilding Preloaded { get; set; }

        public override void Initialize(CWorld world)
        {
            CBuilding building = new CBuilding();

            building.Initialize(world);
            building.Physics.Position = Position;
            building.Coins = Coins;
            building.Powerup = Powerup;
            building.TextureName = TextureName;
            building.UpdateDefinition();

            //building.UpdateTexture();

            Preloaded = building;
        }

        public override void Update(CWorld world)
        {
            world.EntityAdd(Preloaded);
        }
    }
}
