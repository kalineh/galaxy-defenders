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
        public CMenu Menu { get; set; }
        public CSampleShipManager SampleShipManager { get; set; }

        public CStateMainMenu(CGalaxy game)
        {
            Game = game;
            TitleTexture = CContent.LoadTexture2D(Game, "Textures/UI/Title");
            EmptyWorld = new CWorld(game, null);
            Menu = new CMenu(game)
            {
                Position = new Vector2(Game.GraphicsDevice.Viewport.Width / 2.0f - 128.0f, 400.0f),
                MenuOptions = new List<CMenu.MenuOption>()
                {
                    new CMenu.MenuOption() { Text = "Solo Game", Select = NewGame1P },
                    new CMenu.MenuOption() { Text = "Coop Game", Select = NewGame2P },
                    new CMenu.MenuOption() { Text = "Quit", Select = QuitGame, PanelType = CMenu.PanelType.Small, },
                },
            };

            SampleShipManager = new CSampleShipManager(EmptyWorld);

            EmptyWorld.BackgroundScenery = CSceneryPresets.BlueSky(EmptyWorld);
            EmptyWorld.ForegroundScenery = CSceneryPresets.Empty(EmptyWorld);

            if (!Game.EditorMode)
                CAudio.PlayMusic("Title");
        }

        public override void Update()
        {
            Menu.Update();
            DebugInput();

            // allow application exit from main menu
            if (Game.Input.IsPadBackPressedAny() || Game.Input.IsKeyPressed(Keys.Q))
                Game.Exit();

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

        private void NewGame1P(object tag)
        {
            string username = "User";

#if XBOX360
            // TODO: get current primary profile username
            username = null;
#endif

            CSaveData.AddNewProfile(username);
            CSaveData.SetCurrentProfile(username);
            SProfile profile = CSaveData.GetCurrentProfile();
            profile.Name = username;
            profile.Game.Players = 1;
            CSaveData.SetCurrentProfileData(profile);
            Game.State = new CStateFadeTo(Game, this, new CStatePilotSelect(Game));
        }

        private void NewGame2P(object tag)
        {
            string username = "User";

#if XBOX360
            // TODO: get current primary profile username
            username = null;
#endif

            CSaveData.AddNewProfile(username);
            CSaveData.SetCurrentProfile(username);
            SProfile profile = CSaveData.GetCurrentProfile();
            profile.Name = username;
            profile.Game.Players = 2;
            CSaveData.SetCurrentProfileData(profile);
            Game.State = new CStateFadeTo(Game, this, new CStatePilotSelect(Game));
        }

        private void QuitGame(object tag)
        {
            Game.Exit();
        }

        private void DebugInput()
        {
#if DEBUG
            if (!Game.Input.IsKeyDown(Keys.LeftControl))
                return;

            if (Game.Input.IsKeyPressed(Keys.F1))
            {
                CStageDefinition stage1 = CStageDefinition.GetStageDefinitionByName("Stage1");
                Game.State = new CStateGame(Game, stage1);
            }
#endif
        }
    }
}
