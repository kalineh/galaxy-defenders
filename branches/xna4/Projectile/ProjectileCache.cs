//
// ProjectileCache.cs
//

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Galaxy
{
    public interface ICacheableProjectile
    {
    }

    public class CProjectileCache<P>
        where P : ICacheableProjectile, new()
    {
        public List<List<P>> Instances { get; set; }
        private List<int> Counters { get; set; }

        public CProjectileCache(CWorld world)
        {
            ResetAll(256);
        }

        public void ResetAll(int count)
        {
            Instances = new List<List<P>>();
            Counters = new List<int>();

            List<P> one = new List<P>(count);
            List<P> two = new List<P>(count);

            for (int i = 0; i < count; ++i)
            {
                one.Add(new P());
                two.Add(new P());
            }

            Instances.Add(one);
            Instances.Add(two);
            Counters.Add(0);
            Counters.Add(0);
        }

        public P GetProjectileInstance(GameControllerIndex game_controller_index)
        {
            int counter = Counters[(int)game_controller_index];
            counter += 1;
            counter %= Instances[(int)game_controller_index].Count;
            Counters[(int)game_controller_index] = counter;
            P instance = Instances[(int)game_controller_index][counter];
            return instance;
        }
    }
}
