using Microsoft.Xna.Framework;

namespace Tower_Defense
{
    class Enemy : DynamicObject
    {
        private bool myIsAlive;
        private int myHealthPoints;

        public bool IsAlive
        {
            get => myIsAlive;
            set => myIsAlive = value;
        }

        public Enemy(Vector2 aPosition, Point aSize, Vector2 aVelocity, Vector2 aVelocityThreshold, int someHP) : base(aPosition, aSize, aVelocity, aVelocityThreshold)
        {
            this.myHealthPoints = someHP;

            this.myIsAlive = true;
        }

        
    }
}
