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
            { typeof(CStageElementAsteroidField), typeof(CEditorEntityAsteroidField) },
            { typeof(CStageElementBuilding), typeof(CEditorEntityBuilding) },
            { typeof(CStageElementCameraStop), typeof(CEditorEntityCameraStop) },
            { typeof(CStageElementDecoration), typeof(CEditorEntityDecoration) },
            { typeof(CStageElementMusicChanger), typeof(CEditorEntityMusicChanger) },
            { typeof(CStageElementParticleEffectPlayer), typeof(CEditorEntityParticleEffectPlayer) },
            { typeof(CStageElementSecretEntry), typeof(CEditorEntitySecretEntry) },
            { typeof(CStageElementSpawnerEntity), typeof(CEditorEntitySpawnerEntity) },
            { typeof(CStageElementSpawnerEnemy), typeof(CEditorEntitySpawnerEnemy) },
            { typeof(CStageElementStageFinish), typeof(CEditorEntityStageFinish) },
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
