//
// EditorUnknownEntity.cs
//

using System.Reflection;
using System.ComponentModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    /// <summary>
    /// Default converter for unknown entities.
    /// </summary>
    public class CEditorEntityConverterUnknown
        : TypeConverter
    {
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, System.Attribute[] attributes)
        {
            return CEditorConverter.GetPropertyDescriptors(value,
                "StageElement"
            );
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
    }


    /// <summary>
    /// Default entity for unknown stage element type.
    /// </summary>
    [TypeConverter(typeof(CEditorEntityConverterUnknown))]
    public class CEditorEntityUnknown
        : CEditorEntityBase
    {
        [CategoryAttribute("Internal")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public CStageElement StageElement { get; set; }

        public CEditorEntityUnknown(CWorld world, Vector2 position)
            : base(world, position)
        {
            Physics = new CPhysics();
            Physics.PositionPhysics.Position = position;
            Visual = new CVisual(world, CContent.LoadTexture2D(world.Game, "Textures/Top/Pixel"), Color.Red);
            Visual.Scale = new Vector2(15.0f);
        }

        public CEditorEntityUnknown(CWorld world, CStageElement element)
            : this(world, element.Position)
        {
            StageElement = element;
        }
    }
}
