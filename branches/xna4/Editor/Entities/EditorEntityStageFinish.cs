//
// EditorEntityStageFinish.cs
//

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    /// <summary>
    /// Editor entity for CStageFinish
    /// </summary>
    [TypeConverter(typeof(CEditorConverterGenerated))]
    public class CEditorEntityStageFinish
        : CEditorEntityBase
    {
        public CEditorEntityStageFinish(CWorld world, Vector2 position)
            : base(world, position)
        {
            Visual = CVisual.MakeLabel(world.Game, "Stage Finish", Color.Blue);
        }

        public CEditorEntityStageFinish(CWorld world, CStageElement element)
            : this(world, element.Position)
        {
        }

        public override CStageElement GenerateStageElement()
        {
            return new CStageElementStageFinish() { Position = Position };
        }
    }
}
