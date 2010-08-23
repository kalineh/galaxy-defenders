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
        public struct FakeMethodInfo
        {
            public string Name;
        }

        public class StackFrame
        {
            public StackFrame(int a, bool b)
            {
            }

            public FakeMethodInfo GetMethod()
            {
                return new FakeMethodInfo();
            }
        }
    }
}

#endif
