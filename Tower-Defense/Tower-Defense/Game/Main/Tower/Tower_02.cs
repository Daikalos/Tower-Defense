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

            this.myProperties.FireSpeed = TowerProperties.Tower_02.FireSpeed;
            this.myProperties.Range = TowerProperties.Tower_02.Range;
            this.myProperties.Damage = TowerProperties.Tower_02.Damage;
            this.myProperties.NumberOfTargets = TowerProperties.Tower_02.NumberOfTargets;

            this.myProperties.Price = TowerProperties.Tower_02.Price;
            this.myProperties.FireSpeed_Price = TowerProperties.Tower_02.FireSpeed_Price;
            this.myProperties.Range_Price = TowerProperties.Tower_02.Range_Price;
            this.myProperties.Damage_Price = TowerProperties.Tower_02.Damage_Price;
            this.myProperties.NumberOfTargets_Price = TowerProperties.Tower_02.NumberOfTargets_Price;

            this.myProperties.TowerLevelsMax = new int[]
            {
                TowerProperties.Tower_02.FireSpeed_Level_Max,
                TowerProperties.Tower_02.Range_Level_Max,
                TowerProperties.Tower_02.Damage_Level_Max,
                TowerProperties.Tower_02.NumberOfTargets_Level_Max
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
                    Attack(aGameTime, tempSortedList);
                }
            }
        }

        private void Attack(GameTime aGameTime, List<Tuple<Enemy, float>> someEnemies)
        {
            myProperties.FireSpeed -= (float)aGameTime.ElapsedGameTime.TotalSeconds * GameInfo.GameSpeed;
            if (myProperties.FireSpeed <= 0)
            {
                if (someEnemies.Count > 0)
                {
                    Vector2[] tempPositions = new Vector2[someEnemies.Count + 1];
                    tempPositions[0] = OffsetPosition;

                    for (int i = 0; i < myProperties.NumberOfTargets; i++)
                    {
                        if (i < someEnemies.Count)
                        {
                            tempPositions[i + 1] = someEnemies[i].Item1.DestRect.Center.ToVector2();

                            someEnemies[i].Item1.RecieveDamage(myProperties.Damage);
                        }
                    }

                    Vector2[] tempFilteredArray = Array.FindAll(tempPositions, p => p != Vector2.Zero && p != null); //Filter out empty positions

                    ParticleManager.AddParticle(new Laser(Vector2.Zero, Point.Zero, Pen.Purple, 1.0f, tempFilteredArray));
                }

                myProperties.FireSpeed = myProperties.FireSpeedDelay;
            }
        }

        public override void LoadContent()
        {
            base.SetTexture(this.GetType().Name);
        }
    }
}
