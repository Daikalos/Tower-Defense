using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense
{
    static class Level
    {
        private static Tile[,] myTiles;
        private static Point
            myTileSize,
            myMapSize,
            myOffset;

        public static Tile[,] GetTiles
        {
            get => myTiles;
        }

        public static Point TileSize
        {
            get => myTileSize;
        }
        public static Point MapSize
        {
            get => myMapSize;
        }

        public static Tuple<Tile, bool> TileAtPos(Vector2 aPos)
        {
            Matrix tempMatrix = Matrix.Identity;

            //Tile-space to screen-space
            tempMatrix.M11 = TileSize.X / 2; //Vector points
            tempMatrix.M21 = -TileSize.X / 2;
            tempMatrix.M12 = TileSize.Y / 2;
            tempMatrix.M22 = TileSize.Y / 2;

            tempMatrix.M31 = GetTiles[0, 0].Position.X + myTileSize.X / 2; //Offset
            tempMatrix.M32 = GetTiles[0, 0].Position.Y;

            tempMatrix = Matrix.Invert(tempMatrix); //Screen-space to tile-space

            int tempX = (int)Math.Floor((aPos.X * tempMatrix.M11 + aPos.Y * tempMatrix.M21 + tempMatrix.M31)); //Multiply with mouse coordinates
            int tempY = (int)Math.Floor((aPos.X * tempMatrix.M12 + aPos.Y * tempMatrix.M22 + tempMatrix.M32));

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

        public static void DrawTiles(SpriteBatch aSpriteBatch)
        {
            Vector2 tempCameraOffset = new Vector2(Camera.Position.X, Camera.Position.Y);
            Vector2 tempCameraDimensions = new Vector2(
                ((Camera.Position.X - Camera.TopLeftCorner.X) * 2) + TileSize.X * 2,
                ((Camera.Position.Y - Camera.TopLeftCorner.Y) * 2) + TileSize.Y * 2);

            int tempXPosition = (int)(tempCameraOffset.X - (tempCameraDimensions.X / 2));
            int tempXLength = (int)tempCameraOffset.X + (int)Math.Ceiling(tempCameraDimensions.X / 2);

            int tempYPosition = (int)(tempCameraOffset.Y - (tempCameraDimensions.Y / 2));
            int tempYLength = (int)tempCameraOffset.Y + (int)Math.Ceiling(tempCameraDimensions.Y / 2);

            for (int x = tempXPosition; x < tempXLength; x += TileSize.X / 2)
            {
                for (int y = tempYPosition; y < tempYLength; y += TileSize.Y / 2)
                {
                    Tuple<Tile, bool> tempTile = Level.TileAtPos(new Vector2(x, y));
                    if (tempTile.Item2)
                    {
                        Level.TileAtPos(new Vector2(x, y)).Item1.Draw(aSpriteBatch);
                    }
                }
            }
        }
        public static void DrawTilesEditor(SpriteBatch aSpriteBatch)
        {
            Vector2 tempCameraOffset = new Vector2(Camera.Position.X, Camera.Position.Y);
            Vector2 tempCameraDimensions = new Vector2(
                ((Camera.Position.X - Camera.TopLeftCorner.X) * 2) + TileSize.X * 2,
                ((Camera.Position.Y - Camera.TopLeftCorner.Y) * 2) + TileSize.Y * 2);

            int tempXPosition = (int)(tempCameraOffset.X - (tempCameraDimensions.X / 2));
            int tempXLength = (int)tempCameraOffset.X + (int)Math.Ceiling(tempCameraDimensions.X / 2);

            int tempYPosition = (int)(tempCameraOffset.Y - (tempCameraDimensions.Y / 2));
            int tempYLength = (int)tempCameraOffset.Y + (int)Math.Ceiling(tempCameraDimensions.Y / 2);

            for (int x = tempXPosition; x < tempXLength; x += TileSize.X / 2)
            {
                for (int y = tempYPosition; y < tempYLength; y += TileSize.Y / 2)
                {
                    Tuple<Tile, bool> tempTile = Level.TileAtPos(new Vector2(x, y));
                    if (tempTile.Item2)
                    {
                        Level.TileAtPos(new Vector2(x, y)).Item1.DrawEditor(aSpriteBatch);
                    }
                }
            }
        }

        public static bool LoadLevel(GameWindow aWindow, Point aTileSize, string aLevelName)
        {
            if (File.Exists(GameInfo.FolderLevels + aLevelName + ".txt"))
            {
                GameInfo.LevelName = aLevelName;
                myOffset = new Point((aWindow.ClientBounds.Width / 2), (aWindow.ClientBounds.Height / 2));

                string[] myLevelBuilder = File.ReadAllLines(GameInfo.FolderLevels + aLevelName + ".txt");
                myTileSize = aTileSize;

                int tempSizeX = myLevelBuilder[0].Length;
                int tempSizeY = myLevelBuilder.Length;

                myTiles = new Tile[tempSizeX, tempSizeY];

                string tempStartPosString = FileReader.FindInfo(GameInfo.FolderLevelsInfo + aLevelName + "_Info.txt", "Start", '=').First();
                string tempGoalPosString = FileReader.FindInfo(GameInfo.FolderLevelsInfo + aLevelName + "_Info.txt", "Goal", '=').First();

                string tempCleanString = string.Empty;
                string[] tempSplitString = new string[] { string.Empty };

                tempCleanString = tempStartPosString.Replace("{X:", "");
                tempCleanString = tempCleanString.Replace("Y:", "");
                tempCleanString = tempCleanString.Replace("}", "");
                tempSplitString = tempCleanString.Split(' ');

                Vector2 tempStartPos = new Vector2(float.Parse(tempSplitString[0]), float.Parse(tempSplitString[1]));

                tempCleanString = tempGoalPosString.Replace("{X:", "");
                tempCleanString = tempCleanString.Replace("Y:", "");
                tempCleanString = tempCleanString.Replace("}", "");
                tempSplitString = tempCleanString.Split(' ');

                Vector2 tempGoalPos = new Vector2(float.Parse(tempSplitString[0]), float.Parse(tempSplitString[1]));

                for (int x = 0; x < tempSizeX; x++)
                {
                    for (int y = 0; y < tempSizeY; y++) //Isometric
                    {
                        int tempX = (x * myTileSize.X / 2) - (y * myTileSize.X / 2) + myOffset.X - (myTileSize.X / 2);
                        int tempY = (y * myTileSize.Y / 2) + (x * myTileSize.Y / 2);

                        myTiles[x, y] = new Tile(
                            new Vector2(tempX, tempY),
                            myTileSize, myLevelBuilder[y][x]);
                    }
                }

                GameInfo.Path = Pathfinder.FindPath(tempStartPos, tempGoalPos, '#', '-');

                myMapSize = new Point(
                    myTiles.GetLength(0) * myTileSize.X,
                    myTiles.GetLength(1) * myTileSize.Y);

                return true;
            }
            return false;
        }
        public static void SaveLevel(string aLevelName, char[,] aLevel, Vector2 aStart, Vector2 aGoal)
        {
            string tempPathLevel = GameInfo.FolderLevels + aLevelName + ".txt";
            string tempPathHighScores = GameInfo.FolderHighScores + aLevelName + "_HighScores.txt";
            string tempPathLevelInfo = GameInfo.FolderLevelsInfo + aLevelName + "_Info.txt";

            if (File.Exists(tempPathLevel))
            {
                File.Delete(tempPathLevel);
            }
            if (File.Exists(tempPathHighScores))
            {
                File.Delete(tempPathHighScores);
            }
            if (File.Exists(tempPathLevelInfo))
            {
                File.Delete(tempPathLevelInfo);
            }

            FileStream tempFS = File.Create(tempPathLevel);
            tempFS.Close();

            tempFS = File.Create(tempPathHighScores);
            tempFS.Close();

            tempFS = File.Create(tempPathLevelInfo);
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

            File.AppendAllText(tempPathLevelInfo, "Start=" + aStart);
            File.AppendAllText(tempPathLevelInfo, Environment.NewLine + "Goal=" + aGoal);
        }
        public static void DeleteLevel(string aLevelName)
        {
            string tempPathLevel = GameInfo.FolderLevels + aLevelName + ".txt";
            string tempPathHighScores = GameInfo.FolderHighScores + aLevelName + "_HighScores.txt";
            string tempPathLevelInfo = GameInfo.FolderLevelsInfo + aLevelName + "_Info.txt";

            if (File.Exists(tempPathLevel))
            {
                File.Delete(tempPathLevel);
            }
            if (File.Exists(tempPathHighScores))
            {
                File.Delete(tempPathHighScores);
            }
            if (File.Exists(tempPathLevelInfo))
            {
                File.Delete(tempPathLevelInfo);
            }
        }
        public static bool CreateLevel(int[,] aLevelSize)
        {
            int tempSizeX = aLevelSize.GetLength(0);
            int tempSizeY = aLevelSize.GetLength(1);
            Tile[,] tempTiles;

            try
            {
                tempTiles = new Tile[tempSizeX, tempSizeY];

                for (int x = 0; x < tempSizeX; x++)
                {
                    for (int y = 0; y < tempSizeY; y++)
                    {
                        int tempX = (x * myTileSize.X / 2) - (y * myTileSize.X / 2) + myOffset.X - (myTileSize.X / 2);
                        int tempY = (y * myTileSize.Y / 2) + (x * myTileSize.Y / 2);

                        tempTiles[x, y] = new Tile(
                            new Vector2(tempX, tempY),
                            myTileSize, '-');
                    }
                }
            }
            catch (OutOfMemoryException anException)
            {
                MessageBox.Show(anException.ToString());
                return false;
            }

            myTiles = tempTiles;

            myMapSize = new Point(
                myTiles.GetLength(0) * myTileSize.X,
                myTiles.GetLength(1) * myTileSize.Y);

            StringManager.AddString(new DrawString(new Vector2(32, 48),
                Color.Green, true, 3.0f, 0.7f, 0, "Map created"));

            return true;
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
