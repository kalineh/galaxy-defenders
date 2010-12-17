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
        public Dictionary<GameControllerIndex, List<P>> Instances { get; set; }
        private Dictionary<GameControllerIndex, int> Counters { get; set; }

        public CProjectileCache(CWorld world)
        {
            ResetAll(256);
        }

        public void ResetAll(int count)
        {
            Instances = new Dictionary<GameControllerIndex, List<P>>();
            Counters = new Dictionary<GameControllerIndex, int>();

            List<P> one = new List<P>(count);
            List<P> two = new List<P>(count);

            for (int i = 0; i < count; ++i)
            {
                one.Add(new P());
                two.Add(new P());
            }

            Instances.Add(GameControllerIndex.One, one);
            Instances.Add(GameControllerIndex.Two, two);

            Counters.Add(GameControllerIndex.One, 0);
            Counters.Add(GameControllerIndex.Two, 0);
        }

        public P GetProjectileInstance(GameControllerIndex game_controller_index)
        {
            int counter = Counters[game_controller_index];
            counter += 1;
            counter %= Instances[game_controller_index].Count;
            Counters[game_controller_index] = counter;
            P instance = Instances[game_controller_index][counter];
            return instance;
        }
    }
}
