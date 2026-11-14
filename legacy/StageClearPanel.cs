//
// StageClearPanel.cs
//

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;

namespace Galaxy
{
    public class CStageClearPanel
    {
        public CWorld World { get; set; }
        public int Counter { get; set; }
        public CTextLabel LabelStageClear { get; set; }
        public CTextLabel LabelCoins { get; set; }
        public CTextLabel LabelCoinsValue { get; set; }
        public CTextLabel LabelEnemies { get; set; }
        public CTextLabel LabelEnemiesValue { get; set; }
        public CTextLabel LabelBuildings { get; set; }
        public CTextLabel LabelBuildingsValue { get; set; }
        public CTextLabel LabelTotal { get; set; }
        public CTextLabel LabelTotalValue { get; set; }
        public CTextLabel LabelArmorRepair { get; set; }
        public CTextLabel LabelArmorRepairValue { get; set; }
        public List<CTextLabel> LabelAwards { get; set; }
        public List<CTextLabel> LabelAwardValues { get; set; }
        public CVisual MedalClear { get; set; }
        public CVisual MedalBronze { get; set; }
        public CVisual MedalSilver { get; set; }
        public CVisual MedalGold { get; set; }
        public CVisual MedalPlatinum { get; set; }
        public float MedalScale { get; set; }
        public CTextLabel ContinueLabel { get; set; }
        public CVisual ContinueButton { get; set; }

        public const int StageClearShow = 60;
        public const int StatsShow = 100;
        public const int StatsInterval = 20;
        public const int AwardShow = 240;
        public const int AwardInterval = 20;
        public const int MedalShow = 300;

        public CStageClearPanel(CWorld world)
        {
            World = world;
            Counter = -1;
            LabelStageClear = new CTextLabel();
            LabelCoins = new CTextLabel();
            LabelCoinsValue = new CTextLabel();
            LabelEnemies = new CTextLabel();
            LabelEnemiesValue = new CTextLabel();
            LabelBuildings = new CTextLabel();
            LabelBuildingsValue = new CTextLabel();
            LabelTotal = new CTextLabel();
            LabelTotalValue = new CTextLabel();
            LabelArmorRepair = new CTextLabel();
            LabelArmorRepairValue = new CTextLabel();
            LabelAwards = new List<CTextLabel>();
            LabelAwardValues = new List<CTextLabel>();

            LabelStageClear.Value = "Stage Clear";
            LabelCoins.Value = "Coins";
            LabelEnemies.Value = "Enemies";
            LabelBuildings.Value = "Buildings";
            LabelTotal.Value = "Total";
            LabelArmorRepair.Value = "Repair Cost";

            LabelStageClear.Alignment = CTextLabel.EAlignment.Center;
            LabelCoins.Alignment = CTextLabel.EAlignment.Right;
            LabelEnemies.Alignment = CTextLabel.EAlignment.Right;
            LabelBuildings.Alignment = CTextLabel.EAlignment.Right;
            LabelTotal.Alignment = CTextLabel.EAlignment.Right;
            LabelArmorRepair.Alignment = CTextLabel.EAlignment.Right;

            LabelCoinsValue.Alignment = CTextLabel.EAlignment.Left;
            LabelEnemiesValue.Alignment = CTextLabel.EAlignment.Left;
            LabelBuildingsValue.Alignment = CTextLabel.EAlignment.Left;
            LabelTotalValue.Alignment = CTextLabel.EAlignment.Left;
            LabelArmorRepairValue.Alignment = CTextLabel.EAlignment.Left;

            MedalClear = CVisual.MakeSpriteCached1(World.Game, "Textures/UI/MedalClear");
            MedalBronze = CVisual.MakeSpriteCached1(World.Game, "Textures/UI/MedalBronze");
            MedalSilver = CVisual.MakeSpriteCached1(World.Game, "Textures/UI/MedalSilver");
            MedalGold = CVisual.MakeSpriteCached1(World.Game, "Textures/UI/MedalGold");
            MedalPlatinum = CVisual.MakeSpriteCached1(World.Game, "Textures/UI/MedalPlatinum");

            MedalClear.Alpha = 0.0f;
            MedalBronze.Alpha = 0.0f;
            MedalSilver.Alpha = 0.0f;
            MedalGold.Alpha = 0.0f;
            MedalPlatinum.Alpha = 0.0f;

            MedalScale = 2.0f;

            ContinueLabel = new CTextLabel() { Value = "Continue", Alignment = CTextLabel.EAlignment.Left };
            ContinueButton = CVisual.MakeSpriteCached1(World.Game, "Textures/UI/Xbox/xboxControllerButtonX");
        }

        public void Start()
        {
            Counter = 0;    
            World.Stats.CheckAwards();

            LabelCoinsValue.Value = String.Format("{0}%", World.Stats.GetCoinsCollectedPercent());
            LabelEnemiesValue.Value = String.Format("{0}%", World.Stats.GetEnemyKillsPercent());
            LabelBuildingsValue.Value = String.Format("{0}%", World.Stats.GetBuildingKillsPercent());
            LabelTotalValue.Value = String.Format("{0}%", World.Stats.GetTotalPercent());

            foreach (CStats.SAward award in World.Stats.Awards)
            {
                if (award.Type == CStats.AwardType.BronzeMedal)
                    MedalBronze.Alpha = 1.0f; 
                if (award.Type == CStats.AwardType.SilverMedal)
                    MedalSilver.Alpha = 1.0f; 
                if (award.Type == CStats.AwardType.GoldMedal)
                    MedalGold.Alpha = 1.0f; 
                if (award.Type == CStats.AwardType.PlatinumMedal)
                    MedalPlatinum.Alpha = 1.0f; 
            }

            if (MedalBronze.Alpha == 0.0f &&
                MedalSilver.Alpha == 0.0f &&
                MedalGold.Alpha == 0.0f &&
                MedalPlatinum.Alpha == 0.0f)
            {
                MedalClear.Alpha = 1.0f;
            }
        }

        public void Update()
        {
            if (Counter == -1)
                return;

            Counter += 1;

            for (int i = 0; i < World.Stats.GetAwardCount(); ++i)
            {
                if (Counter == AwardShow + AwardInterval * i)
                {
                    CStats.SAward award = World.Stats.GetAward(i);

                    CTextLabel label = new CTextLabel();
                    CTextLabel value = new CTextLabel();

                    label.Value = String.Format("{0}", award.Text);
                    value.Value = String.Format("+{0}{1}", award.Bonus, '￥');

                    label.Alignment = CTextLabel.EAlignment.Right;
                    value.Alignment = CTextLabel.EAlignment.Left;

                    LabelAwards.Add(label);
                    LabelAwardValues.Add(value);

                    // NOTE: we want to add this with the correct timing so the score text animates correctly
                    float multiplier = World.Ships.Count > 1 ? 0.75f : 1.0f;
                    foreach (CShip ship in World.Ships)
                    {
                        ship.Score += (int)(award.Bonus * multiplier);
                    }
                }
            }

            if (Counter == StatsShow + StatsInterval * 5)
            {
                // NOTE: disable repair for design purpose
                /*
                int total_repair = 0;
                foreach (CShip ship in World.Ships)
                {
                    float damage = ship.Chassis.Armor - ship.CurrentArmor;
                    int repair_cost = (int)(50.0f * damage);
                    ship.Score -= repair_cost;
                    total_repair += repair_cost;
                }
                LabelArmorRepairValue.Value = String.Format("-{0}￥", total_repair);
                 * */
            }
        }

        public void Draw(SpriteBatch sprite_batch)
        {
            if (Counter == -1)
                return;

            //Vector2 text_position = Vector2.Zero;
            //string text = "";
            //sprite_batch.DrawString(Game.GameRegularFont, text, text_position, Color.White);

            Vector2 base_ = new Vector2(1920.0f / 2.0f + 8.0f, 140.0f);

            if (Counter >= StageClearShow)
            {
                Vector2 panel_position = new Vector2(World.Game.Resolution.X / 2.0f - 75.0f, 150.0f);
                sprite_batch.Draw(World.Game.PixelTexture, new Rectangle((int)panel_position.X - 140, (int)panel_position.Y - 35, 450, 570), null, new Color(76, 76, 76), 0.0f, Vector2.Zero, SpriteEffects.None, 0.0f);
                sprite_batch.Draw(World.Game.PixelTexture, new Rectangle((int)panel_position.X - 130, (int)panel_position.Y - 25, 430, 550), null, new Color(30, 30, 30), 0.0f, Vector2.Zero, SpriteEffects.None, 0.0f);

                LabelStageClear.Draw(sprite_batch, World.Game.GameLargeFont, base_ + new Vector2(+0.0f, 40.0f), Color.White);
            }

            float percent_base = 140.0f;
            float spacing = 30.0f;

            if (Counter >= StatsShow + StatsInterval * 0)
            {
                LabelCoins.Draw(sprite_batch, World.Game.GameRegularFont, base_ + new Vector2(-8.0f, percent_base + spacing * 0.0f), Color.White);
                LabelCoinsValue.Draw(sprite_batch, World.Game.GameRegularFont, base_ + new Vector2(+8.0f, percent_base + spacing * 0.0f), Color.White);
            }
               
            if (Counter >= StatsShow + StatsInterval * 1)
            {
                LabelEnemies.Draw(sprite_batch, World.Game.GameRegularFont, base_ + new Vector2(-8.0f, percent_base + spacing * 1.0f), Color.White);
                LabelEnemiesValue.Draw(sprite_batch, World.Game.GameRegularFont, base_ + new Vector2(+8.0f, percent_base + spacing * 1.0f), Color.White);
            }
            if (Counter >= StatsShow + StatsInterval * 2)
            {
                LabelBuildings.Draw(sprite_batch, World.Game.GameRegularFont, base_ + new Vector2(-8.0f, percent_base + spacing * 2.0f), Color.White);
                LabelBuildingsValue.Draw(sprite_batch, World.Game.GameRegularFont, base_ + new Vector2(+8.0f, percent_base + spacing * 2.0f), Color.White);
            }

            if (Counter >= StatsShow + StatsInterval * 4)
            {
                LabelTotal.Draw(sprite_batch, World.Game.GameRegularFont, base_ + new Vector2(-8.0f, percent_base + spacing * 3.0f), Color.White);
                LabelTotalValue.Draw(sprite_batch, World.Game.GameRegularFont, base_ + new Vector2(+8.0f, percent_base + spacing * 3.0f), Color.White);
            }

            // NOTE: disable repair for design purpose
            //if (Counter >= StatsShow + StatsInterval * 5)
            //{
                //LabelArmorRepair.Draw(sprite_batch, World.Game.GameRegularFont, base_ + new Vector2(-8.0f, percent_base + spacing * 4.5f), Color.White);
                //LabelArmorRepairValue.Draw(sprite_batch, World.Game.GameRegularFont, base_ + new Vector2(+8.0f, percent_base + spacing * 4.5f), Color.LightPink);
            //}

            for (int i = 0; i < LabelAwards.Count; ++i)
            {
                CTextLabel label = LabelAwards[i];
                CTextLabel value = LabelAwardValues[i];

                float offset = 30.0f * i;

                label.Draw(sprite_batch, World.Game.GameRegularFont, base_ + new Vector2(-8.0f, 320.0f + offset), Color.White);
                value.Draw(sprite_batch, World.Game.GameRegularFont, base_ + new Vector2(+8.0f, 320.0f + offset), Color.LightGreen);
            }

            if (Counter >= MedalShow)
            {
                Vector2 medal_position = base_ + new Vector2(0.0f, 400.0f);
                MedalScale = Math.Max(1.0f, MedalScale - 0.125f);
                MedalClear.Draw(sprite_batch, medal_position, 0.0f, MedalScale);
                MedalBronze.Draw(sprite_batch, medal_position, 0.0f, MedalScale);
                MedalSilver.Draw(sprite_batch, medal_position, 0.0f, MedalScale);
                MedalGold.Draw(sprite_batch, medal_position, 0.0f, MedalScale);
                MedalPlatinum.Draw(sprite_batch, medal_position, 0.0f, MedalScale);
            }

            if (Counter >= MedalShow + 60)
            {
                float scale = 1.0f + (float)Math.Sin(World.Game.GameFrame * 0.1f) * 0.01f;
                ContinueLabel.Draw(sprite_batch, World.Game.GameRegularFont, base_ + new Vector2(-60.0f, 490.0f), Color.LightGray, scale);
                ContinueButton.Draw(sprite_batch, base_ + new Vector2(-80.0f, 490.0f), 0.0f);
            }
        }
    }
}
