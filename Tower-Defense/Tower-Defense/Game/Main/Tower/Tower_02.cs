using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using LilyPath;

namespace Tower_Defense
{
    class Tower_02 : Tower
    {
        public Tower_02(Vector2 aPosition, Point aSize) : base(aPosition, aSize)
        {
            this.myProperties.Name = TowerProperties.Tower_02.Name;

            this.myProperties.AttackRate = TowerProperties.Tower_02.AttackRate;
            this.myProperties.Range = TowerProperties.Tower_02.Range;
            this.myProperties.Power = TowerProperties.Tower_02.Power;
            this.myProperties.NumberOfTargets = TowerProperties.Tower_02.NumberOfTargets;

            this.myProperties.Price = TowerProperties.Tower_02.Price;
            this.myProperties.AttackRate_Price = TowerProperties.Tower_02.AttackRate_Price;
            this.myProperties.Range_Price = TowerProperties.Tower_02.Range_Price;
            this.myProperties.Power_Price = TowerProperties.Tower_02.Power_Price;
            this.myProperties.NumberOfTargets_Price = TowerProperties.Tower_02.NumberOfTargets_Price;

            this.myProperties.TowerLevelsMax = new int[]
            {
                TowerProperties.Tower_02.AttackRate_Level_Max,
                TowerProperties.Tower_02.Range_Level_Max,
                TowerProperties.Tower_02.Power_Level_Max,
                TowerProperties.Tower_02.NumberOfTargets_Level_Max
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
                    Action(aGameTime);
                }
                else
                {
                    myProperties.AttackRate = myProperties.AttackRateDelay; //Reset attack if no enemies to within distance
                }
            }
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

                            myEnemies[i].Item1.ResetSpeedTimer = myProperties.Power;

                            List<Spark> tempSparks = new List<Spark>();
                            for (int j = 0; j < StaticRandom.RandomNumber(2, 5); j++)
                            {
                                tempSparks.Add(new Spark(
                                    new Vector2(myEnemies[i].Item1.OffsetPosition.X, myEnemies[i].Item1.OffsetPosition.Y - (myEnemies[i].Item1.DestRect.Height / 2)),
                                    new Point(2, 4)));
                            }
                            ParticleManager.AddParticle(tempSparks.ToArray());
                        }
                    }

                    ParticleManager.AddParticle(new Laser(Vector2.Zero, Point.Zero, 1.0f, Pen.White, tempPositions.ToArray()));
                }

                myProperties.AttackRate = myProperties.AttackRateDelay;
            }
        }

        public override void LoadContent()
        {
            base.SetTexture(this.GetType().Name);
        }
    }
}
