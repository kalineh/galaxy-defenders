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
        [CategoryAttribute("Core")]
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
                Coins = Coins,
                Powerup = Powerup,
                TextureName = TextureName,
            };

            return result;
        }

        private void UpdateTexture()
        {
            Visual = CVisual.MakeSprite(World, "Textures/Static/" + TextureName);
            Visual = Visual ?? CVisual.MakeLabel(World, TextureName);
        }
    }
}
