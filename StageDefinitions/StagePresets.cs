//
// StagePresetMovers.cs
//

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Galaxy
{
    public class CStagePresets
    {
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
                    1.0f * speed,
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
                    1.0f * speed,
                    0.0f,
                },

                VelocityLerpRate = 0.1f,
            };
        }

        public static CMover MoveDownUp(float speed)
        {
            return new CMoverSequence()
            {
                Velocity = new List<Vector2>()
                {
                    new Vector2(0.0f, speed),
                    new Vector2(0.0f, -speed),
                },
                Duration = new List<float>()
                {
                    0.2f * speed,
                    0.0f,
                },

                VelocityLerpRate = 0.02f,
                AlwaysMaxSpeed = false,
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
                    0.3f * speed,
                    0.0f * speed,
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
                    0.3f * speed,
                    0.0f * speed,
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

        public static CMover MoveDownLoopRightUp(float speed)
        {
            return new CMoverSequence()
            {
                Velocity = new List<Vector2>()
                {
                    new Vector2(0.0f, speed),
                    new Vector2(speed, 0.0f),
                    new Vector2(0.0f, -speed),
                    new Vector2(-speed, 0.0f),
                    new Vector2(0.0f, speed),
                },
                Duration = new List<float>()
                {
                    0.2f * speed,
                    0.2f * speed,
                    0.1f * speed,
                    0.2f * speed,
                    0.2f * speed,
                },

                VelocityLerpRate = 0.03f,
            };
        }

        public static CMover MoveDownLoopLeftUp(float speed)
        {
            return new CMoverSequence()
            {
                Velocity = new List<Vector2>()
                {
                    new Vector2(0.0f, speed),
                    new Vector2(-speed, 0.0f),
                    new Vector2(0.0f, -speed),
                    new Vector2(speed, 0.0f),
                    new Vector2(0.0f, speed),
                },
                Duration = new List<float>()
                {
                    0.2f * speed,
                    0.2f * speed,
                    0.1f * speed,
                    0.2f * speed,
                    0.2f * speed,
                },

                VelocityLerpRate = 0.03f,
            };
        }

        public static CSpawnTimerInterval TimerInterval(float delay)
        {
            return new CSpawnTimerInterval()
            {
                Interval = delay,
            };
        }
    }
}