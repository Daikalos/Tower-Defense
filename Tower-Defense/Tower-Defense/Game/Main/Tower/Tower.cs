﻿using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense
{
    abstract class Tower : StaticObject
    {
        protected Tower_Properties myProperties;

        private Vector2 myOffsetPosition; //Middle of tile
        private bool myIsAlive; //Incase if tower is sold

        public Tower_Properties Properties
        {
            get => myProperties;
            set => myProperties = value;
        }

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

            this.myProperties = new Tower_Properties();

            this.myProperties.TowerLevels = new int[]
            {
                1, 1, 1, 1
            };

            this.myProperties.FireSpeedLevel = myProperties.TowerLevels[0];
            this.myProperties.RangeLevel = myProperties.TowerLevels[1];
            this.myProperties.DamageLevel = myProperties.TowerLevels[2];
            this.myProperties.NumberOfTargetsLevel = myProperties.TowerLevels[3];
        }

        public virtual void Update(GameTime aGameTime) //Template for manager
        {
            base.Update();

            myProperties.TowerLevels[0] = myProperties.FireSpeedLevel;
            myProperties.TowerLevels[1] = myProperties.RangeLevel;
            myProperties.TowerLevels[2] = myProperties.DamageLevel;
            myProperties.TowerLevels[3] = myProperties.NumberOfTargetsLevel;

            IsClicked();
            Attack(aGameTime);
        }

        private void Attack(GameTime aGameTime)
        {
            myProperties.FireSpeed -= (float)aGameTime.ElapsedGameTime.TotalSeconds;
            if (myProperties.FireSpeed <= 0)
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

                if (tempDistToEnemy.Length > 0)
                {
                    Tuple<Enemy, float>[] tempFilteredArray = Array.FindAll(tempDistToEnemy, d => d.Item2 != float.MaxValue);
                    Tuple<Enemy, float>[] tempSortedArray = tempFilteredArray.OrderBy(d => d.Item2).ToArray();

                    if (tempSortedArray.Length > 0)
                    {
                        for (int i = 0; i < myProperties.NumberOfTargets; i++)
                        {
                            if (i < tempSortedArray.Length)
                            {
                                tempSortedArray[i].Item1.Properties.HealthPoints -= myProperties.Damage;
                            }
                        }
                    }
                }

                myProperties.FireSpeed = myProperties.FireSpeedDelay;
            }
        }

        public bool IsClicked()
        {
            if (KeyMouseReader.LeftClick())
            {
                Tile tempTile1 = Level.TileAtPos(Camera.ViewToWorld(KeyMouseReader.CurrentMouseState.Position.ToVector2())).Item1;
                Tile tempTile2 = Level.TileAtPos(myOffsetPosition).Item1;

                if (tempTile1 == tempTile2)
                {
                    return true;
                }
            }
            return false;
        }

        public abstract void LoadContent();

        public class Tower_Properties
        {
            public string Name { get; set; }

            public float FireSpeed { get; set; }
            public float FireSpeedDelay { get; set; }
            public float Range { get; set; }
            public int Damage { get; set; }
            public int NumberOfTargets { get; set; }

            public int[] TowerLevels { get; set; }
            public int FireSpeedLevel { get; set; }
            public int RangeLevel { get; set; }
            public int DamageLevel { get; set; }
            public int NumberOfTargetsLevel { get; set; }

            public int FireSpeed_Price { get; set; }
            public int Range_Price { get; set; }
            public int Damage_Price { get; set; }
            public int NumberOfTargets_Price { get; set; }
        }
    }
}