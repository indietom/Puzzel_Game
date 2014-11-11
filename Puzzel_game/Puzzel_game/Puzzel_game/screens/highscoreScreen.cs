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
    class highscoreScreen:objects
    {
        public sbyte delay { get; set; }

        KeyboardState keyboard;
        KeyboardState prevKeyboard;

        GamePadState gamepad;
        GamePadState prevGamepad;

        public void update()
        {
            prevKeyboard = keyboard;
            keyboard = Keyboard.GetState();

            prevGamepad = gamepad;
            gamepad = GamePad.GetState(PlayerIndex.One);

            delay += 1;
            if (keyboard.IsKeyDown(Keys.Escape) && prevKeyboard.IsKeyUp(Keys.Escape) && delay >= 10 || Game1.joyButtonHit(gamepad, prevGamepad) == 'b' && delay >= 10)
            {
                Game1.gameState = "menu";
            }
        }

        public void draw(SpriteBatch spriteBatch, Texture2D spritesheet, SpriteFont font, fileManager fm)
        {
            spriteBatch.Draw(spritesheet, new Rectangle(0, 0, 640, 480), new Rectangle(694, 529, 32, 32), Color.White);
            for(int i = 0; i < 10; i++)
            {
                drawText(spriteBatch, font, 0, 1, (i+1) + ". " + fm.highScores[i], 220-50, 32 + i * 32, Color.Yellow);
            }
        }
    }
}
