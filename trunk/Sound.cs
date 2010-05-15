//
// Sound.cs
//

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections;

namespace Galaxy
{
    public class CSound
    {
        public CWorld World { get; set; }

        public class CEntry 
        {
            public List<SoundEffectInstance> Instances = new List<SoundEffectInstance>();
            public int MaxConcurrent = 4;
            public int MinSpacing = 1;
        }
        public Dictionary<string, CEntry> Registry { get; set; }
        public Dictionary<string, bool> PlayLock { get; set; }

        public CFiberManager Fibers { get; set; }

        public float MainVolume { get; set; }

        public AudioEngine AudioEngine { get; set; }
        public SoundBank SoundBank { get; set; }

        public CSound(CWorld world)
        {
            World = world;
            Registry = new Dictionary<string, CEntry>() {


                // SOUND LIST

                // 
                /* 
                 * MenuMoveItem
                 * MenuSelect
                 * MenuCancel
                 * MenuBuy
                 * MenuUpgrade
                 * MenuDowngrade
                 * 
                 * PlayerShieldHit
                 * PlayerArmorHit
                 * PlayerArmorWarning
                 * PlayerDie
                 * 
                 * WeaponShootLaser
                 * WeaponShootPlasma
                 * WeaponShootMissile
                 * WeaponShootSeekBomb
                 * WeaponShootMiniShot
                 * 
                 * WeaponHitLaser
                 * WeaponHitPlasma
                 * WeaponHitMissile
                 * WeaponHitSeekBomb
                 * WeaponHitMiniShot
                 * 
                 * BuildingExplode
                 * BonusGet
                 * BigBonusGet
                 * 
                 * EnemyDie
                 * PowerupShipDie
                 * 
                 */


                { "LaserHit", new CEntry() { MaxConcurrent = 4, MinSpacing = 2 } },
                { "ExplosionSound", new CEntry() { MaxConcurrent = 2, MinSpacing = 2 } },
                { "PlayerShieldHit", new CEntry() { MaxConcurrent = 1, MinSpacing = 4 } },
                { "BonusGet", new CEntry() { MaxConcurrent = 1, MinSpacing = 2 } },
            };
            PlayLock = new Dictionary<string, bool>();
            Fibers = new CFiberManager();

            //AudioEngine = new AudioEngine(

            MainVolume = 0.4f;
        }

        public static void DirectPlay(CGalaxy game, string sound, float volume)
        {
            SoundEffect effect = game.Content.Load<SoundEffect>("SE/" + sound);
            SoundEffectInstance instance = effect.CreateInstance();
            instance.Volume = volume;
            instance.Play();
        }

        public void Play(string sound)
        {
            Play(sound, 1.0f, 0.0f, 0.0f);
        }

        public void Play(string sound, float volume)
        {
            Play(sound, volume, 0.0f, 0.0f);
        }

        public void Play(string sound, float volume, float pitch, float pan)
        {
            // TODO: get-or-create impl
            if (!Registry.ContainsKey(sound))
                Registry.Add(sound, new CEntry());

            CEntry entry = Registry[sound];
            if (entry.Instances.Count >= entry.MaxConcurrent)
                return;

            if (PlayLock.ContainsKey(sound))
                return;

            SoundEffect effect = World.Game.Content.Load<SoundEffect>("SE/" + sound);
            SoundEffectInstance instance = effect.CreateInstance();
            instance.Volume = volume * MainVolume;
            instance.Pitch = pitch;
            instance.Pan = pan;
            instance.Play();
            entry.Instances.Add(instance);

            Fibers.Fork(() => _RemoveWhenDone(sound, effect.Duration));

            // NOTE: take lock in current frame so lock processing is correct for subsequent sounds this frame
            PlayLock.Add(sound, true);
            Fibers.Fork(() => _LockSoundPlayability(sound, entry.MinSpacing));
        }

        public void Update()
        {
            Fibers.Update();
        }

        public IEnumerable _RemoveWhenDone(string name, TimeSpan wait)
        {
            int frames = Time.ToFrames((float)wait.TotalSeconds);
            while (frames-- > 0)
                yield return frames;

            List<SoundEffectInstance> instances = Registry[name].Instances;
            instances.RemoveAt(0);
        }

        public IEnumerable _LockSoundPlayability(string name, int frames)
        {
            while (frames-- > 0)
                yield return frames;
            PlayLock.Remove(name);
        }
    }
}
