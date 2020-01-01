using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LilyPath;

namespace Tower_Defense
{
    class Tower_00 : Tower
    {
        public Tower_00(Vector2 aPosition, Point aSize) : base(aPosition, aSize)
        {
            this.myProperties.Name = TowerProperties.Tower_00.Name;

            this.myProperties.AttackRate = TowerProperties.Tower_00.AttackRate;
            this.myProperties.Range = TowerProperties.Tower_00.Range;
            this.myProperties.Damage = TowerProperties.Tower_00.Damage;
            this.myProperties.NumberOfTargets = TowerProperties.Tower_00.NumberOfTargets;

            this.myProperties.Price = TowerProperties.Tower_00.Price;
            this.myProperties.AttackRate_Price = TowerProperties.Tower_00.AttackRate_Price;
            this.myProperties.Range_Price = TowerProperties.Tower_00.Range_Price;
            this.myProperties.Damage_Price = TowerProperties.Tower_00.Damage_Price;
            this.myProperties.NumberOfTargets_Price = TowerProperties.Tower_00.NumberOfTargets_Price;

            this.myProperties.TowerLevelsMax = new int[]
            {
                TowerProperties.Tower_00.AttackRate_Level_Max,
                TowerProperties.Tower_00.Range_Level_Max,
                TowerProperties.Tower_00.Damage_Level_Max,
                TowerProperties.Tower_00.NumberOfTargets_Level_Max
            };

            this.myProperties.AttackRateDelay = myProperties.AttackRate;
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
                            EnemyManager.Enemies[i].DistanceTraveled));
                    }
                }

                List<Tuple<Enemy, float>> tempSortedList = tempDistToEnemy.OrderByDescending(d => d.Item2).ToList();

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

            tempRotateTowerY = (int)(tempRotateTowerX / 2); //2 = amount of frames in x-axis
            tempRotateTowerX = (int)(tempRotateTowerX % 2); //2 = amount of frames in y-axis

            mySourceRect = new Rectangle(
                (int)((myTexture.Width / 2) * tempRotateTowerX),
                (int)((myTexture.Height / 2) * tempRotateTowerY),
                (int)(myTexture.Width / 2),
                (int)(myTexture.Height / 2));
        }

        private void Attack(GameTime aGameTime, List<Tuple<Enemy, float>> someEnemies)
        {
            myProperties.AttackRate -= (float)aGameTime.ElapsedGameTime.TotalSeconds * GameInfo.GameSpeed;
            if (myProperties.AttackRate <= 0)
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

                    ParticleManager.AddParticle(new Laser(Vector2.Zero, Point.Zero, 1.0f, Pen.Purple, tempPositions.ToArray()));
                }

                myProperties.AttackRate = myProperties.AttackRateDelay;
            }
        }

        public override void LoadContent()
        {
            base.SetTexture(this.GetType().Name);

            SourceRect = new Rectangle(0, 0, myTexture.Width / 2, myTexture.Height / 2);
        }
    }
}
