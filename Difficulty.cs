//
// Difficulty.cs
//

using System;

namespace Galaxy
{
    public class CDifficulty
    {
        public enum DifficultyLevel
        {
            Easy = 0,
            Normal = 1,
            Hard = 2,
            LOL = 3,
        }

        public static float[] HealthScale = new float[4]
        {
            0.7f,
            1.0f,
            1.2f,
            2.0f,
        };

        public static float[] MoneyScale = new float[4]
        {
            1.2f,
            1.0f,
            1.1f,
            1.5f,
        };
    }
}
