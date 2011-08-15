//
// ShopMenu.cs
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
    public class CShopMenu
    {
        public CGalaxy Game { get; set; }
        public CWorld World { get; set; }
        public CMenu CategoryMenu { get; set; }
        public CMenu ItemMenu { get; set; }
        private CMenu MenuUpgradeShip { get; set; }
        private CMenu MenuPrimaryWeapon { get; set; }
        private CMenu MenuSecondaryWeapon { get; set; }
        private CMenu MenuSidekick { get; set; }
        private CMenu MenuChassis { get; set; }
        private CMenu MenuGenerator { get; set; }
        private CMenu MenuShield { get; set; }
        private delegate void DrawMenuErrataFunction();
        private DrawMenuErrataFunction DrawMenuErrata { get; set; }
        private CShip SampleShip { get; set; }
        private int SampleShotDelay { get; set; }
        private SProfilePilotState WorkingProfile;
        private SProfilePilotState LockedProfile;
        private Texture2D ShopPanelTexture { get; set; }
        private Texture2D ShopUpgradePanelTexture { get; set; }
        private CVisual ShopUpgradeBars8Visual { get; set; }
        private CVisual ShopUpgradeBars6Visual { get; set; }
        private GameControllerIndex ControllerIndex { get; set; }
        private Texture2D ShopPurchasePanelTexture { get; set; }
        private CTextLabel ShopPurchaseTextLabel { get; set; }
        private CVisual ShopItemPanelVisual { get; set; }
        public bool IsFlyToStage { get; set; }
        public bool IsMoveToStage { get; set; }

        private struct SLabels
        {
            public CTextLabel WeaponHeader;
            public CTextLabel WeaponPrice;
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
                WeaponHeader = new CTextLabel();
                WeaponPrice = new CTextLabel();
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

                WeaponHeader.Value = "WEAPON";
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

        public CShopMenu(CGalaxy game)
        {
            Game = game;

            WorkingProfile = GetShoppingPilotData();
            LockedProfile = WorkingProfile;

            ShopPanelTexture = CContent.LoadTexture2D(game, "Textures/UI/Shop/ShopPanel");
            ShopUpgradePanelTexture = CContent.LoadTexture2D(game, "Textures/UI/Shop/ShopUpgradePanel");
            ShopUpgradeBars8Visual = CVisual.MakeSpriteFromGame(game, "Textures/UI/Shop/ShopUpgradeBars8", Vector2.One, Color.White);
            ShopUpgradeBars8Visual.TileY = 8;
            ShopUpgradeBars8Visual.Recache();
            ShopUpgradeBars6Visual = CVisual.MakeSpriteFromGame(game, "Textures/UI/Shop/ShopUpgradeBars6", Vector2.One, Color.White);
            ShopUpgradeBars6Visual.TileY = 6;
            ShopUpgradeBars6Visual.Recache();
            ShopPurchasePanelTexture = CContent.LoadTexture2D(game, "Textures/UI/Shop/ShopPurchaseButtonPanel");
            ShopPurchaseTextLabel = new CTextLabel() { Value = "Purchase" };
            ShopItemPanelVisual = CVisual.MakeSpriteFromGame(game, "Textures/UI/Shop/ShopItemPanel", Vector2.One, Color.White);
            ShopItemPanelVisual.NormalizedOrigin = new Vector2(0.5f, 0.0f);
            ShopItemPanelVisual.TileX = 8;
            ShopItemPanelVisual.Recache();

            Labels = new SLabels(null);

        }

        public void Initialize(GameControllerIndex controller_index, CWorld world)
        {
            World = world;

            //
            // Upgrade Ship
            //
            MenuUpgradeShip = new CMenu(Game)
            {
                Position = GetMenuPosition(),
                MenuOptions = new List<CMenu.CMenuOption>()
                {
                    new CMenu.CMenuOption() { Text = "Chassis", Select = EditChassis, IconName = "Textures/UI/Shop/IconCategoryChassis" },
                    new CMenu.CMenuOption() { Text = "Generator", Select = EditGenerator, IconName = "Textures/UI/Shop/IconCategoryChassis" },
                    new CMenu.CMenuOption() { Text = "Shield", Select = EditShield, IconName = "Textures/UI/Shop/IconCategoryChassis" },
                    new CMenu.CMenuOption() { Text = "Main Weapon", Select = EditPrimaryWeapon, IconName = "Textures/UI/Shop/IconCategoryChassis" },
                    new CMenu.CMenuOption() { Text = "Support Weapon", Select = EditSecondaryWeapon, IconName = "Textures/UI/Shop/IconCategoryChassis" },
                    new CMenu.CMenuOption() { Text = "Sidekick", Select = EditSidekick, IconName = "Textures/UI/Shop/IconCategoryChassis" },
                },
                HideText = true,
                CursorIconName = "Textures/UI/Shop/IconCategoryCursor",
                OnCancel = ReturnToMainMenu,
            };

            //
            // Primary Weapon
            //
            MenuPrimaryWeapon = new CMenu(Game)
            {
                Position = GetMenuPosition(),
                MenuOptions = new List<CMenu.CMenuOption>(),
                OnCancel = CloseItemMenu,
                CursorIconName = "Textures/UI/Shop/IconItemCursor",
            };

            IEnumerable<string> primary_weapon_parts_own = new List<string>() { GetShoppingPilotData().WeaponPrimaryType };
            //IEnumerable<string> primary_weapon_parts_all = primary_weapon_parts_own.Concat(CMap.GetMapNodeByStageName(CSaveData.GetCurrentGameData(Game).Stage).AvailablePrimaryWeaponParts);
            IEnumerable<string> primary_weapon_parts_all = primary_weapon_parts_own.Concat(CMap.MakeRandomPrimaryWeapons(Game));
            IEnumerable<string> primary_weapon_parts = primary_weapon_parts_all.Distinct().OrderBy(W => CWeaponFactory.GetPriceForLevel(W, 0));
            foreach (string weapon_part in primary_weapon_parts)
            {
                MenuPrimaryWeapon.MenuOptions.Add(
                    new CMenu.CMenuOption()
                    {
                        Text = weapon_part,
                        SubText = "Cost: " + CWeaponFactory.GetPriceForLevel(weapon_part, 0),
                        IconName = CWeaponFactory.GetIconName(weapon_part),
                        OverlayIcon = ShopUpgradeBars8Visual,
                        OverlayOffset = new Vector2(0.0f, 28.0f),
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

            //
            // Secondary Weapon
            //
            MenuSecondaryWeapon = new CMenu(Game)
            {
                Position = GetMenuPosition(),
                MenuOptions = new List<CMenu.CMenuOption>(),
                OnCancel = CloseItemMenu,
                CursorIconName = "Textures/UI/Shop/IconItemCursor",
            };

            MenuSecondaryWeapon.MenuOptions.Add(new CMenu.CMenuOption() { Text = "None", SubText = "Cost: 0", Select = SelectSecondaryWeaponEmpty, Highlight = HighlightSecondaryWeapon, Data = "", IconName = "Textures/UI/Shop/IconItemNone", });
            IEnumerable<string> secondary_weapon_parts_own = new List<string>() { GetShoppingPilotData().WeaponSecondaryType };
            //IEnumerable<string> secondary_weapon_parts_all = secondary_weapon_parts_own.Concat(CMap.GetMapNodeByStageName(CSaveData.GetCurrentGameData(Game).Stage).AvailableSecondaryWeaponParts);
            IEnumerable<string> secondary_weapon_parts_all = secondary_weapon_parts_own.Concat(CMap.MakeRandomSecondaryWeapons(Game));
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
                        IconName = CWeaponFactory.GetIconName(weapon_part),
                        OverlayIcon = ShopUpgradeBars6Visual,
                        OverlayOffset = new Vector2(0.0f, 28.0f),
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

            //
            // Sidekick
            //
            MenuSidekick = new CMenu(Game)
            {
                Position = GetMenuPosition(),
                MenuOptions = new List<CMenu.CMenuOption>(),
                OnCancel = CloseItemMenu,
                CursorIconName = "Textures/UI/Shop/IconItemCursor",
            };

            MenuSidekick.MenuOptions.Add(new CMenu.CMenuOption() { Text = "None", SubText = "Cost: 0", Select = SelectSidekickEmpty, Highlight = HighlightSidekick, Data = "", IconName = "Textures/UI/Shop/IconItemNone", });
            IEnumerable<string> sidekick_left_weapon_parts_own = new List<string>() { GetShoppingPilotData().WeaponSidekickType };
            //IEnumerable<string> sidekick_left_weapon_parts_all = sidekick_left_weapon_parts_own.Concat(CMap.GetMapNodeByStageName(CSaveData.GetCurrentGameData(Game).Stage).AvailableSidekickWeaponParts);
            IEnumerable<string> sidekick_left_weapon_parts_all = sidekick_left_weapon_parts_own.Concat(CMap.MakeRandomSidekickWeapons(Game));
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
                        IconName = CWeaponFactory.GetIconName(weapon_part),
                        Data = weapon_part,
                        Select = SelectSidekick,
                        SelectValidate = SelectValidateSidekick,
                        Highlight = HighlightSidekick,
                    }
                );
            }

            //
            // Chassis
            //
            MenuChassis = new CMenu(Game)
            {
                Position = GetMenuPosition(),
                MenuOptions = new List<CMenu.CMenuOption>(),
                OnCancel = CloseItemMenu,
                CursorIconName = "Textures/UI/Shop/IconItemCursor",
            };

            IEnumerable<string> chassis_parts_own = new List<string>() { GetShoppingPilotData().ChassisType };
            //IEnumerable<string> chassis_parts_all = chassis_parts_own.Concat(CMap.GetMapNodeByStageName(CSaveData.GetCurrentGameData(Game).Stage).AvailableChassisParts);
            IEnumerable<string> chassis_parts_all = chassis_parts_own.Concat(CMap.MakeRandomChassisParts(Game));
            IEnumerable<string> chassis_parts = chassis_parts_all.Distinct().OrderBy(C => ChassisDefinitions.GetPart(C).Price);
            foreach (string chassis_part in chassis_parts)
            {
                MenuChassis.MenuOptions.Add(
                    new CMenu.CMenuOption()
                    {
                        Text = chassis_part,
                        SubText = "Cost: " + ChassisDefinitions.GetPart(chassis_part).Price,
                        IconName = ChassisDefinitions.GetPart(chassis_part).IconName,
                        Data = chassis_part,
                        Select = SelectChassis,
                        SelectValidate = SelectValidateChassis,
                        Highlight = HighlightChassis,
                    }
                );
            }

            //
            // Generator
            //
            MenuGenerator = new CMenu(Game)
            {
                Position = GetMenuPosition(),
                MenuOptions = new List<CMenu.CMenuOption>(),
                OnCancel = CloseItemMenu,
                CursorIconName = "Textures/UI/Shop/IconItemCursor",
            };

            IEnumerable<string> generator_parts_own = new List<string>() { GetShoppingPilotData().GeneratorType };
            //IEnumerable<string> generator_parts_all = generator_parts_own.Concat(CMap.GetMapNodeByStageName(CSaveData.GetCurrentGameData(Game).Stage).AvailableGeneratorParts);
            IEnumerable<string> generator_parts_all = generator_parts_own.Concat(CMap.MakeRandomGeneratorParts(Game));
            IEnumerable<string> generator_parts = generator_parts_all.Distinct().OrderBy(G => GeneratorDefinitions.GetPart(G).Price);
            foreach (string generator_part in generator_parts)
            {

                MenuGenerator.MenuOptions.Add(
                    new CMenu.CMenuOption()
                    {
                        Text = generator_part,
                        SubText = "Cost: " + GeneratorDefinitions.GetPart(generator_part).Price,
                        IconName = GeneratorDefinitions.GetPart(generator_part).IconName,
                        Data = generator_part,
                        Select = SelectGenerator,
                        SelectValidate = SelectValidateGenerator,
                        Highlight = HighlightGenerator,
                    }
                );
            }

            //
            // Shield
            //
            MenuShield = new CMenu(Game)
            {
                Position = GetMenuPosition(),
                MenuOptions = new List<CMenu.CMenuOption>(),
                OnCancel = CloseItemMenu,
                CursorIconName = "Textures/UI/Shop/IconItemCursor",
            };

            IEnumerable<string> shield_parts_own = new List<string>() { GetShoppingPilotData().ShieldType };
            //IEnumerable<string> shield_parts_all = shield_parts_own.Concat(CMap.GetMapNodeByStageName(CSaveData.GetCurrentGameData(Game).Stage).AvailableShieldParts);
            IEnumerable<string> shield_parts_all = shield_parts_own.Concat(CMap.MakeRandomShieldParts(Game));
            IEnumerable<string> shield_parts = shield_parts_all.Distinct().OrderBy(S => ShieldDefinitions.GetPart(S).Price);
            foreach (string shield_part in shield_parts)
            {
                MenuShield.MenuOptions.Add(
                    new CMenu.CMenuOption()
                    {
                        Text = shield_part,
                        SubText = "Cost: " + ShieldDefinitions.GetPart(shield_part).Price,
                        IconName = ShieldDefinitions.GetPart(shield_part).IconName,
                        Data = shield_part,
                        Select = SelectShield,
                        SelectValidate = SelectValidateShield,
                        Highlight = HighlightShield,
                    }
                );
            }

            CategoryMenu = MenuUpgradeShip;
            ItemMenu = null;
            DrawMenuErrata = DrawMenuBaseErrata;

            RefreshSampleDisplay();
        }

        public void Update()
        {
            MenuUpdateHighlights();

            if (ItemMenu == null)
                CategoryMenu.Update();
            else
                ItemMenu.Update();

            UpdateMenuPositions();

#if DEBUG
            if (Game.Input.IsL1Pressed(ControllerIndex) || Game.Input.IsKeyPressed(Keys.PageDown))
                EditMoney(-10000);
            if (Game.Input.IsR1Pressed(ControllerIndex) || Game.Input.IsKeyPressed(Keys.PageUp))
                EditMoney(+10000);
#endif

            if (Game.Input.IsPadStartPressed(ControllerIndex) || Game.Input.IsKeyPressed(ControllerIndex == GameControllerIndex.One ? Keys.F1 : Keys.F2))
                FlyToStage();

            Game.HudManager.Huds[(int)ControllerIndex].Ship = SampleShip;
            Game.HudManager.Huds[(int)ControllerIndex].Update();
            SampleShip.UpdateGenerator();

            if (IsFlyToStage)
            {
                SampleShip.Physics.Friction = 1.0f;
                SampleShip.Physics.Velocity += new Vector2(0.0f, -1.5f);
                SampleShip.Physics.Solve();

                if (SampleShip.Physics.Position.Y < -600.0f)
                    IsMoveToStage = true;
            }

            SampleShotDelay = Math.Max(0, SampleShotDelay - 1);
            if (SampleShotDelay <= 0)
            {
                SampleShip.FirePrimarySecondaryWeapons();
                SampleShip.ChargeAndFireFullSidekick(SampleShip.WeaponSidekickLeft);
                SampleShip.ChargeAndFireFullSidekick(SampleShip.WeaponSidekickRight);
                SampleShip.UpdateWeapons();
            }
        }

        public void DrawSampleShip(SpriteBatch sprite_batch)
        {
            SampleShip.Draw(sprite_batch);
        }

        public void Draw(SpriteBatch sprite_batch)
        {
            Vector2 panel_position = GetMenuPosition() - GetMenuPanelOffset();
            SpriteEffects panel_effects = ControllerIndex == GameControllerIndex.Two ? SpriteEffects.FlipHorizontally : SpriteEffects.None ;
            sprite_batch.Draw(ShopPanelTexture, panel_position, null, Color.White, 0.0f, Vector2.Zero, 1.0f, panel_effects, CLayers.UI - 0.5f);

            CategoryMenu.Draw(sprite_batch);

            if (ItemMenu != null)
            {
                ShopItemPanelVisual.Frame = Math.Min(8, ItemMenu.MenuOptions.Count) - 1;
                ShopItemPanelVisual.Recache();
                ShopItemPanelVisual.Draw(sprite_batch, ItemMenu.Position + new Vector2(-6.0f, -58.0f), 0.0f);

                ItemMenu.Draw(sprite_batch);
            }

            //Game.DefaultSpriteBatch.DrawString(Game.GameRegularFont, "equipment usage", new Vector2(960.0f, 962.0f), Color.LightGray, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, CLayers.UI+ CLayers.SubLayerIncrement);
            //Game.DefaultSpriteBatch.DrawString(Game.GameRegularFont, (int)Math.Round(c * 100) + "%", new Vector2(960.0f, 962.0f), Color.LightGray, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, CLayers.UI+ CLayers.SubLayerIncrement);

            //DrawMoneyPoolBarAll();
            DrawMoney();
            DrawMenuErrata();

            //if (Game.PlayersInGame > 1)
            //{
                //Game.DefaultSpriteBatch.DrawString(Game.GameRegularFont, "L1", new Vector2(328.0f, 240.0f), Color.Gray, 0.0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0.0f);
                //Game.DefaultSpriteBatch.DrawString(Game.GameRegularFont, "R1", new Vector2(1600.0f, 240.0f), Color.Gray, 0.0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0.0f);
            //}
        }

        private void MenuUpdateHighlights()
        {
            if (ItemMenu == MenuPrimaryWeapon)
                foreach (CMenu.CMenuOption option in MenuPrimaryWeapon.MenuOptions)
                    option.SpecialHighlight = (string) option.Data == LockedProfile.WeaponPrimaryType;
            if (ItemMenu == MenuSecondaryWeapon)
                foreach (CMenu.CMenuOption option in MenuSecondaryWeapon.MenuOptions)
                    option.SpecialHighlight = (string) option.Data == LockedProfile.WeaponSecondaryType;
            if (ItemMenu == MenuSidekick)
                foreach (CMenu.CMenuOption option in MenuSidekick.MenuOptions)
                    option.SpecialHighlight = (string) option.Data == LockedProfile.WeaponSidekickType;
            if (ItemMenu == MenuChassis)
                foreach (CMenu.CMenuOption option in MenuChassis.MenuOptions)
                    option.SpecialHighlight = (string) option.Data == LockedProfile.ChassisType;
            if (ItemMenu == MenuGenerator)
                foreach (CMenu.CMenuOption option in MenuGenerator.MenuOptions)
                    option.SpecialHighlight = (string) option.Data == LockedProfile.GeneratorType;
            if (ItemMenu == MenuShield)
                foreach (CMenu.CMenuOption option in MenuShield.MenuOptions)
                    option.SpecialHighlight = (string) option.Data == LockedProfile.ShieldType;
        }

        private SProfilePilotState GetShoppingPilotData()
        {
            return CSaveData.GetCurrentGameData(Game).Pilots[(int)ControllerIndex];
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
            Game.DefaultSpriteBatch.DrawString(Game.GameRegularFont, WorkingProfile.Money.ToString(), Game.HudManager.Huds[(int)ControllerIndex].MoneyTextPosition + new Vector2(0.0f, -48.0f), color, 0.0f, Vector2.Zero, 1.5f, SpriteEffects.None, CLayers.UI+ CLayers.SubLayerIncrement);
        }

        private void RefreshSampleDisplay()
        {
            // HACK: disable toggle weapons
            if (SampleShip != null)
            {
                SampleShip.DidntFirePrimarySecondaryWeapons();
                SampleShip.World.EntityDelete(SampleShip);
            }

            SampleShip = CShipFactory.GenerateShip(World, WorkingProfile, ControllerIndex);

            float x = 155.0f;
            if (Game.PlayersInGame > 1)
                x = ControllerIndex == GameControllerIndex.One ? -90.0f : 90.0f;
            SampleShip.Physics.Position = new Vector2(x, 250.0f);

            SampleShotDelay = 15;
        }

        private void RevertWorkingProfile(object tag)
        {
            WorkingProfile = LockedProfile;
            RefreshSampleDisplay();
        }

        public void FlyToStage()
        {
            SaveLockedProfile();

            SampleShip.IsInvincible += 1;
            SampleShip.IsReflectBullets += 1;

            IsFlyToStage = true;
        }

        public void CancelFlyToStage()
        {
            RefreshSampleDisplay();

            IsFlyToStage = false;
        }

        private void LockWorkingProfile()
        {
            LockedProfile = WorkingProfile;
            
            // TODO: sample ship fly
        }

        private void SaveLockedProfile()
        {
            SProfile profile = CSaveData.GetCurrentProfile();
            profile.Game[Game.PlayersInGame - 1].Pilots[(int)ControllerIndex] = LockedProfile;
            CSaveData.SetCurrentProfileData(profile);
            CSaveData.SaveRequest();
        }

        private string GetWeaponDisplayString(string typename, int level)
        {
            return String.Format("{0} Mk {1}", CWeaponFactory.GetDisplayName(typename), GetRomanNumerals(level));
        }

        private string GetRomanNumerals(int value)
        {
            switch (value)
            {
                case 0: return "I";
                case 1: return "II";
                case 2: return "III";
                case 3: return "IV";
                case 4: return "V";
                case 5: return "VI";
                case 6: return "VII";
                case 7: return "VIII";
                case 8: return "IX";
                case 9: return "X";
            }

            return "";
        }

        private Color GetPurchaseColor(bool can_afford)
        {
            if (!can_afford)
                return Color.DarkGray;

            return Color.Gray;
        }

        private void DrawMenuBaseErrata()
        {
            Vector2 info_position = GetInfoPosition();
            Vector2 text_position = info_position + GetInfoOffset();

            Game.DefaultSpriteBatch.Draw(ShopUpgradePanelTexture, info_position, Color.White);

            if (ItemMenu == MenuPrimaryWeapon)
            {
                int max = CWeaponFactory.GetMaxLevel(WorkingProfile.WeaponPrimaryType);
                int level = WorkingProfile.WeaponPrimaryLevel;

                CMenu.CMenuOption option = ItemMenu.MenuOptions[ItemMenu.Cursor];
                if (!option.CancelOption && WorkingProfile.WeaponPrimaryType != "")
                {
                    Vector2 position = text_position;
                    ShopUpgradeBars8Visual.Frame = level;
                    ShopUpgradeBars8Visual.Recache();
                    ShopUpgradeBars8Visual.Draw(Game.DefaultSpriteBatch, position + new Vector2(-6.0f, 176.0f), 0.0f);

                    int price = CWeaponFactory.GetPriceForLevel(WorkingProfile.WeaponPrimaryType, WorkingProfile.WeaponPrimaryLevel);

                    Labels.WeaponPrice.Value = price;
                    Labels.WeaponHeader.Value = GetWeaponDisplayString(WorkingProfile.WeaponPrimaryType, level);

                    Labels.WeaponHeader.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, position, Color.White);
                    Labels.WeaponPrice.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, position + new Vector2(0.0f, 30.0f), Color.White);

                    if (SelectValidatePrimaryWeapon(ItemMenu.MenuOptions[ItemMenu.Cursor].Data) || WorkingProfile.WeaponPrimaryType == "None")
                        DrawPurchasePanel();
                }
            }
            else if (ItemMenu == MenuSecondaryWeapon)
            {
                int max = CWeaponFactory.GetMaxLevel(WorkingProfile.WeaponSecondaryType);
                int level = WorkingProfile.WeaponSecondaryLevel;

                CMenu.CMenuOption option = ItemMenu.MenuOptions[ItemMenu.Cursor];
                if (!option.CancelOption && WorkingProfile.WeaponSecondaryType != "")
                {
                    Vector2 position = text_position;
                    ShopUpgradeBars6Visual.Frame = level;
                    ShopUpgradeBars6Visual.Recache();
                    ShopUpgradeBars6Visual.Draw(Game.DefaultSpriteBatch, position + new Vector2(-6.0f, 176.0f), 0.0f);

                    int price = CWeaponFactory.GetPriceForLevel(WorkingProfile.WeaponSecondaryType, WorkingProfile.WeaponSecondaryLevel);
                    int next_price = CWeaponFactory.GetPriceForLevel(WorkingProfile.WeaponSecondaryType, WorkingProfile.WeaponSecondaryLevel + 1);

                    Labels.WeaponPrice.Value = price;
                    Labels.WeaponHeader.Value = GetWeaponDisplayString(WorkingProfile.WeaponSecondaryType, level);

                    Labels.WeaponHeader.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, position + new Vector2(0.0f, 30.0f), Color.White);
                    Labels.WeaponPrice.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, position, Color.White);
                }

                if (!option.CancelOption)
                {
                    // delete me
                    //if (WorkingProfile.WeaponSecondaryType == "")
                    //{
                        //Vector2 position = text_position;
                        //Labels.BaseCostPrice.Value = 0;
                        //Labels.BaseCostHeader.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, position, Color.White);
                        //Labels.BaseCostPrice.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, position + new Vector2(0.0f, 30.0f), Color.White);
                    //}

                    if (SelectValidateSecondaryWeapon(ItemMenu.MenuOptions[ItemMenu.Cursor].Data))
                        DrawPurchasePanel();
                }
            }
            else if (ItemMenu == MenuSidekick)
            {
                CMenu.CMenuOption option = ItemMenu.MenuOptions[ItemMenu.Cursor];
                if (!option.CancelOption && WorkingProfile.WeaponSidekickType != "")
                {
                    Vector2 position = text_position + new Vector2(0.0f, 30.0f);
                    int price = CWeaponFactory.GetPriceForLevel(WorkingProfile.WeaponSidekickType, WorkingProfile.WeaponSidekickLevel);

                    Labels.WeaponPrice.Value = price;
                    Labels.WeaponHeader.Value = CWeaponFactory.GetDisplayName(WorkingProfile.WeaponSidekickType);

                    Labels.WeaponHeader.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, position + new Vector2(0.0f, 30.0f), Color.White);
                    Labels.WeaponPrice.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, position, Color.White);
                }

                if (!option.CancelOption)
                {
                    // delete me
                    //if (WorkingProfile.WeaponSidekickType == "")
                    //{
                        //Vector2 position = text_position;
                        //Labels.BaseCostPrice.Value = 0;
                        //Labels.BaseCostHeader.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, position, Color.White);
                        //Labels.BaseCostPrice.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, position + new Vector2(0.0f, 30.0f), Color.White);
                    //}

                    if (SelectValidateSidekick(ItemMenu.MenuOptions[ItemMenu.Cursor].Data))
                        DrawPurchasePanel();
                }
            }
            else if (ItemMenu == MenuChassis)
            {
                CChassisPart part = ChassisDefinitions.GetPart(WorkingProfile.ChassisType);
                CMenu.CMenuOption option = ItemMenu.MenuOptions[ItemMenu.Cursor];
                if (!option.CancelOption)
                {
                    Vector2 position0 = text_position + new Vector2(0.0f, 10.0f);
                    Vector2 position1 = text_position + new Vector2(0.0f, 80.0f);
                    Vector2 position2 = text_position + new Vector2(0.0f, 150.0f);

                    Labels.ArmorValue.Value = Convert.ToInt32(part.Armor * 100.0f);
                    Labels.ArmorHeader.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, position0, Color.White);
                    Labels.ArmorValue.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, position0 + new Vector2(0.0f, 30.0f), Color.White);

                    Labels.SpeedValue.Value = Convert.ToInt32(part.Speed * 100.0f);
                    Labels.SpeedHeader.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, position1, Color.White);
                    Labels.SpeedValue.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, position1 + new Vector2(0.0f, 30.0f), Color.White);

                    // delete me?
                    //Labels.DensityValue.Value = Convert.ToInt32(part.CollisionResistance * 100.0f);
                    //Labels.DensityHeader.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, position2, Color.White);
                    //Labels.DensityValue.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, position2 + new Vector2(0.0f, 30.0f), Color.White);
                }

                if (!option.CancelOption)
                {
                    if (SelectValidateChassis(ItemMenu.MenuOptions[ItemMenu.Cursor].Data))
                        DrawPurchasePanel();
                }
            }
            else if (ItemMenu == MenuGenerator)
            {
                CGeneratorPart part = GeneratorDefinitions.GetPart(WorkingProfile.GeneratorType);
                CMenu.CMenuOption option = ItemMenu.MenuOptions[ItemMenu.Cursor];
                if (!option.CancelOption)
                {
                    Vector2 position0 = text_position + new Vector2(0.0f, 10.0f);
                    Vector2 position1 = text_position + new Vector2(0.0f, 80.0f);
                    Vector2 position2 = text_position + new Vector2(0.0f, 160.0f);

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
                    if (SelectValidateGenerator(ItemMenu.MenuOptions[ItemMenu.Cursor].Data))
                        DrawPurchasePanel();
                }
            }
            else if (ItemMenu == MenuShield)
            {
                CShieldPart part = ShieldDefinitions.GetPart(WorkingProfile.ShieldType);
                CMenu.CMenuOption option = ItemMenu.MenuOptions[ItemMenu.Cursor];
                if (!option.CancelOption)
                {
                    Vector2 position0 = text_position + new Vector2(0.0f, 10.0f);
                    Vector2 position1 = text_position + new Vector2(0.0f, 80.0f);
                    Vector2 position2 = text_position + new Vector2(0.0f, 150.0f);

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
                    if (SelectValidateShield(ItemMenu.MenuOptions[ItemMenu.Cursor].Data))
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
            Vector2 info_position = GetInfoPosition();
            Vector2 text_position = info_position + GetInfoOffset();

            string[] strings = {
                "WELCOME TO SHOP",
                "PURCHASE UPGRADES",
                "AND WEAPONS HERE",
                "",
            };

            if (Game.PlayersInGame > 1)
            {
                strings[3] = "L1/R1 TO CHANGE PLAYER";
            }

            if (ItemMenu == MenuUpgradeShip)
            {
                switch (ItemMenu.Cursor)
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

            float offset = Game.PlayersInGame == 1 ? 20.0f : 0.0f;
            Game.DefaultSpriteBatch.DrawStringAlignCenter(Game.GameRegularFont, text_position + new Vector2(0.0f, 30.0f + offset), strings[0], Color.White);
            Game.DefaultSpriteBatch.DrawStringAlignCenter(Game.GameRegularFont, text_position + new Vector2(0.0f, 60.0f + offset), strings[1], Color.White);
            Game.DefaultSpriteBatch.DrawStringAlignCenter(Game.GameRegularFont, text_position + new Vector2(0.0f, 90.0f + offset), strings[2], Color.White);
            Game.DefaultSpriteBatch.DrawStringAlignCenter(Game.GameRegularFont, text_position + new Vector2(0.0f, 150.0f + offset), strings[3], Color.White);
        }

        private void StageSelect(object tag)
        {
            SaveLockedProfile();
            Game.State = new CStateFadeTo(Game, Game.State, new CStateStageSelect(Game));
        }

        private void EditPrimaryWeapon(object tag)
        {
            OpenItemMenu(MenuPrimaryWeapon);
        }

        private void EditSecondaryWeapon(object tag)
        {
            OpenItemMenu(MenuSecondaryWeapon);
        }

        private void EditSidekick(object tag)
        {
            OpenItemMenu(MenuSidekick);
        }

        private void EditMoney(int amount)
        {
            WorkingProfile.Money += amount;
            WorkingProfile.Money = Math.Max(0, WorkingProfile.Money);

            LockWorkingProfile();
        }

        private void EditChassis(object tag)
        {
            OpenItemMenu(MenuChassis);
        }

        private void EditGenerator(object tag)
        {
            OpenItemMenu(MenuGenerator);
        }

        private void EditShield(object tag)
        {
            OpenItemMenu(MenuShield);
        }

        private void Back(object tag)
        {
            SaveLockedProfile();
            Game.State = new CStateFadeTo(Game, Game.State, new CStateMainMenu(Game));
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

        private Vector2 GetMenuPosition()
        {
            float x = Game.Resolution.X;
            float y = Game.Resolution.Y;
            return ControllerIndex == GameControllerIndex.One ?
                new Vector2(x * 0.291f, y * 0.351f) :
                new Vector2(x * 0.601f, y * 0.351f);
        }

        private Vector2 GetMenuPanelOffset()
        {
            float x = Game.Resolution.X;
            float y = Game.Resolution.Y;
            return ControllerIndex == GameControllerIndex.One ?
                new Vector2(x * 0.065f, y * 0.060f) :
                new Vector2(x * 0.016f, y * 0.022f);
        }

        private Vector2 GetInfoPosition()
        {
            float x = Game.Resolution.X;
            float y = Game.Resolution.Y;
            return ControllerIndex == GameControllerIndex.One ?
                new Vector2(x * 0.020f, y * 0.400f) :
                new Vector2(x * 0.920f, y * 0.400f);
        }

        private Vector2 GetInfoOffset()
        {
            float x = Game.Resolution.X;
            float y = Game.Resolution.Y;
            return ControllerIndex == GameControllerIndex.One ?
                new Vector2(x * 0.106f, y * 0.031f) :
                new Vector2(x * 0.106f, y * 0.031f);
        }

        private void DrawPurchasePanel()
        {
            Vector2 menu_position = GetMenuPosition();
            Vector2 panel_position = menu_position + new Vector2(20.0f, 564.0f);
            Game.DefaultSpriteBatch.Draw(ShopPurchasePanelTexture, panel_position, Color.White);
            ShopPurchaseTextLabel.Alignment = CTextLabel.EAlignment.Left;
            float scale = 1.0f + (float)(Math.Abs(Math.Sin(SampleShip.World.Game.GameFrame * 0.1f))) * 0.015f;
            ShopPurchaseTextLabel.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, panel_position + new Vector2(60.0f, 42.0f), Color.White, scale);
            //Game.DefaultSpriteBatch.DrawString(Game.GameRegularFont, "Purchase", panel_position + new Vector2(58.0f, 24.0f), Color.White);
        }

        private void OpenItemMenu(CMenu child)
        {
            ItemMenu = child;
            ItemMenu.ForceRefresh();
        }

        private void CloseItemMenu()
        {
            ItemMenu = null;
        }

        private void ReturnToUpgradeMenu(object tag)
        {
            CloseItemMenu();
        }

        private void ReturnToMainMenu()
        {
            Game.State = new CStateFadeTo(Game, Game.State, new CStateMainMenu(Game));
        }

        private void UpdateMenuPositions()
        {
            CategoryMenu.Position = GetMenuPosition();
            if (ItemMenu != null)
            {
                // HACK: force get cursor icon
                CategoryMenu.GetCursorIcon();
                ItemMenu.GetCursorIcon();

                float cursor = CategoryMenu.Cursor * CategoryMenu.CursorIconVisual.Texture.Height;
                float category_height = CategoryMenu.MenuOptions.Count * CategoryMenu.CursorIconVisual.Texture.Height;
                float item_height = ItemMenu.MenuOptions.Count * ItemMenu.CursorIconVisual.Texture.Height;
                float bottom = cursor + item_height;

                // add more safe area
                bottom += 20.0f;

                float overlap = bottom - category_height;

                overlap = Math.Max(0.0f, overlap);

                ItemMenu.Position = CategoryMenu.Position + new Vector2(0.0f, cursor) + new Vector2(134.0f, -overlap);
            }
        }
    }
}
