using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Puzzel_game
{
    class spawner
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
        bool dropDrill;

        static public bool lost;

        KeyboardState keyboard;
        KeyboardState prevKeyboard;

        public void update(List<block> blocks, ref player player)
        {
            prevKeyboard = keyboard;
            keyboard = Keyboard.GetState();

            Random random = new Random();

            if(lost)
            {
                
            }

            if(keyboard.IsKeyDown(Keys.D1) && prevKeyboard.IsKeyUp(Keys.D1) && player.bombCount >= 1)
            {
                dropBomb = true;
                dropDrill = false;
            }

            if (keyboard.IsKeyDown(Keys.D3) && prevKeyboard.IsKeyUp(Keys.D3) && player.drillCount >= 1)
            {
                dropDrill = true;
                dropBomb = false;
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
