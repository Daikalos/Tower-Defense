using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense
{
    class ShopManager : DynamicObject
    {
        private SpriteFont my8bitFont;
        private Button[] myBuyOptions;
        private Vector2[] myBuyOptionsOffset;
        private Vector2 
            myShowPosition,
            myHidePosition;
        private int mySelectedTower;
        private string[] myBuyPrice;

        public ShopManager(Vector2 aPosition, Point aSize, Vector2 aOffset, float aSpeed) : base(aPosition, aSize, aSpeed)
        {
            this.myBuyOptions = new Button[]
            {
                new Button(aPosition, new Point(118, 92), null, -1, string.Empty, 0.0f, 1.10f),
                new Button(aPosition, new Point(118, 92), null, -1, string.Empty, 0.0f, 1.10f),
                new Button(aPosition, new Point(118, 92), null, -1, string.Empty, 0.0f, 1.10f),
            };

            this.myHidePosition = aPosition - aOffset;
            this.myShowPosition = aPosition - aSize.ToVector2();

            this.myBuyOptionsOffset = new Vector2[myBuyOptions.Length];
            this.myBuyPrice = new string[myBuyOptions.Length];
            this.mySelectedTower = -1;

            for (int i = 0; i < myBuyOptions.Length; i++)
            {
                myBuyOptionsOffset[i] = new Vector2(50 + (137 * (i % 2)), 24 + (147 * (i / 2))); //Values are from shop menu buy option offsets

                switch (i) //Because price is individual, switch or similiar method must be used
                {
                    case 0:
                        myBuyPrice[i] = "$200";
                        break;
                    case 1:
                        myBuyPrice[i] = "$325";
                        break;
                    case 2:
                        myBuyPrice[i] = "$615";
                        break;
                }
            }
        }

        public void Update(GameTime aGameTime, GameWindow aWindow)
        {
            MenuSlide(aGameTime);

            for (int i = 0; i < myBuyOptions.Length; i++)
            {
                myBuyOptions[i].Update(aWindow);
                myBuyOptions[i].Position = myPosition + myBuyOptionsOffset[i];

                if (myBuyOptions[i].IsClicked())
                {
                    mySelectedTower = i;
                }
            }

            if (KeyMouseReader.RightClick() && mySelectedTower != -1)
            {
                mySelectedTower = -1;
            }
        }

        public override void Draw(SpriteBatch aSpriteBatch)
        {
            aSpriteBatch.End();

            aSpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend,
                SamplerState.AnisotropicClamp, null, null, null, Camera.TranslationMatrix); //Reset spritebatch to ignore depth

            aSpriteBatch.Draw(myTexture, Camera.TopLeftCorner + myPosition / Camera.Zoom, 
                SourceRect, Color.White, 0.0f, Vector2.Zero, 1.0f / Camera.Zoom, SpriteEffects.None, 0.0f);

            Array.ForEach(myBuyOptions, o => o.Draw(aSpriteBatch));

            for (int i = 0; i < myBuyPrice.Length; i++)
            {
                StringManager.CameraDrawStringLeft(aSpriteBatch, my8bitFont, myBuyPrice[i], 
                    new Vector2(myBuyOptions[i].Position.X + 2, myBuyOptions[i].Position.Y + 105), Color.MediumSeaGreen, 0.5f);
            }

            if (mySelectedTower >= 0 && mySelectedTower < myBuyOptions.Length)
            {
                aSpriteBatch.Draw(myBuyOptions[mySelectedTower].Texture, Camera.ViewToWorld(KeyMouseReader.CurrentMouseState.Position.ToVector2()), 
                    null, Color.White, 0.0f, Vector2.Zero, 1.0f / Camera.Zoom, SpriteEffects.None, 0.0f);
            }
        }

        private void MenuSlide(GameTime aGameTime)
        {
            if (KeyMouseReader.CurrentMouseState.Position.X > myPosition.X)
            {
                if (myPosition.X - myCurrentSpeed > myShowPosition.X)
                {
                    myCurrentSpeed = mySpeed * 60 * (float)aGameTime.ElapsedGameTime.TotalSeconds;
                    myPosition.X -= myCurrentSpeed;
                }
                else
                {
                    myPosition.X = myShowPosition.X;
                }
            }
            if (KeyMouseReader.CurrentMouseState.Position.X < myPosition.X)
            {
                if (myPosition.X + myCurrentSpeed < myHidePosition.X)
                {
                    myCurrentSpeed = mySpeed * 60 * (float)aGameTime.ElapsedGameTime.TotalSeconds;
                    myPosition.X += myCurrentSpeed;
                }
                else
                {
                    myPosition.X = myHidePosition.X;
                }
            }
        }

        public void LoadContent()
        {
            my8bitFont = ResourceManager.RequestFont("8-bit");

            myTexture = ResourceManager.RequestTexture("Shop_Menu");
            mySourceRect = new Rectangle(0, 0, myTexture.Width, myTexture.Height);

            for (int i = 0; i < myBuyOptions.Length; i++)
            {
                myBuyOptions[i].SetTexture("Buy_Tower_" + Extensions.NumberFormat(i));
            }
        }
    }
}
