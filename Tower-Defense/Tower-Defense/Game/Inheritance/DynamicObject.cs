using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense
{
    class DynamicObject : GameObject
    {
        protected float mySpeed;

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
