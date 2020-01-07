using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense
{
    public class MainGame : Game
    {
        private GraphicsDeviceManager myGraphics;
        private SpriteBatch mySpriteBatch;

        private State myGameState;

        public void ChangeState(State aNewState)
        {
            myGameState = aNewState;
            myGameState.LoadContent();
        }

        public MainGame()
        {
            myGraphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            myGraphics.PreferredBackBufferWidth = 1600;
            myGraphics.PreferredBackBufferHeight = 900;
            myGraphics.ApplyChanges();

            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromSeconds(1.0f / 60.0f); //60 fps
            MaxElapsedTime = TargetElapsedTime;

            IsMouseVisible = true;

            ResourceManager.Initialize();
            GameInfo.Initialize();
            Background.Initialize(12.0f);
            Camera.Initialize(Window, 15);
            UserInterface.Initialize(Vector2.Zero, new Point(Window.ClientBounds.Width, Window.ClientBounds.Height), GraphicsDevice);

            myGameState = new MenuState(this, Window);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            mySpriteBatch = new SpriteBatch(GraphicsDevice);

            ResourceManager.AddFont("8-bit", this.Content.Load<SpriteFont>("Fonts/8bit"));

            ResourceManager.AddTexture("Null", this.Content.Load<Texture2D>("Sprites/other/null"));
            ResourceManager.AddTexture("Empty", this.Content.Load<Texture2D>("Sprites/other/empty"));

            ResourceManager.AddTexture("Border_Long", this.Content.Load<Texture2D>("Sprites/Main/border_long"));
            ResourceManager.AddTexture("Border_Short", this.Content.Load<Texture2D>("Sprites/Main/border_short"));
            ResourceManager.AddTexture("Border_Small", this.Content.Load<Texture2D>("Sprites/Main/border_small"));
            ResourceManager.AddTexture("Border_Upgrade", this.Content.Load<Texture2D>("Sprites/Main/border_upgrade"));

            ResourceManager.AddTexture("Play_Button", this.Content.Load<Texture2D>("Sprites/Main/play_button"));
            ResourceManager.AddTexture("SpeedUp_Button", this.Content.Load<Texture2D>("Sprites/Main/speedup_button"));

            ResourceManager.AddTexture("Background", this.Content.Load<Texture2D>("Sprites/Main/background"));
            ResourceManager.AddTexture("Shop_Menu", this.Content.Load<Texture2D>("Sprites/Main/shop_menu"));
            ResourceManager.AddTexture("Upgrade_Menu", this.Content.Load<Texture2D>("Sprites/Main/upgrade_menu"));

            for (int i = 0; i < 6; i++)
            {
                ResourceManager.AddTexture("Tile_" + Extensions.NumberFormat(i), 
                    this.Content.Load<Texture2D>("Sprites/Tileset/tile_" + Extensions.NumberFormat(i)));
            }

            for (int i = 0; i < 4; i++)
            {
                ResourceManager.AddTexture("Enemy_" + Extensions.NumberFormat(i), 
                    this.Content.Load<Texture2D>("Sprites/Main/enemy_" + Extensions.NumberFormat(i)));
            }
            ResourceManager.AddTexture("Healthbar", this.Content.Load<Texture2D>("Sprites/Main/healthbar"));

            for (int i = 0; i < 3; i++)
            {
                ResourceManager.AddTexture("Tower_" + Extensions.NumberFormat(i),
                    this.Content.Load<Texture2D>("Sprites/Main/tower_" + Extensions.NumberFormat(i)));
                ResourceManager.AddTexture("Tower_" + Extensions.NumberFormat(i) + "_Icon",
                    this.Content.Load<Texture2D>("Sprites/Main/tower_" + Extensions.NumberFormat(i) + "_icon"));
            }

            for (int i = 0; i < 1; i++)
            {
                ResourceManager.AddTexture("Explosion_" + Extensions.NumberFormat(i),
                    this.Content.Load<Texture2D>("Sprites/Main/explosion_" + Extensions.NumberFormat(i)));
            }

            Background.LoadContent();
            myGameState.LoadContent();
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime aGameTime)
        {
            KeyMouseReader.Update();
            Background.Update();

            myGameState.Update(aGameTime, Window);

            base.Update(aGameTime);
        }

        protected override void Draw(GameTime aGameTime)
        {
            GraphicsDevice.Clear(new Color(30, 30, 90));

            mySpriteBatch.Begin();

            Background.Draw(mySpriteBatch, aGameTime, Window);

            myGameState.Draw(mySpriteBatch, aGameTime, Window);

            mySpriteBatch.End();

            base.Draw(aGameTime);
        }
    }
}
