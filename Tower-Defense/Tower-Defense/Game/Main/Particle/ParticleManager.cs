using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LilyPath;

namespace Tower_Defense
{
    static class ParticleManager
    {
        private static List<Particle> myParticles;
        private static DrawBatch myDrawBatch;

        public static void Initialize(GraphicsDevice aGraphicsDevice)
        {
            myParticles = new List<Particle>();
            myDrawBatch = new DrawBatch(aGraphicsDevice);
        }

        public static void AddParticle(Particle aParticle)
        {
            Depth.AddObject(aParticle);
            myParticles.Add(aParticle);

            aParticle.LoadContent();
        }

        public static void Update(GameTime aGameTime)
        {
            for (int i = myParticles.Count - 1; i >= 0; i--)
            {
                myParticles[i].Update(aGameTime);
                if (!myParticles[i].IsAlive)
                {
                    Depth.RemoveObject(myParticles[i]);
                    myParticles.Remove(myParticles[i]);
                }
            }
        }

        public static void Draw(SpriteBatch aSpriteBatch, GameTime aGameTime)
        {
            myParticles.ForEach(p => p.Draw(aSpriteBatch, myDrawBatch, aGameTime));
        }
    }
}
