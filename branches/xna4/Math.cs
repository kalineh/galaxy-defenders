//
// Math.cs
// 

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public static class Vector2Extensions
    {
        public static Vector2 Half(this Vector2 self)
        {
            return new Vector2(0.5f, 0.5f);     
        }
        
        public static Vector2 Rotate(this Vector2 self, float rotation)
        {
            Quaternion transform = Quaternion.CreateFromAxisAngle(Vector3.UnitZ, rotation);
            Vector2 result = Vector2.Transform(self, transform);

            // TODO: fix this!
            //float c = (float)Math.Cos(rotation);
            //float s = (float)Math.Sin(rotation);
            //self.X *= -c;
            //self.Y *= s;

            return result;
        }

        // TODO: better impl
        public static float ToAngle(this Vector2 vector)
        {
            return (float)Math.Atan2(vector.Y, vector.X);
        }

        public static Vector2 Perp(this Vector2 vector)
        {
            return new Vector2(-vector.Y, vector.X);
        }

        public static Vector2 Normal(this Vector2 vector)
        {
            Vector2 result = new Vector2(vector.X, vector.Y);
            result.Normalize();
            return result;
        }

        public static bool IsEffectivelyZero(this Vector2 vector)
        {
            return vector.LengthSquared() < 0.00001f;
        }

        public static void Set(this Vector2 self, float x, float y)
        {
            self.X = x;
            self.Y = y;
        }

        public static void Set(this Vector2 self, Vector2 value)
        {
            self.X = value.X;
            self.Y = value.Y;
        }

        public static Vector3 xy0(this Vector2 vector) { return new Vector3(vector.X, vector.Y, 0.0f); }
        public static Vector3 xy1(this Vector2 vector) { return new Vector3(vector.X, vector.Y, 1.0f); }
        public static Vector3 xyz(this Vector2 vector, float z) { return new Vector3(vector.X, vector.Y, z); }

        public static Vector2 ToVector2(this Vector3 vector)
        {
            return new Vector2(vector.X, vector.Y);
        }
        
        public static Vector3 ToVector3(this Vector2 vector)
        {
            return new Vector3(vector.X, vector.Y, 0.0f);
        }
        
        public static Vector3 ToVector3(this Vector2 vector, float z)
        {
            return new Vector3(vector.X, vector.Y, z);
        }
    }

    public static class EnhanceRandom
    {
        public static bool NextBool(this Random self)
        {
            return (self.Next() & 1) == 0;
        }

        public static float NextFloat(this Random self)
        {
            return (float)self.NextDouble();
        }

        public static float NextSignedFloat(this Random self)
        {
            return (float)self.NextDouble() * (float)self.NextSign();
        }

        public static float NextAngle(this Random self)
        {
            return self.NextFloat() * MathHelper.TwoPi;
        }

        public static float NextSign(this Random self)
        {
            return self.NextDouble() < 0.5 ? -1.0f : 1.0f;
        }

        public static Vector2 NextVector2(this Random self)
        {
            return Vector2.UnitX.Rotate(self.NextFloat() * MathHelper.TwoPi);
        }

        public static Vector2 NextVector2(this Random self, float magnitude)
        {
            return Vector2.UnitX.Rotate(self.NextFloat() * MathHelper.TwoPi) * magnitude;
        }

        public static Vector2 NextVector2Variable(this Random self)
        {
            return new Vector2(self.NextFloat(), self.NextFloat());
        }
    }

    public static class Units
    {
        public static float PercentToRatio(float percent)
        {
            return percent / 100.0f;
        }

        public static float PercentToRatio(int percent)
        {
            return (float)percent / 100.0f;
        }
    }

    public static class Interpolation
    {
        public static float MoveToAngle(float from, float to, float amount)
        {
            float offset = MathHelper.WrapAngle(to - from);
            float move = MathHelper.Clamp(offset, -amount, amount);
            return MathHelper.WrapAngle(from + move);
        }

        public static float MoveToValue(float from, float to, float step)
        {
            float offset = to - from;
            float move = step * offset;
            return from + move;
        }
    }

    public static class ColorExtensions
    {
        public static Color Smoothstep(Color a, Color b, float t)
        {
            float x = (t * t) * (3.0f - 2.0f * t);

            Color result = new Color(
                (byte)(a.R + (b.R - a.R) * x),
                (byte)(a.G + (b.G - a.G) * x),
                (byte)(a.B + (b.B - a.B) * x),
                (byte)(a.A + (b.A - a.A) * x)
            );

            return result;
        }
    }
}