using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense
{
    class ShopManager : DynamicObject
    {
        private Vector2 
            myShowPosition,
            myHidePosition;

        public ShopManager(Vector2 aPosition, Point aSize, Vector2 aOffset, float aSpeed) : base(aPosition, aSize, aSpeed)
        {
            this.myHidePosition = aPosition - aOffset;
            this.myShowPosition = aPosition - aSize.ToVector2();
        }

        public void Update(GameTime aGameTime, GameWindow aWindow)
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

        public override void Draw(SpriteBatch aSpriteBatch)
        {
            aSpriteBatch.Draw(myTexture, Camera.TopLeftCorner + myPosition / Camera.Zoom, 
                SourceRect, Color.White, 0.0f, Vector2.Zero, 1.0f / Camera.Zoom, SpriteEffects.None, 0.0f);
        }
    }
}
