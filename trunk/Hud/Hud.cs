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
        public bool Primary { get; set; }
        public CWorld World { get; set; }
        public float Energy { get; set; }
        public float Shield { get; set; }
        public float Armor { get; set; }
        public int? MoneyOverride { get; set; }

        private CVisual LeftPanelVisual { get; set; }
        private CVisual RightPanelVisual { get; set; }
        private CVisual EnergyVisual { get; set; }
        private CVisual ShieldVisual { get; set; }
        private CVisual ArmorVisual { get; set; }
        private CVisual MoneyIconVisual { get; set; }
        private CVisual EnergyIconVisual { get; set; }
        private CVisual ShieldIconVisual { get; set; }
        private CVisual ArmorIconVisual { get; set; }

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

            EnergyVisual.Depth += CLayers.SubLayerIncrement;
            ShieldVisual.Depth += CLayers.SubLayerIncrement;
            ArmorVisual.Depth += CLayers.SubLayerIncrement;
            EnergyIconVisual.Depth += CLayers.SubLayerIncrement;
            ShieldIconVisual.Depth += CLayers.SubLayerIncrement;
            ArmorIconVisual.Depth += CLayers.SubLayerIncrement;
            MoneyIconVisual.Depth += CLayers.SubLayerIncrement;

            LeftPanelVisual.NormalizedOrigin = new Vector2(0.0f, 0.0f);
            RightPanelVisual.NormalizedOrigin = new Vector2(1.0f, 0.0f);
            EnergyVisual.NormalizedOrigin = new Vector2(0.0f, 0.5f);
            ShieldVisual.NormalizedOrigin = new Vector2(0.0f, 0.5f);
            ArmorVisual.NormalizedOrigin = new Vector2(0.0f, 0.5f);
            MoneyIconVisual.NormalizedOrigin = new Vector2(0.0f, 0.5f);
            EnergyIconVisual.NormalizedOrigin = new Vector2(0.0f, 0.5f);
            ShieldIconVisual.NormalizedOrigin = new Vector2(0.0f, 0.5f);
            ArmorIconVisual.NormalizedOrigin = new Vector2(0.0f, 0.5f);

            MoneyOverride = null;
        }

        public void Update(CShip ship)
        {
            Energy = ship == null ? 0.0f : ship.CurrentEnergy / ship.Generator.Energy;
            Shield = ship == null ? 0.0f : ship.CurrentShield / ship.Shield.Shield;
            Armor = ship == null ? 0.0f : ship.CurrentArmor / ship.Chassis.Armor;

            EnergyVisual.Scale = new Vector2(Energy, 1.0f);
            ShieldVisual.Scale = new Vector2(Shield, 1.0f);
            ArmorVisual.Scale = new Vector2(Armor, 1.0f);
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

            if (Primary)
            {
                MoneyIconVisual.Draw(sprite_batch, MoneyIconPosition, 0.0f);
                int money = CSaveData.GetCurrentProfile().Money + World.Score;

                if (MoneyOverride != null)
                    money = (int)MoneyOverride;

                sprite_batch.DrawString(World.Game.DefaultFont, money.ToString(), MoneyTextPosition, new Color(170, 177, 115), 0.0f, Vector2.Zero, 1.5f, SpriteEffects.None, CLayers.UI + CLayers.SubLayerIncrement);
            }
        }
    }
}
