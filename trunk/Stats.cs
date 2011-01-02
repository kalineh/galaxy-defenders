//
// Stats.cs
//

using System;
using System.Collections.Generic;

namespace Galaxy
{
    public class CStats
    {
        public enum AwardType
        {
            Enemy0,
            Enemy80,
            Enemy100,
            Building0,
            Building80,
            Building100,
            Shot0,
            Shot1000,
            Collision0,
            Collision50,
            Coins100,
        }

        public struct SAward
        {
            public string Text;
            public int Bonus;
        }

        public static Dictionary<AwardType, SAward> AwardDefinitions;
        
        static CStats()
        {
            AwardDefinitions = new Dictionary<AwardType, SAward>();
            AwardDefinitions[AwardType.Enemy0] = new SAward { Text = "Pacifist", Bonus = 5000 };
            AwardDefinitions[AwardType.Enemy80] = new SAward { Text = "Killer", Bonus = 500 };
            AwardDefinitions[AwardType.Enemy100] = new SAward { Text = "Annihilator", Bonus = 1500 };
            AwardDefinitions[AwardType.Building0] = new SAward { Text = "Safety First", Bonus = 500 };
            AwardDefinitions[AwardType.Building80] = new SAward { Text = "Collateral Damage", Bonus = 500 };
            AwardDefinitions[AwardType.Building100] = new SAward { Text = "Demolitions Expert", Bonus = 1500 };
            AwardDefinitions[AwardType.Shot0] = new SAward { Text = "Evasive Action", Bonus = 1000 };
            AwardDefinitions[AwardType.Shot1000] = new SAward { Text = "Bullet Magnet", Bonus = 500 };
            AwardDefinitions[AwardType.Collision0] = new SAward { Text = "Untouchable", Bonus = 500 };
            AwardDefinitions[AwardType.Collision50] = new SAward { Text = "Brute Force", Bonus = 1000 };
            AwardDefinitions[AwardType.Coins100] = new SAward { Text = "Scrooge", Bonus = 500 };
        }

        public int CoinsTotal { get; set; }
        public int CoinsCollected { get; set; }
        public int EnemyTotal { get; set; }
        public int EnemyKills { get; set; }
        public int BuildingTotal { get; set; }
        public int BuildingKills { get; set; }
        public float ShotDamageDealt { get; set; }
        public float ShotDamageReceived { get; set; }
        public float CollisionDamageDealt { get; set; }
        public float CollisionDamageReceived { get; set; }
        public List<SAward> Awards { get; set; }

        public void Initialize(CWorld world)
        {
            Dictionary<int, List<CStageElement>> elements = world.Stage.Definition.Elements;
            foreach (KeyValuePair<int, List<CStageElement>> items in elements)
            {
                foreach (CStageElement element in items.Value)
                {
                    if (element is CStageElementSpawnerEnemy)
                    {
                        CStageElementSpawnerEnemy enemy = element as CStageElementSpawnerEnemy;

                        if (enemy.Type == typeof(CFence))
                            continue;

                        EnemyTotal += 1;
                    }

                    if (element is CStageElementBuilding)
                    {
                        CStageElementBuilding building = element as CStageElementBuilding;
                        BuildingTotal += 1;
                    }
                }
            }
        }

        public string GetCoinsCollectedString()
        {
            float percent = CoinsTotal == 0 ? 0.0f : 100.0f / CoinsTotal * CoinsCollected;
            string result = String.Format("COINS: {0}%", (int)percent);
            return result;
        }

        public string GetEnemyKillsString()
        {
            float percent = EnemyTotal == 0 ? 0.0f : 100.0f / EnemyTotal * EnemyKills;
            string result = String.Format("ENEMIES: {0}%", (int)percent);
            return result;
        }

        public string GetBuildingKillsString()
        {
            float percent = BuildingTotal == 0 ? 0.0f : 100.0f / BuildingTotal * BuildingKills;
            string result = String.Format("BUILDING: {0}%", (int)percent);
            return result;
        }

        public string GetTotalPercentString()
        {
            float coins = CoinsTotal == 0 ? 0.0f : 100.0f / CoinsTotal * CoinsCollected;
            float enemy = EnemyTotal == 0 ? 0.0f : 100.0f / EnemyTotal * EnemyKills;
            float building = BuildingTotal == 0 ? 0.0f : 100.0f / BuildingTotal * BuildingKills;
            string result = String.Format("TOTAL: {0}%", (int)(coins + enemy + building));
            return result;
        }

        public void CheckAwards()
        {
            Awards = new List<SAward>();

            if (EnemyTotal > 0)
            {
                float percent = EnemyTotal == 0 ? 0.0f : 100.0f / EnemyTotal * EnemyKills;

                if (EnemyTotal > 0 && EnemyKills == 0)
                    Awards.Add(AwardDefinitions[AwardType.Enemy0]);
                if (percent > 80.0f)
                    Awards.Add(AwardDefinitions[AwardType.Enemy80]);
                if (EnemyKills == EnemyTotal)
                    Awards.Add(AwardDefinitions[AwardType.Enemy100]);
            }

            if (BuildingTotal > 0)
            {
                float percent = BuildingTotal == 0 ? 0.0f : 100.0f / BuildingTotal * BuildingKills;

                if (BuildingTotal > 0 && BuildingKills == 0)
                    Awards.Add(AwardDefinitions[AwardType.Building0]);
                if (percent > 80.0f)
                    Awards.Add(AwardDefinitions[AwardType.Building80]);
                if (BuildingKills == BuildingTotal)
                    Awards.Add(AwardDefinitions[AwardType.Building100]);
            }

            if (ShotDamageReceived == 0)
                Awards.Add(AwardDefinitions[AwardType.Shot0]);
            if (ShotDamageReceived > 1000)
                Awards.Add(AwardDefinitions[AwardType.Shot1000]);

            if (CollisionDamageReceived == 0)
                Awards.Add(AwardDefinitions[AwardType.Collision0]);
            if (CollisionDamageReceived > 50)
                Awards.Add(AwardDefinitions[AwardType.Collision50]);

            if (CoinsTotal > 0)
            {
                if (CoinsCollected == CoinsTotal)
                    Awards.Add(AwardDefinitions[AwardType.Coins100]);
            }
        }

        public int GetAwardCount()
        {
            return Awards.Count;
        }

        public SAward GetAward(int index)
        {
            SAward award = Awards[index];
            return award;
        }
    }
}
