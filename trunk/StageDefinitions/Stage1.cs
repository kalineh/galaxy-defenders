﻿//
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

                float StageTime = 0.0f;

                // asteroids
                StageTime += 2.0f;
                stage.AddElement(StageTime, new CSpawnerEntity {
                    Type = typeof(CAsteroid),
                    SpawnCount = 10,
                    SpawnPosition = new CSpawnPositionRandom(),
                    SpawnTimer = new CSpawnTimerRandom() { Frequency = 0.11f, IncreaseRate = 0.01f },
                    CustomElement = new CSpawnerCustomAsteroid(),
                });


                // Turret wave
                StageTime += 6.0f;
                stage.AddElement(StageTime, new CSpawnerEntity
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

                StageTime += 6.0f;
                stage.AddElement(StageTime, new CSpawnerEntity
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
                StageTime += 8.0f;
                stage.AddElement(StageTime, new CSpawnerEntity {
                    Type = typeof(CSinBall),
                    SpawnCount = 3,
                    SpawnPosition = new CSpawnPositionFixed() { Position = new Vector2(200.0f, -100.0f) },
                    SpawnTimer = new CSpawnTimerInterval() { Delay = 0.6f },
                    CustomElement = new CSpawnerCustomMover() { Mover = new CMoverFixedVelocity() { Velocity = new Vector2(0.0f, 2.5f) } },
                });

                StageTime += 4.0f;
                stage.AddElement(StageTime, new CSpawnerEntity {
                    Type = typeof(CSinBall),
                    SpawnCount = 3,
                    SpawnPosition = new CSpawnPositionFixed() { Position = new Vector2(400.0f, -100.0f) },
                    SpawnTimer = new CSpawnTimerInterval() { Delay = 0.6f },
                    CustomElement = new CSpawnerCustomMover() { Mover = new CMoverFixedVelocity() { Velocity = new Vector2(0.0f, 2.5f) } },
                });

                StageTime += 4.0f;
                stage.AddElement(StageTime, new CSpawnerEntity {
                    Type = typeof(CSinBall),
                    SpawnCount = 3,
                    SpawnPosition = new CSpawnPositionFixed() { Position = new Vector2(600.0f, -100.0f) },
                    SpawnTimer = new CSpawnTimerInterval() { Delay = 0.6f },
                    CustomElement = new CSpawnerCustomMover() { Mover = new CMoverFixedVelocity() { Velocity = new Vector2(0.0f, 2.5f) } },
                });

                // heavy asteroids
                StageTime += 6.0f;
                stage.AddElement(StageTime, new CSpawnerEntity {
                    Type = typeof(CAsteroid),
                    SpawnCount = 30,
                    SpawnPosition = new CSpawnPositionRandom(),
                    SpawnTimer = new CSpawnTimerRandom() { Frequency = 0.11f, IncreaseRate = 0.01f },
                    CustomElement = new CSpawnerCustomAsteroid(),
                });

                // wave 2
                StageTime += 8.0f;
                stage.AddElement(StageTime, new CSpawnerEntity {
                    Type = typeof(CPewPew),
                    SpawnCount = 4,
                    SpawnPosition = new CSpawnPositionFixed() { Position = new Vector2(200.0f, -100.0f) },
                    SpawnTimer = new CSpawnTimerInterval() { Delay = 0.8f },
                    CustomElement = new CSpawnerCustomMover() { Mover = new CMoverFixedVelocity() { Velocity = new Vector2(1.5f, 1.5f) } },
                });

                StageTime += 4.0f;
                stage.AddElement(StageTime, new CSpawnerEntity {
                    Type = typeof(CPewPew),
                    SpawnCount = 4,
                    SpawnPosition = new CSpawnPositionFixed() { Position = new Vector2(600.0f, -100.0f) },
                    SpawnTimer = new CSpawnTimerInterval() { Delay = 0.8f },
                    CustomElement = new CSpawnerCustomMover() { Mover = new CMoverFixedVelocity() { Velocity = new Vector2(-1.5f, 1.5f) } },
                });

                // heavy asteroids
                StageTime += 8.0f;
                stage.AddElement(StageTime, new CSpawnerEntity {
                    Type = typeof(CAsteroid),
                    SpawnCount = 30,
                    SpawnPosition = new CSpawnPositionRandom(),
                    SpawnTimer = new CSpawnTimerRandom() { Frequency = 0.11f, IncreaseRate = 0.01f },
                    CustomElement = new CSpawnerCustomAsteroid(),
                });

                // boss
                StageTime += 8.0f;
                stage.AddElement(StageTime, new CSpawnerEntity {
                    Type = typeof(CSinBall),
                    SpawnCount = 1,
                    SpawnPosition = new CSpawnPositionFixed() { Position = new Vector2(400.0f, -100.0f) },
                    SpawnTimer = new CSpawnTimerInterval() { Delay = 1.0f },
                    CustomElement = new CSpawnerCustomCode() { Code = SinBallBoss },
                });

                // stage end
                StageTime += 12.0f;
                stage.AddElement(StageTime, new CStageFinish());

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