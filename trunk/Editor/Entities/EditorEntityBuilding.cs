//
// EditorEntityBuilding.cs
//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    /// <summary>
    /// Editor entity for CEditorEntityBuilding.
    /// </summary>
    [TypeConverter(typeof(CEditorConverterGenerated))]
    public class CEditorEntityBuilding
        : CEditorEntityBase
    {
        // TODO: generating automatically for now
        //[CategoryAttribute("Core")]
        public float HealthMax { get; set; }

        [CategoryAttribute("Bonus")]
        public int Coins { get; set; }

        [CategoryAttribute("Bonus")]
        public bool Powerup { get; set; }

        private string _TextureName = "Building1";

        [CategoryAttribute("Texture")]
        public string TextureName
        {
            get { return _TextureName; }
            set { _TextureName = value; UpdateTexture(); }
        }

        public CEditorEntityBuilding(CWorld world, Type type, Vector2 position)
            : base(world, position)
        {
            UpdateTexture();
        }

        public CEditorEntityBuilding(CWorld world, Vector2 position)
            : this(world, typeof(CBuilding), position)
        {
        }

        public CEditorEntityBuilding(CWorld world, CStageElement element)
            : this(world, typeof(CBuilding), element.Position)
        {
            CStageElementBuilding building = element as CStageElementBuilding;
            HealthMax = building.HealthMax;
            Coins = building.Coins;
            Powerup = building.Powerup;
            TextureName = building.TextureName;
        }

        public override CStageElement GenerateStageElement()
        {
            CStageElementBuilding result = new CStageElementBuilding()
            {
                Position = Position,
                HealthMax = HealthMax,
                Coins = Coins,
                Powerup = Powerup,
                TextureName = TextureName,
            };

            return result;
        }

        private void SetDefaultHealth()
        {
            // TODO: do this in a way that doesnt suck
            // TODO: all use default health
            //if (HealthMax > 0.0f)
                //return;

            switch (TextureName)
            {
                case "Building1": HealthMax = 7.0f; return;
                case "Building2": HealthMax = 3.0f; return;
                case "Building3": HealthMax = 7.0f; return;
                case "Building4": HealthMax = 11.0f; return;
                case "Building5": HealthMax = 11.0f; return;
            }
        }

        private void UpdateTexture()
        {
            Visual = CVisual.MakeSprite(World, "Textures/Static/" + TextureName);
            Visual = Visual ?? CVisual.MakeLabel(World, TextureName);

            SetDefaultHealth();
        }
    }
}
