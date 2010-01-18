//
// StateShop.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CStateShop
        : CState
    {
        public CGalaxy Game { get; set; }
        private CWorld EmptyWorld { get; set; }
        private CStars Stars { get; set; }
        private int Cursor { get; set; }

        public CStateShop(CGalaxy game)
        {
            Game = game;
            EmptyWorld = new CWorld(game);
            Stars = new CStars(EmptyWorld, Game.Content.Load<Texture2D>("Star"), 1.0f, 3.0f);
            Cursor = 0;
        }

        public override void Update()
        {
            Stars.Update();

            Cursor += Keyboard.GetState().IsKeyDown(Keys.Down) ? 1 : 0;
            Cursor -= Keyboard.GetState().IsKeyDown(Keys.Up) ? 1 : 0;

            Cursor = Math.Min(Cursor, 3);
            Cursor = Math.Max(Cursor, 0);

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                switch (Cursor)
                {
                    case 0:
                        break;

                    case 1:
                        Game.Exit();
                        break;
                }
            }
        }

        public override void Draw(SpriteBatch sprite_batch)
        {
            Game.GraphicsDevice.Clear(Color.Black);

            sprite_batch.Begin();

            Stars.Draw(sprite_batch);

            sprite_batch.DrawString(Game.DefaultFont, "Start Game", new Vector2(330, 300), Color.White);
            sprite_batch.DrawString(Game.DefaultFont, "   Quit   ", new Vector2(330, 320), Color.White);
            sprite_batch.DrawString(Game.DefaultFont, ">", new Vector2(310, 300 + 20 * Cursor), Color.White);

            sprite_batch.End();
        }
    }
}
