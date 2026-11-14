//
// ProjectileCache.cs
//

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Galaxy
{
    public class ProjectileCacheManager
    {
        public static CProjectileCache<CBeam> Beams = new CProjectileCache<CBeam>();
        public static CProjectileCache<CBeamFocus> BeamFocuses = new CProjectileCache<CBeamFocus>();
        public static CProjectileCache<CBlade> Blades = new CProjectileCache<CBlade>();
        public static CProjectileCache<CBomblet> Bomblets = new CProjectileCache<CBomblet>();
        public static CProjectileCache<CBoomerang> Boomerangs = new CProjectileCache<CBoomerang>();
        public static CProjectileCache<CChargeShot> ChargeShots = new CProjectileCache<CChargeShot>();
        public static CProjectileCache<CDrunkMissile> DrunkMissiles = new CProjectileCache<CDrunkMissile>();
        public static CProjectileCache<CFlame> Flames = new CProjectileCache<CFlame>();
        public static CProjectileCache<CLaser> Lasers = new CProjectileCache<CLaser>();
        public static CProjectileCache<CLightning> Lightnings = new CProjectileCache<CLightning>();
        public static CProjectileCache<CMiniShot> MiniShots = new CProjectileCache<CMiniShot>();
        public static CProjectileCache<CMissile> Missiles = new CProjectileCache<CMissile>();
        public static CProjectileCache<CPlasma> Plasmas = new CProjectileCache<CPlasma>();
        public static CProjectileCache<CSeekBomb> SeekBombs = new CProjectileCache<CSeekBomb>();
        public static CProjectileCache<CVulcan> Vulcans = new CProjectileCache<CVulcan>();

        public static CProjectileCache<CEnemyCannonShot> EnemyCannonShots = new CProjectileCache<CEnemyCannonShot>();
        public static CProjectileCache<CEnemyLaser> EnemyLasers = new CProjectileCache<CEnemyLaser>();
        public static CProjectileCache<CEnemyMiniShot> EnemyMiniShots = new CProjectileCache<CEnemyMiniShot>();
        public static CProjectileCache<CEnemyMissile> EnemyMissiles = new CProjectileCache<CEnemyMissile>();
        public static CProjectileCache<CEnemyPellet> EnemyPellets = new CProjectileCache<CEnemyPellet>();
        public static CProjectileCache<CEnemyShot> EnemyShots = new CProjectileCache<CEnemyShot>();

        public static void ResetWorld(CWorld world)
        {
            Beams.ResetWorld(world); 
            BeamFocuses.ResetWorld(world); 
            Blades.ResetWorld(world);
            Bomblets.ResetWorld(world);
            Boomerangs.ResetWorld(world);
            ChargeShots.ResetWorld(world);
            DrunkMissiles.ResetWorld(world);
            Flames.ResetWorld(world);
            Lasers.ResetWorld(world);
            Lightnings.ResetWorld(world);
            MiniShots.ResetWorld(world);
            Missiles.ResetWorld(world);
            Plasmas.ResetWorld(world);
            SeekBombs.ResetWorld(world);
            Vulcans.ResetWorld(world);

            EnemyCannonShots.ResetWorld(world);
            EnemyLasers.ResetWorld(world);
            EnemyMiniShots.ResetWorld(world);
            EnemyMissiles.ResetWorld(world);
            EnemyPellets.ResetWorld(world);
            EnemyShots.ResetWorld(world);
        }
    };

    public interface ICacheableProjectile
    {
    }

    public class CProjectileCache<P>
        where P : ICacheableProjectile, new()
    {
        public List<List<P>> Instances { get; set; }
        private List<int> Counters { get; set; }

        public CProjectileCache()
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

        public void ResetWorld(CWorld world)
        {
            for (int i = 0; i < Instances.Count; ++i)
            {
                (Instances[0][i] as CEntity).World = world;
                (Instances[1][i] as CEntity).World = world;
            }
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
