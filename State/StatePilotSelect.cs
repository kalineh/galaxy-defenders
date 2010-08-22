//
// StatePilotSelect.cs
//

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Galaxy
{
    public class CStatePilotSelect
        : CState
    {
        public CGalaxy Game { get; set; }
        public Texture2D TitleTexture { get; set; }
        private CWorld EmptyWorld { get; set; }
        public CMenu Menu { get; set; }
        public CShip SampleShip { get; set; }

        public CStatePilotSelect(CGalaxy game)
        {
            Game = game;
            TitleTexture = CContent.LoadTexture2D(Game, "Textures/UI/Title");
            EmptyWorld = new CWorld(game);
            Menu = new CMenu(game)
            {
                Position = new Vector2(Game.GraphicsDevice.Viewport.Width / 2.0f - 90.0f, 350.0f),
                MenuOptions = new List<CMenu.MenuOption>()
                {
                    new CMenu.MenuOption() { Text = "Kazuki", Select = SelectPilot, Highlight = HighlightPilot, Data = "Kazuki" },
                    new CMenu.MenuOption() { Text = "Rabbit", Select = SelectPilot, Highlight = HighlightPilot, Data = "Rabbit" },
                    new CMenu.MenuOption() { Text = "Gunthor", Select = SelectPilot, Highlight = HighlightPilot, Data = "Gunthor" },
                    //new CMenu.MenuOption() { Text = "???", Highlight = HighlightPilot, SelectValidate = ValidatePilot, Data = "Mystery" },
                }
            };

            SampleShip = CShipFactory.GenerateShip(EmptyWorld, CSaveData.GetCurrentProfile(), PlayerIndex.One);
            SampleShip.Physics.PositionPhysics.Position = new Vector2(-50.0f, 150.0f);

            EmptyWorld.Scenery = CSceneryPresets.BlueSky(EmptyWorld);

            if (!Game.EditorMode)
                CAudio.PlayMusic("Title");
        }

        public override void Update()
        {
            Menu.Update();

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
            EmptyWorld.DrawHuds(EmptyWorld.GameCamera);

            Game.DefaultSpriteBatch.Begin();
            Game.DefaultSpriteBatch.Draw(TitleTexture, new Vector2(Game.GraphicsDevice.Viewport.Width / 2.0f - 128.0f, 200.0f), Color.White);
            Menu.Draw(Game.DefaultSpriteBatch);
            Game.DefaultSpriteBatch.End();

        }

        private void SelectPilot(object tag)
        {
            SProfile profile = CSaveData.GetCurrentProfile();
            profile.Pilot = (string)tag ?? "";
            CSaveData.SetCurrentProfileData(profile);
            Game.State = new CStateFadeTo(Game, this, new CStateMainMenu(Game));
        }

        private void HighlightPilot(object tag)
        {
            SProfile profile = CSaveData.GetCurrentProfile();
            profile.Pilot = (string)tag ?? "";
            CSaveData.SetCurrentProfileData(profile);
            EmptyWorld.Huds[0].UpdatePilot();
        }

        private bool ValidatePilot(object tag)
        {
            return CSaveData.GetCurrentProfile().HasClearedGame;
        }

        private void Back(object tag)
        {
            Game.State = new CStateFadeTo(Game, this, new CStateProfileSelect(Game));
        }
    }
}
