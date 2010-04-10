//
// StateFadeTo.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CStateFadeTo
        : CState
    {
        public CGalaxy Game { get; private set; }
        public CState Source { get; private set; }
        public CState Target { get; private set; }
        public CFader Fader { get; private set; }
        private SpriteBatch SpriteBatch { get; set; }

        public CStateFadeTo(CGalaxy game, CState source, CState target)
        {
            Game = game;
            Source = source;
            Target = target;
            Fader = new CFader(Game);
            SpriteBatch = new SpriteBatch(game.GraphicsDevice);
        }

        public override void Update()
        {
            Fader.Update();

            if (Fader.IsFadeOut())
            {
                Target.Update();
            }

            if (Fader.IsComplete())
            {
                Game.State = Target;
            }
        }

        public override void Draw()
        {
            if (Fader.IsFadeIn())
            {
                Source.Draw();
            }

            if (Fader.IsFadeOut())
            {
                Target.Draw();
            }

            SpriteBatch.Begin();
            Fader.Draw(SpriteBatch);
            SpriteBatch.End();
        }
    }
}
