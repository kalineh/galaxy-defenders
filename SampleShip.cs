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
            Ship = CShipFactory.GenerateShip(World, CSaveData.GetCurrentProfile(), player_index);
            Ship.Physics.PositionPhysics.Position = position;
        }

        public override void Update()
        {
            Ship.Physics.AnglePhysics.AngularFriction = 0.01f;
            Ship.Physics.AnglePhysics.AngularVelocity = 0.025f;
            Ship.Physics.AnglePhysics.Solve();
            Ship.Physics.PositionPhysics.Velocity = Vector2.UnitX.Rotate(Ship.Physics.AnglePhysics.Rotation) * 1.5f;
            Ship.Physics.PositionPhysics.Solve();
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
