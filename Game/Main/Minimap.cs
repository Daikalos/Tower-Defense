using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense
{
    class Minimap : StaticObject
    {
        //Rendertarget
        private readonly GraphicsDevice myDevice;
        private readonly SpriteBatch mySpriteBatch;
        private readonly RenderTarget2D myMiniMap;
        private readonly RenderTarget myTargetToRender;

        public Minimap(Vector2 aPosition, Point aSize, RenderTarget aTargetToRender, GraphicsDevice aGraphicsDevice) : base(aPosition, aSize)
        {
            this.myTargetToRender = aTargetToRender;
            this.myDevice = aGraphicsDevice;
            this.mySpriteBatch = new SpriteBatch(aGraphicsDevice);
            this.myMiniMap = new RenderTarget2D(aGraphicsDevice, aGraphicsDevice.Viewport.Width, aGraphicsDevice.Viewport.Height);
        }

        public override void Draw(SpriteBatch aSpriteBatch)
        {
            aSpriteBatch.End();
            aSpriteBatch.Begin();

            aSpriteBatch.Draw(myMiniMap, myDestRect, Color.White);
        }

        public void SetRenderTarget(GameTime aGameTime, GameWindow aWindow)
        {
            myDevice.SetRenderTarget(myMiniMap);
            myDevice.Clear(Color.Black);

            mySpriteBatch.Begin();

            myTargetToRender?.Invoke(mySpriteBatch, aGameTime, aWindow);

            mySpriteBatch.End();

            myDevice.SetRenderTarget(null);
        }

        public delegate void RenderTarget(SpriteBatch aSpriteBatch, GameTime aGameTime, GameWindow aWindow);
    }
}
