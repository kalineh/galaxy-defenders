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
        : CEntity
    {
        [CategoryAttribute("Internal")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public CStageElement StageElement { get; set; }

        public CEditorEntityUnknown(CWorld world, CStageElement element)
            : base(world, "EditorEntityUnknown")
        {
            StageElement = element;

            Physics = new CPhysics();
            Physics.PositionPhysics.Position = element.Position;
            Visual = new CVisual(CContent.LoadTexture2D(world.Game, "Textures/Top/Pixel"), Color.Red);
            Visual.Scale = new Vector2(15.0f);
        }
    }
}
