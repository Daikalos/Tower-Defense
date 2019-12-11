﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
/*
namespace Tower_Defense
{
    static class EnemyManager
    {
        static List<Enemy> myEnemies;

        public static List<Enemy> Enemies
        {
            get => myEnemies;
        }

        public static void Initialize()
        {
            myEnemies = new List<Enemy>();
        }

        public static void Update(GameTime aGameTime)
        {
            for (int i = myEnemies.Count - 1; i >= 0; i--)
            {
                myEnemies[i].Update(aGameTime);
                if (!myEnemies[i].IsAlive)
                {
                    myEnemies.RemoveAt(i);
                }
            }
        }

        public static void Draw(SpriteBatch aSpriteBatch, GameTime aGameTime)
        {
            for (int i = myEnemies.Count - 1; i >= 0; i--)
            {
                myEnemies[i].Draw(aSpriteBatch, aGameTime);
            }
        }

        public static void AddChaseEnemy(Vector2 aPos)
        {
            myEnemies?.Add(new Chase(aPos, new Point(32, 48), new Vector2(1.0f, 0.0f), new Vector2(1.0f, 5.0f)));
        }
        public static void AddPatrolEnemy(Vector2 aPos)
        {
            myEnemies?.Add(new Patrol(aPos, new Point(32), new Vector2(1.0f, 0.0f), new Vector2(1.0f, 5.0f)));
        }
        public static void RemoveAll()
        {
            myEnemies.RemoveAll(e => e.IsAlive);
        }

        public static void SetTexture()
        {
            foreach (Enemy enemy in myEnemies)
            {
                if (enemy is Chase)
                {
                    enemy.SetTexture("Koopa_Walking");
                }
                if (enemy is Patrol)
                {
                    enemy.SetTexture("Goomba_Walking");
                }
            }
        }
    }
}
*/