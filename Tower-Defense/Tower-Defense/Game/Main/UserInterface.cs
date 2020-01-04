using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense
{
    static class UserInterface
    {
        //Rendertarget
        private static GraphicsDevice myDevice;
        private static SpriteBatch mySpriteBatch;
        private static RenderTarget2D myInterface;
        private static RenderTarget myRenderTarget;

        private static Vector2 myPosition;
        private static Point mySize;
        private static Rectangle myDestRect;
        private static Color[] myPixels;

        public static void Initialize(Vector2 aPosition, Point aSize, GraphicsDevice aGraphicsDevice)
        {
            myDevice = aGraphicsDevice;
            mySpriteBatch = new SpriteBatch(aGraphicsDevice);
            myInterface = new RenderTarget2D(aGraphicsDevice, aGraphicsDevice.Viewport.Width, aGraphicsDevice.Viewport.Height);

            myPosition = aPosition;
            mySize = aSize;
            myDestRect = new Rectangle((int)myPosition.X, (int)myPosition.Y, mySize.X, mySize.Y);
            myPixels = new Color[myInterface.Width * myInterface.Height];
        }

        public static void Draw(SpriteBatch aSpriteBatch)
        {
            aSpriteBatch.End();
            aSpriteBatch.Begin();

            aSpriteBatch.Draw(myInterface, myDestRect, Color.White);
        }

        public static bool IsMouseOutside()
        {
            if (KeyMouseReader.MousePos.X >= 0 && KeyMouseReader.MousePos.Y >= 0 && KeyMouseReader.MousePos.X < myInterface.Width && KeyMouseReader.MousePos.Y < myInterface.Height)
            {
                myInterface.GetData(0, new Rectangle((int)KeyMouseReader.MousePos.X, (int)KeyMouseReader.MousePos.Y, 1, 1), myPixels, 0, 1);

                return myPixels[0].A < 10;
            }
            return false;
        }

        public static void SetRenderTarget(GameTime aGameTime, GameWindow aWindow)
        {
            myDevice.SetRenderTarget(myInterface);
            myDevice.Clear(Color.Transparent);

            mySpriteBatch.Begin();

            myRenderTarget(mySpriteBatch, aGameTime, aWindow);

            mySpriteBatch.End();

            myDevice.SetRenderTarget(null);
        }

        public static void DefineRenderTarget(RenderTarget aTarget)
        {
            myRenderTarget = aTarget;
        }

        public delegate void RenderTarget(SpriteBatch aSpriteBatch, GameTime aGameTime, GameWindow aWindow);
    }
}
