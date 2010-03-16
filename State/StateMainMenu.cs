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
            EmptyWorld = new CWorld(game);
            Menu = new CMenu(game)
            {
                Position = new Vector2(500.0f, 300.0f),
                MenuOptions = new List<CMenu.MenuOption>()
                {
                    new CMenu.MenuOption() { Text = "Start Game", Function = StartGame },
                    new CMenu.MenuOption() { Text = "Select Profile", Function = SelectProfile },
                    new CMenu.MenuOption() { Text = "Quit", Function = Quit },
                }
            };
            SampleShip = new CShip(EmptyWorld, CSaveData.GetCurrentProfile(), new Vector2(-200.0f, 0.0f));

            EmptyWorld.Scenery = new CSceneryChain(EmptyWorld,
                new CBackground(EmptyWorld, new Color(133, 145, 181)),
                new CStars(EmptyWorld, CContent.LoadTexture2D(Game, "Textures/Background/Star"), 0.4f, 1.2f)
            );
        }

        public override void Update()
        {
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
            EmptyWorld.Scenery.Update();
            EmptyWorld.GameCamera.Position = new Vector3(0.0f, 0.0f, 0.0f);
        }

        public override void Draw()
        {
            Game.GraphicsDevice.Clear(Color.Black);
            EmptyWorld.DrawBackground(EmptyWorld.GameCamera);

            Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.FrontToBack, SaveStateMode.None, EmptyWorld.GameCamera.WorldMatrix);
            SampleShip.Draw(Game.DefaultSpriteBatch);
            Game.DefaultSpriteBatch.End();

            EmptyWorld.DrawEntities(EmptyWorld.GameCamera);

            Game.DefaultSpriteBatch.Begin();
            Game.DefaultSpriteBatch.Draw(TitleTexture, new Vector2(250.0f, 100.0f), Color.White);
            Menu.Draw(Game.DefaultSpriteBatch);
            Game.DefaultSpriteBatch.End();
        }

        private void StartGame(object tag)
        {
            Game.State = new CStateFadeTo(Game, this, new CStateShop(Game));
        }

        private void SelectProfile(object tag)
        {
            Game.State = new CStateFadeTo(Game, this, new CStateProfileSelect(Game));
        }

        private void Quit(object tag)
        {
            Game.Exit();
        }
    }
}
