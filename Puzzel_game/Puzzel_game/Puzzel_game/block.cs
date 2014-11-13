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

        bool blockClose;

        const sbyte DESTROY_RADIUS = 64;

        sbyte particleType;
        sbyte particleColor;

        public float blastRadius { get; set; }
        public float splashRadius { get; set; }

        short worth;

        public sbyte blockTouching { get; set; }

        sbyte moveCountY;
        sbyte maxMoveCountY;

        sbyte moveCountX;
        sbyte maxMoveCountX;

        sbyte colorType;

        public bool active { get; set; }

        bool cantMoveY;
        bool cantMoveR;
        bool cantMoveL;

        bool[] filledBoxes = new bool[8];
        bool[] boxGotten = new bool[8];

        bool middleBlock;
        bool rightBlock;
        bool leftBlock;

        public Color color {get; set;}

        KeyboardState keyboard;
        KeyboardState prevKeyboard;

        GamePadState gamepad;
        GamePadState prevGamepad;


        public block(float x2, float y2, sbyte type2, sbyte colorType2)
        {
            setCoords(x2, y2);
            type = type2;

            active = true;

            colorType = colorType2;

            maxMoveCountY = 16*4;

            blastRadius = 32 * 3;
            splashRadius = 32 * 4;

            assignSprite();

            if(x == Game1.grid(320 / 32)-32 + 32*1)
            {
                middleBlock = true;
            }
            if (x == Game1.grid(320 / 32) - 32 + 32 * 0)
            {
                leftBlock = true;
            }
            if (x == Game1.grid(320 / 32) - 32 + 32 * 2)
            {
                rightBlock = true;
            }

        }

        public void update(List<block> blocks, List<effect> effects, List<particle> particles, ref player player, level level)
        {
            Rectangle hitbox = new Rectangle((int)x, (int)y, 32, 32);
            Random random = new Random();

            if(spawner.lost)
            {
                active = false;
            }

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
            
            if(type == 3)
            {
                effects.Add(new effect(x+16-8, y-16, 0, 6, 315, 16, 4));
                active = false;
                cantMoveY = false;
                maxMoveCountY = 8;
            }

            if(!active && y <= 0)
            {
                spawner.lost = true;
            }

            if(!active)
            {
                maxMoveCountY = 8;
                leftBlock = false;
                rightBlock = false;
                middleBlock = false;
            }
            else
            {
                maxMoveCountY = (level.currentLevel < 10) ? (sbyte)(16 * 4 - level.currentLevel * 6) : (sbyte)(16 * 4 - 60);
            }
     
            if(type == 3)
            {
                if(y >= 480-64-32)
                {
                    y += 1;
                }
                if(y >= 480)
                {
                    destroy = true;
                }
            }

            if (active || type == 3)
                Game1.activeBlocks = true;
            else
                Game1.activeBlocks = false;

            foreach(block b in blocks)
            {
                if (b.type == 5 && b.y == y && !b.active && !active && b.colorType == colorType)
                {
                    for (int i = 0; i < 35; i++)
                    {
                        effects.Add(new effect(x + 16 + random.Next(-64, 64), y + 16 + random.Next(-64, 64), 0, 6, 298, 16, 8));
                    }
                    destroy = true;
                    blockClose = true;
                }
                if (b.type == 5 && b.y == y+32 && !b.active && !active && b.colorType == colorType)
                {
                    for (int i = 0; i < 35; i++)
                    {
                        effects.Add(new effect(x + 16 + random.Next(-64, 64), y + 16 + random.Next(-64, 64), 0, 6, 298, 16, 8));
                    }
                    destroy = true;
                    blockClose = true;
                }
                if (b.type == 3 && type != 3)
                {
                    if (b.y + 32 == y && b.x == x)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            effects.Add(new effect(x + random.Next(32), y + random.Next(32), 0, 6, 298, 16, 4));
                        }
                        player.score += worth;
                        destroy = true;
                    }
                }
                if(!active && !b.active)
                {
                    if (b.type == 2 && b.distanceTo(x, y) <= b.blastRadius)
                    {
                        for (int i = 0; i < 360; i++)
                        {
                            effects.Add(new effect(b.x + random.Next((int)-blastRadius, (int)blastRadius), b.y + random.Next((int)-blastRadius, (int)blastRadius), 0, 6, 298, 16, 4));
                            effects.Add(new effect(b.x + (float)Math.Cos(i) * blastRadius, b.y + (float)Math.Sin(i) * blastRadius, 0, 6, 298, 16, 4));
                        }
                        player.score += worth;
                        destroy = true;
                    }
                    if (y + Game1.grid(1) == b.y && x == b.x && colorType == b.colorType)
                    {
                        //b.blockTouching = blockTouching;
                        //blockTouching = b.blockTouching;
                        filledBoxes[0] = true;
                        b.filledBoxes[0] = true;
                        if (!b.boxGotten[0])
                        {
                            b.blockTouching += 1;
                            b.boxGotten[0] = true;
                        }
                        if(!boxGotten[0])
                        {
                            blockTouching += 1;
                            boxGotten[0] = true;
                        }
                    }
                    if (y - Game1.grid(1) == b.y && x == b.x && colorType == b.colorType)
                    {
                        //b.blockTouching = blockTouching;
                        //blockTouching = b.blockTouching;
                        filledBoxes[1] = true;
                        b.filledBoxes[1] = true;
                        if (!b.boxGotten[1])
                        {
                            b.blockTouching += 1;
                            b.boxGotten[1] = true;
                        }
                        if (!boxGotten[1])
                        {
                            blockTouching += 1;
                            boxGotten[1] = true;
                        }
                    }
                    if (x + Game1.grid(1) == b.x && y == b.y && colorType == b.colorType)
                    {
                        //b.blockTouching = blockTouching;
                        //blockTouching = b.blockTouching;
                        filledBoxes[2] = true;
                        b.filledBoxes[2] = true;
                        if (!b.boxGotten[2])
                        {
                            b.blockTouching += 1;
                            b.boxGotten[2] = true;
                        }
                        if (!boxGotten[2])
                        {
                            blockTouching += 1;
                            boxGotten[2] = true;
                        }
                    }
                    if (x - Game1.grid(1) == b.x && y == b.y && colorType == b.colorType)
                    {
                        //b.blockTouching = blockTouching;
                        //blockTouching = b.blockTouching;
                        filledBoxes[3] = true;
                        b.filledBoxes[3] = true;
                        if (!b.boxGotten[3])
                        {
                            b.blockTouching += 1;
                            b.boxGotten[3] = true;
                        }
                        if (!boxGotten[3])
                        {
                            blockTouching += 1;
                            boxGotten[3] = true;
                        }
                    }
                }

                if (colorType == b.colorType && b.distanceTo(x, y) <= 64 && !active && !b.active)
                {
                    if(blockTouching < b.blockTouching)
                        blockTouching = b.blockTouching;
                }
               
                if (colorType == b.colorType && b.distanceTo(x, y) <= 64 && !active && !b.active)
                {
                    if (blockTouching >= 3)
                    {
                        blockTouching = 4;
                        b.blockTouching = 4;
                    }
                }
                //Console.WriteLine(distanceTo(b.x + 16, b.y + 16, true));

                //if (blockTouching >= 3 && colorType == b.colorType && distanceTo(b.x+16, b.y+16) <= 64)
                //{
                //    if (!active && !b.active)
                //    {
                //        destroy = true;
                //        b.destroy = true;
                //        b.blockClose = true;
                //        b.blockTouching = 3;
                //        Console.WriteLine("LE LEL2");
                //    }
                //}
                if (b.blockTouching >= 3 && colorType == b.colorType && b.distanceTo(x + 16, y + 16, true) <= 64)
                {
                   blockTouching = 3;
                }

                if(y + Game1.grid(1) == b.y && x == b.x || y + Game1.grid(1) >= 480-64)
                {
                    cantMoveY = true;
                    moveCountY = 0;
                }
                else
                {
                    if (y <= 480 - 64 - 32 - 32 - 32)
                    {
                        //cantMoveY = false;
                    }
                }

                if (b.leftBlock && middleBlock && b.cantMoveR && active && b.active && b.y == y)
                {
                    cantMoveR = true;
                }

                if (b.rightBlock && middleBlock && b.cantMoveL && active && b.active && b.y == y)
                {
                    cantMoveL = true;
                }

                if(middleBlock && b.active && active)
                {
                    if (b.rightBlock && b.cantMoveL && x + 32 == b.x && y == b.y)
                    {
                        cantMoveL = true;
                    }
                    if (b.leftBlock && b.cantMoveR && x - 32 == b.x && y == b.y)
                    {
                        cantMoveR = true;
                    }
                }

                if (rightBlock && b.leftBlock && b.cantMoveR && b.active && active && b.y == y && x - 64 == b.x)
                {
                    cantMoveR = true;
                }
                if (leftBlock && b.rightBlock && b.cantMoveL && b.active && active && x + 64 == b.x)
                {
                    cantMoveL = true;
                }

                if (x + 32 == b.x && y == b.y && active && !b.active|| x + Game1.grid(1) == Game1.grid(15) && active)
                {
                    cantMoveL = true;
                }

                if (x - 32 == b.x && y == b.y && active && !b.active || x - Game1.grid(1) == Game1.grid(4) && active)
                {
                    cantMoveR = true;
                }
                if(middleBlock && b.leftBlock && b.y == y && x  - 32 == b.x)
                {
                    cantMoveR = b.cantMoveR;
                }
                if (middleBlock && b.rightBlock && b.y == y && x + 32 == b.x)
                {
                    cantMoveL = b.cantMoveL;
                }
                if (x + Game1.grid(2) == Game1.grid(15) && active)
                {
                    if(middleBlock)
                        cantMoveL = true;
                }
                if (x - Game1.grid(2) == Game1.grid(4) && active)
                {
                    if (middleBlock)
                        cantMoveR = true;
                }
                //if (x + 64 == b.x && y == b.y && active && !b.active || x + Game1.grid(2) == Game1.grid(15) && active)
                //{
                //    if(middleBlock)
                //        cantMoveL = true;
                //}
                //if (x - 64 == b.x && y == b.y && active && !b.active || x - Game1.grid(2) == Game1.grid(4) && active)
                //{
                //    if (middleBlock)
                //        cantMoveR = true;
                //}
                if(rightBlock)
                {
                    if (x - 64+32 == b.x && y == b.y && active && !b.active || x - Game1.grid(3) == Game1.grid(4) && active)
                    {
                        cantMoveR = true;
                    }
                }
                if(leftBlock)
                {
                    if (x + 64+32 == b.x && y == b.y && active && !b.active || x + Game1.grid(3) == Game1.grid(15) && active)
                    {
                        cantMoveL = true;
                    }
                }
            }
            if(cantMoveY)
            {
                active = false;
            }
            if(type == 3 && y >= 480-64-32) 
            {
                //destroy = true;
            }
            if (blockTouching >= 3 || blockClose)
            {
                if (!active)
                {
                    for (int i = 0; i < 35; i++)
                    {
                        effects.Add(new effect(x + 16 + random.Next(-64, 64), y + 16 + random.Next(-64, 64), 0, 6, 298, 16, 8));
                    }
                    for (int i = 0; i < 15; i++)
                    {
                        particleType = (type == 4) ? particleType = 2 : particleType = 1;
                        particleColor = (type == 4) ? particleColor = 0 : particleColor = 1;
                        particles.Add(new particle(x + 16 + random.Next(-16, 16), y + 16 + random.Next(-16, 16), 1, particleType, particleColor, Color.White, random.Next(-360, 0), random.Next(5, 8)));
                    }
                    if (type == 4)
                    {
                        player.money += worth;
                    }
                    destroy = true;
                    player.score += worth;
                }
            }
        }
        public void input()
        {
            prevKeyboard = keyboard;
            keyboard = Keyboard.GetState();

            prevGamepad = gamepad;
            gamepad = GamePad.GetState(PlayerIndex.One);

            if(active)
            {
                if (keyboard.IsKeyDown(Game1.down) && prevKeyboard.IsKeyUp(Game1.down) && keyboard.IsKeyUp(Game1.left) && keyboard.IsKeyUp(Game1.right) && !cantMoveY || Game1.joyHit(gamepad, prevGamepad) == 'd' && Game1.joyHit(gamepad, prevGamepad) != 'l' && Game1.joyHit(gamepad, prevGamepad) != 'r' && !cantMoveY)
                {
                    y += Game1.grid(1);
                    moveCountY = 0;
                }
                if (keyboard.IsKeyDown(Game1.left) && prevKeyboard.IsKeyUp(Game1.left) && !cantMoveY && !cantMoveR || Game1.joyHit(gamepad, prevGamepad) == 'l' && !cantMoveY && !cantMoveR)
                {
                    x -= Game1.grid(1);
                    if (cantMoveL)
                        cantMoveL = false;
                }
                if (keyboard.IsKeyDown(Game1.right) && prevKeyboard.IsKeyUp(Game1.right) && !cantMoveY && !cantMoveL || Game1.joyHit(gamepad, prevGamepad) == 'r' && !cantMoveY && !cantMoveL)
                {
                    x += Game1.grid(1);
                    if (cantMoveR)
                        cantMoveR = false;
                }
            }
        }
        public void assignSprite()
        {
            setSpriteCoords(frame(type-1), 1);
            setSize(32);
            worth = 1000;
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
                    color = Color.White;
                    break;
            }
        }
    }
}
