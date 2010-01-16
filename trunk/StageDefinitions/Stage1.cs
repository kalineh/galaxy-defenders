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

                // asteroids
                stage.AddElement(0.0f, new CSpawnerEntity {
                    Type = typeof(CAsteroid),
                    SpawnCount = 30,
                    SpawnPosition = new CSpawnPositionRandom(),
                    SpawnTimer = new CSpawnTimerRandom(),
                    CustomElement = new CSpawnerCustomAsteroid(),
                });

                // wave 1
                stage.AddElement(0.0f, new CSpawnerEntity {
                    Type = typeof(CSinBall),
                    SpawnCount = 3,
                    SpawnPosition = new CSpawnPositionFixed() { Position = new Vector2(200.0f, -100.0f) },
                    SpawnTimer = new CSpawnTimerInterval() { Delay = 1.0f },
                    CustomElement = new CSpawnerCustomMover() { Mover = new CMoverFixedVelocity() { Velocity = new Vector2(0.0f, 1.5f) } },
                });

                stage.AddElement(6.0f, new CSpawnerEntity {
                    Type = typeof(CSinBall),
                    SpawnCount = 3,
                    SpawnPosition = new CSpawnPositionFixed() { Position = new Vector2(400.0f, -100.0f) },
                    SpawnTimer = new CSpawnTimerInterval() { Delay = 1.0f },
                    CustomElement = new CSpawnerCustomMover() { Mover = new CMoverFixedVelocity() { Velocity = new Vector2(0.0f, 1.5f) } },
                });

                stage.AddElement(12.0f, new CSpawnerEntity {
                    Type = typeof(CSinBall),
                    SpawnCount = 3,
                    SpawnPosition = new CSpawnPositionFixed() { Position = new Vector2(600.0f, -100.0f) },
                    SpawnTimer = new CSpawnTimerInterval() { Delay = 1.0f },
                    CustomElement = new CSpawnerCustomMover() { Mover = new CMoverFixedVelocity() { Velocity = new Vector2(0.0f, 1.5f) } },
                });

                // heavy asteroids
                stage.AddElement(18.0f, new CSpawnerEntity {
                    Type = typeof(CAsteroid),
                    SpawnCount = 20,
                    SpawnPosition = new CSpawnPositionRandom(),
                    SpawnTimer = new CSpawnTimerRandom() { Frequency = 0.08f, IncreaseRate = 0.01f },
                    CustomElement = new CSpawnerCustomAsteroid(),
                });

                // wave 2
                stage.AddElement(30.0f, new CSpawnerEntity {
                    Type = typeof(CPewPew),
                    SpawnCount = 4,
                    SpawnPosition = new CSpawnPositionFixed() { Position = new Vector2(200.0f, -100.0f) },
                    SpawnTimer = new CSpawnTimerInterval() { Delay = 1.0f },
                    CustomElement = new CSpawnerCustomMover() { Mover = new CMoverFixedVelocity() { Velocity = new Vector2(1.5f, 1.5f) } },
                });

                stage.AddElement(34.0f, new CSpawnerEntity {
                    Type = typeof(CPewPew),
                    SpawnCount = 4,
                    SpawnPosition = new CSpawnPositionFixed() { Position = new Vector2(600.0f, -100.0f) },
                    SpawnTimer = new CSpawnTimerInterval() { Delay = 1.0f },
                    CustomElement = new CSpawnerCustomMover() { Mover = new CMoverFixedVelocity() { Velocity = new Vector2(-1.5f, 1.5f) } },
                });

                // heavy asteroids
                stage.AddElement(42.0f, new CSpawnerEntity {
                    Type = typeof(CAsteroid),
                    SpawnCount = 20,
                    SpawnPosition = new CSpawnPositionRandom(),
                    SpawnTimer = new CSpawnTimerRandom() { Frequency = 0.08f, IncreaseRate = 0.01f },
                    CustomElement = new CSpawnerCustomAsteroid(),
                });

                // boss
                stage.AddElement(50.0f, new CSpawnerEntity {
                    Type = typeof(CSinBall),
                    SpawnCount = 1,
                    SpawnPosition = new CSpawnPositionFixed() { Position = new Vector2(400.0f, -100.0f) },
                    SpawnTimer = new CSpawnTimerInterval() { Delay = 1.0f },
                    CustomElement = new CSpawnerCustomCode() { Code = SinBallBoss },
                });

                return stage;
            }

            public static void SinBallBoss(CEntity entity)
            {
                CSinBall boss = entity as CSinBall;

                boss.Health *= 15.0f;
                boss.Visual.Scale *= 2.5f;
                boss.Mover = new CMoverFixedVelocity() { Velocity = new Vector2(0.0f, 0.3f) };
                CollisionCircle collision = boss.Collision as CollisionCircle;
                collision.Radius *= 2.5f;
            }

        };
    }
}