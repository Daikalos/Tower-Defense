using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LilyPath;

namespace Tower_Defense
{
    class Laser : Particle
    {
        private GraphicsPath myDrawPath;
        private float myTimer;

        public Laser(Vector2 aPosition, Point aSize, Pen aPen, float aDelay, params Vector2[] somePoints) : base(aPosition, aSize)
        {
            myDrawPath = new GraphicsPath(aPen, somePoints);

            this.myTimer = aDelay;
        }

        public override void Update(GameTime aGameTime)
        {
            myTimer -= (float)aGameTime.ElapsedGameTime.TotalSeconds * GameInfo.GameSpeed;
            if (myTimer <= 0)
            {
                IsAlive = false;
            }
        }

        public override void Draw(SpriteBatch aSpriteBatch, DrawBatch aDrawBatch, GameTime aGameTime)
        {
            aDrawBatch.Begin(DrawSortMode.Deferred, BlendState.AlphaBlend,
                SamplerState.AnisotropicClamp, null, null, null, Camera.TranslationMatrix);

            aDrawBatch.DrawPath(myDrawPath);

            aDrawBatch.End();
        }

        public override void DrawWithDepth(SpriteBatch aSpriteBatch, GameTime aGameTime, float aDepth)
        {
            //Empty
        }

        public override void LoadContent()
        {
            //No loading needed
        }
    }
}
