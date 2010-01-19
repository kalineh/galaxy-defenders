//
// StagePresetMovers.cs
//

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Galaxy
{
    namespace Stages
    {
        public class CStagePresets
        {
            public static CSpawnerEntity MakeWave(Type type, int count, CSpawnPosition position, CSpawnTimer timer, CMover mover)
            {
                return new CSpawnerEntity()
                {
                    Type = type,
                    SpawnCount = count,
                    SpawnPosition = position,
                    SpawnTimer = timer,
                    CustomElement = new CSpawnerCustomMover() {
                        Mover = mover,
                    },
                };
            }

            public static CSpawnPositionFixed PositionTopCenter()
            {
                return new CSpawnPositionFixed()
                {
                    Position = new Vector2(400.0f, -128.0f),
                };
            }

            public static CSpawnPositionFixed PositionTopLeft()
            {
                return new CSpawnPositionFixed()
                {
                    Position = new Vector2(0.0f, -128.0f),
                };
            }

            public static CSpawnPositionFixed PositionTopRight()
            {
                return new CSpawnPositionFixed()
                {
                    Position = new Vector2(800.0f, -128.0f),
                };
            }
            
            public static CSpawnPositionFixed PositionTopLeftCenter()
            {
                return new CSpawnPositionFixed()
                {
                    Position = new Vector2(200.0f, -128.0f),
                };
            }
            
            public static CSpawnPositionFixed PositionTopRightCenter()
            {
                return new CSpawnPositionFixed()
                {
                    Position = new Vector2(600.0f, -128.0f),
                };
            }

            public static CSpawnPositionFixed PositionHighLeft()
            {
                return new CSpawnPositionFixed()
                {
                    Position = new Vector2(-128.0f, 200.0f),
                };
            }

            public static CSpawnPositionFixed PositionHighRight()
            {
                return new CSpawnPositionFixed()
                {
                    Position = new Vector2(928.0f, 200.0f),
                };
            }

            public static CMover MoveDownToDownRight(float speed)
            {
                return new CMoverSequence()
                {
                    Velocity = new List<Vector2>()
                    {
                        new Vector2(0.0f, speed),
                        new Vector2(speed, speed),
                    },
                    Duration = new List<float>()
                    {
                        5.0f,
                        0.0f,
                    },
                };
            }

            public static CMover MoveDownToDownLeft(float speed)
            {
                return new CMoverSequence()
                {
                    Velocity = new List<Vector2>()
                    {
                        new Vector2(0.0f, speed),
                        new Vector2(speed, speed),
                    },
                    Duration = new List<float>()
                    {
                        5.0f,
                        0.0f,
                    },

                    VelocityLerpRate = 0.1f,
                };
            }

            public static CMover MoveDown(float speed)
            {
                return new CMoverFixedVelocity()
                {
                    Velocity = new Vector2(0.0f, speed),
                };
            }

            public static CMover MoveLeft(float speed)
            {
                return new CMoverFixedVelocity()
                {
                    Velocity = new Vector2(-speed, 0.0f),
                };
            }

            public static CMover MoveRight(float speed)
            {
                return new CMoverFixedVelocity()
                {
                    Velocity = new Vector2(speed, 0.0f),
                };
            }

            public static CMover MoveDownLeft(float speed)
            {
                return new CMoverFixedVelocity()
                {
                    Velocity = new Vector2(-speed, speed),
                };
            }

            public static CMover MoveDownRight(float speed)
            {
                return new CMoverFixedVelocity()
                {
                    Velocity = new Vector2(speed, speed),
                };
            }

            public static CMover MoveDownLerpLeft(float speed)
            {
                return new CMoverSequence()
                {
                    Velocity = new List<Vector2>()
                    {
                        new Vector2(0.0f, speed),
                        new Vector2(-speed, 0.0f),
                    },
                    Duration = new List<float>()
                    {
                        1.5f,
                        0.0f,
                    },

                    VelocityLerpRate = 0.03f,
                };
            }

            public static CMover MoveDownLerpRight(float speed)
            {
                return new CMoverSequence()
                {
                    Velocity = new List<Vector2>()
                    {
                        new Vector2(0.0f, speed),
                        new Vector2(speed, 0.0f),
                    },
                    Duration = new List<float>()
                    {
                        1.5f,
                        0.0f,
                    },

                    VelocityLerpRate = 0.03f,
                };
            }

            public static CMover MoveDownDownLeft(float speed)
            {
                return new CMoverFixedVelocity()
                {
                    Velocity = new Vector2(speed * -0.5f, speed),
                };
            }

            public static CMover MoveDownDownRight(float speed)
            {
                return new CMoverFixedVelocity()
                {
                    Velocity = new Vector2(speed * 0.5f, speed),
                };
            }

            public static CSpawnTimerInterval TimerDelay(float delay)
            {
                return new CSpawnTimerInterval()
                {
                    Delay = delay,
                };
            }
        }
    }
}