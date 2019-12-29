namespace Tower_Defense
{
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
            public static int Damage_Price => 700;
            public static int NumberOfTargets_Price => 1500;

            //Tower Max Level
            public static int FireSpeed_Level_Max => 25;
            public static int Range_Level_Max => 20;
            public static int Damage_Level_Max => 10;
            public static int NumberOfTargets_Level_Max => 5;
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
            public static int Range_Price => 300;
            public static int Damage_Price => 1000;
            public static int NumberOfTargets_Price => 1800;

            //Tower Max Level
            public static int FireSpeed_Level_Max => 15;
            public static int Range_Level_Max => 10;
            public static int Damage_Level_Max => 15;
            public static int NumberOfTargets_Level_Max => 10;
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

            //Tower Max Level
            public static int FireSpeed_Level_Max => 25;
            public static int Range_Level_Max => 25;
            public static int Damage_Level_Max => 25;
            public static int NumberOfTargets_Level_Max => 25;
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
