﻿//
// Stage1.cs
//


namespace Galaxy
{
    namespace Stages
    {
        public class Stage1
        {
            public static float GravitySpeed = 1.0f;

            private static float WaveBigAsteroids(float stage_time, CStageDefinition stage)
            {
                stage_time += 0.0f;
                stage.AddElement(Time.ToFrames(stage_time), new CSpawnerEntity {
                    Type = typeof(CAsteroid),
                    SpawnCount = 20,
                    SpawnPosition = new CSpawnPositionRandom(),
                    SpawnTimer = new CSpawnTimerRandom() { Frequency = 0.001f, IncreaseRate = 0.0f },
                    CustomElement = new CSpawnerCustomBigAsteroid(),
                });

                return stage_time;
            }

            private static float WaveVerticalTurret(float stage_time, CStageDefinition stage)
            {
                stage_time += 2.0f;
                stage.AddElement(Time.ToFrames(stage_time), CStagePresets.MakeWave(
                    typeof(CTurret), 4,
                    CStagePresets.PositionTopLeftCenter(),
                    CStagePresets.TimerInterval(0.2f),
                    CStagePresets.MoveDown(5.0f)
                ));

                stage_time += 2.0f;
                stage.AddElement(Time.ToFrames(stage_time), CStagePresets.MakeWave(
                    typeof(CTurret), 4,
                    CStagePresets.PositionTopRightCenter(),
                    CStagePresets.TimerInterval(0.2f),
                    CStagePresets.MoveDown(5.0f)
                ));

                stage_time += 2.0f;
                stage.AddElement(Time.ToFrames(stage_time), CStagePresets.MakeWave(
                    typeof(CTurret), 4,
                    CStagePresets.PositionTopCenter(),
                    CStagePresets.TimerInterval(0.2f),
                    CStagePresets.MoveDown(5.0f)
                ));

                return stage_time;
            }

            private static float WaveCrossTurret(float stage_time, CStageDefinition stage)
            {
                stage_time += 2.0f;
                stage.AddElement(Time.ToFrames(stage_time), CStagePresets.MakeWave(
                    typeof(CTurret), 3,
                    CStagePresets.PositionTopLeftCenter(),
                    CStagePresets.TimerInterval(0.2f),
                    CStagePresets.MoveDownLerpRight(5.0f)
                ));

                stage_time += 0.0f;
                stage.AddElement(Time.ToFrames(stage_time), CStagePresets.MakeWave(
                    typeof(CTurret), 3,
                    CStagePresets.PositionTopRightCenter(),
                    CStagePresets.TimerInterval(0.2f),
                    CStagePresets.MoveDownLerpLeft(5.0f)
                ));

                return stage_time;
            }

            private static float WaveSideTurret(float stage_time, CStageDefinition stage)
            {
                stage_time += 0.0f;
                stage.AddElement(Time.ToFrames(stage_time), CStagePresets.MakeWave(
                    typeof(CTurret), 3,
                    CStagePresets.PositionHighLeft(),
                    CStagePresets.TimerInterval(0.2f),
                    CStagePresets.MoveRight(5.0f)
                ));

                stage_time += 0.0f;
                stage.AddElement(Time.ToFrames(stage_time), CStagePresets.MakeWave(
                    typeof(CTurret), 3,
                    CStagePresets.PositionHighRight(),
                    CStagePresets.TimerInterval(0.2f),
                    CStagePresets.MoveLeft(5.0f)
                ));

                return stage_time;
            }

            private static float WaveLoopTurrets(float stage_time, CStageDefinition stage)
            {
                stage_time += 0.0f;
                stage.AddElement(Time.ToFrames(stage_time), CStagePresets.MakeWave(
                    typeof(CTurret), 3,
                    CStagePresets.PositionTopLeftCenter(),
                    CStagePresets.TimerInterval(0.2f),
                    CStagePresets.MoveDownLoopRightUp(5.0f)
                ));

                return stage_time;
            }

            private static float WaveTurretBriefVertical(float stage_time, CStageDefinition stage)
            {
                stage_time += 0.0f;
                stage.AddElement(Time.ToFrames(stage_time), CStagePresets.MakeWave(
                    typeof(CTurret), 1,
                    CStagePresets.PositionTopCenter(),
                    CStagePresets.TimerInterval(0.0f),
                    CStagePresets.MoveDownUp(5.0f)
                ));
                stage.AddElement(Time.ToFrames(stage_time), CStagePresets.MakeWave(
                    typeof(CTurret), 1,
                    CStagePresets.PositionTopLeftCenter(),
                    CStagePresets.TimerInterval(0.0f),
                    CStagePresets.MoveDownUp(5.0f)
                ));
                stage.AddElement(Time.ToFrames(stage_time), CStagePresets.MakeWave(
                    typeof(CTurret), 1,
                    CStagePresets.PositionTopRightCenter(),
                    CStagePresets.TimerInterval(0.0f),
                    CStagePresets.MoveDownUp(5.0f)
                ));

                return stage_time;
            }

            public static CStageDefinition GenerateDefinition()
            {
                CStageDefinition stage = new CStageDefinition("Stage1");

                float scroll_speed = 1.0f;

                float stage_time = 0.0f;

                WaveTurretBriefVertical(stage_time, stage);
                WaveBigAsteroids(stage_time, stage);            

                stage_time += 2.0f;
                stage.AddElement(Time.ToFrames(stage_time), CStagePresets.MakeWave(
                    typeof(CTurret), 3,
                    CStagePresets.PositionTopLeftCenter(),
                    CStagePresets.TimerInterval(0.2f),
                    CStagePresets.MoveDownLoopRightUp(5.0f)
                ));

                stage_time += 1.0f;
                stage.AddElement(Time.ToFrames(stage_time), CStagePresets.MakeWave(
                    typeof(CTurret), 3,
                    CStagePresets.PositionTopRightCenter(),
                    CStagePresets.TimerInterval(0.2f),
                    CStagePresets.MoveDownLoopLeftUp(5.0f)
                ));

                stage.AddElement(Time.ToFrames(stage_time), CStagePresets.MakeWave(
                    typeof(CSpacePlatform), 1,
                    CStagePresets.PositionTopRightCenter(),
                    CStagePresets.TimerInterval(0.0f),
                    CStagePresets.MoveDown(scroll_speed)
                ));

                stage.AddElement(Time.ToFrames(stage_time), CStagePresets.MakeWave(
                    typeof(CBuilding), 1,
                    CStagePresets.PositionTopRightCenter(),
                    CStagePresets.TimerInterval(0.0f),
                    CStagePresets.MoveDown(scroll_speed)
                ));

                stage_time += 2.0f;
                stage.AddElement(Time.ToFrames(stage_time), CStagePresets.MakeWave(
                    typeof(CTurret), 3,
                    CStagePresets.PositionTopLeftCenter(),
                    CStagePresets.TimerInterval(0.2f),
                    CStagePresets.MoveDownLoopRightUp(5.0f)
                ));

                stage_time += 1.0f;
                stage.AddElement(Time.ToFrames(stage_time), CStagePresets.MakeWave(
                    typeof(CTurret), 3,
                    CStagePresets.PositionTopRightCenter(),
                    CStagePresets.TimerInterval(0.2f),
                    CStagePresets.MoveDownLoopLeftUp(5.0f)
                ));

                stage_time += 3.0f;
                stage.AddElement(Time.ToFrames(stage_time), CStagePresets.MakeWave(
                    typeof(CBigSpacePlatform), 1,
                    CStagePresets.PositionTopLeftCenter(),
                    CStagePresets.TimerInterval(0.0f),
                    CStagePresets.MoveDown(scroll_speed)
                ));

                stage.AddElement(Time.ToFrames(stage_time), CStagePresets.MakeWave(
                    typeof(CBuilding), 1,
                    CStagePresets.PositionTopLeftCenter(),
                    CStagePresets.TimerInterval(0.0f),
                    CStagePresets.MoveDown(scroll_speed)
                ));

                WaveTurretBriefVertical(stage_time, stage);

                stage_time += 3.0f;
                stage.AddElement(Time.ToFrames(stage_time), CStagePresets.MakeWave(
                    typeof(CTurret), 3,
                    CStagePresets.PositionTopRightCenter(),
                    CStagePresets.TimerInterval(0.2f),
                    CStagePresets.MoveDownLerpLeft(5.0f)
                ));

                stage_time += 0.0f;
                stage.AddElement(Time.ToFrames(stage_time), CStagePresets.MakeWave(
                    typeof(CTurret), 3,
                    CStagePresets.PositionTopLeftCenter(),
                    CStagePresets.TimerInterval(0.2f),
                    CStagePresets.MoveDownLerpRight(5.0f)
                ));

                WaveTurretBriefVertical(stage_time, stage);

                stage_time += 30.0f;
                stage.AddElement(Time.ToFrames(stage_time), new CStageFinish());

                return stage;
            }
        };
    }
}