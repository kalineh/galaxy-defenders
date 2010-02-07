//
// StageCodeWriter.cs
//

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Galaxy
{
    public class CStageCodeWriter
    {
        private static string Indent(int count, string s)
        {
            return new String(' ', count) + s;
        }

        private static string PrimitiveToString(object instance)
        {
            Type type = instance.GetType();
            if (type == typeof(float))
                return String.Format("{0:0.0#}f", (float)instance);
            if (type == typeof(bool))
                return instance.ToString().ToLower();
            return instance.ToString();
        }

        private static string PropertyToString(PropertyInfo property, Object instance)
        {
            StringBuilder sb = new StringBuilder();

            // TODO: either write raw type, or recurse into sub-type properties
            if (property.PropertyType.IsPrimitive)
            {
                object value = property.GetValue(instance, null);
                string primitive = PrimitiveToString(value);
                sb.AppendLine(String.Format("{0} = {1},", property.Name, primitive));
            }
            else if (property.PropertyType == typeof(System.Type))
            {
                sb.AppendLine(String.Format("{0} = typeof({1}),", property.Name, property.GetValue(instance, null).ToString()));
            }
            else if (property.PropertyType.IsArray)
            {
                object value = property.GetValue(instance, null);
                sb.AppendLine(String.Format("skip unhandled array: {0}", value.ToString()));
            }
            else if (property.PropertyType.IsGenericType)
            {
                // TODO: assuming sequence type!
                object value = property.GetValue(instance, null);
                bool b = property.PropertyType == typeof(List<>);
                IEnumerable enumerable = value as IEnumerable;
                if (enumerable != null)
                {
                    foreach (Object child in enumerable)
                    {
                        Type child_type = child.GetType();
                        sb.AppendLine(String.Format("new {0}() {{", child_type.ToString()));
                        foreach (PropertyInfo child_property in child_type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                        {
                            string properties = PropertyToString(child_property, child);
                            sb.AppendLine(properties);
                        }
                        sb.AppendLine("});");
                    }
                }
                else
                {
                    sb.AppendLine(String.Format("skip unknown Generic: {0}", value.ToString()));
                }
            }
            else if (property.PropertyType.IsClass)
            {
                Object child = property.GetValue(instance, null);

                sb.AppendLine(String.Format("{0} = new {1}() {{", property.Name, child.GetType().ToString()));
                string child_properties = PropertiesToString(child);
                sb.Append(child_properties);
                sb.AppendLine("},");
            }
            else if (property.PropertyType.IsValueType)
            {
                Object child = property.GetValue(instance, null);

                // TODO: dont need to write properties for value types?
                //string child_properties = PropertiesToString(child);
                //sb.Append(child_properties);

                sb.AppendLine(String.Format("{0} = new {1}() {{", property.Name, child.GetType().ToString()));
                string child_values = ValuesToString(child);
                sb.Append(child_values);
                sb.AppendLine("},");
            }
            else
            {
                sb.AppendLine(String.Format("*** {0} = {1},", property.Name, property.GetValue(instance, null).ToString()));
            }

            return sb.ToString();
        }

        private static string PropertiesToString(Object instance)
        {
            // TODO: base types!
            StringBuilder sb = new StringBuilder();
            Type type = instance.GetType();
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo property in properties)
            {
                string s = PropertyToString(property, instance);
                sb.Append(s);
            }
            return sb.ToString();
        }

        private static string ValuesToString(Object instance)
        {
            StringBuilder sb = new StringBuilder();
            Type type = instance.GetType();
            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach (FieldInfo field in fields)
            {
                object value = field.GetValue(instance);
                string primitive = PrimitiveToString(value);
                //string value = field.GetValue(instance).ToString();
                //if (field.FieldType == typeof(float))
                    //value += "f";
                //if (field.FieldType == typeof(bool))
                    //value = value.ToLower();

                sb.AppendLine(String.Format("{0} = {1},", field.Name, primitive));
            }
            
            return sb.ToString();
        }

        public static void WriteObjectGraph(StringBuilder sb, object instance)
        {
            if (instance == null)
            {
                sb.AppendLine("null,");
                return;
            }

            Type type = instance.GetType();
            if (instance is System.Type)
            {
                WriteType(sb, instance);
            }
            else if (type.IsPrimitive)
            {
                WritePrimitive(sb, instance);
            }
            else if (type.IsValueType)
            {
                WriteValueGraph(sb, instance);
            }
            else if (type.IsArray)
            {
                //WriteArray(sb, instance);
            }
            else if (type.IsGenericType)
            {
                WriteList(sb, instance);
            }
            else if (type.IsClass)
            {
                WriteClassGraph(sb, instance);
            }
            else
            {
                throw new Exception(String.Format("WriteObjectGraph(): unhandled type: {0}", type.ToString()));
            }
        }

        public static void WriteType(StringBuilder sb, object instance)
        {
            sb.AppendLine(String.Format("typeof({0}),", instance.ToString()));
        }

        public static void WriteClassGraph(StringBuilder sb, object instance)
        {
            Type type = instance.GetType();
            sb.AppendLine(String.Format("new {0}() {{", type.ToString()));

            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo property in properties)
            {
                object child = property.GetValue(instance, null);
                sb.Append(String.Format("{0} = ", property.Name));
                WriteObjectGraph(sb, child);
            }

            sb.AppendLine("},");
        }

        public static void WriteValueGraph(StringBuilder sb, object instance)
        {
            Type type = instance.GetType();
            sb.AppendLine(String.Format("new {0}() {{", type.ToString()));

            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach (FieldInfo field in fields)
            {
                object child = field.GetValue(instance);
                sb.Append(String.Format("{0} = ", field.Name));
                WriteObjectGraph(sb, child);
            }

            sb.AppendLine("},");
        }

        public static void WritePrimitive(StringBuilder sb, object instance)
        {
            Type type = instance.GetType();
            if (type == typeof(float))
                sb.Append(String.Format("{0:0.0#}f", (float)instance));
            else if (type == typeof(bool))
                sb.Append(instance.ToString().ToLower());
            else
                sb.Append(instance.ToString());
            sb.AppendLine(",");
        }

        public static void WriteArray(StringBuilder sb, object instance)
        {
            Type type = instance.GetType();
            sb.AppendLine(String.Format("new {0}() {{", type.ToString()));
            // write data
                // foreach element
                //   write graph
            sb.AppendLine("}");
        }

        public static void WriteList(StringBuilder sb, object instance)
        {
            // TODO: if not list this will die horribly

            Type type = instance.GetType();
            sb.AppendLine(String.Format("new System.Collections.Generic.List<{0}>() {{", type.GetGenericArguments()[0]));

            IEnumerable enumerable = instance as IEnumerable;
            if (enumerable != null)
            {
                foreach (Object child in enumerable)
                {
                    WriteObjectGraph(sb, child);
                }
            }

            sb.AppendLine("},");
        }

        public static void Save(CStageDefinition stage_definition)
        {
            StringBuilder sb = new StringBuilder(50 * 1024 * 1024);
            string stage = stage_definition.Name;
            string stage_filename = stage_definition.ToFilename();

            // header
            sb.AppendLine("//");
            sb.AppendLine(String.Format("// {0}.cs", stage));
            sb.AppendLine("//");

            // namespaces
            sb.AppendLine("namespace Galaxy {");
            sb.AppendLine("namespace Stages {");
            sb.AppendLine(String.Format("public class {0} {{", stage));

            // definition opening
            sb.AppendLine("public static CStageDefinition GenerateDefinition() {");
            sb.AppendLine(String.Format("CStageDefinition stage = new CStageDefinition(\"{0}\");", stage));

            // stage data
            foreach (KeyValuePair<int, List<CStageElement>> time_element in stage_definition.Elements)
            {
                int time = time_element.Key;
                foreach (CStageElement element in time_element.Value)
                {
                    sb.AppendLine(String.Format("stage.AddElement({0}, ", time));
                    WriteObjectGraph(sb, element);
                    sb.Length = sb.Length - 3; // newline + carriage return + trailing comma
                    sb.AppendLine(");");
                }
                sb.AppendLine("");
            }

            // end definition
            sb.AppendLine("return stage;");
            sb.AppendLine("}");

            // close namespaces
            sb.AppendLine("}");
            sb.AppendLine("} // namespace Stages");
            sb.AppendLine("} // namespace Galaxy");

            // write file
            TextWriter tw = new StreamWriter(stage_filename);
            tw.Write(sb.ToString());
            tw.Close();
        }
    }
}
