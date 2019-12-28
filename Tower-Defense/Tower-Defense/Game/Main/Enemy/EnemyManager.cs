using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense
{
    static class EnemyManager
    {
        private static List<Enemy> myEnemies;

        public static List<Enemy> Enemies
        {
            get => myEnemies;
        }

        public static void Initialize()
        {
            myEnemies = new List<Enemy>();
        }

        public static void AddEnemy(int aType)
        {
            Enemy tempEnemy;
            switch(aType)
            {
                case 0:
                    tempEnemy = new Enemy_00(GameInfo.Path[0].GetCenter(), new Point(64));

                    myEnemies.Add(tempEnemy);
                    Depth.AddObject(tempEnemy);

                    tempEnemy.LoadContent();
                    break;
                case 1:
                    tempEnemy = new Enemy_01(GameInfo.Path[0].GetCenter(), new Point(64));

                    myEnemies.Add(tempEnemy);
                    Depth.AddObject(tempEnemy);

                    tempEnemy.LoadContent();
                    break;
                case 2:
                    tempEnemy = new Enemy_02(GameInfo.Path[0].GetCenter(), new Point(64));

                    myEnemies.Add(tempEnemy);
                    Depth.AddObject(tempEnemy);

                    tempEnemy.LoadContent();
                    break;
                case 3:
                    tempEnemy = new Enemy_03(GameInfo.Path[0].GetCenter(), new Point(64));

                    myEnemies.Add(tempEnemy);
                    Depth.AddObject(tempEnemy);

                    tempEnemy.LoadContent();
                    break;
            }
        }

        public static void Update(GameTime aGameTime)
        {
            for (int i = myEnemies.Count - 1; i >= 0; i--)
            {
                myEnemies[i].Update(aGameTime);
                if (!myEnemies[i].IsAlive)
                {
                    Depth.RemoveObject(myEnemies[i]);
                    myEnemies.RemoveAt(i);
                }
            }
        }

        public static void Draw(SpriteBatch aSpriteBatch)
        {
            for (int i = 0; i < myEnemies.Count; i++)
            {
                myEnemies[i].Draw(aSpriteBatch);
            }
        }

        public static void RemoveAll()
        {
            myEnemies.RemoveAll(e => e.IsAlive);
        }

        public static void SetTexture()
        {
            foreach (Enemy enemy in myEnemies)
            {
                enemy.LoadContent();
            }
        }
    }
}