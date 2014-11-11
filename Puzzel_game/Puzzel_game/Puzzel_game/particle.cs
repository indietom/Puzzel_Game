using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Puzzel_game
{
    class particle:objects
    {
        sbyte type;
        sbyte spriteType;
        sbyte colorType;

        byte r;
        byte g;
        byte b;

        byte changeColorCount;

        public float rotation { get; set; }

        public Color color { get; set; }

        public particle(float x2, float y2, sbyte type2, sbyte spriteType2, sbyte colorType2, Color color2, float ang, float spe)
        {
            speed = spe;
            angle = ang;
            color = color2;
            colorType = colorType2;
            setCoords(x2, y2);
            type = type2;
            spriteType = spriteType2;
            assignTypes();
        }

        public void update()
        {
            Random random = new Random();
            checkOnScreen();
            switch(type)
            {
                case 1:
                    angleMath();
                    x += veclocity_x;
                    y += veclocity_y;

                    if( angle > -90 && angle < 90)
			            angle += 5;
				    if( angle < -90 && angle > -270)
			           angle -= 5;
                    break;
                case 2:
                    angleMath();
                    angle = 90;
                    speed += 0.1f;
                    x += veclocity_x;
                    y += veclocity_y;
                    break;
            }
            switch(colorType)
            {
                case 1:
                    changeColorCount += 1;
                    if (changeColorCount >= 2)
                    {
                        r = (byte)random.Next(100, 255);
                        g = (byte)random.Next(100, 255);
                        b = (byte)random.Next(100, 255);
                        changeColorCount = 0;
                    }
                    color = new Color(r, g, b);
                    break;
            }
            switch(spriteType)
            {
                case 2:
                    animationCount += 1;
                    if(animationCount >= 4)
                    {
                        currentFrame += 1;
                        animationCount = 0;
                    }
                    if(currentFrame >= 5)
                    {
                        currentFrame = 0;
                    }
                    imy = frame(currentFrame, 8);
                    break;
                case 3:
                    rotation += 10;
                    break;
            }
        }

        public void assignTypes()
        {
            Random random = new Random();
            if(colorType == 0)
            {
                color = Color.White;
            }
            switch(type)
            {

            }
            switch(spriteType)
            {
                case 1:
                    setSpriteCoords(256, 1);
                    setSize(8, 8);
                    break;
                case 2:
                    setSpriteCoords(265, 1);
                    setSize(8, 8);
                    break;
                case 3:
                    setSpriteCoords(274, frame(random.Next(2)));
                    setSize(8, 8);
                    rotated = true;
                    break;
            }
        }
    }
}
