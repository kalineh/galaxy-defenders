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
        private static WaveBank PauseMusicWaveBank { get; set; }
        private static WaveBank SecretMusicWaveBank { get; set; }
        private static SoundBank MusicSoundBank { get; set; }
        private static SoundBank PauseMusicSoundBank { get; set; }
        private static SoundBank SecretMusicSoundBank { get; set; }

        private static string CurrentMusicName { get; set; }
        private static Cue CurrentMusic { get; set; }
        private static Cue CurrentPauseMusic { get; set; }
        private static Cue CurrentSecretMusic { get; set; }

        private static AudioCategory SFXCategory { get; set; }
        private static AudioCategory MusicCategory { get; set; }
        private static AudioCategory PauseMusicCategory { get; set; }
        private static AudioCategory SecretMusicCategory { get; set; }

        // NOTE: no GetVolume in AudioCategory :(
        private static float SFXVolume { get; set; }
        private static float MusicVolume { get; set; }

        public delegate void MusicChangeDelegate(string music_name);
        public static MusicChangeDelegate OnMusicChange { get; set; }

        public static CMusicBin MusicBin { get; set; }

        public static void Initialize(CGalaxy Game)
        {
            AudioEngine = new AudioEngine("Content/XACT/galaxy.xgs");
            WaveBank = new WaveBank(AudioEngine, "Content/XACT/SFX.xwb");
            SoundBank = new SoundBank(AudioEngine, "Content/XACT/SFX.xsb");
            MusicWaveBank = new WaveBank(AudioEngine, "Content/XACT/Music.xwb", 0, 8);
            PauseMusicWaveBank = new WaveBank(AudioEngine, "Content/XACT/PauseMusic.xwb", 0, 8);
            SecretMusicWaveBank = new WaveBank(AudioEngine, "Content/XACT/SecretMusic.xwb", 0, 8);
            MusicSoundBank = new SoundBank(AudioEngine, "Content/XACT/Music.xsb");
            PauseMusicSoundBank = new SoundBank(AudioEngine, "Content/XACT/PauseMusic.xsb");
            SecretMusicSoundBank = new SoundBank(AudioEngine, "Content/XACT/SecretMusic.xsb");

            // cannot play before first update, so just update in advance
            AudioEngine.Update();

            SFXCategory = AudioEngine.GetCategory("SFX");
            MusicCategory = AudioEngine.GetCategory("Music");
            PauseMusicCategory = AudioEngine.GetCategory("PauseMusic");
            SecretMusicCategory = AudioEngine.GetCategory("SecretMusic");

            MusicBin = new CMusicBin(Game);
        }

        public static void Update()
        {
            AudioEngine.Update();
            UpdateMusicBin();
        }

        public static void UpdateMusicBin()
        {
            if (CurrentMusic == null)
                return;

            if (CurrentPauseMusic != null)
                return;

            //if (!CurrentMusic.IsPrepared)
                //return;

            if (!CurrentMusic.IsPlaying)
                return;

            if (CurrentMusic.IsPaused)
                return;

            MusicBin.Tick();
        }

        public static void Shutdown()
        {
            if (AudioEngine != null)
                AudioEngine.Dispose();

            if (CurrentMusic != null)
            {
                CurrentMusic = null;
                CurrentMusicName = null;
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

        public static Cue GetSFXCue(string name)
        {
            Cue cue = SoundBank.GetCue(name);
            return cue;
        }

        public static void PlayMusic(string name)
        {
            if (CurrentPauseMusic != null)
            {
                if (CurrentMusic != null && CurrentMusic.IsPaused)
                    CurrentMusic.Resume();
            }

            if (CurrentMusicName == name)
                return;

            CurrentMusicName = name;
            CurrentMusic = MusicSoundBank.GetCue(name);
            CurrentMusic.Play();

            if (OnMusicChange != null)
                OnMusicChange(name);

            MusicBin.OpenMusic(String.Format("Content/MusicBin/{0}.mid.bin", name));
        }

        public static void StopMusic()
        {
            if (CurrentMusic == null)
                return;

            CurrentMusic.Stop(AudioStopOptions.AsAuthored);
            CurrentMusic = null;
            CurrentMusicName = null;
        }

        public static void PauseMusic()
        {
            if (CurrentMusic == null)
                return;

            CurrentMusic.Pause();
        }

        public static void UnpauseMusic()
        {
            if (CurrentMusic == null)
                return;

            CurrentMusic.Resume();
        }

        public static void PlayPauseMusic(string name)
        {
            if (CurrentMusic != null)
                CurrentMusic.Pause();

            if (CurrentSecretMusic != null)
                CurrentSecretMusic.Pause();

            while (!PauseMusicWaveBank.IsPrepared)
            {
                AudioEngine.Update();    
            }

            CurrentPauseMusic = PauseMusicSoundBank.GetCue(name);
            CurrentPauseMusic.Play();
        }

        public static void StopPauseMusic()
        {
            if (CurrentPauseMusic != null)
            {
                CurrentPauseMusic.Stop(AudioStopOptions.AsAuthored);
                CurrentPauseMusic = null;
            }

            if (CurrentSecretMusic != null && CurrentSecretMusic.IsPaused)
                CurrentSecretMusic.Resume();

            if (CurrentMusic != null && CurrentMusic.IsPaused)
            {
                CurrentMusic.Resume();
            }
        }

        public static void PlaySecretMusic(string name)
        {
            CurrentSecretMusic = SecretMusicSoundBank.GetCue(name);
            CurrentSecretMusic.Play();
        }

        public static void StopSecretMusic()
        {
            CurrentSecretMusic.Stop(AudioStopOptions.AsAuthored);
            CurrentSecretMusic = null;
        }

        public static void SetSFXVolume(float volume)
        {
            SFXVolume = volume;    
            SFXCategory.SetVolume(volume);
        }

        public static void SetMusicVolume(float volume)
        {
            MusicVolume = volume;    
            MusicCategory.SetVolume(volume);
            PauseMusicCategory.SetVolume(volume);
        }

        public static float GetSFXVolume()
        {
            return SFXVolume;    
        }

        public static float GetMusicVolume()
        {
            return MusicVolume;    
        }

        public static float GetMusicTimeMilliseconds()
        {
            // only supporting visualization effects for main music
            if (CurrentMusic != null)
                return CurrentMusic.GetVariable("AttackTime");

            return 0.0f;
        }

        public static Dictionary<string, float> MusicDurations = new Dictionary<string, float>()
        {
            // NOTE: just rough approximations
            { "The_Voyage", 2.0f },
            { "Alkali Earth", 4.0f },
            { "Luminosity_and_Viscosity", 2.6f },
            { "Sirius", 2.0f },
            { "Aerial_Assault", 2.4f },
            { "Afraid_of_Darkness", 3.1f },
            { "Insurrection", 2.3f },
            { "Troubled_Dreams", 2.1f },
            { "Turbo", 2.8f },
            { "fighting_for_control", 1.8f },
            { "Math_Party", 2.5f },
            { "Eye_of_the_Predator", 4.3f },
            { "The_Hidden_Answer", 1.5f },
        };

        public static float GetMusicDurationSeconds(string name)
        {
            // XACT doesnt seem to have any way to get this data :(
            float result = 0.0f;
            MusicDurations.TryGetValue(name, out result);
            return result;
        }
    }
}
