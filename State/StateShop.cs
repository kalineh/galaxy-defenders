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
        private CStars Stars { get; set; }
        public CMenu Menu { get; set; }
        private CMenu MenuBase { get; set; }
        private CMenu MenuPrimaryWeapon { get; set; }
        private CMenu MenuSecondaryWeapon { get; set; }
        private delegate void DrawMenuErrataFunction();
        private DrawMenuErrataFunction DrawMenuErrata { get; set; }
        private CShip SampleShip { get; set; }
        private SProfile WorkingProfile;

        public CStateShop(CGalaxy game)
        {
            Game = game;
            EmptyWorld = new CWorld(game);
            Stars = new CStars(EmptyWorld, CContent.LoadTexture2D(Game, "Textures/Background/Star"), 1.0f, 3.0f);
            MenuBase = new CMenu(game)
            {
                Position = new Vector2(500.0f, 300.0f),
                MenuOptions = new List<CMenu.MenuOption>()
                {
                    new CMenu.MenuOption() { Text = "Start Game", Function = StartGame },
                    new CMenu.MenuOption() { Text = "Primary Weapon", Function = EditPrimaryWeapon },
                    new CMenu.MenuOption() { Text = "Secondary Weapon", Function = EditSecondaryWeapon },
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
                    new CMenu.MenuOption() { Text = "Remove", Function = SecondaryRemove },
                    new CMenu.MenuOption() { Text = "Power Up", Function = SecondaryPowerUp },
                    new CMenu.MenuOption() { Text = "Power Down", Function = SecondaryPowerDown },
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
            Stars.Update();
            Menu.Update();
            EmptyWorld.UpdateEntities();
            SampleShip.FireAllWeapons();
            SampleShip.UpdateWeapons();
        }

        public override void Draw()
        {
            Game.GraphicsDevice.Clear(new Color(133, 145, 181));

            Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.FrontToBack, SaveStateMode.None, EmptyWorld.GameCamera.WorldMatrix);
            Stars.Draw(Game.DefaultSpriteBatch);
            Game.DefaultSpriteBatch.End();

            EmptyWorld.DrawEntities(EmptyWorld.GameCamera);

            Game.DefaultSpriteBatch.Begin();
            Menu.Draw(Game.DefaultSpriteBatch);
            DrawMenuErrata();
            Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, "Money: " + WorkingProfile.Money, new Vector2(10.0f, 10.0f), Color.White);
            Game.DefaultSpriteBatch.End();

            Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.FrontToBack, SaveStateMode.None, EmptyWorld.GameCamera.WorldMatrix);
            SampleShip.Draw(Game.DefaultSpriteBatch);
            Game.DefaultSpriteBatch.End();
        }

        private void RefreshSampleDisplay()
        {
            EmptyWorld.Stop();
            SampleShip = new CShip(EmptyWorld, WorkingProfile, new Vector2(-100.0f, 150.0f));
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
                        if (CWeaponFactory.CanUpgrade(WorkingProfile.WeaponPrimaryType, WorkingProfile.WeaponPrimaryLevel))
                        {
                            int price = CWeaponFactory.GetPriceForLevel(WorkingProfile.WeaponPrimaryType, WorkingProfile.WeaponPrimaryLevel + 1);
                            Color color = WorkingProfile.Money > price ? Color.White : Color.Red;
                            Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, "Cost: -" + price, new Vector2(10.0f, 30.0f), color);
                        }
                        break;
                    }

                    case 2:
                    {
                        if (CWeaponFactory.CanDowngrade(WorkingProfile.WeaponPrimaryType, WorkingProfile.WeaponPrimaryLevel))
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
                        if (CWeaponFactory.CanUpgrade(WorkingProfile.WeaponSecondaryType, WorkingProfile.WeaponSecondaryLevel))
                        {
                            int price = CWeaponFactory.GetPriceForLevel(WorkingProfile.WeaponSecondaryType, WorkingProfile.WeaponSecondaryLevel + 1);
                            Color color = WorkingProfile.Money > price ? Color.White : Color.Red;
                            Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, "Cost: -" + price, new Vector2(10.0f, 30.0f), color);
                        }
                        break;
                    }

                    case 3:
                    {
                        if (CWeaponFactory.CanDowngrade(WorkingProfile.WeaponSecondaryType, WorkingProfile.WeaponSecondaryLevel))
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
            string replace = CWeaponFactory.GetNextWeaponInCycle(current);
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

            if (!CWeaponFactory.CanUpgrade(WorkingProfile.WeaponPrimaryType, WorkingProfile.WeaponPrimaryLevel))
                return;

            WorkingProfile.Money -= next;
            WorkingProfile.WeaponPrimaryLevel += 1;

            RefreshSampleDisplay();
        }

        private void PrimaryPowerDown(object tag)
        {
            if (!CWeaponFactory.CanDowngrade(WorkingProfile.WeaponPrimaryType, WorkingProfile.WeaponPrimaryLevel))
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
            string replace = CWeaponFactory.GetNextWeaponInCycle(current);
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

        private void SecondaryRemove(object tag)
        {
            string current = WorkingProfile.WeaponSecondaryType;
            string replace = CWeaponFactory.GetNextWeaponInCycle(current);
            int total = CWeaponFactory.GetTotalPriceForLevel(WorkingProfile.WeaponSecondaryType, WorkingProfile.WeaponSecondaryLevel);
            WorkingProfile.Money += total;
            WorkingProfile.WeaponSecondaryType = "";
            WorkingProfile.WeaponSecondaryLevel = 0;
            RefreshSampleDisplay();
        }

        private void SecondaryPowerUp(object tag)
        {
            int current = CWeaponFactory.GetPriceForLevel(WorkingProfile.WeaponSecondaryType, WorkingProfile.WeaponSecondaryLevel);
            int next = CWeaponFactory.GetPriceForLevel(WorkingProfile.WeaponSecondaryType, WorkingProfile.WeaponSecondaryLevel + 1);

            if (WorkingProfile.Money < next)
                return;

            if (!CWeaponFactory.CanUpgrade(WorkingProfile.WeaponSecondaryType, WorkingProfile.WeaponSecondaryLevel))
                return;

            WorkingProfile.Money -= next;
            WorkingProfile.WeaponSecondaryLevel += 1;

            RefreshSampleDisplay();
        }

        private void SecondaryPowerDown(object tag)
        {
            if (!CWeaponFactory.CanDowngrade(WorkingProfile.WeaponSecondaryType, WorkingProfile.WeaponSecondaryLevel))
                return;

            WorkingProfile.Money += CWeaponFactory.GetPriceForLevel(WorkingProfile.WeaponSecondaryType, WorkingProfile.WeaponSecondaryLevel);
            WorkingProfile.WeaponSecondaryLevel -= 1;

            RefreshSampleDisplay();
        }

        private void ReturnToBaseMenu(object tag)
        {
            Menu = MenuBase;
        }
    }
}
