using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense
{
    class Minimap : StaticObject
    {
        //Rendertarget
        private readonly GraphicsDevice myDevice;
        private readonly SpriteBatch mySpriteBatch;
        private RenderTarget2D myMiniMap;

        public Minimap(Vector2 aPosition, Point aSize, GraphicsDevice aGraphicsDevice) : base(aPosition, aSize)
        {
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

        public void SetRenderTarget(GameTime aGameTime, PlayState aPlayState)
        {
            myDevice.SetRenderTarget(myMiniMap);
            myDevice.Clear(Color.Black);

            mySpriteBatch.Begin();

            aPlayState.DrawLevel(mySpriteBatch, aGameTime);

            mySpriteBatch.End();

            myDevice.SetRenderTarget(null);
        }
    }
}
