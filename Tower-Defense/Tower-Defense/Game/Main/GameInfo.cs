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
        private static List<Tile> myPath;
        private static bool myIsPaused;
        private static int[] myHighScores;
        private static int 
            myScore,
            myMoney,
            myHealth,
            myGameSpeed;
        private static string
            myLevelName,
            myFolderLevels,
            myFolderLevelsInfo,
            myFolderHighScores;

        public static List<Tile> Path
        {
            get => myPath;
            set => myPath = value;
        }
        public static bool IsPaused
        {
            get => myIsPaused;
            set => myIsPaused = value;
        }
        public static int[] HighScores
        {
            get => myHighScores;
        }
        public static int Score
        {
            get => myScore;
            set => myScore = value;
        }
        public static int Money
        {
            get => myMoney;
            set => myMoney = value;
        }
        public static int Health
        {
            get => myHealth;
            set => myHealth = value;
        }
        public static int GameSpeed
        {
            get => myGameSpeed;
            set => myGameSpeed = value;
        }
        public static string LevelName
        {
            get => myLevelName;
            set => myLevelName = value;
        }
        public static string FolderLevels
        {
            get => myFolderLevels;
            set => myFolderLevels = value;
        }
        public static string FolderLevelsInfo
        {
            get => myFolderLevelsInfo;
            set => myFolderLevelsInfo = value;
        }
        public static string FolderHighScores
        {
            get => myFolderHighScores;
            set => myFolderHighScores = value;
        }

        public static void Initialize()
        {
            myScore = 0;
            myGameSpeed = 1;
            myHealth = 100;
            myMoney = 500000; //Starting values
        }

        public static void LoadHighScore(string aLevelName)
        {
            string tempPath = GameInfo.FolderHighScores + aLevelName + "_HighScores.txt";

            string[] tempScores = FileReader.FindInfo(tempPath, "HighScore", '=');
            myHighScores = Array.ConvertAll(tempScores, s => Int32.Parse(s));

            if (myHighScores.Length == 0)
            {
                myHighScores = new int[] { 0 };
            }

            Array.Sort(myHighScores);
            Array.Reverse(myHighScores);
        }
        public static void SaveHighScore(string aLevelName)
        {
            string tempPath = GameInfo.FolderHighScores + aLevelName + "_HighScores.txt";

            if (myHighScores.Length > 0)
            {
                if (myScore > 0)
                {
                    if (myHighScores.First() != 0)
                    {
                        File.AppendAllText(tempPath, Environment.NewLine + "HighScore=" + myScore.ToString());
                    }
                    else
                    {
                        File.AppendAllText(tempPath, "HighScore=" + myScore.ToString());
                    }
                }
            }
        }

        public static void Draw(SpriteBatch aSpriteBatch, GameWindow aWindow, SpriteFont aFont)
        {
            StringManager.CameraDrawStringLeft(aSpriteBatch, aFont, "Score: " + myScore.ToString(),
                new Vector2(32, 64), Color.LightSlateGray, 0.6f);
            StringManager.CameraDrawStringLeft(aSpriteBatch, aFont, "Money: $" + myMoney.ToString(), 
                new Vector2(32, 128), Color.MediumSeaGreen, 0.6f);
            StringManager.CameraDrawStringLeft(aSpriteBatch, aFont, "Health: " + myHealth.ToString(),
                new Vector2(32, 160), Color.IndianRed, 0.6f);
            StringManager.CameraDrawStringMid(aSpriteBatch, aFont, GameInfo.LevelName, 
                new Vector2((aWindow.ClientBounds.Width / 2), 32), Color.LightSlateGray, 0.7f);
            
            if (myPath.Count > 1)
            {
                StringManager.DrawStringMid(aSpriteBatch, aFont, "Start", myPath[0].DestRect.Center.ToVector2(), Color.Black, 0.3f);
                StringManager.DrawStringMid(aSpriteBatch, aFont, "Goal", myPath[myPath.Count - 1].DestRect.Center.ToVector2(), Color.Black, 0.3f);
            }
        }

        public static void AddScore(Vector2 aPos, float aDelay, int someScore)
        {
            myScore += someScore;

            StringManager.DrawStrings.Add(new DrawString(new Vector2(aPos.X, aPos.Y - Level.TileSize.Y),
                Color.Black, false, aDelay, 0.4f, 1, someScore.ToString()));
        }
    }
}
