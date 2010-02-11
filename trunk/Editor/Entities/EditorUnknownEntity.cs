//
// EditorUnknownEntity.cs
//

using System.ComponentModel;

namespace Galaxy
{
    namespace Editor
    {

        /// <summary>
        /// Default entity for unknown stage element type.
        /// </summary>
        [TypeConverter(typeof(CEditorEntityConverter))]
        public class CUnknown
            : CEntity
        {
            [CategoryAttribute("Internal")]
            [TypeConverter(typeof(ExpandableObjectConverter))]
            public CStageElement StageElement { get; set; }

            public CUnknown(CWorld world, CStageElement stage_element)
                : base(world, "EditorEntity")
            {
                Physics = new CPhysics();
            }

            public override float GetRadius()
            {
                return 15.0f;
            }
        }
    }
}
