using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LilyPath;

namespace Tower_Defense
{
    class Tower_02 : Tower
    {
        public Tower_02(Vector2 aPosition, Point aSize) : base(aPosition, aSize)
        {
            this.Properties.Name = TowerProperties.Tower_02.Name;

            this.myProperties.AttackRate = TowerProperties.Tower_02.AttackRate;
            this.myProperties.Range = TowerProperties.Tower_02.Range;
            this.myProperties.Damage = TowerProperties.Tower_02.Damage;
            this.myProperties.NumberOfTargets = TowerProperties.Tower_02.NumberOfTargets;

            this.myProperties.Price = TowerProperties.Tower_02.Price;
            this.myProperties.AttackRate_Price = TowerProperties.Tower_02.AttackRate_Price;
            this.myProperties.Range_Price = TowerProperties.Tower_02.Range_Price;
            this.myProperties.Damage_Price = TowerProperties.Tower_02.Damage_Price;
            this.myProperties.NumberOfTargets_Price = TowerProperties.Tower_02.NumberOfTargets_Price;

            this.myProperties.TowerLevelsMax = new int[]
            {
                TowerProperties.Tower_02.AttackRate_Level_Max,
                TowerProperties.Tower_02.Range_Level_Max,
                TowerProperties.Tower_02.Damage_Level_Max,
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
                    Attack(aGameTime, tempSortedList);
                }
            }
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
        }
    }
}
