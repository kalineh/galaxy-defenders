//
// MoverPresets.cs
//

#if !XBOX360
using System.Diagnostics;
#else
using Galaxy.Diagnostics;
#endif

using System.Collections.Generic;
using Microsoft.Xna.Framework;
                       
namespace Galaxy
{
    public class CMoverPresets
    {
        public static CMover MoveDownToDownRight(float speed)
        {
            return new CMoverSequence()
            {
                Name = (new StackFrame(0, false)).GetMethod().Name,
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
                Name = (new StackFrame(0, false)).GetMethod().Name,
                Velocity = new List<Vector2>()
                {
                    new Vector2(0.0f, speed),
                    new Vector2(-speed, speed),
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
                Name = (new StackFrame(0, false)).GetMethod().Name,
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

        public static CMover MoveDownStopDown(float speed)
        {
            return new CMoverSequence()
            {
                Name = (new StackFrame(0, false)).GetMethod().Name,
                Velocity = new List<Vector2>()
                {
                    new Vector2(0.0f, speed),
                    new Vector2(0.0f, -speed * 2.0f),
                    new Vector2(0.0f, speed),
                },
                Duration = new List<float>()
                {
                    0.2f * speed,
                    1.3f * speed,
                    0.0f,
                },

                VelocityLerpRate = 0.04f,
                AlwaysMaxSpeed = false,
            };
        }

        public static CMover MoveUp(float speed)
        {
            return new CMoverFixedVelocity()
            {
                Name = (new StackFrame(0, false)).GetMethod().Name,
                Velocity = new Vector2(0.0f, -speed),
            };
        }

        public static CMover MoveDown(float speed)
        {
            return new CMoverFixedVelocity()
            {
                Name = (new StackFrame(0, false)).GetMethod().Name,
                Velocity = new Vector2(0.0f, speed),
            };
        }

        public static CMover MoveLeft(float speed)
        {
            return new CMoverFixedVelocity()
            {
                Name = (new StackFrame(0, false)).GetMethod().Name,
                Velocity = new Vector2(-speed, 0.0f),
            };
        }

        public static CMover MoveRight(float speed)
        {
            return new CMoverFixedVelocity()
            {
                Name = (new StackFrame(0, false)).GetMethod().Name,
                Velocity = new Vector2(speed, 0.0f),
            };
        }

        public static CMover MoveDownLeft(float speed)
        {
            return new CMoverFixedVelocity()
            {
                Name = (new StackFrame(0, false)).GetMethod().Name,
                Velocity = new Vector2(-speed, speed),
            };
        }

        public static CMover MoveDownRight(float speed)
        {
            return new CMoverFixedVelocity()
            {
                Name = (new StackFrame(0, false)).GetMethod().Name,
                Velocity = new Vector2(speed, speed),
            };
        }

        public static CMover MoveDownLerpLeft(float speed)
        {
            return new CMoverSequence()
            {
                Name = (new StackFrame(0, false)).GetMethod().Name,
                Velocity = new List<Vector2>()
                {
                    new Vector2(0.0f, speed),
                    new Vector2(-speed, 0.0f),
                },
                Duration = new List<float>()
                {
                    0.1f * speed,
                    0.0f * speed,
                },

                VelocityLerpRate = 0.13f,
            };
        }

        public static CMover MoveDownLerpRight(float speed)
        {
            return new CMoverSequence()
            {
                Name = (new StackFrame(0, false)).GetMethod().Name,
                Velocity = new List<Vector2>()
                {
                    new Vector2(0.0f, speed),
                    new Vector2(speed, 0.0f),
                },
                Duration = new List<float>()
                {
                    0.1f * speed,
                    0.0f * speed,
                },

                VelocityLerpRate = 0.13f,
            };
        }

        public static CMover MoveDownDownLeft(float speed)
        {
            return new CMoverFixedVelocity()
            {
                Name = (new StackFrame(0, false)).GetMethod().Name,
                Velocity = new Vector2(speed * -0.5f, speed),
            };
        }

        public static CMover MoveDownDownRight(float speed)
        {
            return new CMoverFixedVelocity()
            {
                Name = (new StackFrame(0, false)).GetMethod().Name,
                Velocity = new Vector2(speed * 0.5f, speed),
            };
        }

        public static CMover MoveDownLoopRightUp(float speed)
        {
            return new CMoverSequence()
            {
                Name = (new StackFrame(0, false)).GetMethod().Name,
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
                Name = (new StackFrame(0, false)).GetMethod().Name,
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
    }
}