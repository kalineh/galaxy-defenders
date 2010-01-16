//
// Time.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    class Time
    {
        public static float SingleFrame { get; private set; }

        static Time()
        {
            SingleFrame = ToSeconds(1);
        }

        public static float ToSeconds(int frames)
        {
            return (float)(frames / 60.0f);
        }

        public static int ToFrames(float seconds)
        {
            return (int)(seconds * 60.0f);
        }
    }
}
