﻿//
// StateDifficultySelect.cs
//

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Galaxy
{
    public class CStateDifficultySelect
        : CState
    {
        public CGalaxy Game { get; set; }
        public Texture2D TitleTexture { get; set; }
        private CWorld EmptyWorld { get; set; }
        public CMenu Menu { get; set; }
        public CSampleShipManager SampleShipManager { get; set; }

        public CStateDifficultySelect(CGalaxy game)
        {
            Game = game;
            TitleTexture = CContent.LoadTexture2D(Game, "Textures/UI/Title");
            EmptyWorld = new CWorld(game, null);
            Menu = new CMenu(game)
            {
                Position = new Vector2(Game.GraphicsDevice.Viewport.Width / 2.0f - 128.0f, 400.0f),
                MenuOptions = new List<CMenu.MenuOption>()
                {
                    new CMenu.MenuOption() { Text = "Easy", Select = SelectDifficulty, Data = CDifficulty.DifficultyLevel.Easy },
                    new CMenu.MenuOption() { Text = "Normal", Select = SelectDifficulty, Data = CDifficulty.DifficultyLevel.Normal },
                    new CMenu.MenuOption() { Text = "Hard", Select = SelectDifficulty, Data = CDifficulty.DifficultyLevel.Hard },
                    new CMenu.MenuOption() { Text = "LOL", Select = SelectDifficulty, Data = CDifficulty.DifficultyLevel.LOL },
                    new CMenu.MenuOption() { Text = "Back", Select = Back, CancelOption = true, PanelType = CMenu.PanelType.Small },
                },
            };
            Menu.Cursor = 1;

            SampleShipManager = new CSampleShipManager(EmptyWorld);

            EmptyWorld.BackgroundScenery = CSceneryPresets.BlueSky(EmptyWorld);
            EmptyWorld.ForegroundScenery = CSceneryPresets.Empty(EmptyWorld);

            if (!Game.EditorMode)
                CAudio.PlayMusic("Title");
        }

        public override void Update()
        {
            Menu.Update();

            SampleShipManager.Update();

            EmptyWorld.UpdateEntities();
            EmptyWorld.BackgroundScenery.Update();
            EmptyWorld.ForegroundScenery.Update();
            EmptyWorld.GameCamera.Position = new Vector3(0.0f, 0.0f, 0.0f);

            EmptyWorld.ParticleEffects.Update();
        }

        public override void Draw()
        {
            Game.GraphicsDevice.Clear(Color.Black);
            EmptyWorld.DrawBackground(EmptyWorld.GameCamera);

            SampleShipManager.Draw();

            EmptyWorld.DrawEntities(EmptyWorld.GameCamera);

            Game.DefaultSpriteBatch.Begin();
            Game.DefaultSpriteBatch.Draw(TitleTexture, new Vector2(Game.GraphicsDevice.Viewport.Width / 2.0f - 256.0f, 120.0f), Color.White);
            Menu.Draw(Game.DefaultSpriteBatch);
            Game.DefaultSpriteBatch.End();

            Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None, EmptyWorld.GameCamera.WorldMatrix);
            EmptyWorld.ParticleEffects.Draw(Game.DefaultSpriteBatch);
            Game.DefaultSpriteBatch.End();
        }

        private void SelectDifficulty(object tag)
        {
            SProfile profile = CSaveData.GetCurrentProfile();
            profile.Game.Difficulty = (int)((CDifficulty.DifficultyLevel)tag);
            CSaveData.SetCurrentProfileData(profile);
            Game.State = new CStateFadeTo(Game, this, new CStateStateShop(Game));
        }

        private void Back(object tag)
        {
            Game.State = new CStateFadeTo(Game, this, new CStateMainMenu(Game));
        }
    }
}
