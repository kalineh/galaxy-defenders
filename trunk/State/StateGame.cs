//
// StateGame.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Galaxy
{
    public class CStateGame
        : CState
    {
        public CGalaxy Game { get; private set; }
        public CWorld World { get; private set; }
        
        public CStateGame(CGalaxy game, CWorld reuse_world)
        {
            Game = game;
            Game.GameFrame = 0;

            if (reuse_world == null)
            {
                World = new CWorld(game);
                World.Start();
            }
            else
            {
                World = reuse_world;
                World.SecretEntryCounter = 0;
                World.SecretEntryFader = null;
                World.StageEnd = false;

                foreach (CShip ship in World.GetEntitiesOfType(typeof(CShip)))
                {
                    Vector2 to_center = World.GameCamera.GetCenter().ToVector2() - ship.Physics.PositionPhysics.Position;
                    Vector2 clamped_entry = World.GameCamera.ClampInside(World.SecretEntryPosition, 32.0f);
                    ship.Physics.PositionPhysics.Position = clamped_entry;
                    ship.Physics.PositionPhysics.Velocity = to_center.Normal() * 40.0f;
                }
            }
        }

        public override void Update()
        {
            World.Update();

            if (Game.Input.IsKeyPressed(Keys.Escape) || Game.Input.IsPadBackPressedAny())
            {
                // TODO: is this a good place?
                // TODO: dont want to save here? this should be a cancel, not a save-quit
                //CSaveData.Save();

                CAudio.StopMusic();
                Game.State = new CStateFadeTo(Game, this, new CStateStageSelect(Game));
            }
        }

        public override void Draw()
        {
            World.Draw();
        }
    }
}
