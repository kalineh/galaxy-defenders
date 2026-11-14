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
        }

        public void Update()
        {
            int connected = World.Game.Input.CountConnectedControllers();
            if (SampleShips == null || (SampleShips != null && SampleShips.Count != connected))
            {
                if (connected == 1)
                {
                    SampleShips = new List<CSampleShip>() {
                        new CSampleShip(World.Game, World, new Vector2(-50.0f, 250.0f), GameControllerIndex.One),
                    };
                }
                else if (connected == 2)
                {
                    SampleShips = new List<CSampleShip>() {
                        new CSampleShip(World.Game, World, new Vector2(-50.0f, 250.0f), GameControllerIndex.One),
                        new CSampleShip(World.Game, World, new Vector2(0.0f, 150.0f), GameControllerIndex.Two),
                    };
                }
            }
            
            if (SampleShips == null)
                return;

            int players = World.Game.Input.CountConnectedControllers();
            for (int i = 0; i < players; ++i)
            {
                CSampleShip sample = SampleShips[i];
                sample.Update();
            }
        }

        public void Draw()
        {
            if (SampleShips == null)
                return;

            for (int i = 0; i < SampleShips.Count; ++i)
            {
                CSampleShip sample = SampleShips[i];
                sample.Draw();
            }
        }
    }
}
