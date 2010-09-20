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
        
        public CStateGame(CGalaxy game, CStageDefinition stage_definition)
        {
            Game = game;
            Game.GameFrame = 0;

            World = new CWorld(game, stage_definition);
            World.Start();
        }

        public CStateGame(CGalaxy game, CWorld reuse_world)
        {
            Game = game;
            Game.GameFrame = 0;

            World = reuse_world;
            World.ReturnFromSecret();
        }

        public override void OnExit()
        {
            if (World != null)
            {
                World.Stop(); 
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
