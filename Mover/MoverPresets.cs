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
        public static CMover FromName(string name, float speed_multiplier, float transition_multiplier)
        {
            // TODO: wont work on 360?
            MethodInfo method = typeof(CMoverPresets).GetMethod(name);
            System.Diagnostics.Debug.Assert(method != null);
            CMover result = method.Invoke(null, new object[] { speed_multiplier, transition_multiplier }) as CMover;
            return result;
        }

        public static CMover IgnoreCamera(float unused)
        {
            return new CMoverIgnoreCamera();
        }

        public static CMover Sin(float speed, float transition)
        {
            return new CMoverSin()
            {
                Name = (new StackFrame(0, false)).GetMethod().Name,
                Frequency = Time.SingleFrame * transition,
                Amplitude = speed * 2.0f,
                Down = speed,
            };
        }

        // down
        public static CMover Up(float speed, float transition)
        {
            return new CMoverFixedVelocity()
            {
                Name = (new StackFrame(0, false)).GetMethod().Name,
                Velocity = new Vector2(0.0f, -speed),
            };
        }

        public static CMover Down(float speed, float transition)
        {
            return new CMoverFixedVelocity()
            {
                Name = (new StackFrame(0, false)).GetMethod().Name,
                Velocity = new Vector2(0.0f, speed),
            };
        }

        public static CMover Right(float speed, float transition)
        {
            return new CMoverFixedVelocity()
            {
                Name = (new StackFrame(0, false)).GetMethod().Name,
                Velocity = new Vector2(speed, 0.0f),
            };
        }

        public static CMover Left(float speed, float transition)
        {
            return new CMoverFixedVelocity()
            {
                Name = (new StackFrame(0, false)).GetMethod().Name,
                Velocity = new Vector2(-speed, 0.0f),
            };
        }

        public static CMover ToUp(float speed, float transition)
        {
            return new CMoverSequence()
            {
                Name = (new StackFrame(0, false)).GetMethod().Name,
                Velocity = new List<Vector2>()
                {
                    new Vector2(0.0f, 1.0f) * speed,
                    new Vector2(0.0f, -6.0f) * speed,
                },
                Duration = new List<float>()
                {
                    1.0f * transition,
                    0.0f,
                },

                VelocityLerpRate = 0.02f,
                AlwaysMaxSpeed = false,
            };
        }

        public static CMover DownWaitUp(float speed, float transition)
        {
            return new CMoverSequence()
            {
                Name = (new StackFrame(0, false)).GetMethod().Name,
                Velocity = new List<Vector2>()
                {
                    new Vector2(0.0f, 1.0f) * speed,
                    new Vector2(0.0f, -3.0f) * speed,
                    new Vector2(0.0f, -6.0f) * speed,
                },
                Duration = new List<float>()
                {
                    1.0f * transition,
                    0.5f * transition,
                    0.0f,
                },

                VelocityLerpRate = 0.02f,
                AlwaysMaxSpeed = false,
            };
        }

        public static CMover DownWaitDown(float speed, float transition)
        {
            return new CMoverSequence()
            {
                Name = (new StackFrame(0, false)).GetMethod().Name,
                Velocity = new List<Vector2>()
                {
                    new Vector2(0.0f, 1.0f) * speed,
                    new Vector2(0.0f, -3.0f) * speed,
                    new Vector2(0.0f, 1.0f) * speed,
                },
                Duration = new List<float>()
                {
                    1.0f * transition,
                    0.5f * transition,
                    0.0f,
                },

                VelocityLerpRate = 0.02f,
                AlwaysMaxSpeed = false,
            };
        }

        public static CMover DownLeft(float speed, float transition)
        {
            return new CMoverSequence()
            {
                Name = (new StackFrame(0, false)).GetMethod().Name,
                Velocity = new List<Vector2>()
                {
                    new Vector2(0.0f, 1.0f) * speed,
                    new Vector2(-3.0f, -3.0f) * speed,
                },
                Duration = new List<float>()
                {
                    0.5f * transition,
                    0.0f * transition,
                },

                VelocityLerpRate = 0.02f,
            };
        }

        public static CMover DownRight(float speed, float transition)
        {
            return new CMoverSequence()
            {
                Name = (new StackFrame(0, false)).GetMethod().Name,
                Velocity = new List<Vector2>()
                {
                    new Vector2(0.0f, 1.0f) * speed,
                    new Vector2(3.0f, -3.0f) * speed,
                },
                Duration = new List<float>()
                {
                    0.5f * transition,
                    0.0f * transition,
                },

                VelocityLerpRate = 0.02f,
            };
        }
    }
}