using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense
{
    class Tower : StaticObject
    {
        private Vector2 myOffsetPosition;
        private bool myIsAlive; //Incase if tower is sold

        public Vector2 OffsetPosition
        {
            get => myOffsetPosition;
        }
        public bool IsAlive
        {
            get => myIsAlive;
            set => myIsAlive = value;
        }

        public Tower(Vector2 aPosition, Point aSize) : base(aPosition, aSize)
        {
            this.myOffsetPosition = aPosition;

            this.myPosition.X -= aSize.X / 2;
            this.myPosition.Y -= aSize.Y - (Level.TileSize.Y / 2); //Push tower down to bottom of tile

            this.myDestRect = new Rectangle((int)myPosition.X, (int)myPosition.Y, mySize.X, mySize.Y); //Update destination rect after new values
            this.myIsAlive = true;
        }

        public virtual void Update(GameTime aGameTime) //Template for manager
        {
            base.Update();
        }

        public void SetTexture()
        {
            if (this is Tower_00)
            {
                base.SetTexture("Tower_00");
                SourceRect = new Rectangle(0, 0, myTexture.Width / 2, myTexture.Height / 2);
            }
            if (this is Tower_01)
            {
                base.SetTexture("Tower_01");
                SourceRect = new Rectangle(0, 0, myTexture.Width / 2, myTexture.Height / 2);
            }
            if (this is Tower_02)
            {
                base.SetTexture("Tower_02");
            }
        }
    }
}
