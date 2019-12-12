﻿using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense
{
    static class GameInfo
    {
        private static List<DrawScore> myDrawScore;
        private static bool myIsPaused;
        private static float myDrawScoreDelay;
        private static int[] myHighScores;
        private static int myScore;
        private static string
            myLevelName,
            myFolderLevels,
            myFolderLevelsInfo,
            myFolderHighScores;

        public static bool IsPaused
        {
            get => myIsPaused;
            set => myIsPaused = value;
        }
        public static int[] HighScores
        {
            get => myHighScores;
        }
        public static int HighScore
        {
            get => myHighScores.Max();
        }
        public static int Score
        {
            get => myScore;
            set => myScore = value;
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
        public static string TerrainType
        {
            get => FileReader.FindInfo(FolderLevelsInfo + myLevelName + "_Info.txt", "Terrain", '=').First();
        }

        public static void Initialize(float aDrawScoreDelay)
        {
            myDrawScoreDelay = aDrawScoreDelay;

            myDrawScore = new List<DrawScore>();
            myScore = 0;
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

        public static void Update(GameTime aGameTime)
        {
            for (int i = myDrawScore.Count - 1; i >= 0; i--)
            {
                if (myDrawScore[i].DrawScoreTimer > 0)
                {
                    myDrawScore[i].DrawScoreTimer -= (float)aGameTime.ElapsedGameTime.TotalSeconds;
                }
                else
                {
                    myDrawScore.RemoveAt(i);
                }
            }
        }

        public static void Draw(SpriteBatch aSpriteBatch, GameWindow aWindow, SpriteFont aFont)
        {
            StringManager.DrawStringLeft(aSpriteBatch, aFont, "Score: " + myScore.ToString(), 
                new Vector2(32, 64), Color.Black, 0.7f);
            StringManager.DrawStringMid(aSpriteBatch, aFont, "HighScore: " + HighScore.ToString(), 
                new Vector2((aWindow.ClientBounds.Width / 2), 32), Color.Black, 0.7f);
            StringManager.DrawStringRight(aSpriteBatch, aFont, GameInfo.LevelName, 
                new Vector2((aWindow.ClientBounds.Width - 32), 64), Color.Black, 0.7f);

            for (int i = myDrawScore.Count - 1; i >= 0; i--)
            {
                if (myDrawScore[i].DrawScoreTimer > 0)
                {
                    StringManager.DrawStringMid(aSpriteBatch, aFont, myDrawScore[i].DrawScoreValue.ToString(), myDrawScore[i].DrawScorePosition, Color.Black, 0.3f);
                }
            }
        }

        public static void AddScore(Vector2 aPos, int someScore)
        {
            myScore += someScore;

            myDrawScore.Add(new DrawScore(new Vector2(aPos.X, aPos.Y - Level.TileSize.Y),
                myDrawScoreDelay, someScore));
        }
    }

    class DrawScore
    {
        private Vector2 myDrawScorePosition;
        private float myDrawScoreTimer;
        private int myDrawScoreValue;

        public Vector2 DrawScorePosition
        {
            get => myDrawScorePosition;
            set => myDrawScorePosition = value;
        }
        public float DrawScoreTimer
        {
            get => myDrawScoreTimer;
            set => myDrawScoreTimer = value;
        }
        public int DrawScoreValue
        {
            get => myDrawScoreValue;
            set => myDrawScoreValue = value;
        }


        public DrawScore(Vector2 aDrawScorePosition, float aDrawScoreTimer, int aDrawScoreValue)
        {
            this.myDrawScorePosition = aDrawScorePosition;
            this.myDrawScoreTimer = aDrawScoreTimer;
            this.myDrawScoreValue = aDrawScoreValue;
        }
    }
}
