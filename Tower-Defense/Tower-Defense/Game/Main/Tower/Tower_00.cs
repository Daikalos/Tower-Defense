using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense
{
    class Tower_00 : Tower
    {
        public Tower_00(Vector2 aPosition, Point aSize) : base(aPosition, aSize)
        {

        }

        public override void Update(GameTime aGameTime)
        {
            base.Update();

            if (EnemyManager.Enemies.Count > 0)
            {
                float tempAngle = Extensions.AngleToPoint(OffsetPosition, EnemyManager.Enemies[0].OffsetPosition) + 180.0f; //180 to match spritesheet

                float
                    tempRotateTowerX = 0.0f,
                    tempRotateTowerY = 0.0f;

                tempRotateTowerX = (tempAngle / 360.0f) * 4; //4 = total of frames

                tempRotateTowerY = (int)(tempRotateTowerX / 2);
                tempRotateTowerX = (int)(tempRotateTowerX % 2);

                mySourceRect = new Rectangle(
                    (int)((myTexture.Width / 2) * tempRotateTowerX), 
                    (int)((myTexture.Height / 2) * tempRotateTowerY), 
                    (int)(myTexture.Width / 2), 
                    (int)(myTexture.Height / 2));
            }
        }
    }
}
