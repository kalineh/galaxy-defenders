//
// EditorEntityCameraStop.cs
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
    public class CEditorEntityCameraStop
        : CEditorEntityBase
    {
        public CEditorEntityCameraStop(CWorld world, Vector2 position)
            : base(world, position)
        {
            Visual = CVisual.MakeLabel(world, "Camera Stop", Color.Blue);
        }

        public CEditorEntityCameraStop(CWorld world, CStageElement element)
            : this(world, element.Position)
        {
        }

        public override CStageElement GenerateStageElement()
        {
            return new CStageElementCameraStop() { Position = Position };
        }
    }
}
