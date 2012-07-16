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
            0,
            1500,
            2500,
        };

        public static int[] StartingWeaponLevel = new int[3]
        {
            2,
            2,
            2,
        };

        public static float[] HealthScale = new float[3]
        {
            1.0f,
            1.25f,
            1.55f,
        };

        public static float[] BossHealthScale = new float[3]
        {
            1.0f,
            1.5f,
            2.0f,
        };

        // TODO: damage scale
        // TODO: fire delay scale

        public static float[] MoneyScale = new float[3]
        {
            1.0f,
            1.10f,
            1.25f,
        };

        public static float[] ReloadSpeedScale = new float[3]
        {
            1.1f,
            0.9f,
            0.8f,
        };
    }
}
