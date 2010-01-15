﻿//
// Stage1.cs
//

namespace galaxy
{
    namespace Stages
    {
        public class Stage1
        {
            public static CStageDefinition GenerateDefinition()
            {
                CStageDefinition stage = new CStageDefinition("Stage1");

                stage.AddElement(0.0f, new CStageAsteroidSpawner {
                    Frequency = 0.01f,
                    IncreaseRate = 0.0f,
                    SpawnCount = 100,
                });

                return stage;
            }
        };
    }
}