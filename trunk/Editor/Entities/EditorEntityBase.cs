//
// EditorEntityBase.cs
//

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Reflection;
using Microsoft.Xna.Framework;

namespace Galaxy
{
    /// <summary>
    /// Editor entity base class.
    /// </summary>
    public class CEditorEntityBase
        : CEntity
    {
        [CategoryAttribute("Core")]
        public Vector2 Position
        {
            get { return Physics.PositionPhysics.Position; }
            set { Physics.PositionPhysics.Position = value; }
        }

        /// <summary>
        /// Generate default editor entity from just position.
        /// </summary>
        public CEditorEntityBase(CWorld world, Vector2 position)
            : base(world, "EditorEntityBaseDynamic")
        {
        }

        /// <summary>
        /// Generate editor entity from stage element.
        /// </summary>
        public CEditorEntityBase(CWorld world, CStageElement element)
            : base(world, "EditorEntityBaseStatic")
        {
        }

        public virtual CEditorEntityPreview GeneratePreviewEntity()
        {
            return new CEditorEntityPreview(World, this);
        }

        public virtual CStageElement GenerateStageElement()
        {
            throw new NotImplementedException();
        }
    }
}
