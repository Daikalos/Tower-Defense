using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
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
            this.myProperties.Power = TowerProperties.Tower_00.Power;
            this.myProperties.NumberOfTargets = TowerProperties.Tower_00.NumberOfTargets;

            this.myProperties.Price = TowerProperties.Tower_00.Price;
            this.myProperties.AttackRate_Price = TowerProperties.Tower_00.AttackRate_Price;
            this.myProperties.Range_Price = TowerProperties.Tower_00.Range_Price;
            this.myProperties.Power_Price = TowerProperties.Tower_00.Power_Price;
            this.myProperties.NumberOfTargets_Price = TowerProperties.Tower_00.NumberOfTargets_Price;

            this.myProperties.TowerLevelsMax = new int[]
            {
                TowerProperties.Tower_00.AttackRate_Level_Max,
                TowerProperties.Tower_00.Range_Level_Max,
                TowerProperties.Tower_00.Power_Level_Max,
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
                myEnemies.Clear();

                Rectangle tempRange = new Rectangle(
                    (int)(OffsetPosition.X - (myProperties.Range / 2)),
                    (int)(OffsetPosition.Y - (myProperties.Range / 4)),
                    (int)(myProperties.Range),
                    (int)(myProperties.Range / 2));

                for (int i = 0; i < EnemyManager.Enemies.Count; i++)
                {
                    if (Extensions.PointWithinEllipse(EnemyManager.Enemies[i].OffsetPosition, tempRange))
                    {
                        myEnemies.Add(new Tuple<Enemy, float>(EnemyManager.Enemies[i],
                            EnemyManager.Enemies[i].DistanceTraveled));
                    }
                }

                myEnemies = myEnemies.OrderByDescending(d => d.Item2).ToList();

                if (myEnemies.Count > 0)
                {
                    float tempAngle = Extensions.AngleToPoint(OffsetPosition, myEnemies.First().Item1.OffsetPosition) + 180.0f; //180 to match spritesheet

                    RotateTower(tempAngle);
                    Action(aGameTime);
                }
                else
                {
                    myProperties.AttackRate = myProperties.AttackRateDelay; //Reset attack if no enemies to within distance
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

        protected override void Action(GameTime aGameTime)
        {
            myProperties.AttackRate -= (float)aGameTime.ElapsedGameTime.TotalSeconds * GameInfo.GameSpeed;
            if (myProperties.AttackRate <= 0)
            {
                if (myEnemies.Count > 0)
                {
                    List<Vector2> tempPositions = new List<Vector2>();
                    tempPositions.Add(OffsetPosition);

                    for (int i = 0; i < myProperties.NumberOfTargets; i++)
                    {
                        if (i < myEnemies.Count)
                        {
                            tempPositions.Add(myEnemies[i].Item1.DestRect.Center.ToVector2());

                            myEnemies[i].Item1.RecieveDamage(myProperties.Power);
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
