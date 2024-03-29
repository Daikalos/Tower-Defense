﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense
{
    static class StringManager
    {
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
}
