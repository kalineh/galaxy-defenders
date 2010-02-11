//
// StateGame.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Galaxy
{
    public class CStateGame
        : CState
    {
        public CGalaxy Game { get; private set; }
        public CWorld World { get; private set; }

        public CStateGame(CGalaxy game)
        {
            Game = game;
            Game.GameFrame = 0;
            World = new CWorld(game);
            World.Start();
        }

        public override void Update()
        {
            World.Update();

            if (Game.Input.IsKeyPressed(Keys.Escape))
            {
                // TODO: is this a good place?
                CSaveData.Save();
                Game.State = new CStateFadeTo(Game, this, new CStateMainMenu(Game));
            }
        }

        public override void Draw()
        {
            World.Draw();
        }
    }
}
