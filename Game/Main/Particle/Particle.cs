using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LilyPath;

namespace Tower_Defense
{
    abstract class Particle : GameObject
    {
        public bool IsAlive { get; set; }

        public Particle(Vector2 aPosition, Point aSize) : base(aPosition, aSize)
        {
            IsAlive = true;
        }

        public abstract void Update(GameTime aGameTime);

        public abstract void Draw(SpriteBatch aSpriteBatch, DrawBatch aDrawBatch, GameTime aGameTime);

        public abstract void LoadContent();
    }
}
