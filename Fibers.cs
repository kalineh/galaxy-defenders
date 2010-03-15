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
        public bool Active { get; set; }
        public IEnumerable Enumerable { get; set; }
        public IEnumerator Enumerator { get; set; }

        public CFiber(CFiberManager manager, DExecutable executable)
        {
            Manager = manager;
            Executable = executable;
            Active = true;
        }
    }

    public class CFiberManager
    {
        public List<CFiber> Fibers { get; set; }

        public CFiberManager()
        {
            Fibers = new List<CFiber>();
        }

        public CFiber Fork(CFiber.DExecutable executable)
        {
            CFiber result = new CFiber(this, executable);
            Fibers.Add(result);
            return result;
        }

        public void Update()
        {
            foreach (CFiber fiber in Fibers)
            {
                if (fiber.Enumerable == null)
                {
                    fiber.Enumerable = fiber.Executable();
                    fiber.Enumerator = fiber.Enumerable.GetEnumerator();
                }
                bool has_elements = fiber.Enumerator.MoveNext();
                fiber.Active = has_elements;
            }

            Fibers.RemoveAll(fiber => fiber.Active == false);
        }
    }
}
