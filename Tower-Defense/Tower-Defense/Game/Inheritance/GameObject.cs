using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense
{
    class GameObject
    {
        protected Texture2D myTexture;
        protected Vector2
            myPosition,
            myOrigin;
        protected Rectangle 
            myBoundingBox,
            myDestRect,
            mySourceRect;
        protected Point mySize;

        public Texture2D Texture
        {
            get => myTexture;
            set => myTexture = value;
        }
        public Vector2 Position
        {
            get => myPosition;
            set => myPosition = value;
        }
        public virtual Rectangle BoundingBox
        {
            get => myDestRect;
            set => myDestRect = value;
        }
        public virtual Rectangle DestRect
        {
            get => myDestRect;
            set => myDestRect = value;
        }
        public virtual Rectangle SourceRect
        {
            get => mySourceRect;
            set => mySourceRect = value;
        }
        public Point Size
        {
            get => mySize;
            set => mySize = value;
        }

        protected GameObject(Vector2 aPosition, Point aSize)
        {
            this.myPosition = aPosition;
            this.mySize = aSize;

            this.myOrigin = Vector2.Zero;
            this.myDestRect = new Rectangle((int)myPosition.X, (int)myPosition.Y, mySize.X, mySize.Y);
        }

        public virtual void Update()
        {
            myDestRect = new Rectangle((int)myPosition.X, (int)myPosition.Y, mySize.X, mySize.Y);
        }

        public virtual void Draw(SpriteBatch aSpriteBatch)
        {
            aSpriteBatch.Draw(myTexture, myDestRect, mySourceRect, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 1.0f);
        }

        public virtual void DrawWithDepth(SpriteBatch aSpriteBatch, GameTime aGameTime, float aDepth)
        {
            aSpriteBatch.Draw(myTexture, myDestRect, mySourceRect, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, aDepth);
        }

        public virtual void SetTexture(string aName)
        {
            if (myTexture != ResourceManager.RequestTexture(aName))
            {
                myTexture = ResourceManager.RequestTexture(aName);
                mySourceRect = new Rectangle(0, 0, myTexture.Width, myTexture.Height);
            }
        }

        public void SetOrigin(Point someFrames)
        {
            myOrigin = new Vector2(myTexture.Width / 2 / someFrames.X, myTexture.Height / 2 / someFrames.Y);
        }
    }
}
