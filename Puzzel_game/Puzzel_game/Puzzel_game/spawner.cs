using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Puzzel_game
{
    class spawner:objects
    {
        sbyte nextType;

        sbyte[] nextColor = new sbyte[3];

        sbyte spawnPosistion;
        sbyte chanceOfBomb;
        
        short spawnCount;
        short maxSpawnCount;

        sbyte chanceOfMoney;
        sbyte specificBlock;

        bool dropBomb;
        bool dropBucket;
        bool dropDrill;

        static public bool lost;

        short spawnGroundCount;
        short spawnSkyCount;
        short spawnObjectsCount;

        KeyboardState keyboard;
        KeyboardState prevKeyboard;

        public void update(List<block> blocks, List<backgroundObject> backgroundObjects, ref player player)
        {
            prevKeyboard = keyboard;
            keyboard = Keyboard.GetState();

            Random random = new Random();

            if(Game1.environment == 0)
            {
                spawnGroundCount += 1;
                spawnObjectsCount += 1;
                spawnSkyCount += 1;

                if(spawnGroundCount >= 32)
                {
                    backgroundObjects.Add(new backgroundObject(Game1.grid(15), 480-64*2, 1, 364, 32, 32, -1, 1));
                    backgroundObjects.Add(new backgroundObject(Game1.grid(15), 480 - 32*3, 1, 364, 32, 32, -1, 1));
                    spawnGroundCount = 0;
                }
                if(spawnSkyCount >= 124)
                {
                    backgroundObjects.Add(new backgroundObject(Game1.grid(15+random.Next(5)), 480 - 32 * 10 - random.Next(200), frame(random.Next(0,2), 65), 265, 65, 32, -2, 1));
                    spawnSkyCount = (short)random.Next(32);
                }

                if (spawnObjectsCount >= 124)
                {
                    backgroundObjects.Add(new backgroundObject(Game1.grid(15) + random.Next(100), 480 - 64 * 2 - 32*2+5, frame(random.Next(0, 3), 65), 298, 65, 65, -1, 1));
                    spawnObjectsCount = (short)random.Next(32);
                }
            }

            if(lost)
            {
                
            }

            if(keyboard.IsKeyDown(Keys.D1) && prevKeyboard.IsKeyUp(Keys.D1) && player.bombCount >= 1)
            {
                dropBomb = true;
                dropDrill = false;
                dropBucket = false;
            }
            if (keyboard.IsKeyDown(Keys.D3) && prevKeyboard.IsKeyUp(Keys.D3) && player.bucketCount >= 1)
            {
                dropBucket = true;
                dropDrill = false;
                dropBomb = false;
            }
            if (keyboard.IsKeyDown(Keys.D2) && prevKeyboard.IsKeyUp(Keys.D2) && player.drillCount >= 1)
            {
                dropDrill = true;
                dropBomb = false;
                dropBucket = false;
            }

            if(blocks.Count <= 0)
            {
                spawnCount = 124;
            }

            if(!Game1.activeBlocks)
            {
                spawnCount = 124;
            }

            maxSpawnCount = 124;

            if(dropBomb &&spawnCount >= 124) 
            {
                blocks.Add(new block(Game1.grid(320 / 32) - 32 + 32 * 1, 0, 2, 0));
                dropBomb = false;
                player.bombCount -= 1;
                spawnCount = 0;
            }

            if(dropBucket && spawnCount >= 124)
            {
                blocks.Add(new block(Game1.grid(320 / 32) - 32 + 32 * 1, 0, 5, (sbyte)random.Next(1,7)));
                dropBucket = false;
                player.bucketCount -= 1;
                spawnCount = 0;
            }

            if (dropDrill && spawnCount >= 124)
            {
                blocks.Add(new block(Game1.grid(320 / 32)+Game1.grid(random.Next(-5, 5)), 32, 3, 0));
                dropDrill = false;
                player.drillCount -= 1;
                spawnCount = 0;
            }
            if(spawnCount >= maxSpawnCount && !dropBomb && !dropDrill && !lost)
            {
                chanceOfMoney = (sbyte)random.Next(1, 21);
                specificBlock = (chanceOfMoney == 7) ? specificBlock = 4 : specificBlock = 1;
                for (int i = 0; i < 3; i++)
                {
                    nextColor[i] = (sbyte)random.Next(1, 8);
                    blocks.Add(new block(Game1.grid(320 / 32) - 32 + 32 * i, 0, specificBlock, nextColor[i]));
                    specificBlock = 1;
                }
                spawnCount = 0;
            }
        }
    }
}
