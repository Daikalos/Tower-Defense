using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense
{
    internal class AnimationManager
    {
        //Animation-Info
        private Rectangle mySourceRect;
        private Point myCurrentFramePos;
        private float myAnimationSpeed;
        private float myTimer;
        private int myCurrentFrame;
        private bool myIsFinished;

        //Sprite-Info
        private Point myFrameAmount;
        private bool myIsLoop;

        public Rectangle SourceRect
        {
            get => mySourceRect;
        }
        public float AnimationSpeed
        {
            get => myAnimationSpeed;
            set => myAnimationSpeed = value;
        }
        public bool IsFinished
        {
            get => myIsFinished;
            set => myIsFinished = value;
        }

        public AnimationManager(Point aFrameAmount, float aAnimationSpeed, bool aIsLoop)
        {
            this.myCurrentFrame = 0;
            this.myIsFinished = false;

            this.myFrameAmount = aFrameAmount;
            this.myAnimationSpeed = aAnimationSpeed;
            this.myIsLoop = aIsLoop;
        }

        public void Draw(SpriteBatch aSpriteBatch, GameTime aGameTime, Texture2D aTexture, Rectangle aDestRect, Point aFrameSize, Color aColor, float aRotation, Vector2 aOrigin, SpriteEffects aSE, float aDepth)
        {
            if (myIsFinished) return;

            if (!GameInfo.IsPaused)
            {
                myTimer += (float)aGameTime.ElapsedGameTime.TotalSeconds;
                if (myTimer > myAnimationSpeed)
                {
                    myCurrentFrame++;
                    myCurrentFramePos.X++;
                    if (myCurrentFrame >= (myFrameAmount.X * myFrameAmount.Y))
                    {
                        if (myIsLoop)
                        {
                            myCurrentFrame = 0;
                            myCurrentFramePos = new Point(0, 0);
                        }
                        else
                        {
                            myCurrentFrame = (myFrameAmount.X * myFrameAmount.Y) - 1;
                            myIsFinished = true;
                        }
                    }
                    if (myCurrentFramePos.X >= myFrameAmount.X) //Animation
                    {
                        myCurrentFramePos.Y++;
                        myCurrentFramePos.X = 0;
                    }
                    myTimer = 0;
                }
            }

            mySourceRect = new Rectangle(aFrameSize.X * myCurrentFramePos.X, aFrameSize.Y * myCurrentFramePos.Y, aFrameSize.X, aFrameSize.Y);

            aSpriteBatch.Draw(aTexture, aDestRect, mySourceRect, aColor, aRotation, aOrigin, aSE, aDepth);
        }

        public void DrawByRow(SpriteBatch aSpriteBatch, GameTime aGameTime, Texture2D aTexture, Rectangle aDestRect, Point aFrameSize, Color aColor, float aRotation, Vector2 aOrigin, SpriteEffects aSE, float aDepth, int aCurrentRow)
        {
            if (myIsFinished) return;

            if (!GameInfo.IsPaused)
            {
                myTimer += (float)aGameTime.ElapsedGameTime.TotalSeconds;
                if (myTimer > myAnimationSpeed)
                {
                    myCurrentFrame++;
                    myCurrentFramePos.X++;
                    if (myCurrentFrame >= myFrameAmount.X)
                    {
                        if (myIsLoop)
                        {
                            myCurrentFrame = 0;
                            myCurrentFramePos = new Point(0, aCurrentRow);
                        }
                        else
                        {
                            myCurrentFrame = (myFrameAmount.X * aCurrentRow) - 1;
                            myIsFinished = true;
                        }
                    }
                    if (myCurrentFramePos.X >= myFrameAmount.X) //Animation
                    {
                        myCurrentFramePos.X = 0;
                    }
                    myTimer = 0;
                }
            }

            mySourceRect = new Rectangle(aFrameSize.X * myCurrentFramePos.X, aFrameSize.Y * aCurrentRow, aFrameSize.X, aFrameSize.Y);

            aSpriteBatch.Draw(aTexture, aDestRect, mySourceRect, aColor, aRotation, aOrigin, aSE, aDepth);
        }
    }
}