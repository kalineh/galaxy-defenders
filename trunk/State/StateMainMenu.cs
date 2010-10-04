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
        public CShip SampleShip { get; set; }

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
                    new CMenu.MenuOption() { Text = "Start Game", Select = StartGame },
                    new CMenu.MenuOption() { Text = "Select Profile", Select = SelectProfile },
                    new CMenu.MenuOption() { Text = "Quit Qame", Select = QuitGame, PanelType = CMenu.PanelType.Small, },
                }
            };
            SampleShip = CShipFactory.GenerateShip(EmptyWorld, CSaveData.GetCurrentProfile(), PlayerIndex.One);
            SampleShip.Physics.PositionPhysics.Position = new Vector2(-50.0f, 150.0f);

            EmptyWorld.BackgroundScenery = CSceneryPresets.BlueSky(EmptyWorld);
            EmptyWorld.ForegroundScenery = CSceneryPresets.Empty(EmptyWorld);

            if (!Game.EditorMode)
                CAudio.PlayMusic("Title");
        }

        public override void Update()
        {
            Menu.Update();

            // TODO: organize debug somewhere?
            if (Game.Input.IsKeyPressed(Keys.F1))
            {
                CStageDefinition stage1 = CStageDefinition.GetStageDefinitionByName("Stage1");
                Game.State = new CStateGame(Game, stage1);
            }

            // allow application exit from main menu
            if (Game.Input.IsPadBackPressedAny() || Game.Input.IsKeyPressed(Keys.Q))
                Game.Exit();

            // sample display
            SampleShip.Physics.AnglePhysics.AngularFriction = 0.01f;
            SampleShip.Physics.AnglePhysics.AngularVelocity = 0.025f;
            SampleShip.Physics.AnglePhysics.Solve();
            SampleShip.Physics.PositionPhysics.Velocity = Vector2.UnitX.Rotate(SampleShip.Physics.AnglePhysics.Rotation) * 1.5f;
            SampleShip.Physics.PositionPhysics.Solve();
            SampleShip.Visual.Update();

            if (EmptyWorld.Random.NextFloat() < 0.09f)
                SampleShip.FireAllWeapons();
            SampleShip.UpdateWeapons();

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

            Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.FrontToBack, SaveStateMode.None, EmptyWorld.GameCamera.WorldMatrix);
            SampleShip.Draw(Game.DefaultSpriteBatch);
            Game.DefaultSpriteBatch.End();

            EmptyWorld.DrawEntities(EmptyWorld.GameCamera);
            EmptyWorld.DrawHuds(EmptyWorld.GameCamera);

            Game.DefaultSpriteBatch.Begin();
            Game.DefaultSpriteBatch.Draw(TitleTexture, new Vector2(Game.GraphicsDevice.Viewport.Width / 2.0f - 256.0f, 120.0f), Color.White);
            Menu.Draw(Game.DefaultSpriteBatch);
            Game.DefaultSpriteBatch.End();

            Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None, EmptyWorld.GameCamera.WorldMatrix);
            EmptyWorld.ParticleEffects.Draw(Game.DefaultSpriteBatch);
            Game.DefaultSpriteBatch.End();
        }

        private void StartGame(object tag)
        {
            Game.State = new CStateFadeTo(Game, this, new CStateDifficultySelect(Game));
        }

        private void SelectProfile(object tag)
        {
            Game.State = new CStateFadeTo(Game, this, new CStateProfileSelect(Game));
        }

        private void QuitGame(object tag)
        {
            Game.Exit();
        }
    }
}
