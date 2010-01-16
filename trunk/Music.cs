//
// Music.cs
//
// TODO: MediaPlayer.Volume access causes a big delay -- replace with Audio stuff.
//

using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Storage;

namespace Galaxy
{
    public class CFader
    {
        public static void FadeIn(object obj)
        {
            Song song = obj as Song;
            MediaPlayer.Volume = 0.0f;
            MediaPlayer.Play(song);

            float volume = MediaPlayer.Volume;
            while (volume < 1.0f)
            {
                volume += 0.025f;
                volume = MathHelper.Min(volume, 1.0f);
                //Console.WriteLine("FadeIn(): Volume: {0}", volume);
                MediaPlayer.Volume = volume;
                Thread.Sleep(100);
            }
        }

        public static void FadeOut(object obj)
        {
            float volume = MediaPlayer.Volume;
            while (volume > 0.0f)
            {
                volume -= 0.025f;
                volume = MathHelper.Max(volume, 0.0f);
                //Console.WriteLine("FadeOut(): Volume: {0}", volume);
                MediaPlayer.Volume = volume;
                Thread.Sleep(100);
            }

            MediaPlayer.Stop();
        }

        public static void FadeInFromZero(object obj)
        {
            MediaPlayer.Volume = 0.0f;
            FadeIn(obj);
        }

        public static void FadeOutThenIn(object obj)
        {
            FadeOut(obj);
            FadeIn(obj);
        }
    }

    public class CMusic
    {
        public CGalaxy Galaxy { get; private set; }
        public Song Song { get; private set; }
        private Thread Fader { get; set; }

        public CMusic(CGalaxy galaxy)
        {
            Galaxy = galaxy;
            Fader = null;
        }

        public void Play(string song_name)
        {
            // TODO: enable when issue 1 is fixed
            return;

            /*
            Song = Galaxy.Content.Load<Song>(song_name);

            if (Fader != null)
            {
                Fader.Abort();
            }

            Fader = new Thread(new ParameterizedThreadStart(CFader.FadeInFromZero));
            Fader.Name = "MusicFadeThread";
            Fader.Start(Song);
            */
        }

        public void Stop()
        {
            if (Fader != null)
            {
                Fader.Abort();
            }

            Fader = new Thread(new ParameterizedThreadStart(CFader.FadeOut));
            Fader.Start(Song);
        }

        public void Update()
        {
        }
    }
}
