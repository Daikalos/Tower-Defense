using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense
{
    static class Depth
    {
        private static List<GameObject> myObjects;

        public static List<GameObject> Objects
        {
            get => myObjects;
        }

        public static void Initialize()
        {
            myObjects = new List<GameObject>();
        }

        public static void AddObject(params GameObject[] someObject)
        {
            myObjects.AddRange(someObject);
        }

        public static void RemoveObject(params GameObject[] someObject)
        {
            foreach (GameObject obj in someObject)
            {
                myObjects.Remove(obj);
            }
        }

        public static void Draw(SpriteBatch aSpriteBatch, GameTime aGameTime)
        {
            myObjects.ForEach(o => o.DrawWithDepth(aSpriteBatch, aGameTime, 1.0f / (o.Position.Y + o.Size.Y * 1.2f)));
        }
    }
}
