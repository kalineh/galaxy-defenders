//
// StateProfileSelect.cs
//

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
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
            TitleTexture = Game.Content.Load<Texture2D>("Title");
            EmptyWorld = new CWorld(game);
            Stars = new CStars(EmptyWorld, Game.Content.Load<Texture2D>("Star"), 1.0f, 3.0f);
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

        public override void Draw(SpriteBatch sprite_batch)
        {
            Game.GraphicsDevice.Clear(Color.Black);

            sprite_batch.Begin();

            Stars.Draw(sprite_batch);
            sprite_batch.Draw(TitleTexture, new Vector2(250.0f, 100.0f), Color.White);
            Menu.Draw(sprite_batch);

            sprite_batch.End();
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
