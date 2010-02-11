//
// EditorEntityTypes.cs
//

using System;
using System.Collections.Generic;

namespace Galaxy
{
    public static class CEditorEntityTypes
    {
        public static List<Type> Types = new List<Type>() {
            typeof(CEditorEntityUnknown),
            typeof(CEditorEntitySpawnerEntity),
        };
    }
}
