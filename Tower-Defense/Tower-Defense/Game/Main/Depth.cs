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

        public static void AddObject(params GameObject[] aObject)
        {
            myObjects.AddRange(aObject);
        }

        public static void Draw(SpriteBatch aSpriteBatch, GameTime aGameTime)
        {
            myObjects.ForEach(o => o.DrawWithDepth(aSpriteBatch, aGameTime, 1 / (o.Position.Y + o.Size.Y)));
        }
    }
}
