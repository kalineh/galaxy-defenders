﻿//
// StateShop.cs
//

using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Galaxy
{
    public class CStateShop
        : CState
    {
        public CGalaxy Game { get; set; }
        private CWorld EmptyWorld { get; set; }
        public CMenu Menu { get; set; }
        private CMenu MenuBase { get; set; }
        private CMenu MenuUpgradeShip { get; set; }
        private CMenu MenuPrimaryWeapon { get; set; }
        private CMenu MenuSecondaryWeapon { get; set; }
        private CMenu MenuSidekick { get; set; }
        private CMenu MenuChassis { get; set; }
        private CMenu MenuGenerator { get; set; }
        private CMenu MenuShield { get; set; }
        private CMenu MenuTrainPilot { get; set; }
        private CMenu MenuScoreboard { get; set; }
        private delegate void DrawMenuErrataFunction();
        private DrawMenuErrataFunction DrawMenuErrata { get; set; }
        private CShip SampleShip { get; set; }
        private SProfilePilotState WorkingProfile;
        private SProfilePilotState LockedProfile;
        private Texture2D ShopPanelTexture { get; set; }
        private Texture2D ShopUpgradePanelTexture { get; set; }
        private CVisual ShopUpgradeBarsVisual { get; set; }
        private GameControllerIndex ShoppingPlayer { get; set; }
        private int GenerateEnemyDelay { get; set; }
        private int SampleShotDelay { get; set; }
        private Texture2D ShopPurchasePanelTexture { get; set; }
        private CTextLabel ShopPurchaseTextLabel { get; set; }
        private CScorePanel ScorePanel { get; set; }
        private CVisual MoneyPoolEmpty { get; set; }
        private CVisual MoneyPoolFull { get; set; }
        private CVisual MoneyPoolBuy { get; set; }
        private CVisual MoneyPoolSell { get; set; }
        private CVisual MoneyPoolBar { get; set; }
        private CTextLabel MoneyPoolLabel { get; set; }

        private struct SLabels
        {
            public CTextLabel BaseCostHeader;
            public CTextLabel BaseCostPrice;
            public CTextLabel NextUpgradeHeader;
            public CTextLabel NextUpgradePrice;
            public CTextLabel ArmorHeader;
            public CTextLabel ArmorValue;
            public CTextLabel SpeedHeader;
            public CTextLabel SpeedValue;
            public CTextLabel DensityHeader;
            public CTextLabel DensityValue;
            public CTextLabel ShieldHeader;
            public CTextLabel ShieldValue;
            public CTextLabel RegenHeader;
            public CTextLabel RegenValue;
            public CTextLabel EfficiencyHeader;
            public CTextLabel EfficiencyValue;
            public CTextLabel EnergyHeader;
            public CTextLabel EnergyValue;
            public CTextLabel DescHeader;
            public CTextLabel DescValue;

            public SLabels(object unused)
            {
                BaseCostHeader = new CTextLabel();
                BaseCostPrice = new CTextLabel();
                NextUpgradeHeader = new CTextLabel();
                NextUpgradePrice = new CTextLabel();
                ArmorHeader = new CTextLabel();
                ArmorValue = new CTextLabel();
                SpeedHeader = new CTextLabel();
                SpeedValue = new CTextLabel();
                DensityHeader = new CTextLabel();
                DensityValue = new CTextLabel();
                ShieldHeader = new CTextLabel();
                ShieldValue = new CTextLabel();
                RegenHeader = new CTextLabel();
                RegenValue = new CTextLabel();
                EfficiencyHeader = new CTextLabel();
                EfficiencyValue = new CTextLabel();
                EnergyHeader = new CTextLabel();
                EnergyValue = new CTextLabel();
                DescHeader = new CTextLabel();
                DescValue = new CTextLabel();

                BaseCostHeader.Value = "BASE COST";
                NextUpgradeHeader.Value = "NEXT UPGRADE COST";
                ArmorHeader.Value = "ARMOR";
                SpeedHeader.Value = "SPEED";
                DensityHeader.Value = "DENSITY";
                ShieldHeader.Value = "SHIELD";
                RegenHeader.Value = "REGEN";
                EfficiencyHeader.Value = "EFFICIENCY %";
                EnergyHeader.Value = "ENERGY";
                DescHeader.Value = "DESCRIPTION";
            }
        };
        private SLabels Labels { get; set; }

        public CStateShop(CGalaxy game)
        {
            Game = game;
            EmptyWorld = new CWorld(game, null);

            EmptyWorld.BackgroundScenery = new CSceneryChain(EmptyWorld,
                new CBackground(EmptyWorld, new Color(0, 0, 0)),
                new CStars(EmptyWorld, CContent.LoadTexture2D(Game, "Textures/Background/Star"), 0.4f, 3.5f),
                new CStars(EmptyWorld, CContent.LoadTexture2D(Game, "Textures/Background/Star"), 0.6f, 2.1f)
            );
            EmptyWorld.ForegroundScenery = CSceneryPresets.Empty(EmptyWorld);

            WorkingProfile = GetShoppingPilotData();
            LockedProfile = WorkingProfile;

            ShopPanelTexture = CContent.LoadTexture2D(game, "Textures/UI/Shop/ShopPanel");
            ShopUpgradePanelTexture = CContent.LoadTexture2D(game, "Textures/UI/Shop/ShopUpgradePanel");
            ShopUpgradeBarsVisual = CVisual.MakeSpriteFromGame(game, "Textures/UI/Shop/ShopUpgradeBars", Vector2.One, Color.White);
            ShopUpgradeBarsVisual.TileY = 8;
            ShopPurchasePanelTexture = CContent.LoadTexture2D(game, "Textures/UI/Shop/ShopPurchaseButtonPanel");
            ShopPurchaseTextLabel = new CTextLabel() { Value = "Purchase" };
            MoneyPoolEmpty = CVisual.MakeSpriteUncached(game, "Textures/UI/MoneyPoolEmpty");
            MoneyPoolFull = CVisual.MakeSpriteUncached(game, "Textures/UI/MoneyPoolFull");
            MoneyPoolBuy = CVisual.MakeSpriteUncached(game, "Textures/UI/MoneyPoolBuy");
            MoneyPoolSell = CVisual.MakeSpriteUncached(game, "Textures/UI/MoneyPoolSell");
            MoneyPoolBar = CVisual.MakeSpriteUncached(game, "Textures/UI/MoneyPoolBar");

            MoneyPoolEmpty.NormalizedOrigin = new Vector2(0.0f, 0.5f);
            MoneyPoolFull.NormalizedOrigin = new Vector2(0.0f, 0.5f);
            MoneyPoolBuy.NormalizedOrigin = new Vector2(0.0f, 0.5f);
            MoneyPoolSell.NormalizedOrigin = new Vector2(0.0f, 0.5f);

            MoneyPoolLabel = new CTextLabel() { Alignment = CTextLabel.EAlignment.Center };

            Labels = new SLabels(null);

            //
            // Base
            //
            MenuBase = new CMenu(game)
            {
                Position = GetShoppingPlayerMenuPosition(),
                MenuOptions = new List<CMenu.CMenuOption>()
                {
                    new CMenu.CMenuOption() { Text = "Enter Shop", Select = UpgradeShip },
                    new CMenu.CMenuOption() { Text = "Play Next Stage", Select = StageSelect },
                    new CMenu.CMenuOption() { Text = "View Scoreboard", Select = ShowScoreboard },
                    new CMenu.CMenuOption() { Text = "Back", Select = Back, CancelOption = true, PanelType = CMenu.PanelType.Small },
                }
            };

            //
            // Upgrade Ship
            //
            MenuUpgradeShip = new CMenu(game)
            {
                Position = GetShoppingPlayerMenuPosition(),
                MenuOptions = new List<CMenu.CMenuOption>()
                {
                    new CMenu.CMenuOption() { Text = "Chassis", Select = EditChassis },
                    new CMenu.CMenuOption() { Text = "Generator", Select = EditGenerator },
                    new CMenu.CMenuOption() { Text = "Shield", Select = EditShield },
                    new CMenu.CMenuOption() { Text = "Main Weapon", Select = EditPrimaryWeapon },
                    new CMenu.CMenuOption() { Text = "Support Weapon", Select = EditSecondaryWeapon },
                    new CMenu.CMenuOption() { Text = "Sidekick", Select = EditSidekick },
                    // NOTE: disabling to move to clear-game unlocks
                    //new CMenu.CMenuOption() { Text = "Pilot Training", Select = TrainPilot },
#if DEBUG
                    new CMenu.CMenuOption() { Text = "* Money (L/R)", Axis = EditMoney, AxisValidate = (tag, axis) => { return true; } },
#endif
                    new CMenu.CMenuOption() { Text = "Back", Select = ReturnToBaseMenu, CancelOption = true, PanelType = CMenu.PanelType.Small },
                }
            };

            //
            // Training
            //
            MenuTrainPilot = new CMenu(game)
            {
                Position = GetShoppingPlayerMenuPosition(),
                MenuOptions = new List<CMenu.CMenuOption>(),
            };

            string pilot = GetShoppingPilotData().Pilot;
            for (int i = 0; i < 3; ++i)
            {
                MenuTrainPilot.MenuOptions.Add(new CMenu.CMenuOption() { Text = CAbility.GetAbilityName(pilot, i), Select = TrainAbility, Highlight = HighlightAbility, SelectValidate = ValidateAbilityWithLockedProfile, Data = i, });
            }
            MenuTrainPilot.MenuOptions.Add(new CMenu.CMenuOption()
                                           {
                                               Text = "Back",
                                               Select = ReturnToUpgradeShip,
                                               Highlight = CancelHighlightAbility,
                                               CancelOption = true,
                                               PanelType = CMenu.PanelType.Small
                                           });

            //
            // Primary Weapon
            //
            MenuPrimaryWeapon = new CMenu(game)
            {
                Position = GetShoppingPlayerMenuPosition(),
                MenuOptions = new List<CMenu.CMenuOption>(),
            };

            IEnumerable<string> primary_weapon_parts_own = new List<string>() { GetShoppingPilotData().WeaponPrimaryType };
            IEnumerable<string> primary_weapon_parts_all = primary_weapon_parts_own.Concat(CMap.GetMapNodeByStageName(CSaveData.GetCurrentGameData(Game).Stage).AvailablePrimaryWeaponParts);
            IEnumerable<string> primary_weapon_parts = primary_weapon_parts_all.Distinct().OrderBy(W => CWeaponFactory.GetPriceForLevel(W, 0));
            foreach (string weapon_part in primary_weapon_parts)
            {
                MenuPrimaryWeapon.MenuOptions.Add(
                    new CMenu.CMenuOption()
                    {
                        Text = weapon_part,
                        SubText = "Cost: " + CWeaponFactory.GetPriceForLevel(weapon_part, 0),
                        IconName = "Shop/DefaultIcon",
                        Data = weapon_part,
                        Select = SelectPrimaryWeapon,
                        SelectValidate = SelectValidatePrimaryWeapon,
                        Highlight = HighlightPrimaryWeapon,
                        Axis = AxisPrimaryWeapon,
                        AxisValidate = AxisValidatePrimaryWeapon,
                        AxisValue = weapon_part == WorkingProfile.WeaponPrimaryType ? WorkingProfile.WeaponPrimaryLevel : 0,
                    }
                );
            }
            MenuPrimaryWeapon.MenuOptions.Add(new CMenu.CMenuOption()
                                              {
                                                  Text = "Done",
                                                  Select = ReturnToUpgradeShip,
                                                  Highlight = RevertWorkingProfile,
                                                  CancelOption = true,
                                                  PanelType = CMenu.PanelType.Small
                                              });

            //
            // Secondary Weapon
            //
            MenuSecondaryWeapon = new CMenu(game)
            {
                Position = GetShoppingPlayerMenuPosition(),
                MenuOptions = new List<CMenu.CMenuOption>(),
            };

            MenuSecondaryWeapon.MenuOptions.Add(new CMenu.CMenuOption() { Text = "None", SubText = "Cost: 0", Select = SelectSecondaryWeaponEmpty, Highlight = HighlightSecondaryWeapon, Data = "" });
            IEnumerable<string> secondary_weapon_parts_own = new List<string>() { GetShoppingPilotData().WeaponSecondaryType };
            IEnumerable<string> secondary_weapon_parts_all = secondary_weapon_parts_own.Concat(CMap.GetMapNodeByStageName(CSaveData.GetCurrentGameData(Game).Stage).AvailableSecondaryWeaponParts);
            IEnumerable<string> secondary_weapon_parts = secondary_weapon_parts_all.Distinct().OrderBy(W => CWeaponFactory.GetPriceForLevel(W, 0));
            foreach (string weapon_part in secondary_weapon_parts)
            {
                if (weapon_part == "")
                    continue;

                MenuSecondaryWeapon.MenuOptions.Add(
                    new CMenu.CMenuOption()
                    {
                        Text = weapon_part,
                        SubText = "Cost: " + CWeaponFactory.GetPriceForLevel(weapon_part, 0),
                        Data = weapon_part,
                        Select = SelectSecondaryWeapon,
                        SelectValidate = SelectValidateSecondaryWeapon,
                        Highlight = HighlightSecondaryWeapon,
                        Axis = AxisSecondaryWeapon,
                        AxisValidate = AxisValidateSecondaryWeapon,
                        AxisValue = weapon_part == WorkingProfile.WeaponSecondaryType ? WorkingProfile.WeaponSecondaryLevel : 0,
                    }
                );

                MenuSecondaryWeapon.MenuOptions[0].SpecialHighlight = true;
            }
            MenuSecondaryWeapon.MenuOptions.Add(new CMenu.CMenuOption()
                                                {
                                                    Text = "Done",
                                                    Select = ReturnToUpgradeShip,
                                                    Highlight = RevertWorkingProfile,
                                                    CancelOption = true,
                                                    PanelType = CMenu.PanelType.Small
                                                });

            //
            // Sidekick
            //
            MenuSidekick = new CMenu(game)
            {
                Position = GetShoppingPlayerMenuPosition(),
                MenuOptions = new List<CMenu.CMenuOption>(),
            };

            MenuSidekick.MenuOptions.Add(new CMenu.CMenuOption() { Text = "None", SubText = "Cost: 0", Select = SelectSidekickEmpty, Highlight = HighlightSidekick, Data = "" });
            IEnumerable<string> sidekick_left_weapon_parts_own = new List<string>() { GetShoppingPilotData().WeaponSidekickType };
            IEnumerable<string> sidekick_left_weapon_parts_all = sidekick_left_weapon_parts_own.Concat(CMap.GetMapNodeByStageName(CSaveData.GetCurrentGameData(Game).Stage).AvailableSidekickWeaponParts);
            IEnumerable<string> sidekick_left_weapon_parts = sidekick_left_weapon_parts_all.Distinct().OrderBy(W => CWeaponFactory.GetPriceForLevel(W, 0));
            foreach (string weapon_part in sidekick_left_weapon_parts)
            {
                if (weapon_part == "")
                    continue;

                MenuSidekick.MenuOptions.Add(
                    new CMenu.CMenuOption()
                    {
                        Text = weapon_part,
                        SubText = "Cost: " + CWeaponFactory.GetPriceForLevel(weapon_part, 0),
                        Data = weapon_part,
                        Select = SelectSidekick,
                        SelectValidate = SelectValidateSidekick,
                        Highlight = HighlightSidekick,
                    }
                );
            }

            MenuSidekick.MenuOptions.Add(new CMenu.CMenuOption()
                                             {
                                                 Text = "Done",
                                                 Select = ReturnToUpgradeShip,
                                                 Highlight = RevertWorkingProfile,
                                                 CancelOption = true,
                                                 PanelType = CMenu.PanelType.Small
                                             });

            //
            // Chassis
            //
            MenuChassis = new CMenu(game)
            {
                Position = GetShoppingPlayerMenuPosition(),
                MenuOptions = new List<CMenu.CMenuOption>(),
            };

            IEnumerable<string> chassis_parts_own = new List<string>() { GetShoppingPilotData().ChassisType };
            IEnumerable<string> chassis_parts_all = chassis_parts_own.Concat(CMap.GetMapNodeByStageName(CSaveData.GetCurrentGameData(Game).Stage).AvailableChassisParts);
            IEnumerable<string> chassis_parts = chassis_parts_all.Distinct().OrderBy(C => ChassisDefinitions.GetPart(C).Price);
            foreach (string chassis_part in chassis_parts)
            {
                MenuChassis.MenuOptions.Add(
                    new CMenu.CMenuOption()
                    {
                        Text = chassis_part,
                        SubText = "Cost: " + ChassisDefinitions.GetPart(chassis_part).Price,
                        Data = chassis_part,
                        Select = SelectChassis,
                        SelectValidate = SelectValidateChassis,
                        Highlight = HighlightChassis,
                    }
                );
            }
            MenuChassis.MenuOptions.Add(new CMenu.CMenuOption() { Text = "Done", Select = ReturnToUpgradeShip, Highlight = RevertWorkingProfile, CancelOption = true, PanelType = CMenu.PanelType.Small });

            //
            // Generator
            //
            MenuGenerator = new CMenu(game)
            {
                Position = GetShoppingPlayerMenuPosition(),
                MenuOptions = new List<CMenu.CMenuOption>(),
            };

            IEnumerable<string> generator_parts_own = new List<string>() { GetShoppingPilotData().GeneratorType };
            IEnumerable<string> generator_parts_all = generator_parts_own.Concat(CMap.GetMapNodeByStageName(CSaveData.GetCurrentGameData(Game).Stage).AvailableGeneratorParts);
            IEnumerable<string> generator_parts = generator_parts_all.Distinct().OrderBy(G => GeneratorDefinitions.GetPart(G).Price);
            foreach (string generator_part in generator_parts)
            {

                MenuGenerator.MenuOptions.Add(
                    new CMenu.CMenuOption()
                    {
                        Text = generator_part,
                        SubText = "Cost: " + GeneratorDefinitions.GetPart(generator_part).Price,
                        Data = generator_part,
                        Select = SelectGenerator,
                        SelectValidate = SelectValidateGenerator,
                        Highlight = HighlightGenerator,
                    }
                );
            }
            MenuGenerator.MenuOptions.Add(new CMenu.CMenuOption() { Text = "Done", Select = ReturnToUpgradeShip, Highlight = RevertWorkingProfile, CancelOption = true, PanelType = CMenu.PanelType.Small });

            //
            // Shield
            //
            MenuShield = new CMenu(game)
            {
                Position = GetShoppingPlayerMenuPosition(),
                MenuOptions = new List<CMenu.CMenuOption>(),
            };

            //
            // Scoreboard
            //
            MenuScoreboard = new CMenu(game)
            {
                Position = GetShoppingPlayerMenuPosition(),
                MenuOptions = new List<CMenu.CMenuOption>()
                {
                    new CMenu.CMenuOption() { Text = "Back", Select = CloseScoreboard, CancelOption = true, PanelType = CMenu.PanelType.Normal },
                }
            };

            IEnumerable<string> shield_parts_own = new List<string>() { GetShoppingPilotData().ShieldType };
            IEnumerable<string> shield_parts_all = shield_parts_own.Concat(CMap.GetMapNodeByStageName(CSaveData.GetCurrentGameData(Game).Stage).AvailableShieldParts);
            IEnumerable<string> shield_parts = shield_parts_all.Distinct().OrderBy(S => ShieldDefinitions.GetPart(S).Price);
            foreach (string shield_part in shield_parts)
            {
                MenuShield.MenuOptions.Add(
                    new CMenu.CMenuOption()
                    {
                        Text = shield_part,
                        SubText = "Cost: " + ShieldDefinitions.GetPart(shield_part).Price,
                        Data = shield_part,
                        Select = SelectShield,
                        SelectValidate = SelectValidateShield,
                        Highlight = HighlightShield,
                    }
                );
            }
            MenuShield.MenuOptions.Add(new CMenu.CMenuOption() { Text = "Done", Select = ReturnToUpgradeShip, Highlight = RevertWorkingProfile, CancelOption = true, PanelType = CMenu.PanelType.Small });


            Menu = MenuBase;
            DrawMenuErrata = DrawMenuBaseErrata;

            EmptyWorld.GameCamera.Position = new Vector3(0.0f, 0.0f, 0.0f);

            if (!Game.EditorMode)
                CAudio.PlayMusic("Konami's_Moon_Base");

            RefreshSampleDisplay();
        }

        public override void OnExit()
        {
            base.OnExit();

            EmptyWorld.Stop();

            Game.HudManager.Huds[0].ShowMoney = true;
            Game.HudManager.Huds[1].ShowMoney = true;
        }

        public override void Update()
        {
            if (Game.PlayersInGame > 1)
            {
                if (Game.Input.IsL1PressedAny() || Game.Input.IsKeyPressed(Keys.F1))
                    ChangeShoppingPlayer(GameControllerIndex.One);
                if (Game.Input.IsR1PressedAny() || Game.Input.IsKeyPressed(Keys.F2))
                    ChangeShoppingPlayer(GameControllerIndex.Two);
            }

            MenuUpdateHighlights();
            Menu.Update();
            Game.HudManager.Huds[(int)ShoppingPlayer].Ship = SampleShip;
            Game.HudManager.Huds[(int)ShoppingPlayer].ShowMoney = false;
            Game.HudManager.Huds[(int)ShoppingPlayer].Update();
            UpdateGenerateEnemy();
            EmptyWorld.GameCamera.Update();
            EmptyWorld.UpdateEntitiesSingleThreadCollision();
            EmptyWorld.ProcessEntityAdd();
            EmptyWorld.ProcessEntityDelete();
            EmptyWorld.BackgroundScenery.Update();
            EmptyWorld.ForegroundScenery.Update();
            EmptyWorld.ParticleEffects.Update();
            SampleShip.UpdateGenerator();

            SampleShotDelay = Math.Max(0, SampleShotDelay - 1);
            if (SampleShotDelay <= 0)
            {
                SampleShip.FirePrimarySecondaryWeapons();
                SampleShip.ChargeAndFireFullSidekick(SampleShip.WeaponSidekickLeft);
                SampleShip.ChargeAndFireFullSidekick(SampleShip.WeaponSidekickRight);
                SampleShip.UpdateWeapons();
            }

            if (ScorePanel != null && ScorePanel.IsVisible())
                ScorePanel.Update();
        }

        public override void Draw()
        {
            EmptyWorld.DrawBackground(EmptyWorld.GameCamera);
            EmptyWorld.DrawEntities(EmptyWorld.GameCamera);

            Game.DefaultSpriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, EmptyWorld.GameCamera.WorldMatrix);
            SampleShip.Draw(Game.DefaultSpriteBatch);
            Game.DefaultSpriteBatch.End();

            Game.DefaultSpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, EmptyWorld.GameCamera.WorldMatrix);
            EmptyWorld.ParticleEffects.Draw(Game.DefaultSpriteBatch);
            Game.DefaultSpriteBatch.End();

            Game.DefaultSpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Game.RenderScaleMatrix);

            float panel_x = ShoppingPlayer == GameControllerIndex.One ? 928.0f : 480.0f;
            SpriteEffects panel_effects = ShoppingPlayer == GameControllerIndex.One ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            Game.DefaultSpriteBatch.Draw(ShopPanelTexture, new Vector2(panel_x, -90.0f), null, Color.White, 0.0f, Vector2.Zero, 1.0f, panel_effects, CLayers.UI);
            Menu.Draw(Game.DefaultSpriteBatch);

            // TODO: cache!
            float used = GetShoppingPilotTotalUsedMoney();
            float total = used + LockedProfile.Money;

            float delta = LockedProfile.Money - WorkingProfile.Money;
            float a = 0.0f;         
            float b = used + Math.Min(0.0f, delta); // negative
            float c = used;
            float d = used + Math.Max(0.0f, delta); // positive
            float e = total;

            a /= total;
            b /= total;
            c /= total;
            d /= total;
            e /= total;

            float alpha = 0.5f + (float)(Math.Abs(Math.Sin(Game.GameFrame * 0.05f))) * 0.25f;

            // TODO: better display somehow
            if (WorkingProfile.Money < 0)
                alpha *= 0.3f;

            DrawMoneyPoolBar(MoneyPoolEmpty.Texture, a, e, 1.0f);
            DrawMoneyPoolBar(MoneyPoolFull.Texture, a, b, 1.0f);
            DrawMoneyPoolBar(MoneyPoolSell.Texture, b, c, alpha);
            DrawMoneyPoolBar(MoneyPoolBuy.Texture, c, d, alpha);
            MoneyPoolBar.Draw(Game.DefaultSpriteBatch, new Vector2(502.0f + 920.0f * b, 972.0f), 0.0f);
            MoneyPoolBar.Draw(Game.DefaultSpriteBatch, new Vector2(502.0f + 920.0f * c, 972.0f), 0.0f);
            MoneyPoolBar.Draw(Game.DefaultSpriteBatch, new Vector2(502.0f + 920.0f * d, 972.0f), 0.0f);

            MoneyPoolLabel.Value = String.Format("{0}/{1}￥", used, total);
            MoneyPoolLabel.Draw(Game.DefaultSpriteBatch, Game.GameLargeFont, new Vector2(966.0f, 972.0f), Color.LightGray);

            // TODO: garbage
            //Game.DefaultSpriteBatch.DrawString(Game.GameRegularFont, (int)Math.Round(c * 100) + "%", new Vector2(960.0f, 962.0f), Color.LightGray, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, CLayers.UI+ CLayers.SubLayerIncrement);
            //Game.DefaultSpriteBatch.DrawString(Game.GameRegularFont, "equipment usage", new Vector2(960.0f, 962.0f), Color.LightGray, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, CLayers.UI+ CLayers.SubLayerIncrement);
            //Game.DefaultSpriteBatch.DrawString(Game.GameRegularFont, (int)Math.Round(c * 100) + "%", new Vector2(960.0f, 962.0f), Color.LightGray, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, CLayers.UI+ CLayers.SubLayerIncrement);

            DrawMoney();
            DrawMenuErrata();

            //if (Game.PlayersInGame > 1)
            //{
                //Game.DefaultSpriteBatch.DrawString(Game.GameRegularFont, "L1", new Vector2(328.0f, 240.0f), Color.Gray, 0.0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0.0f);
                //Game.DefaultSpriteBatch.DrawString(Game.GameRegularFont, "R1", new Vector2(1600.0f, 240.0f), Color.Gray, 0.0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0.0f);
            //}

            if (ScorePanel != null && ScorePanel.IsVisible())
                ScorePanel.Draw(Game.DefaultSpriteBatch);

            Game.DefaultSpriteBatch.End();
        }

        void DrawMoneyPoolBar(Texture2D texture, float from, float to, float alpha)
        {
            float x = from;
            float fullness = to - from;
            float width = 920.0f;
            Vector2 pool_position = new Vector2(966.0f + width * x, 972.0f);

            // NOTE: sprite scaling is completely broken, so we just hack it here
            Rectangle dst = new Rectangle((int)(pool_position.X - width / 2.0f), (int)pool_position.Y - 64, (int)(fullness * width), 128);
            Rectangle src = new Rectangle((int)(x * width), 0, (int)(fullness * width), 128);
            Game.DefaultSpriteBatch.Draw(
                texture,
                dst,
                src,
                new Color(255, 255, 255, (byte)(alpha * 255.0f)),
                0.0f,
                new Vector2(0.0f, 0.5f),
                SpriteEffects.None,
                0.0f
            );

        }

        private void MenuUpdateHighlights()
        {
            if (Menu == MenuPrimaryWeapon)
                foreach (CMenu.CMenuOption option in MenuPrimaryWeapon.MenuOptions)
                    option.SpecialHighlight = (string) option.Data == LockedProfile.WeaponPrimaryType;
            if (Menu == MenuSecondaryWeapon)
                foreach (CMenu.CMenuOption option in MenuSecondaryWeapon.MenuOptions)
                    option.SpecialHighlight = (string) option.Data == LockedProfile.WeaponSecondaryType;
            if (Menu == MenuSidekick)
                foreach (CMenu.CMenuOption option in MenuSidekick.MenuOptions)
                    option.SpecialHighlight = (string) option.Data == LockedProfile.WeaponSidekickType;
            if (Menu == MenuChassis)
                foreach (CMenu.CMenuOption option in MenuChassis.MenuOptions)
                    option.SpecialHighlight = (string) option.Data == LockedProfile.ChassisType;
            if (Menu == MenuGenerator)
                foreach (CMenu.CMenuOption option in MenuGenerator.MenuOptions)
                    option.SpecialHighlight = (string) option.Data == LockedProfile.GeneratorType;
            if (Menu == MenuShield)
                foreach (CMenu.CMenuOption option in MenuShield.MenuOptions)
                    option.SpecialHighlight = (string) option.Data == LockedProfile.ShieldType;
            if (Menu == MenuTrainPilot)
                foreach (CMenu.CMenuOption option in MenuTrainPilot.MenuOptions)
                    if (option.Data != null)
                        option.SpecialHighlight = HasAbilityWithLockedProfile(option.Data);
        }

        private SProfilePilotState GetShoppingPilotData()
        {
            return CSaveData.GetCurrentGameData(Game).Pilots[(int)ShoppingPlayer];
        }

        private int GetShoppingPilotTotalUsedMoney()
        {
            int item_value =
                CWeaponFactory.GetTotalPriceForLevel(LockedProfile.WeaponPrimaryType, LockedProfile.WeaponPrimaryLevel) +
                CWeaponFactory.GetTotalPriceForLevel(LockedProfile.WeaponSecondaryType, LockedProfile.WeaponSecondaryLevel) +
                CWeaponFactory.GetTotalPriceForLevel(LockedProfile.WeaponSidekickType, LockedProfile.WeaponSidekickLevel) +
                ChassisDefinitions.GetPart(LockedProfile.ChassisType).Price +
                GeneratorDefinitions.GetPart(LockedProfile.GeneratorType).Price +
                ShieldDefinitions.GetPart(LockedProfile.ShieldType).Price;

            int skill_value = 
                (LockedProfile.AbilityUnlocked0 ? CAbility.AbilityPrice : 0) +
                (LockedProfile.AbilityUnlocked1 ? CAbility.AbilityPrice : 0) +
                (LockedProfile.AbilityUnlocked2 ? CAbility.AbilityPrice : 0);

            return item_value + skill_value;
        }

        private void DrawShipStats()
        {
            Vector2 KeysBase = new Vector2(10.0f, 10.0f);
            Vector2 ValuesBase = new Vector2(180.0f, 10.0f);
            Vector2 Step = new Vector2(0.0f, 30.0f);

            string[] keys = {
                "Money",
                "Chassis",
                "Generator",
                "Shield",
                "Main Weapon",
                "Support Weapon",
                "Sidekick Left",
                "Sidekick Right"
            };

            string[] values = 
            {
                LockedProfile.Money.ToString(),
                LockedProfile.ChassisType,
                LockedProfile.GeneratorType,
                LockedProfile.ShieldType,
                LockedProfile.WeaponPrimaryType,
                LockedProfile.WeaponSecondaryType,
                LockedProfile.WeaponSidekickType,
            };

            foreach (int index in Enumerable.Range(0, keys.Length))
            {
                Game.DefaultSpriteBatch.DrawString(Game.GameRegularFont, keys[index], KeysBase + Step * index, Color.White);
            }

            foreach (int index in Enumerable.Range(0, values.Length))
            {
                if (values[index] == "")
                    Game.DefaultSpriteBatch.DrawString(Game.GameRegularFont, "None", ValuesBase + Step * index, Color.White);
                else
                    Game.DefaultSpriteBatch.DrawString(Game.GameRegularFont, values[index], ValuesBase + Step * index, Color.White);
            }
        }

        private void DrawMoney()
        {
            int base_ = WorkingProfile.Money;
            int diff = WorkingProfile.Money - LockedProfile.Money;

            if (diff == 0)
                return;

            Color color = Color.White;
            if (diff < 0)
                color = Color.LightSalmon;
            if (diff > 0)
                color = Color.LightGreen;

            // TODO: money display
            Game.DefaultSpriteBatch.DrawString(Game.GameRegularFont, WorkingProfile.Money.ToString(), Game.HudManager.Huds[(int)ShoppingPlayer].MoneyTextPosition + new Vector2(0.0f, -48.0f), color, 0.0f, Vector2.Zero, 1.5f, SpriteEffects.None, CLayers.UI+ CLayers.SubLayerIncrement);
        }

        private void RefreshSampleDisplay()
        {
            // HACK: disable toggle weapons
            if (SampleShip != null)
            {
                SampleShip.DidntFirePrimarySecondaryWeapons();
                EmptyWorld.EntityDelete(SampleShip);
            }

            SampleShip = CShipFactory.GenerateShip(EmptyWorld, WorkingProfile, ShoppingPlayer);

            float x = ShoppingPlayer == GameControllerIndex.One ? -190.0f : 190.0f;
            SampleShip.Physics.Position = new Vector2(x, 55.0f);

            SampleShotDelay = 15;

            SetScoreboardPosition();

            Game.HudManager.Huds[(int)ShoppingPlayer].ShowMoney = false;
        }

        private void RevertWorkingProfile(object tag)
        {
            WorkingProfile = LockedProfile;
            RefreshSampleDisplay();
        }

        private void LockWorkingProfile()
        {
            LockedProfile = WorkingProfile;
        }

        private void SaveLockedProfile()
        {
            SProfile profile = CSaveData.GetCurrentProfile();
            profile.Game[Game.PlayersInGame - 1].Pilots[(int)ShoppingPlayer] = LockedProfile;
            CSaveData.SetCurrentProfileData(profile);
            CSaveData.SaveRequest();
        }

        private void DrawMenuBaseErrata()
        {
            Vector2 panel_position = new Vector2(Game.Resolution.X / 2.0f - 442.0f, Game.Resolution.Y - 452.0f);
            Vector2 blank_position = new Vector2(Game.Resolution.X / 2.0f - 180.0f, Game.Resolution.Y - 410.0f);

            if (ShoppingPlayer == GameControllerIndex.Two)
            {
                panel_position = new Vector2(Game.Resolution.X / 2.0f - 70.0f, panel_position.Y);
                blank_position = new Vector2(Game.Resolution.X / 2.0f + 202.0f, blank_position.Y);
            }

            Game.DefaultSpriteBatch.Draw(ShopUpgradePanelTexture, panel_position, Color.White);

            if (Menu == MenuPrimaryWeapon)
            {
                int max = CWeaponFactory.GetMaxLevel(WorkingProfile.WeaponPrimaryType);
                int level = WorkingProfile.WeaponPrimaryLevel;

                CMenu.CMenuOption option = Menu.MenuOptions[Menu.Cursor];
                if (!option.CancelOption && WorkingProfile.WeaponPrimaryType != "")
                {
                    Vector2 position = blank_position;
                    ShopUpgradeBarsVisual.Frame = level;
                    ShopUpgradeBarsVisual.Recache();
                    ShopUpgradeBarsVisual.Draw(Game.DefaultSpriteBatch, position + new Vector2(-6.0f, 176.0f), 0.0f);

                    int price = CWeaponFactory.GetPriceForLevel(WorkingProfile.WeaponPrimaryType, WorkingProfile.WeaponPrimaryLevel);
                    int next_price = CWeaponFactory.GetPriceForLevel(WorkingProfile.WeaponPrimaryType, WorkingProfile.WeaponPrimaryLevel + 1);

                    Labels.BaseCostPrice.Value = price;
                    Labels.BaseCostHeader.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, position, Color.White);
                    Labels.BaseCostPrice.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, position + new Vector2(0.0f, 30.0f), Color.White);

                    Labels.NextUpgradeHeader.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, position + new Vector2(0.0f, 62.0f), Color.White);
                    if (level < max)
                    {
                        Labels.NextUpgradePrice.Value = next_price;
                        Labels.NextUpgradePrice.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, position + new Vector2(0.0f, 90.0f), Color.White);
                    }
                    else
                    {
                        // TODO: debug me to check that no strings are generated each time
                        Labels.NextUpgradePrice.Value = "MAX";
                        Labels.NextUpgradePrice.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, position + new Vector2(0.0f, 90.0f), Color.White);
                    }

                    if (SelectValidatePrimaryWeapon(Menu.MenuOptions[Menu.Cursor].Data) || WorkingProfile.WeaponPrimaryType == "None")
                        DrawPurchasePanel();
                }
            }
            else if (Menu == MenuSecondaryWeapon)
            {
                int max = CWeaponFactory.GetMaxLevel(WorkingProfile.WeaponSecondaryType);
                int level = WorkingProfile.WeaponSecondaryLevel;

                CMenu.CMenuOption option = Menu.MenuOptions[Menu.Cursor];
                if (!option.CancelOption && WorkingProfile.WeaponSecondaryType != "")
                {
                    Vector2 position = blank_position;
                    ShopUpgradeBarsVisual.Frame = level;
                    ShopUpgradeBarsVisual.Recache();
                    ShopUpgradeBarsVisual.Draw(Game.DefaultSpriteBatch, position + new Vector2(-6.0f, 176.0f), 0.0f);

                    int price = CWeaponFactory.GetPriceForLevel(WorkingProfile.WeaponSecondaryType, WorkingProfile.WeaponSecondaryLevel);
                    int next_price = CWeaponFactory.GetPriceForLevel(WorkingProfile.WeaponSecondaryType, WorkingProfile.WeaponSecondaryLevel + 1);

                    Labels.BaseCostPrice.Value = price;
                    Labels.BaseCostHeader.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, position, Color.White);
                    Labels.BaseCostPrice.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, position + new Vector2(0.0f, 30.0f), Color.White);

                    Labels.NextUpgradeHeader.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, position + new Vector2(0.0f, 62.0f), Color.White);
                    if (level < max)
                    {
                        Labels.NextUpgradePrice.Value = next_price;
                        Labels.NextUpgradePrice.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, position + new Vector2(0.0f, 90.0f), Color.White);
                    }
                    else
                    {
                        // TODO: debug me to check that no strings are generated each time
                        Labels.NextUpgradePrice.Value = "MAX";
                        Labels.NextUpgradePrice.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, position + new Vector2(0.0f, 90.0f), Color.White);
                    }
                }

                if (!option.CancelOption)
                {
                    if (WorkingProfile.WeaponSecondaryType == "")
                    {
                        Vector2 position = blank_position;
                        Labels.BaseCostPrice.Value = 0;
                        Labels.BaseCostHeader.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, position, Color.White);
                        Labels.BaseCostPrice.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, position + new Vector2(0.0f, 30.0f), Color.White);
                    }

                    if (SelectValidateSecondaryWeapon(Menu.MenuOptions[Menu.Cursor].Data))
                        DrawPurchasePanel();
                }
            }
            else if (Menu == MenuSidekick)
            {
                CMenu.CMenuOption option = Menu.MenuOptions[Menu.Cursor];
                if (!option.CancelOption && WorkingProfile.WeaponSidekickType != "")
                {
                    Vector2 position = blank_position + new Vector2(0.0f, 30.0f);
                    int price = CWeaponFactory.GetPriceForLevel(WorkingProfile.WeaponSidekickType, WorkingProfile.WeaponSidekickLevel);

                    Labels.BaseCostPrice.Value = price;
                    Labels.BaseCostHeader.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, position, Color.White);
                    Labels.BaseCostPrice.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, position + new Vector2(0.0f, 30.0f), Color.White);
                }

                if (!option.CancelOption)
                {
                    if (WorkingProfile.WeaponSidekickType == "")
                    {
                        Vector2 position = blank_position;
                        Labels.BaseCostPrice.Value = 0;
                        Labels.BaseCostHeader.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, position, Color.White);
                        Labels.BaseCostPrice.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, position + new Vector2(0.0f, 30.0f), Color.White);
                    }

                    if (SelectValidateSidekick(Menu.MenuOptions[Menu.Cursor].Data))
                        DrawPurchasePanel();
                }
            }
            else if (Menu == MenuChassis)
            {
                CChassisPart part = ChassisDefinitions.GetPart(WorkingProfile.ChassisType);
                CMenu.CMenuOption option = Menu.MenuOptions[Menu.Cursor];
                if (!option.CancelOption)
                {
                    Vector2 position0 = blank_position + new Vector2(0.0f, 10.0f);
                    Vector2 position1 = blank_position + new Vector2(0.0f, 80.0f);
                    Vector2 position2 = blank_position + new Vector2(0.0f, 150.0f);

                    Labels.ArmorValue.Value = Convert.ToInt32(part.Armor * 100.0f);
                    Labels.ArmorHeader.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, position0, Color.White);
                    Labels.ArmorValue.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, position0 + new Vector2(0.0f, 30.0f), Color.White);

                    Labels.SpeedValue.Value = Convert.ToInt32(part.Speed * 100.0f);
                    Labels.SpeedHeader.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, position1, Color.White);
                    Labels.SpeedValue.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, position1 + new Vector2(0.0f, 30.0f), Color.White);

                    Labels.DensityValue.Value = Convert.ToInt32(part.CollisionResistance * 100.0f);
                    Labels.DensityHeader.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, position2, Color.White);
                    Labels.DensityValue.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, position2 + new Vector2(0.0f, 30.0f), Color.White);
                }

                if (!option.CancelOption)
                {
                    if (SelectValidateChassis(Menu.MenuOptions[Menu.Cursor].Data))
                        DrawPurchasePanel();
                }
            }
            else if (Menu == MenuGenerator)
            {
                CGeneratorPart part = GeneratorDefinitions.GetPart(WorkingProfile.GeneratorType);
                CMenu.CMenuOption option = Menu.MenuOptions[Menu.Cursor];
                if (!option.CancelOption)
                {
                    Vector2 position0 = blank_position + new Vector2(0.0f, 10.0f);
                    Vector2 position1 = blank_position + new Vector2(0.0f, 80.0f);
                    Vector2 position2 = blank_position + new Vector2(0.0f, 160.0f);

                    Labels.EnergyValue.Value = Convert.ToInt32(part.Energy * 100.0f);
                    Labels.EnergyHeader.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, position0, Color.White);
                    Labels.EnergyValue.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, position0 + new Vector2(0.0f, 30.0f), Color.White);

                    Labels.RegenValue.Value = Convert.ToInt32(part.Regen * 10000.0f);
                    Labels.RegenHeader.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, position1, Color.White);
                    Labels.RegenValue.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, position1 + new Vector2(0.0f, 30.0f), Color.White);

                    if (!String.IsNullOrEmpty(part.Description))
                    {
                        Labels.DescValue.Value = part.Description;
                        Labels.DescHeader.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, position2, Color.White);
                        Labels.DescValue.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, position2 + new Vector2(0.0f, 30.0f), Color.White);
                    }
                }

                if (!option.CancelOption)
                {
                    if (SelectValidateGenerator(Menu.MenuOptions[Menu.Cursor].Data))
                        DrawPurchasePanel();
                }
            }
            else if (Menu == MenuShield)
            {
                CShieldPart part = ShieldDefinitions.GetPart(WorkingProfile.ShieldType);
                CMenu.CMenuOption option = Menu.MenuOptions[Menu.Cursor];
                if (!option.CancelOption)
                {
                    Vector2 position0 = blank_position + new Vector2(0.0f, 10.0f);
                    Vector2 position1 = blank_position + new Vector2(0.0f, 80.0f);
                    Vector2 position2 = blank_position + new Vector2(0.0f, 150.0f);

                    Labels.ShieldValue.Value = Convert.ToInt32(part.Shield * 100.0f);
                    Labels.ShieldHeader.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, position0, Color.White);
                    Labels.ShieldValue.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, position0 + new Vector2(0.0f, 30.0f), Color.White);

                    Labels.RegenValue.Value = Convert.ToInt32(part.EnergyDrain * 100.0f);
                    Labels.RegenHeader.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, position1, Color.White);
                    Labels.RegenValue.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, position1 + new Vector2(0.0f, 30.0f), Color.White);

                    Labels.EfficiencyValue.Value = Convert.ToInt32(part.Efficiency * 100.0f);
                    Labels.EfficiencyHeader.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, position2, Color.White);
                    Labels.EfficiencyValue.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, position2 + new Vector2(0.0f, 30.0f), Color.White);
                }

                if (!option.CancelOption)
                {
                    if (SelectValidateShield(Menu.MenuOptions[Menu.Cursor].Data))
                        DrawPurchasePanel();
                }
            }
            else
            {
                ShowHelpInfo();
            }
        }

        private void ShowHelpInfo()
        {
            Vector2 blank_position = new Vector2(Game.Resolution.X / 2.0f - 190.0f, Game.Resolution.Y - 400.0f);

            if (ShoppingPlayer == GameControllerIndex.Two)
            {
                blank_position = new Vector2(Game.Resolution.X / 2.0f + 192.0f, blank_position.Y);
            }

            string[] strings = {
                "WELCOME TO SHOP",
                "PURCHASE UPGRADES",
                "AND TRAINING HERE",
                "",
            };

            if (Game.PlayersInGame > 1)
            {
                strings[3] = "L1/R1 TO CHANGE PLAYER";
            }

            if (Menu == MenuUpgradeShip)
            {
                switch (Menu.Cursor)
                {
                    case 0: // chassis
                        strings[0] = "CHASSIS";
                        strings[1] = "UPGRADE FOR IMPROVED";
                        strings[2] = "HULL ARMOR AND SPEED";
                        break;

                    case 1: // generator
                        strings[0] = "GENERATOR";
                        strings[1] = "PROVIDES ENERGY FOR";
                        strings[2] = "WEAPONS AND SHIELD";
                        break;

                    case 2: // shield
                        strings[0] = "SHIELD";
                        strings[1] = "UPGRADE FOR IMPROVED";
                        strings[2] = "STORAGE AND REGEN";
                        break;

                    case 3: // primary
                        strings[0] = "PRIMARY WEAPON";
                        strings[1] = "SHIP MAIN WEAPON";
                        strings[2] = "MAKE IT STRONG";
                        break;

                    case 4: // secondary
                        strings[0] = "SECONDARY WEAPON";
                        strings[1] = "AUXILARY FIREPOWER";
                        strings[2] = "USE WITHOUT CAUTION";
                        break;

                    case 5: // sidekick left
                        strings[0] = "SIDEKICK LEFT";
                        strings[1] = "INCREASE FIREPOWER";
                        strings[2] = "FURTHER WITH SIDEKICKS";
                        break;

                    case 6: // sidekick right
                        strings[0] = "SIDEKICK RIGHT";
                        strings[1] = "INCREASE FIREPOWER";
                        strings[2] = "FURTHER WITH SIDEKICKS";
                        break;

                    case 7: // +10000
                    case 8: // -10000
                    default:
                        break;
                }
            }
            else if (Menu == MenuTrainPilot)
            {
                strings[0] = "PILOT ABILITIES";
                strings[1] = "EQUIP FOR SPECIAL";
                strings[2] = "ABILITIES!";
            }

            float offset = Game.PlayersInGame == 1 ? 20.0f : 0.0f;
            Game.DefaultSpriteBatch.DrawStringAlignCenter(Game.GameRegularFont, blank_position + new Vector2(0.0f, 30.0f + offset), strings[0], Color.White);
            Game.DefaultSpriteBatch.DrawStringAlignCenter(Game.GameRegularFont, blank_position + new Vector2(0.0f, 60.0f + offset), strings[1], Color.White);
            Game.DefaultSpriteBatch.DrawStringAlignCenter(Game.GameRegularFont, blank_position + new Vector2(0.0f, 90.0f + offset), strings[2], Color.White);
            Game.DefaultSpriteBatch.DrawStringAlignCenter(Game.GameRegularFont, blank_position + new Vector2(0.0f, 150.0f + offset), strings[3], Color.White);
        }

        private void StageSelect(object tag)
        {
            SaveLockedProfile();
            Game.State = new CStateFadeTo(Game, this, new CStateStageSelect(Game));
        }

        private void ShowScoreboard(object tag)
        {
            SaveLockedProfile();
            ScorePanel = new CScorePanel(Game);
            ScorePanel.SetVisible(true);
            ScorePanel.ShowContinue = false;
            SetScoreboardPosition();
            Menu = MenuScoreboard;
        }

        private void UpgradeShip(object tag)
        {
            Menu = MenuUpgradeShip;
        }

        private void TrainPilot(object tag)
        {
            Menu = MenuTrainPilot;
            MenuTrainPilot.MoveToFirstValid();
        }

        private void EditPrimaryWeapon(object tag)
        {
            Menu = MenuPrimaryWeapon;
            Menu.ForceRefresh();
        }

        private void EditSecondaryWeapon(object tag)
        {
            Menu = MenuSecondaryWeapon;
            Menu.ForceRefresh();
        }

        private void EditSidekick(object tag)
        {
            Menu = MenuSidekick;
            Menu.ForceRefresh();
        }

        private void EditMoney(object tag, int axis)
        {
            if (axis == 0)
                return;

            Menu.MenuOptions[Menu.Cursor].AxisValue = 0;

            if (axis < 0)
                WorkingProfile.Money -= 10000;
            else if (axis > 0)
                WorkingProfile.Money += 10000;

            WorkingProfile.Money = Math.Max(0, WorkingProfile.Money);
            LockWorkingProfile();
        }

        private void EditChassis(object tag)
        {
            Menu = MenuChassis;
            Menu.ForceRefresh();
        }

        private void EditGenerator(object tag)
        {
            Menu = MenuGenerator;
            Menu.ForceRefresh();
        }

        private void EditShield(object tag)
        {
            Menu = MenuShield;
            Menu.ForceRefresh();
        }

        private void Back(object tag)
        {
            SaveLockedProfile();
            Game.State = new CStateFadeTo(Game, this, new CStateMainMenu(Game));
        }

        private void SelectPrimaryWeapon(object tag)
        {
            if (WorkingProfile.WeaponPrimaryType == LockedProfile.WeaponPrimaryType && WorkingProfile.WeaponPrimaryLevel == LockedProfile.WeaponPrimaryLevel)
                return;

            CAudio.PlaySound("MenuBuy");

            LockWorkingProfile();
            RefreshSampleDisplay();
        }

        private bool SelectValidatePrimaryWeapon(object tag)
        {
            int sell = CWeaponFactory.GetTotalPriceForLevel(WorkingProfile.WeaponPrimaryType, WorkingProfile.WeaponPrimaryLevel);
            int buy = CWeaponFactory.GetPriceForLevel((string)tag, 0);
            if (buy > WorkingProfile.Money + sell)
                return false;

            return true;
        }

        private void HighlightPrimaryWeapon(object tag)
        {
            if (WorkingProfile.WeaponPrimaryType != (string)tag)
            {
                int level = 0;
                if (LockedProfile.WeaponPrimaryType == (string)tag)
                    level = LockedProfile.WeaponPrimaryLevel;

                int sell = CWeaponFactory.GetTotalPriceForLevel(WorkingProfile.WeaponPrimaryType, WorkingProfile.WeaponPrimaryLevel);
                int buy = CWeaponFactory.GetTotalPriceForLevel((string)tag, level);
                int remaining = WorkingProfile.Money + sell - buy;

                WorkingProfile.Money += sell;
                WorkingProfile.WeaponPrimaryType = (string)tag;
                WorkingProfile.WeaponPrimaryLevel = level;
                WorkingProfile.Money -= buy;

                RefreshSampleDisplay();
            }
        }

        private void AxisPrimaryWeapon(object tag, int axis)
        {
            if (!AxisValidatePrimaryWeapon(tag, axis))
                return;

            WorkingProfile.Money += CWeaponFactory.GetTotalPriceForLevel(WorkingProfile.WeaponPrimaryType, WorkingProfile.WeaponPrimaryLevel);
            WorkingProfile.WeaponPrimaryType = (string)tag;
            WorkingProfile.WeaponPrimaryLevel = axis;
            WorkingProfile.Money -= CWeaponFactory.GetTotalPriceForLevel(WorkingProfile.WeaponPrimaryType, WorkingProfile.WeaponPrimaryLevel);
            RefreshSampleDisplay();
        }

        private bool AxisValidatePrimaryWeapon(object tag, int axis)
        {
            // we cannot axis on a weapon we are unable to highlight due to price
            if (WorkingProfile.WeaponPrimaryType != (string)tag)
                return false;

            if (axis < 0 || axis > CWeaponFactory.GetMaxLevel((string)tag))
                return false;

            int sell = CWeaponFactory.GetTotalPriceForLevel(WorkingProfile.WeaponPrimaryType, WorkingProfile.WeaponPrimaryLevel);
            int buy = CWeaponFactory.GetTotalPriceForLevel(WorkingProfile.WeaponPrimaryType, axis);
            int remaining = WorkingProfile.Money + sell - buy;
            if (remaining < 0)
                return false;
            
            return true;
        }

        private void SelectSecondaryWeaponEmpty(object tag)
        {
            if (WorkingProfile.WeaponSecondaryType == LockedProfile.WeaponSecondaryType && WorkingProfile.WeaponSecondaryLevel == LockedProfile.WeaponSecondaryLevel)
                return;

            CAudio.PlaySound("MenuBuy");

            int sell = CWeaponFactory.GetTotalPriceForLevel(LockedProfile.WeaponSecondaryType, LockedProfile.WeaponSecondaryLevel);
            LockedProfile.Money += sell;
            LockedProfile.WeaponSecondaryType = "";
            LockedProfile.WeaponSecondaryLevel = 0;
            RevertWorkingProfile(null);
            RefreshSampleDisplay();
        }

        private void SelectSecondaryWeapon(object tag)
        {
            if (WorkingProfile.WeaponSecondaryType == LockedProfile.WeaponSecondaryType && WorkingProfile.WeaponSecondaryLevel == LockedProfile.WeaponSecondaryLevel)
                return;

            CAudio.PlaySound("MenuBuy");

            LockWorkingProfile();
            RefreshSampleDisplay();
        }

        private bool SelectValidateSecondaryWeapon(object tag)
        {
            int sell = CWeaponFactory.GetTotalPriceForLevel(WorkingProfile.WeaponSecondaryType, WorkingProfile.WeaponSecondaryLevel);
            int buy = CWeaponFactory.GetPriceForLevel((string)tag, 0);
            if (buy > WorkingProfile.Money + sell)
                return false;

            return true;
        }

        private void HighlightSecondaryWeapon(object tag)
        {
            if (WorkingProfile.WeaponSecondaryType != (string)tag)
            {
                int level = 0;
                if (LockedProfile.WeaponSecondaryType == (string)tag)
                    level = LockedProfile.WeaponSecondaryLevel;

                int sell = CWeaponFactory.GetTotalPriceForLevel(WorkingProfile.WeaponSecondaryType, WorkingProfile.WeaponSecondaryLevel);
                int buy = CWeaponFactory.GetTotalPriceForLevel((string)tag, level);
                int remaining = WorkingProfile.Money + sell - buy;

                WorkingProfile.Money += sell;
                WorkingProfile.WeaponSecondaryType = (string)tag;
                WorkingProfile.WeaponSecondaryLevel = level;
                WorkingProfile.Money -= buy;

                RefreshSampleDisplay();
            }
        }

        private void HighlightSecondaryWeaponEmpty(object tag)
        {
            if (LockedProfile.WeaponSecondaryType == "")
            {
                int sell = CWeaponFactory.GetTotalPriceForLevel(WorkingProfile.WeaponSecondaryType, WorkingProfile.WeaponSecondaryLevel);

                WorkingProfile.Money += sell;
                WorkingProfile.WeaponSecondaryType = (string)tag;
                WorkingProfile.WeaponSecondaryLevel = 0;

                RefreshSampleDisplay();
            }
        }

        private void AxisSecondaryWeapon(object tag, int axis)
        {
            if (!AxisValidateSecondaryWeapon(tag, axis))
                return;

            WorkingProfile.Money += CWeaponFactory.GetTotalPriceForLevel(WorkingProfile.WeaponSecondaryType, WorkingProfile.WeaponSecondaryLevel);
            WorkingProfile.WeaponSecondaryType = (string)tag;
            WorkingProfile.WeaponSecondaryLevel = axis;
            WorkingProfile.Money -= CWeaponFactory.GetTotalPriceForLevel(WorkingProfile.WeaponSecondaryType, WorkingProfile.WeaponSecondaryLevel);
            RefreshSampleDisplay();
        }

        private bool AxisValidateSecondaryWeapon(object tag, int axis)
        {
            // we cannot axis on a weapon we are unable to highlight due to price
            if (WorkingProfile.WeaponSecondaryType != (string)tag)
                return false;

            if (axis < 0 || axis > CWeaponFactory.GetMaxLevel((string)tag))
                return false;

            int sell = CWeaponFactory.GetTotalPriceForLevel(WorkingProfile.WeaponSecondaryType, WorkingProfile.WeaponSecondaryLevel);
            int buy = CWeaponFactory.GetTotalPriceForLevel(WorkingProfile.WeaponSecondaryType, axis);
            int remaining = WorkingProfile.Money + sell - buy;
            if (remaining < 0)
                return false;
            
            return true;
        }

        private void SelectSidekick(object tag)
        {
            if (WorkingProfile.WeaponSidekickType == LockedProfile.WeaponSidekickType && WorkingProfile.WeaponSidekickLevel == LockedProfile.WeaponSidekickLevel)
                return;

            CAudio.PlaySound("MenuBuy");

            LockWorkingProfile();
            RefreshSampleDisplay();
        }

        private void SelectSidekickEmpty(object tag)
        {
            if (WorkingProfile.WeaponSidekickType == LockedProfile.WeaponSidekickType && WorkingProfile.WeaponSidekickLevel == LockedProfile.WeaponSidekickLevel)
                return;

            CAudio.PlaySound("MenuBuy");

            int sell = CWeaponFactory.GetTotalPriceForLevel(LockedProfile.WeaponSidekickType, LockedProfile.WeaponSidekickLevel);
            LockedProfile.Money += sell;
            LockedProfile.WeaponSidekickType = "";
            LockedProfile.WeaponSidekickLevel = 0;
            RevertWorkingProfile(null);
            RefreshSampleDisplay();
        }

        private bool SelectValidateSidekick(object tag)
        {
            int sell = CWeaponFactory.GetTotalPriceForLevel(WorkingProfile.WeaponSidekickType, WorkingProfile.WeaponSidekickLevel);
            int buy = CWeaponFactory.GetPriceForLevel((string)tag, 0);
            if (buy > WorkingProfile.Money + sell)
                return false;

            return true;
        }

        private void HighlightSidekick(object tag)
        {
            if (WorkingProfile.WeaponSidekickType != (string)tag)
            {
                int level = 0;
                if (LockedProfile.WeaponSidekickType == (string)tag)
                    level = LockedProfile.WeaponSidekickLevel;

                int sell = CWeaponFactory.GetTotalPriceForLevel(WorkingProfile.WeaponSidekickType, WorkingProfile.WeaponSidekickLevel);
                int buy = CWeaponFactory.GetTotalPriceForLevel((string)tag, level);
                int remaining = WorkingProfile.Money + sell - buy;

                WorkingProfile.Money += sell;
                WorkingProfile.WeaponSidekickType = (string)tag;
                WorkingProfile.WeaponSidekickLevel = level;
                WorkingProfile.Money -= buy;

                RefreshSampleDisplay();
            }
        }

        private void HighlightSidekickEmpty(object tag)
        {
            if (LockedProfile.WeaponSidekickType == "")
            {
                int sell = CWeaponFactory.GetTotalPriceForLevel(WorkingProfile.WeaponSidekickType, WorkingProfile.WeaponSidekickLevel);

                WorkingProfile.Money += sell;
                WorkingProfile.WeaponSidekickType = (string)tag;
                WorkingProfile.WeaponSidekickLevel = 0;

                RefreshSampleDisplay();
            }
        }

        private void SelectChassis(object tag)
        {
            if (WorkingProfile.ChassisType == LockedProfile.ChassisType && WorkingProfile.ChassisType == LockedProfile.ChassisType) 
                return;

            CAudio.PlaySound("MenuBuy");

            LockWorkingProfile();
            RefreshSampleDisplay();
        }

        private bool SelectValidateChassis(object tag)
        {
            int sell = ChassisDefinitions.GetPart(WorkingProfile.ChassisType).Price;
            int buy = ChassisDefinitions.GetPart((string)tag).Price;
            if (buy > WorkingProfile.Money + sell)
                return false;

            return true;
        }

        private void HighlightChassis(object tag)
        {
            if (WorkingProfile.ChassisType != (string)tag)
            {
                int sell = ChassisDefinitions.GetPart(WorkingProfile.ChassisType).Price;
                int buy = ChassisDefinitions.GetPart((string)tag).Price;
                int remaining = WorkingProfile.Money + sell - buy;

                WorkingProfile.Money += sell;
                WorkingProfile.ChassisType = (string)tag;
                WorkingProfile.Money -= buy;

                RefreshSampleDisplay();
            }
        }

        private void SelectGenerator(object tag)
        {
            if (WorkingProfile.GeneratorType == LockedProfile.GeneratorType && WorkingProfile.GeneratorType == LockedProfile.GeneratorType) 
                return;

            CAudio.PlaySound("MenuBuy");

            int before = GetShoppingPilotTotalUsedMoney();
            LockWorkingProfile();
            RefreshSampleDisplay();
            int after = GetShoppingPilotTotalUsedMoney();
        }

        private bool SelectValidateGenerator(object tag)
        {
            int sell = GeneratorDefinitions.GetPart(WorkingProfile.GeneratorType).Price;
            int buy = GeneratorDefinitions.GetPart((string)tag).Price;
            if (buy > WorkingProfile.Money + sell)
                return false;

            return true;
        }

        private void HighlightGenerator(object tag)
        {
            if (WorkingProfile.GeneratorType != (string)tag)
            {
                int sell = GeneratorDefinitions.GetPart(WorkingProfile.GeneratorType).Price;
                int buy = GeneratorDefinitions.GetPart((string)tag).Price;
                int remaining = WorkingProfile.Money + sell - buy;

                WorkingProfile.Money += sell;
                WorkingProfile.GeneratorType = (string)tag;
                WorkingProfile.Money -= buy;

                RefreshSampleDisplay();
            }
        }

        private void SelectShield(object tag)
        {
            if (WorkingProfile.ShieldType == LockedProfile.ShieldType && WorkingProfile.ShieldType == LockedProfile.ShieldType) 
                return;

            CAudio.PlaySound("MenuBuy");

            LockWorkingProfile();
            RefreshSampleDisplay();
        }

        private bool SelectValidateShield(object tag)
        {
            int sell = ShieldDefinitions.GetPart(WorkingProfile.ShieldType).Price;
            int buy = ShieldDefinitions.GetPart((string)tag).Price;
            if (buy > WorkingProfile.Money + sell)
                return false;

            return true;
        }

        private void HighlightShield(object tag)
        {
            if (WorkingProfile.ShieldType != (string)tag)
            {
                int sell = ShieldDefinitions.GetPart(WorkingProfile.ShieldType).Price;
                int buy = ShieldDefinitions.GetPart((string)tag).Price;
                int remaining = WorkingProfile.Money + sell - buy;

                WorkingProfile.Money += sell;
                WorkingProfile.ShieldType = (string)tag;
                WorkingProfile.Money -= buy;

                RefreshSampleDisplay();
            }
        }

        private void ReturnToBaseMenu(object tag)
        {
            Menu = MenuBase;
        }

        private void CloseScoreboard(object tag)
        {
            Menu = MenuBase;
            ScorePanel.SetVisible(false);
        }

        private void ReturnToUpgradeShip(object tag)
        {
            Menu = MenuUpgradeShip;
        }

        private void TrainAbility(object tag)
        {
            string ability_name = string.Format("AbilityUnlocked{0}", tag.ToString());
            FieldInfo field = typeof(SProfilePilotState).GetField(ability_name);
            bool has_ability = (bool)field.GetValue(WorkingProfile);

            if (!has_ability)
            {
                if (LockedProfile.Money < CAbility.AbilityPrice)
                    return;

                LockedProfile.Money -= CAbility.AbilityPrice;
            }
            else
            {
                LockedProfile.Money += CAbility.AbilityPrice;
            }

            has_ability = !has_ability;

            object reference = (object)LockedProfile;
            field.SetValue(reference, has_ability);
            LockedProfile = (SProfilePilotState)reference;

            RevertWorkingProfile(null);
            MenuUpdateHighlights();
        }

        private void HighlightAbility(object tag)
        {
            string ability_name = string.Format("AbilityUnlocked{0}", tag.ToString());
            FieldInfo field = typeof(SProfilePilotState).GetField(ability_name);
            bool has_ability = (bool)field.GetValue(WorkingProfile);

            if (has_ability)
                WorkingProfile.Money = LockedProfile.Money + CAbility.AbilityPrice;
            else
                WorkingProfile.Money = LockedProfile.Money - CAbility.AbilityPrice;
        }

        private void CancelHighlightAbility(object tag)
        {
            RevertWorkingProfile(null);
            MenuUpdateHighlights();
        }

        private bool ValidateAbility(object tag, SProfilePilotState profile)
        {
            string ability_name = string.Format("AbilityUnlocked{0}", tag.ToString());
            FieldInfo field = typeof(SProfilePilotState).GetField(ability_name);
            bool has_ability = (bool)field.GetValue(LockedProfile);

            if (!has_ability)
            {
                if (profile.Money < CAbility.AbilityPrice)
                    return false;
            }

            return true;
        }

        private bool ValidateAbilityWithLockedProfile(object tag)
        {
            return ValidateAbility(tag, LockedProfile);
        }

        private bool HasAbilityWithLockedProfile(object tag)
        {
            string ability_name = string.Format("AbilityUnlocked{0}", tag.ToString());
            FieldInfo field = typeof(SProfilePilotState).GetField(ability_name);
            bool has_ability = (bool)field.GetValue(LockedProfile);
            return has_ability;
        }

        private void ChangeShoppingPlayer(GameControllerIndex game_controller_index)
        {
            if (ShoppingPlayer == game_controller_index)
                return;

            SaveLockedProfile();

            ShoppingPlayer = game_controller_index;
            WorkingProfile = GetShoppingPilotData();
            LockedProfile = WorkingProfile;

            Vector2 position = GetShoppingPlayerMenuPosition();
            MenuBase.Position = position;
            MenuUpgradeShip.Position = position;
            MenuPrimaryWeapon.Position = position;
            MenuSecondaryWeapon.Position = position;
            MenuSidekick.Position = position;
            MenuChassis.Position = position;
            MenuGenerator.Position = position;
            MenuShield.Position = position;
            MenuTrainPilot.Position = position;

            Menu.ForceRefresh();

            RefreshSampleDisplay();
        }
        
        private Vector2 GetShoppingPlayerMenuPosition()
        {
            return ShoppingPlayer == GameControllerIndex.One ? new Vector2(1114.0f, 230.0f) : new Vector2(550.0f, 230.0f);
        }

        private void UpdateGenerateEnemy()
        {
            GenerateEnemyDelay += 1;
            if (GenerateEnemyDelay < 30)
                return;

            CEnemy enemy = null;
            int rand = EmptyWorld.Random.Next() % 4;
            switch (rand)
            {
                case 0: enemy = new CBall(); break;
                case 1: enemy = new CShootBall(); break;
                case 2: enemy = new CIsosceles(); break;
                case 3: enemy = new CBigBall(); break;
            }
            enemy.Initialize(EmptyWorld);
            bool left = EmptyWorld.Random.NextBool();
            enemy.Mover = left ? CMoverPresets.DownRight(6.0f, 1.5f) : CMoverPresets.DownLeft(6.0f, 1.5f);
            float offset = left ? -120.0f : 120.0f;
            enemy.Physics.Position = new Vector2(SampleShip.Physics.Position.X + offset, -570.0f);
            enemy.Coins = 0;
            EmptyWorld.EntityAdd(enemy);

            GenerateEnemyDelay = 0 - (int)(EmptyWorld.Random.NextFloat() * 60);
        }

        private void DrawPurchasePanel()
        {
            Vector2 panel_position = Menu.Position + new Vector2(20.0f, 564.0f);
            Game.DefaultSpriteBatch.Draw(ShopPurchasePanelTexture, panel_position, Color.White);
            ShopPurchaseTextLabel.Alignment = CTextLabel.EAlignment.Left;
            float scale = 1.0f + (float)(Math.Abs(Math.Sin(EmptyWorld.Game.GameFrame * 0.1f))) * 0.015f;
            ShopPurchaseTextLabel.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, panel_position + new Vector2(60.0f, 42.0f), Color.White, scale);
            //Game.DefaultSpriteBatch.DrawString(Game.GameRegularFont, "Purchase", panel_position + new Vector2(58.0f, 24.0f), Color.White);
        }

        private void SetScoreboardPosition()
        {
            if (ScorePanel != null && ScorePanel.IsVisible())
                ScorePanel.BasePosition = new Vector2(ShoppingPlayer == GameControllerIndex.One ? 774.0f : 1146.0f, 156.0f);
        }
    }
}