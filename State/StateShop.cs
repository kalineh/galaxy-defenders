//
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
        private CMenu MenuSidekickLeft { get; set; }
        private CMenu MenuSidekickRight { get; set; }
        private CMenu MenuChassis { get; set; }
        private CMenu MenuGenerator { get; set; }
        private CMenu MenuShield { get; set; }
        private CMenu MenuTrainPilot { get; set; }
        private delegate void DrawMenuErrataFunction();
        private DrawMenuErrataFunction DrawMenuErrata { get; set; }
        private CShip SampleShip { get; set; }
        private SProfilePilotState WorkingProfile;
        private SProfilePilotState LockedProfile;
        private Texture2D ShopPanelTexture { get; set; }
        private Texture2D ShopUpgradePanelTexture { get; set; }
        private CVisual ShopUpgradeBarsVisual { get; set; }
        private GameControllerIndex ShoppingPlayer { get; set; }

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
            public CTextLabel ShieldHeader;
            public CTextLabel ShieldValue;
            public CTextLabel RegenHeader;
            public CTextLabel RegenValue;
            public CTextLabel EnergyHeader;
            public CTextLabel EnergyValue;

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
                ShieldHeader = new CTextLabel();
                ShieldValue = new CTextLabel();
                RegenHeader = new CTextLabel();
                RegenValue = new CTextLabel();
                EnergyHeader = new CTextLabel();
                EnergyValue = new CTextLabel();
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

            Labels = new SLabels(null);

            //
            // Base
            //
            MenuBase = new CMenu(game)
            {
                Position = new Vector2(Game.Resolution.X / 2.0f + 154.0f, 320.0f),
                MenuOptions = new List<CMenu.CMenuOption>()
                {
                    new CMenu.CMenuOption() { Text = "Play Next Stage", Select = StageSelect },
                    new CMenu.CMenuOption() { Text = "Upgrade Ship", Select = UpgradeShip },
                    new CMenu.CMenuOption() { Text = "Train Pilot", Select = TrainPilot },
                    new CMenu.CMenuOption() { Text = "Back", Select = Back, CancelOption = true, PanelType = CMenu.PanelType.Small },
                }
            };

            //
            // Upgrade Ship
            //
            MenuUpgradeShip = new CMenu(game)
            {
                Position = new Vector2(Game.Resolution.X / 2.0f + 154.0f, 320.0f),
                MenuOptions = new List<CMenu.CMenuOption>()
                {
                    new CMenu.CMenuOption() { Text = "Chassis", Select = EditChassis },
                    new CMenu.CMenuOption() { Text = "Generator", Select = EditGenerator },
                    new CMenu.CMenuOption() { Text = "Shield", Select = EditShield },
                    new CMenu.CMenuOption() { Text = "Main Weapon", Select = EditPrimaryWeapon },
                    new CMenu.CMenuOption() { Text = "Support Weapon", Select = EditSecondaryWeapon },
                    new CMenu.CMenuOption() { Text = "Sidekick Left", Select = EditSidekickLeft },
                    new CMenu.CMenuOption() { Text = "Sidekick Right", Select = EditSidekickRight },
                    new CMenu.CMenuOption() { Text = "* +10000$", Select = EditMoney, Data = 10000 },
                    new CMenu.CMenuOption() { Text = "* -10000$", Select = EditMoney, Data = -10000 },
                    new CMenu.CMenuOption() { Text = "Done", Select = ReturnToBaseMenu, CancelOption = true, PanelType = CMenu.PanelType.Small },
                }
            };

            //
            // Training
            //
            MenuTrainPilot = new CMenu(game)
            {
                Position = new Vector2(Game.Resolution.X / 2.0f + 154.0f, 320.0f),
                MenuOptions = new List<CMenu.CMenuOption>(),
            };

            string pilot = GetShoppingPilotData().Pilot;
            for (int i = 0; i < 3; ++i)
            {
                MenuTrainPilot.MenuOptions.Add(new CMenu.CMenuOption() { Text = CAbility.GetAbilityName(pilot, i), Select = TrainAbility, Highlight = HighlightAbility, SelectValidate = ValidateAbilityWithLockedProfile, Data = i, });
            }
            MenuTrainPilot.MenuOptions.Add(new CMenu.CMenuOption()
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
                Position = new Vector2(Game.Resolution.X / 2.0f + 154.0f, 320.0f),
                MenuOptions = new List<CMenu.CMenuOption>(),
            };

            IEnumerable<string> primary_weapon_parts_own = new List<string>() { GetShoppingPilotData().WeaponPrimaryType };
            IEnumerable<string> primary_weapon_parts_all = primary_weapon_parts_own.Concat(CMap.GetMapNodeByStageName(CSaveData.GetCurrentGameData(Game).Stage).AvailablePrimaryWeaponParts);
            IEnumerable<string> primary_weapon_parts = primary_weapon_parts_all.Distinct();
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
                Position = new Vector2(Game.Resolution.X / 2.0f + 154.0f, 320.0f),
                MenuOptions = new List<CMenu.CMenuOption>(),
            };

            MenuSecondaryWeapon.MenuOptions.Add(new CMenu.CMenuOption() { Text = "None", SubText = "Cost: 0", Select = SelectSecondaryWeaponEmpty, Highlight = HighlightSecondaryWeapon, Data = "" });
            IEnumerable<string> secondary_weapon_parts_own = new List<string>() { GetShoppingPilotData().WeaponSecondaryType };
            IEnumerable<string> secondary_weapon_parts_all = secondary_weapon_parts_own.Concat(CMap.GetMapNodeByStageName(CSaveData.GetCurrentGameData(Game).Stage).AvailableSecondaryWeaponParts);
            IEnumerable<string> secondary_weapon_parts = secondary_weapon_parts_all.Distinct();
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
            // Sidekick Left
            //
            MenuSidekickLeft = new CMenu(game)
            {
                Position = new Vector2(Game.Resolution.X / 2.0f + 154.0f, 320.0f),
                MenuOptions = new List<CMenu.CMenuOption>(),
            };

            MenuSidekickLeft.MenuOptions.Add(new CMenu.CMenuOption() { Text = "None", SubText = "Cost: 0", Select = SelectSidekickLeftEmpty, Highlight = HighlightSidekickLeft, Data = "" });
            IEnumerable<string> sidekick_left_weapon_parts_own = new List<string>() { GetShoppingPilotData().WeaponSidekickLeftType };
            IEnumerable<string> sidekick_left_weapon_parts_all = sidekick_left_weapon_parts_own.Concat(CMap.GetMapNodeByStageName(CSaveData.GetCurrentGameData(Game).Stage).AvailableSidekickWeaponParts);
            IEnumerable<string> sidekick_left_weapon_parts = sidekick_left_weapon_parts_all.Distinct();
            foreach (string weapon_part in sidekick_left_weapon_parts)
            {
                if (weapon_part == "")
                    continue;

                MenuSidekickLeft.MenuOptions.Add(
                    new CMenu.CMenuOption()
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

            MenuSidekickLeft.MenuOptions.Add(new CMenu.CMenuOption()
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
                Position = new Vector2(Game.Resolution.X / 2.0f + 154.0f, 320.0f),
                MenuOptions = new List<CMenu.CMenuOption>(),
            };

            MenuSidekickRight.MenuOptions.Add(new CMenu.CMenuOption() { Text = "None", SubText = "Cost: 0", Select = SelectSidekickRightEmpty, Highlight = HighlightSidekickRight, Data = "" });
            IEnumerable<string> sidekick_right_weapon_parts_own = new List<string>() { GetShoppingPilotData().WeaponSidekickRightType };
            IEnumerable<string> sidekick_right_weapon_parts_all = sidekick_right_weapon_parts_own.Concat(CMap.GetMapNodeByStageName(CSaveData.GetCurrentGameData(Game).Stage).AvailableSidekickWeaponParts);
            IEnumerable<string> sidekick_right_weapon_parts = sidekick_right_weapon_parts_all.Distinct();
            foreach (string weapon_part in sidekick_right_weapon_parts)
            {
                if (weapon_part == "")
                    continue;

                MenuSidekickRight.MenuOptions.Add(
                    new CMenu.CMenuOption()
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
            MenuSidekickRight.MenuOptions.Add(new CMenu.CMenuOption() { Text = "Done", Select = ReturnToUpgradeShip, Highlight = RevertWorkingProfile, CancelOption = true, PanelType = CMenu.PanelType.Small });

            //
            // Chassis
            //
            MenuChassis = new CMenu(game)
            {
                Position = new Vector2(Game.Resolution.X / 2.0f + 154.0f, 320.0f),
                MenuOptions = new List<CMenu.CMenuOption>(),
            };

            IEnumerable<string> chassis_parts_own = new List<string>() { GetShoppingPilotData().ChassisType };
            IEnumerable<string> chassis_parts_all = chassis_parts_own.Concat(CMap.GetMapNodeByStageName(CSaveData.GetCurrentGameData(Game).Stage).AvailableChassisParts);
            IEnumerable<string> chassis_parts = chassis_parts_all.Distinct();
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
                Position = new Vector2(Game.Resolution.X / 2.0f + 154.0f, 320.0f),
                MenuOptions = new List<CMenu.CMenuOption>(),
            };

            IEnumerable<string> generator_parts_own = new List<string>() { GetShoppingPilotData().GeneratorType };
            IEnumerable<string> generator_parts_all = generator_parts_own.Concat(CMap.GetMapNodeByStageName(CSaveData.GetCurrentGameData(Game).Stage).AvailableGeneratorParts);
            IEnumerable<string> generator_parts = generator_parts_all.Distinct();
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
                Position = new Vector2(Game.Resolution.X / 2.0f + 154.0f, 320.0f),
                MenuOptions = new List<CMenu.CMenuOption>(),
            };

            IEnumerable<string> shield_parts_own = new List<string>() { GetShoppingPilotData().ShieldType };
            IEnumerable<string> shield_parts_all = shield_parts_own.Concat(CMap.GetMapNodeByStageName(CSaveData.GetCurrentGameData(Game).Stage).AvailableShieldParts);
            IEnumerable<string> shield_parts = shield_parts_all.Distinct();
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

            Game.HudManager.Huds[0].MoneyOverride = null;
            Game.HudManager.Huds[1].MoneyOverride = null;
        }

        public override void Update()
        {
            if (Game.PlayersInGame > 1)
            {
                if (Game.Input.IsPadL1PressedAny() || Game.Input.IsKeyPressed(Keys.F1))
                    ChangeShoppingPlayer(GameControllerIndex.One);
                if (Game.Input.IsPadR1PressedAny() || Game.Input.IsKeyPressed(Keys.F2))
                    ChangeShoppingPlayer(GameControllerIndex.Two);
            }

            MenuUpdateHighlights();
            Menu.Update();
            Game.HudManager.Huds[(int)ShoppingPlayer].MoneyOverride = (int)LockedProfile.Money;
            Game.HudManager.Huds[(int)ShoppingPlayer].Update();
            EmptyWorld.GameCamera.Update();
            EmptyWorld.UpdateEntities();
            EmptyWorld.BackgroundScenery.Update();
            EmptyWorld.ForegroundScenery.Update();
            EmptyWorld.ParticleEffects.Update();
            SampleShip.UpdateGenerator();
            SampleShip.FirePrimarySecondaryWeapons();
            SampleShip.FireSidekickLeft();
            SampleShip.FireSidekickRight();
            SampleShip.UpdateWeapons();
        }

        public override void Draw()
        {
            EmptyWorld.DrawBackground(EmptyWorld.GameCamera);

            EmptyWorld.DrawEntities(EmptyWorld.GameCamera);

            Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.FrontToBack, SaveStateMode.None, EmptyWorld.GameCamera.WorldMatrix);
            SampleShip.Draw(Game.DefaultSpriteBatch);
            Game.DefaultSpriteBatch.End();

            Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None, EmptyWorld.GameCamera.WorldMatrix);
            EmptyWorld.ParticleEffects.Draw(Game.DefaultSpriteBatch);
            Game.DefaultSpriteBatch.End();

            Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None, Game.RenderScaleMatrix);
            Game.DefaultSpriteBatch.Draw(ShopPanelTexture, new Vector2(928.0f, 0.0f), Color.White);
            Menu.Draw(Game.DefaultSpriteBatch);

            DrawMoney();
            DrawMenuErrata();

            //if (Game.PlayersInGame > 1)
            //{
                //Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, "L1", new Vector2(328.0f, 240.0f), Color.Gray, 0.0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0.0f);
                //Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, "R1", new Vector2(1600.0f, 240.0f), Color.Gray, 0.0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0.0f);
            //}

            Game.DefaultSpriteBatch.End();
        }

        private void MenuUpdateHighlights()
        {
            if (Menu == MenuPrimaryWeapon)
                foreach (CMenu.CMenuOption option in MenuPrimaryWeapon.MenuOptions)
                    option.SpecialHighlight = (string) option.Data == LockedProfile.WeaponPrimaryType;
            if (Menu == MenuSecondaryWeapon)
                foreach (CMenu.CMenuOption option in MenuSecondaryWeapon.MenuOptions)
                    option.SpecialHighlight = (string) option.Data == LockedProfile.WeaponSecondaryType;
            if (Menu == MenuSidekickLeft)
                foreach (CMenu.CMenuOption option in MenuSidekickLeft.MenuOptions)
                    option.SpecialHighlight = (string) option.Data == LockedProfile.WeaponSidekickLeftType;
            if (Menu == MenuSidekickRight)
                foreach (CMenu.CMenuOption option in MenuSidekickRight.MenuOptions)
                    option.SpecialHighlight = (string) option.Data == LockedProfile.WeaponSidekickRightType;
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

            // TODO: money display
            Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, WorkingProfile.Money.ToString(), Game.HudManager.Huds[(int)ShoppingPlayer].MoneyTextPosition + new Vector2(0.0f, -48.0f), color, 0.0f, Vector2.Zero, 1.5f, SpriteEffects.None, CLayers.UI+ CLayers.SubLayerIncrement);
        }

        private void RefreshSampleDisplay()
        {
            // TODO: replace with simpler version (just clear entities and HUD)
            EmptyWorld.Stop();
            SampleShip = CShipFactory.GenerateShip(EmptyWorld, WorkingProfile, ShoppingPlayer);
            SampleShip.Physics.Position = new Vector2(-190.0f, 100.0f);
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
            Vector2 panel_position = new Vector2(Game.Resolution.X / 2.0f - 442.0f, Game.Resolution.Y - 362.0f);
            Vector2 blank_position = new Vector2(Game.Resolution.X / 2.0f - 180.0f, Game.Resolution.Y - 320.0f);
            Game.DefaultSpriteBatch.Draw(ShopUpgradePanelTexture, panel_position, Color.White);

            // NOTE: this is totally gonna ship like this
            // HACK: bad hack time, fix me up
            // TODO: put stuff in menu itself?
            // TODO: check can upgrade
            // TODO: nicer display of purchasable items
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
                    ShopUpgradeBarsVisual.Draw(Game.DefaultSpriteBatch, position + new Vector2(256.0f, 218.0f), 0.0f);

                    int price = CWeaponFactory.GetPriceForLevel(WorkingProfile.WeaponPrimaryType, WorkingProfile.WeaponPrimaryLevel);
                    int next_price = CWeaponFactory.GetPriceForLevel(WorkingProfile.WeaponPrimaryType, WorkingProfile.WeaponPrimaryLevel + 1);

                    Labels.BaseCostPrice.Value = price;
                    Labels.BaseCostHeader.Draw(Game.DefaultSpriteBatch, Game.DefaultFont, position, Color.White);
                    Labels.BaseCostPrice.Draw(Game.DefaultSpriteBatch, Game.DefaultFont, position + new Vector2(0.0f, 30.0f), Color.White);

                    Labels.NextUpgradeHeader.Draw(Game.DefaultSpriteBatch, Game.DefaultFont, position + new Vector2(0.0f, 62.0f), Color.White);
                    if (level < max)
                    {
                        Labels.NextUpgradePrice.Value = next_price;
                        Labels.NextUpgradePrice.Draw(Game.DefaultSpriteBatch, Game.DefaultFont, position + new Vector2(0.0f, 90.0f), Color.White);
                    }
                    else
                    {
                        // TODO: debug me to check that no strings are generated each time
                        Labels.NextUpgradePrice.Value = "MAX";
                        Labels.NextUpgradePrice.Draw(Game.DefaultSpriteBatch, Game.DefaultFont, position + new Vector2(0.0f, 90.0f), Color.White);
                    }
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
                    ShopUpgradeBarsVisual.Draw(Game.DefaultSpriteBatch, position + new Vector2(256.0f, 218.0f), 0.0f);

                    int price = CWeaponFactory.GetPriceForLevel(WorkingProfile.WeaponSecondaryType, WorkingProfile.WeaponSecondaryLevel);
                    int next_price = CWeaponFactory.GetPriceForLevel(WorkingProfile.WeaponSecondaryType, WorkingProfile.WeaponSecondaryLevel + 1);

                    Labels.BaseCostPrice.Value = price;
                    Labels.BaseCostHeader.Draw(Game.DefaultSpriteBatch, Game.DefaultFont, position, Color.White);
                    Labels.BaseCostPrice.Draw(Game.DefaultSpriteBatch, Game.DefaultFont, position + new Vector2(0.0f, 30.0f), Color.White);

                    Labels.NextUpgradeHeader.Draw(Game.DefaultSpriteBatch, Game.DefaultFont, position + new Vector2(0.0f, 62.0f), Color.White);
                    if (level < max)
                    {
                        Labels.NextUpgradePrice.Value = next_price;
                        Labels.NextUpgradePrice.Draw(Game.DefaultSpriteBatch, Game.DefaultFont, position + new Vector2(0.0f, 90.0f), Color.White);
                    }
                    else
                    {
                        // TODO: debug me to check that no strings are generated each time
                        Labels.NextUpgradePrice.Value = "MAX";
                        Labels.NextUpgradePrice.Draw(Game.DefaultSpriteBatch, Game.DefaultFont, position + new Vector2(0.0f, 90.0f), Color.White);
                    }
                }
            }
            else if (Menu == MenuSidekickLeft)
            {
                CMenu.CMenuOption option = Menu.MenuOptions[Menu.Cursor];
                if (!option.CancelOption && WorkingProfile.WeaponSidekickLeftType != "")
                {
                    Vector2 position = blank_position + new Vector2(0.0f, 30.0f);
                    int price = CWeaponFactory.GetPriceForLevel(WorkingProfile.WeaponSidekickLeftType, WorkingProfile.WeaponSidekickLeftLevel);

                    Labels.BaseCostPrice.Value = price;
                    Labels.BaseCostHeader.Draw(Game.DefaultSpriteBatch, Game.DefaultFont, position, Color.White);
                    Labels.BaseCostPrice.Draw(Game.DefaultSpriteBatch, Game.DefaultFont, position + new Vector2(0.0f, 30.0f), Color.White);
                }
            }
            else if (Menu == MenuSidekickRight)
            {
                CMenu.CMenuOption option = Menu.MenuOptions[Menu.Cursor];
                if (!option.CancelOption && WorkingProfile.WeaponSidekickRightType != "")
                {
                    Vector2 position = blank_position + new Vector2(0.0f, 30.0f);
                    int price = CWeaponFactory.GetPriceForLevel(WorkingProfile.WeaponSidekickRightType, WorkingProfile.WeaponSidekickRightLevel);

                    Labels.BaseCostPrice.Value = price;
                    Labels.BaseCostHeader.Draw(Game.DefaultSpriteBatch, Game.DefaultFont, position, Color.White);
                    Labels.BaseCostPrice.Draw(Game.DefaultSpriteBatch, Game.DefaultFont, position + new Vector2(0.0f, 30.0f), Color.White);
                }
            }
            else if (Menu == MenuChassis)
            {
                CChassisPart part = ChassisDefinitions.GetPart(WorkingProfile.ChassisType);
                CMenu.CMenuOption option = Menu.MenuOptions[Menu.Cursor];
                if (!option.CancelOption)
                {
                    Vector2 position = blank_position + new Vector2(0.0f, 50.0f);

                    Labels.ArmorValue.Value = Convert.ToInt32(part.Armor * 100.0f);
                    Labels.ArmorHeader.Draw(Game.DefaultSpriteBatch, Game.DefaultFont, position, Color.White);
                    Labels.ArmorValue.Draw(Game.DefaultSpriteBatch, Game.DefaultFont, position + new Vector2(0.0f, 30.0f), Color.White);

                    Labels.SpeedValue.Value = Convert.ToInt32(part.Speed * 100.0f);
                    Labels.SpeedHeader.Draw(Game.DefaultSpriteBatch, Game.DefaultFont, position, Color.White);
                    Labels.SpeedValue.Draw(Game.DefaultSpriteBatch, Game.DefaultFont, position + new Vector2(0.0f, 30.0f), Color.White);
                }
            }
            else if (Menu == MenuGenerator)
            {
                CGeneratorPart part = GeneratorDefinitions.GetPart(WorkingProfile.GeneratorType);
                CMenu.CMenuOption option = Menu.MenuOptions[Menu.Cursor];
                if (!option.CancelOption)
                {
                    Vector2 position = blank_position + new Vector2(0.0f, 50.0f);

                    Labels.EnergyValue.Value = Convert.ToInt32(part.Energy * 100.0f);
                    Labels.EnergyHeader.Draw(Game.DefaultSpriteBatch, Game.DefaultFont, position, Color.White);
                    Labels.EnergyValue.Draw(Game.DefaultSpriteBatch, Game.DefaultFont, position + new Vector2(0.0f, 30.0f), Color.White);

                    Labels.RegenValue.Value = Convert.ToInt32(part.Regen * 100.0f);
                    Labels.RegenHeader.Draw(Game.DefaultSpriteBatch, Game.DefaultFont, position, Color.White);
                    Labels.RegenValue.Draw(Game.DefaultSpriteBatch, Game.DefaultFont, position + new Vector2(0.0f, 30.0f), Color.White);
                }
            }
            else if (Menu == MenuShield)
            {
                CShieldPart part = ShieldDefinitions.GetPart(WorkingProfile.ShieldType);
                CMenu.CMenuOption option = Menu.MenuOptions[Menu.Cursor];
                if (!option.CancelOption)
                {
                    Vector2 position = blank_position + new Vector2(0.0f, 50.0f);

                    Labels.ShieldValue.Value = Convert.ToInt32(part.Shield * 100.0f);
                    Labels.ShieldHeader.Draw(Game.DefaultSpriteBatch, Game.DefaultFont, position, Color.White);
                    Labels.ShieldValue.Draw(Game.DefaultSpriteBatch, Game.DefaultFont, position + new Vector2(0.0f, 30.0f), Color.White);

                    Labels.RegenValue.Value = Convert.ToInt32(part.Regen * 100.0f);
                    Labels.RegenHeader.Draw(Game.DefaultSpriteBatch, Game.DefaultFont, position, Color.White);
                    Labels.RegenValue.Draw(Game.DefaultSpriteBatch, Game.DefaultFont, position + new Vector2(0.0f, 30.0f), Color.White);
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
            Vector2 blank_position = new Vector2(Game.Resolution.X / 2.0f - 220.0f, Game.Resolution.Y - 32.0f);
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
                strings[1] = "TRAINING IS";
                strings[2] = "NON-REFUNDABLE!";
            }

            float offset = Game.PlayersInGame == 1 ? 20.0f : 0.0f;
            Game.DefaultSpriteBatch.DrawStringAlignCenter(Game.DefaultFont, blank_position + new Vector2(0.0f, 30.0f + offset), strings[0], Color.White);
            Game.DefaultSpriteBatch.DrawStringAlignCenter(Game.DefaultFont, blank_position + new Vector2(0.0f, 60.0f + offset), strings[1], Color.White);
            Game.DefaultSpriteBatch.DrawStringAlignCenter(Game.DefaultFont, blank_position + new Vector2(0.0f, 90.0f + offset), strings[2], Color.White);
            Game.DefaultSpriteBatch.DrawStringAlignCenter(Game.DefaultFont, blank_position + new Vector2(0.0f, 120.0f + offset), strings[3], Color.White);
        }

        private void StageSelect(object tag)
        {
            SaveLockedProfile();
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
            if (WorkingProfile.WeaponSidekickLeftType == LockedProfile.WeaponSidekickLeftType && WorkingProfile.WeaponSidekickLeftLevel == LockedProfile.WeaponSidekickLeftLevel)
                return;

            CAudio.PlaySound("MenuBuy");

            LockWorkingProfile();
            RefreshSampleDisplay();
        }

        private void SelectSidekickLeftEmpty(object tag)
        {
            if (WorkingProfile.WeaponSidekickLeftType == LockedProfile.WeaponSidekickLeftType && WorkingProfile.WeaponSidekickLeftLevel == LockedProfile.WeaponSidekickLeftLevel)
                return;

            CAudio.PlaySound("MenuBuy");

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
            if (WorkingProfile.WeaponSidekickRightType == LockedProfile.WeaponSidekickRightType && WorkingProfile.WeaponSidekickRightLevel == LockedProfile.WeaponSidekickRightLevel)
                return;

            CAudio.PlaySound("MenuBuy");

            LockWorkingProfile();
            RefreshSampleDisplay();
        }

        private void SelectSidekickRightEmpty(object tag)
        {
            if (WorkingProfile.WeaponSidekickRightType == LockedProfile.WeaponSidekickRightType && WorkingProfile.WeaponSidekickRightLevel == LockedProfile.WeaponSidekickRightLevel)
                return;

            CAudio.PlaySound("MenuBuy");

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
            if (WorkingProfile.ChassisType == LockedProfile.ChassisType && WorkingProfile.ChassisType == LockedProfile.ChassisType) 
                return;

            CAudio.PlaySound("MenuBuy");

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
            if (WorkingProfile.GeneratorType == LockedProfile.GeneratorType && WorkingProfile.GeneratorType == LockedProfile.GeneratorType) 
                return;

            CAudio.PlaySound("MenuBuy");

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
            if (WorkingProfile.ShieldType == LockedProfile.ShieldType && WorkingProfile.ShieldType == LockedProfile.ShieldType) 
                return;

            CAudio.PlaySound("MenuBuy");

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
            string ability_name = string.Format("AbilityUnlocked{0}", tag.ToString());
            FieldInfo field = typeof(SProfilePilotState).GetField(ability_name);
            bool has_ability = (bool)field.GetValue(WorkingProfile);
            if (has_ability)
                return;

            // TODO: price cleanup
            int price = 10000 + (int)tag * 5000;
            if (LockedProfile.Money < price)
                return;

            LockedProfile.Money -= price;
            object reference = (object)LockedProfile;
            field.SetValue(reference, true);
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
                return;

            int price = 10000 + (int)tag * 5000;
            WorkingProfile.Money = LockedProfile.Money - price;
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
            if (has_ability)
                return false;

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

            RefreshSampleDisplay();
        }
    }
}
