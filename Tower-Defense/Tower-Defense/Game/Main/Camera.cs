using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Tower_Defense
{
    static class Camera
    {
        private static Vector2 
            myPosition,
            myViewportSize,
            myZoomLimit;
        private static Point myOldMousePosition;
        private static float 
            myMoveSpeed,
            myZoom,
            myZoomValue;

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

        public static void Initialize(GameWindow aWindow, Vector2 aPosition, float aSpeed)
        {
            myPosition = aPosition;
            myViewportSize = aWindow.ClientBounds.Size.ToVector2();
            myMoveSpeed = aSpeed;
            myOldMousePosition = Point.Zero;
            myZoomLimit = new Vector2(0.5f, 2.0f);
            myZoom = 1.0f;
            myZoomValue = 0.05f;
        }

        public static void MoveCamera(GameTime aGameTime)
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
            }

            if (KeyMouseReader.ScrollUp())
            {
                myZoom += myZoomValue;
                myZoom = MathHelper.Clamp(myZoom, myZoomLimit.X, myZoomLimit.Y);
            }
            if (KeyMouseReader.ScrollDown())
            {
                myZoom -= myZoomValue;
                myZoom = MathHelper.Clamp(myZoom, myZoomLimit.X, myZoomLimit.Y);
            }

            if (KeyMouseReader.KeyHold(Keys.Up))
            {
                myPosition.Y -= myMoveSpeed * 60 * (float)aGameTime.ElapsedGameTime.TotalSeconds;
            }
            if (KeyMouseReader.KeyHold(Keys.Down))
            {
                myPosition.Y += myMoveSpeed * 60 * (float)aGameTime.ElapsedGameTime.TotalSeconds;
            }
            if (KeyMouseReader.KeyHold(Keys.Left))
            {
                myPosition.X -= myMoveSpeed * 60 * (float)aGameTime.ElapsedGameTime.TotalSeconds;
            }
            if (KeyMouseReader.KeyHold(Keys.Right))
            {
                myPosition.X += myMoveSpeed * 60 * (float)aGameTime.ElapsedGameTime.TotalSeconds;
            }

            if (KeyMouseReader.KeyHold(Keys.OemPlus))
            {
                myZoom += myZoomValue / 2;
                myZoom = MathHelper.Clamp(myZoom, myZoomLimit.X, myZoomLimit.Y);
            }
            if (KeyMouseReader.KeyHold(Keys.OemMinus))
            {
                myZoom -= myZoomValue / 2;
                myZoom = MathHelper.Clamp(myZoom, myZoomLimit.X, myZoomLimit.Y);
            }
        }

        public static Vector2 ViewToWorld(Vector2 aPosition)
        {
            return Vector2.Transform(aPosition, Matrix.Invert(TranslationMatrix));
        }
    }
}
