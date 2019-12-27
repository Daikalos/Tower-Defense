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
        private UpgradeManager myUpgrade;

        public PlayState(MainGame aGame, GameWindow aWindow) : base(aGame)
        {
            EnemyManager.Initialize();
            TowerManager.Initialize();

            GameInfo.Initialize();
            GameInfo.LoadHighScore(GameInfo.LevelName);

            Level.LoadLevel(aWindow, new Point(64, 32), GameInfo.LevelName);

            for (int i = 0; i < Level.GetTiles.GetLength(0); i++)
            {
                for (int j = 0; j < Level.GetTiles.GetLength(1); j++)
                {
                    if (Level.GetTiles[i, j].TileType == '#') //Blocks
                    {
                        Depth.AddObject(Level.GetTiles[i, j]);
                    }
                }         
            }

            Camera.Initialize(aWindow, new Vector2(aWindow.ClientBounds.Width / 2, aWindow.ClientBounds.Height / 2), 5);

            myBackButton = new Button(
                new Vector2(aWindow.ClientBounds.Width - 128 - 16, aWindow.ClientBounds.Height - 48 - 16),
                new Point(128, 48), Menu, 1, "MENU", 0.6f, 1.0f, 1.03f);

            myShop = new ShopManager(
                new Vector2(aWindow.ClientBounds.Width, 0), 
                new Point(aWindow.ClientBounds.Width / 5, aWindow.ClientBounds.Height), 18.0f,
                myGame.GraphicsDevice, new Vector2(32, 0));

            myUpgrade = new UpgradeManager(
                new Vector2(0, aWindow.ClientBounds.Height),
                new Point(aWindow.ClientBounds.Width / 3, aWindow.ClientBounds.Height / 5), 18.0f,
                myGame.GraphicsDevice, new Vector2(0, 32));

            EnemyManager.AddEnemy(new Enemy_00(GameInfo.Path[0].GetCenter(), new Point(64)));
            EnemyManager.AddEnemy(new Enemy_01(GameInfo.Path[0].GetCenter(), new Point(64)));
            EnemyManager.AddEnemy(new Enemy_02(GameInfo.Path[0].GetCenter(), new Point(64)));
            EnemyManager.AddEnemy(new Enemy_03(GameInfo.Path[0].GetCenter(), new Point(64)));
        }

        public override void Update(GameTime aGameTime, GameWindow aWindow)
        {
            if (!GameInfo.IsPaused)
            {
                myShop.Update(aGameTime, aWindow);
                myUpgrade.Update(aGameTime, aWindow);

                Camera.MoveCamera(aGameTime);
                EnemyManager.Update(aGameTime);
                TowerManager.Update(aGameTime, myUpgrade);

                if (GameInfo.Health <= 0)
                {
                    GameInfo.SaveHighScore(GameInfo.LevelName);
                    myGame.ChangeState(new DeadState(myGame));
                }
            }
            else
            {
                myBackButton.Update(aWindow);
            }

            if (KeyMouseReader.KeyPressed(Keys.Space))
            {
                Camera.Initialize(aWindow, new Vector2(aWindow.ClientBounds.Width / 2, aWindow.ClientBounds.Height / 2), 5);
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
                SamplerState.AnisotropicClamp, null, null, null, Camera.TranslationMatrix);

            Level.DrawTiles(aSpriteBatch);
            Depth.Draw(aSpriteBatch, aGameTime);

            EnemyManager.Draw(aSpriteBatch);
            TowerManager.Draw(aSpriteBatch);
            StringManager.Draw(aSpriteBatch, my8bitFont);

            aSpriteBatch.End();

            aSpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend,
                SamplerState.AnisotropicClamp, null, null, null, Camera.TranslationMatrix); //Reset spritebatch to ignore depth

            if (GameInfo.IsPaused)
            {
                StringManager.CameraDrawStringMid(aSpriteBatch, my8bitFont, "PAUSED", 
                    new Vector2(aWindow.ClientBounds.Width / 2, aWindow.ClientBounds.Height / 2), Color.LightSlateGray, 1.5f);
                myBackButton.Draw(aSpriteBatch);
            }
            else
            {
                GameInfo.Draw(aSpriteBatch, aWindow, my8bitFont);

                myShop.Draw(aSpriteBatch);
                myUpgrade.Draw(aSpriteBatch);
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
            myBackButton.LoadContent();
            myShop.LoadContent();
            myUpgrade.LoadContent();
        }
    }
}