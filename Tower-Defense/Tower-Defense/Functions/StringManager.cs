using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense
{
    static class StringManager
    {
        private static List<DrawString> myDrawStrings;

        public static List<DrawString> DrawStrings
        {
            get => myDrawStrings;
            set => myDrawStrings = value;
        }

        public static void Initialize()
        {
            myDrawStrings = new List<DrawString>();
        }

        public static void Update(GameTime aGameTime)
        {
            for (int i = myDrawStrings.Count - 1; i >= 0; i--)
            {
                if (myDrawStrings[i].Delay > 0)
                {
                    myDrawStrings[i].Delay -= (float)aGameTime.ElapsedGameTime.TotalSeconds;
                }
                else
                {
                    myDrawStrings.RemoveAt(i);
                }
            }
        }

        public static void Draw(SpriteBatch aSpriteBatch, SpriteFont aFont)
        {
            for (int i = myDrawStrings.Count - 1; i >= 0; i--)
            {
                if (myDrawStrings[i].DrawWithCamera)
                {
                    switch(myDrawStrings[i].ReferencePoint)
                    {
                        case 0:
                            StringManager.CameraDrawStringLeft(aSpriteBatch, aFont, myDrawStrings[i].String, myDrawStrings[i].Position, myDrawStrings[i].Color, myDrawStrings[i].Size);
                            break;
                        case 1:
                            StringManager.CameraDrawStringMid(aSpriteBatch, aFont, myDrawStrings[i].String, myDrawStrings[i].Position, myDrawStrings[i].Color, myDrawStrings[i].Size);
                            break;
                        case 2:
                            StringManager.CameraDrawStringRight(aSpriteBatch, aFont, myDrawStrings[i].String, myDrawStrings[i].Position, myDrawStrings[i].Color, myDrawStrings[i].Size);
                            break;
                    }
                }
                else
                {
                    switch (myDrawStrings[i].ReferencePoint)
                    {
                        case 0:
                            StringManager.DrawStringLeft(aSpriteBatch, aFont, myDrawStrings[i].String, myDrawStrings[i].Position, myDrawStrings[i].Color, myDrawStrings[i].Size);
                            break;
                        case 1:
                            StringManager.DrawStringMid(aSpriteBatch, aFont, myDrawStrings[i].String, myDrawStrings[i].Position, myDrawStrings[i].Color, myDrawStrings[i].Size);
                            break;
                        case 2:
                            StringManager.DrawStringRight(aSpriteBatch, aFont, myDrawStrings[i].String, myDrawStrings[i].Position, myDrawStrings[i].Color, myDrawStrings[i].Size);
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Draw a string with the farthest left point of the string as reference (standard, but includes Y correction as origin)
        /// </summary>
        public static void DrawStringLeft(SpriteBatch aSpriteBatch, SpriteFont aFont, string aString, Vector2 aPosition, Color aColor, float aSize)
        {
            if (aFont != null)
            {
                aSpriteBatch.DrawString(aFont, aString, new Vector2(
                    (aPosition.X),
                    (aPosition.Y - (aFont.MeasureString(aString).Y / 2) * aSize)), aColor, 0.0f, Vector2.Zero, aSize, SpriteEffects.None, 0.0f);
            }
        }

        /// <summary>
        /// Draw a string with the middle point of the string as reference
        /// </summary>
        public static void DrawStringMid(SpriteBatch aSpriteBatch, SpriteFont aFont, string aString, Vector2 aPosition, Color aColor, float aSize)
        {
            if (aFont != null)
            {
                aSpriteBatch.DrawString(aFont, aString, new Vector2(
                    (aPosition.X - (aFont.MeasureString(aString).X / 2) * aSize),
                    (aPosition.Y - (aFont.MeasureString(aString).Y / 2) * aSize)), aColor, 0.0f, Vector2.Zero, aSize, SpriteEffects.None, 0.0f);
            }
        }

        /// <summary>
        /// Draw a string with the farthest right point of the string as reference
        /// </summary>
        public static void DrawStringRight(SpriteBatch aSpriteBatch, SpriteFont aFont, string aString, Vector2 aPosition, Color aColor, float aSize)
        {
            if (aFont != null)
            {
                aSpriteBatch.DrawString(aFont, aString, new Vector2(
                    (aPosition.X) - (aFont.MeasureString(aString).X * aSize),
                    (aPosition.Y) - (aFont.MeasureString(aString).Y / 2) * aSize), aColor, 0.0f, Vector2.Zero, aSize, SpriteEffects.None, 0.0f);
            }
        }

        /// <summary>
        /// Draw a string with the farthest left point of the string as reference (standard, but includes Y correction as origin) using the camera
        /// </summary>
        public static void CameraDrawStringLeft(SpriteBatch aSpriteBatch, SpriteFont aFont, string aString, Vector2 aPosition, Color aColor, float aSize)
        {
            if (aFont != null)
            {
                aSpriteBatch.DrawString(aFont, aString, Camera.TopLeftCorner + new Vector2(
                    (aPosition.X / Camera.Zoom), 
                    (aPosition.Y / Camera.Zoom) - (aFont.MeasureString(aString).Y / 2) * aSize / Camera.Zoom), aColor, 0.0f, Vector2.Zero, aSize / Camera.Zoom, SpriteEffects.None, 0.0f);
            }
        }

        /// <summary>
        /// Draw a string with the middle point of the string as reference using the camera
        /// </summary>
        public static void CameraDrawStringMid(SpriteBatch aSpriteBatch, SpriteFont aFont, string aString, Vector2 aPosition, Color aColor, float aSize)
        {
            if (aFont != null)
            {
                aSpriteBatch.DrawString(aFont, aString, Camera.TopLeftCorner + new Vector2(
                    (aPosition.X / Camera.Zoom) - (aFont.MeasureString(aString).X / 2) * aSize / Camera.Zoom,
                    (aPosition.Y / Camera.Zoom) - (aFont.MeasureString(aString).Y / 2) * aSize / Camera.Zoom), aColor, 0.0f, Vector2.Zero, aSize / Camera.Zoom, SpriteEffects.None, 0.0f);
            }
        }

        /// <summary>
        /// Draw a string with the farthest right point of the string as reference using the camera
        /// </summary>
        public static void CameraDrawStringRight(SpriteBatch aSpriteBatch, SpriteFont aFont, string aString, Vector2 aPosition, Color aColor, float aSize)
        {
            if (aFont != null)
            {
                aSpriteBatch.DrawString(aFont, aString, Camera.TopLeftCorner + new Vector2(
                    (aPosition.X / Camera.Zoom) - (aFont.MeasureString(aString).X) * aSize / Camera.Zoom,
                    (aPosition.Y / Camera.Zoom) - (aFont.MeasureString(aString).Y / 2) * aSize / Camera.Zoom), aColor, 0.0f, Vector2.Zero, aSize / Camera.Zoom, SpriteEffects.None, 0.0f);
            }
        }
    }

    class DrawString
    {
        private Vector2 myPosition;
        private Color myColor;
        private bool myDrawWithCamera;
        private float 
            myDelay,
            mySize;
        private int myReferencePoint;
        private string myString;

        public Vector2 Position
        {
            get => myPosition;
        }
        public Color Color
        {
            get => myColor;
        }
        public bool DrawWithCamera
        {
            get => myDrawWithCamera;
        }
        public float Delay
        {
            get => myDelay;
            set => myDelay = value;
        }
        public float Size
        {
            get => mySize;
        }
        public int ReferencePoint
        {
            get => myReferencePoint;
        }
        public string String
        {
            get => myString;
        }

        public DrawString(Vector2 aPosition, Color aColor, bool aDrawWithCamera, float aDelay, float aSize, int aReferencePoint, string aString)
        {
            this.myPosition = aPosition;
            this.myColor = aColor;
            this.myDrawWithCamera = aDrawWithCamera;
            this.myDelay = aDelay;
            this.mySize = aSize;
            this.myReferencePoint = aReferencePoint;
            this.myString = aString;
        }
    }
}
