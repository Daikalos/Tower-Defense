using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense
{
    //Acts as starting values for each enemy type

    static class EnemyProperties
    {
        public static class Enemy_00
        {
            //Start values for each enemy
            public static float Speed => 0.9f;
            public static int HealthPoints => 2;
        }

        public static class Enemy_01
        {
            //Start values for each enemy
            public static float Speed => 0.5f;
            public static int HealthPoints => 4;
        }

        public static class Enemy_02
        {
            //Start values for each enemy
            public static float Speed => 0.4f;
            public static int HealthPoints => 5;
        }

        public static class Enemy_03
        {
            //Start values for each enemy
            public static float Speed => 1.5f;
            public static int HealthPoints => 1;
        }

        public static class Enemy_Info
        {
            //General info about enemies
            public static float HealthPoint_Increase => 0.2f;
            public static float AmountToSpawn_Increase => 1.20f;
            public static float SpawnDelay_Decrease => 0.95f;
            public static float SpawnDelay_Min => 0.05f;

            public static int Enemy_Value => 10;
            public static int EnemyTypes => 4;
        }
    }
}
