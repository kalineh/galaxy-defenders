//
// Music.cs
//
// TODO: MediaPlayer.Volume access causes a big delay -- replace with Audio stuff.
//

using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using System;

namespace Galaxy
{
    public class CCrossFader
    {
        public static void FadeIn(object obj)
        {
            Song song = obj as Song;
            //MediaPlayer.Volume = 0.0f;
            //MediaPlayer.Play(song);

            float volume = MediaPlayer.Volume;
            while (volume < 0.75f)
            {
                volume += 0.025f;
                volume = MathHelper.Min(volume, 0.75f);
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

            //MediaPlayer.Stop();
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
        private Thread CrossFader { get; set; }

        public CMusic(CGalaxy galaxy)
        {
            Galaxy = galaxy;
            CrossFader = null;
            MediaPlayer.Volume = 0.5f;
        }

        public void Play(string song_name)
        {
            // TODO: debug disable, volume hang bug
#if !XBOX360
            return;
#endif

            Song new_song = Galaxy.Content.Load<Song>(song_name);
            bool from_zero = new_song != Song;
            Song = new_song;

            if (CrossFader != null)
            {
                CrossFader.Abort();
            }

            if (from_zero)
            {
                MediaPlayer.Stop();
                MediaPlayer.Volume = 0.5f;
                try
                {
                    MediaPlayer.Play(Song);
                }
                catch (Exception)
                {
                    return;
                }
            }

            //if (from_zero)
                //CrossFader = new Thread(new ParameterizedThreadStart(CCrossFader.FadeInFromZero));
            //else
                //CrossFader = new Thread(new ParameterizedThreadStart(CCrossFader.FadeIn));
            //CrossFader.Name = "MusicFadeThread";
            // TODO: fix music system
            //CrossFader.Start(Song);
        }

        public void Stop()
        {
            if (CrossFader != null)
            {
                CrossFader.Abort();
            }

#if !XBOX360
            CrossFader = new Thread(new ParameterizedThreadStart(CCrossFader.FadeOut));
            // TODO: fix music system
            CrossFader.Start(Song);
#endif

            MediaPlayer.Stop();
        }

        public void StopImmediate()
        {
            MediaPlayer.Stop();
        }

        public void Update()
        {
        }

        public void SetVolume(float volume)
        {
            MediaPlayer.Volume = volume;
        }
    }
}
