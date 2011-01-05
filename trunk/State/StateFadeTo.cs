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
        public static CStateFadeTo InProgress { get; set; }

        public CGalaxy Game { get; private set; }
        public CState Source { get; private set; }
        public CState Target { get; private set; }
        public CFader Fader { get; private set; }
        private SpriteBatch SpriteBatch { get; set; }
        public bool NoExitSource { get; set; }

        public CStateFadeTo(CGalaxy game, CState source, CState target)
        {
            Game = game;
            Source = source;
            Target = target;
            Fader = new CFader(Game);
            SpriteBatch = new SpriteBatch(game.GraphicsDevice);

            if (InProgress != null)
            {
                // HACK: ensure that existing fadeout has OnExit called if we skip to a new state quickly
                if (!InProgress.Fader.IsComplete())
                {
                    if (!InProgress.NoExitSource)
                    {
                        InProgress.Source.OnExit();
                    }
                }
            }
            InProgress = this;
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
                if (!NoExitSource)
                {
                    Source.OnExit();
                    Source = null;
                    InProgress = null;
                }

                System.GC.Collect();
                Target.OnEnter();
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

            SpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None, Game.RenderScaleMatrix);
            Fader.Draw(SpriteBatch);
            SpriteBatch.End();
        }
    }

    public class CStateChangeTo
        : CState
    {
        public CStateChangeTo(CGalaxy game, CState source, CState target)
        {
            source.OnExit();
            System.GC.Collect();
            target.OnEnter();
            game.State = target;
        }

        public override void Update()
        {
        }

        public override void Draw()
        {
        }
    }
}
