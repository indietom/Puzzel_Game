using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Puzzel_game
{
    class options:objects
    {
        sbyte delay;

        sbyte currentcontrolScheme;
        sbyte selection;

        GamePadState gamepad;
        GamePadState prevGamepad;

        KeyboardState keyboard;
        KeyboardState prevKeyboard;

        public void update()
        {
            prevKeyboard = keyboard;
            keyboard = Keyboard.GetState();

            prevGamepad = gamepad;
            gamepad = GamePad.GetState(PlayerIndex.One);

            if (delay <= 9)
                delay += 1;
            if(delay >= 10)
            {
                if (gamepad.IsConnected)
                {

                }
                else
                {
                    switch(selection)
                    {
                        case 0:
                            if(keyboard.IsKeyDown(Keys.Left) && prevKeyboard.IsKeyUp(Keys.Left) || keyboard.IsKeyDown(Keys.A) && prevKeyboard.IsKeyUp(Keys.A))
                            {
                                Game1.controlScheme -= 1;
                                if (Game1.controlScheme < 0)
                                    Game1.controlScheme = 1;
                            }
                            if (keyboard.IsKeyDown(Keys.Right) && prevKeyboard.IsKeyUp(Keys.Right) || keyboard.IsKeyDown(Keys.D) && prevKeyboard.IsKeyUp(Keys.D))
                            {
                                Game1.controlScheme += 1;
                                if (Game1.controlScheme > 1)
                                    Game1.controlScheme = 0;
                            }
                            break;
                    }
                }
            }
        }
        public void draw(SpriteBatch spriteBatch, Texture2D spritesheet, SpriteFont font)
        {
            spriteBatch.Draw(spritesheet, new Rectangle(0, 0, 640, 480), new Rectangle(694, 529, 32, 32), Color.White);
            drawText(spriteBatch, font, 0, 1, "CONTROLS-", 0, 32, Color.White);
            if(GamePad.GetState(PlayerIndex.One).IsConnected)
            {
                drawText(spriteBatch, font, 0, 0.6f, "Press # to swap the equiped power-up \nand & to drop it \nThumbsticks to move the blocks", 10, 32*2, Color.White);
                drawText(spriteBatch, font, 0, 0.6f, "Press & to go back", 0, 480 - 32, Color.White);
            }
            else
            {
                drawText(spriteBatch, font, 0, 0.6f, "<      > (use the arrow keys or wasd \n       to change the controls)", 10, 32 * 2, Color.White);
                spriteBatch.Draw(spritesheet, new Vector2(10 + 12, 32 * 2), new Rectangle(496+32+1, frame(Game1.controlScheme), 32, 32), Color.White);
                drawText(spriteBatch, font, 0, 0.6f, "1, 2, 3, 4 to spawn the diffrent power-ups", 10, 32 * 3+12, Color.White);
                drawText(spriteBatch, font, 0, 0.6f, "Press Escape to go back", 0, 480-32, Color.White);
            }
        }
    }
}
