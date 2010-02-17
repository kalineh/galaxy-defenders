//
// StateProfileSelect.cs
//

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CStateProfileSelect
        : CState
    {
        public CGalaxy Game { get; set; }
        public Texture2D TitleTexture { get; set; }
        private CWorld EmptyWorld { get; set; }
        private CStars Stars { get; set; }
        public CMenu Menu { get; set; }

        public CStateProfileSelect(CGalaxy game)
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
                    new CMenu.MenuOption() { Text = "* New Profile", Function = NewProfile },
                }
            };

            foreach (SProfile profile in CSaveData.SaveData.Profiles)
            {
                Menu.MenuOptions.Add( new CMenu.MenuOption() { Text = profile.Name, Function = () => SelectProfile(), Data = profile } );
            }

            Menu.MenuOptions.Add( new CMenu.MenuOption() { Text = "Back", Function = Back } );
        }

        public override void Update()
        {
            Stars.Update();
            Menu.Update();
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

        public void NewProfile()
        {
            CSaveData.AddNewProfile("New User");
            CSaveData.SetCurrentProfile("New User");
            Game.State = new CStateFadeTo(Game, this, new CStateMainMenu(Game));
        }

        public void SelectProfile()
        {
            SProfile profile = (SProfile)Menu.MenuOptions[ Menu.Cursor ].Data;
            CSaveData.SetCurrentProfile(profile.Name);
            CSaveData.Save();
            Game.State = new CStateFadeTo(Game, this, new CStateMainMenu(Game));
        }

        public void Back()
        {
            Game.State = new CStateFadeTo(Game, this, new CStateMainMenu(Game));
        }
    }
}
