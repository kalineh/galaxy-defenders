//
// StateShop.cs
//

using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Galaxy
{
    public class CStateStateShop
        : CState
    {
        public CGalaxy Game { get; set; }
        private CWorld EmptyWorld { get; set; }
        public CMenu Menu { get; set; }
        private CMenu MenuBase { get; set; }
        private CMenu MenuUpgradeShip { get; set; }
        private CMenu MenuPrimaryWeapon { get; set; }
        private CMenu MenuSecondaryWeapon { get; set; }
        private CMenu MenuSidekickLeft { get; set; }
        private CMenu MenuSidekickRight { get; set; }
        private CMenu MenuChassis { get; set; }
        private CMenu MenuGenerator { get; set; }
        private CMenu MenuShield { get; set; }
        private CMenu MenuTrainPilot { get; set; }
        private delegate void DrawMenuErrataFunction();
        private DrawMenuErrataFunction DrawMenuErrata { get; set; }
        private CShip SampleShip { get; set; }
        private SProfile WorkingProfile;
        private SProfile LockedProfile;
        private Texture2D ShopPanelTexture { get; set; }
        private Texture2D ShopUpgradePanelTexture { get; set; }
        private CVisual ShopUpgradeBarsVisual { get; set; }

        public CStateStateShop(CGalaxy game)
        {
            Game = game;
            EmptyWorld = new CWorld(game, null);

            EmptyWorld.BackgroundScenery = new CSceneryChain(EmptyWorld,
                new CBackground(EmptyWorld, new Color(0, 0, 0)),
                new CStars(EmptyWorld, CContent.LoadTexture2D(Game, "Textures/Background/Star"), 0.4f, 3.5f),
                new CStars(EmptyWorld, CContent.LoadTexture2D(Game, "Textures/Background/Star"), 0.6f, 2.1f)
            );
            EmptyWorld.ForegroundScenery = CSceneryPresets.Empty(EmptyWorld);

            WorkingProfile = CSaveData.GetCurrentProfile();
            LockedProfile = WorkingProfile;

            ShopPanelTexture = CContent.LoadTexture2D(game, "Textures/UI/Shop/ShopPanel");
            ShopUpgradePanelTexture = CContent.LoadTexture2D(game, "Textures/UI/Shop/ShopUpgradePanel");
            ShopUpgradeBarsVisual = CVisual.MakeSpriteFromGame(game, "Textures/UI/Shop/ShopUpgradeBars", Vector2.One, Color.White);
            ShopUpgradeBarsVisual.TileY = 8;

            //
            // Base
            //
            MenuBase = new CMenu(game)
            {
                Position = new Vector2(Game.GraphicsDevice.Viewport.Width / 2.0f + 154.0f, 320.0f),
                MenuOptions = new List<CMenu.MenuOption>()
                {
                    new CMenu.MenuOption() { Text = "Play Next Stage", Select = StageSelect },
                    new CMenu.MenuOption() { Text = "Upgrade Ship", Select = UpgradeShip },
                    new CMenu.MenuOption() { Text = "Train Pilot", Select = TrainPilot },
                    new CMenu.MenuOption() { Text = "Back", Select = Back, CancelOption = true, PanelType = CMenu.PanelType.Small },
                }
            };

            //
            // Upgrade Ship
            //
            MenuUpgradeShip = new CMenu(game)
            {
                Position = new Vector2(Game.GraphicsDevice.Viewport.Width / 2.0f + 154.0f, 320.0f),
                MenuOptions = new List<CMenu.MenuOption>()
                {
                    new CMenu.MenuOption() { Text = "Chassis", Select = EditChassis },
                    new CMenu.MenuOption() { Text = "Generator", Select = EditGenerator },
                    new CMenu.MenuOption() { Text = "Shield", Select = EditShield },
                    new CMenu.MenuOption() { Text = "Main Weapon", Select = EditPrimaryWeapon },
                    new CMenu.MenuOption() { Text = "Support Weapon", Select = EditSecondaryWeapon },
                    new CMenu.MenuOption() { Text = "Sidekick Left", Select = EditSidekickLeft },
                    new CMenu.MenuOption() { Text = "Sidekick Right", Select = EditSidekickRight },
                    new CMenu.MenuOption() { Text = "* +10000$", Select = EditMoney, Data = 10000 },
                    new CMenu.MenuOption() { Text = "* -10000$", Select = EditMoney, Data = -10000 },
                    new CMenu.MenuOption() { Text = "Done", Select = ReturnToBaseMenu, CancelOption = true, PanelType = CMenu.PanelType.Small },
                }
            };

            //
            // Training
            //
            MenuTrainPilot = new CMenu(game)
            {
                Position = new Vector2(Game.GraphicsDevice.Viewport.Width / 2.0f + 154.0f, 320.0f),
                MenuOptions = new List<CMenu.MenuOption>(),
            };

            string pilot = CSaveData.GetCurrentProfile().Pilot;
            for (int i = 0; i < 3; ++i)
            {
                MenuTrainPilot.MenuOptions.Add(new CMenu.MenuOption() { Text = CAbility.GetAbilityName(pilot, i), Select = TrainAbility, Highlight = HighlightAbility, SelectValidate = ValidateAbilityWithLockedProfile, Data = i, });
            }
            MenuTrainPilot.MenuOptions.Add(new CMenu.MenuOption()
                                           {
                                               Text = "Done",
                                               Select = ReturnToBaseMenu,
                                               Highlight = CancelHighlightAbility,
                                               CancelOption = true,
                                               PanelType = CMenu.PanelType.Small
                                           });

            //
            // Primary Weapon
            //
            MenuPrimaryWeapon = new CMenu(game)
            {
                Position = new Vector2(Game.GraphicsDevice.Viewport.Width / 2.0f + 154.0f, 320.0f),
                MenuOptions = new List<CMenu.MenuOption>(),
            };
            IEnumerable<string> primary_weapon_parts_own = new List<string>() { CSaveData.GetCurrentProfile().WeaponPrimaryType };
            IEnumerable<string> primary_weapon_parts_all = primary_weapon_parts_own.Concat(CMap.GetMapNodeByStageName(WorkingProfile.CurrentStage).AvailablePrimaryWeaponParts);
            IEnumerable<string> primary_weapon_parts = primary_weapon_parts_all.Distinct();
            foreach (string weapon_part in primary_weapon_parts)
            {
                MenuPrimaryWeapon.MenuOptions.Add(
                    new CMenu.MenuOption()
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
            MenuPrimaryWeapon.MenuOptions.Add(new CMenu.MenuOption()
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
                Position = new Vector2(Game.GraphicsDevice.Viewport.Width / 2.0f + 154.0f, 320.0f),
                MenuOptions = new List<CMenu.MenuOption>(),
            };

            MenuSecondaryWeapon.MenuOptions.Add(new CMenu.MenuOption() { Text = "None", SubText = "Cost: 0", Select = SelectSecondaryWeaponEmpty, Highlight = HighlightSecondaryWeapon, Data = "" });
            IEnumerable<string> secondary_weapon_parts_own = new List<string>() { CSaveData.GetCurrentProfile().WeaponSecondaryType };
            IEnumerable<string> secondary_weapon_parts_all = secondary_weapon_parts_own.Concat(CMap.GetMapNodeByStageName(WorkingProfile.CurrentStage).AvailableSecondaryWeaponParts);
            IEnumerable<string> secondary_weapon_parts = secondary_weapon_parts_all.Distinct();
            foreach (string weapon_part in secondary_weapon_parts)
            {
                if (weapon_part == "")
                    continue;

                MenuSecondaryWeapon.MenuOptions.Add(
                    new CMenu.MenuOption()
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
            }
            MenuSecondaryWeapon.MenuOptions.Add(new CMenu.MenuOption()
                                                {
                                                    Text = "Done",
                                                    Select = ReturnToUpgradeShip,
                                                    Highlight = RevertWorkingProfile,
                                                    CancelOption = true,
                                                    PanelType = CMenu.PanelType.Small
                                                });

            //
            // Sidekick Left
            //
            MenuSidekickLeft = new CMenu(game)
            {
                Position = new Vector2(Game.GraphicsDevice.Viewport.Width / 2.0f + 154.0f, 320.0f),
                MenuOptions = new List<CMenu.MenuOption>(),
            };

            MenuSidekickLeft.MenuOptions.Add(new CMenu.MenuOption() { Text = "None", SubText = "Cost: 0", Select = SelectSidekickLeftEmpty, Highlight = HighlightSidekickLeft, Data = "" });
            IEnumerable<string> sidekick_left_weapon_parts_own = new List<string>() { CSaveData.GetCurrentProfile().WeaponSidekickLeftType };
            IEnumerable<string> sidekick_left_weapon_parts_all = sidekick_left_weapon_parts_own.Concat(CMap.GetMapNodeByStageName(WorkingProfile.CurrentStage).AvailableSidekickWeaponParts);
            IEnumerable<string> sidekick_left_weapon_parts = sidekick_left_weapon_parts_all.Distinct();
            foreach (string weapon_part in sidekick_left_weapon_parts)
            {
                if (weapon_part == "")
                    continue;

                MenuSidekickLeft.MenuOptions.Add(
                    new CMenu.MenuOption()
                    {
                        Text = weapon_part,
                        SubText = "Cost: " + CWeaponFactory.GetPriceForLevel(weapon_part, 0),
                        Data = weapon_part,
                        Select = SelectSidekickLeft,
                        SelectValidate = SelectValidateSidekickLeft,
                        Highlight = HighlightSidekickLeft,
                    }
                );
            }

            MenuSidekickLeft.MenuOptions.Add(new CMenu.MenuOption()
                                             {
                                                 Text = "Done",
                                                 Select = ReturnToUpgradeShip,
                                                 Highlight = RevertWorkingProfile,
                                                 CancelOption = true,
                                                 PanelType = CMenu.PanelType.Small
                                             });

            //
            // Sidekick Right
            //
            MenuSidekickRight = new CMenu(game)
            {
                Position = new Vector2(Game.GraphicsDevice.Viewport.Width / 2.0f + 154.0f, 320.0f),
                MenuOptions = new List<CMenu.MenuOption>(),
            };

            MenuSidekickRight.MenuOptions.Add(new CMenu.MenuOption() { Text = "None", SubText = "Cost: 0", Select = SelectSidekickRightEmpty, Highlight = HighlightSidekickRight, Data = "" });
            IEnumerable<string> sidekick_right_weapon_parts_own = new List<string>() { CSaveData.GetCurrentProfile().WeaponSidekickRightType };
            IEnumerable<string> sidekick_right_weapon_parts_all = sidekick_right_weapon_parts_own.Concat(CMap.GetMapNodeByStageName(WorkingProfile.CurrentStage).AvailableSidekickWeaponParts);
            IEnumerable<string> sidekick_right_weapon_parts = sidekick_right_weapon_parts_all.Distinct();
            foreach (string weapon_part in sidekick_right_weapon_parts)
            {
                if (weapon_part == "")
                    continue;

                MenuSidekickRight.MenuOptions.Add(
                    new CMenu.MenuOption()
                    {
                        Text = weapon_part,
                        SubText = "Cost: " + CWeaponFactory.GetPriceForLevel(weapon_part, 0),
                        Data = weapon_part,
                        Select = SelectSidekickRight,
                        SelectValidate = SelectValidateSidekickRight,
                        Highlight = HighlightSidekickRight,
                    }
                );
            }
            MenuSidekickRight.MenuOptions.Add(new CMenu.MenuOption() { Text = "Done", Select = ReturnToUpgradeShip, Highlight = RevertWorkingProfile, CancelOption = true, PanelType = CMenu.PanelType.Small });

            //
            // Chassis
            //
            MenuChassis = new CMenu(game)
            {
                Position = new Vector2(Game.GraphicsDevice.Viewport.Width / 2.0f + 154.0f, 320.0f),
                MenuOptions = new List<CMenu.MenuOption>(),
            };

            IEnumerable<string> chassis_parts_own = new List<string>() { CSaveData.GetCurrentProfile().ChassisType };
            IEnumerable<string> chassis_parts_all = chassis_parts_own.Concat(CMap.GetMapNodeByStageName(WorkingProfile.CurrentStage).AvailableChassisParts);
            IEnumerable<string> chassis_parts = chassis_parts_all.Distinct();
            foreach (string chassis_part in chassis_parts)
            {
                MenuChassis.MenuOptions.Add(
                    new CMenu.MenuOption()
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
            MenuChassis.MenuOptions.Add(new CMenu.MenuOption() { Text = "Done", Select = ReturnToUpgradeShip, Highlight = RevertWorkingProfile, CancelOption = true, PanelType = CMenu.PanelType.Small });

            //
            // Generator
            //
            MenuGenerator = new CMenu(game)
            {
                Position = new Vector2(Game.GraphicsDevice.Viewport.Width / 2.0f + 154.0f, 320.0f),
                MenuOptions = new List<CMenu.MenuOption>(),
            };

            IEnumerable<string> generator_parts_own = new List<string>() { CSaveData.GetCurrentProfile().GeneratorType };
            IEnumerable<string> generator_parts_all = generator_parts_own.Concat(CMap.GetMapNodeByStageName(WorkingProfile.CurrentStage).AvailableGeneratorParts);
            IEnumerable<string> generator_parts = generator_parts_all.Distinct();
            foreach (string generator_part in generator_parts)
            {

                MenuGenerator.MenuOptions.Add(
                    new CMenu.MenuOption()
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
            MenuGenerator.MenuOptions.Add(new CMenu.MenuOption() { Text = "Done", Select = ReturnToUpgradeShip, Highlight = RevertWorkingProfile, CancelOption = true, PanelType = CMenu.PanelType.Small });

            //
            // Shield
            //
            MenuShield = new CMenu(game)
            {
                Position = new Vector2(Game.GraphicsDevice.Viewport.Width / 2.0f + 154.0f, 320.0f),
                MenuOptions = new List<CMenu.MenuOption>(),
            };

            IEnumerable<string> shield_parts_own = new List<string>() { CSaveData.GetCurrentProfile().ShieldType };
            IEnumerable<string> shield_parts_all = shield_parts_own.Concat(CMap.GetMapNodeByStageName(WorkingProfile.CurrentStage).AvailableShieldParts);
            IEnumerable<string> shield_parts = shield_parts_all.Distinct();
            foreach (string shield_part in shield_parts)
            {
                MenuShield.MenuOptions.Add(
                    new CMenu.MenuOption()
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
            MenuShield.MenuOptions.Add(new CMenu.MenuOption() { Text = "Done", Select = ReturnToUpgradeShip, Highlight = RevertWorkingProfile, CancelOption = true, PanelType = CMenu.PanelType.Small });


            Menu = MenuBase;
            DrawMenuErrata = DrawMenuBaseErrata;

            EmptyWorld.GameCamera.Position = new Vector3(0.0f, 0.0f, 0.0f);

            if (!Game.EditorMode)
                CAudio.PlayMusic("Title");

            RefreshSampleDisplay();
        }

        public override void Update()
        {
            MenuUpdateHighlights();
            Menu.Update();
            EmptyWorld.Huds[0].MoneyOverride = (int)LockedProfile.Money;
            EmptyWorld.Huds[0].Update(SampleShip);
            EmptyWorld.UpdateEntities();
            EmptyWorld.BackgroundScenery.Update();
            EmptyWorld.ForegroundScenery.Update();
            EmptyWorld.ParticleEffects.Update();
            SampleShip.UpdateGenerator();
            SampleShip.FireAllWeapons();
            SampleShip.UpdateWeapons();
        }

        public override void Draw()
        {
            Game.GraphicsDevice.Clear(Color.Black);
            EmptyWorld.DrawBackground(EmptyWorld.GameCamera);

            EmptyWorld.DrawEntities(EmptyWorld.GameCamera);

            Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.FrontToBack, SaveStateMode.None, EmptyWorld.GameCamera.WorldMatrix);
            SampleShip.Draw(Game.DefaultSpriteBatch);
            Game.DefaultSpriteBatch.End();

            Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None, EmptyWorld.GameCamera.WorldMatrix);
            EmptyWorld.ParticleEffects.Draw(Game.DefaultSpriteBatch);
            Game.DefaultSpriteBatch.End();

            EmptyWorld.DrawHuds(EmptyWorld.GameCamera);

            Game.DefaultSpriteBatch.Begin();
            Game.DefaultSpriteBatch.Draw(ShopPanelTexture, new Vector2(928.0f, 0.0f), Color.White);
            Menu.Draw(Game.DefaultSpriteBatch);
            //DrawShipStats();
            DrawMoney();
            DrawMenuErrata();
            Game.DefaultSpriteBatch.End();
        }

        private void MenuUpdateHighlights()
        {
            if (Menu == MenuPrimaryWeapon)
                foreach (CMenu.MenuOption option in MenuPrimaryWeapon.MenuOptions)
                    option.SpecialHighlight = (string) option.Data == LockedProfile.WeaponPrimaryType;
            if (Menu == MenuSecondaryWeapon)
                foreach (CMenu.MenuOption option in MenuSecondaryWeapon.MenuOptions)
                    option.SpecialHighlight = (string) option.Data == LockedProfile.WeaponSecondaryType;
            if (Menu == MenuSidekickLeft)
                foreach (CMenu.MenuOption option in MenuSidekickLeft.MenuOptions)
                    option.SpecialHighlight = (string) option.Data == LockedProfile.WeaponSidekickLeftType;
            if (Menu == MenuSidekickRight)
                foreach (CMenu.MenuOption option in MenuSidekickRight.MenuOptions)
                    option.SpecialHighlight = (string) option.Data == LockedProfile.WeaponSidekickRightType;
            if (Menu == MenuChassis)
                foreach (CMenu.MenuOption option in MenuChassis.MenuOptions)
                    option.SpecialHighlight = (string) option.Data == LockedProfile.ChassisType;
            if (Menu == MenuGenerator)
                foreach (CMenu.MenuOption option in MenuGenerator.MenuOptions)
                    option.SpecialHighlight = (string) option.Data == LockedProfile.GeneratorType;
            if (Menu == MenuShield)
                foreach (CMenu.MenuOption option in MenuShield.MenuOptions)
                    option.SpecialHighlight = (string) option.Data == LockedProfile.ShieldType;
            if (Menu == MenuTrainPilot)
                foreach (CMenu.MenuOption option in MenuTrainPilot.MenuOptions)
                    if (option.Data != null)
                        option.SpecialHighlight = HasAbilityWithLockedProfile(option.Data);
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
                LockedProfile.WeaponSidekickLeftType,
                LockedProfile.WeaponSidekickRightType
            };

            foreach (int index in Enumerable.Range(0, keys.Length))
            {
                Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, keys[index], KeysBase + Step * index, Color.White);
            }

            foreach (int index in Enumerable.Range(0, values.Length))
            {
                if (values[index] == "")
                    Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, "None", ValuesBase + Step * index, Color.White);
                else
                    Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, values[index], ValuesBase + Step * index, Color.White);
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

            Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, WorkingProfile.Money.ToString(), EmptyWorld.Huds[0].MoneyTextPosition + new Vector2(0.0f, -48.0f), color, 0.0f, Vector2.Zero, 1.5f, SpriteEffects.None, CLayers.UI+ CLayers.SubLayerIncrement);
        }

        private void RefreshSampleDisplay()
        {
            EmptyWorld.Stop();
            SampleShip = CShipFactory.GenerateShip(EmptyWorld, WorkingProfile, PlayerIndex.One);
            SampleShip.Physics.PositionPhysics.Position = new Vector2(-190.0f, 100.0f);
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

        private void DrawMenuBaseErrata()
        {
            Vector2 blank_position = new Vector2(Game.GraphicsDevice.Viewport.Width / 2.0f - 442.0f, Game.GraphicsDevice.Viewport.Height - 362.0f);
            Game.DefaultSpriteBatch.Draw(ShopUpgradePanelTexture, blank_position, Color.White);

            // NOTE: this is totally gonna ship like this
            // HACK: bad hack time, fix me up
            // TODO: put stuff in menu itself?
            // TODO: check can upgrade
            // TODO: nicer display of purchasable items
            if (Menu == MenuPrimaryWeapon)
            {
                int max = CWeaponFactory.GetMaxLevel(WorkingProfile.WeaponPrimaryType);
                int level = WorkingProfile.WeaponPrimaryLevel;

                CMenu.MenuOption option = Menu.MenuOptions[Menu.Cursor];
                if (!option.CancelOption && WorkingProfile.WeaponPrimaryType != "")
                {
                    Vector2 position = blank_position;
                    ShopUpgradeBarsVisual.Frame = level;
                    ShopUpgradeBarsVisual.Recache();
                    ShopUpgradeBarsVisual.Draw(Game.DefaultSpriteBatch, position + new Vector2(256.0f, 218.0f), 0.0f);

                    int price = CWeaponFactory.GetPriceForLevel(WorkingProfile.WeaponPrimaryType, WorkingProfile.WeaponPrimaryLevel);
                    int next_price = CWeaponFactory.GetPriceForLevel(WorkingProfile.WeaponPrimaryType, WorkingProfile.WeaponPrimaryLevel + 1);
                    Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, "BASE COST:", CMenu.CenteredText(Game, position + new Vector2(0.0f, 0.0f), new Vector2(512.0f, 64.0f), "BASE COST:"), Color.White);
                    Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, price.ToString(), CMenu.CenteredText(Game, position + new Vector2(0.0f, 30.0f), new Vector2(512.0f, 64.0f), price.ToString()), Color.White);

                    Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, "NEXT UPGRADE:", CMenu.CenteredText(Game, position + new Vector2(0.0f, 62.0f), new Vector2(512.0f, 64.0f), "NEXT UPGRADE:"), Color.White);
                    if (level < max)
                        Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, next_price.ToString(), CMenu.CenteredText(Game, position + new Vector2(0.0f, 90.0f), new Vector2(512.0f, 64.0f), next_price.ToString()), Color.White);
                    else
                        Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, "-----", CMenu.CenteredText(Game, position + new Vector2(0.0f, 90.0f), new Vector2(512.0f, 64.0f), "-----"), Color.White);
                }
            }
            else if (Menu == MenuSecondaryWeapon)
            {
                int max = CWeaponFactory.GetMaxLevel(WorkingProfile.WeaponSecondaryType);
                int level = WorkingProfile.WeaponSecondaryLevel;

                CMenu.MenuOption option = Menu.MenuOptions[Menu.Cursor];
                if (!option.CancelOption && WorkingProfile.WeaponSecondaryType != "")
                {
                    Vector2 position = blank_position;
                    ShopUpgradeBarsVisual.Frame = level;
                    ShopUpgradeBarsVisual.Recache();
                    ShopUpgradeBarsVisual.Draw(Game.DefaultSpriteBatch, position + new Vector2(256.0f, 218.0f), 0.0f);

                    int price = CWeaponFactory.GetPriceForLevel(WorkingProfile.WeaponSecondaryType, WorkingProfile.WeaponSecondaryLevel);
                    int next_price = CWeaponFactory.GetPriceForLevel(WorkingProfile.WeaponSecondaryType, WorkingProfile.WeaponSecondaryLevel + 1);
                    Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, "BASE COST:", CMenu.CenteredText(Game, position + new Vector2(0.0f, 0.0f), new Vector2(512.0f, 64.0f), "BASE COST:"), Color.White);
                    Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, price.ToString(), CMenu.CenteredText(Game, position + new Vector2(0.0f, 30.0f), new Vector2(512.0f, 64.0f), price.ToString()), Color.White);

                    Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, "NEXT UPGRADE:", CMenu.CenteredText(Game, position + new Vector2(0.0f, 62.0f), new Vector2(512.0f, 64.0f), "NEXT UPGRADE:"), Color.White);
                    if (level < max)
                        Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, next_price.ToString(), CMenu.CenteredText(Game, position + new Vector2(0.0f, 90.0f), new Vector2(512.0f, 64.0f), next_price.ToString()), Color.White);
                    else
                        Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, "-----", CMenu.CenteredText(Game, position + new Vector2(0.0f, 90.0f), new Vector2(512.0f, 64.0f), "-----"), Color.White);
                }
            }
            else if (Menu == MenuSidekickLeft)
            {
                CMenu.MenuOption option = Menu.MenuOptions[Menu.Cursor];
                if (!option.CancelOption && WorkingProfile.WeaponSidekickLeftType != "")
                {
                    Vector2 position = blank_position;
                    int price = CWeaponFactory.GetPriceForLevel(WorkingProfile.WeaponSidekickLeftType, WorkingProfile.WeaponSidekickLeftLevel);
                    Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, "BASE COST:", CMenu.CenteredText(Game, position + new Vector2(0.0f, 30.0f), new Vector2(512.0f, 64.0f), "BASE COST:"), Color.White);
                    Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, price.ToString(), CMenu.CenteredText(Game, position + new Vector2(0.0f, 60.0f), new Vector2(512.0f, 64.0f), price.ToString()), Color.White);
                }
            }
            else if (Menu == MenuSidekickRight)
            {
                CMenu.MenuOption option = Menu.MenuOptions[Menu.Cursor];
                if (!option.CancelOption && WorkingProfile.WeaponSidekickRightType != "")
                {
                    Vector2 position = blank_position;
                    int price = CWeaponFactory.GetPriceForLevel(WorkingProfile.WeaponSidekickRightType, WorkingProfile.WeaponSidekickRightLevel);
                    Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, "BASE COST:", CMenu.CenteredText(Game, position + new Vector2(0.0f, 30.0f), new Vector2(512.0f, 64.0f), "BASE COST:"), Color.White);
                    Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, price.ToString(), CMenu.CenteredText(Game, position + new Vector2(0.0f, 60.0f), new Vector2(512.0f, 64.0f), price.ToString()), Color.White);
                }
            }
            else if (Menu == MenuChassis)
            {
                CChassisPart part = ChassisDefinitions.GetPart(WorkingProfile.ChassisType);
                CMenu.MenuOption option = Menu.MenuOptions[Menu.Cursor];
                if (!option.CancelOption)
                {
                    Vector2 position = blank_position + new Vector2(0.0f, 40.0f);
                    string armor = Convert.ToInt32(part.Armor * 100.0f).ToString();
                    string speed = Convert.ToInt32(part.Speed * 100.0f).ToString();
                    Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, "ARMOR:", CMenu.CenteredText(Game, position + new Vector2(0.0f, 10.0f), new Vector2(512.0f, 64.0f), "ARMOR:"), Color.White);
                    Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, armor, CMenu.CenteredText(Game, position + new Vector2(0.0f, 40.0f), new Vector2(512.0f, 64.0f), armor), Color.White);
                    Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, "SPEED:", CMenu.CenteredText(Game, position + new Vector2(0.0f, 80.0f), new Vector2(512.0f, 64.0f), "SPEED:"), Color.White);
                    Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, speed, CMenu.CenteredText(Game, position + new Vector2(0.0f, 110.0f), new Vector2(512.0f, 64.0f), speed), Color.White);
                }
            }
            else if (Menu == MenuGenerator)
            {
                CGeneratorPart part = GeneratorDefinitions.GetPart(WorkingProfile.GeneratorType);
                CMenu.MenuOption option = Menu.MenuOptions[Menu.Cursor];
                if (!option.CancelOption)
                {
                    Vector2 position = blank_position + new Vector2(0.0f, 40.0f);
                    string energy = Convert.ToInt32(part.Energy * 100.0f).ToString();
                    string regen = Convert.ToInt32(part.Regen * 100.0f).ToString();
                    Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, "ENERGY:", CMenu.CenteredText(Game, position + new Vector2(0.0f, 10.0f), new Vector2(512.0f, 64.0f), "ENERGY:"), Color.White);
                    Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, energy, CMenu.CenteredText(Game, position + new Vector2(0.0f, 40.0f), new Vector2(512.0f, 64.0f), energy), Color.White);
                    Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, "REGEN:", CMenu.CenteredText(Game, position + new Vector2(0.0f, 80.0f), new Vector2(512.0f, 64.0f), "REGEN:"), Color.White);
                    Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, regen, CMenu.CenteredText(Game, position + new Vector2(0.0f, 110.0f), new Vector2(512.0f, 64.0f), regen), Color.White);
                }
            }
            else if (Menu == MenuShield)
            {
                CShieldPart part = ShieldDefinitions.GetPart(WorkingProfile.ShieldType);
                CMenu.MenuOption option = Menu.MenuOptions[Menu.Cursor];
                if (!option.CancelOption)
                {
                    Vector2 position = blank_position + new Vector2(0.0f, 40.0f);
                    string energy = Convert.ToInt32(part.Shield * 100.0f).ToString();
                    string regen = Convert.ToInt32(part.Regen * 100.0f).ToString();
                    Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, "ENERGY:", CMenu.CenteredText(Game, position + new Vector2(0.0f, 10.0f), new Vector2(512.0f, 64.0f), "ENERGY:"), Color.White);
                    Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, energy, CMenu.CenteredText(Game, position + new Vector2(0.0f, 40.0f), new Vector2(512.0f, 64.0f), energy), Color.White);
                    Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, "REGEN:", CMenu.CenteredText(Game, position + new Vector2(0.0f, 80.0f), new Vector2(512.0f, 64.0f), "REGEN:"), Color.White);
                    Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, regen, CMenu.CenteredText(Game, position + new Vector2(0.0f, 110.0f), new Vector2(512.0f, 64.0f), regen), Color.White);
                }
            }
            else
            {
                ShowHelpInfo();
            }
        }

        private void ShowHelpInfo()
        {
            // TODO: less hack? more hack? more betterer hack?
            Vector2 blank_position = new Vector2(Game.GraphicsDevice.Viewport.Width / 2.0f - 442.0f, Game.GraphicsDevice.Viewport.Height - 362.0f);
            string[] strings = {
                "WELCOME TO SHOP",
                "PURCHASE UPGRADES",
                "AND TRAINING HERE",
            };

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
                strings[1] = "TRAINING IS";
                strings[2] = "NON-REFUNDABLE!";
            }

            Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, strings[0], CMenu.CenteredText(Game, blank_position + new Vector2(0.0f, 40.0f), new Vector2(512.0f, 64.0f), strings[0]), Color.White);
            Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, strings[1], CMenu.CenteredText(Game, blank_position + new Vector2(0.0f, 120.0f), new Vector2(512.0f, 64.0f), strings[1]), Color.White);
            Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, strings[2], CMenu.CenteredText(Game, blank_position + new Vector2(0.0f, 150.0f), new Vector2(512.0f, 64.0f), strings[2]), Color.White);
        }

        private void StageSelect(object tag)
        {
            CSaveData.SetCurrentProfileData(LockedProfile);
            CSaveData.Save();
            Game.State = new CStateFadeTo(Game, this, new CStateStageSelect(Game));
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

        private void EditSidekickLeft(object tag)
        {
            Menu = MenuSidekickLeft;
            Menu.ForceRefresh();
        }

        private void EditSidekickRight(object tag)
        {
            Menu = MenuSidekickRight;
            Menu.ForceRefresh();
        }

        private void EditMoney(object tag)
        {
            WorkingProfile.Money += (int)tag;
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
            CSaveData.SetCurrentProfileData(LockedProfile);
            CSaveData.Save();
            Game.State = new CStateFadeTo(Game, this, new CStateMainMenu(Game));
        }

        private void SelectPrimaryWeapon(object tag)
        {
            LockWorkingProfile();
            RefreshSampleDisplay();
        }

        private bool SelectValidatePrimaryWeapon(object tag)
        {
            if (WorkingProfile.WeaponPrimaryType == (string)tag)
                return true;

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
                if (remaining < 0)
                    return;

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
            int sell = CWeaponFactory.GetTotalPriceForLevel(LockedProfile.WeaponSecondaryType, LockedProfile.WeaponSecondaryLevel);
            LockedProfile.Money += sell;
            LockedProfile.WeaponSecondaryType = "";
            LockedProfile.WeaponSecondaryLevel = 0;
            RevertWorkingProfile(null);
            RefreshSampleDisplay();
        }

        private void SelectSecondaryWeapon(object tag)
        {
            LockWorkingProfile();
            RefreshSampleDisplay();
        }

        private bool SelectValidateSecondaryWeapon(object tag)
        {
            if (WorkingProfile.WeaponSecondaryType == (string)tag)
                return true;

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
                if (remaining < 0)
                    return;

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

        private void SelectSidekickLeft(object tag)
        {
            LockWorkingProfile();
            RefreshSampleDisplay();
        }

        private void SelectSidekickLeftEmpty(object tag)
        {
            int sell = CWeaponFactory.GetTotalPriceForLevel(LockedProfile.WeaponSidekickLeftType, LockedProfile.WeaponSidekickLeftLevel);
            LockedProfile.Money += sell;
            LockedProfile.WeaponSidekickLeftType = "";
            LockedProfile.WeaponSidekickLeftLevel = 0;
            RevertWorkingProfile(null);
            RefreshSampleDisplay();
        }

        private bool SelectValidateSidekickLeft(object tag)
        {
            if (WorkingProfile.WeaponSidekickLeftType == (string)tag)
                return true;

            int sell = CWeaponFactory.GetTotalPriceForLevel(WorkingProfile.WeaponSidekickLeftType, WorkingProfile.WeaponSidekickLeftLevel);
            int buy = CWeaponFactory.GetPriceForLevel((string)tag, 0);
            if (buy > WorkingProfile.Money + sell)
                return false;

            return true;
        }

        private void HighlightSidekickLeft(object tag)
        {
            if (WorkingProfile.WeaponSidekickLeftType != (string)tag)
            {
                int level = 0;
                if (LockedProfile.WeaponSidekickLeftType == (string)tag)
                    level = LockedProfile.WeaponSidekickLeftLevel;

                int sell = CWeaponFactory.GetTotalPriceForLevel(WorkingProfile.WeaponSidekickLeftType, WorkingProfile.WeaponSidekickLeftLevel);
                int buy = CWeaponFactory.GetTotalPriceForLevel((string)tag, level);
                int remaining = WorkingProfile.Money + sell - buy;
                if (remaining < 0)
                    return;

                WorkingProfile.Money += sell;
                WorkingProfile.WeaponSidekickLeftType = (string)tag;
                WorkingProfile.WeaponSidekickLeftLevel = level;
                WorkingProfile.Money -= buy;

                RefreshSampleDisplay();
            }
        }

        private void HighlightSidekickLeftEmpty(object tag)
        {
            if (LockedProfile.WeaponSidekickLeftType == "")
            {
                int sell = CWeaponFactory.GetTotalPriceForLevel(WorkingProfile.WeaponSidekickLeftType, WorkingProfile.WeaponSidekickLeftLevel);

                WorkingProfile.Money += sell;
                WorkingProfile.WeaponSidekickLeftType = (string)tag;
                WorkingProfile.WeaponSidekickLeftLevel = 0;

                RefreshSampleDisplay();
            }
        }

        private void SelectSidekickRight(object tag)
        {
            LockWorkingProfile();
            RefreshSampleDisplay();
        }

        private void SelectSidekickRightEmpty(object tag)
        {
            int sell = CWeaponFactory.GetTotalPriceForLevel(LockedProfile.WeaponSidekickRightType, LockedProfile.WeaponSidekickRightLevel);
            LockedProfile.Money += sell;
            LockedProfile.WeaponSidekickRightType = "";
            LockedProfile.WeaponSidekickRightLevel = 0;
            RevertWorkingProfile(null);
            RefreshSampleDisplay();
        }

        private bool SelectValidateSidekickRight(object tag)
        {
            if (WorkingProfile.WeaponSidekickRightType == (string)tag)
                return true;

            int sell = CWeaponFactory.GetTotalPriceForLevel(WorkingProfile.WeaponSidekickRightType, WorkingProfile.WeaponSidekickRightLevel);
            int buy = CWeaponFactory.GetPriceForLevel((string)tag, 0);
            if (buy > WorkingProfile.Money + sell)
                return false;

            return true;
        }

        private void HighlightSidekickRight(object tag)
        {
            if (WorkingProfile.WeaponSidekickRightType != (string)tag)
            {
                int level = 0;
                if (LockedProfile.WeaponSidekickRightType == (string)tag)
                    level = LockedProfile.WeaponSidekickRightLevel;

                int sell = CWeaponFactory.GetTotalPriceForLevel(WorkingProfile.WeaponSidekickRightType, WorkingProfile.WeaponSidekickRightLevel);
                int buy = CWeaponFactory.GetTotalPriceForLevel((string)tag, level);
                int remaining = WorkingProfile.Money + sell - buy;
                if (remaining < 0)
                    return;

                WorkingProfile.Money += sell;
                WorkingProfile.WeaponSidekickRightType = (string)tag;
                WorkingProfile.WeaponSidekickRightLevel = level;
                WorkingProfile.Money -= buy;

                RefreshSampleDisplay();
            }
        }

        private void HighlightSidekickRightEmpty(object tag)
        {
            if (LockedProfile.WeaponSidekickRightType == "")
            {
                int sell = CWeaponFactory.GetTotalPriceForLevel(WorkingProfile.WeaponSidekickRightType, WorkingProfile.WeaponSidekickRightLevel);

                WorkingProfile.Money += sell;
                WorkingProfile.WeaponSidekickRightType = (string)tag;
                WorkingProfile.WeaponSidekickRightLevel = 0;

                RefreshSampleDisplay();
            }
        }

        private void SelectChassis(object tag)
        {
            LockWorkingProfile();
            RefreshSampleDisplay();
        }

        private bool SelectValidateChassis(object tag)
        {
            if (WorkingProfile.ChassisType == (string)tag)
                return true;

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
                if (remaining < 0)
                    return;

                WorkingProfile.Money += sell;
                WorkingProfile.ChassisType = (string)tag;
                WorkingProfile.Money -= buy;

                RefreshSampleDisplay();
            }
        }

        private void SelectGenerator(object tag)
        {
            LockWorkingProfile();
            RefreshSampleDisplay();
        }

        private bool SelectValidateGenerator(object tag)
        {
            if (WorkingProfile.GeneratorType == (string)tag)
                return true;

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
                if (remaining < 0)
                    return;

                WorkingProfile.Money += sell;
                WorkingProfile.GeneratorType = (string)tag;
                WorkingProfile.Money -= buy;

                RefreshSampleDisplay();
            }
        }

        private void SelectShield(object tag)
        {
            LockWorkingProfile();
            RefreshSampleDisplay();
        }

        private bool SelectValidateShield(object tag)
        {
            if (WorkingProfile.ShieldType == (string)tag)
                return true;

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
                if (remaining < 0)
                    return;

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

        private void ReturnToUpgradeShip(object tag)
        {
            Menu = MenuUpgradeShip;
        }

        private void TrainAbility(object tag)
        {
            string ability_name = string.Format("Ability{0}", tag.ToString());
            FieldInfo field = typeof(SProfile).GetField(ability_name);
            bool has_ability = (bool)field.GetValue(CSaveData.GetCurrentProfile());
            if (has_ability)
                return;

            // TODO: price cleanup
            int price = 10000 + (int)tag * 5000;
            if (LockedProfile.Money < price)
                return;

            LockedProfile.Money -= price;
            object reference = (object)LockedProfile;
            field.SetValue(reference, true);
            LockedProfile = (SProfile)reference;

            RevertWorkingProfile(null);
            MenuUpdateHighlights();
        }

        private void HighlightAbility(object tag)
        {
            string ability_name = string.Format("Ability{0}", tag.ToString());
            FieldInfo field = typeof(SProfile).GetField(ability_name);
            bool has_ability = (bool)field.GetValue(LockedProfile);
            if (has_ability)
                return;

            int price = 10000 + (int)tag * 5000;
            WorkingProfile.Money = LockedProfile.Money - price;
        }

        private void CancelHighlightAbility(object tag)
        {
            RevertWorkingProfile(null);
            MenuUpdateHighlights();
        }

        private bool ValidateAbility(object tag, SProfile profile)
        {
            string ability_name = string.Format("Ability{0}", tag.ToString());
            FieldInfo field = typeof(SProfile).GetField(ability_name);
            bool has_ability = (bool)field.GetValue(LockedProfile);
            if (has_ability)
                return true;

            // TODO: price cleanup
            int price = 10000 + (int)tag * 5000;
            if (profile.Money < price)
                return false;

            return true;
        }

        private bool ValidateAbilityWithLockedProfile(object tag)
        {
            return ValidateAbility(tag, LockedProfile);
        }

        private bool HasAbilityWithLockedProfile(object tag)
        {
            string ability_name = string.Format("Ability{0}", tag.ToString());
            FieldInfo field = typeof(SProfile).GetField(ability_name);
            bool has_ability = (bool)field.GetValue(LockedProfile);
            return has_ability;
        }
    }
}
