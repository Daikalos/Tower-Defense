using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense
{
    class Enemy : DynamicObject
    {
        //Enemy
        private AnimationManager myEnemyAnimation;
        private Vector2 myOffset;
        private bool myIsAlive;
        private int
            myDirection,
            myHealthPoints,
            myMaxHealthPoints,
            myEnemyType,
            myWalkToTile;

        //Healthbar
        private Texture2D myHealthbar;
        private Vector2 myHealthbarOffset;
        private Rectangle 
            myHealthbarDest, 
            myHealthbarSource;

        public bool IsAlive
        {
            get => myIsAlive;
            set => myIsAlive = value;
        }

        public Enemy(Vector2 aPosition, Point aSize, float aSpeed, int someHP, int anEnemyType) : base(aPosition, aSize, aSpeed)
        {
            this.myHealthPoints = someHP;
            this.myMaxHealthPoints = someHP;
            this.myEnemyType = anEnemyType;

            this.myPosition = GameInfo.Path[0].Position; //Spawn position
            this.myIsAlive = true;
            this.myWalkToTile = 1;

            switch (myEnemyType)
            {
                case 0:
                    myEnemyAnimation = new AnimationManager(new Point(16, 4), 0.12f, true);
                    myOffset = new Vector2(0, (GameInfo.Path[0].Size.Y / 2) - mySize.Y + 12);
                    break;
                case 1:
                    myEnemyAnimation = new AnimationManager(new Point(16, 4), 0.12f, true);
                    myOffset = new Vector2(0, (GameInfo.Path[0].Size.Y / 2) - mySize.Y + 12);
                    break;
                case 2:
                    myEnemyAnimation = new AnimationManager(new Point(16, 4), 0.12f, true);
                    myOffset = new Vector2(0, (GameInfo.Path[0].Size.Y / 2) - mySize.Y + 12);
                    break;
                case 3:
                    myEnemyAnimation = new AnimationManager(new Point(8, 4), 0.12f, true);
                    myOffset = new Vector2(0, (GameInfo.Path[0].Size.Y / 2) - mySize.Y + 24);
                    break;
            }
            this.myPosition += myOffset;
        }

        public void Update(GameTime aGameTime)
        {
            //A constant speed is not desired in a tower defense, will make game too fast at lower frames
            base.Update();

            myHealthbarDest = new Rectangle(
                (int)(myPosition.X + myHealthbarOffset.X), 
                (int)(myPosition.Y + myHealthbarOffset.Y), 
                myHealthbarDest.Width, 
                myHealthbarDest.Height);

            Movement(aGameTime);
            SetDirection();
        }

        public override void Draw(SpriteBatch aSpriteBatch)
        {
            Rectangle tempSource = new Rectangle(0, 0, myHealthbarSource.Width * (myHealthPoints / myMaxHealthPoints), myHealthbarSource.Height);
            aSpriteBatch.Draw(myHealthbar, myHealthbarDest, tempSource, Color.White * 0.8f, 0.0f, Vector2.Zero, SpriteEffects.None, 0.0f);
        }

        public override void DrawWithDepth(SpriteBatch aSpriteBatch, GameTime aGameTime, float aDepth)
        {
            switch (myEnemyType)
            {
                case 0:
                    myEnemyAnimation.DrawByRow(aSpriteBatch, aGameTime, myTexture, myDestRect, new Point(64), Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, aDepth, myDirection);
                    break;
                case 1:
                    myEnemyAnimation.DrawByRow(aSpriteBatch, aGameTime, myTexture, myDestRect, new Point(64), Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, aDepth, myDirection);
                    break;
                case 2:
                    myEnemyAnimation.DrawByRow(aSpriteBatch, aGameTime, myTexture, myDestRect, new Point(64), Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, aDepth, myDirection);
                    break;
                case 3:
                    myEnemyAnimation.DrawByRow(aSpriteBatch, aGameTime, myTexture, myDestRect, new Point(192), Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, aDepth, myDirection);
                    break;
            }
        }

        private void Movement(GameTime aGameTime)
        {
            Vector2 tempDir = (GameInfo.Path[myWalkToTile].Position - myPosition + myOffset);
            Vector2 tempNorm = Extensions.Normalize(tempDir);

            if (Vector2.Distance(myPosition - myOffset, GameInfo.Path[myWalkToTile].Position) < myCurrentSpeed)
            {
                myWalkToTile++;
                if (myWalkToTile > GameInfo.Path.Count - 1)
                {
                    myWalkToTile = GameInfo.Path.Count - 1;
                }
            }
            else
            {
                myCurrentSpeed = mySpeed * 60 * (float)aGameTime.ElapsedGameTime.TotalSeconds;
                myPosition += tempNorm * myCurrentSpeed;
            }
        }
        private void SetDirection()
        {
            if (GameInfo.Path[myWalkToTile].Position.X > GameInfo.Path[myWalkToTile - 1].Position.X)
            {
                if (GameInfo.Path[myWalkToTile].Position.Y > GameInfo.Path[myWalkToTile - 1].Position.Y)
                {
                    myDirection = 0;
                }
                if (GameInfo.Path[myWalkToTile].Position.Y < GameInfo.Path[myWalkToTile - 1].Position.Y)
                {
                    myDirection = 1;
                }
            }
            if (GameInfo.Path[myWalkToTile].Position.X < GameInfo.Path[myWalkToTile - 1].Position.X)
            {
                if (GameInfo.Path[myWalkToTile].Position.Y < GameInfo.Path[myWalkToTile - 1].Position.Y)
                {
                    myDirection = 2;
                }
                if (GameInfo.Path[myWalkToTile].Position.Y > GameInfo.Path[myWalkToTile - 1].Position.Y)
                {
                    myDirection = 3;
                }
            }
        }

        public void SetTexture()
        {
            SetTexture("Enemy_" + Extensions.NumberFormat(myEnemyType));

            myHealthbar = ResourceManager.RequestTexture("Healthbar");
            myHealthbarDest = new Rectangle(0, 0, 
                (myHealthbar.Width / 2) - (myHealthbar.Width / 8), 
                (myHealthbar.Height / 2) - (myHealthbar.Height / 8));
            myHealthbarSource = new Rectangle(0, 0, myHealthbar.Width, myHealthbar.Height);
            myHealthbarOffset = new Vector2((myHealthbar.Width / 8) / 2, -8);
        }
    }
}
