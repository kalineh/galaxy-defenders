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
            public int Limit = 4;
        }
        public Dictionary<string, CEntry> Registry { get; set; }

        public CFiberManager Fibers { get; set; }

        public CSound(CWorld world)
        {
            World = world;
            Registry = new Dictionary<string, CEntry>() {
                { "sample-limit-1", new CEntry() { Limit = 2 } },
                { "sample-limit-2", new CEntry() { Limit = 2 } },
            };

            Fibers = new CFiberManager();
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
            if (entry.Instances.Count >= entry.Limit)
                return;

            SoundEffect effect = World.Game.Content.Load<SoundEffect>("SE/" + sound);
            SoundEffectInstance instance = effect.CreateInstance();
            instance.Volume = volume;
            instance.Pitch = pitch;
            instance.Pan = pan;
            instance.Play();
            entry.Instances.Add(instance);

            Fibers.Fork( () => _RemoveWhenDone(sound, effect.Duration) );
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
    }
}
