﻿//
// StateGame.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Galaxy
{
    public class CStateGame
        : CState
    {
        public CGalaxy Game { get; private set; }
        public CWorld World { get; private set; }

        public CStateGame(CGalaxy game)
        {
            Game = game;
            World = new CWorld(game);
            World.Start();
        }

        public override void Update()
        {
            World.Update();

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Game.State = new CStateFadeTo(Game, this, new CStateMainMenu(Game));
            }
        }

        public override void Draw(SpriteBatch sprite_batch)
        {
            World.Draw(sprite_batch);
        }
    }
}