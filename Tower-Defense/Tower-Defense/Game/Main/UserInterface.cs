using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense
{
    class UserInterface : StaticObject
    {
        //Rendertarget
        private readonly GraphicsDevice myDevice;
        private readonly SpriteBatch mySpriteBatch;
        private readonly RenderTarget2D myInterface;
        private readonly RenderTarget myRenderTarget;

        public UserInterface(Vector2 aPosition, Point aSize, RenderTarget aRenderTarget, GraphicsDevice aGraphicsDevice) : base(aPosition, aSize)
        {
            this.myRenderTarget = aRenderTarget;
            this.myDevice = aGraphicsDevice;
            this.mySpriteBatch = new SpriteBatch(aGraphicsDevice);
            this.myInterface = new RenderTarget2D(aGraphicsDevice, aGraphicsDevice.Viewport.Width, aGraphicsDevice.Viewport.Height);
        }

        public override void Draw(SpriteBatch aSpriteBatch)
        {
            aSpriteBatch.End();
            aSpriteBatch.Begin();

            aSpriteBatch.Draw(myInterface, myDestRect, Color.White);
        }

        public delegate void RenderTarget(SpriteBatch aSpriteBatch, GameTime aGameTime, GameWindow aWindow);

        public void SetRenderTarget(GameTime aGameTime, GameWindow aWindow)
        {
            myDevice.SetRenderTarget(myInterface);
            myDevice.Clear(Color.Transparent);

            mySpriteBatch.Begin();

            myRenderTarget(mySpriteBatch, aGameTime, aWindow);

            mySpriteBatch.End();

            myDevice.SetRenderTarget(null);
        }
    }
}
