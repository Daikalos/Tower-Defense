using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense
{
    abstract class Enemy : GameObject
    {
        //Enemy
        protected Animation myEnemyAnimation;
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
            base.Update();

            myHealthbarDest = new Rectangle(
                (int)(myPosition.X + myHealthbarOffset.X), 
                (int)(myPosition.Y + myHealthbarOffset.Y), 
                myHealthbarDest.Width, 
                myHealthbarDest.Height);

            myOffsetPosition = myPosition - myOffset;

            if (myProperties.HealthPoints <= 0)
            {
                GameInfo.Score += myMaxHealthPoints;
                GameInfo.Money += myMaxHealthPoints * EnemyProperties.Enemy_Info.Enemy_Value;

                ParticleManager.AddParticle(new Explosion(new Vector2(myOffsetPosition.X, myOffsetPosition.Y - (mySize.Y / 2)), new Point(48)));
                myIsAlive = false;
            }

            Movement(aGameTime);
            SetDirection();
        }

        public override void Draw(SpriteBatch aSpriteBatch)
        {
            Rectangle tempDest = new Rectangle(
                myHealthbarDest.X,
                myHealthbarDest.Y,
                (int)(myHealthbarDest.Width * ((float)myProperties.HealthPoints / (float)myMaxHealthPoints)),
                myHealthbarDest.Height);

            Rectangle tempSource = new Rectangle(
                0, 
                0, 
                (int)(myHealthbarSource.Width * ((float)myProperties.HealthPoints / (float)myMaxHealthPoints)), 
                myHealthbarSource.Height);

            aSpriteBatch.Draw(myHealthbar, tempDest, tempSource, Color.White);
        }

        private void Movement(GameTime aGameTime)
        {
            Vector2 tempDir = (GameInfo.Path[myWalkToTile].GetCenter() - myPosition + myOffset);
            Vector2 tempNorm = Extensions.Normalize(tempDir);

            ReachedGoal();

            myCurrentSpeed = myProperties.Speed * 60 * (float)aGameTime.ElapsedGameTime.TotalSeconds * GameInfo.GameSpeed;
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
                GameInfo.Health -= myProperties.HealthPoints;
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
                else
                {
                    myDirection = 1;
                }
            }
            else
            {
                if (GameInfo.Path[myWalkToTile].GetCenter().Y < GameInfo.Path[myWalkToTile - 1].GetCenter().Y)
                {
                    myDirection = 2;
                }
                else
                {
                    myDirection = 3;
                }
            }
        }

        public void RecieveDamage(int anAmount)
        {
            myProperties.HealthPoints -= anAmount;
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

        protected class Enemy_Properties
        {
            public float Speed { get; set; }
            public int HealthPoints { get; set; }
        }
    }
}
