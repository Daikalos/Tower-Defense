using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense
{
    class Tower_00 : Tower
    {
        public Tower_00(Vector2 aPosition, Point aSize) : base(aPosition, aSize)
        {
            this.myProperties.Name = TowerProperties.Tower_00.Name;

            this.myProperties.FireSpeed = TowerProperties.Tower_00.FireSpeed;
            this.myProperties.Range = TowerProperties.Tower_00.Range;
            this.myProperties.Damage = TowerProperties.Tower_00.Damage;
            this.myProperties.NumberOfTargets = TowerProperties.Tower_00.NumberOfTargets;

            this.myProperties.FireSpeed_Price = TowerProperties.Tower_00.FireSpeed_Price;
            this.myProperties.Range_Price = TowerProperties.Tower_00.Range_Price;
            this.myProperties.Damage_Price = TowerProperties.Tower_00.Damage_Price;
            this.myProperties.NumberOfTargets_Price = TowerProperties.Tower_00.NumberOfTargets_Price;

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
                Tuple<Enemy, float>[] tempDistToEnemy = new Tuple<Enemy, float>[EnemyManager.Enemies.Count];
                Rectangle tempRange = new Rectangle(
                    (int)(OffsetPosition.X - (myProperties.Range / 2)),
                    (int)(OffsetPosition.Y - (myProperties.Range / 4)),
                    (int)(myProperties.Range),
                    (int)(myProperties.Range / 2));

                for (int i = 0; i < tempDistToEnemy.Length; i++)
                {
                    tempDistToEnemy[i] = new Tuple<Enemy, float>(EnemyManager.Enemies[i], float.MaxValue);
                    if (Extensions.PointWithinEllipse(EnemyManager.Enemies[i].OffsetPosition, tempRange))
                    {
                        tempDistToEnemy[i] = new Tuple<Enemy, float>(EnemyManager.Enemies[i],
                            Vector2.Distance(OffsetPosition, EnemyManager.Enemies[i].OffsetPosition));
                    }
                }

                if (tempDistToEnemy.Length > 0 && Array.Exists(tempDistToEnemy, d => d.Item2 != float.MaxValue))
                {
                    Tuple<Enemy, float>[] tempSortedArray = tempDistToEnemy.OrderBy(d => d.Item2).ToArray();

                    float tempAngle = Extensions.AngleToPoint(OffsetPosition, tempSortedArray.First().Item1.OffsetPosition) + 180.0f; //180 to match spritesheet

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

        public override void LoadContent()
        {
            base.SetTexture(this.GetType().Name);

            SourceRect = new Rectangle(0, 0, myTexture.Width / 2, myTexture.Height / 2);
        }
    }
}
