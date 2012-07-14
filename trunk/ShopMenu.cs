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
        public static bool ShowAllItems { get; set; }

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
        private Texture2D ShopPurchasePanelInvalidTexture { get; set; }
        private CTextLabel ShopPurchaseTextLabel { get; set; }
        private CVisual ShopItemPanelVisual { get; set; }
        private CVisual IconItemEquippedVisual { get; set; }
        public bool IsFlyToStage { get; set; }
        public bool IsMoveToStage { get; set; }

        private struct SLabels
        {
            public CTextLabel ItemHeader;
            public CTextLabel ItemPrice;
            public CTextLabel Info0Name;
            public CTextLabel Info0Data;
            public CTextLabel Info1Name;
            public CTextLabel Info1Data;
            public CTextLabel Info2Name;
            public CTextLabel Info2Data;
            public CTextLabel DescHeader;
            public CTextLabel DescValue;

            public SLabels(object unused)
            {
                ItemHeader = new CTextLabel();
                ItemPrice = new CTextLabel();
                Info0Name = new CTextLabel();
                Info0Data = new CTextLabel();
                Info1Name = new CTextLabel();
                Info1Data = new CTextLabel();
                Info2Name = new CTextLabel();
                Info2Data = new CTextLabel();
                DescHeader = new CTextLabel();
                DescValue = new CTextLabel();

                Info0Name.Alignment = CTextLabel.EAlignment.Right;
                Info1Name.Alignment = CTextLabel.EAlignment.Right;
                Info2Name.Alignment = CTextLabel.EAlignment.Right;
                Info0Data.Alignment = CTextLabel.EAlignment.Left;
                Info1Data.Alignment = CTextLabel.EAlignment.Left;
                Info2Data.Alignment = CTextLabel.EAlignment.Left;
                DescValue.Alignment = CTextLabel.EAlignment.Center;
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
            ShopPurchasePanelInvalidTexture = CContent.LoadTexture2D(game, "Textures/UI/Shop/ShopPurchaseButtonPanelInvalid");
            ShopPurchaseTextLabel = new CTextLabel() { Value = "Purchase" };
            ShopItemPanelVisual = CVisual.MakeSpriteFromGame(game, "Textures/UI/Shop/ShopItemPanel", Vector2.One, Color.White);
            ShopItemPanelVisual.NormalizedOrigin = new Vector2(0.5f, 0.0f);
            ShopItemPanelVisual.TileX = 8;
            ShopItemPanelVisual.Recache();
            IconItemEquippedVisual = CVisual.MakeSpriteFromGame(game, "Textures/UI/Shop/IconItemEquipped", Vector2.One, Color.White);

            Labels = new SLabels(null);

            ShowAllItems = false;
#if DEBUG
            ShowAllItems = true;
#endif

        }

        public void Initialize(GameControllerIndex controller_index, CWorld world)
        {
            World = world;
            ControllerIndex = controller_index;

            bool cleared_game = CSaveData.GetCurrentGameData(Game).ClearedGame == true;

            //
            // Upgrade Ship
            //
            MenuUpgradeShip = new CMenu(Game)
            {
                Position = GetMenuPosition(),
                MenuOptions = new List<CMenu.CMenuOption>()
                {
                    new CMenu.CMenuOption() { Text = "Chassis", Select = EditChassis, IconName = "Textures/UI/Shop/IconCategoryChassis" },
                    new CMenu.CMenuOption() { Text = "Generator", Select = EditGenerator, IconName = "Textures/UI/Shop/IconCategoryGenerator" },
                    new CMenu.CMenuOption() { Text = "Shield", Select = EditShield, IconName = "Textures/UI/Shop/IconCategoryShield" },
                    new CMenu.CMenuOption() { Text = "Main Weapon", Select = EditPrimaryWeapon, IconName = "Textures/UI/Shop/IconCategoryWeaponPrimary" },
                    new CMenu.CMenuOption() { Text = "Support Weapon", Select = EditSecondaryWeapon, IconName = "Textures/UI/Shop/IconCategoryWeaponSecondary" },
                    new CMenu.CMenuOption() { Text = "Sidekick", Select = EditSidekick, IconName = "Textures/UI/Shop/IconCategoryWeaponSidekick" },
                },
                HideText = true,
                CursorIconName = "Textures/UI/Shop/IconCategoryCursor",
                //OnCancel = ReturnToMainMenu,
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
            IEnumerable<string> primary_weapon_parts_all = (ShowAllItems || cleared_game) ?
                primary_weapon_parts_own.Concat(CMap.GetMapNodeByStageName(CSaveData.GetCurrentGameData(Game).Stage).AvailablePrimaryWeaponParts) :
                primary_weapon_parts_own.Concat(CMap.MakeRandomPrimaryWeapons(Game));
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
                        OverlayOffset2 = new Vector2(6.0f, 0.0f),
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

            MenuSecondaryWeapon.MenuOptions.Add(new CMenu.CMenuOption() { Text = "None", SubText = "Cost: 0", Select = SelectSecondaryWeaponEmpty, Highlight = HighlightSecondaryWeapon, Data = "", IconName = "Textures/UI/Shop/IconItemNone", OverlayOffset2 = new Vector2(6.0f, 0.0f), });
            IEnumerable<string> secondary_weapon_parts_own = new List<string>() { GetShoppingPilotData().WeaponSecondaryType };
            IEnumerable<string> secondary_weapon_parts_all = (ShowAllItems || cleared_game) ? 
                secondary_weapon_parts_own.Concat(CMap.GetMapNodeByStageName(CSaveData.GetCurrentGameData(Game).Stage).AvailableSecondaryWeaponParts) :
                secondary_weapon_parts_own.Concat(CMap.MakeRandomSecondaryWeapons(Game));
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
                        OverlayOffset2 = new Vector2(6.0f, 0.0f),
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

            MenuSidekick.MenuOptions.Add(new CMenu.CMenuOption() { Text = "None", SubText = "Cost: 0", Select = SelectSidekickEmpty, Highlight = HighlightSidekick, Data = "", IconName = "Textures/UI/Shop/IconItemNone", OverlayOffset2 = new Vector2(6.0f, 0.0f), });
            IEnumerable<string> sidekick_left_weapon_parts_own = new List<string>() { GetShoppingPilotData().WeaponSidekickType };
            IEnumerable<string> sidekick_left_weapon_parts_all = (ShowAllItems || cleared_game) ? 
                sidekick_left_weapon_parts_own.Concat(CMap.GetMapNodeByStageName(CSaveData.GetCurrentGameData(Game).Stage).AvailableSidekickWeaponParts) :
                sidekick_left_weapon_parts_own.Concat(CMap.MakeRandomSidekickWeapons(Game));
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
                        OverlayOffset2 = new Vector2(6.0f, 0.0f),
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
            IEnumerable<string> chassis_parts_all = ShowAllItems ? 
                chassis_parts_own.Concat(CMap.GetMapNodeByStageName(CSaveData.GetCurrentGameData(Game).Stage).AvailableChassisParts) :
                chassis_parts_own.Concat(CMap.MakeRandomChassisParts(Game));

            if (cleared_game)
            {
                chassis_parts_all = new List<string>() { 
                    "Interceptor",
                    "Phoenix",
                    "Lightning",
                    "Dragon",
                    "Demon",
                    "Ace",
                };
            }

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
                        OverlayOffset2 = new Vector2(6.0f, 0.0f),
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
            IEnumerable<string> generator_parts_all = ShowAllItems ? 
                generator_parts_own.Concat(CMap.GetMapNodeByStageName(CSaveData.GetCurrentGameData(Game).Stage).AvailableGeneratorParts) :
                generator_parts_own.Concat(CMap.MakeRandomGeneratorParts(Game));

            if (cleared_game)
            {
                generator_parts_all = new List<string>() { 
                    "Vortex Generator",
                    "Capacitor Generator",
                    "Electron Generator",
                    "Kinetic Generator",
                    "Magnetic Generator",
                    "Fusion Generator",
                };
            }

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
                        OverlayOffset2 = new Vector2(6.0f, 0.0f),
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
            IEnumerable<string> shield_parts_all = ShowAllItems ?
                shield_parts_own.Concat(CMap.GetMapNodeByStageName(CSaveData.GetCurrentGameData(Game).Stage).AvailableShieldParts) :
                shield_parts_own.Concat(CMap.MakeRandomShieldParts(Game));

            if (cleared_game)
            {
                shield_parts_all = new List<string>() { 
                    "Advanced Shield",
                    "Micro Shield",
                    "Actuator Shield",
                    "Power Shield",
                    "Tank Shield",
                    "Ultimate Shield",
                };
            }

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
                        OverlayOffset2 = new Vector2(6.0f, 0.0f),
                    }
                );
            }

            CategoryMenu = MenuUpgradeShip;
            ItemMenu = null;

            RefreshSampleDisplay();
        }

        public void Update()
        {
            if (IsFlyToStage)
            {
                if (Game.Input.IsPadBackPressed(ControllerIndex) || Game.Input.IsKeyPressed(Keys.Escape))
                    CancelFlyToStage();
            }
            else
            {
                if (ItemMenu == null)
                    CategoryMenu.Update();
                else
                    ItemMenu.Update();
            }

            UpdateMenuPositions();

            int primary_level = WorkingProfile.WeaponPrimaryLevel;
            int secondary_level = WorkingProfile.WeaponSecondaryLevel;
            ShopUpgradeBars8Visual.Frame = primary_level;
            ShopUpgradeBars6Visual.Frame = secondary_level;
            ShopUpgradeBars8Visual.Recache();
            ShopUpgradeBars6Visual.Recache();

#if DEBUG
            if (Game.Input.IsL1Pressed(ControllerIndex) || Game.Input.IsKeyPressed(Keys.PageDown))
                EditMoney(-10000);
            if (Game.Input.IsR1Pressed(ControllerIndex) || Game.Input.IsKeyPressed(Keys.PageUp))
                EditMoney(+10000);
#endif

            if (Game.Input.IsPadStartPressed(ControllerIndex) || Game.Input.IsKeyPressed(ControllerIndex == GameControllerIndex.One ? Keys.F1 : Keys.F2))
                FlyToStage();

#if SOAK_TEST
            if (!IsFlyToStage)
                FlyToStage();
#endif

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

            DrawMoney();
            DrawMenuBaseErrata();
        }

        private void MenuUpdateEquippedIcons()
        {
            if (ItemMenu == MenuPrimaryWeapon)
                foreach (CMenu.CMenuOption option in MenuPrimaryWeapon.MenuOptions)
                    option.OverlayIcon2 = (string)option.Data == LockedProfile.WeaponPrimaryType ? IconItemEquippedVisual : null;
            if (ItemMenu == MenuSecondaryWeapon)
                foreach (CMenu.CMenuOption option in MenuSecondaryWeapon.MenuOptions)
                    option.OverlayIcon2 = (string)option.Data == LockedProfile.WeaponSecondaryType ? IconItemEquippedVisual : null;
            if (ItemMenu == MenuSidekick)
                foreach (CMenu.CMenuOption option in MenuSidekick.MenuOptions)
                    option.OverlayIcon2 = (string)option.Data == LockedProfile.WeaponSidekickType ? IconItemEquippedVisual : null;
            if (ItemMenu == MenuChassis)
                foreach (CMenu.CMenuOption option in MenuChassis.MenuOptions)
                    option.OverlayIcon2 = (string)option.Data == LockedProfile.ChassisType ? IconItemEquippedVisual : null;
            if (ItemMenu == MenuGenerator)
                foreach (CMenu.CMenuOption option in MenuGenerator.MenuOptions)
                    option.OverlayIcon2 = (string)option.Data == LockedProfile.GeneratorType ? IconItemEquippedVisual : null;
            if (ItemMenu == MenuShield)
                foreach (CMenu.CMenuOption option in MenuShield.MenuOptions)
                    option.OverlayIcon2 = (string)option.Data == LockedProfile.ShieldType ? IconItemEquippedVisual : null;
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

            Color color = new Color(160, 160, 160);
            Color earn_money = new Color(140, 170, 180);
            Color lose_money = new Color(130, 130, 130);
            Color cannot_afford = new Color(180, 130, 130);

            if (diff < 0)
                if (base_ < 0)
                    color = cannot_afford;
                else
                    color = lose_money;

            if (diff > 0)
                color = earn_money;

            // TODO: money display
            // TODO: reduce garbage
            Game.HudManager.Huds[(int)ControllerIndex].ShowMoney = false;
            string money_string = string.Format("{0}￥", WorkingProfile.Money);
            Game.DefaultSpriteBatch.DrawStringAlignCenter(Game.GameLargeFont, Game.HudManager.Huds[(int)ControllerIndex].MoneyTextPosition, money_string, color, 1.15f);
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

            MenuUpdateEquippedIcons();

            // should we try and force collect recent created objects? strings, etc
            // TODO: seems something is leaking atm
            //System.GC.Collect(0, GCCollectionMode.Forced);
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
            SampleShip.Physics.Velocity = Vector2.Zero;

            IsFlyToStage = true;
        }

        public void CancelFlyToStage()
        {
            RefreshSampleDisplay();

            SampleShip.IsInvincible -= 1;
            SampleShip.IsReflectBullets -= 1;
            SampleShip.Physics.Velocity = Vector2.Zero;

            IsFlyToStage = false;
            IsMoveToStage = false;
        }

        private void LockWorkingProfile()
        {
            LockedProfile = WorkingProfile;
            
            // TODO: sample ship fly
        }

        public void SaveLockedProfile()
        {
            SProfile profile = CSaveData.GetCurrentProfile();
            profile.Game[Game.PlayersInGame - 1].Pilots[(int)ControllerIndex] = LockedProfile;
            CSaveData.SetCurrentProfileData(profile);
            CSaveData.SaveRequest();
        }

        private string GetWeaponDisplayString(string typename, int level)
        {
            // TODO: reduce garbage
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

        private int GetWeaponPowerDisplayValue(string typename, int level)
        {
            if (String.IsNullOrEmpty(typename))
                return 0;

            float damage = WeaponDefinitions.Items[typename].Data[level][0].Damage;
            float adjusted = damage * WeaponDefinitions.Items[typename].Data[level].Count;

            if (typename == "Beam")
            {
                // hits per frame, make it look more damaging
                adjusted += damage * level * 4.0f;
            }
            if (typename == "Boomerang")
            {
                // hits multiple times, give an estimated average damage per enemy
                adjusted *= 4.0f;
            }
            if (typename == "Blade")
            {
                // hits multiple times, give an estimated average damage per enemy
                adjusted *= 4.0f;
            }

            return Convert.ToInt32(adjusted * 100.0f);
        }

        private int GetWeaponReloadDisplayValue(string typename, int level)
        {
            if (String.IsNullOrEmpty(typename))
                return 0;

            //float rate = 1.5f - WeaponDefinitions.Items[typename].Data[level][0].ReloadTime;
            //float adjusted = rate * WeaponDefinitions.Items[typename].Data[level].Count;
            float adjusted = WeaponDefinitions.Items[typename].Data[level][0].ReloadTime;

            if (typename == "Beam")
            {
                adjusted = 1.0f;
            }

            return Convert.ToInt32(adjusted * 100.0f);
        }

        private int GetWeaponEnergyDisplayValue(string typename, int level)
        {
            if (String.IsNullOrEmpty(typename))
                return 0;

            float energy = WeaponDefinitions.Items[typename].Data[level][0].Energy;
            float adjusted = energy * WeaponDefinitions.Items[typename].Data[level].Count;

            if (typename == "Beam")
            {
                adjusted += WeaponDefinitions.Items[typename].Data[level][0].ToggleEnergyDrain * 50.0f;
            }

            return Convert.ToInt32(adjusted * 100.0f);
        }

        private void DrawMenuBaseErrata()
        {
            Vector2 info_position = GetInfoPosition();
            Vector2 text_position = info_position + GetInfoTextOffset();
            Vector2 text_line0 = new Vector2(0.0f, 30.0f);
            Vector2 text00 = text_position + text_line0 * 0.0f;
            Vector2 text01 = text_position + text_line0 * 1.0f;
            Vector2 text_line1 = new Vector2(30.0f, 0.0f);
            Vector2 text_header_split = new Vector2(0.0f, 70.0f);
            Vector2 text_section = new Vector2(0.0f, 30.0f);
            Vector2 text10 = text_position + text_line1 * 0.0f + text_section * 1.0f + text_header_split;
            Vector2 text11 = text_position + text_line1 * 1.0f + text_section * 1.0f + text_header_split;
            Vector2 text20 = text_position + text_line1 * 0.0f + text_section * 2.0f + text_header_split;
            Vector2 text21 = text_position + text_line1 * 1.0f + text_section * 2.0f + text_header_split;
            Vector2 text30 = text_position + text_line1 * 0.0f + text_section * 3.0f + text_header_split;
            Vector2 text31 = text_position + text_line1 * 1.0f + text_section * 3.0f + text_header_split;
            Color text_color = new Color(160, 160, 160);
            Color text_color_upgrade = Color.Green;
            Color text_color_downgrade = Color.DarkRed;

            Vector2 textw0 = text_position + text_line0 * 2.0f;
            Vector2 textw1 = text_position + text_line0 * 3.0f;

            // delete me
            Vector2 text_line = text_line0;

            SpriteEffects flip = ControllerIndex == GameControllerIndex.Two ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            Game.DefaultSpriteBatch.Draw(ShopUpgradePanelTexture, info_position, null, Color.White, 0.0f, Vector2.Zero, 1.0f, flip, 0.0f);

            Labels.Info0Data.Suffix = "";
            Labels.Info1Data.Suffix = "";
            Labels.Info2Data.Suffix = "";

            if (ItemMenu == MenuPrimaryWeapon)
            {
                int max = CWeaponFactory.GetMaxLevel(WorkingProfile.WeaponPrimaryType);
                int level = WorkingProfile.WeaponPrimaryLevel;
                CMenu.CMenuOption option = ItemMenu.MenuOptions[ItemMenu.Cursor];
                bool is_current = (string)option.Data == LockedProfile.WeaponPrimaryType && option.AxisValue == LockedProfile.WeaponPrimaryLevel;

                if (WorkingProfile.WeaponPrimaryType != "")
                {
                    int price = CWeaponFactory.GetPriceForLevel(WorkingProfile.WeaponPrimaryType, WorkingProfile.WeaponPrimaryLevel);

                    if (is_current)
                    {
                        Labels.ItemPrice.Value = "equipped";
                        Labels.ItemPrice.Suffix = "";
                    }
                    else
                    {
                        Labels.ItemPrice.Value = price;
                        Labels.ItemPrice.Suffix = "￥";
                    }

                    Labels.ItemHeader.Value = GetWeaponDisplayString(WorkingProfile.WeaponPrimaryType, level);

                    Labels.ItemHeader.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, text00, text_color);
                    Labels.ItemPrice.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, text01, text_color);

                    int working_power = GetWeaponPowerDisplayValue(WorkingProfile.WeaponPrimaryType, WorkingProfile.WeaponPrimaryLevel);
                    int locked_power = GetWeaponPowerDisplayValue(LockedProfile.WeaponPrimaryType, LockedProfile.WeaponPrimaryLevel);
                    bool is_upgrade_power = working_power > locked_power;
                    bool is_same_power = working_power == locked_power;
                    Color color_power = is_upgrade_power ? text_color_upgrade : (is_same_power ? text_color : text_color_downgrade);
                    Labels.Info0Name.Value = "Power";
                    Labels.Info0Data.Value = GetWeaponPowerDisplayValue(WorkingProfile.WeaponPrimaryType, WorkingProfile.WeaponPrimaryLevel);
                    Labels.Info0Name.Suffix = is_upgrade_power ? "  ↑" : (is_same_power ? "" : "  ↓");
                    Labels.Info0Name.SuffixColor = color_power;
                    Labels.Info0Name.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, text10, text_color);
                    Labels.Info0Data.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, text11, text_color);

                    int working_rate = GetWeaponReloadDisplayValue(WorkingProfile.WeaponPrimaryType, WorkingProfile.WeaponPrimaryLevel);
                    int locked_rate = GetWeaponReloadDisplayValue(LockedProfile.WeaponPrimaryType, LockedProfile.WeaponPrimaryLevel);
                    bool is_upgrade_rate = working_rate > locked_rate;
                    bool is_same_rate = working_rate == locked_rate;
                    Color color_rate = is_upgrade_rate ? text_color_upgrade : (is_same_rate ? text_color : text_color_downgrade);
                    Labels.Info1Name.Value = "Reload";
                    Labels.Info1Data.Value = GetWeaponReloadDisplayValue(WorkingProfile.WeaponPrimaryType, WorkingProfile.WeaponPrimaryLevel);
                    Labels.Info1Name.Suffix = is_upgrade_rate ? "  ↑" : (is_same_rate ? "" : "  ↓");
                    Labels.Info1Name.SuffixColor = color_rate;
                    Labels.Info1Name.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, text20, text_color);
                    Labels.Info1Data.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, text21, text_color);

                    int working_energy = GetWeaponEnergyDisplayValue(WorkingProfile.WeaponPrimaryType, WorkingProfile.WeaponPrimaryLevel);
                    int locked_energy = GetWeaponEnergyDisplayValue(LockedProfile.WeaponPrimaryType, LockedProfile.WeaponPrimaryLevel);
                    bool is_upgrade_energy = working_energy > locked_energy;
                    bool is_same_energy = working_energy == locked_energy;
                    Color color_energy = is_upgrade_energy ? text_color_upgrade : (is_same_energy ? text_color : text_color_downgrade);
                    Labels.Info2Name.Value = "Energy";
                    Labels.Info2Data.Value = GetWeaponEnergyDisplayValue(WorkingProfile.WeaponPrimaryType, WorkingProfile.WeaponPrimaryLevel);
                    Labels.Info2Name.Suffix = is_upgrade_energy ? "  ↑" : (is_same_energy ? "" : "  ↓");
                    Labels.Info2Name.SuffixColor = color_energy;
                    Labels.Info2Name.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, text30, text_color);
                    Labels.Info2Data.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, text31, text_color);

                    bool valid = SelectValidatePrimaryWeapon(ItemMenu.MenuOptions[ItemMenu.Cursor].Data) || WorkingProfile.WeaponPrimaryType == "None";
                    DrawPurchasePanel(valid && !is_current);
                }
            }
            else if (ItemMenu == MenuSecondaryWeapon)
            {
                int max = CWeaponFactory.GetMaxLevel(WorkingProfile.WeaponSecondaryType);
                int level = WorkingProfile.WeaponSecondaryLevel;
                CMenu.CMenuOption option = ItemMenu.MenuOptions[ItemMenu.Cursor];
                bool is_current = (string)option.Data == LockedProfile.WeaponSecondaryType && option.AxisValue == LockedProfile.WeaponSecondaryLevel;

                if (WorkingProfile.WeaponSecondaryType != "")
                {
                    int price = CWeaponFactory.GetPriceForLevel(WorkingProfile.WeaponSecondaryType, WorkingProfile.WeaponSecondaryLevel);
                    int next_price = CWeaponFactory.GetPriceForLevel(WorkingProfile.WeaponSecondaryType, WorkingProfile.WeaponSecondaryLevel + 1);

                    if (is_current)
                    {
                        Labels.ItemPrice.Value = "equipped";
                        Labels.ItemPrice.Suffix = "";
                    }
                    else
                    {
                        Labels.ItemPrice.Value = price;
                        Labels.ItemPrice.Suffix = "￥";
                    }

                    Labels.ItemHeader.Value = GetWeaponDisplayString(WorkingProfile.WeaponSecondaryType, level);

                    Labels.ItemHeader.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, text00, text_color);
                    Labels.ItemPrice.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, text01, text_color);

                    int working_power = GetWeaponPowerDisplayValue(WorkingProfile.WeaponSecondaryType, WorkingProfile.WeaponSecondaryLevel);
                    int locked_power = GetWeaponPowerDisplayValue(LockedProfile.WeaponSecondaryType, LockedProfile.WeaponSecondaryLevel);
                    bool is_upgrade_power = working_power > locked_power;
                    bool is_same_power = working_power == locked_power;
                    Color color_power = is_upgrade_power ? text_color_upgrade : (is_same_power ? text_color : text_color_downgrade);
                    Labels.Info0Name.Value = "Power";
                    Labels.Info0Data.Value = GetWeaponPowerDisplayValue(WorkingProfile.WeaponSecondaryType, WorkingProfile.WeaponSecondaryLevel);
                    Labels.Info0Name.Suffix = is_upgrade_power ? "  ↑" : (is_same_power ? "" : "  ↓");
                    Labels.Info0Name.SuffixColor = color_power;
                    Labels.Info0Name.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, text10, text_color);
                    Labels.Info0Data.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, text11, text_color);

                    int working_rate = GetWeaponReloadDisplayValue(WorkingProfile.WeaponSecondaryType, WorkingProfile.WeaponSecondaryLevel);
                    int locked_rate = GetWeaponReloadDisplayValue(LockedProfile.WeaponSecondaryType, LockedProfile.WeaponSecondaryLevel);
                    bool is_upgrade_rate = working_rate > locked_rate;
                    bool is_same_rate = working_rate == locked_rate;
                    Color color_rate = is_upgrade_rate ? text_color_upgrade : (is_same_rate ? text_color : text_color_downgrade);
                    Labels.Info1Name.Value = "Reload";
                    Labels.Info1Data.Value = GetWeaponReloadDisplayValue(WorkingProfile.WeaponSecondaryType, WorkingProfile.WeaponSecondaryLevel);
                    Labels.Info1Name.Suffix = is_upgrade_rate ? "  ↑" : (is_same_rate ? "" : "  ↓");
                    Labels.Info1Name.SuffixColor = color_rate;
                    Labels.Info1Name.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, text20, text_color);
                    Labels.Info1Data.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, text21, text_color);

                    int working_energy = GetWeaponEnergyDisplayValue(WorkingProfile.WeaponSecondaryType, WorkingProfile.WeaponSecondaryLevel);
                    int locked_energy = GetWeaponEnergyDisplayValue(LockedProfile.WeaponSecondaryType, LockedProfile.WeaponSecondaryLevel);
                    bool is_upgrade_energy = working_energy > locked_energy;
                    bool is_same_energy = working_energy == locked_energy;
                    Color color_energy = is_upgrade_energy ? text_color_upgrade : (is_same_energy ? text_color : text_color_downgrade);
                    Labels.Info2Name.Value = "Energy";
                    Labels.Info2Data.Value = GetWeaponEnergyDisplayValue(WorkingProfile.WeaponSecondaryType, WorkingProfile.WeaponSecondaryLevel);
                    Labels.Info2Name.Suffix = is_upgrade_energy ? "  ↑" : (is_same_energy ? "" : "  ↓");
                    Labels.Info2Name.SuffixColor = color_energy;
                    Labels.Info2Name.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, text30, text_color);
                    Labels.Info2Data.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, text31, text_color);


                    bool valid = SelectValidateSecondaryWeapon(ItemMenu.MenuOptions[ItemMenu.Cursor].Data);
                    DrawPurchasePanel(valid && !is_current);
                }
                else
                {
                    Labels.ItemHeader.Value = "None";
                    Labels.ItemHeader.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, textw0, text_color);
                    DrawPurchasePanel(!is_current);
                }
            }
            else if (ItemMenu == MenuSidekick)
            {
                CMenu.CMenuOption option = ItemMenu.MenuOptions[ItemMenu.Cursor];
                bool is_current = (string)option.Data == LockedProfile.WeaponSidekickType;

                if (WorkingProfile.WeaponSidekickType != "")
                {
                    int price = CWeaponFactory.GetPriceForLevel(WorkingProfile.WeaponSidekickType, WorkingProfile.WeaponSidekickLevel);

                    if (is_current)
                    {
                        Labels.ItemPrice.Value = "equipped";
                        Labels.ItemPrice.Suffix = "";
                    }
                    else
                    {
                        Labels.ItemPrice.Value = price;
                        Labels.ItemPrice.Suffix = "￥";
                    }

                    Labels.ItemHeader.Value = CWeaponFactory.GetDisplayName(WorkingProfile.WeaponSidekickType);

                    Labels.ItemHeader.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, text00, text_color);
                    Labels.ItemPrice.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, text01, text_color);

                    int working_power = GetWeaponPowerDisplayValue(WorkingProfile.WeaponSidekickType, WorkingProfile.WeaponSidekickLevel);
                    int locked_power = GetWeaponPowerDisplayValue(LockedProfile.WeaponSidekickType, LockedProfile.WeaponSidekickLevel);
                    bool is_upgrade_power = working_power > locked_power;
                    bool is_same_power = working_power == locked_power;
                    Color color_power = is_upgrade_power ? text_color_upgrade : (is_same_power ? text_color : text_color_downgrade);
                    Labels.Info0Name.Value = "Power";
                    Labels.Info0Data.Value = GetWeaponPowerDisplayValue(WorkingProfile.WeaponSidekickType, WorkingProfile.WeaponSidekickLevel);
                    Labels.Info0Name.Suffix = is_upgrade_power ? "  ↑" : (is_same_power ? "" : "  ↓");
                    Labels.Info0Name.SuffixColor = color_power;
                    Labels.Info0Name.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, text10, text_color);
                    Labels.Info0Data.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, text11, text_color);

                    int working_rate = GetWeaponReloadDisplayValue(WorkingProfile.WeaponSidekickType, WorkingProfile.WeaponSidekickLevel);
                    int locked_rate = GetWeaponReloadDisplayValue(LockedProfile.WeaponSidekickType, LockedProfile.WeaponSidekickLevel);
                    bool is_upgrade_rate = working_rate > locked_rate;
                    bool is_same_rate = working_rate == locked_rate;
                    Color color_rate = is_upgrade_rate ? text_color_upgrade : (is_same_rate ? text_color : text_color_downgrade);
                    Labels.Info1Name.Value = "Reload";
                    Labels.Info1Data.Value = GetWeaponReloadDisplayValue(WorkingProfile.WeaponSidekickType, WorkingProfile.WeaponSidekickLevel);
                    Labels.Info1Name.Suffix = is_upgrade_rate ? "  ↑" : (is_same_rate ? "" : "  ↓");
                    Labels.Info1Name.SuffixColor = color_rate;
                    Labels.Info1Name.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, text20, text_color);
                    Labels.Info1Data.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, text21, text_color);

                    int working_energy = GetWeaponEnergyDisplayValue(WorkingProfile.WeaponSidekickType, WorkingProfile.WeaponSidekickLevel);
                    int locked_energy = GetWeaponEnergyDisplayValue(LockedProfile.WeaponSidekickType, LockedProfile.WeaponSidekickLevel);
                    bool is_upgrade_energy = working_energy > locked_energy;
                    bool is_same_energy = working_energy == locked_energy;
                    Color color_energy = is_upgrade_energy ? text_color_upgrade : (is_same_energy ? text_color : text_color_downgrade);
                    Labels.Info2Name.Value = "Energy";
                    Labels.Info2Data.Value = GetWeaponEnergyDisplayValue(WorkingProfile.WeaponSidekickType, WorkingProfile.WeaponSidekickLevel);
                    Labels.Info2Name.Suffix = is_upgrade_energy ? "  ↑" : (is_same_energy ? "" : "  ↓");
                    Labels.Info2Name.SuffixColor = color_energy;
                    Labels.Info2Name.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, text30, text_color);
                    Labels.Info2Data.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, text31, text_color);

                    bool valid = SelectValidateSidekick(ItemMenu.MenuOptions[ItemMenu.Cursor].Data);
                    DrawPurchasePanel(valid && !is_current);
                }
                else
                {
                    Labels.ItemHeader.Value = "None";
                    Labels.ItemHeader.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, textw0, text_color);
                    DrawPurchasePanel(!is_current);
                }
            }
            else if (ItemMenu == MenuChassis)
            {
                CChassisPart part = ChassisDefinitions.GetPart(WorkingProfile.ChassisType);
                CMenu.CMenuOption option = ItemMenu.MenuOptions[ItemMenu.Cursor];
                bool is_current = (string)option.Data == LockedProfile.ChassisType;

                Labels.ItemHeader.Value = WorkingProfile.ChassisType;
                if (is_current)
                {
                    Labels.ItemPrice.Value = "equipped";
                    Labels.ItemPrice.Suffix = "";
                }
                else
                {
                    Labels.ItemPrice.Value = part.Price;
                    Labels.ItemPrice.Suffix = "￥";
                }

                Labels.ItemPrice.Suffix = is_current ? "" : "￥";
                Labels.ItemHeader.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, text00, text_color);
                Labels.ItemPrice.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, text01, text_color);

                int working_armor = Convert.ToInt32(part.Armor * 100.0f);
                int locked_armor = Convert.ToInt32(ChassisDefinitions.GetPart(LockedProfile.ChassisType).Armor * 100.0f);
                bool is_upgrade_armor = working_armor > locked_armor;
                bool is_same_armor = working_armor == locked_armor;
                Color color_armor = is_upgrade_armor ? text_color_upgrade : (is_same_armor ? text_color : text_color_downgrade);
                Labels.Info0Name.Value = "Armor";
                Labels.Info0Data.Value = Convert.ToInt32(part.Armor * 100.0f);
                Labels.Info0Name.Suffix = is_upgrade_armor ? "  ↑" : (is_same_armor ? "" : "  ↓");
                Labels.Info0Name.SuffixColor = color_armor;
                Labels.Info0Name.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, text10, text_color);
                Labels.Info0Data.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, text11, text_color);

                int working_speed = Convert.ToInt32(part.Speed * 100.0f);
                int locked_speed = Convert.ToInt32(ChassisDefinitions.GetPart(LockedProfile.ChassisType).Speed * 100.0f);
                bool is_upgrade_speed = working_speed > locked_speed;
                bool is_same_speed = working_speed == locked_speed;
                Color color_speed = is_upgrade_speed ? text_color_upgrade : (is_same_speed ? text_color : text_color_downgrade);
                Labels.Info1Name.Value = "Speed";
                Labels.Info1Data.Value = Convert.ToInt32(part.Speed * 100.0f);
                Labels.Info1Name.Suffix = is_upgrade_speed ? "  ↑" : (is_same_speed ? "" : "  ↓");
                Labels.Info1Name.SuffixColor = color_speed;
                Labels.Info1Name.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, text20, text_color);
                Labels.Info1Data.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, text21, text_color);

                int working_density = Convert.ToInt32(part.CollisionResistance * 100.0f);
                int locked_density = Convert.ToInt32(ChassisDefinitions.GetPart(LockedProfile.ChassisType).CollisionResistance * 100.0f);
                bool is_upgrade_density = working_density > locked_density;
                bool is_same_density = working_density == locked_density;
                Color color_density = is_upgrade_density ? text_color_upgrade : (is_same_density ? text_color : text_color_downgrade);
                Labels.Info2Name.Value = "Density";
                Labels.Info2Data.Value = Convert.ToInt32(part.CollisionResistance * 100.0f);
                Labels.Info2Data.Suffix = "%";
                Labels.Info2Name.Suffix = is_upgrade_density ? "  ↑" : (is_same_density ? "" : "  ↓");
                Labels.Info2Name.SuffixColor = color_density;
                Labels.Info2Name.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, text30, text_color);
                Labels.Info2Data.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, text31, text_color);

                bool valid = SelectValidateChassis(ItemMenu.MenuOptions[ItemMenu.Cursor].Data);
                DrawPurchasePanel(valid && !is_current);
            }
            else if (ItemMenu == MenuGenerator)
            {
                CGeneratorPart part = GeneratorDefinitions.GetPart(WorkingProfile.GeneratorType);
                CMenu.CMenuOption option = ItemMenu.MenuOptions[ItemMenu.Cursor];
                bool is_current = (string)option.Data == LockedProfile.GeneratorType;

                Labels.ItemHeader.Value = WorkingProfile.GeneratorType;
                if (is_current)
                {
                    Labels.ItemPrice.Value = "equipped";
                    Labels.ItemPrice.Suffix = "";
                }
                else
                {
                    Labels.ItemPrice.Value = part.Price;
                    Labels.ItemPrice.Suffix = "￥";
                }

                Labels.ItemPrice.Suffix = is_current ? "" : "￥";
                Labels.ItemHeader.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, text00, text_color);
                Labels.ItemPrice.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, text01, text_color);

                int working_energy = Convert.ToInt32(part.Energy * 100.0f);
                int locked_energy = Convert.ToInt32(GeneratorDefinitions.GetPart(LockedProfile.GeneratorType).Energy * 100.0f);
                bool is_upgrade_energy = working_energy > locked_energy;
                bool is_same_energy = working_energy == locked_energy;
                Color color_energy = is_upgrade_energy ? text_color_upgrade : (is_same_energy ? text_color : text_color_downgrade);
                Labels.Info0Name.Value = "Energy";
                Labels.Info0Data.Value = Convert.ToInt32(part.Energy * 100.0f);
                Labels.Info0Name.Suffix = is_upgrade_energy ? "  ↑" : (is_same_energy ? "" : "  ↓");
                Labels.Info0Name.SuffixColor = color_energy;
                Labels.Info0Name.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, text10, text_color);
                Labels.Info0Data.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, text11, text_color);

                int working_regen = Convert.ToInt32(part.Regen * 100.0f);
                int locked_regen = Convert.ToInt32(GeneratorDefinitions.GetPart(LockedProfile.GeneratorType).Regen * 100.0f);
                bool is_upgrade_regen = working_regen > locked_regen;
                bool is_same_regen = working_regen == locked_regen;
                Color color_regen = is_upgrade_regen ? text_color_upgrade : (is_same_regen ? text_color : text_color_downgrade);
                Labels.Info1Name.Value = "Regen";
                Labels.Info1Data.Value = Convert.ToInt32(part.Regen * 100.0f);
                Labels.Info1Name.Suffix = is_upgrade_regen ? "  ↑" : (is_same_regen ? "" : "  ↓");
                Labels.Info1Name.SuffixColor = color_regen;
                Labels.Info1Name.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, text20, text_color);
                Labels.Info1Data.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, text21, text_color);

                if (!String.IsNullOrEmpty(part.Description))
                {
                    Labels.DescValue.Value = part.Description;
                    //Labels.DescHeader.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, text30, text_color);
                    Labels.DescValue.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, text31 + new Vector2(-16.0f, 0.0f), text_color);
                }

                bool valid = SelectValidateGenerator(ItemMenu.MenuOptions[ItemMenu.Cursor].Data);
                DrawPurchasePanel(valid && !is_current);
            }
            else if (ItemMenu == MenuShield)
            {
                CShieldPart part = ShieldDefinitions.GetPart(WorkingProfile.ShieldType);
                CMenu.CMenuOption option = ItemMenu.MenuOptions[ItemMenu.Cursor];
                bool is_current = (string)option.Data == LockedProfile.ShieldType;

                Labels.ItemHeader.Value = WorkingProfile.ShieldType;
                if (is_current)
                {
                    Labels.ItemPrice.Value = "equipped";
                    Labels.ItemPrice.Suffix = "";
                }
                else
                {
                    Labels.ItemPrice.Value = part.Price;
                    Labels.ItemPrice.Suffix = "￥";
                }

                Labels.ItemHeader.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, text00, text_color);
                Labels.ItemPrice.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, text01, text_color);

                int working_shield = Convert.ToInt32(part.Shield * 100.0f);
                int locked_shield = Convert.ToInt32(ShieldDefinitions.GetPart(LockedProfile.ShieldType).Shield * 100.0f);
                bool is_upgrade_shield = working_shield > locked_shield;
                bool is_same_shield = working_shield == locked_shield;
                Color color_shield = is_upgrade_shield ? text_color_upgrade : (is_same_shield ? text_color : text_color_downgrade);
                Labels.Info0Name.Value = "Shield";
                Labels.Info0Data.Value = Convert.ToInt32(part.Shield * 100.0f);
                Labels.Info0Name.Suffix = is_upgrade_shield ? "  ↑" : (is_same_shield ? "" : "  ↓");
                Labels.Info0Name.SuffixColor = color_shield;
                Labels.Info0Name.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, text10, text_color);
                Labels.Info0Data.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, text11, text_color);

                int working_regen = Convert.ToInt32(part.EnergyDrain * 100.0f);
                int locked_regen = Convert.ToInt32(ShieldDefinitions.GetPart(LockedProfile.ShieldType).EnergyDrain * 100.0f);
                bool is_upgrade_regen = working_regen > locked_regen;
                bool is_same_regen = working_regen == locked_regen;
                Color color_regen = is_upgrade_regen ? text_color_upgrade : (is_same_regen ? text_color : text_color_downgrade);
                Labels.Info1Name.Value = "Regen";
                Labels.Info1Data.Value = Convert.ToInt32(part.EnergyDrain * 100.0f);
                Labels.Info1Name.Suffix = is_upgrade_regen ? "  ↑" : (is_same_regen ? "" : "  ↓");
                Labels.Info1Name.SuffixColor = color_regen;
                Labels.Info1Name.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, text20, text_color);
                Labels.Info1Data.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, text21, text_color);

                int working_efficiency = Convert.ToInt32(part.Efficiency * 100.0f);
                int locked_efficiency = Convert.ToInt32(ShieldDefinitions.GetPart(LockedProfile.ShieldType).Efficiency * 100.0f);
                bool is_upgrade_efficiency = working_efficiency > locked_efficiency;
                bool is_same_efficiency = working_efficiency == locked_efficiency;
                Color color_efficiency = is_upgrade_efficiency ? text_color_upgrade : (is_same_efficiency ? text_color : text_color_downgrade);
                Labels.Info2Name.Value = "Efficiency";
                Labels.Info2Data.Value = Convert.ToInt32(part.Efficiency * 100.0f);
                Labels.Info2Data.Suffix = "%";
                Labels.Info2Name.Suffix = is_upgrade_efficiency ? "  ↑" : (is_same_efficiency ? "" : "  ↓");
                Labels.Info2Name.SuffixColor = color_efficiency;
                Labels.Info2Name.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, text30, text_color);
                Labels.Info2Data.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, text31, text_color);

                bool valid = SelectValidateShield(ItemMenu.MenuOptions[ItemMenu.Cursor].Data);
                DrawPurchasePanel(valid && !is_current);
            }
            else
            {
                ShowHelpInfo();
            }
        }

        private void ShowHelpInfo()
        {
            Vector2 info_position = GetInfoPosition();
            Vector2 text_position = info_position + GetInfoTextOffset();

            string[] strings = {
                "Welcome to Shop",
                "Purchase upgrades",
                "and weapons here",
                "",
            };

            if (ItemMenu == null)
            {
                switch (CategoryMenu.Cursor)
                {
                    case 0: // chassis
                        strings[0] = "Chassis";
                        strings[1] = "Upgrade for improved";
                        strings[2] = "hull armor and speed";
                        break;

                    case 1: // generator
                        strings[0] = "Generator";
                        strings[1] = "Provides energy for";
                        strings[2] = "weapons and shield";
                        break;

                    case 2: // shield
                        strings[0] = "Shield";
                        strings[1] = "Upgrade for improved";
                        strings[2] = "shield and regen";
                        break;

                    case 3: // primary
                        strings[0] = "Primary Weapon";
                        strings[1] = "Ship main weapon";
                        strings[2] = "Make it strong";
                        break;

                    case 4: // secondary
                        strings[0] = "Support Weapon";
                        strings[1] = "Auxilary firepower";
                        strings[2] = "Use without caution";
                        break;

                    case 5: // sidekick
                        strings[0] = "Sidekick Weapon";
                        strings[1] = "Increase firepower";
                        strings[2] = "further with sidekicks";
                        break;

                    default:
                        break;
                }
            }

            float offset = 0.0f;
            Color color = new Color(160, 160, 160);
            Game.DefaultSpriteBatch.DrawStringAlignCenter(Game.GameRegularFont, text_position + new Vector2(0.0f, 30.0f + offset), strings[0], color);
            Game.DefaultSpriteBatch.DrawStringAlignCenter(Game.GameRegularFont, text_position + new Vector2(0.0f, 70.0f + offset), strings[1], color);
            Game.DefaultSpriteBatch.DrawStringAlignCenter(Game.GameRegularFont, text_position + new Vector2(0.0f, 100.0f + offset), strings[2], color);
            Game.DefaultSpriteBatch.DrawStringAlignCenter(Game.GameRegularFont, text_position + new Vector2(0.0f, 160.0f + offset), strings[3], color);
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
            LockedProfile.Money = WorkingProfile.Money;
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

            LockWorkingProfile();
            RefreshSampleDisplay();
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
                new Vector2(x * 0.707f, y * 0.351f);
        }

        private Vector2 GetMenuPanelOffset()
        {
            float x = Game.Resolution.X;
            float y = Game.Resolution.Y;
            return ControllerIndex == GameControllerIndex.One ?
                new Vector2(x * 0.065f, y * 0.060f) :
                new Vector2(x * 0.047f, y * 0.060f);
        }

        private Vector2 GetInfoPosition()
        {
            float x = Game.Resolution.X;
            float y = Game.Resolution.Y;
            return ControllerIndex == GameControllerIndex.One ?
                new Vector2(x * 0.020f, y * 0.400f) :
                new Vector2(x * 0.765f, y * 0.400f);
        }

        private Vector2 GetInfoTextOffset()
        {
            float x = Game.Resolution.X;
            float y = Game.Resolution.Y;
            return ControllerIndex == GameControllerIndex.One ?
                new Vector2(x * 0.103f, y * 0.031f) :
                new Vector2(x * 0.103f, y * 0.031f);
        }

        private void DrawPurchasePanel(bool valid)
        {
            // just early out?
            if (!valid)
                return;

            Vector2 menu_position = GetMenuPosition();
            Vector2 panel_position = menu_position + new Vector2(-54.0f, 554.0f);
            Texture2D texture = valid ? ShopPurchasePanelTexture : ShopPurchasePanelInvalidTexture;
            Game.DefaultSpriteBatch.Draw(texture, panel_position, Color.White);
            ShopPurchaseTextLabel.Alignment = CTextLabel.EAlignment.Left;
            float t = valid ? SampleShip.World.Game.GameFrame * 0.1f : 0.0f;
            float scale = 1.0f + (float)(Math.Abs(Math.Sin(t * 0.4f))) * 0.025f;
            Color color = valid ? new Color(160, 160, 160) : new Color(110, 110, 110);
            ShopPurchaseTextLabel.Draw(Game.DefaultSpriteBatch, Game.GameRegularFont, panel_position + new Vector2(60.0f, 42.0f), color, scale);
            //Game.DefaultSpriteBatch.DrawString(Game.GameRegularFont, "Purchase", panel_position + new Vector2(58.0f, 24.0f), Color.White);
        }

        private void OpenItemMenu(CMenu child)
        {
            ItemMenu = child;
            ItemMenu.ForceRefresh();
            MenuUpdateEquippedIcons();
        }

        private void CloseItemMenu()
        {
            ItemMenu = null;
            RevertWorkingProfile(null);
            RefreshSampleDisplay();
        }

        private void ReturnToMainMenu()
        {
            // ignore if flying to stage, will cancel first
            if (IsFlyToStage)
                return;

            // we should certainly save changes
            SaveLockedProfile();

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
