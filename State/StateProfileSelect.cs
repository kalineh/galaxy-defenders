﻿//
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
        public CMenu Menu { get; set; }

        public CStateProfileSelect(CGalaxy game)
        {
            Game = game;
            TitleTexture = CContent.LoadTexture2D(Game, "Textures/UI/Title");
            EmptyWorld = new CWorld(game, null);
            Menu = new CMenu(game)
            {
                Position = new Vector2(Game.GraphicsDevice.Viewport.Width / 2.0f - 90.0f, 350.0f),
                MenuOptions = new List<CMenu.MenuOption>()
                {
                    new CMenu.MenuOption() { Text = "* New Profile", Select = NewProfile },
                }
            };

            foreach (SProfile profile in CSaveData.SaveData.Profiles)
            {
                Menu.MenuOptions.Add( new CMenu.MenuOption() { Text = profile.Name, Select = SelectProfile, Data = profile } );
            }

            Menu.MenuOptions.Add( new CMenu.MenuOption() { Text = "Back", Select = Back, CancelOption = true } );
        }

        public override void Update()
        {
            Menu.Update();
        }

        public override void Draw()
        {
            Game.GraphicsDevice.Clear(Color.Black);

            Game.DefaultSpriteBatch.Begin();

            Game.DefaultSpriteBatch.Draw(TitleTexture, new Vector2(Game.GraphicsDevice.Viewport.Width / 2.0f - 128.0f, 200.0f), Color.White);
            Menu.Draw(Game.DefaultSpriteBatch);

            Game.DefaultSpriteBatch.End();

            EmptyWorld.DrawHuds(EmptyWorld.GameCamera);
        }

        public void NewProfile(object tag)
        {
            CSaveData.AddNewProfile("New User");
            CSaveData.SetCurrentProfile("New User");
            Game.State = new CStateFadeTo(Game, this, new CStatePilotSelect(Game));
        }

        public void SelectProfile(object tag)
        {
            SProfile profile = (SProfile)Menu.MenuOptions[ Menu.Cursor ].Data;
            CSaveData.SetCurrentProfile(profile.Name);
            CSaveData.Save();
            Game.State = new CStateFadeTo(Game, this, new CStateMainMenu(Game));
        }

        public void Back(object tag)
        {
            Game.State = new CStateFadeTo(Game, this, new CStateMainMenu(Game));
        }
    }
}
