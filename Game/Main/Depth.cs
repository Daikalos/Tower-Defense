﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense
{
    static class Depth
    {
        private static List<GameObject> myObjects;

        public static float ObjectDepth(GameObject aObject)
        {
            float tempDepth = 1.0f / (aObject.Position.Y + aObject.Size.Y * 0.9f);
            tempDepth = tempDepth > 0 ? tempDepth : 0;

            return tempDepth;
        }

        public static void Initialize()
        {
            myObjects = new List<GameObject>();
        }

        public static void AddObject(params GameObject[] someObjects)
        {
            myObjects.AddRange(someObjects);
        }

        public static void RemoveObject(params GameObject[] someObjects)
        {
            foreach (GameObject obj in someObjects)
            {
                myObjects.Remove(obj);
            }
        }

        public static void Draw(SpriteBatch aSpriteBatch, GameTime aGameTime)
        {
            myObjects.ForEach(o => o.DrawWithDepth(aSpriteBatch, aGameTime, ObjectDepth(o)));
        }
    }
}
