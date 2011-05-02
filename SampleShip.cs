//
// SampleShip.cs
//

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Galaxy
{
    public class CSampleShip
        : CState
    {
        public CGalaxy Game { get; set; }
        private CWorld World { get; set; }
        public CShip Ship { get; set; }

        public CSampleShip(CGalaxy game, CWorld world, Vector2 position, GameControllerIndex game_controller_index)
        {
            Game = game;
            World = world;

            // NOTE: too confusing as to what is being shown
            //int controllers = Game.Input.CountConnectedControllers();
            //SProfileGameData profile = CSaveData.GetCurrentProfile().Game[controllers - 1];
            //Ship = CShipFactory.GenerateShip(World, profile.Pilots[(int)game_controller_index], game_controller_index);

            Ship = CShipFactory.GenerateShip(World, CSaveData.CreateDefaultProfile("sample").Game[0].Pilots[(int)game_controller_index], game_controller_index);
            Ship.Physics.Position = position;
        }

        public override void Update()
        {
            Ship.Physics.Velocity = Vector2.UnitX.Rotate(Ship.Physics.Rotation) * 1.5f;
            Ship.Physics.AngularFriction = 0.01f;
            Ship.Physics.AngularVelocity = 0.025f;
            Ship.Physics.Solve();
            Ship.Visual.Update();

            Ship.ChargeSidekickLeft();
            Ship.ChargeSidekickRight();

            if (World.Random.NextFloat() < 0.09f)
            {
                Ship.FirePrimarySecondaryWeapons();
                Ship.FireSidekickLeft();
                Ship.FireSidekickRight();
            }

            Ship.UpdateWeapons();
        }

        public override void Draw()
        {
            Game.DefaultSpriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, World.GameCamera.WorldMatrix);
            Ship.Draw(Game.DefaultSpriteBatch);
            Game.DefaultSpriteBatch.End();
        }
    }
}
