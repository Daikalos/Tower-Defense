using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using LilyPath;

namespace Tower_Defense
{
    class DeadState : State
    {
        private SpriteFont my8bitFont;

        public DeadState(MainGame aGame) : base(aGame)
        {

        }

        public override void Update(GameTime aGameTime, GameWindow aWindow)
        {
            if (KeyMouseReader.KeyPressed(Keys.Back))
            {
                myGame.ChangeState(new MenuState(myGame, aWindow));
            }
        }

        public override void Draw(SpriteBatch aSpriteBatch, GameTime aGameTime, GameWindow aWindow)
        {
            StringManager.DrawStringMid(aSpriteBatch, my8bitFont, "YOU DIED",
                new Vector2(aWindow.ClientBounds.Width / 2, (aWindow.ClientBounds.Height / 2) - 96), Color.LightSlateGray, 1.2f);
            StringManager.DrawStringMid(aSpriteBatch, my8bitFont, "Wave: " + GameInfo.Wave.ToString(),
                new Vector2(aWindow.ClientBounds.Width / 2, (aWindow.ClientBounds.Height / 2) - 46), Color.LightSlateGray, 0.7f);
            StringManager.DrawStringMid(aSpriteBatch, my8bitFont, "Score: " + GameInfo.Score.ToString(),
                new Vector2(aWindow.ClientBounds.Width / 2, (aWindow.ClientBounds.Height / 2) - 16), Color.LightSlateGray, 0.7f);
            StringManager.DrawStringLeft(aSpriteBatch, my8bitFont, "Press return to go back to menu",
                new Vector2(12, aWindow.ClientBounds.Height - 12), Color.LightSlateGray, 0.5f);
        }

        public override void LoadContent()
        {
            my8bitFont = ResourceManager.RequestFont("8-bit");
        }
    }
}
