//
// EditorConverters.cs
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Galaxy
{
    public static class CEditorConverter
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
