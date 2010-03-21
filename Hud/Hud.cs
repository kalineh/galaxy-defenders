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
        public CWorld World { get; set; }
        public float Energy { get; set; }
        public float Shield { get; set; }
        public float Armor { get; set; }

        private CVisual MainPanelVisual { get; set; }
        private CVisual EnergyVisual { get; set; }
        private CVisual ShieldVisual { get; set; }
        private CVisual ArmorVisual { get; set; }
        private CVisual MoneyPanelVisual { get; set; }

        private Vector2 MainPanelPosition { get; set; }
        private Vector2 EnergyPosition { get; set; }
        private Vector2 ShieldPosition { get; set; }
        private Vector2 ArmorPosition { get; set; }
        private Vector2 MoneyPanelPosition { get; set; }

        public CHud(CWorld world)
        {
            World = world;

            MainPanelVisual = CVisual.MakeSprite(World, "Textures/UI/MainPanel");
            EnergyVisual = CVisual.MakeSprite(World, "Textures/UI/Energy");
            ShieldVisual = CVisual.MakeSprite(World, "Textures/UI/Shield");
            ArmorVisual = CVisual.MakeSprite(World, "Textures/UI/Armor");
            MoneyPanelVisual = CVisual.MakeSprite(World, "Textures/UI/MoneyPanel");

            MainPanelPosition = new Vector2(400.0f, 768.0f);
            EnergyPosition = new Vector2(400.0f, 768.0f);
            ShieldPosition = new Vector2(799.0f, 764.0f);
            ArmorPosition = new Vector2(1.0f, 764.0f);
            MoneyPanelPosition = new Vector2(0.0f, 0.0f);

            EnergyVisual.SpriteEffects = SpriteEffects.FlipHorizontally;
            EnergyVisual.Depth += CLayers.SubLayerIncrement;
            ShieldVisual.Depth += CLayers.SubLayerIncrement;
            ShieldVisual.NormalizedOrigin = new Vector2(1.0f, 0.5f);
            ArmorVisual.Depth += CLayers.SubLayerIncrement;
            ArmorVisual.NormalizedOrigin = new Vector2(0.0f, 0.5f);
            MoneyPanelVisual.NormalizedOrigin = new Vector2(0.0f, 0.0f);
        }

        public void Update(CShip ship)
        {
            Energy = ship == null ? 0.0f : ship.CurrentEnergy / ship.Generator.Energy;
            Shield = ship == null ? 0.0f : ship.CurrentShield / ship.Shield.Shield;
            Armor = ship == null ? 0.0f : ship.CurrentArmor / ship.Chassis.Armor;

            EnergyVisual.Scale = new Vector2(Energy, Energy);
            ShieldVisual.Scale = new Vector2(Shield, 1.0f);
            ArmorVisual.Scale = new Vector2(Armor, 1.0f);
        }

        public void Draw(SpriteBatch sprite_batch)
        {
            MainPanelVisual.Draw(sprite_batch, MainPanelPosition, 0.0f);
            EnergyVisual.Draw(sprite_batch, EnergyPosition, 0.0f);
            ShieldVisual.Draw(sprite_batch, ShieldPosition, 0.0f);
            ArmorVisual.Draw(sprite_batch, ArmorPosition, 0.0f);

            MoneyPanelVisual.Draw(sprite_batch, MoneyPanelPosition, 0.0f);
            // TODO: split to Hud system

            int money = CSaveData.GetCurrentProfile().Money + World.Score;
            sprite_batch.DrawString(World.Game.DefaultFont, "￥" + money, new Vector2(4.0f, 4.0f), Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, MoneyPanelVisual.Depth + CLayers.SubLayerIncrement);
        }
    }
}
