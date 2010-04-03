//
// Stubs360.cs
//

#if XBOX360

using System;
using System.Reflection;

namespace Galaxy
{
    [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Struct)]
    public class TypeConverter
        : System.Attribute
    {
        public TypeConverter(Type ignore)
        {
        }
    }
    [System.AttributeUsage(System.AttributeTargets.Property | System.AttributeTargets.Field)]
    public class CategoryAttribute
        : System.Attribute
    {
        public CategoryAttribute(string ignore)
        {
        }
    }
    [System.AttributeUsage(System.AttributeTargets.Property | System.AttributeTargets.Field)]
    public class Browsable
        : System.Attribute
    {
        public Browsable(bool ignore)
        {
        }
    }

    namespace Diagnostics
    {
        public class StackFrame
        {
            public StackFrame(int a, bool b)
            {
            }

            public MethodInfo GetMethod()
            {
                return null;
            }
        }
    }

    public static class ActivatorExtensions
    {
        public static object CreateInstance(Type type, params object[] arguments)
        {
            object entity = Activator.CreateInstance(type);

            try
            {
                MethodInfo method = type.GetMethod("Init360");
                method.Invoke(entity, arguments);
            }
            catch (System.Reflection.AmbiguousMatchException)
            {
                try
                {
                    MethodInfo method = type.GetMethod("Init360", BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
                    method.Invoke(entity, arguments);
                }
                catch (System.MissingMethodException e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            return entity;
        }
    }
}

#endif
