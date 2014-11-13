using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Puzzel_game
{
    class ui:objects
    {
        KeyboardState keyboard;
        KeyboardState prevKeyboard;

        GamePadState gamepad;
        GamePadState prevGamepad;

        string getScore;
        string getLevel;

        sbyte[] getInv = new sbyte[4];

        string[] invNames = new string[4];

        bool savedHighscore;

        public ui()
        {
            getScore = "";
            getLevel = "";

            for(int i = 0; i < 4; i++)
            {
                getInv[i] = 0;
            }
            invNames[0] = "Bombs";
            invNames[1] = "Drills";
            invNames[2] = "Bucket";
            invNames[3] = "Nukes";
        }

        public void update(player player, level level, List<block> blocks, fileManager fileManager)
        {
            prevKeyboard = keyboard;
            keyboard = Keyboard.GetState();

            prevGamepad = gamepad;
            gamepad = GamePad.GetState(PlayerIndex.One);

            getScore = "SCORE: " + player.score.ToString();
            getLevel = "LEVEL: " + level.currentLevel.ToString();

            getInv[0] = player.bombCount;
            getInv[1] = player.drillCount;
            getInv[2] = player.bucketCount;
            getInv[3] = player.nukeCount;

            if(spawner.lost)
            {
                if(!savedHighscore)
                {
                    fileManager.saveHighScore(player);
                    savedHighscore = true;
                }
                if(keyboard.IsKeyDown(Keys.Space) && prevKeyboard.IsKeyUp(Keys.Space) || Game1.joyButtonHit(gamepad, prevGamepad) == 'a')
                {
                    fileManager.saveGame(player);
                    blocks.Clear();
                    spawner.lost = false;
                    player.reset();
                    savedHighscore = false;
                }
                if (keyboard.IsKeyDown(Keys.Escape) && prevKeyboard.IsKeyUp(Keys.Escape) || Game1.joyButtonHit(gamepad, prevGamepad) == 'b')
                {
                    fileManager.saveGame(player);
                    blocks.Clear();
                    spawner.lost = false;
                    player.reset();
                    savedHighscore = false;
                    Game1.playing = false;
                    Game1.gameState = "menu";
                }
            }

        }
        public void draw(Texture2D spritesheet, SpriteBatch spriteBatch, SpriteFont font)
        {
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    spriteBatch.Draw(spritesheet, new Vector2(j * 32, i * 32), new Rectangle(frame(15), 1, 32, 32), Color.White);
                    spriteBatch.Draw(spritesheet, new Vector2(j * 32 + 32*10+32*5, i * 32), new Rectangle(frame(15), 1, 32, 32), Color.White);
                }
            }
            for(int i = 0; i < 10; i++)
            {
                spriteBatch.Draw(spritesheet, new Vector2(i * 32 + 32 * 5, 13 * 32), new Rectangle(frame(15), 1, 32, 32), Color.White);
                spriteBatch.Draw(spritesheet, new Vector2(i * 32 + 32 * 5, 14 * 32), new Rectangle(frame(15), 1, 32, 32), Color.White);
            }
            drawText(spriteBatch, font, 0, 0.7f, getScore, 32 * 7, 32 * 13, Color.LightGreen);
            drawText(spriteBatch, font, 0, 0.7f, getLevel, 5, 10, Color.DarkOrange);

            for (int i = 0; i < 4; i++)
            {
                spriteBatch.Draw(spritesheet, new Vector2(640 - 48*3, 32 + i * (48+32)), new Rectangle(628, 1, 32, 32), Color.White);
                spriteBatch.Draw(spritesheet, new Vector2(640 - 48 * 3, 32 + i * (48 + 32)), new Rectangle(661, frame(i), 32, 32), Color.White);
                drawText(spriteBatch, font, 0, 0.5f, "Left: " + getInv[i].ToString(), 640 - 48*3, 64 + i * (48+32), Color.White);
                drawText(spriteBatch, font, 0, 0.5f, invNames[i], 640 - 48 * 3, 32-10-5 + i * (48 + 32), Color.White);
            }

            if(spawner.lost)
            {
                drawText(spriteBatch, font, 0, 1, "GAME OVER", 220 - 11, 120 - 1, Color.Black);
                drawText(spriteBatch, font, 0, 1, "GAME OVER", 220-10, 120, Color.Red);

                drawText(spriteBatch, font, 0, 0.6f, "Final Score: " + getScore, 220 - 20 - 1, 120 + 48 - 1, Color.Black);
                drawText(spriteBatch, font, 0, 0.6f, "Final Score: " + getScore, 220 - 20, 120+48, Color.LightGreen);
               
                if(fileManager.newHighscore)
                {
                    drawText(spriteBatch, font, -25, 0.3f, "New highscore!", 220 - 20, 120 + 48-13, Color.Gold);
                }
                if(GamePad.GetState(PlayerIndex.One).IsConnected)
                {
                    drawText(spriteBatch, font, 0, 0.4f, "Press A to restart", 220 - 20, 120 + 48 + 48 - 8, Color.Black);
                    drawText(spriteBatch, font, 0, 0.4f, "Press A to restart", 220 - 20, 120 + 48 + 48 - 8, Color.LightPink);

                    drawText(spriteBatch, font, 0, 0.4f, "Press B to go back to the menu", 220 - 20, 120 + 48 + 48 - 8 + 48, Color.Black);
                    drawText(spriteBatch, font, 0, 0.4f, "Press B to go back to the menu", 220 - 20, 120 + 48 + 48 - 8 + 48, Color.LightPink);
                }
                else
                {
                    drawText(spriteBatch, font, 0, 0.4f, "Press Space to restart", 220 - 20 - 1, 120 + 48 + 48 - 1 - 8, Color.Black);
                    drawText(spriteBatch, font, 0, 0.4f, "Press Space to restart", 220 - 20, 120 + 48 + 48 - 8, Color.LightPink);

                    drawText(spriteBatch, font, 0, 0.4f, "Press Escape to go back to the menu", 220 - 20 - 1, 120 + 48 + 48 - 1 - 8 + 48, Color.Black);
                    drawText(spriteBatch, font, 0, 0.4f, "Press Escape to go back to the menu", 220 - 20, 120 + 48 + 48 - 8 + 48, Color.LightPink);
                }
            }
        }
    }
}
