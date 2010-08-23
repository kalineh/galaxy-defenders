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
        public Dictionary<PlayerIndex, List<P>> Instances { get; set; }
        private Dictionary<PlayerIndex, int> Counters { get; set; }

        public CProjectileCache(CWorld world)
        {
            ResetAll(256);
        }

        public void ResetAll(int count)
        {
            Instances = new Dictionary<PlayerIndex, List<P>>();
            Counters = new Dictionary<PlayerIndex, int>();

            List<P> one = new List<P>(count);
            List<P> two = new List<P>(count);

            for (int i = 0; i < count; ++i)
            {
                one.Add(new P());
                two.Add(new P());
            }

            Instances.Add(PlayerIndex.One, one);
            Instances.Add(PlayerIndex.Two, two);

            Counters.Add(PlayerIndex.One, 0);
            Counters.Add(PlayerIndex.Two, 0);
        }

        public P GetProjectileInstance(PlayerIndex player_index)
        {
            int counter = Counters[player_index];
            counter += 1;
            counter %= Instances[player_index].Count;
            Counters[player_index] = counter;
            P instance = Instances[player_index][counter];
            return instance;
        }
    }
}
