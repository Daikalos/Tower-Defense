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
            isSmall,
            isUpgrade
        }

        private SpriteFont my8bitFont;
        private OnClick myIsClicked; //Uncertain about naming
        private ButtonType myButtonType;
        private string myDisplayText;
        private float 
            myTextSize,
            myScale,
            myUpScale,
            mySaveScale;

        public string DisplayText
        {
            get => myDisplayText;
        }

        public Button(Vector2 aPosition, Point aSize, OnClick aClickFunction, int aButtonType, string aDisplayText, float aTextSize, float aScale, float aUpScale) : base(aPosition, aSize)
        {
            this.myIsClicked = aClickFunction;
            this.myButtonType = (ButtonType)aButtonType;
            this.myDisplayText = aDisplayText;
            this.myTextSize = aTextSize;
            this.myScale = aScale;
            this.myUpScale = aUpScale;

            this.mySaveScale = myScale;
        }

        public void Update(GameWindow aWindow)
        {
            base.Update();

            myScale = mySaveScale;

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
            aSpriteBatch.Draw(myTexture, Camera.TopLeftCorner + new Vector2(myPosition.X + myOrigin.X * mySaveScale, myPosition.Y + myOrigin.Y * mySaveScale) / Camera.Zoom, 
                SourceRect, Color.White, 0.0f, myOrigin, myScale / Camera.Zoom, SpriteEffects.None, 0.0f);
            StringManager.CameraDrawStringMid(aSpriteBatch, my8bitFont, myDisplayText, myDestRect.Center.ToVector2(), new Color(59, 76, 93), myTextSize);
        }

        public bool IsClicked()
        {
            return
                KeyMouseReader.LeftClick() &&
                BoundingBox.Contains(KeyMouseReader.MousePos);
        }
        public bool IsHold()
        {
            return BoundingBox.Contains(KeyMouseReader.MousePos);
        }

        public delegate void OnClick(GameWindow aWindow);

        public void LoadContent()
        {
            my8bitFont = ResourceManager.RequestFont("8-bit");

            switch (myButtonType)
            {
                case ButtonType.isLong:
                    base.SetTexture("Border_Long");
                    break;
                case ButtonType.isShort:
                    base.SetTexture("Border_Short");
                    break;
                case ButtonType.isSmall:
                    base.SetTexture("Border_Small");
                    break;
                case ButtonType.isUpgrade:
                    base.SetTexture("Border_Upgrade");
                    break;
            }

            SetOrigin(new Point(1));
        }

        public override void SetTexture(string aName)
        {
            base.SetTexture(aName);

            my8bitFont = ResourceManager.RequestFont("8-bit");
            SetOrigin(new Point(1));
        }
    }
}
