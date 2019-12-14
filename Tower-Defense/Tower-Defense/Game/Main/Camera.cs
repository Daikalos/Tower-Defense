using Microsoft.Xna.Framework;

namespace Tower_Defense
{
    static class Camera
    {
        private static Vector2 
            myPosition,
            myViewportSize;
        private static Point myOldMousePosition;
        private static float myZoom;

        public static Vector2 Position
        {
            get => myPosition;
            set => myPosition = value;
        }
        public static Vector2 TopLeftCorner
        {
            get => myPosition - ViewportCenter * (1 / myZoom);
        }
        public static Vector2 ViewportCenter
        {
            get => new Vector2(myViewportSize.X / 2, myViewportSize.Y / 2);
        }
        public static float Zoom
        {
            get => myZoom;
        }
        public static Matrix TranslationMatrix
        {
            get
            {
                return 
                    Matrix.CreateTranslation(new Vector3(-myPosition, 0)) *               
                    Matrix.CreateScale(myZoom, myZoom, 1.0f) *
                    Matrix.CreateTranslation(new Vector3(ViewportCenter, 0));
            }
        }

        public static void Initialize(GameWindow aWindow, Vector2 aPosition)
        {
            myPosition = aPosition;
            myViewportSize = aWindow.ClientBounds.Size.ToVector2();
            myOldMousePosition = Point.Zero;
            myZoom = 1.0f;
        }

        public static void MoveCamera()
        {
            if (KeyMouseReader.MiddleMouseClick())
            {
                myOldMousePosition = ViewToWorld(KeyMouseReader.CurrentMouseState.Position.ToVector2()).ToPoint();
            }
            if (KeyMouseReader.MiddleMouseHold() && myOldMousePosition != Point.Zero)
            {
                Point tempNewPos = ViewToWorld(KeyMouseReader.CurrentMouseState.Position.ToVector2()).ToPoint();
                Point tempDeltaPos = myOldMousePosition - tempNewPos;

                myPosition += tempDeltaPos.ToVector2();

                myOldMousePosition = ViewToWorld(KeyMouseReader.CurrentMouseState.Position.ToVector2()).ToPoint();
            }

            if (KeyMouseReader.ScrollUp())
            {
                myZoom += 0.05f;
                myZoom = MathHelper.Clamp(myZoom, 0.5f, 2.0f);
            }
            if (KeyMouseReader.ScrollDown())
            {
                myZoom -= 0.05f;
                myZoom = MathHelper.Clamp(myZoom, 0.5f, 2.0f);
            }
        }

        public static Vector2 ViewToWorld(Vector2 aPosition)
        {
            return Vector2.Transform(aPosition, Matrix.Invert(TranslationMatrix));
        }
    }
}
