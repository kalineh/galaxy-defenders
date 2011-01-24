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
            StageClear,
            BronzeMedal,
            SilverMedal,
            GoldMedal,
            PlatinumMedal,
        }

        public const int RequirementBronze = 150;
        public const int RequirementSilver = 200;
        public const int RequirementGold = 250;
        public const int RequirementPlatinum = 300;

        public struct SAward
        {
            public AwardType Type;
            public string Text;
            public int Bonus;
        }

        public static Dictionary<AwardType, SAward> AwardDefinitions;
        
        static CStats()
        {
            AwardDefinitions = new Dictionary<AwardType, SAward>();
            AwardDefinitions[AwardType.StageClear] = new SAward { Type = AwardType.StageClear, Text = "Stage Clear", Bonus = 5000 };
            AwardDefinitions[AwardType.BronzeMedal] = new SAward { Type = AwardType.BronzeMedal, Text = "Skill Bonus", Bonus = 100 };
            AwardDefinitions[AwardType.SilverMedal] = new SAward { Type = AwardType.SilverMedal, Text = "Skill Bonus", Bonus = 250 };
            AwardDefinitions[AwardType.GoldMedal] = new SAward { Type = AwardType.GoldMedal, Text = "Skill Bonus", Bonus = 750 };
            AwardDefinitions[AwardType.PlatinumMedal] = new SAward { Type = AwardType.PlatinumMedal, Text = "Skill Bonus", Bonus = 1500 };
        }

        public int CoinsTotal { get; set; }
        public int CoinsCollected { get; set; }
        public int EnemyTotal { get; set; }
        public int EnemyKills { get; set; }
        public int BuildingTotal { get; set; }
        public int BuildingKills { get; set; }

        // TODO: remove these if unused
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

        public int GetCoinsCollectedPercent()
        {
            float percent = CoinsTotal == 0 ? 0.0f : 100.0f / CoinsTotal * CoinsCollected;
            return (int)Math.Round(percent);
        }

        public int GetEnemyKillsPercent()
        {
            float percent = EnemyTotal == 0 ? 0.0f : 100.0f / EnemyTotal * EnemyKills;
            return (int)Math.Round(percent);
        }

        public int GetBuildingKillsPercent()
        {
            float percent = BuildingTotal == 0 ? 0.0f : 100.0f / BuildingTotal * BuildingKills;
            return (int)Math.Round(percent);
        }

        public int GetTotalPercent()
        {
            int coins = GetCoinsCollectedPercent();
            int enemy = GetEnemyKillsPercent();
            int building = GetBuildingKillsPercent();
            return coins + enemy + building;
        }

        public string GetTotalPercentString()
        {
            int total = GetTotalPercent();
            string result = String.Format("TOTAL: {0}%", total);
            return result;
        }

        public void CheckAwards()
        {
            Awards = new List<SAward>();

            Awards.Add(AwardDefinitions[AwardType.StageClear]);

            int total = GetTotalPercent();

            if (total >= RequirementPlatinum)
            {
                Awards.Add(AwardDefinitions[AwardType.PlatinumMedal]);    
            }
            else if (total >= RequirementGold)
            {
                Awards.Add(AwardDefinitions[AwardType.GoldMedal]);    
            }
            else if (total >= RequirementSilver)
            {
                Awards.Add(AwardDefinitions[AwardType.SilverMedal]);    
            }
            else if (total >= RequirementBronze)
            {
                Awards.Add(AwardDefinitions[AwardType.BronzeMedal]);    
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

        public int GetMedalTypeSaveIndex()
        {
            int total = GetTotalPercent();

            if (total >= RequirementPlatinum)
                return 5;
            else if (total >= RequirementGold)
                return 4;
            else if (total >= RequirementSilver)
                return 3;
            else if (total >= RequirementBronze)
                return 2;

            return 1;
        }
    }
}
