using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tower_Defense
{
    class PlayState : State
    {
        private SpriteFont my8bitFont;
        private Button myBackButton;

        public PlayState(MainGame aGame, GameWindow aWindow) : base(aGame)
        {
            //EnemyManager.Initialize();

            GameInfo.Initialize(0.5f);
            GameInfo.LoadHighScore(GameInfo.LevelName);

            Level.LoadLevel(aWindow, new Point(64, 32), GameInfo.LevelName);

            Camera.Initialize(aWindow, new Vector2(aWindow.ClientBounds.Width / 2, aWindow.ClientBounds.Height / 2));

            myBackButton = new Button(new Vector2(aWindow.ClientBounds.Width - 128 - 16, aWindow.ClientBounds.Height - 48 - 16),
                    new Point(128, 48),
                    new Button.OnClick(() => Button.Back(aGame, aWindow)),
                    "MENU", 0.6f);
        }

        public override void Update(GameTime aGameTime, GameWindow aWindow)
        {
            if (!GameInfo.IsPaused)
            {
                Camera.MoveCamera();
                Level.Update();
                //EnemyManager.Update(aGameTime);
                GameInfo.Update(aGameTime);
            }
            else
            {
                myBackButton.Update();
            }

            if (KeyMouseReader.KeyPressed(Keys.Escape))
            {
                GameInfo.IsPaused = !GameInfo.IsPaused;
            }
        }

        public override void Draw(SpriteBatch aSpriteBatch, GameTime aGameTime, GameWindow aWindow)
        {
            aSpriteBatch.End();

            aSpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend,
                null, null, null, null, Camera.TranslationMatrix);

            Level.DrawTiles(aSpriteBatch, aGameTime);
            //EnemyManager.Draw(aSpriteBatch, aGameTime);
            GameInfo.Draw(aSpriteBatch, aWindow, my8bitFont);

            if (GameInfo.IsPaused)
            {
                StringManager.DrawStringMid(aSpriteBatch, my8bitFont, "PAUSED", 
                    new Vector2(aWindow.ClientBounds.Width / 2, aWindow.ClientBounds.Height / 2), Color.Black, 1.5f);
                myBackButton.Draw(aSpriteBatch);
            }
        }

        public override void LoadContent()
        {
            my8bitFont = ResourceManager.RequestFont("8-bit");

            Level.LoadContent();
            myBackButton.LoadContent();
        }
    }
}