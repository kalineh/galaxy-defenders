//
// StateFadeTo.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
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

        public CStateFadeTo(CGalaxy game, CState source, CState target)
        {
            Game = game;
            Source = source;
            Target = target;
            Fader = new CFader(Game);
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

        public override void Draw(SpriteBatch sprite_batch)
        {
            if (Fader.IsFadeIn())
            {
                Source.Draw(sprite_batch);
            }

            if (Fader.IsFadeOut())
            {
                Target.Draw(sprite_batch);
            }

            sprite_batch.Begin();
            Fader.Draw(sprite_batch);
            sprite_batch.End();
        }
    }
}
