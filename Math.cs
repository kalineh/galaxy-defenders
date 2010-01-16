//
// Math.cs
// 

using System;
using Microsoft.Xna.Framework;

namespace Galaxy
{
    public static class EnhanceVector2
    {
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
    }

    public static class EnhanceRandom
    {
        public static float NextFloat(this Random self)
        {
            return (float)self.NextDouble();
        }

        public static float NextAngle(this Random self)
        {
            return self.NextFloat() * MathHelper.TwoPi;
        }

        public static float RandomSign(this Random self)
        {
            return self.NextDouble() < 0.5 ? -1.0f : 1.0f;
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
}