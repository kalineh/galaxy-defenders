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

//
// TODO: put functionality in here! move functionality from CStateEditor
// TODO: this should be the mock game world, and all the mouse/interface state handling
//

namespace Galaxy
{
    // TODO: camera zoom
    // TODO: drag-scroll
    // TODO: add entity
    // TODO: generate stage to text
    // TODO: place entity
    // TODO: edit entity
    // TODO: load stage definition

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
