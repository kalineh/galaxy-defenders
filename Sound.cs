//
// Sound.cs
//

using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections;

namespace Galaxy
{
    public class CAudio
    {
        private static AudioEngine AudioEngine { get; set; }
        private static WaveBank WaveBank { get; set; }
        private static SoundBank SoundBank { get; set; }

        private static WaveBank MusicWaveBank { get; set; }
        private static SoundBank MusicSoundBank { get; set; }

        private static Cue CurrentMusic { get; set; }

        public static void Initialize()
        {
            AudioEngine = new AudioEngine("Content/XACT/galaxy.xgs");
            WaveBank = new WaveBank(AudioEngine, "Content/XACT/SFX.xwb");
            SoundBank = new SoundBank(AudioEngine, "Content/XACT/SFX.xsb");
            MusicWaveBank = new WaveBank(AudioEngine, "Content/XACT/Music.xwb", 0, 8);
            MusicSoundBank = new SoundBank(AudioEngine, "Content/XACT/Music.xsb");

            // cannot play before first update, so just update in advance
            AudioEngine.Update();
        }

        public static void Update()
        {
            AudioEngine.Update();
        }

        public static void Shutdown()
        {
            AudioEngine.Dispose();
            SoundBank.Dispose();

            if (CurrentMusic != null)
            {
                CurrentMusic.Dispose();
                CurrentMusic = null;
            }
        }

        public static void PlaySound(string name)
        {
            PlaySound(name, 1.0f);
        }

        public static void PlaySound(string name, float volume)
        {
            Cue cue = SoundBank.GetCue(name);
            cue.Play();
        }

        public static void PlayMusic(string name)
        {
            CurrentMusic = MusicSoundBank.GetCue(name);
            CurrentMusic.Play();
        }

        public static void StopMusic()
        {
            if (CurrentMusic == null)
                return;

            CurrentMusic.Stop(AudioStopOptions.AsAuthored);
            CurrentMusic = null;
        }
    }
}
