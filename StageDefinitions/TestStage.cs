//
// TestStage.cs
//

using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Galaxy
{
    namespace Stages
    {
        public class TestStage
        {
            public static CStageDefinition GenerateDefinition()
            {
                CStageDefinition stage = new CStageDefinition("TestStage");

                float StageTime = 0.0f;

                // asteroids
                StageTime += 2.0f;
                stage.AddElement(Time.ToFrames(StageTime), new CSpawnerEntity {
                    Type = typeof(CAsteroid),
                    SpawnCount = 20,
                    SpawnPosition = new CSpawnPositionRandom(),
                    SpawnTimer = new CSpawnTimerRandom() { Frequency = 0.07f, IncreaseRate = 0.0f },
                    CustomElement = new CSpawnerCustomAsteroid(),
                });

                // Turret wave
                StageTime += 6.0f;
                stage.AddElement(Time.ToFrames(StageTime), new CSpawnerEntity {
                    Type = typeof(CTurret),
                    SpawnCount = 3,
                    SpawnPosition = new CSpawnPositionFixed() { Position = new Vector2(350.0f, -100.0f) },
                    SpawnTimer = new CSpawnTimerInterval() { Interval = 1.0f },
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
                stage.AddElement(Time.ToFrames(StageTime), new CSpawnerEntity
                {
                    Type = typeof(CTurret),
                    SpawnCount = 3,
                    SpawnPosition = new CSpawnPositionFixed() { Position = new Vector2(450.0f, -100.0f) },
                    SpawnTimer = new CSpawnTimerInterval() { Interval = 1.0f },
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
                stage.AddElement(Time.ToFrames(StageTime), new CSpawnerEntity {
                    Type = typeof(CSinBall),
                    SpawnCount = 3,
                    SpawnPosition = new CSpawnPositionFixed() { Position = new Vector2(200.0f, -100.0f) },
                    SpawnTimer = new CSpawnTimerInterval() { Interval = 0.6f },
                    CustomElement = new CSpawnerCustomMover() { Mover = new CMoverFixedVelocity() { Velocity = new Vector2(0.0f, 2.5f) } },
                });

                StageTime += 4.0f;
                stage.AddElement(Time.ToFrames(StageTime), new CSpawnerEntity {
                    Type = typeof(CSinBall),
                    SpawnCount = 3,
                    SpawnPosition = new CSpawnPositionFixed() { Position = new Vector2(400.0f, -100.0f) },
                    SpawnTimer = new CSpawnTimerInterval() { Interval = 0.6f },
                    CustomElement = new CSpawnerCustomMover() { Mover = new CMoverFixedVelocity() { Velocity = new Vector2(0.0f, 2.5f) } },
                });

                StageTime += 4.0f;
                stage.AddElement(Time.ToFrames(StageTime), new CSpawnerEntity {
                    Type = typeof(CSinBall),
                    SpawnCount = 3,
                    SpawnPosition = new CSpawnPositionFixed() { Position = new Vector2(600.0f, -100.0f) },
                    SpawnTimer = new CSpawnTimerInterval() { Interval = 0.6f },
                    CustomElement = new CSpawnerCustomMover() { Mover = new CMoverFixedVelocity() { Velocity = new Vector2(0.0f, 2.5f) } },
                });

                // heavy asteroids
                StageTime += 6.0f;
                stage.AddElement(Time.ToFrames(StageTime), new CSpawnerEntity {
                    Type = typeof(CAsteroid),
                    SpawnCount = 30,
                    SpawnPosition = new CSpawnPositionRandom(),
                    SpawnTimer = new CSpawnTimerRandom() { Frequency = 0.11f, IncreaseRate = 0.01f },
                    CustomElement = new CSpawnerCustomAsteroid(),
                });

                // wave 2
                StageTime += 8.0f;
                stage.AddElement(Time.ToFrames(StageTime), new CSpawnerEntity {
                    Type = typeof(CPewPew),
                    SpawnCount = 4,
                    SpawnPosition = new CSpawnPositionFixed() { Position = new Vector2(200.0f, -100.0f) },
                    SpawnTimer = new CSpawnTimerInterval() { Interval = 0.8f },
                    CustomElement = new CSpawnerCustomMover() { Mover = new CMoverFixedVelocity() { Velocity = new Vector2(1.5f, 1.5f) } },
                });

                StageTime += 4.0f;
                stage.AddElement(Time.ToFrames(StageTime), new CSpawnerEntity {
                    Type = typeof(CPewPew),
                    SpawnCount = 4,
                    SpawnPosition = new CSpawnPositionFixed() { Position = new Vector2(600.0f, -100.0f) },
                    SpawnTimer = new CSpawnTimerInterval() { Interval = 0.8f },
                    CustomElement = new CSpawnerCustomMover() { Mover = new CMoverFixedVelocity() { Velocity = new Vector2(-1.5f, 1.5f) } },
                });

                // heavy asteroids
                StageTime += 8.0f;
                stage.AddElement(Time.ToFrames(StageTime), new CSpawnerEntity {
                    Type = typeof(CAsteroid),
                    SpawnCount = 30,
                    SpawnPosition = new CSpawnPositionRandom(),
                    SpawnTimer = new CSpawnTimerRandom() { Frequency = 0.11f, IncreaseRate = 0.01f },
                    CustomElement = new CSpawnerCustomAsteroid(),
                });

                // boss
                StageTime += 8.0f;
                stage.AddElement(Time.ToFrames(StageTime), new CSpawnerEntity {
                    Type = typeof(CSinBall),
                    SpawnCount = 1,
                    SpawnPosition = new CSpawnPositionFixed() { Position = new Vector2(400.0f, -100.0f) },
                    SpawnTimer = new CSpawnTimerInterval() { Interval = 1.0f },
                    CustomElement = new CSpawnerCustomCode() { Code = SinBallBoss },
                });

                // stage end
                StageTime += 12.0f;
                stage.AddElement(Time.ToFrames(StageTime), new CStageFinish());

                return stage;
            }

            public static void SinBallBoss(CEntity entity)
            {
                CSinBall boss = entity as CSinBall;

                boss.Health *= 15.0f;
                boss.BonusDrop = 30;
                boss.Visual.Scale *= 2.5f;
                boss.Mover = new CMoverSequence()
                {
                    Velocity = new List<Vector2>() {
                        new Vector2(0.0f, 1.0f),
                        new Vector2(0.0f, 0.0f),
                    },
                    Duration = new List<float>() {
                        3.5f,
                        0.0f,
                    },
                    VelocityLerpRate = 0.02f,
                };
                CollisionCircle collision = boss.Collision as CollisionCircle;
                collision.Radius *= 2.5f;
            }

        };
    }
}