using System;
using Microsoft.Xna.Framework;

namespace Tower_Defense
{
    static class Camera
    {
        private static Vector2 myPosition;

        public static Vector2 Position
        {
            get => myPosition;
            set => myPosition = value;
        }
        public static Matrix TranslationMatrix
        {
            get
            {
                return Matrix.CreateTranslation(new Vector3(-myPosition, 0));
            }
        }

        public static void Reset()
        {
            myPosition = Vector2.Zero;
        }

        public static void MoveCamera(GameWindow aWindow, float aNewPosition)
        {
            if (SnapToMap(aWindow, aNewPosition))
            {
                myPosition.X += aNewPosition;
            }
        }

        private static bool SnapToMap(GameWindow aWindow, float aNewPosition)
        {
            if (myPosition.X + aNewPosition + aWindow.ClientBounds.Width > Level.MapSize.X)
            {
                myPosition.X = Level.MapSize.X - aWindow.ClientBounds.Width;
                return false;
            }
            if (myPosition.X + aNewPosition < 0)
            {
                myPosition.X = 0;
                return false;
            }
            if (myPosition.Y + aNewPosition <= 0)
            {
                myPosition.Y = 0;
                return false;
            }
            if (myPosition.Y + aNewPosition + aWindow.ClientBounds.Height > Level.MapSize.Y)
            {
                myPosition.Y = Level.MapSize.Y - aWindow.ClientBounds.Height;
            }
            return true;
        }
    }
}
