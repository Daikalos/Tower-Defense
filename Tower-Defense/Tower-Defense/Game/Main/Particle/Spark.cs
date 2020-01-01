using LilyPath;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense
{
    class Spark : Particle
    {
        private Vector2 myVelocity;
        private float 
            myGravity,
            myTimer;

        public Spark(Vector2 aPosition, Point aSize) : base(aPosition, aSize)
        {
            this.myVelocity = new Vector2(StaticRandom.RandomNumber(-2, 3), StaticRandom.RandomNumber(-5, 0));
            this.myGravity = 0.2f;
            this.myTimer = StaticRandom.RandomNumber(3, 6) / 10.0f;
        }

        public override void Update(GameTime aGameTime)
        {
            myVelocity.Y += myGravity * 60 * (float)aGameTime.ElapsedGameTime.TotalSeconds * GameInfo.GameSpeed;
            myPosition += myVelocity * 60 * (float)aGameTime.ElapsedGameTime.TotalSeconds * GameInfo.GameSpeed;

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

            aDrawBatch.FillRectangle(Brush.White, myPosition, mySize.X, mySize.Y);

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
