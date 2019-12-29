using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense
{
    static class GameInfo
    {
        public static List<Tile> Path { get; set; }

        public static bool IsPaused { get; set; }

        public static float GameSpeed { get; set; }

        public static int[] HighScores { get; set; }
        public static int Score { get; set; }
        public static int Money { get; set; }
        public static int Health { get; set; }
        public static int Wave { get; set; }
        public static int TotalWaves { get; set; }

        public static string LevelName { get; set; }

        //Get properties, will not change
        public static int MoneyEachWave => 50;

        public static string FolderLevels => "../../../../Levels/Levels/";
        public static string FolderLevelsInfo => "../../../../Levels/Levels_Info/";
        public static string FolderHighScores => "../../../../Levels/HighScores/";

        public static void Initialize()
        {
            //Starting values
            Score = 0;
            GameSpeed = 1;
            Health = 100;
            Money = 1000;
            Wave = 1;
        }

        public static void LoadHighScore(string aLevelName)
        {
            string tempPath = GameInfo.FolderHighScores + aLevelName + "_HighScores.txt";

            string[] tempScores = FileReader.FindAllInfoOfName(tempPath, "HighScore", '=');
            HighScores = Array.ConvertAll(tempScores, s => Int32.Parse(s));

            if (HighScores.Length == 0)
            {
                HighScores = new int[] { 0 };
            }

            Array.Sort(HighScores);
            Array.Reverse(HighScores);
        }
        public static void SaveHighScore(string aLevelName)
        {
            string tempPath = GameInfo.FolderHighScores + aLevelName + "_HighScores.txt";

            if (HighScores.Length > 0)
            {
                if (Score > 0)
                {
                    if (HighScores.First() != 0)
                    {
                        File.AppendAllText(tempPath, Environment.NewLine + "HighScore=" + Score.ToString());
                    }
                    else
                    {
                        File.AppendAllText(tempPath, "HighScore=" + Score.ToString());
                    }
                }
            }
        }

        public static void Draw(SpriteBatch aSpriteBatch, GameWindow aWindow, SpriteFont aFont)
        {
            StringManager.CameraDrawStringLeft(aSpriteBatch, aFont, "Wave: " + Wave.ToString(),
                new Vector2(32, 32), Color.LightSlateGray, 0.6f);
            StringManager.CameraDrawStringLeft(aSpriteBatch, aFont, "Score: " + Score.ToString(),
                new Vector2(32, 64), Color.LightSlateGray, 0.6f);
            StringManager.CameraDrawStringLeft(aSpriteBatch, aFont, "Enemies: " + SpawnManager.TotalAmountToSpawn.ToString(),
                new Vector2(32, 96), Color.LightSlateGray, 0.6f);
            StringManager.CameraDrawStringLeft(aSpriteBatch, aFont, "Money: $" + Money.ToString(), 
                new Vector2(32, 160), Color.MediumSeaGreen, 0.6f);
            StringManager.CameraDrawStringLeft(aSpriteBatch, aFont, "Health: " + Health.ToString(),
                new Vector2(32, 192), Color.IndianRed, 0.6f);
            StringManager.CameraDrawStringMid(aSpriteBatch, aFont, GameInfo.LevelName, 
                new Vector2((aWindow.ClientBounds.Width / 2), 32), Color.LightSlateGray, 0.7f);
            
            if (Path.Count > 1 && !SpawnManager.SpawnEnemies)
            {
                StringManager.DrawStringMid(aSpriteBatch, aFont, "Start", Path[0].DestRect.Center.ToVector2(), Color.Black, 0.3f);
                StringManager.DrawStringMid(aSpriteBatch, aFont, "Goal", Path[Path.Count - 1].DestRect.Center.ToVector2(), Color.Black, 0.3f);
            }
        }
    }
}
