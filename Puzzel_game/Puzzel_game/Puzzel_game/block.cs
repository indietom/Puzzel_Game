using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Puzzel_game
{
    class block:objects
    {
        sbyte type;

        short worth;

        sbyte moveCountY;
        sbyte maxMoveCountY;

        sbyte moveCountX;
        sbyte maxMoveCountX;

        sbyte colorType;

        bool active;

        bool cantMoveY;
        bool cantMoveR;
        bool cantMoveL;

        public Color color {get; set;}

        public Keys left;
        public Keys right;
        public Keys down;

        KeyboardState keyboard;
        KeyboardState prevKeyboard;

        public block(float x2, float y2, sbyte type2, sbyte colorType2)
        {
            setCoords(x2, y2);
            type = type2;

            active = true;

            colorType = colorType2;

            maxMoveCountY = 16*4;

            left = Keys.Left;
            right = Keys.Right;
            down = Keys.Down;

            assignSprite();
        }

        public void update(List<block> blocks)
        {
            Rectangle hitbox = new Rectangle((int)x, (int)y, 32, 32);
            moveCountY += 1;
            if(moveCountY >= maxMoveCountY)
            {
                y += Game1.grid(1);
                moveCountY = 0;
            }
            if(y >= 480-64-32)
            {
                cantMoveY = true;
            }
            foreach(block b in blocks)
            {
                if(y + Game1.grid(1) == b.y && x == b.x)
                {
                    cantMoveY = true;
                }
                else
                {
                    //cantMoveY = false;
                }
                if(x + 32 == b.x && y == b.y && active)
                {
                    cantMoveL = true;
                }
                else
                {
                    if (b.active)
                    {
                        cantMoveL = false;
                    }
                }
                if (x - 32 == b.x && y == b.y && active)
                {
                    Console.WriteLine("can't move right");
                    cantMoveR = true;
                }
                else
                {
                    if (b.active)
                    {
                        cantMoveR = false;
                    }
                }
            }
            if(cantMoveY)
            {
                active = false;
                moveCountY = 0;
            }
        }
        public void input()
        {
            prevKeyboard = keyboard;
            keyboard = Keyboard.GetState();

            if(active)
            {
                if(keyboard.IsKeyDown(down) && prevKeyboard.IsKeyUp(down) && !cantMoveY)
                {
                    y += Game1.grid(1);
                    moveCountY = 0;
                }
                if (keyboard.IsKeyDown(left) && prevKeyboard.IsKeyUp(left) && !cantMoveY && !cantMoveR)
                {
                    x -= Game1.grid(1);
                }
                if (keyboard.IsKeyDown(right) && prevKeyboard.IsKeyUp(right) && !cantMoveY && !cantMoveL)
                {
                    x += Game1.grid(1);
                }
            }
        }
        public void assignSprite()
        {
            setSpriteCoords(frame(type-1), 1);
            setSize(32);
            switch(colorType)
            {
                case 1:
                    color = new Color(0, 0, 255);
                    break;
                case 2:
                    color = new Color(0, 255, 0);
                    break;
                case 3:
                    color = new Color(255, 0, 0);
                    break;
                case 4:
                    color = new Color(255, 255, 0);
                    break;
                case 5:
                    color = new Color(160, 32, 240);
                    break;
                case 6:
                    color = new Color(255, 105, 180);
                    break;
                default:
                    color = new Color(20, 20, 20);
                    break;
            }
        }
    }
}
