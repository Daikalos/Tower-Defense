using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense
{
    //Used to draw values in shop manager and acts as starting values for each tower type

    static class TowerProperties
    {
        public static class Tower_00
        {
            //Tower general info
            public static string Name => "Laser";

            //Tower stats
            public static float FireSpeed => 4.0f;
            public static float Range => 256;
            public static int Damage => 1;
            public static int NumberOfTargets => 1;

            //Tower prices
            public static int Price => 200;
            public static int FireSpeed_Price => 500;
            public static int Range_Price => 150;
            public static int Damage_Price => 600;
            public static int NumberOfTargets_Price => 1200;
        }

        public static class Tower_01
        {
            //Tower general info
            public static string Name => "Fusion";

            //Tower stats
            public static float FireSpeed => 3.0f;
            public static float Range => 128;
            public static int Damage => 2;
            public static int NumberOfTargets => 2;

            //Tower prices
            public static int Price => 325;
            public static int FireSpeed_Price => 450;
            public static int Range_Price => 200;
            public static int Damage_Price => 1400;
            public static int NumberOfTargets_Price => 1800;
        }

        public static class Tower_02
        {
            //Tower general info
            public static string Name => ":)";

            //Tower stats
            public static float FireSpeed => 4.0f;
            public static float Range => 256;
            public static int Damage => 1;
            public static int NumberOfTargets => 1;

            //Tower prices
            public static int Price => 450;
            public static int FireSpeed_Price => 150;
            public static int Range_Price => 150;
            public static int Damage_Price => 150;
            public static int NumberOfTargets_Price => 150;
        }

        public static class Tower_Upgrade
        {
            //Tower Upgrade values
            public static float FireSpeed_Upgrade => 0.95f;
            public static float Range_Upgrade => 8.0f;
            public static int Damage_Upgrade => 1;
            public static int NumberOfTargets_Upgrade => 1;
        }
    }
}
