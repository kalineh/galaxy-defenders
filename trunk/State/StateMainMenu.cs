//
// StateMainMenu.cs
//

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CStateMainMenu
        : CState
    {
        public CGalaxy Game { get; set; }
        public Texture2D TitleTexture { get; set; }
        private CWorld EmptyWorld { get; set; }
        private CStars Stars { get; set; }
        public CMenu Menu { get; set; }

        public CStateMainMenu(CGalaxy game)
        {
            Game = game;
            TitleTexture = Game.Content.Load<Texture2D>("Title");
            EmptyWorld = new CWorld(game);
            Stars = new CStars(EmptyWorld, Game.Content.Load<Texture2D>("Star"), 1.0f, 3.0f);
            Menu = new CMenu(game)
            {
                Position = new Vector2(300.0f, 300.0f),
                MenuOptions = new List<CMenu.MenuOption>()
                {
                    new CMenu.MenuOption() { Text = "Start Game", Function = StartGame },
                    new CMenu.MenuOption() { Text = "Select Profile", Function = SelectProfile },
                    new CMenu.MenuOption() { Text = "Quit", Function = Quit },
                }
            };
        }

        public override void Update()
        {
            Stars.Update();
            Menu.Update();

            // TODO: organize debug somewhere?
            if (Game.Input.IsKeyPressed(Keys.F2))
            {
                Game.State = new CStateGame(Game);
            }
        }

        public override void Draw(SpriteBatch sprite_batch)
        {
            Game.GraphicsDevice.Clear(Color.Black);

            sprite_batch.Begin();

            Stars.Draw(sprite_batch);
            sprite_batch.Draw(TitleTexture, new Vector2(250.0f, 100.0f), Color.White);
            Menu.Draw(sprite_batch);

            sprite_batch.End();
        }

        private void StartGame()
        {
            Game.State = new CStateFadeTo(Game, this, new CStateShop(Game));
        }

        private void SelectProfile()
        {
            Game.State = new CStateFadeTo(Game, this, new CStateProfileSelect(Game));
        }

        private void Quit()
        {
            Game.Exit();
        }
    }
}
