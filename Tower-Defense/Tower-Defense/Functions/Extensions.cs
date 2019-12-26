using System;
using Microsoft.Xna.Framework;

namespace Tower_Defense
{
    static class Extensions
    {
        public static string NumberFormat(int aNumber)
        {
            if (aNumber < 10)
            {
                return "0" + aNumber;
            }
            return aNumber.ToString();
        }

        public static Vector2 Normalize(Vector2 aVector)
        {
            if (aVector != Vector2.Zero)
            {
                aVector.Normalize();
                return aVector;
            }
            return new Vector2();
        }

        /// <summary>
        /// In degrees 0-360
        /// </summary>
        public static float AngleToPoint(Vector2 aPointA, Vector2 aPointB)
        {
            return (float)Math.Atan2((aPointB.Y - aPointA.Y), (aPointB.X - aPointA.X)) * (180.0f / (float)Math.PI);
        }
    }
}
