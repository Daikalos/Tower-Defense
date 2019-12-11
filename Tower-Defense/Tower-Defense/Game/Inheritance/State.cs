using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense
{
    public abstract class State
    {
        protected MainGame myGame;

        public State(MainGame aGame)
        {
            this.myGame = aGame;
            GameInfo.IsPaused = false;
        }

        public abstract void Update(GameTime aGameTime, GameWindow aWindow);

        public abstract void Draw(SpriteBatch aSpriteBatch, GameTime aGameTime, GameWindow aWindow);

        public abstract void LoadContent();
    }
}
