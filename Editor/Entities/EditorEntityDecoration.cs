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
        private string _TextureName = "DarkBlue1";

        [CategoryAttribute("Texture")]
        [EditorAttribute(typeof(CTextureSelector<CTextureSelectoryCategoryDecoration>), typeof(UITypeEditor))]
        public string TextureName
        {
            get { return _TextureName; }
            set { _TextureName = value; UpdateTexture(); }
        }

        [CategoryAttribute("Texture")]
        public float DepthOffset { get; set; }

        private float _Rotation = 0.0f;

        [CategoryAttribute("Core")]
        public float Rotation
        {
            get { return _Rotation; }
            set { _Rotation = value; UpdateTexture(); }
        }

        public CEditorEntityDecoration(CWorld world, Type type, Vector2 position)
            : base(world, position)
        {
            TextureName = "DarkBlue1";
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
            DepthOffset = ((CStageElementDecoration)element).DepthOffset;
            Rotation = ((CStageElementDecoration)element).Rotation;
        }

        public override CStageElement GenerateStageElement()
        {
            CStageElementDecoration result = new CStageElementDecoration()
            {
                Position = Position,
                Rotation = Rotation,
                TextureName = TextureName,
                DepthOffset = DepthOffset,
            };

            return result;
        }

        private void UpdateTexture()
        {
            Visual = CVisual.MakeSpriteUncached(World.Game, "Textures/Decoration/" + TextureName);
            Visual = Visual ?? CVisual.MakeLabel(World.Game, TextureName);
            Visual.Depth += DepthOffset;
            Physics.Rotation = MathHelper.ToRadians(Rotation);
        }
    }
}
