using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower_Defense
{
    static class SpawnManager
    {
        private static int[] Enemy_AmountToSpawn { get; set; }
        private static float[] Enemy_SpawnTimer { get; set; }
        private static float[] Enemy_SpawnDelay { get; set; }

        private static int AmountToSpawn_Increase { get; set; }
        private static float SpawnDelay_Decrease { get; set; }

        public static bool SpawnEnemies { get; set; }
        public static int TotalAmountToSpawn => Enemy_AmountToSpawn.Sum();

        public static void Initialize()
        {
            Enemy_AmountToSpawn = new int[SpawnInfo.Enemy_Amount.Length];
            Enemy_SpawnTimer = new float[SpawnInfo.Enemy_SpawnRate.Length];
            Enemy_SpawnDelay = new float[SpawnInfo.Enemy_SpawnRate.Length];

            SpawnEnemies = false;
        }

        public static void InitiateWave()
        {
            if (!SpawnEnemies)
            {
                SpawnDelay_Decrease = (float)MathHelper.Clamp(
                    (float)Math.Pow(EnemyProperties.Enemy_Info.SpawnDelay_Decrease, (GameInfo.Wave + SpawnInfo.Difficulty)),
                    EnemyProperties.Enemy_Info.SpawnDelay_Min,
                    float.MaxValue);
                
                AmountToSpawn_Increase = (int)Math.Pow(EnemyProperties.Enemy_Info.AmountToSpawn_Increase, (GameInfo.Wave + SpawnInfo.Difficulty));

                for (int i = 0; i < Enemy_SpawnDelay.Length; i++)
                {
                    Enemy_SpawnTimer[i] = SpawnInfo.Enemy_SpawnRate[i] * SpawnDelay_Decrease;
                }

                for (int i = 0; i < Enemy_SpawnDelay.Length; i++)
                {
                    Enemy_SpawnDelay[i] = SpawnInfo.Enemy_SpawnRate[i] * SpawnDelay_Decrease;
                }

                for (int i = 0; i < Enemy_AmountToSpawn.Length; i++)
                {
                    Enemy_AmountToSpawn[i] = GameInfo.Wave >= SpawnInfo.Enemy_Start[i] ? SpawnInfo.Enemy_Amount[i] + AmountToSpawn_Increase : 0;
                }

                for (int i = 0; i < GameInfo.Path.Count; i++)
                {
                    GameInfo.Path[i].TileForm = 5;
                    if (i == 0 || i == GameInfo.Path.Count - 1)
                    {
                        GameInfo.Path[i].TileForm = 4;
                    }
                    GameInfo.Path[i].LoadContent();
                }

                SpawnEnemies = true;
            }
        }

        public static void Update(GameTime aGameTime)
        {
            if (SpawnEnemies)
            {
                for (int i = 0; i < Enemy_SpawnTimer.Length; i++)
                {
                    if (Enemy_AmountToSpawn[i] > 0)
                    {
                        Enemy_SpawnTimer[i] -= (float)aGameTime.ElapsedGameTime.TotalSeconds * GameInfo.GameSpeed;
                        if (Enemy_SpawnTimer[i] <= 0)
                        {
                            EnemyManager.AddEnemy(i);
                                
                            Enemy_AmountToSpawn[i]--;
                            Enemy_SpawnTimer[i] = Enemy_SpawnDelay[i] * SpawnDelay_Decrease;
                        }
                    }
                }

                if (EnemyManager.Enemies.Count <= 0 && Array.TrueForAll(Enemy_AmountToSpawn, a => a <= 0)) //All enemies dead and no longer spawns
                {
                    GameInfo.Wave++;
                    GameInfo.Money += GameInfo.MoneyEachWave * GameInfo.Wave;
                    SpawnEnemies = false;

                    for (int i = 0; i < GameInfo.Path.Count; i++)
                    {
                        GameInfo.Path[i].TileForm = 4;
                        if (i == 0 || i == GameInfo.Path.Count - 1)
                        {
                            GameInfo.Path[i].TileForm = 5;
                        }
                        GameInfo.Path[i].LoadContent();
                    }
                }
            }
        }
    }
}
