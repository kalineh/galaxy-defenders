//
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
                    Frequency = Units.PercentToRatio(3),
                    IncreaseRate = 0.0f,
                    SpawnCount = 100,
                });

                stage.AddElement(1.0f, new CStageEnemySpawner {
                    Type = typeof(CSinBall),
                    Frequency = Units.PercentToRatio(5),
                    IncreaseRate = 0.0f,
                    SpawnCount = 10,
                });

                return stage;
            }
        };
    }
}