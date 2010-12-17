//
// SampleShipManager.cs
//

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Galaxy
{
    public class CSampleShipManager
    {
        private CWorld World { get; set; }
        public List<CSampleShip> SampleShips { get; set; }

        public CSampleShipManager(CWorld world)
        {
            World = world;    

            SampleShips = new List<CSampleShip>() {
                new CSampleShip(world.Game, world, new Vector2(-50.0f, 250.0f), GameControllerIndex.One),
                new CSampleShip(world.Game, world, new Vector2(0.0f, 150.0f), GameControllerIndex.Two),
            };
        }

        public void Update()
        {
            int players = World.Game.Input.CountConnectedControllers();
            for (int i = 0; i < players; ++i)
            {
                CSampleShip sample = SampleShips[i];
                sample.Update();
            }
        }

        public void Draw()
        {
            int players = World.Game.Input.CountConnectedControllers();
            for (int i = 0; i < players; ++i)
            {
                CSampleShip sample = SampleShips[i];
                sample.Draw();
            }
        }
    }
}
