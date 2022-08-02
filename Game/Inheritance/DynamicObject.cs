using Microsoft.Xna.Framework;

namespace Tower_Defense
{
    class DynamicObject : GameObject
    {
        protected float 
            mySpeed,
            myCurrentSpeed;

        public float Speed
        {
            get => mySpeed;
            set => mySpeed = value;
        }
        
        public DynamicObject(Vector2 aPosition, Point aSize, float aSpeed) : base(aPosition, aSize)
        {
            this.mySpeed = aSpeed;
        }
    }
}
