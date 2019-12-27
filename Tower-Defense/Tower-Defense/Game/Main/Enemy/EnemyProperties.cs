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
    }
}
