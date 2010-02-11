//
// EditorEntityTypes.cs
//

using System;
using System.Linq;
using System.Collections.Generic;

namespace Galaxy
{
    public static class CEditorEntityTypes
    {
        public static List<Type> Types 
        {
            get {
                return ElementToEditorEntityMapping.Values.ToList();
            }
        }

        public static Dictionary<Type, Type> ElementToEditorEntityMapping = new Dictionary<Type, Type>()
        {
            { typeof(CStageElement), typeof(CEditorEntityUnknown) },
            { typeof(CSpawnerEntity), typeof(CEditorEntitySpawnerEntity) },
            { typeof(CStageFinish), typeof(CEditorEntityStageFinish) },
            { typeof(CAsteroidField), typeof(CEditorEntityAsteroidField) },
            // more
        };

        public static Dictionary<Type, Type> EditorEntityToElementMapping = null;

        static CEditorEntityTypes()
        {
            EditorEntityToElementMapping = new Dictionary<Type, Type>();
            foreach (KeyValuePair<Type, Type> kv in ElementToEditorEntityMapping)
            {
                EditorEntityToElementMapping.Add(kv.Value, kv.Key); 
            }
        }

    }
}
