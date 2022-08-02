namespace Tower_Defense
{
    static class TowerProperties
    {
        public static class Tower_00
        {
            //Tower general info
            public static string Name => "Laser";

            //Tower stats
            public static float AttackRate => 3.5f;
            public static float Range => 256;
            public static int Power => 1;
            public static int NumberOfTargets => 1;

            //Tower prices
            public static int Price => 200;
            public static int AttackRate_Price => 500;
            public static int Range_Price => 150;
            public static int Power_Price => 700;
            public static int NumberOfTargets_Price => 1500;

            //Tower Max Level
            public static int AttackRate_Level_Max => 25;
            public static int Range_Level_Max => 20;
            public static int Power_Level_Max => 5;
            public static int NumberOfTargets_Level_Max => 5;
        }

        public static class Tower_01
        {
            //Tower general info
            public static string Name => "Fusion";

            //Tower stats
            public static float AttackRate => 3.0f;
            public static float Range => 128;
            public static int Power => 2;
            public static int NumberOfTargets => 2;

            //Tower prices
            public static int Price => 325;
            public static int AttackRate_Price => 450;
            public static int Range_Price => 300;
            public static int Power_Price => 1000;
            public static int NumberOfTargets_Price => 1800;

            //Tower Max Level
            public static int AttackRate_Level_Max => 15;
            public static int Range_Level_Max => 10;
            public static int Power_Level_Max => 15;
            public static int NumberOfTargets_Level_Max => 10;
        }

        public static class Tower_02
        {
            //Tower general info
            public static string Name => "Electric";

            //Tower stats
            public static float AttackRate => 3.0f;
            public static float Range => 156;
            public static int Power => 2;
            public static int NumberOfTargets => 2;

            //Tower prices
            public static int Price => 450;
            public static int AttackRate_Price => 400;
            public static int Range_Price => 600;
            public static int Power_Price => 1200;
            public static int NumberOfTargets_Price => 600;

            //Tower Max Level
            public static int AttackRate_Level_Max => 20;
            public static int Range_Level_Max => 15;
            public static int Power_Level_Max => 3;
            public static int NumberOfTargets_Level_Max => 20;
        }

        public static class Tower_Upgrade
        {
            //Tower Upgrade values
            public static float AttackRate_Upgrade => 0.95f;
            public static float Range_Upgrade => 8.0f;
            public static int Power_Upgrade => 1;
            public static int NumberOfTargets_Upgrade => 1;
        }
    }
}
