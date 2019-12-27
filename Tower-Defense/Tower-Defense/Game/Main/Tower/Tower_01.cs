using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense
{
    class Tower_01 : Tower
    {
        public Tower_01(Vector2 aPosition, Point aSize) : base(aPosition, aSize)
        {
            this.myProperties.Name = TowerProperties.Tower_01.Name;

            this.myProperties.FireSpeed = TowerProperties.Tower_01.FireSpeed;
            this.myProperties.Range = TowerProperties.Tower_01.Range;
            this.myProperties.Damage = TowerProperties.Tower_01.Damage;
            this.myProperties.NumberOfTargets = TowerProperties.Tower_01.NumberOfTargets;

            this.myProperties.FireSpeed_Price = TowerProperties.Tower_01.FireSpeed_Price;
            this.myProperties.Range_Price = TowerProperties.Tower_01.Range_Price;
            this.myProperties.Damage_Price = TowerProperties.Tower_01.Damage_Price;
            this.myProperties.NumberOfTargets_Price = TowerProperties.Tower_01.NumberOfTargets_Price;

            this.myProperties.FireSpeedDelay = myProperties.FireSpeed;
        }

        public override void Update(GameTime aGameTime)
        {
            base.Update(aGameTime);

            RotateTower();
        }

        private void RotateTower()
        {
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

        public override void LoadContent()
        {
            base.SetTexture(this.GetType().Name);

            SourceRect = new Rectangle(0, 0, myTexture.Width / 2, myTexture.Height / 2);
        }
    }
}
