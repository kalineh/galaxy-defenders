//
// EditorEntityStageFinish.cs
//

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Reflection;
using Microsoft.Xna.Framework;

namespace Galaxy
{
    /// <summary>
    /// Editor entity for CStageFinish
    /// </summary>
    [TypeConverter(typeof(CEditorSpawnerEntityConverter))]
    public class CEditorEntityStageFinish
        : CEditorEntityBase
    {
        public CEditorEntityStageFinish(CWorld world, Vector2 position)
            : base(world, position)
        {
            Physics = new CPhysics();
        }

        public CEditorEntityStageFinish(CWorld world, CStageElement element)
            : this(world, element.Position)
        {
        }
    }
}
