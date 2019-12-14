using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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

            Camera.Initialize(aWindow, new Vector2(aWindow.ClientBounds.Width / 2, aWindow.ClientBounds.Height / 2));

            myButtons = new Button[]
            {
                new Button(
                    new Vector2((aWindow.ClientBounds.Width / 2) - 226, (aWindow.ClientBounds.Height / 2) - 32 - 90),
                    new Point(452, 64),
                    null,
                    "PLAY", 1.1f),
                new Button(
                    new Vector2((aWindow.ClientBounds.Width / 2) - 226, (aWindow.ClientBounds.Height / 2) - 32 - 10),
                    new Point(452, 64),
                    new Button.OnClick(() => Button.Editor(aGame, aWindow)),
                    "EDITOR", 1.1f),
                new Button(
                    new Vector2((aWindow.ClientBounds.Width / 2) - 226, (aWindow.ClientBounds.Height / 2) - 32 + 70),
                    new Point(452, 64),
                    new Button.OnClick(() => Button.Leaderboard(aGame)),
                    "LEADERBOARD", 1.1f),
                new Button(
                    new Vector2((aWindow.ClientBounds.Width / 2) - 226, (aWindow.ClientBounds.Height / 2) - 32 + 150),
                    new Point(452, 64),
                    new Button.OnClick(() => Button.Exit(aGame)),
                    "EXIT", 1.1f),
            };
        }

        public override void Update(GameTime aGameTime, GameWindow aWindow)
        {
            if (!myLoadLevel)
            {
                Array.ForEach(myButtons, b => b.Update());

                Play(aWindow);
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
                new Color(210, 210, 210), 1.5f);

            if (!myLoadLevel)
            {
                Array.ForEach(myButtons, b => b.Draw(aSpriteBatch));
            }
            else
            {
                Array.ForEach(myLevels, b => b.Draw(aSpriteBatch));
                StringManager.DrawStringLeft(aSpriteBatch, my8bitFont, "Press escape to go back to menu", 
                    new Vector2(16, aWindow.ClientBounds.Height - 16), Color.Black, 0.4f);
            }
        }

        private void Play(GameWindow aWindow)
        {
            if (myButtons[0].IsClicked())
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
                            new Point(226, 32), null, tempLevelNames[i], 0.4f);
                        myLevels[tempAddLevel].LoadContent();

                        tempAddLevel++;
                    }
                }
            }
        }

        private void LoadLevel(GameWindow aWindow)
        {
            foreach (Button button in myLevels)
            {
                button.Update();
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
