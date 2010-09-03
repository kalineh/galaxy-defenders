﻿//
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
        public bool Primary { get; set; }
        public CWorld World { get; set; }
        public float Energy { get; set; }
        public float Shield { get; set; }
        public float Armor { get; set; }
        public int? MoneyOverride { get; set; }
        public bool CanUseAbility0 { get; set; }
        public bool CanUseAbility1 { get; set; }
        public bool CanUseAbility2 { get; set; }
        public bool IsActiveAbility0 { get; set; }
        public bool IsActiveAbility1 { get; set; }
        public bool IsActiveAbility2 { get; set; }

        private CVisual LeftPanelVisual { get; set; }
        private CVisual RightPanelVisual { get; set; }
        private CVisual EnergyVisual { get; set; }
        private CVisual ShieldVisual { get; set; }
        private CVisual ArmorVisual { get; set; }
        private CVisual MoneyIconVisual { get; set; }
        private CVisual EnergyIconVisual { get; set; }
        private CVisual ShieldIconVisual { get; set; }
        private CVisual ArmorIconVisual { get; set; }
        private CVisual PortraitIconVisual { get; set; }
        private CVisual Ability0IconVisual { get; set; }
        private CVisual Ability1IconVisual { get; set; }
        private CVisual Ability2IconVisual { get; set; }

        private Vector2 BasePosition { get; set; }
        private Vector2 LeftPanelPosition { get; set; }
        private Vector2 RightPanelPosition { get; set; }
        private Vector2 EnergyPosition { get; set; }
        private Vector2 ShieldPosition { get; set; }
        private Vector2 ArmorPosition { get; set; }
        public Vector2 MoneyTextPosition { get; set; }
        private Vector2 MoneyIconPosition { get; set; }
        private Vector2 EnergyIconPosition { get; set; }
        private Vector2 ShieldIconPosition { get; set; }
        private Vector2 ArmorIconPosition { get; set; }
        private Vector2 PortraitIconPosition { get; set; }
        private Vector2 Ability0IconPosition { get; set; }
        private Vector2 Ability1IconPosition { get; set; }
        private Vector2 Ability2IconPosition { get; set; }

        private string CachedMoneyString { get; set; }
        private int LastMoney { get; set; }

        public CHud(CWorld world, Vector2 base_position, bool primary)
        {
            Primary = primary;
            World = world;
            BasePosition = base_position;

            LeftPanelVisual = CVisual.MakeSpriteUncached(World, "Textures/UI/LeftPanel");
            RightPanelVisual = CVisual.MakeSpriteUncached(World, "Textures/UI/RightPanel");
            EnergyVisual = CVisual.MakeSpriteUncached(World, "Textures/UI/Energy");
            ShieldVisual = CVisual.MakeSpriteUncached(World, "Textures/UI/Shield");
            ArmorVisual = CVisual.MakeSpriteUncached(World, "Textures/UI/Armor");
            MoneyIconVisual = CVisual.MakeSpriteUncached(World, "Textures/UI/MoneyIcon");
            EnergyIconVisual = CVisual.MakeSpriteUncached(World, "Textures/UI/EnergyIcon");
            ShieldIconVisual = CVisual.MakeSpriteUncached(World, "Textures/UI/ShieldIcon");
            ArmorIconVisual = CVisual.MakeSpriteUncached(World, "Textures/UI/ArmorIcon");
            PortraitIconVisual = CVisual.MakeSpriteUncached(World, "Textures/UI/" + CSaveData.GetCurrentProfile().Pilot + "Portrait");
            Ability0IconVisual = CVisual.MakeSpriteUncached(World, "Textures/UI/" + CAbility.GetAbilityName(CSaveData.GetCurrentProfile().Pilot, 0) + "Icon");
            Ability1IconVisual = CVisual.MakeSpriteUncached(World, "Textures/UI/" + CAbility.GetAbilityName(CSaveData.GetCurrentProfile().Pilot, 1) + "Icon");
            Ability2IconVisual = CVisual.MakeSpriteUncached(World, "Textures/UI/" + CAbility.GetAbilityName(CSaveData.GetCurrentProfile().Pilot, 2) + "Icon");

            LeftPanelPosition = new Vector2(0.0f, 0.0f);
            RightPanelPosition = new Vector2(World.Game.GraphicsDevice.Viewport.Width, 0.0f);

            MoneyTextPosition = BasePosition + new Vector2(156.0f, -280.0f - 40.0f);
            EnergyPosition = BasePosition + new Vector2(140.0f, -220.0f);
            ShieldPosition = BasePosition + new Vector2(140.0f, -160.0f);
            ArmorPosition = BasePosition + new Vector2(140.0f, -100.0f);
            MoneyIconPosition = BasePosition + new Vector2(70.0f, -280.0f);
            EnergyIconPosition = BasePosition + new Vector2(70.0f, -220.0f);
            ShieldIconPosition = BasePosition + new Vector2(70.0f, -160.0f);
            ArmorIconPosition = BasePosition + new Vector2(70.0f, -100.0f);
            PortraitIconPosition = BasePosition + new Vector2(240.0f, -600.0f);
            Ability0IconPosition = BasePosition + new Vector2(160.0f, -450.0f);
            Ability1IconPosition = BasePosition + new Vector2(240.0f, -450.0f);
            Ability2IconPosition = BasePosition + new Vector2(320.0f, -450.0f);

            EnergyVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 1.0f;
            ShieldVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 1.0f;
            ArmorVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 1.0f;
            EnergyIconVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 1.0f;
            ShieldIconVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 1.0f;
            ArmorIconVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 1.0f;
            MoneyIconVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 1.0f;
            PortraitIconVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 1.0f;
            Ability0IconVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 1.0f;
            Ability1IconVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 1.0f;
            Ability2IconVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 1.0f;

            LeftPanelVisual.NormalizedOrigin = new Vector2(0.0f, 0.0f);
            RightPanelVisual.NormalizedOrigin = new Vector2(1.0f, 0.0f);
            EnergyVisual.NormalizedOrigin = new Vector2(0.0f, 0.5f);
            ShieldVisual.NormalizedOrigin = new Vector2(0.0f, 0.5f);
            ArmorVisual.NormalizedOrigin = new Vector2(0.0f, 0.5f);
            MoneyIconVisual.NormalizedOrigin = new Vector2(0.0f, 0.5f);
            EnergyIconVisual.NormalizedOrigin = new Vector2(0.0f, 0.5f);
            ShieldIconVisual.NormalizedOrigin = new Vector2(0.0f, 0.5f);
            ArmorIconVisual.NormalizedOrigin = new Vector2(0.0f, 0.5f);

            // TODO: force a recache, need to handle non-entity visuals better
            LeftPanelVisual.Update();
            RightPanelVisual.Update();
            EnergyVisual.Update();
            ShieldVisual.Update();
            ArmorVisual.Update();
            MoneyIconVisual.Update();
            EnergyIconVisual.Update();
            ShieldIconVisual.Update();
            ArmorIconVisual.Update();
            PortraitIconVisual.Update();
            Ability0IconVisual.Update();
            Ability1IconVisual.Update();
            Ability2IconVisual.Update();

            MoneyOverride = null;
            CachedMoneyString = "0";
        }

        public void Update(CShip ship)
        {
            Energy = ship == null ? 0.0f : ship.CurrentEnergy / ship.Generator.Energy;
            Shield = ship == null ? 0.0f : ship.CurrentShield / ship.Shield.Shield;
            Armor = ship == null ? 0.0f : ship.CurrentArmor / ship.Chassis.Armor;

            EnergyVisual.Scale = new Vector2(Energy, 1.0f);
            ShieldVisual.Scale = new Vector2(Shield, 1.0f);
            ArmorVisual.Scale = new Vector2(Armor, 1.0f);

            CanUseAbility0 = ship.Pilot.Ability0.CanEnable();
            CanUseAbility1 = ship.Pilot.Ability1.CanEnable();
            CanUseAbility2 = ship.Pilot.Ability2.CanEnable();

            IsActiveAbility0 = ship.Pilot.Ability0.Active > 0.0f;
            IsActiveAbility1 = ship.Pilot.Ability1.Active > 0.0f;
            IsActiveAbility2 = ship.Pilot.Ability2.Active > 0.0f;
        }

        public void Draw(SpriteBatch sprite_batch)
        {
            if (Primary)
            {
                LeftPanelVisual.Draw(sprite_batch, LeftPanelPosition, 0.0f);
                RightPanelVisual.Draw(sprite_batch, RightPanelPosition, 0.0f);
            }

            EnergyVisual.Draw(sprite_batch, EnergyPosition, 0.0f);
            ShieldVisual.Draw(sprite_batch, ShieldPosition, 0.0f);
            ArmorVisual.Draw(sprite_batch, ArmorPosition, 0.0f);
            EnergyIconVisual.Draw(sprite_batch, EnergyIconPosition, 0.0f);
            ShieldIconVisual.Draw(sprite_batch, ShieldIconPosition, 0.0f);
            ArmorIconVisual.Draw(sprite_batch, ArmorIconPosition, 0.0f);
            PortraitIconVisual.Draw(sprite_batch, PortraitIconPosition, 0.0f);

            Ability0IconVisual.Color = CanUseAbility0 ? Color.White : Color.Gray;
            Ability1IconVisual.Color = CanUseAbility1 ? Color.White : Color.Gray;
            Ability2IconVisual.Color = CanUseAbility2 ? Color.White : Color.Gray;

            Ability0IconVisual.Color = IsActiveAbility0 ? Color.Green : Ability0IconVisual.Color;
            Ability1IconVisual.Color = IsActiveAbility1 ? Color.Green : Ability1IconVisual.Color;
            Ability2IconVisual.Color = IsActiveAbility2 ? Color.Green : Ability2IconVisual.Color;

            Ability0IconVisual.UpdateColor();
            Ability1IconVisual.UpdateColor();
            Ability2IconVisual.UpdateColor();

            Ability0IconVisual.Draw(sprite_batch, Ability0IconPosition, 0.0f);
            Ability1IconVisual.Draw(sprite_batch, Ability1IconPosition, 0.0f);
            Ability2IconVisual.Draw(sprite_batch, Ability2IconPosition, 0.0f);

            if (Primary)
            {
                MoneyIconVisual.Draw(sprite_batch, MoneyIconPosition, 0.0f);
                int money = CSaveData.GetCurrentProfile().Money + World.Score;

                if (MoneyOverride != null)
                    money = (int)MoneyOverride;

                if (money != LastMoney)
                {
                    CachedMoneyString = money.ToString();
                    LastMoney = money;
                }
                sprite_batch.DrawString(World.Game.DefaultFont, CachedMoneyString, MoneyTextPosition, new Color(170, 177, 115), 0.0f, Vector2.Zero, 1.5f, SpriteEffects.None, CLayers.UI + CLayers.SubLayerIncrement);
            }
        }

        public void DrawEditor(SpriteBatch sprite_batch)
        {
            sprite_batch.DrawString(World.Game.DefaultFont, String.Format( "E: {0:00#}", (int)(Energy * 100.0f)), new Vector2(8.0f, 960.0f), Color.White);
            sprite_batch.DrawString(World.Game.DefaultFont, String.Format( "S: {0:00#}", (int)(Shield * 100.0f)), new Vector2(8.0f, 990.0f), Color.White);
            sprite_batch.DrawString(World.Game.DefaultFont, String.Format( "A: {0:00#}", (int)(Armor * 100.0f)), new Vector2(8.0f, 1020.0f), Color.White);
        }

        public void UpdatePilot()
        {
            PortraitIconVisual = CVisual.MakeSpriteUncached(World, "Textures/UI/" + CSaveData.GetCurrentProfile().Pilot + "Portrait");
            PortraitIconVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 1.0f;
            PortraitIconVisual.Update();
            Ability0IconVisual = CVisual.MakeSpriteUncached(World, "Textures/UI/" + CAbility.GetAbilityName(CSaveData.GetCurrentProfile().Pilot, 0) + "Icon");
            Ability1IconVisual = CVisual.MakeSpriteUncached(World, "Textures/UI/" + CAbility.GetAbilityName(CSaveData.GetCurrentProfile().Pilot, 1) + "Icon");
            Ability2IconVisual = CVisual.MakeSpriteUncached(World, "Textures/UI/" + CAbility.GetAbilityName(CSaveData.GetCurrentProfile().Pilot, 2) + "Icon");
            Ability0IconVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 1.0f;
            Ability1IconVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 1.0f;
            Ability2IconVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 1.0f;
            Ability0IconVisual.Update();
            Ability1IconVisual.Update();
            Ability2IconVisual.Update();
        }
    }
}
