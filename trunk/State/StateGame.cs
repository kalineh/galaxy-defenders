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

        // HACK: overload for secret stage
        public CStateGame(CGalaxy game, CStageDefinition stage_definition, CWorld return_world, bool is_secret)
        {
            Game = game;
            Game.GameFrame = 0;

            World = new CWorld(game, stage_definition);

            // NOTE: need to set secret world to provide special secret world music handling
            World.ReturnWorld = return_world;
            World.IsSecretWorld = is_secret;

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

            // NOTE: replaced by pause menu, don't need anymore?
            //if (Game.Input.IsKeyPressed(Keys.Escape) || Game.Input.IsPadBackPressedAny())
            //{
                //CAudio.StopMusic();
                //Game.State = new CStateFadeTo(Game, this, new CStateShop(Game));
            //}
        }

        public override void Draw()
        {
            World.Draw();
        }
    }
}
