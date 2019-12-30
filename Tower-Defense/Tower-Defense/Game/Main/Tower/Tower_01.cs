using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LilyPath;

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

            this.myProperties.Price = TowerProperties.Tower_01.Price;
            this.myProperties.FireSpeed_Price = TowerProperties.Tower_01.FireSpeed_Price;
            this.myProperties.Range_Price = TowerProperties.Tower_01.Range_Price;
            this.myProperties.Damage_Price = TowerProperties.Tower_01.Damage_Price;
            this.myProperties.NumberOfTargets_Price = TowerProperties.Tower_01.NumberOfTargets_Price;

            this.myProperties.TowerLevelsMax = new int[]
            {
                TowerProperties.Tower_01.FireSpeed_Level_Max,
                TowerProperties.Tower_01.Range_Level_Max,
                TowerProperties.Tower_01.Damage_Level_Max,
                TowerProperties.Tower_01.NumberOfTargets_Level_Max
            };

            this.myProperties.FireSpeedDelay = myProperties.FireSpeed;
        }

        public override void Update(GameTime aGameTime)
        {
            base.Update(aGameTime);

            CheckEnemyDistance(aGameTime);
        }

        private void CheckEnemyDistance(GameTime aGameTime)
        {
            if (EnemyManager.Enemies.Count > 0)
            {
                List<Tuple<Enemy, float>> tempDistToEnemy = new List<Tuple<Enemy, float>>();
                Rectangle tempRange = new Rectangle(
                    (int)(OffsetPosition.X - (myProperties.Range / 2)),
                    (int)(OffsetPosition.Y - (myProperties.Range / 4)),
                    (int)(myProperties.Range),
                    (int)(myProperties.Range / 2));

                for (int i = 0; i < EnemyManager.Enemies.Count; i++)
                {
                    if (Extensions.PointWithinEllipse(EnemyManager.Enemies[i].OffsetPosition, tempRange))
                    {
                        tempDistToEnemy.Add(new Tuple<Enemy, float>(EnemyManager.Enemies[i],
                            Vector2.Distance(OffsetPosition, EnemyManager.Enemies[i].OffsetPosition)));
                    }
                }

                List<Tuple<Enemy, float>> tempSortedList = tempDistToEnemy.OrderBy(d => d.Item2).ToList();

                if (tempSortedList.Count > 0)
                {
                    float tempAngle = Extensions.AngleToPoint(OffsetPosition, tempSortedList.First().Item1.OffsetPosition) + 180.0f; //180 to match spritesheet
                    RotateTower(tempAngle);

                    Attack(aGameTime, tempSortedList);
                }
            }
        }

        private void RotateTower(float aAngle)
        {
            float
                tempRotateTowerX = 0.0f, 
                tempRotateTowerY = 0.0f;

            tempRotateTowerX = (aAngle / 360.0f) * 4; //4 = total of frames

            tempRotateTowerY = (int)(tempRotateTowerX / 2);
            tempRotateTowerX = (int)(tempRotateTowerX % 2);

            mySourceRect = new Rectangle(
                (int)((myTexture.Width / 2) * tempRotateTowerX),
                (int)((myTexture.Height / 2) * tempRotateTowerY),
                (int)(myTexture.Width / 2),
                (int)(myTexture.Height / 2));
        }

        private void Attack(GameTime aGameTime, List<Tuple<Enemy, float>> someEnemies)
        {
            myProperties.FireSpeed -= (float)aGameTime.ElapsedGameTime.TotalSeconds * GameInfo.GameSpeed;
            if (myProperties.FireSpeed <= 0)
            {
                if (someEnemies.Count > 0)
                {
                    List<Vector2> tempPositions = new List<Vector2>();
                    tempPositions.Add(OffsetPosition);

                    for (int i = 0; i < myProperties.NumberOfTargets; i++)
                    {
                        if (i < someEnemies.Count)
                        {
                            tempPositions.Add(someEnemies[i].Item1.DestRect.Center.ToVector2());

                            someEnemies[i].Item1.RecieveDamage(myProperties.Damage);
                        }
                    }

                    ParticleManager.AddParticle(new Laser(Vector2.Zero, Point.Zero, Pen.Purple, 1.0f, tempPositions.ToArray()));
                }

                myProperties.FireSpeed = myProperties.FireSpeedDelay;
            }
        }

        public override void LoadContent()
        {
            base.SetTexture(this.GetType().Name);

            SourceRect = new Rectangle(0, 0, myTexture.Width / 2, myTexture.Height / 2);
        }
    }
}
