//
// EditorConverters.cs
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Linq;

namespace Galaxy
{
    public class CEditorConverterGenerated
        : ExpandableObjectConverter
    {
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            List<string> names = new List<string>();
            Type type = value.GetType();
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo property in properties)
            {
                object[] attribs = property.GetCustomAttributes(typeof(CategoryAttribute), true);
                if (attribs.Count() == 0)
                    continue;
                names.Add(property.Name);
            }

            return CEditorConverter.GetPropertyDescriptors(value, names.ToArray());
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
    }

    public static class CEditorConverter
    {
        // TODO: move me somewhere goodly
        public static IEnumerable<T> MakeEnumerable<T>(IEnumerable collection)
        {
            foreach (object x in collection)
                if (x is T)
                    yield return (T)x;
        }

        public static PropertyDescriptorCollection GetPropertyDescriptors(object value, params string[] names)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(value);
            IEnumerable<PropertyDescriptor> enumerator = MakeEnumerable<PropertyDescriptor>(properties);
            var filtered = from p in enumerator where names.Contains(p.Name) select p;
            return new PropertyDescriptorCollection( filtered.ToArray() );
        }
    }
}
