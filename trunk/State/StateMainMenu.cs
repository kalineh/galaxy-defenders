//
// StateMainMenu.cs
//

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
            TitleTexture = CContent.LoadTexture2D(Game, "Textures/UI/Title");
            EmptyWorld = new CWorld(game);
            Stars = new CStars(EmptyWorld, CContent.LoadTexture2D(Game, "Textures/Background/Star"), 1.0f, 3.0f);
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

            // TODO: organize debug somewhere?
            if (Game.Input.IsKeyPressed(Keys.F3))
            {
                Game.State = new CStateEditor(Game);
            }
        }

        public override void Draw()
        {
            Game.GraphicsDevice.Clear(new Color(133, 145, 181));

            Game.DefaultSpriteBatch.Begin();

            Stars.Draw(Game.DefaultSpriteBatch);
            Game.DefaultSpriteBatch.Draw(TitleTexture, new Vector2(250.0f, 100.0f), Color.White);
            Menu.Draw(Game.DefaultSpriteBatch);

            Game.DefaultSpriteBatch.End();
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
