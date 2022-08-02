using System;

namespace Tower_Defense
{
    static class SpawnInfo
    {
        public static int[] Enemy_Start { get; set; }
        public static float[] Enemy_SpawnRate { get; set; }
        public static int[] Enemy_Amount { get; set; }

        public static int Difficulty { get; set; }

        public static void Initialize()
        {
            string[] tempEnemy_Start = FileReader.FindAllInfo(GameInfo.FolderLevelsInfo + GameInfo.LevelName + "_Info.txt", '=',
                "Enemy_00_Start", "Enemy_01_Start", "Enemy_02_Start", "Enemy_03_Start");
            Enemy_Start = Array.ConvertAll(tempEnemy_Start, x => Int32.Parse(x));

            string[] tempEnemy_SpawnRate = FileReader.FindAllInfo(GameInfo.FolderLevelsInfo + GameInfo.LevelName + "_Info.txt", '=',
                "Enemy_00_SpawnRate", "Enemy_01_SpawnRate", "Enemy_02_SpawnRate", "Enemy_03_SpawnRate");
            Enemy_SpawnRate = Array.ConvertAll(tempEnemy_SpawnRate, x => float.Parse(x));

            string[] tempEnemy_Amount = FileReader.FindAllInfo(GameInfo.FolderLevelsInfo + GameInfo.LevelName + "_Info.txt", '=',
                "Enemy_00_Amount", "Enemy_01_Amount", "Enemy_02_Amount", "Enemy_03_Amount");
            Enemy_Amount = Array.ConvertAll(tempEnemy_Amount, x => Int32.Parse(x));

            string tempDifficulty = FileReader.FindInfoOfName(GameInfo.FolderLevelsInfo + GameInfo.LevelName + "_Info.txt", "Difficulty", '=');
            Difficulty = Int32.Parse(tempDifficulty);
        }
    }
}
