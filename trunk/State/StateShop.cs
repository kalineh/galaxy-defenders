﻿//
// StateShop.cs
//

using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
        private delegate void DrawMenuErrataFunction();
        private DrawMenuErrataFunction DrawMenuErrata { get; set; }
        private CShip SampleShip { get; set; }
        private SProfile WorkingProfile;
        private SProfile LockedProfile;

        public CStateShop(CGalaxy game)
        {
            Game = game;
            EmptyWorld = new CWorld(game);

            EmptyWorld.Scenery = new CSceneryChain(EmptyWorld,
                new CBackground(EmptyWorld, new Color(0, 0, 0)),
                new CStars(EmptyWorld, CContent.LoadTexture2D(Game, "Textures/Background/Star"), 0.4f, 3.5f),
                new CStars(EmptyWorld, CContent.LoadTexture2D(Game, "Textures/Background/Star"), 0.6f, 2.1f)
            );

            WorkingProfile = CSaveData.GetCurrentProfile();
            LockedProfile = WorkingProfile;

            //
            // Base
            //
            MenuBase = new CMenu(game)
            {
                Position = new Vector2(500.0f, 300.0f),
                MenuOptions = new List<CMenu.MenuOption>()
                {
                    new CMenu.MenuOption() { Text = "Play Next Stage", Select = StageSelect },
                    new CMenu.MenuOption() { Text = "Upgrade Ship", Select = UpgradeShip },
                    new CMenu.MenuOption() { Text = "Return to Menu", Select = Back },
                }
            };

            //
            // Upgrade Ship
            //
            MenuUpgradeShip = new CMenu(game)
            {
                Position = new Vector2(500.0f, 300.0f),
                MenuOptions = new List<CMenu.MenuOption>()
                {
                    new CMenu.MenuOption() { Text = "Chassis", Select = EditChassis },
                    new CMenu.MenuOption() { Text = "Generator", Select = EditGenerator },
                    new CMenu.MenuOption() { Text = "Shield", Select = EditShield },
                    new CMenu.MenuOption() { Text = "Main Weapon", Select = EditPrimaryWeapon },
                    new CMenu.MenuOption() { Text = "Support Weapon", Select = EditSecondaryWeapon },
                    new CMenu.MenuOption() { Text = "Sidekick Left", Select = EditSidekickLeft },
                    new CMenu.MenuOption() { Text = "Sidekick Right", Select = EditSidekickRight },
                    new CMenu.MenuOption() { Text = "Done", Select = ReturnToBaseMenu },
                }
            };

            //
            // Primary Weapon
            //
            MenuPrimaryWeapon = new CMenu(game)
            {
                Position = new Vector2(500.0f, 300.0f),
                MenuOptions = new List<CMenu.MenuOption>(),
            };
            foreach (string weapon_part in CMap.GetMapNodeByStageName(WorkingProfile.CurrentStage).AvailablePrimaryWeaponParts)
            {
                MenuPrimaryWeapon.MenuOptions.Add(
                    new CMenu.MenuOption()
                    {
                        Text = weapon_part,
                        Data = weapon_part,
                        Select = SelectPrimaryWeapon,
                        Highlight = HighlightPrimaryWeapon,
                        Axis = AxisPrimaryWeapon,
                        AxisValidate = AxisValidatePrimaryWeapon,
                        AxisValue = weapon_part == WorkingProfile.WeaponPrimaryType ? WorkingProfile.WeaponPrimaryLevel : 0,
                    }
                );
            }
            MenuPrimaryWeapon.MenuOptions.Add(new CMenu.MenuOption() { Text = "Done", Select = ReturnToUpgradeShip, Highlight = RevertWorkingProfile });

            //
            // Secondary Weapon
            //
            MenuSecondaryWeapon = new CMenu(game)
            {
                Position = new Vector2(500.0f, 300.0f),
                MenuOptions = new List<CMenu.MenuOption>(),
            };

            MenuSecondaryWeapon.MenuOptions.Add(new CMenu.MenuOption() { Text = "None", Select = SelectSecondaryWeaponEmpty, Highlight = HighlightSecondaryWeaponEmpty, Data = "" });
            foreach (string weapon_part in CMap.GetMapNodeByStageName(WorkingProfile.CurrentStage).AvailableSecondaryWeaponParts)
            {
                MenuSecondaryWeapon.MenuOptions.Add(
                    new CMenu.MenuOption()
                    {
                        Text = weapon_part,
                        Data = weapon_part,
                        Select = SelectSecondaryWeapon,
                        Highlight = HighlightSecondaryWeapon,
                        Axis = AxisSecondaryWeapon,
                        AxisValidate = AxisValidateSecondaryWeapon,
                        AxisValue = weapon_part == WorkingProfile.WeaponSecondaryType ? WorkingProfile.WeaponSecondaryLevel : 0,
                    }
                );
            }
            MenuSecondaryWeapon.MenuOptions.Add(new CMenu.MenuOption() { Text = "Done", Select = ReturnToUpgradeShip, Highlight = RevertWorkingProfile });

            //
            // Sidekick Left
            //
            MenuSidekickLeft = new CMenu(game)
            {
                Position = new Vector2(500.0f, 300.0f),
                MenuOptions = new List<CMenu.MenuOption>(),
            };

            MenuSidekickLeft.MenuOptions.Add(new CMenu.MenuOption() { Text = "None", Select = SelectSidekickLeftEmpty, Highlight = HighlightSidekickLeftEmpty, Data = "" });
            foreach (string weapon_part in CMap.GetMapNodeByStageName(WorkingProfile.CurrentStage).AvailableSidekickWeaponParts)
            {
                MenuSidekickLeft.MenuOptions.Add(
                    new CMenu.MenuOption()
                    {
                        Text = weapon_part,
                        Data = weapon_part,
                        Select = SelectSidekickLeft,
                        Highlight = HighlightSidekickLeft,
                    }
                );
            }

            MenuSidekickLeft.MenuOptions.Add(new CMenu.MenuOption() { Text = "Done", Select = ReturnToUpgradeShip, Highlight = RevertWorkingProfile });

            //
            // Sidekick Right
            //
            MenuSidekickRight = new CMenu(game)
            {
                Position = new Vector2(500.0f, 300.0f),
                MenuOptions = new List<CMenu.MenuOption>(),
            };

            MenuSidekickRight.MenuOptions.Add(new CMenu.MenuOption() { Text = "None", Select = SelectSidekickRightEmpty, Data = "" });
            foreach (string weapon_part in CMap.GetMapNodeByStageName(WorkingProfile.CurrentStage).AvailableSidekickWeaponParts)
            {
                MenuSidekickRight.MenuOptions.Add(
                    new CMenu.MenuOption()
                    {
                        Text = weapon_part,
                        Data = weapon_part,
                        Select = SelectSidekickRight,
                        Highlight = HighlightSidekickRight,
                    }
                );
            }
            MenuSidekickRight.MenuOptions.Add(new CMenu.MenuOption() { Text = "Done", Select = ReturnToUpgradeShip, Highlight = RevertWorkingProfile });

            //
            // Chassis
            //
            MenuChassis = new CMenu(game)
            {
                Position = new Vector2(500.0f, 300.0f),
                MenuOptions = new List<CMenu.MenuOption>(),
            };

            foreach (string chassis_part in CMap.GetMapNodeByStageName(WorkingProfile.CurrentStage).AvailableChassisParts)
            {
                MenuChassis.MenuOptions.Add(
                    new CMenu.MenuOption()
                    {
                        Text = chassis_part,
                        Data = chassis_part,
                        Select = SelectChassis,
                        Highlight = HighlightChassis,
                    }
                );
            }
            MenuChassis.MenuOptions.Add(new CMenu.MenuOption() { Text = "Done", Select = ReturnToUpgradeShip, Highlight = RevertWorkingProfile });

            //
            // Generator
            //
            MenuGenerator = new CMenu(game)
            {
                Position = new Vector2(500.0f, 300.0f),
                MenuOptions = new List<CMenu.MenuOption>(),
            };

            foreach (string generator_part in CMap.GetMapNodeByStageName(WorkingProfile.CurrentStage).AvailableGeneratorParts)
            {
                MenuGenerator.MenuOptions.Add(
                    new CMenu.MenuOption()
                    {
                        Text = generator_part,
                        Data = generator_part,
                        Select = SelectGenerator,
                        Highlight = HighlightGenerator,
                    }
                );
            }
            MenuGenerator.MenuOptions.Add(new CMenu.MenuOption() { Text = "Done", Select = ReturnToUpgradeShip, Highlight = RevertWorkingProfile });

            //
            // Shield
            //
            MenuShield = new CMenu(game)
            {
                Position = new Vector2(500.0f, 300.0f),
                MenuOptions = new List<CMenu.MenuOption>(),
            };

            foreach (string shield_part in CMap.GetMapNodeByStageName(WorkingProfile.CurrentStage).AvailableShieldParts)
            {
                MenuShield.MenuOptions.Add(
                    new CMenu.MenuOption()
                    {
                        Text = shield_part,
                        Data = shield_part,
                        Select = SelectShield,
                        Highlight = HighlightShield,
                    }
                );
            }
            MenuShield.MenuOptions.Add(new CMenu.MenuOption() { Text = "Done", Select = ReturnToUpgradeShip, Highlight = RevertWorkingProfile });


            Menu = MenuBase;
            DrawMenuErrata = DrawMenuBaseErrata;

            EmptyWorld.GameCamera.Position = new Vector3(0.0f, 0.0f, 0.0f);

            RefreshSampleDisplay();
        }

        public override void Update()
        {
            Menu.Update();
            EmptyWorld.UpdateEntities();
            EmptyWorld.Scenery.Update();
            SampleShip.UpdateGenerator();
            SampleShip.FireAllWeapons();
            SampleShip.UpdateWeapons();
        }

        public override void Draw()
        {
            Game.GraphicsDevice.Clear(Color.Black);
            EmptyWorld.DrawBackground(EmptyWorld.GameCamera);

            Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.FrontToBack, SaveStateMode.None, EmptyWorld.GameCamera.WorldMatrix);
            Game.DefaultSpriteBatch.End();

            EmptyWorld.DrawEntities(EmptyWorld.GameCamera);

            Game.DefaultSpriteBatch.Begin();
            Menu.Draw(Game.DefaultSpriteBatch);
            DrawMenuErrata();
            Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, "Money: " + WorkingProfile.Money, new Vector2(10.0f, 10.0f), Color.White);
            Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, "Primary: " + WorkingProfile.WeaponPrimaryType + ", level " + WorkingProfile.WeaponPrimaryLevel, new Vector2(10.0f, 50.0f), Color.White);
            Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, "Secondary: " + (WorkingProfile.WeaponSecondaryType ?? "None") + ", level " + WorkingProfile.WeaponSecondaryLevel, new Vector2(10.0f, 70.0f), Color.White);
            Game.DefaultSpriteBatch.End();

            Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.FrontToBack, SaveStateMode.None, EmptyWorld.GameCamera.WorldMatrix);
            SampleShip.Draw(Game.DefaultSpriteBatch);
            Game.DefaultSpriteBatch.End();
        }

        private void RefreshSampleDisplay()
        {
            EmptyWorld.Stop();
            SampleShip = CShipFactory.GenerateShip(EmptyWorld, WorkingProfile);
            SampleShip.Physics.PositionPhysics.Position = new Vector2(-100.0f, 150.0f);
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
            // TODO: implement me!
            return;

            // HACK: bad hack time, fix me up
            // TODO: put stuff in menu itself?
            // TODO: check can upgrade
            // TODO: nicer display of purchasable items
            if (Menu == MenuPrimaryWeapon)
            {
                switch (Menu.Cursor)
                {
                    case 1:
                    {
                        if (CWeaponFactory.CanUpgrade(WorkingProfile.WeaponPrimaryType, WorkingProfile.WeaponPrimaryLevel, 1))
                        {
                            int price = CWeaponFactory.GetPriceForLevel(WorkingProfile.WeaponPrimaryType, WorkingProfile.WeaponPrimaryLevel + 1);
                            Color color = WorkingProfile.Money > price ? Color.White : Color.Red;
                            Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, "Cost: -" + price, new Vector2(10.0f, 30.0f), color);
                        }
                        break;
                    }

                    case 2:
                    {
                        if (CWeaponFactory.CanDowngrade(WorkingProfile.WeaponPrimaryType, WorkingProfile.WeaponPrimaryLevel, 1))
                        {
                            int price = CWeaponFactory.GetPriceForLevel(WorkingProfile.WeaponPrimaryType, WorkingProfile.WeaponPrimaryLevel);
                            Color color = Color.Green;
                            Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, "Cost: +" + price, new Vector2(10.0f, 30.0f), color);
                        }
                        break;
                    }
                        
                }
            }
            if (Menu == MenuSecondaryWeapon)
            {
                switch (Menu.Cursor)
                {
                    case 1:
                    {
                        int price = CWeaponFactory.GetTotalPriceForLevel(WorkingProfile.WeaponSecondaryType, WorkingProfile.WeaponSecondaryLevel);
                        Color color = Color.Green;
                        Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, "Cost: +" + price, new Vector2(10.0f, 30.0f), color);
                        break;
                    }

                    case 2:
                    {
                        if (CWeaponFactory.CanUpgrade(WorkingProfile.WeaponSecondaryType, WorkingProfile.WeaponSecondaryLevel, 1))
                        {
                            int price = CWeaponFactory.GetPriceForLevel(WorkingProfile.WeaponSecondaryType, WorkingProfile.WeaponSecondaryLevel + 1);
                            Color color = WorkingProfile.Money > price ? Color.White : Color.Red;
                            Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, "Cost: -" + price, new Vector2(10.0f, 30.0f), color);
                        }
                        break;
                    }

                    case 3:
                    {
                        if (CWeaponFactory.CanDowngrade(WorkingProfile.WeaponSecondaryType, WorkingProfile.WeaponSecondaryLevel, 1))
                        {
                            int price = CWeaponFactory.GetPriceForLevel(WorkingProfile.WeaponSecondaryType, WorkingProfile.WeaponSecondaryLevel);
                            Color color = Color.Green;
                            Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, "Cost: +" + price, new Vector2(10.0f, 30.0f), color);
                        }
                        break;
                    }
                        
                }
            }
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

        private void EditPrimaryWeapon(object tag)
        {
            Menu = MenuPrimaryWeapon;
        }

        private void EditSecondaryWeapon(object tag)
        {
            Menu = MenuSecondaryWeapon;
        }

        private void EditSidekickLeft(object tag)
        {
            Menu = MenuSidekickLeft;
        }

        private void EditSidekickRight(object tag)
        {
            Menu = MenuSidekickRight;
        }

        private void EditChassis(object tag)
        {
            Menu = MenuChassis;
        }

        private void EditGenerator(object tag)
        {
            Menu = MenuGenerator;
        }

        private void EditShield(object tag)
        {
            Menu = MenuShield;
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

            if (axis < 0 || axis >= CWeaponFactory.GetMaxLevel((string)tag))
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

            if (axis < 0 || axis >= CWeaponFactory.GetMaxLevel((string)tag))
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

        private void HighlightChassis(object tag)
        {
            if (WorkingProfile.ChassisType != (string)tag)
            {
                int sell = ChassisDefinitions.GetPart((string)tag).Price;
                int buy = ChassisDefinitions.GetPart(WorkingProfile.ChassisType).Price;
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

        private void HighlightGenerator(object tag)
        {
            if (WorkingProfile.GeneratorType != (string)tag)
            {
                int sell = GeneratorDefinitions.GetPart((string)tag).Price;
                int buy = GeneratorDefinitions.GetPart(WorkingProfile.GeneratorType).Price;
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

        private void HighlightShield(object tag)
        {
            if (WorkingProfile.ShieldType != (string)tag)
            {
                int sell = ShieldDefinitions.GetPart((string)tag).Price;
                int buy = ShieldDefinitions.GetPart(WorkingProfile.ShieldType).Price;
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

        private void ChassisSwapType(object tag)
        {
            WorkingProfile.ChassisType = ChassisDefinitions.GetNextDefinition(WorkingProfile.ChassisType);
            RefreshSampleDisplay();
        }

        private void GeneratorSwapType(object tag)
        {
            WorkingProfile.GeneratorType = GeneratorDefinitions.GetNextDefinition(WorkingProfile.GeneratorType);
            RefreshSampleDisplay();
        }

        private void ShieldSwapType(object tag)
        {
            WorkingProfile.ShieldType = ShieldDefinitions.GetNextDefinition(WorkingProfile.ShieldType);
            RefreshSampleDisplay();
        }
    }
}
