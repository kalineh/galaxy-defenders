//
// Hud.cs
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CHud
    {
        public GameControllerIndex GameControllerIndex { get; set; }
        public CGalaxy Game { get; set; }
        public CShip Ship { get; set; }
        public string NameText { get; set; }
        public float Energy { get; set; }
        public float Shield { get; set; }
        public float Armor { get; set; }
        public bool ShowMoney { get; set; }
        public bool CanUseAbility0 { get; set; }
        public bool CanUseAbility1 { get; set; }
        public bool CanUseAbility2 { get; set; }
        public bool IsActiveAbility0 { get; set; }
        public bool IsActiveAbility1 { get; set; }
        public bool IsActiveAbility2 { get; set; }

        private CVisual LeftPanelVisual { get; set; }
        private CVisual RightPanelVisual { get; set; }
        private CVisual LeftPanelFramesVisual { get; set; }
        private CVisual RightPanelFramesVisual { get; set; }
        private CVisual EnergyVisual { get; set; }
        private CVisual ShieldVisual { get; set; }
        private CVisual ArmorVisual { get; set; }
        private CVisual MoneyIconVisual { get; set; }
        private CVisual EnergyIconVisual { get; set; }
        private CVisual ShieldIconVisual { get; set; }
        private CVisual ArmorIconVisual { get; set; }
        private CVisual PortraitIconVisual { get; set; }
        private CVisual FireIconVisual { get; set; }
        private CVisual Ability0IconVisual { get; set; }
        private CVisual Ability1IconVisual { get; set; }
        private CVisual Ability2IconVisual { get; set; }
        private CVisual FireButtonIconVisual { get; set; }
        private CVisual Ability0IconButtonVisual { get; set; }
        private CVisual Ability1IconButtonVisual { get; set; }
        private CVisual Ability2IconButtonVisual { get; set; }
        private CVisual Ability0IconGreyButtonVisual { get; set; }
        private CVisual Ability1IconGreyButtonVisual { get; set; }
        private CVisual Ability2IconGreyButtonVisual { get; set; }

        public Vector2 BasePosition { get; set; }
        private Vector2 LeftPanelPosition { get; set; }
        private Vector2 RightPanelPosition { get; set; }
        public Vector2 NameTextPosition { get; set; }
        private Vector2 EnergyPosition { get; set; }
        private Vector2 ShieldPosition { get; set; }
        private Vector2 ArmorPosition { get; set; }
        public Vector2 MoneyTextPosition { get; set; }
        private Vector2 MoneyIconPosition { get; set; }
        private Vector2 EnergyIconPosition { get; set; }
        private Vector2 ShieldIconPosition { get; set; }
        private Vector2 ArmorIconPosition { get; set; }
        private Vector2 PortraitIconPosition { get; set; }
        private Vector2 FireIconPosition { get; set; }
        private Vector2 Ability0IconPosition { get; set; }
        private Vector2 Ability1IconPosition { get; set; }
        private Vector2 Ability2IconPosition { get; set; }

        private string CachedMoneyString { get; set; }
        private float MoneyStringScale { get; set; }
        private int LastMoney { get; set; }
        private int BaseMoney { get; set; }

        public CHud(CGalaxy game, Vector2 base_position, GameControllerIndex game_controller_index)
        {
            GameControllerIndex = game_controller_index;
            Game = game;
            BasePosition = base_position;

            NameText = CSaveData.GetCurrentGameData(Game).Pilots[(int)GameControllerIndex].Pilot;

            LeftPanelVisual = CVisual.MakeSpriteUncached(Game, "Textures/UI/PanelBackground");
            RightPanelVisual = CVisual.MakeSpriteUncached(Game, "Textures/UI/PanelBackground");
            LeftPanelFramesVisual = CVisual.MakeSpriteUncached(Game, "Textures/UI/PanelFrames");
            RightPanelFramesVisual = CVisual.MakeSpriteUncached(Game, "Textures/UI/PanelFrames");
            EnergyVisual = CVisual.MakeSpriteUncached(Game, "Textures/UI/Energy");
            ShieldVisual = CVisual.MakeSpriteUncached(Game, "Textures/UI/Shield");
            ArmorVisual = CVisual.MakeSpriteUncached(Game, "Textures/UI/Armor");
            MoneyIconVisual = CVisual.MakeSpriteUncached(Game, "Textures/UI/MoneyIcon");
            EnergyIconVisual = CVisual.MakeSpriteUncached(Game, "Textures/UI/EnergyIcon");
            ShieldIconVisual = CVisual.MakeSpriteUncached(Game, "Textures/UI/ShieldIcon");
            ArmorIconVisual = CVisual.MakeSpriteUncached(Game, "Textures/UI/ArmorIcon");

            // TODO: less crappiness
            PortraitIconVisual = CVisual.MakeSpriteUncached(Game, "Textures/Top/Pixel");
            FireIconVisual = CVisual.MakeSpriteUncached(Game, "Textures/Top/Pixel");
            Ability0IconVisual = CVisual.MakeSpriteUncached(Game, "Textures/Top/Pixel");
            Ability1IconVisual = CVisual.MakeSpriteUncached(Game, "Textures/Top/Pixel");
            Ability2IconVisual = CVisual.MakeSpriteUncached(Game, "Textures/Top/Pixel");
            FireButtonIconVisual = CVisual.MakeSpriteUncached(Game, "Textures/UI/Xbox/xboxControllerButtonX");
            Ability0IconButtonVisual = CVisual.MakeSpriteUncached(Game, "Textures/UI/Xbox/xboxControllerButtonY");
            Ability1IconButtonVisual = CVisual.MakeSpriteUncached(Game, "Textures/UI/Xbox/xboxControllerButtonB");
            Ability2IconButtonVisual = CVisual.MakeSpriteUncached(Game, "Textures/UI/Xbox/xboxControllerButtonA");
            Ability0IconGreyButtonVisual = CVisual.MakeSpriteUncached(Game, "Textures/UI/Xbox/xboxControllerButtonYgreyscale");
            Ability1IconGreyButtonVisual = CVisual.MakeSpriteUncached(Game, "Textures/UI/Xbox/xboxControllerButtonBgreyscale");
            Ability2IconGreyButtonVisual = CVisual.MakeSpriteUncached(Game, "Textures/UI/Xbox/xboxControllerButtonAgreyscale");

            LeftPanelPosition = new Vector2(0.0f, 0.0f);
            RightPanelPosition = new Vector2(Game.Resolution.X, 0.0f);

            NameTextPosition = BasePosition + new Vector2(242.0f, -928.0f);
            MoneyTextPosition = BasePosition + new Vector2(242.0f, -850.0f);
            EnergyPosition = BasePosition + new Vector2(140.0f, -266.0f);
            ShieldPosition = BasePosition + new Vector2(140.0f, -183.0f);
            ArmorPosition = BasePosition + new Vector2(140.0f, -107.0f);
            MoneyIconPosition = BasePosition + new Vector2(70.0f, -800.0f);
            EnergyIconPosition = BasePosition + new Vector2(70.0f, -266.0f);
            ShieldIconPosition = BasePosition + new Vector2(70.0f, -183.0f);
            ArmorIconPosition = BasePosition + new Vector2(70.0f + 32.0f, -107.0f);
            PortraitIconPosition = BasePosition + new Vector2(238.0f, -664.0f);
            FireIconPosition = BasePosition + new Vector2(182.0f, -468.0f);
            Ability0IconPosition = BasePosition + new Vector2(240.0f, -532.0f);
            Ability1IconPosition = BasePosition + new Vector2(298.0f, -468.0f);
            Ability2IconPosition = BasePosition + new Vector2(240.0f, -404.0f);

            LeftPanelVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 0.0f;
            RightPanelVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 0.0f;
            LeftPanelFramesVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 3.0f;
            RightPanelFramesVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 3.0f;
            EnergyVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 2.0f;
            ShieldVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 2.0f;
            ArmorVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 2.0f;
            EnergyIconVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 2.0f;
            ShieldIconVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 2.0f;
            ArmorIconVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 2.0f;
            MoneyIconVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 2.0f;
            PortraitIconVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 2.0f;
            FireIconVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 2.0f;
            Ability0IconVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 2.0f;
            Ability1IconVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 2.0f;
            Ability2IconVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 2.0f;
            FireButtonIconVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 3.0f;
            Ability0IconButtonVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 3.0f;
            Ability1IconButtonVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 3.0f;
            Ability2IconButtonVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 3.0f;
            Ability0IconGreyButtonVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 3.0f;
            Ability1IconGreyButtonVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 3.0f;
            Ability2IconGreyButtonVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 3.0f;

            EnergyVisual.Scale = new Vector2(1.1f, 1.15f);
            ShieldVisual.Scale = new Vector2(1.1f, 1.15f);
            ArmorVisual.Scale = new Vector2(1.1f, 1.15f);

            LeftPanelVisual.NormalizedOrigin = new Vector2(0.0f, 0.0f);
            RightPanelVisual.NormalizedOrigin = new Vector2(1.0f, 0.0f);
            LeftPanelFramesVisual.NormalizedOrigin = new Vector2(0.0f, 0.0f);
            RightPanelFramesVisual.NormalizedOrigin = new Vector2(1.0f, 0.0f);
            EnergyVisual.NormalizedOrigin = new Vector2(0.0f, 0.5f);
            ShieldVisual.NormalizedOrigin = new Vector2(0.0f, 0.5f);
            ArmorVisual.NormalizedOrigin = new Vector2(0.0f, 0.5f);
            MoneyIconVisual.NormalizedOrigin = new Vector2(0.0f, 0.5f);
            EnergyIconVisual.NormalizedOrigin = new Vector2(0.0f, 0.5f);
            ShieldIconVisual.NormalizedOrigin = new Vector2(0.0f, 0.5f);
            ArmorIconVisual.NormalizedOrigin = new Vector2(0.5f, 0.5f);

            // TODO: force a recache, need to handle non-entity visuals better
            LeftPanelVisual.Update();
            RightPanelVisual.Update();
            LeftPanelFramesVisual.Update();
            RightPanelFramesVisual.Update();
            EnergyVisual.Update();
            ShieldVisual.Update();
            ArmorVisual.Update();
            MoneyIconVisual.Update();
            EnergyIconVisual.Update();
            ShieldIconVisual.Update();
            ArmorIconVisual.Update();
            PortraitIconVisual.Update();
            FireIconVisual.Update();
            Ability0IconVisual.Update();
            Ability1IconVisual.Update();
            Ability2IconVisual.Update();
            FireButtonIconVisual.Update();
            Ability0IconButtonVisual.Update();
            Ability1IconButtonVisual.Update();
            Ability2IconButtonVisual.Update();
            Ability0IconGreyButtonVisual.Update();
            Ability1IconGreyButtonVisual.Update();
            Ability2IconGreyButtonVisual.Update();

            CachedMoneyString = "0￥";
            MoneyStringScale = 1.0f;

            SProfileGameData profile = CSaveData.GetCurrentGameData(Game);
            BaseMoney = CSaveData.CalculateTotalMoney(profile.Pilots[(int)GameControllerIndex]);
        }

        public void Update()
        {
            if (!Game.HudManager.IsPilotLocked((int)GameControllerIndex))
                return;

            if (Ship == null)
            {
                Energy = 1.0f;
                Shield = 1.0f;
                Armor = 1.0f;

                EnergyVisual.Scale = new Vector2(Energy * 1.1f, 1.15f);
                ShieldVisual.Scale = new Vector2(Shield * 1.1f, 1.15f);
                ArmorVisual.Scale = new Vector2(Armor * 1.1f, 1.15f);

                CanUseAbility0 = false;
                CanUseAbility1 = false;
                CanUseAbility2 = false;

                IsActiveAbility0 = false;
                IsActiveAbility1 = false;
                IsActiveAbility2 = false;
            }
            else
            {
                Energy = Ship.CurrentEnergy / Ship.Generator.Energy;
                Shield = Ship.CurrentShield / Ship.Shield.Shield;
                Armor = Ship.CurrentArmor / Ship.Chassis.Armor;

                EnergyVisual.Scale = new Vector2(Energy * 1.1f, 1.15f);
                ShieldVisual.Scale = new Vector2(Shield * 1.1f, 1.15f);
                ArmorVisual.Scale = new Vector2(Armor * 1.1f, 1.15f);

                CanUseAbility0 = Ship.Pilot.Ability0.CanEnable();
                CanUseAbility1 = Ship.Pilot.Ability1.CanEnable();
                CanUseAbility2 = Ship.Pilot.Ability2.CanEnable();

                IsActiveAbility0 = Ship.Pilot.Ability0.Active > 0.0f;
                IsActiveAbility1 = Ship.Pilot.Ability1.Active > 0.0f;
                IsActiveAbility2 = Ship.Pilot.Ability2.Active > 0.0f;
            }
        }

        public void Draw(SpriteBatch sprite_batch)
        {
#if DEBUG
            float old_scale = CGalaxy.GlobalScale;
            CGalaxy.GlobalScale = 1.0f;
#endif

            if (GameControllerIndex == GameControllerIndex.One)
            {
                LeftPanelVisual.Draw(sprite_batch, LeftPanelPosition, 0.0f);
            }
            else
            {
                RightPanelVisual.Draw(sprite_batch, RightPanelPosition, 0.0f);
            }

            if (!Game.HudManager.IsPilotLocked((int)GameControllerIndex))
                return;

            if (GameControllerIndex == GameControllerIndex.One)
            {
                LeftPanelFramesVisual.Draw(sprite_batch, LeftPanelPosition, 0.0f);
            }
            else
            {
                RightPanelFramesVisual.Draw(sprite_batch, RightPanelPosition, 0.0f);
            }

            EnergyVisual.Draw(sprite_batch, EnergyPosition, 0.0f);
            ShieldVisual.Draw(sprite_batch, ShieldPosition, 0.0f);
            ArmorVisual.Draw(sprite_batch, ArmorPosition, 0.0f);
            EnergyIconVisual.Draw(sprite_batch, EnergyIconPosition, 0.0f);
            ShieldIconVisual.Draw(sprite_batch, ShieldIconPosition, 0.0f);

            if (Ship != null)
            {
                if (Ship.IsLowArmor())
                {
                    byte rgb = (byte)((Game.GameFrame % 30 < 6) ? 127 : 255);
                    ArmorVisual.Color = new Color(rgb, rgb, rgb);
                    ArmorVisual.UpdateColor();
                }
                else
                {
                    ArmorVisual.Color = Color.White;
                    ArmorVisual.UpdateColor();
                }
            }

            ArmorIconVisual.Draw(sprite_batch, ArmorIconPosition, 0.0f);

            PortraitIconVisual.Draw(sprite_batch, PortraitIconPosition, 0.0f);

            // TODO: active effect display?

            FireIconVisual.Draw(sprite_batch, FireIconPosition, 0.0f);
            Ability0IconVisual.Draw(sprite_batch, Ability0IconPosition, 0.0f);
            Ability1IconVisual.Draw(sprite_batch, Ability1IconPosition, 0.0f);
            Ability2IconVisual.Draw(sprite_batch, Ability2IconPosition, 0.0f);

            CVisual button0 = CanUseAbility0 ? Ability0IconButtonVisual : Ability0IconGreyButtonVisual;
            CVisual button1 = CanUseAbility1 ? Ability1IconButtonVisual : Ability1IconGreyButtonVisual;
            CVisual button2 = CanUseAbility2 ? Ability2IconButtonVisual : Ability2IconGreyButtonVisual;

            FireButtonIconVisual.Draw(sprite_batch, FireIconPosition + new Vector2(38.0f, 0.0f), 0.0f);
            button0.Draw(sprite_batch, Ability0IconPosition + new Vector2(0.0f, 42.0f), 0.0f);
            button1.Draw(sprite_batch, Ability1IconPosition + new Vector2(-38.0f, 0.0f), 0.0f);
            button2.Draw(sprite_batch, Ability2IconPosition + new Vector2(0.0f, -42.0f), 0.0f);

            Color color = new Color(160, 160, 160);
            sprite_batch.DrawStringAlignCenter(Game.GameLargeFont, NameTextPosition, NameText, color, 1.0f);

            // NOTE: disabling money display for now
            // NOTE: a bit too confusing with the money pool bar, it should be just Score
            DrawMoney(sprite_batch);

#if DEBUG
            CGalaxy.GlobalScale = old_scale;
#endif
        }

        public void DrawMoney(SpriteBatch sprite_batch)
        {
            //
            // TODO: fix these before enabling money
            // - non-override shows ship data wrong
            // - show only score, not money
            // - ideally should be 0 at game start, not StartingMoney
            //

            // TODO: just not show?
            //MoneyIconVisual.Draw(sprite_batch, MoneyIconPosition, 0.0f);

            //int money = BaseMoney;

            if (Ship == null)
                return;

            if (ShowMoney == false)
                return;

            // TODO: score needs to be maintained across secret stages?
            // add current stage score if valid
            int money = Ship.Score;

            if (money != LastMoney)
            {
                // TODO: move string scale to the score add place, so shop doesnt cause this
                if (money > LastMoney)
                    MoneyStringScale = 1.15f;

                CachedMoneyString = String.Format("{0}￥", money);
                LastMoney = money;
            }

            float base_color = 160.0f / 255.0f;
            float c = base_color + (MoneyStringScale - 1.0f) * 1.25f;
            Color money_color = new Color(c, c, c);

            // TODO: remove display altogether?
            sprite_batch.DrawStringAlignCenter(Game.GameLargeFont, MoneyTextPosition, CachedMoneyString, money_color, MoneyStringScale);

            MoneyStringScale = MathHelper.Max(1.0f, MoneyStringScale - 0.01f);
        }

        public void DrawEditor(SpriteBatch sprite_batch)
        {
            sprite_batch.DrawString(Game.GameRegularFont, String.Format( "E: {0:00#}", (int)(Energy * 100.0f)), new Vector2(8.0f, 960.0f), Color.White);
            sprite_batch.DrawString(Game.GameRegularFont, String.Format( "S: {0:00#}", (int)(Shield * 100.0f)), new Vector2(8.0f, 990.0f), Color.White);
            sprite_batch.DrawString(Game.GameRegularFont, String.Format( "A: {0:00#}", (int)(Armor * 100.0f)), new Vector2(8.0f, 1020.0f), Color.White);
        }

        public void UpdatePilot()
        {
            SProfileGameData profile = CSaveData.GetCurrentGameData(Game);
            PortraitIconVisual = CVisual.MakeSpriteUncached(Game, "Textures/UI/" + profile.Pilots[(int)GameControllerIndex].Pilot + "Portrait");
            PortraitIconVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 1.0f;
            PortraitIconVisual.Update();
            FireIconVisual = CVisual.MakeSpriteUncached(Game, "Textures/UI/AbilityIcons/FireIcon");
            Ability0IconVisual = CVisual.MakeSpriteUncached(Game, "Textures/UI/AbilityIcons/" + CAbility.GetAbilityName(profile.Pilots[(int)GameControllerIndex].Pilot, 0) + "Icon");
            Ability1IconVisual = CVisual.MakeSpriteUncached(Game, "Textures/UI/AbilityIcons/" + CAbility.GetAbilityName(profile.Pilots[(int)GameControllerIndex].Pilot, 1) + "Icon");
            Ability2IconVisual = CVisual.MakeSpriteUncached(Game, "Textures/UI/AbilityIcons/" + CAbility.GetAbilityName(profile.Pilots[(int)GameControllerIndex].Pilot, 2) + "Icon");
            FireIconVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 2.0f;
            Ability0IconVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 2.0f;
            Ability1IconVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 2.0f;
            Ability2IconVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 2.0f;
            FireIconVisual.Update();
            Ability0IconVisual.Update();
            Ability1IconVisual.Update();
            Ability2IconVisual.Update();
        }
    }
}
