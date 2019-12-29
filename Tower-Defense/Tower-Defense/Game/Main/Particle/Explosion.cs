using LilyPath;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense
{
    class Explosion : Particle
    {
        private Animation myExplosion;

        public Explosion(Vector2 aPosition, Point aSize) : base(aPosition, aSize)
        {
            myExplosion = new Animation(new Point(5, 5), 0.05f, false);
        }

        public override void Update(GameTime aGameTime)
        {
            IsAlive = !myExplosion.IsFinished;
        }

        public override void Draw(SpriteBatch aSpriteBatch, DrawBatch aDrawBatch, GameTime aGameTime)
        {
            //Empty
        }

        public override void DrawWithDepth(SpriteBatch aSpriteBatch, GameTime aGameTime, float aDepth)
        {
            myExplosion.Draw(aSpriteBatch, aGameTime, myTexture, myDestRect, new Point(64), Color.White, 0.0f,
                myOrigin, SpriteEffects.None, aDepth);
        }

        public override void LoadContent()
        {
            myTexture = ResourceManager.RequestTexture("Explosion_00");
            SetOrigin(new Point(5, 5));
        }
    }
}
