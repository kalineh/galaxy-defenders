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
            public static float GravitySpeed = 1.0f;

            private static float WaveBigAsteroids(float stage_time, CStageDefinition stage)
            {
                stage_time += 0.0f;
                stage.AddElement(stage_time, new CSpawnerEntity {
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
                stage.AddElement(stage_time, CStagePresets.MakeWave(
                    typeof(CTurret), 4,
                    CStagePresets.PositionTopLeftCenter(),
                    CStagePresets.TimerDelay(0.2f),
                    CStagePresets.MoveDown(5.0f)
                ));

                stage_time += 2.0f;
                stage.AddElement(stage_time, CStagePresets.MakeWave(
                    typeof(CTurret), 4,
                    CStagePresets.PositionTopRightCenter(),
                    CStagePresets.TimerDelay(0.2f),
                    CStagePresets.MoveDown(5.0f)
                ));

                stage_time += 2.0f;
                stage.AddElement(stage_time, CStagePresets.MakeWave(
                    typeof(CTurret), 4,
                    CStagePresets.PositionTopCenter(),
                    CStagePresets.TimerDelay(0.2f),
                    CStagePresets.MoveDown(5.0f)
                ));

                return stage_time;
            }

            private static float WaveCrossTurret(float stage_time, CStageDefinition stage)
            {
                stage_time += 2.0f;
                stage.AddElement(stage_time, CStagePresets.MakeWave(
                    typeof(CTurret), 3,
                    CStagePresets.PositionTopLeftCenter(),
                    CStagePresets.TimerDelay(0.2f),
                    CStagePresets.MoveDownLerpRight(5.0f)
                ));

                stage_time += 0.0f;
                stage.AddElement(stage_time, CStagePresets.MakeWave(
                    typeof(CTurret), 3,
                    CStagePresets.PositionTopRightCenter(),
                    CStagePresets.TimerDelay(0.2f),
                    CStagePresets.MoveDownLerpLeft(5.0f)
                ));

                return stage_time;
            }

            private static float WaveSideTurret(float stage_time, CStageDefinition stage)
            {
                stage_time += 0.0f;
                stage.AddElement(stage_time, CStagePresets.MakeWave(
                    typeof(CTurret), 3,
                    CStagePresets.PositionHighLeft(),
                    CStagePresets.TimerDelay(0.2f),
                    CStagePresets.MoveRight(5.0f)
                ));

                stage_time += 0.0f;
                stage.AddElement(stage_time, CStagePresets.MakeWave(
                    typeof(CTurret), 3,
                    CStagePresets.PositionHighRight(),
                    CStagePresets.TimerDelay(0.2f),
                    CStagePresets.MoveLeft(5.0f)
                ));

                return stage_time;
            }

            private static float WaveLoopTurrets(float stage_time, CStageDefinition stage)
            {
                stage_time += 0.0f;
                stage.AddElement(stage_time, CStagePresets.MakeWave(
                    typeof(CTurret), 3,
                    CStagePresets.PositionTopLeftCenter(),
                    CStagePresets.TimerDelay(0.2f),
                    CStagePresets.MoveDownLoopRightUp(5.0f)
                ));

                return stage_time;
            }

            public static CStageDefinition GenerateDefinition()
            {
                CStageDefinition stage = new CStageDefinition("Stage1");

                float stage_time = 0.0f;

                stage_time += 2.0f;
                stage.AddElement(stage_time, CStagePresets.MakeWave(
                    typeof(CTurret), 3,
                    CStagePresets.PositionTopLeftCenter(),
                    CStagePresets.TimerDelay(0.2f),
                    CStagePresets.MoveDownLoopRightUp(5.0f)
                ));

                stage_time += 2.0f;
                stage.AddElement(stage_time, CStagePresets.MakeWave(
                    typeof(CTurret), 3,
                    CStagePresets.PositionTopRightCenter(),
                    CStagePresets.TimerDelay(0.2f),
                    CStagePresets.MoveDownLoopLeftUp(5.0f)
                ));

                stage.AddElement(stage_time, CStagePresets.MakeWave(
                    typeof(CSpacePlatform), 1,
                    CStagePresets.PositionTopRightCenter(),
                    CStagePresets.TimerDelay(0.0f),
                    CStagePresets.MoveDown(1.0f)
                ));


                stage.AddElement(stage_time, CStagePresets.MakeWave(
                    typeof(CBuilding), 1,
                    CStagePresets.PositionTopRightCenter(),
                    CStagePresets.TimerDelay(0.0f),
                    CStagePresets.MoveDown(1.0f)
                ));

                stage_time += 30.0f;
                stage.AddElement(stage_time, new CStageFinish());

                return stage;
            }
        };
    }
}