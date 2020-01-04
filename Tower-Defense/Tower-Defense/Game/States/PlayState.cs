using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tower_Defense
{
    class PlayState : State
    {
        private SpriteFont my8bitFont;
        private Button
            myBackButton,
            myPlayButton,
            mySpeedUpButton;
        private Minimap myMinimap;
        private ShopManager myShop;
        private UpgradeManager myUpgrade;

        public PlayState(MainGame aGame, GameWindow aWindow) : base(aGame)
        {
            EnemyManager.Initialize();
            TowerManager.Initialize();

            GameInfo.Initialize();

            ParticleManager.Initialize(myGame.GraphicsDevice);

            Level.LoadLevel(aWindow, new Point(64, 32), GameInfo.LevelName);

            SpawnManager.Initialize();

            UserInterface.DefineRenderTarget(DrawHUD);

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

            this.myBackButton = new Button(
                new Vector2(aWindow.ClientBounds.Width - 128 - 16, aWindow.ClientBounds.Height - 48 - 16),
                new Point(128, 48), Menu, 1, "MENU", 0.6f, 1.0f, 1.03f);

            this.myPlayButton = new Button(
                new Vector2(32, (aWindow.ClientBounds.Height / 4) + 256),
                new Point(64, 64), Play, -1, string.Empty, 0.0f, 1.0f, 1.03f);

            this.mySpeedUpButton = new Button(
                new Vector2(156, (aWindow.ClientBounds.Height / 4) + 256),
                new Point(128, 64), SpeedUp, -1, string.Empty, 0.0f, 1.0f, 1.03f);

            this.myMinimap = new Minimap(Vector2.Zero,
                new Point(aWindow.ClientBounds.Width / 4, aWindow.ClientBounds.Height / 4), myGame.GraphicsDevice);

            this.myShop = new ShopManager(
                new Vector2(aWindow.ClientBounds.Width, 0),
                new Point(aWindow.ClientBounds.Width / 5, aWindow.ClientBounds.Height), 18.0f,
                myGame.GraphicsDevice, new Vector2(32, 0));

            this.myUpgrade = new UpgradeManager(
                new Vector2(0, aWindow.ClientBounds.Height),
                new Point(aWindow.ClientBounds.Width / 3, aWindow.ClientBounds.Height / 5), 18.0f,
                myGame.GraphicsDevice, new Vector2(0, 32));
        }

        public override void Update(GameTime aGameTime, GameWindow aWindow)
        {
            if (!GameInfo.IsPaused)
            {
                SpawnManager.Update(aGameTime);

                myShop.Update(aGameTime, aWindow);
                myUpgrade.Update(aGameTime, aWindow);

                myPlayButton.Update(aWindow);
                mySpeedUpButton.Update(aWindow);

                Camera.MoveCamera(aGameTime);
                EnemyManager.Update(aGameTime);
                TowerManager.Update(aGameTime, myUpgrade);

                ParticleManager.Update(aGameTime);

                if (GameInfo.Wave >= GameInfo.TotalWaves)
                {
                    GameInfo.LoadHighScore(GameInfo.LevelName);
                    GameInfo.SaveHighScore(GameInfo.LevelName);

                    myGame.ChangeState(new WinState(myGame));
                }
                if (GameInfo.Health <= 0)
                {
                    myGame.ChangeState(new DeadState(myGame));
                }
            }
            else
            {
                myBackButton.Update(aWindow);
            }

            if (KeyMouseReader.KeyPressed(Keys.Space))
            {
                Camera.Reset();
            }
            if (KeyMouseReader.KeyPressed(Keys.Escape))
            {
                GameInfo.IsPaused = !GameInfo.IsPaused;
            }
        }

        public override void Draw(SpriteBatch aSpriteBatch, GameTime aGameTime, GameWindow aWindow)
        {
            myMinimap.SetRenderTarget(aGameTime, this);
            UserInterface.SetRenderTarget(aGameTime, aWindow);

            DrawLevel(aSpriteBatch, aGameTime);
            UserInterface.Draw(aSpriteBatch);
        }

        public void DrawLevel(SpriteBatch aSpriteBatch, GameTime aGameTime)
        {
            aSpriteBatch.End();

            aSpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend,
                SamplerState.AnisotropicClamp, null, null, null, Camera.TranslationMatrix);

            Level.DrawTiles(aSpriteBatch);
            Depth.Draw(aSpriteBatch, aGameTime);

            EnemyManager.Draw(aSpriteBatch); //Draws healthbar
            TowerManager.Draw(aSpriteBatch); //Not needed because depth draws tower

            aSpriteBatch.End();

            aSpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend,
                SamplerState.AnisotropicClamp, null, null, null, Camera.TranslationMatrix); //Reset spritebatch to ignore depth

            ParticleManager.Draw(aSpriteBatch, aGameTime);
        }
        private void DrawHUD(SpriteBatch aSpriteBatch, GameTime aGameTime, GameWindow aWindow)
        {
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

                myPlayButton.Draw(aSpriteBatch);
                mySpeedUpButton.Draw(aSpriteBatch);

                myMinimap.Draw(aSpriteBatch);

                aSpriteBatch.End();

                aSpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend,
                    SamplerState.AnisotropicClamp, null, null, null, Camera.TranslationMatrix);

                myUpgrade.Draw(aSpriteBatch);
                myShop.Draw(aSpriteBatch);
            }
        }

        private void Play(GameWindow aWindow)
        {
            SpawnManager.InitiateWave();
        }
        private void SpeedUp(GameWindow aWindow)
        {
            GameInfo.GameSpeed = (GameInfo.GameSpeed != 4) ? GameInfo.GameSpeed = 4 : GameInfo.GameSpeed = 1;
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

            myPlayButton.SetTexture("Play_Button");
            mySpeedUpButton.SetTexture("SpeedUp_Button");
        }
    }
}