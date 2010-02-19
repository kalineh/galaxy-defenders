//
// EditorEntityDecoration.cs
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
    /// Editor entity for CEditorEntityDecoration.
    /// </summary>
    [TypeConverter(typeof(CEditorConverterGenerated))]
    public class CEditorEntityDecoration
        : CEditorEntityBase
    {
        private string _TextureName = "Platform1";

        [CategoryAttribute("Texture")]
        public string TextureName
        {
            get { return _TextureName; }
            set { _TextureName = value; UpdateTexture(); }
        }

        public CEditorEntityDecoration(CWorld world, Type type, Vector2 position)
            : base(world, position)
        {
            TextureName = "Platform1";
            UpdateTexture();
        }

        public CEditorEntityDecoration(CWorld world, Vector2 position)
            : this(world, typeof(CDecoration), position)
        {
        }

        public CEditorEntityDecoration(CWorld world, CStageElement element)
            : this(world, typeof(CDecoration), element.Position)
        {
            TextureName = ((CStageElementDecoration)element).TextureName;
        }

        public override CStageElement GenerateStageElement()
        {
            CStageElementDecoration result = new CStageElementDecoration()
            {
                Position = Position,
                TextureName = TextureName,
            };

            return result;
        }

        private void UpdateTexture()
        {
            Visual = CVisual.MakeSprite(World, "Textures/Decoration/" + TextureName);
            Visual = Visual ?? CVisual.MakeLabel(World, TextureName);
        }
    }
}
