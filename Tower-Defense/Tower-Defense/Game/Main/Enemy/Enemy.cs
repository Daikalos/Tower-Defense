using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense
{
    class Enemy : GameObject
    {
        //Enemy
        protected AnimationManager myEnemyAnimation;
        protected Vector2 
            myOffsetPosition,
            myOffset;
        protected bool myIsAlive;
        protected float
            myCurrentSpeed;
        protected int
            myDirection,
            myMaxHealthPoints,
            myWalkToTile;

        protected Enemy_Properties myProperties;

        //Healthbar
        private Texture2D myHealthbar;
        private Vector2 myHealthbarOffset;
        private Rectangle 
            myHealthbarDest, 
            myHealthbarSource;

        public Vector2 OffsetPosition
        {
            get => myOffsetPosition;
        }
        public bool IsAlive
        {
            get => myIsAlive;
            set => myIsAlive = value;
        }

        public Enemy_Properties Properties
        {
            get => myProperties;
        }

        public Enemy(Vector2 aPosition, Point aSize) : base(aPosition, aSize)
        {
            this.myOffsetPosition = aPosition;

            this.myIsAlive = true;
            this.myWalkToTile = 1;

            this.myProperties = new Enemy_Properties();

            //Each individual value on enemies is fixed and can only be modified from EnemyProperties
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

            myOffsetPosition = myPosition - myOffset;

            Movement(aGameTime);
            SetDirection();
        }

        public override void Draw(SpriteBatch aSpriteBatch)
        {
            Rectangle tempSource = new Rectangle(0, 0, myHealthbarSource.Width * (myProperties.HealthPoints / myMaxHealthPoints), myHealthbarSource.Height);
            aSpriteBatch.Draw(myHealthbar, myHealthbarDest, tempSource, Color.White * 0.8f, 0.0f, Vector2.Zero, SpriteEffects.None, 0.0f);
        }

        private void Movement(GameTime aGameTime)
        {
            Vector2 tempDir = (GameInfo.Path[myWalkToTile].GetCenter() - myPosition + myOffset);
            Vector2 tempNorm = Extensions.Normalize(tempDir);

            ReachedGoal();

            myCurrentSpeed = myProperties.Speed * 60 * (float)aGameTime.ElapsedGameTime.TotalSeconds;
            if (Vector2.Distance(myPosition - myOffset, GameInfo.Path[myWalkToTile].GetCenter()) < myCurrentSpeed)
            {
                myWalkToTile++;
                if (myWalkToTile > GameInfo.Path.Count - 1)
                {
                    myWalkToTile = GameInfo.Path.Count - 1;
                }
            }
            else
            {
                myPosition += tempNorm * myCurrentSpeed;
            }
        }

        private void ReachedGoal()
        {
            if (Vector2.Distance(myPosition - myOffset, GameInfo.Path[GameInfo.Path.Count - 1].GetCenter()) < myCurrentSpeed)
            {
                myIsAlive = false;
                GameInfo.Health -= Properties.HealthPoints;
            }
        }

        private void SetDirection()
        {
            if (GameInfo.Path[myWalkToTile].GetCenter().X > GameInfo.Path[myWalkToTile - 1].GetCenter().X)
            {
                if (GameInfo.Path[myWalkToTile].GetCenter().Y > GameInfo.Path[myWalkToTile - 1].GetCenter().Y)
                {
                    myDirection = 0;
                }
                if (GameInfo.Path[myWalkToTile].GetCenter().Y < GameInfo.Path[myWalkToTile - 1].GetCenter().Y)
                {
                    myDirection = 1;
                }
            }
            if (GameInfo.Path[myWalkToTile].GetCenter().X < GameInfo.Path[myWalkToTile - 1].GetCenter().X)
            {
                if (GameInfo.Path[myWalkToTile].GetCenter().Y < GameInfo.Path[myWalkToTile - 1].GetCenter().Y)
                {
                    myDirection = 2;
                }
                if (GameInfo.Path[myWalkToTile].GetCenter().Y > GameInfo.Path[myWalkToTile - 1].GetCenter().Y)
                {
                    myDirection = 3;
                }
            }
        }

        public void LoadContent()
        {
            SetTexture(this.GetType().Name);

            myHealthbar = ResourceManager.RequestTexture("Healthbar");
            myHealthbarDest = new Rectangle(0, 0, 
                (myHealthbar.Width / 2) - (myHealthbar.Width / 8), 
                (myHealthbar.Height / 2) - (myHealthbar.Height / 8));
            myHealthbarSource = new Rectangle(0, 0, myHealthbar.Width, myHealthbar.Height);
            myHealthbarOffset = new Vector2((myHealthbar.Width / 8) / 2, -8);
        }

        public class Enemy_Properties
        {
            private float mySpeed;
            private int myHealthPoints;

            public float Speed
            {
                get => mySpeed;
                set => mySpeed = value;
            }
            public int HealthPoints
            {
                get => myHealthPoints;
                set => myHealthPoints = value;
            }
        }
    }
}
