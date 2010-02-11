//
// EditorEntityTypes.cs
//

using System;
using System.Collections.Generic;

namespace Galaxy
{
    namespace Editor
    {
        public static class CEditorEntityTypes
        {
            public static List<Type> EntityTypeList = new List<Type>() {
                typeof(CAsteroid),
                typeof(CBeard),
                typeof(CPewPew),
                typeof(CSinBall),
                typeof(CTurret),
                typeof(CSpacePlatform),
                typeof(CBigSpacePlatform),
                typeof(CBuilding),
            };

            public static List<string> ToNames()
            {
                List<string> results = new List<string>();

                foreach (Type type in EntityTypeList)
                {
                    results.Add(type.Name);
                }

                return results;
            }
        }
    }
}
