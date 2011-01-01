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
            Normal,
            Hard,
            Extreme,
        }

        public static int[] StartingMoney = new int[3]
        {
            5000,
            7500,
            10000,
        };

        public static float[] HealthScale = new float[3]
        {
            1.0f,
            1.35f,
            1.85f,
        };

        public static float[] MoneyScale = new float[3]
        {
            1.0f,
            1.25f,
            1.75f,
        };
    }
}
