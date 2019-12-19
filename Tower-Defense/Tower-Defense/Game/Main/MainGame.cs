using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tower_Defense
{
    public class MainGame : Game
    {
        GraphicsDeviceManager myGraphics;
        SpriteBatch mySpriteBatch;

        State myGameState;

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

            ResourceManager.Initialize();

            StringManager.Initialize();

            GameInfo.Initialize();
            GameInfo.FolderLevels = "../../../../Levels/Levels/";
            GameInfo.FolderLevelsInfo = "../../../../Levels/Levels_Info/";
            GameInfo.FolderHighScores = "../../../../Levels/HighScores/";

            myGameState = new MenuState(this, Window);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            mySpriteBatch = new SpriteBatch(GraphicsDevice);

            ResourceManager.AddFont("8-bit", this.Content.Load<SpriteFont>("Fonts/8bit"));

            ResourceManager.AddTexture("Null", this.Content.Load<Texture2D>("Sprites/other/null"));

            ResourceManager.AddTexture("Border_Long", this.Content.Load<Texture2D>("Sprites/Main/border_long"));
            ResourceManager.AddTexture("Border_Short", this.Content.Load<Texture2D>("Sprites/Main/border_short"));
            ResourceManager.AddTexture("Border_Small", this.Content.Load<Texture2D>("Sprites/Main/border_small"));

            ResourceManager.AddTexture("Healthbar", this.Content.Load<Texture2D>("Sprites/Main/healthbar"));

            for (int i = 0; i < 4; i++)
            {
                ResourceManager.AddTexture("Enemy_" + Extensions.NumberFormat(i), 
                    this.Content.Load<Texture2D>("Sprites/Main/enemy_" + Extensions.NumberFormat(i)));
            }

            for (int i = 0; i < 10; i++)
            {
                ResourceManager.AddTexture("Forest_Tile_" + Extensions.NumberFormat(i), 
                    this.Content.Load<Texture2D>("Sprites/Forest_Tileset/forest_tile_" + Extensions.NumberFormat(i)));
            }

            for (int i = 0; i < 10; i++)
            {
                ResourceManager.AddTexture("Deep-Forest_Tile_" + Extensions.NumberFormat(i), 
                    this.Content.Load<Texture2D>("Sprites/Deep-Forest_Tileset/deep-forest_tile_" + Extensions.NumberFormat(i)));
            }

            for (int i = 0; i < 10; i++)
            {
                ResourceManager.AddTexture("Snow_Tile_" + Extensions.NumberFormat(i), 
                    this.Content.Load<Texture2D>("Sprites/Snow_Tileset/snow_tile_" + Extensions.NumberFormat(i)));
            }

            for (int i = 0; i < 10; i++)
            {
                ResourceManager.AddTexture("Barren_Tile_" + Extensions.NumberFormat(i),
                    this.Content.Load<Texture2D>("Sprites/Barren_Tileset/barren_tile_" + Extensions.NumberFormat(i)));
            }

            myGameState.LoadContent();
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime aGameTime)
        {
            KeyMouseReader.Update();

            StringManager.Update(aGameTime);

            myGameState.Update(aGameTime, Window);

            base.Update(aGameTime);
        }

        protected override void Draw(GameTime aGameTime)
        {
            GraphicsDevice.Clear(new Color(30, 30, 90));

            mySpriteBatch.Begin();

            myGameState.Draw(mySpriteBatch, aGameTime, Window);

            mySpriteBatch.End();

            base.Draw(aGameTime);
        }
    }
}
