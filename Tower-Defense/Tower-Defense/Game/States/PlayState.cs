using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tower_Defense
{
    class PlayState : State
    {
        private SpriteFont my8bitFont;
        private Button myBackButton;
        private ShopManager myShop;

        public PlayState(MainGame aGame, GameWindow aWindow) : base(aGame)
        {
            EnemyManager.Initialize();

            GameInfo.Initialize();
            GameInfo.LoadHighScore(GameInfo.LevelName);

            Level.LoadLevel(aWindow, new Point(64, 32), GameInfo.LevelName);

            for (int i = 0; i < Level.GetTiles.GetLength(0); i++)
            {
                for (int j = 0; j < Level.GetTiles.GetLength(1); j++)
                {
                    if (Level.GetTiles[i, j].IsObstacle)
                    {
                        Depth.AddObject(Level.GetTiles[i, j]);
                    }
                }
            }

            Camera.Initialize(aWindow, new Vector2(aWindow.ClientBounds.Width / 2, aWindow.ClientBounds.Height / 2));

            myBackButton = new Button(new Vector2(aWindow.ClientBounds.Width - 128 - 16, aWindow.ClientBounds.Height - 48 - 16),
                    new Point(128, 48), Menu, 1, "MENU", 0.6f);

            Enemy tempEnemy = new Enemy(new Vector2(100, 100), new Point(64), 2, 3, 0);

            EnemyManager.AddEnemy(tempEnemy);

            Depth.AddObject(tempEnemy);
        }

        public override void Update(GameTime aGameTime, GameWindow aWindow)
        {
            if (!GameInfo.IsPaused)
            {
                Camera.MoveCamera();
                Level.Update();
                EnemyManager.Update();
            }
            else
            {
                myBackButton.Update(aWindow);
            }

            if (KeyMouseReader.KeyPressed(Keys.Escape))
            {
                GameInfo.IsPaused = !GameInfo.IsPaused;
            }
        }

        public override void Draw(SpriteBatch aSpriteBatch, GameTime aGameTime, GameWindow aWindow)
        {
            aSpriteBatch.End();

            aSpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend,
                SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise, null, Camera.TranslationMatrix);

            Level.DrawTiles(aSpriteBatch);
            Depth.Draw(aSpriteBatch, aGameTime);

            GameInfo.Draw(aSpriteBatch, aWindow, my8bitFont);
            StringManager.Draw(aSpriteBatch, my8bitFont);

            if (GameInfo.IsPaused)
            {
                StringManager.CameraDrawStringMid(aSpriteBatch, my8bitFont, "PAUSED", 
                    new Vector2(aWindow.ClientBounds.Width / 2, aWindow.ClientBounds.Height / 2), Color.Black, 1.5f);
                myBackButton.Draw(aSpriteBatch);
            }
        }

        private void Menu(GameWindow aWindow)
        {
            myGame.ChangeState(new MenuState(myGame, aWindow));
        }

        public override void LoadContent()
        {
            my8bitFont = ResourceManager.RequestFont("8-bit");

            Level.LoadContent();
            EnemyManager.SetTexture();
            myBackButton.LoadContent();
        }
    }
}