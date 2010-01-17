//
// Stage1.cs
//

using System.Collections.Generic;
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

                // TEST
                stage.AddElement(0.0f, new CSpawnerEntity
                {
                    Type = typeof(CTurret),
                    SpawnCount = 3,
                    SpawnPosition = new CSpawnPositionFixed() { Position = new Vector2(400.0f, -100.0f) },
                    SpawnTimer = new CSpawnTimerInterval() { Delay = 1.0f },
                    CustomElement = new CSpawnerCustomMover() {
                        Mover = new CMoverSequence() {
                            Velocity = new List<Vector2>() {
                                new Vector2(0.0f, 2.0f),
                                new Vector2(-2.0f, 0.0f),
                                new Vector2(0.0f, 2.0f)
                            },
                            Duration = new List<float>() {
                                1.5f,
                                1.0f,
                                0.0f
                            },
                            VelocityLerpRate = 0.05f,
                        },
                    }
                });

                stage.AddElement(0.0f, new CSpawnerEntity
                {
                    Type = typeof(CTurret),
                    SpawnCount = 3,
                    SpawnPosition = new CSpawnPositionFixed() { Position = new Vector2(500.0f, -100.0f) },
                    SpawnTimer = new CSpawnTimerInterval() { Delay = 1.0f },
                    CustomElement = new CSpawnerCustomMover() {
                        Mover = new CMoverSequence() {
                            Velocity = new List<Vector2>() {
                                new Vector2(0.0f, 2.0f),
                                new Vector2(2.0f, 0.0f),
                                new Vector2(0.0f, 2.0f)
                            },
                            Duration = new List<float>() {
                                1.5f,
                                1.0f,
                                0.0f
                            },
                            VelocityLerpRate = 0.05f,
                        },
                    }
                });

                // wave 1
                stage.AddElement(0.0f, new CSpawnerEntity {
                    Type = typeof(CSinBall),
                    SpawnCount = 3,
                    SpawnPosition = new CSpawnPositionFixed() { Position = new Vector2(200.0f, -100.0f) },
                    SpawnTimer = new CSpawnTimerInterval() { Delay = 0.6f },
                    CustomElement = new CSpawnerCustomMover() { Mover = new CMoverFixedVelocity() { Velocity = new Vector2(0.0f, 2.5f) } },
                });

                stage.AddElement(3.0f, new CSpawnerEntity {
                    Type = typeof(CSinBall),
                    SpawnCount = 3,
                    SpawnPosition = new CSpawnPositionFixed() { Position = new Vector2(400.0f, -100.0f) },
                    SpawnTimer = new CSpawnTimerInterval() { Delay = 0.6f },
                    CustomElement = new CSpawnerCustomMover() { Mover = new CMoverFixedVelocity() { Velocity = new Vector2(0.0f, 2.5f) } },
                });

                stage.AddElement(6.0f, new CSpawnerEntity {
                    Type = typeof(CSinBall),
                    SpawnCount = 3,
                    SpawnPosition = new CSpawnPositionFixed() { Position = new Vector2(600.0f, -100.0f) },
                    SpawnTimer = new CSpawnTimerInterval() { Delay = 0.6f },
                    CustomElement = new CSpawnerCustomMover() { Mover = new CMoverFixedVelocity() { Velocity = new Vector2(0.0f, 2.5f) } },
                });

                // heavy asteroids
                stage.AddElement(10.0f, new CSpawnerEntity {
                    Type = typeof(CAsteroid),
                    SpawnCount = 30,
                    SpawnPosition = new CSpawnPositionRandom(),
                    SpawnTimer = new CSpawnTimerRandom() { Frequency = 0.11f, IncreaseRate = 0.01f },
                    CustomElement = new CSpawnerCustomAsteroid(),
                });

                // wave 2
                stage.AddElement(20.0f, new CSpawnerEntity {
                    Type = typeof(CPewPew),
                    SpawnCount = 4,
                    SpawnPosition = new CSpawnPositionFixed() { Position = new Vector2(200.0f, -100.0f) },
                    SpawnTimer = new CSpawnTimerInterval() { Delay = 0.8f },
                    CustomElement = new CSpawnerCustomMover() { Mover = new CMoverFixedVelocity() { Velocity = new Vector2(1.5f, 1.5f) } },
                });

                stage.AddElement(28.0f, new CSpawnerEntity {
                    Type = typeof(CPewPew),
                    SpawnCount = 4,
                    SpawnPosition = new CSpawnPositionFixed() { Position = new Vector2(600.0f, -100.0f) },
                    SpawnTimer = new CSpawnTimerInterval() { Delay = 0.8f },
                    CustomElement = new CSpawnerCustomMover() { Mover = new CMoverFixedVelocity() { Velocity = new Vector2(-1.5f, 1.5f) } },
                });

                // heavy asteroids
                stage.AddElement(32.0f, new CSpawnerEntity {
                    Type = typeof(CAsteroid),
                    SpawnCount = 30,
                    SpawnPosition = new CSpawnPositionRandom(),
                    SpawnTimer = new CSpawnTimerRandom() { Frequency = 0.11f, IncreaseRate = 0.01f },
                    CustomElement = new CSpawnerCustomAsteroid(),
                });

                // boss
                stage.AddElement(38.0f, new CSpawnerEntity {
                    Type = typeof(CSinBall),
                    SpawnCount = 1,
                    SpawnPosition = new CSpawnPositionFixed() { Position = new Vector2(400.0f, -100.0f) },
                    SpawnTimer = new CSpawnTimerInterval() { Delay = 1.0f },
                    CustomElement = new CSpawnerCustomCode() { Code = SinBallBoss },
                });

                // stage end
                stage.AddElement(60.0f, new CStageFinish());

                return stage;
            }

            public static void SinBallBoss(CEntity entity)
            {
                CSinBall boss = entity as CSinBall;

                boss.Health *= 15.0f;
                boss.BonusDrop = 30;
                boss.Visual.Scale *= 2.5f;
                boss.Mover = new CMoverFixedVelocity() { Velocity = new Vector2(0.0f, 0.3f) };
                CollisionCircle collision = boss.Collision as CollisionCircle;
                collision.Radius *= 2.5f;
            }

        };
    }
}