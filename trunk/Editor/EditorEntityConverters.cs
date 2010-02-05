//
// EditorEntityConverters.cs
//

using System;
using System.Drawing.Design;
using System.Globalization;
using System.ComponentModel;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Galaxy
{
    namespace Editor
    {
        public static class CConverter
        {
            public static PropertyDescriptorCollection GetPropertyDescriptors(object value, params string[] names)
            {
                // TODO: there -MUST- be an easier way to do this :|
                // TODO: just get property descriptors from a named list
                List<string> namelist = new List<string>(names);
                PropertyDescriptorCollection descriptors = TypeDescriptor.GetProperties(value);
                PropertyDescriptor[] array = new PropertyDescriptor[descriptors.Count];
                descriptors.CopyTo(array, 0);
                IEnumerable<PropertyDescriptor> enumerable = array.AsEnumerable<PropertyDescriptor>();
                List<PropertyDescriptor> properties = new List<PropertyDescriptor>(enumerable);

                PropertyDescriptorCollection results = new PropertyDescriptorCollection(
                    properties.Where(p => 
                        namelist.Exists(i => i == p.Name)
                    ).ToArray()
                );

                return results;
            }
        }

        public class CEntityConverter
            : TypeConverter
        {
            public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
            {
                return CConverter.GetPropertyDescriptors(value,
                    "Name",
                    "Foo"
                );
            }

            public override bool GetPropertiesSupported(ITypeDescriptorContext context)
            {
                return true;
            }
        }

        public class CSpawnerEntityConverter
            : ExpandableObjectConverter
        {
            public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
            {
                return CConverter.GetPropertyDescriptors(value,
                    "Name",
                    "StageElement"
                );
            }

            public override bool GetPropertiesSupported(ITypeDescriptorContext context)
            {
                return true;
            }
        }

        /* TODO: delete me, for reference only
         
        public class CEntityTypeConverter
            : TypeConverter
        {
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                if (sourceType == typeof(String))
                    return true;

                return base.CanConvertFrom(context, sourceType);
            }

            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
            {
                return base.ConvertFrom(context, culture, value);
            }

            public new PropertyDescriptorCollection GetProperties(object value)
            {
                return new PropertyDescriptorCollection(new PropertyDescriptor[] { });
            }

            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            {
                return base.CanConvertTo(context, destinationType);
            }

            public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
            {
                return true;
            }
            public new ICollection GetStandardValues()
            {
                return new List<string>() { "one", "two", "three" };
            }
        }

        [TypeConverter(typeof(CSpawnerEntity))]
        public class CEditorSpawnerEntityConverter
            : TypeConverter
        {
            public override bool CanConvertFrom(ITypeDescriptorContext context,
               Type sourceType)
            {
                Console.WriteLine(String.Format("CanConvert(): {0}", sourceType.ToString()));
                return base.CanConvertFrom(context, sourceType);
            }

            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }
        }
        */
    }
}
