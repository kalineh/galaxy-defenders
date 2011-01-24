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
            2500,
            5000,
            7500,
        };

        public static float[] HealthScale = new float[3]
        {
            1.0f,
            1.35f,
            1.85f,
        };

        // TODO: damage scale
        // TODO: fire delay scale

        public static float[] MoneyScale = new float[3]
        {
            1.0f,
            1.10f,
            1.25f,
        };
    }
}
