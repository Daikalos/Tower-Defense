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

            ResourceManager.AddTexture("Null", this.Content.Load<Texture2D>("Sprites/null"));
            ResourceManager.AddTexture("Border", this.Content.Load<Texture2D>("Sprites/border"));

            for (int i = 0; i < 10; i++)
            {
                ResourceManager.AddTexture("Forest_Tile_" + Extensions.NumberFormat(i), this.Content.Load<Texture2D>("Sprites/Forest_Tileset/forest_tile_" + Extensions.NumberFormat(i)));
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
            GraphicsDevice.Clear(Color.RoyalBlue);

            mySpriteBatch.Begin();

            myGameState.Draw(mySpriteBatch, aGameTime, Window);

            mySpriteBatch.End();

            base.Draw(aGameTime);
        }
    }
}
