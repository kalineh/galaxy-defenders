//
// Stage1.cs
//

using Microsoft.Xna.Framework;

namespace Galaxy
{
    namespace Stages
    {
        public class Stage1
        {
            public static CStageDefinition GenerateDefinition()
            {
                CStageDefinition stage = new CStageDefinition("Stage1");

                stage.AddElement(0.0f, new CStageAsteroidRandomSpawner {
                    Frequency = Units.PercentToRatio(3),
                    IncreaseRate = 0.0f,
                    SpawnCount = 100,
                });

                stage.AddElement(0.0f, new CStageEnemyRandomSpawner {
                    Type = typeof(CSinBall),
                    Frequency = Units.PercentToRatio(5),
                    IncreaseRate = 0.0f,
                    SpawnCount = 10,
                    SpawnPosition = new CRandomSpawnPosition(),
                });

                stage.AddElement(0.0f, new CStageEnemyRandomSpawner {
                    Type = typeof(CSinBall),
                    Frequency = Units.PercentToRatio(3),
                    IncreaseRate = 0.0f,
                    SpawnCount = 3,
                    CustomMover = new CFixedVelocityMover() { Velocity = Vector2.UnitY * 2.0f },
                    SpawnPosition = new CFixedSpawnPosition() { Position = new Vector2(200.0f, -100.0f) }
                });

                stage.AddElement(0.0f, new CStageEnemyRandomSpawner {
                    Type = typeof(CPewPew),
                    Frequency = Units.PercentToRatio(100),
                    IncreaseRate = 0.0f,
                    SpawnCount = 1,
                    SpawnPosition = new CRandomSpawnPosition()
                });

                return stage;
            }
        };
    }
}