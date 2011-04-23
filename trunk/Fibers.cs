//
// Fibers.cs
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
    public class CFiber
    {
        public static object Continue = new Object();
        public delegate IEnumerable DExecutable();

        public CFiberManager Manager { get; set; }
        public DExecutable Executable { get; set; }
        public IEnumerable Enumerable { get; set; }
        public IEnumerator Enumerator { get; set; }
        public int Sleeping { get; set; }

        public CFiber(CFiberManager manager, DExecutable executable)
        {
            Manager = manager;
            Executable = executable;
        }
    }

    public class CFiberManager
    {
        public List<CFiber> ActiveFibers { get; set; }
        public List<CFiber> SleepingFibers { get; set; }

        public CFiberManager()
        {
            ActiveFibers = new List<CFiber>();
            SleepingFibers = new List<CFiber>();
        }

        public CFiber Fork(CFiber.DExecutable executable)
        {
            CFiber result = new CFiber(this, executable);
            ActiveFibers.Add(result);
            return result;
        }

        public void Update()
        {
            for (int i = 0; i < SleepingFibers.Count; ++i)
            {
                CFiber fiber = SleepingFibers[i];
                if (--fiber.Sleeping <= 0)
                {
                    SleepingFibers[i] = SleepingFibers[SleepingFibers.Count - 1];
                    SleepingFibers.RemoveAt(SleepingFibers.Count - 1);
                    ActiveFibers.Add(fiber);
                }
            }

            for (int i = 0; i < ActiveFibers.Count; ++i)
            {
                CFiber fiber = ActiveFibers[i];

                if (fiber.Enumerable == null)
                    fiber.Enumerable = fiber.Executable();
            
                if (fiber.Enumerable != null)
                {
                    if (fiber.Enumerator == null)
                        fiber.Enumerator = fiber.Enumerable.GetEnumerator();

                    if (!fiber.Enumerator.MoveNext())
                    {
                        fiber.Enumerable = null;
                        continue;
                    }

                    if (fiber.Enumerator.Current.GetType() == typeof(int))
                    {
                        if ((int)fiber.Enumerator.Current > 0)
                        {
                            // swap and pop
                            ActiveFibers[i] = ActiveFibers[ActiveFibers.Count - 1];
                            ActiveFibers.RemoveAt(ActiveFibers.Count - 1);
                            fiber.Sleeping = (int)fiber.Enumerator.Current;
                            SleepingFibers.Add(fiber);
                            i--;
                        }
                    }
                }
            }

            // NOTE: linq too slow, generates garbage
            //ActiveFibers.RemoveAll(fiber => fiber.Enumerable == null);
            for (int i = 0; i < ActiveFibers.Count; ++i)
            {
                if (ActiveFibers[i].Enumerable == null)
                {
                    // swap and pop
                    ActiveFibers[i] = ActiveFibers[ActiveFibers.Count - 1];
                    ActiveFibers.RemoveAt(ActiveFibers.Count - 1);
                    i--;
                }
            }
        }

        public void KillAll()
        {
            ActiveFibers.Clear();
            SleepingFibers.Clear();
        }

        public static IEnumerable WaitFrames(int frames)
        {
            while (frames-- > 0)
                yield return frames;
        }

        public static IEnumerable WaitSeconds(float seconds)
        {
            Console.WriteLine("WaitSeconds: {0}", seconds);
            int frames = Time.ToFrames(seconds);
            yield return WaitFrames(frames);
        }
    }
}
