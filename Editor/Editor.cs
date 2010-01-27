//
// Editor.cs
//

using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CEditor
    {
        public CGalaxy Game { get; set; }
        public CWorld World { get; set; }

        public CEditor(CGalaxy game)
        {
            Game = game;
            World = new CWorld(game);
        }

        public void Update()
        {
            World.Update(); 
        }

        public void Draw()
        {
            World.Draw(); 
        }
    }
}
