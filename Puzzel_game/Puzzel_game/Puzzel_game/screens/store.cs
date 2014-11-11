using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Puzzel_game
{
    class store:objects
    {
        const sbyte MAX_OBJECTS = 4;

        // The spghattios are really-io
        public byte delay { get; set; }
       
        KeyboardState keyboard;
        KeyboardState prevKeyboard;

        GamePadState gamepad;
        GamePadState prevGamepad;

        string[] name = new string[MAX_OBJECTS];
        short[] cost = new short[MAX_OBJECTS];
        bool[] canBuy = new bool[MAX_OBJECTS];

        Color[] color = new Color[MAX_OBJECTS];

        string playerMoney = "";
        sbyte playerbombCount = 0;
        sbyte playerDrillCount = 0;
        sbyte playerBucketCount = 0;
        sbyte playerNukeCount = 0;

        sbyte selection;

        string[] description = new string[MAX_OBJECTS];

        public store()
        {
            name[0] = "BOMB";
            name[1] = "DRILL";
            name[2] = "BUCKET";
            name[3] = "NUKE";

            cost[0] = 100;
            cost[1] = 200;
            cost[2] = 400;
            cost[3] = 100*100;
            
            description[0] = "The bomb blowes a hole in the blocks";
            description[1] = "The drill spawns at a random location and \ndrills straight down destroying all blocks \nin it's way";
            description[2] = "The paintbucket destroies one row of blocks \nwith the same color as the bucket";
            description[3] = "The nuke destroies every single block on screen";

        }

        public void update(ref player player, fileManager fileManager)
        {
            playerMoney = player.money.ToString();
            playerbombCount = player.bombCount;
            playerDrillCount = player.drillCount;
            playerBucketCount = player.bucketCount;
            playerNukeCount = player.nukeCount;

            prevKeyboard = keyboard;
            keyboard = Keyboard.GetState();

            prevGamepad = gamepad;
            gamepad = GamePad.GetState(PlayerIndex.One);

            if (keyboard.IsKeyDown(Game1.left) && prevKeyboard.IsKeyUp(Game1.left) && selection != 0 && keyboard.IsKeyUp(Game1.right) && delay >= 10 || Game1.joyHit(gamepad, prevGamepad) == 'l' && selection != 0 && keyboard.IsKeyUp(Keys.Right) && delay >= 10)
            {
                selection -= 1;
            }
            if (keyboard.IsKeyDown(Game1.right) && prevKeyboard.IsKeyUp(Game1.right) && selection != 3 && keyboard.IsKeyUp(Game1.left) && delay >= 10 || Game1.joyHit(gamepad, prevGamepad) == 'r' && selection != 3 && keyboard.IsKeyUp(Keys.Left) && delay >= 10)
            {
                selection += 1;
            }

            if (keyboard.IsKeyDown(Keys.Space) && prevKeyboard.IsKeyUp(Keys.Space) && delay >= 10 || Game1.joyButtonHit(gamepad, prevGamepad) == 'a' && delay >= 10)
            {
                if(selection == 0 && player.money >= cost[selection] && player.bombCount <= 98)
                {
                    player.money -= cost[selection];
                    player.bombCount += 1;
                }
                if (selection == 1 && player.money >= cost[selection] && player.drillCount <= 98)
                {
                    player.money -= cost[selection];
                    player.drillCount += 1;
                }
                if (selection == 2 && player.money >= cost[selection] && player.bucketCount <= 98)
                {
                    player.money -= cost[selection];
                    player.bucketCount += 1;
                }
                if (selection == 3 && player.money >= cost[selection] && player.nukeCount <= 98)
                {
                    player.money -= cost[selection];
                    player.nukeCount += 1;
                }
            }

            if (delay <= 9)
                delay += 1;

            for (int i = 0; i < MAX_OBJECTS; i++)
            {
                if(cost[i] > player.money)
                {
                    color[i] = Color.Red;
                }
                else
                {
                    color[i] = Color.Green;
                }
            }
        }

        public void draw(SpriteBatch spriteBatch, Texture2D spritesheet, SpriteFont font)
        {
            spriteBatch.Draw(spritesheet, new Rectangle(0, 0, 640, 480), new Rectangle(694, 529, 32, 32), Color.White);
            drawText(spriteBatch, font, 0, 2.3f, "STORE", 320 - 5*23, 10, Color.Violet);
            for (int i = 0; i < MAX_OBJECTS; i++)
            {
                drawText(spriteBatch, font, 0, 0.3f, name[i], 200 + i * 60, 140 - 16, Color.White);
                spriteBatch.Draw(spritesheet, new Vector2(200 + i * 60, 140), new Rectangle(628, 1, 32, 32), color[i]);
                spriteBatch.Draw(spritesheet, new Vector2(200 + i * 60, 140), new Rectangle(661, frame(i), 32, 32), Color.White);
                drawText(spriteBatch, font, 0, 0.3f, "COST: " + cost[i].ToString(), 200 + i * 60, 140+32, Color.White);
            }
            spriteBatch.Draw(spritesheet, new Vector2(200 + selection * 60, 140+64-16), new Rectangle(628, 34, 32, 32), Color.SpringGreen);
            drawText(spriteBatch, font, 0, 0.5f, "MONEY: " + playerMoney, 10, 300, Color.LightGreen);
            drawText(spriteBatch, font, 0, 0.5f, "$", 10 + 6 * 12 + (playerMoney.Count() * 12), 300, Color.White);

            drawText(spriteBatch, font, 0, 0.5f, description[selection], 120, 220, Color.Pink);

            if(!GamePad.GetState(PlayerIndex.One).IsConnected)
            {
                drawText(spriteBatch, font, 0, 0.5f, "Press space to buy and Escape to leave", 120 + 100, 320 - 20, Color.White);
            }
            else
            {
                drawText(spriteBatch, font, 0, 0.5f, "Press # to buy and & to leave", 120+100, 320-20, Color.White);
            }


            if (playerbombCount <= 10)
            {
                for (int i = 0; i < playerbombCount; i++)
                {
                    spriteBatch.Draw(spritesheet, new Vector2(3 + i * 32, 332), new Rectangle(661, 1, 32, 32), Color.White);
                }
            }
            else
            {
                spriteBatch.Draw(spritesheet, new Vector2(35, 332), new Rectangle(661, 1, 32, 32), Color.White);
                drawText(spriteBatch, font, 0, 0.7f, ": " + playerbombCount.ToString(), 35+25, 332+2, Color.White);
            }
            if (playerDrillCount <= 10)
            {
                for (int i = 0; i < playerDrillCount; i++)
                {
                    spriteBatch.Draw(spritesheet, new Vector2(3 + i * 32, 332 + 32), new Rectangle(661, frame(1), 32, 32), Color.White);
                }
            }
            else
            {
                spriteBatch.Draw(spritesheet, new Vector2(35, 332+32), new Rectangle(661, frame(1), 32, 32), Color.White);
                drawText(spriteBatch, font, 0, 0.7f, ": " + playerDrillCount.ToString(), 35 + 25, 332 + 2+32, Color.White);
            }
            if (playerBucketCount <= 10)
            {
                for (int i = 0; i < playerBucketCount; i++)
                {
                    spriteBatch.Draw(spritesheet, new Vector2(3 + i * 32, 332 + 64), new Rectangle(661, frame(2), 32, 32), Color.White);
                }
            }
            else
            {
                spriteBatch.Draw(spritesheet, new Vector2(35, 332 + 64), new Rectangle(661, frame(2), 32, 32), Color.White);
                drawText(spriteBatch, font, 0, 0.7f, ": " + playerBucketCount.ToString(), 35 + 25, 332 + 2 + 64, Color.White);
            }
            if (playerNukeCount <= 10)
            {
                for (int i = 0; i < playerNukeCount; i++)
                {
                    spriteBatch.Draw(spritesheet, new Vector2(3 + i * 32, 332 + 32 * 3), new Rectangle(661, frame(3), 32, 32), Color.White);
                }
            }
            else
            {
                spriteBatch.Draw(spritesheet, new Vector2(35, 332 + 32*3), new Rectangle(661, frame(3), 32, 32), Color.White);
                drawText(spriteBatch, font, 0, 0.7f, ": " + playerNukeCount.ToString(), 35 + 25, 332 + 2 + 32*3, Color.White);
            }
        }
    }
}
