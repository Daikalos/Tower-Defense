using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LilyPath;

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

        public static void Update(GameTime aGameTime, UpgradeManager aUpgrade)
        {
            for (int i = myTowers.Count - 1; i >= 0; i--)
            {
                myTowers[i].Update(aGameTime);
                if (myTowers[i].IsClicked())
                {
                    aUpgrade.SelectedTower = myTowers[i];
                }

                if (!myTowers[i].IsAlive)
                {
                    Level.TileAtPos(myTowers[i].OffsetPosition).Item1.IsObstacle = false;

                    Depth.RemoveObject(myTowers[i]);
                    myTowers.Remove(myTowers[i]);
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

        public static void AddTower(int aType, Vector2 aPosition)
        {
            Tower aTower = null;

            switch (aType)
            {
                case 0:
                    aTower = new Tower_00(aPosition, new Point(64));
                    break;
                case 1:
                    aTower = new Tower_01(aPosition, new Point(64));
                    break;
                case 2:
                    aTower = new Tower_02(aPosition, new Point(64, 80));
                    break;
                default:
                    aTower = new Tower_00(aPosition, new Point(64));
                    break;
            }

            Depth.AddObject(aTower);
            myTowers.Add(aTower);

            aTower.LoadContent();
        }
        public static void RemoveAll()
        {
            myTowers.RemoveAll(e => e.IsAlive);
        }

        public static void SetTexture()
        {
            foreach (Tower tower in myTowers)
            {
                tower.SetTexture(tower.GetType().Name);
            }
        }
    }
}
