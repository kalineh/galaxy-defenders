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

                stage.AddElement(0.0f, new CSpawnerEntity
                {
                    Type = typeof(CAsteroid),
                    SpawnCount = 100,
                    SpawnPosition = new CSpawnPositionRandom(),
                    SpawnTimer = new CSpawnTimerRandom(),
                    CustomElement = new CSpawnerCustomAsteroid(),
                });

                stage.AddElement(0.0f, new CSpawnerEntity {
                    Type = typeof(CSinBall),
                    SpawnCount = 10,
                    SpawnPosition = new CSpawnPositionRandom(),
                    SpawnTimer = new CSpawnTimerRandom(),
                });

                stage.AddElement(0.0f, new CSpawnerEntity {
                    Type = typeof(CSinBall),
                    SpawnCount = 3,
                    SpawnPosition = new CSpawnPositionFixed() { Position = new Vector2(200.0f, -100.0f) },
                    SpawnTimer = new CSpawnTimerInterval() { Delay = 1.0f },
                    CustomElement = new CSpawnerCustomMover() { Mover = new CMoverFixedVelocity() { Velocity = new Vector2(1.0f, 1.0f) } },
                });

                stage.AddElement(0.0f, new CSpawnerEntity {
                    Type = typeof(CPewPew),
                    SpawnCount = 1,
                    SpawnPosition = new CSpawnPositionRandom(),
                    SpawnTimer = new CSpawnTimerRandom(),
                });

                return stage;
            }
        };
    }
}