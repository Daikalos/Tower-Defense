using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense
{
    abstract class Tower : StaticObject
    {
        protected Tower_Properties myProperties;

        protected List<Tuple<Enemy, float>> myEnemies; //Enemies to sort by specified order to perform defined action
        protected List<Tuple<Tower, float>> myTowers; //Towers to sort by specified order to perform defined action

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
            this.myEnemies = new List<Tuple<Enemy, float>>();
            this.myTowers = new List<Tuple<Tower, float>>();

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

            this.myProperties.AttackRateLevel = myProperties.TowerLevels[0];
            this.myProperties.RangeLevel = myProperties.TowerLevels[1];
            this.myProperties.PowerLevel = myProperties.TowerLevels[2];
            this.myProperties.NumberOfTargetsLevel = myProperties.TowerLevels[3];
        }

        public virtual void Update(GameTime aGameTime) //Template for manager
        {
            base.Update();

            myProperties.TowerLevels[0] = myProperties.AttackRateLevel;
            myProperties.TowerLevels[1] = myProperties.RangeLevel;
            myProperties.TowerLevels[2] = myProperties.PowerLevel;
            myProperties.TowerLevels[3] = myProperties.NumberOfTargetsLevel;

            IsClicked();
        }

        public bool IsClicked()
        {
            if (KeyMouseReader.LeftClick())
            {
                Tile tempTile1 = Level.TileAtPos(Camera.ViewToWorld(KeyMouseReader.MousePos)).Item1;
                Tile tempTile2 = Level.TileAtPos(myOffsetPosition).Item1;

                if (tempTile1 == tempTile2)
                {
                    return true;
                }
            }
            return false;
        }

        protected abstract void Action(GameTime aGameTime);

        public abstract void LoadContent();

        public class Tower_Properties
        {
            public string Name { get; set; }

            public float AttackRate { get; set; }
            public float AttackRateDelay { get; set; }
            public float Range { get; set; }
            public int Power { get; set; }
            public int NumberOfTargets { get; set; }

            public int[] TowerLevels { get; set; }
            public int[] TowerLevelsMax { get; set; }

            public int AttackRateLevel { get; set; }
            public int RangeLevel { get; set; }
            public int PowerLevel { get; set; }
            public int NumberOfTargetsLevel { get; set; }

            public int Price { get; set; }
            public int AttackRate_Price { get; set; }
            public int Range_Price { get; set; }
            public int Power_Price { get; set; }
            public int NumberOfTargets_Price { get; set; }

        }
    }
}
