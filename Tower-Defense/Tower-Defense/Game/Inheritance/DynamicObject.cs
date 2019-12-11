using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense
{
    class DynamicObject : GameObject
    {
        protected Vector2 
            myVelocity,
            myCurrentVelocity,
            myVelocityThreshold;

        public Vector2 Velocity
        {
            get => myVelocity;
        }
        public Vector2 CurrentVelocity
        {
            get => myCurrentVelocity;
        }
        
        public DynamicObject(Vector2 aPosition, Point aSize, Vector2 aVelocity, Vector2 aVelocityThreshold) : base(aPosition, aSize)
        {
            this.myVelocity = aVelocity; //Speed of object in x, y-axis
            this.myVelocityThreshold = aVelocityThreshold; //Maximum speed in x, y-axis, useless atm

            this.myPosition += new Vector2(0, -mySize.Y); //Adjust spawn position after size
        }

        protected void Movement(GameTime aGameTime)
        {
            myCurrentVelocity = myVelocity * 60 * (float)aGameTime.ElapsedGameTime.TotalSeconds;
            myPosition += myCurrentVelocity;
        }
    }
}
