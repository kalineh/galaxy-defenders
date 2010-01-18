//
// StateShop.cs
//

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
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
        private delegate void DrawMenuErrataFunction(SpriteBatch sprite_batch);
        private DrawMenuErrataFunction DrawMenuErrata { get; set; }
        private CShip SampleShip { get; set; }
        private SProfile WorkingProfile;

        public CStateShop(CGalaxy game)
        {
            Game = game;
            EmptyWorld = new CWorld(game);
            Stars = new CStars(EmptyWorld, Game.Content.Load<Texture2D>("Star"), 1.0f, 3.0f);
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
                    new CMenu.MenuOption() { Text = "Type", Function = PrimarySwapType },
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
                    new CMenu.MenuOption() { Text = "Type", Function = SecondarySwapType },
                    new CMenu.MenuOption() { Text = "Power Up", Function = SecondaryPowerUp },
                    new CMenu.MenuOption() { Text = "Power Down", Function = SecondaryPowerDown },
                    new CMenu.MenuOption() { Text = "Back", Function = ReturnToBaseMenu },
                }
            };
            Menu = MenuBase;
            DrawMenuErrata = DrawMenuBaseErrata;

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

        public override void Draw(SpriteBatch sprite_batch)
        {
            Game.GraphicsDevice.Clear(Color.Black);

            sprite_batch.Begin();

            Stars.Draw(sprite_batch);
            Menu.Draw(sprite_batch);
            DrawMenuErrata(sprite_batch);
            EmptyWorld.DrawEntities(sprite_batch);
            SampleShip.Draw(sprite_batch);

            sprite_batch.End();
        }

        private void RefreshSampleDisplay()
        {
            EmptyWorld.Stop();
            SampleShip = new CShip(EmptyWorld, WorkingProfile, new Vector2(200.0f, 400.0f));
        }

        private void DrawMenuBaseErrata(SpriteBatch sprite_batch)
        {
        }

        private void DrawMenuPrimaryErrata(SpriteBatch sprite_batch)
        {
        }

        private void DrawMenuSecondaryErrata(SpriteBatch sprite_batch)
        {
        }

        private void StartGame()
        {
            CSaveData.SetCurrentProfileData(WorkingProfile);
            CSaveData.Save();
            Game.State = new CStateFadeTo(Game, this, new CStateGame(Game));
        }

        private void EditPrimaryWeapon()
        {
            Menu = MenuPrimaryWeapon;
        }

        private void EditSecondaryWeapon()
        {
            Menu = MenuSecondaryWeapon;
        }

        private void Back()
        {
            Game.State = new CStateFadeTo(Game, this, new CStateMainMenu(Game));
        }

        private void PrimarySwapType()
        {
            string current = WorkingProfile.WeaponPrimaryType;
            string replace = CWeaponFactory.GetNextWeaponInCycle(current);
            WorkingProfile.WeaponPrimaryType = replace;
            WorkingProfile.WeaponPrimaryLevel = 0;
            RefreshSampleDisplay();
        }

        private void PrimaryPowerUp()
        {
            if (CWeaponFactory.CanUpgrade(WorkingProfile.WeaponPrimaryType, WorkingProfile.WeaponPrimaryLevel))
            {
                WorkingProfile.WeaponPrimaryLevel += 1;
                RefreshSampleDisplay();
            }
        }

        private void PrimaryPowerDown()
        {
            if (CWeaponFactory.CanDowngrade(WorkingProfile.WeaponPrimaryType, WorkingProfile.WeaponPrimaryLevel))
            {
                WorkingProfile.WeaponPrimaryLevel -= 1;
                RefreshSampleDisplay();
            }
        }

        private void SecondarySwapType()
        {
            string current = WorkingProfile.WeaponSecondaryType;
            string replace = CWeaponFactory.GetNextWeaponInCycle(current);
            WorkingProfile.WeaponSecondaryType = replace;
            WorkingProfile.WeaponSecondaryLevel = 0;
            RefreshSampleDisplay();
        }

        private void SecondaryPowerUp()
        {
            if (CWeaponFactory.CanUpgrade(WorkingProfile.WeaponSecondaryType, WorkingProfile.WeaponSecondaryLevel))
            {
                WorkingProfile.WeaponSecondaryLevel += 1;
                RefreshSampleDisplay();
            }
        }

        private void SecondaryPowerDown()
        {
            if (CWeaponFactory.CanDowngrade(WorkingProfile.WeaponSecondaryType, WorkingProfile.WeaponSecondaryLevel))
            {
                WorkingProfile.WeaponSecondaryLevel -= 1;
                RefreshSampleDisplay();
            }
        }

        private void ReturnToBaseMenu()
        {
            Menu = MenuBase;
        }
    }
}
