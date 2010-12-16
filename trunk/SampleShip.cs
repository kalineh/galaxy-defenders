//
// SampleShip.cs
//

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

        public CSampleShip(CGalaxy game, CWorld world, Vector2 position, PlayerIndex player_index)
        {
            Game = game;
            World = world;
            Ship = CShipFactory.GenerateShip(World, CSaveData.CreateDefaultProfile("Sample").Game.Pilots[(int)player_index], player_index);
            Ship.Physics.Position = position;
        }

        public override void Update()
        {
            Ship.Physics.Velocity = Vector2.UnitX.Rotate(Ship.Physics.Rotation) * 1.5f;
            Ship.Physics.AngularFriction = 0.01f;
            Ship.Physics.AngularVelocity = 0.025f;
            Ship.Physics.Solve();
            Ship.Visual.Update();

            if (World.Random.NextFloat() < 0.09f)
                Ship.FireAllWeapons();

            Ship.UpdateWeapons();
        }

        public override void Draw()
        {
            Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.FrontToBack, SaveStateMode.None, World.GameCamera.WorldMatrix);
            Ship.Draw(Game.DefaultSpriteBatch);
            Game.DefaultSpriteBatch.End();
        }
    }
}
