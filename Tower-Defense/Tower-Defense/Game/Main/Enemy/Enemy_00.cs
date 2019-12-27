using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense
{
    class Enemy_00 : Enemy
    {
        public Enemy_00(Vector2 aPosition, Point aSize) : base(aPosition, aSize)
        {
            this.myProperties.Speed = EnemyProperties.Enemy_00.Speed;
            this.myProperties.HealthPoints = EnemyProperties.Enemy_00.HealthPoints;

            this.myMaxHealthPoints = myProperties.HealthPoints;

            this.myEnemyAnimation = new AnimationManager(new Point(16, 4), 0.12f, true);
            this.myOffset = new Vector2(-(aSize.X / 2), -(aSize.Y - (Level.TileSize.Y / 2)) - 8);

            this.myPosition += myOffset;
        }

        public override void DrawWithDepth(SpriteBatch aSpriteBatch, GameTime aGameTime, float aDepth)
        {
            myEnemyAnimation.DrawByRow(aSpriteBatch, aGameTime, myTexture, myDestRect, new Point(64), Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, aDepth, myDirection);
        }
    }
}
