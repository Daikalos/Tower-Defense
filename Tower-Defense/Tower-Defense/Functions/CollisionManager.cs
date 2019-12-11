using Microsoft.Xna.Framework;

namespace Tower_Defense
{
    static class CollisionManager
    {
        public static bool Collision(Rectangle aRectangle1, Rectangle aRectangle2)
        {
            return aRectangle1.Intersects(aRectangle2);
        }

        public static bool CheckAbove(GameObject aObject1, GameObject aObject2, Vector2 aVelocity)
        {
            return
                aObject1.BoundingBox.Top + aVelocity.Y < aObject2.BoundingBox.Bottom &&
                aObject1.BoundingBox.Bottom > aObject2.BoundingBox.Bottom &&
                aObject1.BoundingBox.Right > aObject2.BoundingBox.Left &&
                aObject1.BoundingBox.Left < aObject2.BoundingBox.Right;
        }

        public static bool CheckBelow(GameObject aObject1, GameObject aObject2, Vector2 aVelocity)
        {
            return 
                aObject1.BoundingBox.Bottom + aVelocity.Y > aObject2.BoundingBox.Top &&
                aObject1.BoundingBox.Top < aObject2.BoundingBox.Top &&
                aObject1.BoundingBox.Right > aObject2.BoundingBox.Left &&
                aObject1.BoundingBox.Left < aObject2.BoundingBox.Right;
        }

        public static bool CheckLeft(GameObject aObject1, GameObject aObject2, Vector2 aVelocity)
        {
            return
                aObject1.BoundingBox.Left + aVelocity.X < aObject2.BoundingBox.Right &&
                aObject1.BoundingBox.Right > aObject2.BoundingBox.Right &&
                aObject1.BoundingBox.Bottom > aObject2.BoundingBox.Top &&
                aObject1.BoundingBox.Top < aObject2.BoundingBox.Bottom;
        }

        public static bool CheckRight(GameObject aObject1, GameObject aObject2, Vector2 aVelocity)
        {
            return
                aObject1.BoundingBox.Right + aVelocity.X > aObject2.BoundingBox.Left &&
                aObject1.BoundingBox.Left < aObject2.BoundingBox.Left &&
                aObject1.BoundingBox.Bottom > aObject2.BoundingBox.Top &&
                aObject1.BoundingBox.Top < aObject2.BoundingBox.Bottom;
        }
    }
}
