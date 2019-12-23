using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense
{
    static class Background
    {
        private static AnimationManager myBackground;
        private static Texture2D myTexture;
        private static Vector2 myPosition;
        private static Point mySize;
        private static float 
            myAnimationSpeed,
            myParallaxing;

        public static void Initialize(float aParallax)
        {
            myBackground = new AnimationManager(new Point(4, 2), 0.15f, true);
            myPosition = Vector2.Zero;
            myAnimationSpeed = 0.15f;
            myParallaxing = aParallax;
        }

        public static void Update()
        {
            myPosition = -(Camera.Position / myParallaxing);
        }

        public static void Draw(SpriteBatch aSpriteBatch, GameTime aGameTime, GameWindow aWindow)
        {
            Vector2 tempPosition = new Vector2(
                ((-myPosition.X) / mySize.X), 
                ((-myPosition.Y) / mySize.Y));
            Vector2 tempWindowDimensions = new Vector2(
                ((aWindow.ClientBounds.Width) / mySize.X) + 2,
                ((aWindow.ClientBounds.Height) / mySize.Y) + 2);

            int tempXPosition = (int)(tempPosition.X - (tempWindowDimensions.X / 2));
            int tempXLength = (int)tempPosition.X + (int)Math.Ceiling(tempWindowDimensions.X);

            int tempYPosition = (int)(tempPosition.Y - (tempWindowDimensions.Y / 2));
            int tempYLength = (int)tempPosition.Y + (int)Math.Ceiling(tempWindowDimensions.Y);

            for (int x = tempXPosition; x < tempXLength; x++)
            {
                for (int y = tempYPosition; y < tempYLength; y++)
                {
                    Rectangle tempDrawRect = new Rectangle(
                        (int)myPosition.X + mySize.X * x, 
                        (int)myPosition.Y + mySize.Y * y, 
                        mySize.X, 
                        mySize.Y);

                    myBackground.AnimationSpeed = myAnimationSpeed * 
                        (tempXLength - tempXPosition) * 
                        (tempYLength - tempYPosition);

                    myBackground.Draw(aSpriteBatch, aGameTime, myTexture, tempDrawRect, new Point(676), Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 1.0f);
                }
            }
        }

        public static void LoadContent()
        {
            myTexture = ResourceManager.RequestTexture("Background");
            mySize = new Point(myTexture.Width / 4, myTexture.Height / 2);
        }
    }
}
