//
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

        public CStateShop(CGalaxy game)
        {
            Game = game;
            EmptyWorld = new CWorld(game);
            MenuBase = new CMenu(game)
            {
                Position = new Vector2(500.0f, 300.0f),
                MenuOptions = new List<CMenu.MenuOption>()
                {
                    new CMenu.MenuOption() { Text = "Start Game", Function = StartGame },
                    new CMenu.MenuOption() { Text = "Edit Primary Weapon", Function = EditPrimaryWeapon },
                    new CMenu.MenuOption() { Text = "Edit Secondary Weapon", Function = EditSecondaryWeapon },
                    new CMenu.MenuOption() { Text = "Edit Sidekick Left", Function = EditSidekickLeft },
                    new CMenu.MenuOption() { Text = "Edit Sidekick Right", Function = EditSidekickRight },
                    new CMenu.MenuOption() { Text = "Edit Chassis", Function = EditChassis },
                    new CMenu.MenuOption() { Text = "Edit Generator", Function = EditGenerator },
                    new CMenu.MenuOption() { Text = "Edit Shield", Function = EditShield },
                    new CMenu.MenuOption() { Text = "Back", Function = Back },
                }
            };
            MenuPrimaryWeapon = new CMenu(game)
            {
                Position = new Vector2(500.0f, 300.0f),
                MenuOptions = new List<CMenu.MenuOption>()
                {
                    new CMenu.MenuOption() { Text = "Change Type", Function = PrimarySwapType },
                    new CMenu.MenuOption() { Text = "Power Up", Function = PrimaryPowerUp },
                    new CMenu.MenuOption() { Text = "Power Down", Function = PrimaryPowerDown },
                    new CMenu.MenuOption() { Text = "Back", Function = ReturnToBaseMenu },
                }
            };
            MenuSecondaryWeapon = new CMenu(game)
            {
                Position = new Vector2(500.0f, 300.0f),
                MenuOptions = new List<CMenu.MenuOption>()
                {
                    new CMenu.MenuOption() { Text = "Change Type", Function = SecondarySwapType },
                    new CMenu.MenuOption() { Text = "Power Up", Function = SecondaryPowerUp },
                    new CMenu.MenuOption() { Text = "Power Down", Function = SecondaryPowerDown },
                    new CMenu.MenuOption() { Text = "Back", Function = ReturnToBaseMenu },
                }
            };
            MenuSidekickLeft = new CMenu(game)
            {
                Position = new Vector2(500.0f, 300.0f),
                MenuOptions = new List<CMenu.MenuOption>()
                {
                    new CMenu.MenuOption() { Text = "Change Type", Function = SidekickLeftSwapType },
                    new CMenu.MenuOption() { Text = "Back", Function = ReturnToBaseMenu },
                }
            };
            MenuSidekickRight = new CMenu(game)
            {
                Position = new Vector2(500.0f, 300.0f),
                MenuOptions = new List<CMenu.MenuOption>()
                {
                    new CMenu.MenuOption() { Text = "Change Type", Function = SidekickRightSwapType },
                    new CMenu.MenuOption() { Text = "Back", Function = ReturnToBaseMenu },
                }
            };
            MenuChassis = new CMenu(game)
            {
                Position = new Vector2(500.0f, 300.0f),
                MenuOptions = new List<CMenu.MenuOption>()
                {
                    new CMenu.MenuOption() { Text = "Change Type", Function = ChassisSwapType },
                    new CMenu.MenuOption() { Text = "Back", Function = ReturnToBaseMenu },
                }
            };
            MenuGenerator = new CMenu(game)
            {
                Position = new Vector2(500.0f, 300.0f),
                MenuOptions = new List<CMenu.MenuOption>()
                {
                    new CMenu.MenuOption() { Text = "Change Type", Function = GeneratorSwapType },
                    new CMenu.MenuOption() { Text = "Back", Function = ReturnToBaseMenu },
                }
            };
            MenuShield = new CMenu(game)
            {
                Position = new Vector2(500.0f, 300.0f),
                MenuOptions = new List<CMenu.MenuOption>()
                {
                    new CMenu.MenuOption() { Text = "Change Type", Function = ShieldSwapType },
                    new CMenu.MenuOption() { Text = "Back", Function = ReturnToBaseMenu },
                }
            };
            Menu = MenuBase;
            DrawMenuErrata = DrawMenuBaseErrata;

            EmptyWorld.GameCamera.Position = new Vector3(0.0f, 0.0f, 0.0f);

            WorkingProfile = CSaveData.GetCurrentProfile();
            RefreshSampleDisplay();
        }

        public override void Update()
        {
            Menu.Update();
            EmptyWorld.UpdateEntities();
            SampleShip.UpdateGenerator();
            SampleShip.FireAllWeapons();
            SampleShip.UpdateWeapons();
        }

        public override void Draw()
        {
            Game.GraphicsDevice.Clear(Color.Black);

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

        private void DrawMenuBaseErrata()
        {
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

        private void StartGame(object tag)
        {
            CSaveData.SetCurrentProfileData(WorkingProfile);
            CSaveData.Save();
            Game.State = new CStateFadeTo(Game, this, new CStateStageSelect(Game));
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
            CSaveData.SetCurrentProfileData(WorkingProfile);
            CSaveData.Save();
            Game.State = new CStateFadeTo(Game, this, new CStateMainMenu(Game));
        }

        // TODO: replace with selection
        // TODO: money reimburse shouldnt be automagic (will go negative)
        private void PrimarySwapType(object tag)
        {
            string current = WorkingProfile.WeaponPrimaryType;
            string replace = CWeaponFactory.GetNextWeaponInCycle(current, CWeaponFactory.PrimaryWeaponTypes);
            int total = CWeaponFactory.GetTotalPriceForLevel(WorkingProfile.WeaponPrimaryType, WorkingProfile.WeaponPrimaryLevel);
            WorkingProfile.Money += total;
            WorkingProfile.WeaponPrimaryType = replace;
            WorkingProfile.WeaponPrimaryLevel = 0;
            int cost = CWeaponFactory.GetTotalPriceForLevel(WorkingProfile.WeaponPrimaryType, WorkingProfile.WeaponPrimaryLevel);
            WorkingProfile.Money -= cost;
            RefreshSampleDisplay();
        }

        private void PrimaryPowerUp(object tag)
        {
            int current = CWeaponFactory.GetPriceForLevel(WorkingProfile.WeaponPrimaryType, WorkingProfile.WeaponPrimaryLevel);
            int next = CWeaponFactory.GetPriceForLevel(WorkingProfile.WeaponPrimaryType, WorkingProfile.WeaponPrimaryLevel + 1);

            if (WorkingProfile.Money < next)
                return;

            if (!CWeaponFactory.CanUpgrade(WorkingProfile.WeaponPrimaryType, WorkingProfile.WeaponPrimaryLevel, 1))
                return;

            WorkingProfile.Money -= next;
            WorkingProfile.WeaponPrimaryLevel += 1;

            RefreshSampleDisplay();
        }

        private void PrimaryPowerDown(object tag)
        {
            if (!CWeaponFactory.CanDowngrade(WorkingProfile.WeaponPrimaryType, WorkingProfile.WeaponPrimaryLevel, 1))
                return;

            WorkingProfile.Money += CWeaponFactory.GetPriceForLevel(WorkingProfile.WeaponPrimaryType, WorkingProfile.WeaponPrimaryLevel);
            WorkingProfile.WeaponPrimaryLevel -= 1;

            RefreshSampleDisplay();
        }

        // TODO: replace with selection
        // TODO: money reimburse shouldnt be automagic (will go negative)
        private void SecondarySwapType(object tag)
        {
            string current = WorkingProfile.WeaponSecondaryType;
            string replace = CWeaponFactory.GetNextWeaponInCycle(current, CWeaponFactory.SecondaryWeaponTypes);
            if (current != "")
            {
                int total = CWeaponFactory.GetTotalPriceForLevel(WorkingProfile.WeaponSecondaryType, WorkingProfile.WeaponSecondaryLevel);
                WorkingProfile.Money += total;
            }

            WorkingProfile.WeaponSecondaryType = replace;
            WorkingProfile.WeaponSecondaryLevel = 0;
            int cost = CWeaponFactory.GetTotalPriceForLevel(WorkingProfile.WeaponSecondaryType, WorkingProfile.WeaponSecondaryLevel);
            WorkingProfile.Money -= cost;

            RefreshSampleDisplay();
        }

        private void SecondaryPowerUp(object tag)
        {
            int current = CWeaponFactory.GetPriceForLevel(WorkingProfile.WeaponSecondaryType, WorkingProfile.WeaponSecondaryLevel);
            int next = CWeaponFactory.GetPriceForLevel(WorkingProfile.WeaponSecondaryType, WorkingProfile.WeaponSecondaryLevel + 1);

            if (WorkingProfile.Money < next)
                return;

            if (!CWeaponFactory.CanUpgrade(WorkingProfile.WeaponSecondaryType, WorkingProfile.WeaponSecondaryLevel, 1))
                return;

            WorkingProfile.Money -= next;
            WorkingProfile.WeaponSecondaryLevel += 1;

            RefreshSampleDisplay();
        }

        private void SecondaryPowerDown(object tag)
        {
            if (!CWeaponFactory.CanDowngrade(WorkingProfile.WeaponSecondaryType, WorkingProfile.WeaponSecondaryLevel, 1))
                return;

            WorkingProfile.Money += CWeaponFactory.GetPriceForLevel(WorkingProfile.WeaponSecondaryType, WorkingProfile.WeaponSecondaryLevel);
            WorkingProfile.WeaponSecondaryLevel -= 1;

            RefreshSampleDisplay();
        }

        private void SidekickLeftSwapType(object tag)
        {
            string current = WorkingProfile.WeaponSidekickLeftType;
            string replace = CWeaponFactory.GetNextWeaponInCycle(current, CWeaponFactory.SidekickWeaponTypes);
            if (current != "")
            {
                int total = CWeaponFactory.GetTotalPriceForLevel(WorkingProfile.WeaponSidekickLeftType, WorkingProfile.WeaponSidekickLeftLevel);
                WorkingProfile.Money += total;
            }

            WorkingProfile.WeaponSidekickLeftType = replace;
            WorkingProfile.WeaponSidekickLeftLevel = 0;
            int cost = CWeaponFactory.GetTotalPriceForLevel(WorkingProfile.WeaponSidekickLeftType, WorkingProfile.WeaponSidekickLeftLevel);
            WorkingProfile.Money -= cost;

            RefreshSampleDisplay();
        }

        private void SidekickRightSwapType(object tag)
        {
            string current = WorkingProfile.WeaponSidekickRightType;
            string replace = CWeaponFactory.GetNextWeaponInCycle(current, CWeaponFactory.SidekickWeaponTypes);
            if (current != "")
            {
                int total = CWeaponFactory.GetTotalPriceForLevel(WorkingProfile.WeaponSidekickRightType, WorkingProfile.WeaponSidekickRightLevel);
                WorkingProfile.Money += total;
            }

            WorkingProfile.WeaponSidekickRightType = replace;
            WorkingProfile.WeaponSidekickRightLevel = 0;
            int cost = CWeaponFactory.GetTotalPriceForLevel(WorkingProfile.WeaponSidekickRightType, WorkingProfile.WeaponSidekickRightLevel);
            WorkingProfile.Money -= cost;

            RefreshSampleDisplay();
        }

        private void ReturnToBaseMenu(object tag)
        {
            Menu = MenuBase;
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
