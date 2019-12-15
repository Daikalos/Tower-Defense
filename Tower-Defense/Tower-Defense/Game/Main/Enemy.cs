using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense
{
    class Enemy : DynamicObject
    {
        private AnimationManager myEnemyAnimation;
        private bool myIsAlive;
        private int 
            myHealthPoints,
            myWalkToTile,
            myEnemyType;

        public bool IsAlive
        {
            get => myIsAlive;
            set => myIsAlive = value;
        }

        public Enemy(Vector2 aPosition, Point aSize, float aSpeed, int someHP, int anEnemyType) : base(aPosition, aSize, aSpeed)
        {
            this.myHealthPoints = someHP;
            this.myEnemyType = anEnemyType;

            this.myIsAlive = true;
            this.myWalkToTile = 0;

            switch (myEnemyType)
            {
                case 0:
                    myEnemyAnimation = new AnimationManager(new Point(16, 4), 0.12f, true);
                    break;
                case 1:
                    myEnemyAnimation = new AnimationManager(new Point(16, 4), 0.12f, true);
                    break;
                case 2:
                    myEnemyAnimation = new AnimationManager(new Point(16, 4), 0.12f, true);
                    break;
                case 3:
                    myEnemyAnimation = new AnimationManager(new Point(8, 4), 0.12f, true);
                    break;
            }
        }

        public override void Update()
        {
            //A constant speed is not desired in a tower defense, will make game too fast at lower frames
            base.Update();

            myPosition = Camera.ViewToWorld(KeyMouseReader.CurrentMouseState.Position.ToVector2());
        }

        public override void DrawWithDepth(SpriteBatch aSpriteBatch, GameTime aGameTime, float aDepth)
        {
            switch (myEnemyType)
            {
                case 0:
                    myEnemyAnimation.DrawSpriteSheet(aSpriteBatch, aGameTime, myTexture, myDestRect, new Point(64), Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, aDepth);
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
            }
        }

        public void SetTexture()
        {
            SetTexture("Enemy_" + Extensions.NumberFormat(myEnemyType));
        }
    }
}
