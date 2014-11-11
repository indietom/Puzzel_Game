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
    class menu:objects
    {
        const sbyte SELECTIONS_COUNT = 5;

        KeyboardState keyboard;
        KeyboardState prevKeyboard;

        GamePadState gamepad;
        GamePadState prevGamepad;

        float rotation;

        sbyte selection;

        string[] selections = new string[SELECTIONS_COUNT];

        Color[] color = new Color[SELECTIONS_COUNT];

        public menu()
        {
            selections[1] = "STORE";
            selections[2] = "HIGHSCORE";
            selections[3] = "OPTIONS";
            selections[4] = "QUIT";
        }

        public void reset()
        {
            selection = 0;
        }

        public void update()
        {
            prevGamepad = gamepad;
            gamepad = GamePad.GetState(PlayerIndex.One);

            prevKeyboard = keyboard;
            keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Game1.down) && prevKeyboard.IsKeyUp(Game1.down) || Game1.joyHit(gamepad, prevGamepad) == 'd')
            {
                selection += 1;
            }
            if (keyboard.IsKeyDown(Game1.up) && prevKeyboard.IsKeyUp(Game1.up) || Game1.joyHit(gamepad, prevGamepad) == 'u')
            {
                selection -= 1;
            }

            if(keyboard.IsKeyDown(Keys.Space) && prevKeyboard.IsKeyUp(Keys.Space) || Game1.joyButtonHit(gamepad, prevGamepad) == 'a')
            {
                switch(selection)
                {
                    case 0:
                        Game1.gameState = "game";
                        break;
                    case 1:
                        if (!Game1.playing)
                            Game1.gameState = "store";
                        break;
                    case 2:
                        Game1.gameState = "highscore";
                        break;
                    case 3:
                        Game1.gameState = "options";
                        break;
                    case 4:
                        Environment.Exit(0);
                        break;
                }
            }

            if(selection >= 5)
            {
                selection = 0;
            }
            if(selection <= -1)
            {
                selection = 4;
            }

            if(Game1.playing)
            {
                selections[0] = "RESUME";
            }
            else
            {
                selections[0] = "START GAME";
            }

            for(int i = 0; i < SELECTIONS_COUNT; i++)
            {
                if(i == selection)
                {
                    color[i] = Color.LightGreen;
                }
                else
                {
                    color[i] = Color.White;
                }
            }
            if(Game1.playing)
            {
                color[1] = Color.Gray;
            }
        }
        public void draw(SpriteBatch spriteBatch, Texture2D spritesheet, SpriteFont font)
        {
            spriteBatch.Draw(spritesheet, new Rectangle(0, 0, 640, 480), new Rectangle(694, 529, 32, 32), Color.White);
            for(int i = 0; i < SELECTIONS_COUNT; i++)
            {
                drawText(spriteBatch, font, 0, 1, selections[i], 120, 100 + 50 * i, color[i]);
            }
        }
    }
}
