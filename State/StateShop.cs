//
// StateShop.cs
//

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

            Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.BackToFront, SaveStateMode.None);
            Stars.Draw(Game.DefaultSpriteBatch);
            Game.DefaultSpriteBatch.End();

            EmptyWorld.DrawEntities(EmptyWorld.GameCamera);

            Game.DefaultSpriteBatch.Begin();
            Menu.Draw(Game.DefaultSpriteBatch);
            DrawMenuErrata();

            SampleShip.Draw(Game.DefaultSpriteBatch);

            Game.DefaultSpriteBatch.End();
        }

        private void RefreshSampleDisplay()
        {
            EmptyWorld.Stop();
            SampleShip = new CShip(EmptyWorld, WorkingProfile, Game.PlayerSpawnPosition);
        }

        private void DrawMenuBaseErrata()
        {
        }

        private void DrawMenuPrimaryErrata()
        {
        }

        private void DrawMenuSecondaryErrata()
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
