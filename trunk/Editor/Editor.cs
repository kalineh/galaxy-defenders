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

        public void Draw(SpriteBatch sprite_batch)
        {
            Game.GraphicsDevice.Clear(ClearOptions.Target, Color.Black, 0.0f, 0);

            World.Draw(sprite_batch); 

            sprite_batch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.FrontToBack, SaveStateMode.None);
            sprite_batch.End();
        }
    }
}
