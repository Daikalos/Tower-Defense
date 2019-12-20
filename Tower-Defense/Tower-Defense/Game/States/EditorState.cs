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
            isEditingWaves,
            isSelectingPath
        }

        private SpriteFont my8bitFont;
        private Button[] myLevels;
        private Button
            myLoadButton,
            mySaveButton,
            myDeleteButton,
            myPathButton,
            myInfoButton,
            myWaveButton;
        private Tile[] mySelections;
        private LevelInfo myLevelInfoForm;
        private LevelName myLevelNameForm;
        private LevelWave myLevelWaveForm;
        private Vector2 
            myStartPosition,
            myGoalPosition;
        private Rectangle myOffset;
        private EditorStates myEditorState;
        private char mySelectedTile;
        private float
            myTimer,
            myDelay;
        private int myTile;

        public EditorState(MainGame aGame, GameWindow aWindow) : base(aGame)
        {
            Level.LoadLevel(aWindow, new Point(64, 32), "Level_Template");

            Camera.Initialize(aWindow, new Vector2(aWindow.ClientBounds.Width / 2, aWindow.ClientBounds.Height / 2), 5);
                
            this.myLoadButton = new Button(new Vector2(32, 32), new Point(128, 48), PressLoadLevel, 1, "LOAD", 0.6f);
            this.mySaveButton = new Button(new Vector2(32, 96), new Point(128, 48), PressSaveLevel, 1, "SAVE", 0.6f);
            this.myDeleteButton = new Button(new Vector2(32, 160), new Point(128, 48), PressDeleteLevel, 1, "DEL", 0.6f);
            this.myPathButton = new Button(new Vector2(192, 32), new Point(128, 48), PressCreatePath, 1, "PATH", 0.6f);
            this.myInfoButton = new Button(new Vector2(192, 96), new Point(128, 48), PressEditInfo, 1, "INFO", 0.6f);
            this.myWaveButton = new Button(new Vector2(192, 160), new Point(128, 48), PressEditWaves, 1, "WAVE", 0.6f);

            this.mySelections = new Tile[]
            {
                new Tile(new Vector2(aWindow.ClientBounds.Width - 160, 64), new Point(128, 64), '#', GameInfo.TerrainType),
                new Tile(new Vector2(aWindow.ClientBounds.Width - 160, 128), new Point(128, 64), '/', GameInfo.TerrainType)
            };
            this.myLevelInfoForm = new LevelInfo();
            this.myLevelNameForm = new LevelName();
            this.myLevelWaveForm = new LevelWave();
            this.myStartPosition = Vector2.Zero;
            this.myGoalPosition = Vector2.Zero;
            this.myEditorState = EditorStates.isEditing;
            this.myOffset = new Rectangle(
                -Level.TileSize.X / 16, -Level.TileSize.Y / 16, 
                Level.TileSize.X / 8, Level.TileSize.Y / 8);
            this.mySelectedTile = '-';
            this.myDelay = 0.50f;
            this.myTile = -1;
        }

        public override void Update(GameTime aGameTime, GameWindow aWindow)
        {
            switch (myEditorState)
            {
                case EditorStates.isEditing:
                    Camera.MoveCamera(aGameTime);

                    Misc(aGameTime, aWindow);

                    SelectTile();
                    EditMap();
                    break;
                case EditorStates.isLoading:
                    LoadLevel(aWindow);
                    break;
                case EditorStates.isSaving:
                    //Saving in forms
                    break;
                case EditorStates.isDeleting:
                    DeleteLevel(aWindow);
                    break;
                case EditorStates.isSelectingPath:
                    Camera.MoveCamera(aGameTime);

                    CreatePath(aWindow);
                    break;
                case EditorStates.isEditingInfo:
                    //Editing in forms
                    break;
                case EditorStates.isEditingWaves:
                    //Editing in forms
                    break;
            }

            BackPress(aWindow);
        }

        public override void Draw(SpriteBatch aSpriteBatch, GameTime aGameTime, GameWindow aWindow)
        {
            aSpriteBatch.End();

            aSpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend,
                SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise, null, Camera.TranslationMatrix);

            Level.DrawTilesEditor(aSpriteBatch);

            switch (myEditorState)
            {
                case EditorStates.isEditing:
                    Array.ForEach(mySelections, t => t.DrawEditor(aSpriteBatch));

                    if (myTile >= 0 && myTile < mySelections.Length)
                    {
                        aSpriteBatch.Draw(mySelections[myTile].Texture,
                            new Rectangle(
                                (int)Camera.ViewToWorld(KeyMouseReader.CurrentMouseState.Position.ToVector2()).X,
                                (int)Camera.ViewToWorld(KeyMouseReader.CurrentMouseState.Position.ToVector2()).Y,
                                (int)(mySelections[myTile].DestRect.Width),
                                (int)(mySelections[myTile].DestRect.Height)), null, Color.White);
                    }

                    myLoadButton.Draw(aSpriteBatch);
                    mySaveButton.Draw(aSpriteBatch);
                    myDeleteButton.Draw(aSpriteBatch);
                    myInfoButton.Draw(aSpriteBatch);
                    myWaveButton.Draw(aSpriteBatch);
                    myPathButton.Draw(aSpriteBatch);

                    StringManager.CameraDrawStringLeft(aSpriteBatch, my8bitFont, "{X: " + Level.GetTiles.GetLength(0) + " Y: " + Level.GetTiles.GetLength(1) + "}", new Vector2(332, 48), Color.Black * 0.70f, 0.6f);
                    StringManager.CameraDrawStringLeft(aSpriteBatch, my8bitFont, "Press escape to go back to menu", new Vector2(16, aWindow.ClientBounds.Height - 16), Color.Black * 0.50f, 0.4f);
                    break;
                case EditorStates.isLoading:
                    Array.ForEach(myLevels, b => b.Draw(aSpriteBatch));
                    StringManager.CameraDrawStringMid(aSpriteBatch, my8bitFont, "LOAD", new Vector2((aWindow.ClientBounds.Width / 2), 32), Color.Black, 0.9f);
                    StringManager.CameraDrawStringLeft(aSpriteBatch, my8bitFont, "Press escape to go back to editor", new Vector2(16, aWindow.ClientBounds.Height - 16), Color.Black * 0.50f, 0.4f);
                    break;
                case EditorStates.isSaving:
                    StringManager.CameraDrawStringMid(aSpriteBatch, my8bitFont, "Type name of level", new Vector2((aWindow.ClientBounds.Width / 2), 32), Color.Black, 0.8f);
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
                case EditorStates.isEditingInfo:
                    StringManager.CameraDrawStringMid(aSpriteBatch, my8bitFont, "Edit info of level", new Vector2((aWindow.ClientBounds.Width / 2), 32), Color.Black, 0.8f);
                    StringManager.CameraDrawStringLeft(aSpriteBatch, my8bitFont, "Press escape to go back to editor", new Vector2(16, aWindow.ClientBounds.Height - 16), Color.Black * 0.50f, 0.4f);
                    break;
                case EditorStates.isEditingWaves:
                    StringManager.CameraDrawStringMid(aSpriteBatch, my8bitFont, "Edit waves", new Vector2(aWindow.ClientBounds.Width / 2, 32), Color.Black, 0.9f);
                    StringManager.CameraDrawStringLeft(aSpriteBatch, my8bitFont, "Press escape to go back to editor", new Vector2(16, aWindow.ClientBounds.Height - 16), Color.Black * 0.50f, 0.4f);
                    break;
            }

            StringManager.Draw(aSpriteBatch, my8bitFont);

            if (myStartPosition != Vector2.Zero)
            {
                StringManager.DrawStringMid(aSpriteBatch, my8bitFont, "Start", myStartPosition, Color.Black, 0.3f);
            }
            else
            {
                if (GameInfo.Path.Count > 1)
                {
                    StringManager.DrawStringMid(aSpriteBatch, my8bitFont, "Start", GameInfo.Path[0].DestRect.Center.ToVector2(), Color.Black, 0.3f);
                }
            }
            if (myGoalPosition != Vector2.Zero)
            {
                StringManager.DrawStringMid(aSpriteBatch, my8bitFont, "Goal", myGoalPosition, Color.Black, 0.3f);
            }
            else
            {
                if (GameInfo.Path.Count > 1)
                {
                    StringManager.DrawStringMid(aSpriteBatch, my8bitFont, "Goal", GameInfo.Path[GameInfo.Path.Count - 1].DestRect.Center.ToVector2(), Color.Black, 0.3f);
                }
            }
        }

        private void Misc(GameTime aGameTime, GameWindow aWindow)
        {
            myLoadButton.Update(aWindow);
            mySaveButton.Update(aWindow);
            myDeleteButton.Update(aWindow);
            myPathButton.Update(aWindow);
            myInfoButton.Update(aWindow);
            myWaveButton.Update(aWindow);

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

                if (tempTile.Item2)
                {
                    if (mySelectedTile != tempTile.Item1.TileType)
                    {
                        tempTile.Item1.TileType = mySelectedTile;
                        tempTile.Item1.DefineTileProperties();
                        tempTile.Item1.SetTextureEditor();
                    }
                }
            }
            if (KeyMouseReader.RightHold())
            {
                mySelectedTile = '-';
                myTile = -1;

                Tuple<Tile, bool> tempTile = Level.TileAtPos(Camera.ViewToWorld(KeyMouseReader.CurrentMouseState.Position.ToVector2()));

                if (tempTile.Item2)
                {
                    if (mySelectedTile != tempTile.Item1.TileType)
                    {
                        tempTile.Item1.TileType = '-';
                        tempTile.Item1.DefineTileProperties();
                        tempTile.Item1.SetTextureEditor();
                    }
                }
            }
        }

        private void PressLoadLevel(GameWindow aWindow)
        {
            myEditorState = EditorStates.isLoading;
            string[] tempLevelNames = FileReader.FindFileNames(GameInfo.FolderLevels);

            myLevels = new Button[tempLevelNames.Length];

            for (int i = 0; i < myLevels.Length; i++)
            {
                myLevels[i] = new Button(new Vector2((aWindow.ClientBounds.Width / 2) - 113, 64 + (i * 40)),
                    new Point(226, 32), null, 2, tempLevelNames[i], 0.4f);
                myLevels[i].LoadContent();
            }
        }
        private void PressSaveLevel(GameWindow aWindow)
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
                                Level.GetTiles[i, j].TileType = '-'; //Remove unneccesary path tiles
                            }
                        }
                    }
                }

                char[,] tempLevel = new char[
                    Level.MapSize.X / Level.TileSize.X,
                    Level.MapSize.Y / Level.TileSize.Y];

                for (int i = 0; i < tempLevel.GetLength(0); i++)
                {
                    for (int j = 0; j < tempLevel.GetLength(1); j++)
                    {
                        tempLevel[i, j] = Level.GetTiles[i, j].TileType;
                    }
                }

                myLevelNameForm.SetLevelInfo(tempLevel, myStartPosition, myGoalPosition);
                myLevelNameForm.Show();
            }
            else
            {
                StringManager.AddString(new DrawString(new Vector2(aWindow.ClientBounds.Width - 32, aWindow.ClientBounds.Height - 32),
                    Color.Red, true, 3.0f, 0.7f, 2, "No path"));
            }
        }
        private void PressDeleteLevel(GameWindow aWindow)
        {
            myEditorState = EditorStates.isDeleting;
            string[] tempLevelNames = FileReader.FindFileNames(GameInfo.FolderLevels);

            myLevels = new Button[tempLevelNames.Length - 1];

            int tempAddButton = 0;
            for (int i = 0; i < tempLevelNames.Length; i++)
            {
                if (tempLevelNames[i] != "Level_Template")
                {
                    myLevels[tempAddButton] = new Button(new Vector2((aWindow.ClientBounds.Width / 2) - 113, 64 + (tempAddButton * 40)),
                        new Point(226, 32), null, 2, tempLevelNames[i], 0.4f);
                    myLevels[tempAddButton].LoadContent();

                    tempAddButton++;
                }
            }
        }
        private void PressCreatePath(GameWindow aWindow)
        {
            myEditorState = EditorStates.isSelectingPath;
        }
        private void PressEditInfo(GameWindow aWindow)
        {
            myEditorState = EditorStates.isEditingInfo;
            myLevelInfoForm.Show();
        }
        private void PressEditWaves(GameWindow aWindow)
        {
            myEditorState = EditorStates.isEditingWaves;
            myLevelWaveForm.Show();
        }

        private void LoadLevel(GameWindow aWindow)
        {
            foreach (Button button in myLevels)
            {
                button.Update(aWindow);
                if (button.IsClicked())
                {
                    Level.LoadLevel(aWindow, new Point(64, 32), button.DisplayText);

                    LoadContent();

                    myEditorState = EditorStates.isEditing;
                    myLevels = null;
                }
            }
        }
        private void DeleteLevel(GameWindow aWindow)
        {
            foreach (Button button in myLevels)
            {
                button.Update(aWindow);
                if (button.IsClicked())
                {
                    Level.DeleteLevel(button.DisplayText);

                    myEditorState = EditorStates.isEditing;
                    myLevels = null;
                }
            }
        }
        private void CreatePath(GameWindow aWindow)
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
                    StringManager.AddString(new DrawString(new Vector2(aWindow.ClientBounds.Width - 32, aWindow.ClientBounds.Height - 32),
                        Color.DarkGreen, true, 3.0f, 0.7f, 2, "Path found!"));
                }
                else
                {
                    StringManager.AddString(new DrawString(new Vector2(aWindow.ClientBounds.Width - 32, aWindow.ClientBounds.Height - 32),
                        Color.Red, true, 3.0f, 0.7f, 2, "Path not found..."));
                }
            }
        }

        private void BackPress(GameWindow aWindow)
        {
            if (KeyMouseReader.KeyPressed(Keys.Escape))
            {
                if (!myLevelInfoForm.Visible && !myLevelNameForm.Visible && !myLevelWaveForm.Visible)
                {
                    switch (myEditorState)
                    {
                        case EditorStates.isEditing:
                            myGame.ChangeState(new MenuState(myGame, aWindow));
                            break;
                        default:
                            myEditorState = EditorStates.isEditing;
                            Camera.Initialize(aWindow, new Vector2(aWindow.ClientBounds.Width / 2, aWindow.ClientBounds.Height / 2), 5);
                            break;
                    }

                    myLevelInfoForm = new LevelInfo();
                    myLevelNameForm = new LevelName();
                    myLevelWaveForm = new LevelWave();
                }
                else
                {
                    if (myLevelInfoForm.Visible)
                    {
                        StringManager.AddString(new DrawString(new Vector2(aWindow.ClientBounds.Width - 32, aWindow.ClientBounds.Height - 32),
                            Color.Red, true, 3.0f, 0.7f, 2, "Please close info window"));
                    }
                    if (myLevelNameForm.Visible)
                    {
                        StringManager.AddString(new DrawString(new Vector2(aWindow.ClientBounds.Width - 32, aWindow.ClientBounds.Height - 32),
                            Color.Red, true, 3.0f, 0.7f, 2, "Please close save window"));
                    }
                    if (myLevelWaveForm.Visible)
                    {
                        StringManager.AddString(new DrawString(new Vector2(aWindow.ClientBounds.Width - 32, aWindow.ClientBounds.Height - 32),
                            Color.Red, true, 3.0f, 0.7f, 2, "Please close wave window"));
                    }
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
            myWaveButton.LoadContent();
            myPathButton.LoadContent();

            my8bitFont = ResourceManager.RequestFont("8-bit");
        }
    }
}
