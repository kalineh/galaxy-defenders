//
// EntityMoverPresetSelector.cs
//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Globalization;

namespace Galaxy
{
    public class CEntityMoverTypeConverter
        : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destination_type)
        {
            return destination_type == typeof(String);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destination_type)
        {
            if (destination_type == typeof(String))
            {
                CMover mover = value as CMover;
                if (mover.Name != null && mover.Name.Length > 0)
                    return mover.Name;
            }
            return base.ConvertTo(context, culture, value, destination_type);
        }
    }
}
