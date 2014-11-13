using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Puzzel_game
{
    class backgroundObject:objects
    {
        sbyte type;

        float cos;

        public Color color { get; set; }

        public backgroundObject(float x2, float y2, sbyte type2)
        {
            setCoords(x2, y2);
            type = type2;
            assignSprite();
        }

        public backgroundObject(float x2, float y2, int imx2, int imy2, int w, int h, float spe, sbyte dir)
        {
            setCoords(x2, y2);
            setSize(w, h);
            setSpriteCoords(imx2, imy2);
            speed = spe;
            type = 0;
            direction = dir;
        }

        public void movment()
        {
            Random random = new Random();
            if(y <= 0)
            {
                destroy = true;
            }
            switch(type)
            {
                case 0:
                    if(direction == 1)
                    {
                        x += speed;
                    }
                    else
                    {
                        y += speed;
                    }
                    break;
                case 1:
                    cos += 0.5f;
                    x += (float)Math.Cos(cos);
                    y += (float)Math.Cos(5);
                    break;
                case 2:
                    y += 1;
                    color = new Color(random.Next(255), random.Next(255), random.Next(255));
                    break;
            }
            if(y >= 480)
            {
                destroy = true;
            }
        }
        
        public void update()
        {

        }

        public void assignSprite()
        {
            Random random = new Random();
            color = Color.White;
            switch(type)
            {
                case 1:
                    setSpriteCoords(364, frame(random.Next(6, 7)));
                    setSize(7,7);
                    color = Color.White;
                    break;
            }
        }
    }
}
