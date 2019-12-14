using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tower_Defense
{
    class EditorState : State
    {
        enum EditorStates
        {
            isEditing,
            isLoading,
            isSaving,
            isDeleting,
            isEditingInfo,
            isSelectingPath
        }

        private SpriteFont my8bitFont;
        private Tile[] mySelections;
        private Button[] myLevels;
        private Button 
            myLoadButton,
            mySaveButton,
            myDeleteButton,
            myPathButton,
            myInfoButton;
        private Vector2 
            myStartPosition,
            myGoalPosition;
        private Rectangle myOffset;
        private EditorStates myEditorState;
        private char mySelectedTile;
        private char[,] myLevel;
        private float
            myTimer,
            myDelay;
        private int myTile;
        private string myLevelName;

        public EditorState(MainGame aGame, GameWindow aWindow) : base(aGame)
        {
            Level.LoadLevel(aWindow, new Point(64, 32), "Level_Template");

            Camera.Initialize(aWindow, new Vector2(aWindow.ClientBounds.Width / 2, aWindow.ClientBounds.Height / 2));
                
            this.myLoadButton = new Button(new Vector2(32, 32), new Point(128, 48), null, "LOAD", 0.6f);
            this.mySaveButton = new Button(new Vector2(32, 96), new Point(128, 48), null, "SAVE", 0.6f);
            this.myDeleteButton = new Button(new Vector2(32, 160), new Point(128, 48), null, "DEL", 0.6f);
            this.myInfoButton = new Button(new Vector2(192, 32), new Point(128, 48), null, "INFO", 0.6f);
            this.myPathButton = new Button(new Vector2(192, 96), new Point(128, 48), null, "PATH", 0.6f);

            this.myLevel = new char[
                Level.MapSize.X / Level.TileSize.X,
                Level.MapSize.Y / Level.TileSize.Y];

            this.mySelections = new Tile[]
            {
                new Tile(new Vector2(aWindow.ClientBounds.Width - 160, 64), new Point(128, 64), '#', GameInfo.TerrainType),
                new Tile(new Vector2(aWindow.ClientBounds.Width - 160, 128), new Point(128, 64), '/', GameInfo.TerrainType)
            };

            this.myStartPosition = Vector2.Zero;
            this.myGoalPosition = Vector2.Zero;
            this.myEditorState = EditorStates.isEditing;
            this.myOffset = new Rectangle(
                -Level.TileSize.X / 16, -Level.TileSize.Y / 16, 
                Level.TileSize.X / 8, Level.TileSize.Y / 8);
            this.mySelectedTile = '-';
            this.myDelay = 0.50f;
            this.myTile = -1;
            this.myLevelName = string.Empty;
        }

        public override void Update(GameTime aGameTime, GameWindow aWindow)
        {
            switch(myEditorState)
            {
                case EditorStates.isEditing:
                    Camera.MoveCamera();

                    Misc(aGameTime);

                    SelectTile();
                    EditMap();

                    SaveLevel(aWindow);
                    LoadLevel(aWindow);
                    DeleteLevel(aWindow);
                    CreatePath();
                    break;
                case EditorStates.isLoading:
                    SelectLevelToLoad(aWindow);
                    break;
                case EditorStates.isSaving:
                    TypeNameOfLevel();
                    break;
                case EditorStates.isDeleting:
                    SelectLevelToDelete();
                    break;
                case EditorStates.isEditingInfo:

                    break;
                case EditorStates.isSelectingPath:
                    Camera.MoveCamera();

                    SelectPath(aWindow);
                    break;
            }

            if (KeyMouseReader.KeyPressed(Keys.Escape))
            {
                switch (myEditorState)
                {
                    case EditorStates.isEditing:
                        myGame.ChangeState(new MenuState(myGame, aWindow));
                        break;
                    default:
                        myEditorState = EditorStates.isEditing;
                        break;
                }
            }
        }

        public override void Draw(SpriteBatch aSpriteBatch, GameTime aGameTime, GameWindow aWindow)
        {
            aSpriteBatch.End();

            aSpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend,
                null, null, null, null, Camera.TranslationMatrix);

            Level.DrawTiles(aSpriteBatch);

            switch (myEditorState)
            {
                case EditorStates.isEditing:
                    Array.ForEach(mySelections, t => t.Draw(aSpriteBatch));

                    if (myTile >= 0 && myTile < mySelections.Length)
                    {
                        aSpriteBatch.Draw(mySelections[myTile].Texture,
                            new Rectangle(
                                (int)Camera.ViewToWorld(KeyMouseReader.CurrentMouseState.Position.ToVector2()).X,
                                (int)Camera.ViewToWorld(KeyMouseReader.CurrentMouseState.Position.ToVector2()).Y,
                                (int)(mySelections[myTile].BoundingBox.Width / Camera.Zoom),
                                (int)(mySelections[myTile].BoundingBox.Height / Camera.Zoom)), null, Color.White);
                    }

                    myLoadButton.Draw(aSpriteBatch);
                    mySaveButton.Draw(aSpriteBatch);
                    myDeleteButton.Draw(aSpriteBatch);
                    myInfoButton.Draw(aSpriteBatch);
                    myPathButton.Draw(aSpriteBatch);

                    StringManager.CameraDrawStringLeft(aSpriteBatch, my8bitFont, "Press escape to go back to menu", new Vector2(16, aWindow.ClientBounds.Height - 16), Color.Black * 0.50f, 0.4f);
                    break;
                case EditorStates.isLoading:
                    Array.ForEach(myLevels, b => b.Draw(aSpriteBatch));
                    StringManager.CameraDrawStringMid(aSpriteBatch, my8bitFont, "LOAD", new Vector2((aWindow.ClientBounds.Width / 2), 32), Color.Black, 0.9f);
                    StringManager.CameraDrawStringLeft(aSpriteBatch, my8bitFont, "Press escape to go back to editor", new Vector2(16, aWindow.ClientBounds.Height - 16), Color.Black * 0.50f, 0.4f);
                    break;
                case EditorStates.isSaving:
                    StringManager.CameraDrawStringMid(aSpriteBatch, my8bitFont, "Type name of level", new Vector2((aWindow.ClientBounds.Width / 2), 32), Color.Black, 0.8f);
                    StringManager.CameraDrawStringMid(aSpriteBatch, my8bitFont, myLevelName + "_", new Vector2((aWindow.ClientBounds.Width / 2), 96), Color.Black, 0.8f);
                    StringManager.CameraDrawStringLeft(aSpriteBatch, my8bitFont, "Press escape to go back to editor", new Vector2(16, aWindow.ClientBounds.Height - 16), Color.Black * 0.50f, 0.4f);
                    break;
                case EditorStates.isDeleting:
                    Array.ForEach(myLevels, b => b.Draw(aSpriteBatch));
                    StringManager.CameraDrawStringMid(aSpriteBatch, my8bitFont, "DELETE", new Vector2(aWindow.ClientBounds.Width / 2, 32), Color.Black, 0.9f);
                    StringManager.CameraDrawStringLeft(aSpriteBatch, my8bitFont, "Press escape to go back to editor", new Vector2(16, aWindow.ClientBounds.Height - 16), Color.Black * 0.50f, 0.4f);
                    break;
                case EditorStates.isSelectingPath:
                    StringManager.CameraDrawStringMid(aSpriteBatch, my8bitFont, "Select start and end position of path", new Vector2(aWindow.ClientBounds.Width / 2, 32), Color.Black, 0.9f);
                    StringManager.CameraDrawStringLeft(aSpriteBatch, my8bitFont, "Press escape to go back to editor", new Vector2(16, aWindow.ClientBounds.Height - 16), Color.Black * 0.50f, 0.4f);
                    break;
            }

            StringManager.Draw(aSpriteBatch, my8bitFont);

            if (myStartPosition != Vector2.Zero)
            {
                StringManager.DrawStringMid(aSpriteBatch, my8bitFont, "Start", myStartPosition, Color.Black, 0.3f);
            }

            if (myGoalPosition != Vector2.Zero)
            {
                StringManager.DrawStringMid(aSpriteBatch, my8bitFont, "Goal", myGoalPosition, Color.Black, 0.3f);
            }
        }

        private void Misc(GameTime aGameTime)
        {
            Level.Update();

            myLoadButton.Update();
            mySaveButton.Update();
            myDeleteButton.Update();
            myInfoButton.Update();
            myPathButton.Update();

            if (myTimer > 0)
            {
                myTimer -= (float)aGameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        private void SelectTile()
        {
            for (int i = 0; i < mySelections.Length; i++)
            {
                mySelections[i].Update();

                mySelections[i].BoundingBox = new Rectangle(
                    (int)(Camera.TopLeftCorner.X + (mySelections[i].Position.X / Camera.Zoom)), 
                    (int)(Camera.TopLeftCorner.Y + (mySelections[i].Position.Y + (48 * i)) / Camera.Zoom),
                    (int)(mySelections[i].BoundingBox.Width / Camera.Zoom), 
                    (int)(mySelections[i].BoundingBox.Height / Camera.Zoom));

                Rectangle tempRect = mySelections[i].BoundingBox;

                if (tempRect.Contains(Camera.ViewToWorld(KeyMouseReader.CurrentMouseState.Position.ToVector2())))
                {
                    mySelections[i].BoundingBox = new Rectangle(tempRect.X + myOffset.X, tempRect.Y + myOffset.Y,
                        tempRect.Width + myOffset.Width, tempRect.Height + myOffset.Height);

                    if (KeyMouseReader.LeftClick())
                    {
                        mySelectedTile = mySelections[i].TileType;
                        myTile = i;

                        myTimer = myDelay;
                    }
                }
            }
        }
        private void EditMap()
        {
            if (KeyMouseReader.LeftHold() && mySelectedTile != '-' && myTimer <= 0)
            {
                Tuple<Tile, bool> tempTile = Level.TileAtPos(Camera.ViewToWorld(KeyMouseReader.CurrentMouseState.Position.ToVector2()));

                if (tempTile.Item2 && mySelectedTile != tempTile.Item1.TileType)
                {
                    tempTile.Item1.TileType = mySelectedTile;
                    tempTile.Item1.DefineTileProperties();
                    tempTile.Item1.SetTextureEditor();
                }
            }
            if (KeyMouseReader.RightHold())
            {
                mySelectedTile = '-';
                myTile = -1;

                Tuple<Tile, bool> tempTile = Level.TileAtPos(Camera.ViewToWorld(KeyMouseReader.CurrentMouseState.Position.ToVector2()));

                if (tempTile.Item2 && mySelectedTile != tempTile.Item1.TileType)
                {
                    tempTile.Item1.TileType = '-';
                    tempTile.Item1.DefineTileProperties();
                    tempTile.Item1.SetTextureEditor();
                }
            }
        }

        private void LoadLevel(GameWindow aWindow)
        {
            if (myLoadButton.IsClicked())
            {
                myEditorState = EditorStates.isLoading;
                string[] tempLevelNames = FileReader.FindFileNames(GameInfo.FolderLevels);

                myLevels = new Button[tempLevelNames.Length];

                for (int i = 0; i < myLevels.Length; i++)
                {
                    myLevels[i] = new Button(new Vector2((aWindow.ClientBounds.Width / 2) - 113, (aWindow.ClientBounds.Height / 2) - 64 - 200 + (i * 40)),
                        new Point(226, 32), null, tempLevelNames[i], 0.4f);
                    myLevels[i].LoadContent();
                }
            }
        }
        private void SaveLevel(GameWindow aWindow)
        {
            if (mySaveButton.IsClicked())
            {
                GameInfo.Path = Pathfinder.FindPath(myStartPosition, myGoalPosition, '#', '-');

                if (GameInfo.Path.Count > 1)
                {
                    myEditorState = EditorStates.isSaving;

                    for (int i = 0; i < Level.GetTiles.GetLength(0); i++)
                    {
                        for (int j = 0; j < Level.GetTiles.GetLength(1); j++)
                        {
                            if (!GameInfo.Path.Contains(Level.GetTiles[i, j]))
                            {
                                if (Level.GetTiles[i, j].TileType == '/')
                                {
                                    Level.GetTiles[i, j].TileType = '-';
                                }
                            }
                        }
                    }

                    for (int i = 0; i < myLevel.GetLength(0); i++)
                    {
                        for (int j = 0; j < myLevel.GetLength(1); j++)
                        {
                            myLevel[i, j] = Level.GetTiles[i, j].TileType;
                        }
                    }
                }
                else
                {
                    StringManager.DrawStrings.Add(new DrawString(new Vector2(aWindow.ClientBounds.Width - 32, aWindow.ClientBounds.Height - 32),
                        Color.Red, true, 3.0f, 0.7f, 2, "No path"));
                }
            }
        }
        private void DeleteLevel(GameWindow aWindow)
        {
            if (myDeleteButton.IsClicked())
            {
                myEditorState = EditorStates.isDeleting;
                string[] tempLevelNames = FileReader.FindFileNames(GameInfo.FolderLevels);

                myLevels = new Button[tempLevelNames.Length - 1];

                int tempAddButton = 0;
                for (int i = 0; i < tempLevelNames.Length; i++)
                {
                    if (tempLevelNames[i] != "Level_Template")
                    {
                        myLevels[tempAddButton] = new Button(new Vector2((aWindow.ClientBounds.Width / 2) - 113, (aWindow.ClientBounds.Height / 2) - 64 - 200 + (tempAddButton * 40)),
                            new Point(226, 32), null, tempLevelNames[i], 0.4f);
                        myLevels[tempAddButton].LoadContent();

                        tempAddButton++;
                    }
                }
            }
        }
        private void CreatePath()
        {
            if (myPathButton.IsClicked())
            {
                myEditorState = EditorStates.isSelectingPath;
            }
        }

        private void SelectLevelToLoad(GameWindow aWindow)
        {
            foreach (Button button in myLevels)
            {
                button.Update();
                if (button.IsClicked())
                {
                    Level.LoadLevel(aWindow, new Point(64, 32), button.DisplayText);

                    LoadContent();

                    myEditorState = EditorStates.isEditing;
                    myLevels = null;

                    Camera.Initialize(aWindow, new Vector2(aWindow.ClientBounds.Width / 2, aWindow.ClientBounds.Height / 2));
                }
            }
        }
        private void TypeNameOfLevel()
        {
            if (myLevelName.Length < 18) //Name limit
            {
                myLevelName += KeyMouseReader.KeyInput(@"[a-zA-Z]");
            }

            if (myLevelName.Length > 0)
            {
                if (KeyMouseReader.KeyPressed(Keys.Space))
                {
                    myLevelName += "_";
                }
                if (KeyMouseReader.KeyPressed(Keys.Back))
                {
                    myLevelName = myLevelName.Remove(myLevelName.Length - 1, 1);
                }
                if (KeyMouseReader.KeyPressed(Keys.Enter))
                {
                    myEditorState = EditorStates.isEditing;
                    Level.SaveLevel(myLevelName, myLevel, myStartPosition, myGoalPosition);
                }
            }
        }
        private void SelectLevelToDelete()
        {
            foreach (Button button in myLevels)
            {
                button.Update();
                if (button.IsClicked())
                {
                    Level.DeleteLevel(button.DisplayText);

                    myEditorState = EditorStates.isEditing;
                    myLevels = null;
                }
            }
        }
        private void SelectPath(GameWindow aWindow)
        {
            if (KeyMouseReader.LeftClick())
            {
                Tuple<Tile, bool> tempTile = Level.TileAtPos(Camera.ViewToWorld(KeyMouseReader.CurrentMouseState.Position.ToVector2()));

                if (tempTile.Item2)
                {
                    if (tempTile.Item1.TileType == '/')
                    {
                        myStartPosition = tempTile.Item1.BoundingBox.Center.ToVector2();
                    }
                }
            }
            if (KeyMouseReader.RightClick())
            {
                Tuple<Tile, bool> tempTile = Level.TileAtPos(Camera.ViewToWorld(KeyMouseReader.CurrentMouseState.Position.ToVector2()));

                if (tempTile.Item2)
                {
                    if (tempTile.Item1.TileType == '/')
                    {
                        myGoalPosition = tempTile.Item1.DestRect.Center.ToVector2();
                    }
                }
            }

            if (KeyMouseReader.KeyPressed(Keys.Enter))
            {
                GameInfo.Path = Pathfinder.FindPath(myStartPosition, myGoalPosition, '#', '-');

                if (GameInfo.Path.Count > 1)
                {
                    StringManager.DrawStrings.Add(new DrawString(new Vector2(aWindow.ClientBounds.Width - 32, aWindow.ClientBounds.Height - 32),
                        Color.DarkGreen, true, 3.0f, 0.7f, 2, "Path found!"));
                }
                else
                {
                    StringManager.DrawStrings.Add(new DrawString(new Vector2(aWindow.ClientBounds.Width - 32, aWindow.ClientBounds.Height - 32),
                        Color.Red, true, 3.0f, 0.7f, 2, "Path not found..."));
                }
            }
        }

        public override void LoadContent()
        {
            Level.LoadContent();

            Array.ForEach(mySelections, t => t.SetTextureEditor());

            myLoadButton.LoadContent();
            mySaveButton.LoadContent();
            myDeleteButton.LoadContent();
            myInfoButton.LoadContent();
            myPathButton.LoadContent();

            my8bitFont = ResourceManager.RequestFont("8-bit");
        }
    }
}
