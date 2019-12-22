using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense
{
    class Button : StaticObject
    {
        enum ButtonType
        {
            isLong,
            isShort,
            isSmall
        }

        private SpriteFont my8bitFont;
        private OnClick myIsClicked; //Uncertain about naming
        private ButtonType myButtonType;
        private string myDisplayText;
        private float 
            myTextSize,
            myScale,
            myUpScale,
            myResetScale;

        public string DisplayText
        {
            get => myDisplayText;
        }

        public Button(Vector2 aPosition, Point aSize, OnClick aClickFunction, int aButtonType, string aDisplayText, float aTextSize) : base(aPosition, aSize)
        {
            this.myIsClicked = aClickFunction;
            this.myButtonType = (ButtonType)aButtonType;
            this.myDisplayText = aDisplayText;
            this.myTextSize = aTextSize;

            this.myScale = 1.0f;
            this.myUpScale = 1.03f;
            this.myResetScale = myScale;
        }

        public void Update(GameWindow aWindow)
        {
            base.Update();

            myScale = myResetScale;

            if (IsClicked())
            {
                myIsClicked?.Invoke(aWindow);
            }
            if (IsHold())
            {
                myScale = myUpScale;
            }
        }

        public override void Draw(SpriteBatch aSpriteBatch)
        {
            aSpriteBatch.Draw(myTexture, Camera.TopLeftCorner + new Vector2(myPosition.X + myOrigin.X, myPosition.Y + myOrigin.Y) / Camera.Zoom, 
                SourceRect, Color.White, 0.0f, myOrigin, myScale / Camera.Zoom, SpriteEffects.None, 0.0f);
            StringManager.CameraDrawStringMid(aSpriteBatch, my8bitFont, myDisplayText, myDestRect.Center.ToVector2(), new Color(79, 96, 113), myTextSize);
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

        public delegate void OnClick(GameWindow aWindow);

        public void LoadContent()
        {
            my8bitFont = ResourceManager.RequestFont("8-bit");

            switch (myButtonType)
            {
                case ButtonType.isLong:
                    SetTexture("Border_Long");
                    break;
                case ButtonType.isShort:
                    SetTexture("Border_Short");
                    break;
                case ButtonType.isSmall:
                    SetTexture("Border_Small");
                    break;
            }

            SetOrigin(new Point(1));
        }
    }
}
