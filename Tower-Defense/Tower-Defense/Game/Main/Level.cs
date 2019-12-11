using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense
{
    static class Level
    {
        private static Tile[,] myTiles;
        private static Point
            myTileSize,
            myMapSize;

        public static Point TileSize
        {
            get => myTileSize;
        }
        public static Point MapSize
        {
            get => myMapSize;
        }

        public static Tile[,] GetTiles
        {
            get => myTiles;
        }

        public static Tuple<Tile, bool> TileAtPos(Vector2 aPos)
        {
            int tempX = (int)(aPos.X / myTileSize.X);
            int tempY = (int)(aPos.Y / myTileSize.Y);

            if (CheckIn(tempX, tempY))
            {
                return new Tuple<Tile, bool>(myTiles[tempX, tempY], true);
            }

            return new Tuple<Tile, bool>(myTiles[0, 0], false);
        }

        public static Tile ClosestTile(Vector2 aPos, params char[] aTileType)
        {
            Tile tempClosest = null;
            float tempMinDistance = float.MaxValue;

            for (int i = 0; i < myTiles.GetLength(0); i++)
            {
                for (int j = 0; j < myTiles.GetLength(1); j++)
                {
                    foreach (char type in aTileType)
                    {
                        if (myTiles[i, j].TileType != type)
                        {
                            float tempDistance = Vector2.Distance(myTiles[i, j].GetCenter(), aPos);
                            if (tempDistance < tempMinDistance)
                            {
                                tempClosest = myTiles[i, j];
                                tempMinDistance = tempDistance;
                            }
                        }
                    }
                }
            }
            return tempClosest;
        }

        public static void Update()
        {
            for (int i = 0; i < myTiles.GetLength(0); i++)
            {
                for (int j = 0; j < myTiles.GetLength(1); j++)
                {
                    myTiles[i, j].Update();
                }
            }
        }

        public static void DrawTiles(SpriteBatch aSpriteBatch, GameTime aGameTime)
        {
            for (int i = 0; i < myTiles.GetLength(0); i++)
            {
                for (int j = 0; j < myTiles.GetLength(1); j++)
                {
                    myTiles[i, j].Draw(aSpriteBatch);
                }
            }
        }

        public static bool LoadLevel(GameWindow aWindow, Point aTileSize, string aLevelName)
        {
            if (File.Exists(GameInfo.FolderLevels + aLevelName + ".txt"))
            {
                GameInfo.LevelName = aLevelName;

                string[] myLevelBuilder = File.ReadAllLines(GameInfo.FolderLevels + aLevelName + ".txt");
                myTileSize = aTileSize;

                int tempSizeX = myLevelBuilder[0].Length;
                int tempSizeY = myLevelBuilder.Length;

                myTiles = new Tile[tempSizeX, tempSizeY];

                string tempTerrainType = GameInfo.TerrainType;

                for (int x = 0; x < tempSizeX; x++)
                {
                    for (int y = 0; y < tempSizeY; y++) //Isometric
                    {
                        int tempX = (y * myTileSize.X / 2) + (x * myTileSize.X / 2) - (aWindow.ClientBounds.Width / 2) + (aWindow.ClientBounds.Width - (tempSizeX * myTileSize.Y));
                        int tempY = (x * myTileSize.Y / 2) - (y * myTileSize.Y / 2) + (aWindow.ClientBounds.Height / 2);

                        myTiles[x, y] = new Tile(
                            new Vector2(tempX, tempY),
                            myTileSize, myLevelBuilder[y][x], tempTerrainType);
                    }
                }

                myMapSize = new Point(
                    myTiles.GetLength(0) * myTileSize.X,
                    myTiles.GetLength(1) * myTileSize.Y);

                return true;
            }
            return false;
        }
        public static void SaveLevel(string aLevelName, char[,] aLevel)
        {
            string tempPathLevel = GameInfo.FolderLevels + aLevelName + ".txt";
            string tempPathHighScores = GameInfo.FolderHighScores + aLevelName + "_HighScores.txt";

            if (File.Exists(tempPathLevel))
            {
                File.Delete(tempPathLevel);
            }
            if (File.Exists(tempPathHighScores))
            {
                File.Delete(tempPathHighScores);
            }

            FileStream tempFS = File.Create(tempPathLevel);
            tempFS.Close();

            tempFS = File.Create(tempPathHighScores);
            tempFS.Close();

            for (int i = 0; i < aLevel.GetLength(1); i++)
            {
                string tempLevel = "";

                for (int j = 0; j < aLevel.GetLength(0); j++)
                {
                    tempLevel += aLevel[j, i].ToString();
                }

                File.AppendAllText(tempPathLevel, tempLevel);
                File.AppendAllText(tempPathLevel, Environment.NewLine);
            }
        }
        public static void DeleteLevel(string aLevelName)
        {
            string tempPathLevel = GameInfo.FolderLevels + aLevelName + ".txt";

            string tempPathHighScores = GameInfo.FolderHighScores + aLevelName + "_HighScores.txt";

            if (File.Exists(tempPathLevel))
            {
                File.Delete(tempPathLevel);
            }
            if (File.Exists(tempPathHighScores))
            {
                File.Delete(tempPathHighScores);
            }
        }

        public static bool CheckIn(int anX, int anY)
        {
            return
                anX >= 0 &&
                anX < myTiles.GetLength(0) &&
                anY >= 0 &&
                anY < myTiles.GetLength(1);
        }

        public static void LoadContent()
        {
            for (int i = 0; i < myTiles.GetLength(0); i++)
            {
                for (int j = 0; j < myTiles.GetLength(1); j++)
                {
                    myTiles[i, j].SetTexture();
                }
            }
        }
        public static void LoadContentEditor()
        {
            for (int i = 0; i < myTiles.GetLength(0); i++)
            {
                for (int j = 0; j < myTiles.GetLength(1); j++)
                {
                    myTiles[i, j].SetTextureEditor();
                }
            }
        }
    }
}
