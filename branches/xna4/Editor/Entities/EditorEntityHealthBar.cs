//
// EditorEntityHealthBar.cs
//

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    [TypeConverter(typeof(CEditorConverterGenerated))]
    public class CEditorEntityHealthBar
        : CEditorEntityBase
    {
        public CEditorEntityHealthBar(CWorld world, Vector2 position)
            : base(world, position)
        {
            Visual = CVisual.MakeLabel(world.Game, "Health Bar", Color.Blue);
        }

        public CEditorEntityHealthBar(CWorld world, CStageElement element)
            : this(world, element.Position)
        {
        }

        public override CStageElement GenerateStageElement()
        {
            return new CStageElementHealthBar() { Position = Position };
        }
    }
}
