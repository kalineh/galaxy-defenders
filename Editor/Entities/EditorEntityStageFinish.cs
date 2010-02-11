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
        [CategoryAttribute("Core")]
        public Vector2 Position
        {
            get { return Physics.PositionPhysics.Position; }
            set { Physics.PositionPhysics.Position = value; }
        }

        public CEditorEntityStageFinish(CWorld world, CStageElement element)
            : base(world, element)
        {
            Physics = new CPhysics();
        }
    }
}
