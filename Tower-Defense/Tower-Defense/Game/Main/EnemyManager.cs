﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
                myEnemies[i].Update();
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
                
            }
        }
    }
}