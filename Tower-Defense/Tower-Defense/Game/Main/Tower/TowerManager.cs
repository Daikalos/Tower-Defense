using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense
{
    static class TowerManager
    {
        private static List<Tower> myTowers;

        public static List<Tower> Towers
        {
            get => myTowers;
        }

        public static void Initialize()
        {
            myTowers = new List<Tower>();
        }

        public static void AddTower(Tower aTower)
        {
            myTowers.Add(aTower);
            Depth.AddObject(aTower);

            aTower.SetTexture();
        }

        public static void Update(GameTime aGameTime)
        {
            for (int i = myTowers.Count - 1; i >= 0; i--)
            {
                myTowers[i].Update(aGameTime);
                if (!myTowers[i].IsAlive)
                {
                    myTowers.RemoveAt(i);
                }
            }
        }

        public static void Draw(SpriteBatch aSpriteBatch)
        {
            for (int i = 0; i < myTowers.Count; i++)
            {
                myTowers[i].Draw(aSpriteBatch);
            }
        }

        public static void RemoveAll()
        {
            myTowers.RemoveAll(e => e.IsAlive);
        }

        public static void SetTexture()
        {
            foreach (Tower tower in myTowers)
            {
                if (tower is Tower_00)
                {
                    tower.SetTexture("Tower_00");
                }
                if (tower is Tower_01)
                {
                    tower.SetTexture("Tower_01");
                }
                if (tower is Tower_02)
                {
                    tower.SetTexture("Tower_02");
                }
            }
        }
    }
}
