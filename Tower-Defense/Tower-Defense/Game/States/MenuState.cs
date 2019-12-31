using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using LilyPath;

namespace Tower_Defense
{
    class MenuState : State
    {
        private SpriteFont my8bitFont;
        private Button[] 
            myButtons,
            myLevels;
        private bool myLoadLevel;

        public MenuState(MainGame aGame, GameWindow aWindow) : base(aGame)
        {
            aGame.IsMouseVisible = true;

            GameInfo.Initialize();

            Depth.Initialize();

            Camera.Reset();

            myButtons = new Button[]
            {
                new Button(
                    new Vector2((aWindow.ClientBounds.Width / 2) - 226, (aWindow.ClientBounds.Height / 2) - 32 - 90),
                    new Point(452, 64), Play, 0, "PLAY", 1.1f, 1.0f, 1.03f),
                new Button(
                    new Vector2((aWindow.ClientBounds.Width / 2) - 226, (aWindow.ClientBounds.Height / 2) - 32 - 10),
                    new Point(452, 64), Editor, 0, "EDITOR", 1.1f, 1.0f, 1.03f),
                new Button(
                    new Vector2((aWindow.ClientBounds.Width / 2) - 226, (aWindow.ClientBounds.Height / 2) - 32 + 70),
                    new Point(452, 64), Leaderboard, 0, "LEADERBOARD", 1.1f, 1.0f, 1.03f),
                new Button(
                    new Vector2((aWindow.ClientBounds.Width / 2) - 226, (aWindow.ClientBounds.Height / 2) - 32 + 150),
                    new Point(452, 64), Exit, 0, "EXIT", 1.1f, 1.0f, 1.03f),
            };
        }

        public override void Update(GameTime aGameTime, GameWindow aWindow)
        {
            if (!myLoadLevel)
            {
                Array.ForEach(myButtons, b => b.Update(aWindow));
            }
            else
            {
                LoadLevel(aWindow);

                if (KeyMouseReader.KeyPressed(Keys.Escape))
                {
                    myLoadLevel = false;
                    myLevels = null;
                }
            }
        }

        public override void Draw(SpriteBatch aSpriteBatch, GameTime aGameTime, GameWindow aWindow)
        {
            StringManager.DrawStringMid(aSpriteBatch, my8bitFont, "Tower-Defense", 
                new Vector2((aWindow.ClientBounds.Width / 2), (aWindow.ClientBounds.Height / 2) - 200),
                Color.LightSlateGray, 1.5f);

            if (!myLoadLevel)
            {
                Array.ForEach(myButtons, b => b.Draw(aSpriteBatch));
            }
            else
            {
                Array.ForEach(myLevels, b => b.Draw(aSpriteBatch));
                StringManager.DrawStringLeft(aSpriteBatch, my8bitFont, "Press escape to go back to menu", 
                    new Vector2(16, aWindow.ClientBounds.Height - 16), Color.DarkSlateGray, 0.4f);
            }
        }

        private void Play(GameWindow aWindow)
        {
            myLoadLevel = true;
            string[] tempLevelNames = FileReader.FindFileNames(GameInfo.FolderLevels);

            myLevels = new Button[tempLevelNames.Length - 1];

            int tempAddLevel = 0;
            for (int i = 0; i < tempLevelNames.Length; i++)
            {
                if (tempLevelNames[i] != "Level_Template")
                {
                    myLevels[tempAddLevel] = new Button(new Vector2((aWindow.ClientBounds.Width / 2) - 113, (aWindow.ClientBounds.Height / 2) - 64 - 90 + (tempAddLevel * 40)),
                        new Point(226, 32), null, 2, tempLevelNames[i], 0.4f, 1.0f, 1.03f);
                    myLevels[tempAddLevel].LoadContent();

                    tempAddLevel++;
                }
            }
        }

        private void Editor(GameWindow aWindow)
        {
            myGame.ChangeState(new EditorState(myGame, aWindow));
        }

        private void Leaderboard(GameWindow aWindow)
        {
            myGame.ChangeState(new LeaderboardState(myGame));
        }

        private void Exit(GameWindow aWindow)
        {
            myGame.Exit();
        }

        private void LoadLevel(GameWindow aWindow)
        {
            foreach (Button button in myLevels)
            {
                button.Update(aWindow);
                if (button.IsClicked())
                {
                    string tempLevel = button.DisplayText;

                    GameInfo.LevelName = tempLevel;
                    myGame.ChangeState(new PlayState(myGame, aWindow));

                    myLoadLevel = false;
                    myLevels = null;
                }
            }
        }

        public override void LoadContent()
        {
            my8bitFont = ResourceManager.RequestFont("8-bit");
            Array.ForEach(myButtons, b => b.LoadContent());
        }
    }
}
