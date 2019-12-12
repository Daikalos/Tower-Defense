using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense
{
    class Button : StaticObject
    {
        private SpriteFont my8bitFont;
        private Rectangle
            myDrawRect,
            myOffset;
        private OnClick myIsClicked; //Uncertain about naming
        private string myDisplayText;
        private float myTextSize;

        public string DisplayText
        {
            get => myDisplayText;
        }

        public Button(Vector2 aPosition, Point aSize, OnClick aClickFunction, string aDisplayText, float aTextSize) : base(aPosition, aSize)
        {
            this.myIsClicked = aClickFunction;
            this.myDisplayText = aDisplayText;
            this.myTextSize = aTextSize;
        }

        public override void Update()
        {
            base.Update();

            myDrawRect = new Rectangle(
                (int)(Camera.TopLeftCorner.X + (float)DestRect.X / Camera.Zoom),
                (int)(Camera.TopLeftCorner.Y + (float)DestRect.Y / Camera.Zoom),
                (int)((float)DestRect.Width / Camera.Zoom),
                (int)((float)DestRect.Height / Camera.Zoom));

            myOffset = new Rectangle(
                (int)(myDrawRect.X - (mySize.X / 96)),
                (int)(myDrawRect.Y - (mySize.Y / 96)),
                (int)(myDrawRect.Width + (mySize.X / 48)),
                (int)(myDrawRect.Height + (mySize.Y / 48)));

            if (IsClicked())
            {
                myIsClicked?.Invoke();
            }
            if (IsHold())
            {
                myDrawRect = myOffset;
            }
        }

        public override void Draw(SpriteBatch aSpriteBatch)
        {
            aSpriteBatch.Draw(myTexture, myDrawRect, SourceRect, Color.White);
            StringManager.DrawStringMid(aSpriteBatch, my8bitFont, myDisplayText, myDestRect.Center.ToVector2(), Color.Black, myTextSize);
        }

        public bool IsClicked()
        {
            return
                KeyMouseReader.LeftClick() &&
                BoundingBox.Contains(KeyMouseReader.CurrentMouseState.Position);
        }
        public bool IsHold()
        {
            return BoundingBox.Contains(KeyMouseReader.CurrentMouseState.Position);
        }

        public delegate void OnClick();

        public static void Back(MainGame aGame, GameWindow aWindow)
        {
            aGame.ChangeState(new MenuState(aGame, aWindow));
        }
        public static void Editor(MainGame aGame, GameWindow aWindow)
        {
            aGame.ChangeState(new EditorState(aGame, aWindow));
        }
        public static void Leaderboard(MainGame aGame)
        {
            aGame.ChangeState(new LeaderboardState(aGame));
        }
        public static void Exit(MainGame aGame)
        {
            aGame.Exit();
        }

        public void LoadContent()
        {
            SetTexture("Border");
            my8bitFont = ResourceManager.RequestFont("8-bit");
        }
    }
}
