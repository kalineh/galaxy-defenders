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
using System.Reflection;
                       
namespace Galaxy
{
    public class CMoverPresets
    {
        public static CMover FromName(string name, float speed_multiplier)
        {
            MethodInfo method = typeof(CMoverPresets).GetMethod(name);
            CMover result = method.Invoke(null, new object[] { speed_multiplier }) as CMover;
            return result;
        }

        public static CMover IgnoreCamera(float unused)
        {
            return new CMoverIgnoreCamera();
        }

        public static CMover Sin(float speed)
        {
            return new CMoverSin()
            {
                Name = (new StackFrame(0, false)).GetMethod().Name,
                Frequency = 0.05f,
                Amplitude = 4.0f,
                Down = speed,
            };
        }

        // down
        
        // right

        // left

        // intoleft

        // into right

        // 

        public static CMover DownUp(float speed)
        {
            return new CMoverSequence()
            {
                Name = (new StackFrame(0, false)).GetMethod().Name,
                Velocity = new List<Vector2>()
                {
                    new Vector2(0.0f, 2.0f) * speed,
                    new Vector2(0.0f, -1.0f) * speed,
                },
                Duration = new List<float>()
                {
                    0.3f * speed,
                    0.0f,
                },

                VelocityLerpRate = 0.05f,
                AlwaysMaxSpeed = false,
            };
        }

        public static CMover DownStopUp(float speed)
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
                    1.5f * speed,
                    0.0f,
                },

                VelocityLerpRate = 0.03f,
                AlwaysMaxSpeed = false,
            };
        }

        public static CMover DownStopDown(float speed)
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

        public static CMover Up(float speed)
        {
            return new CMoverFixedVelocity()
            {
                Name = (new StackFrame(0, false)).GetMethod().Name,
                Velocity = new Vector2(0.0f, -speed),
            };
        }

        public static CMover Down(float speed)
        {
            return new CMoverFixedVelocity()
            {
                Name = (new StackFrame(0, false)).GetMethod().Name,
                Velocity = new Vector2(0.0f, speed),
            };
        }

        public static CMover Left(float speed)
        {
            return new CMoverFixedVelocity()
            {
                Name = (new StackFrame(0, false)).GetMethod().Name,
                Velocity = new Vector2(-speed, 0.0f),
            };
        }

        public static CMover Right(float speed)
        {
            return new CMoverFixedVelocity()
            {
                Name = (new StackFrame(0, false)).GetMethod().Name,
                Velocity = new Vector2(speed, 0.0f),
            };
        }

        public static CMover DownLeft(float speed)
        {
            return new CMoverFixedVelocity()
            {
                Name = (new StackFrame(0, false)).GetMethod().Name,
                Velocity = new Vector2(-speed, speed),
            };
        }

        public static CMover DownRight(float speed)
        {
            return new CMoverFixedVelocity()
            {
                Name = (new StackFrame(0, false)).GetMethod().Name,
                Velocity = new Vector2(speed, speed),
            };
        }

        public static CMover DownLerpLeft(float speed)
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

        public static CMover DownLerpRight(float speed)
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

        public static CMover DownDownLeft(float speed)
        {
            return new CMoverFixedVelocity()
            {
                Name = (new StackFrame(0, false)).GetMethod().Name,
                Velocity = new Vector2(speed * -0.5f, speed),
            };
        }

        public static CMover DownDownRight(float speed)
        {
            return new CMoverFixedVelocity()
            {
                Name = (new StackFrame(0, false)).GetMethod().Name,
                Velocity = new Vector2(speed * 0.5f, speed),
            };
        }

        public static CMover DownLoopRightUp(float speed)
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

        public static CMover DownLoopLeftUp(float speed)
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