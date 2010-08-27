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
            Collision200,
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
            AwardDefinitions[AwardType.Enemy0] = new SAward { Text = "Pacifist", Bonus = 500 };
            AwardDefinitions[AwardType.Enemy80] = new SAward { Text = "Killer", Bonus = 500 };
            AwardDefinitions[AwardType.Enemy100] = new SAward { Text = "Annihilator", Bonus = 500 };
            AwardDefinitions[AwardType.Building0] = new SAward { Text = "Precision", Bonus = 500 };
            AwardDefinitions[AwardType.Building80] = new SAward { Text = "Collateral Damage", Bonus = 500 };
            AwardDefinitions[AwardType.Building100] = new SAward { Text = "Destroyer", Bonus = 500 };
            AwardDefinitions[AwardType.Shot0] = new SAward { Text = "Evasive Action", Bonus = 500 };
            AwardDefinitions[AwardType.Shot1000] = new SAward { Text = "Bullet Magnet", Bonus = 500 };
            AwardDefinitions[AwardType.Collision0] = new SAward { Text = "Untouchable", Bonus = 500 };
            AwardDefinitions[AwardType.Collision200] = new SAward { Text = "Brute Force", Bonus = 500 };
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
                        CoinsTotal += enemy.Coins;
                        EnemyTotal += 1;
                    }

                    if (element is CStageElementBuilding)
                    {
                        CStageElementBuilding building = element as CStageElementBuilding;
                        CoinsTotal += building.Coins;
                        BuildingTotal += 1;
                    }
                }
            }
        }

        public string GetCoinsCollectedString()
        {
            float percent = CoinsTotal == 0 ? 0.0f : 100.0f / CoinsTotal * CoinsCollected;
            string result = String.Format("Coins Collected: {0}% {1}/{2}", (int)percent, CoinsCollected, CoinsTotal);
            return result;
        }

        public string GetEnemyKillsString()
        {
            float percent = EnemyTotal == 0 ? 0.0f : 100.0f / EnemyTotal * EnemyKills;
            string result = String.Format("Enemy Kills: {0}% {1}/{2}", (int)percent, EnemyKills, EnemyTotal);
            return result;
        }

        public string GetBuildingKillsString()
        {
            float percent = BuildingTotal == 0 ? 0.0f : 100.0f / BuildingTotal * BuildingKills;
            string result = String.Format("Building Kills: {0}% {1}/{2}", (int)percent, BuildingKills, BuildingTotal);
            return result;
        }

        public string GetShotDamageDealtString()
        {
            string result = String.Format("Shot Damage Dealt: {0}", (int)ShotDamageDealt);
            return result;
        }

        public string GetShotDamageReceivedString()
        {
            string result = String.Format("Shot Damage Received: {0}", (int)ShotDamageReceived);
            return result;
        }

        public string GetCollisionDamageDealtString()
        {
            string result = String.Format("Collision Damage Dealt: {0}", (int)CollisionDamageDealt);
            return result;
        }

        public string GetCollisionDamageReceivedString()
        {
            string result = String.Format("Collision Damage Received: {0}", (int)CollisionDamageReceived);
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
            if (CollisionDamageReceived > 1000)
                Awards.Add(AwardDefinitions[AwardType.Collision200]);

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
