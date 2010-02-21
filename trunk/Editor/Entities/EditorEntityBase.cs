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
    [TypeConverter(typeof(CEditorConverterGenerated))]
    public class CEditorEntityBase
        : CEntity
    {
        [CategoryAttribute("Core")]
        public Vector2 Position
        {
            get { return Physics.PositionPhysics.Position; }
            set { Physics.PositionPhysics.Position = value; }
        }

        public bool EditorDirty { get; set; }

        /// <summary>
        /// Generate default editor entity from just position.
        /// </summary>
        public CEditorEntityBase(CWorld world, Vector2 position)
            : base(world)
        {
            Physics = new CPhysics();
            Position = position;
        }

        /// <summary>
        /// Generate editor entity from stage element.
        /// </summary>
        public CEditorEntityBase(CWorld world, CStageElement element)
            : base(world)
        {
        }

        public virtual CEditorEntityPreview GeneratePreviewEntity()
        {
            return null;
        }

        public virtual CStageElement GenerateStageElement()
        {
            throw new NotImplementedException();
        }

        public bool IsEditorDirty()
        {
            bool result = EditorDirty;
            EditorDirty = false;
            return result;
        }
    }
}
