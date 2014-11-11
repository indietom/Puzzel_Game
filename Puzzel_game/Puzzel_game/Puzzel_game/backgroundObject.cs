using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Puzzel_game
{
    class backgroundObject:objects
    {
        sbyte type;

        float cos;

        public backgroundObject(float x2, float y2, sbyte type2)
        {
            setCoords(x2, y2);
            type = type2;
            assignSprite();
        }

        public void movment()
        {
            switch(type)
            {
                case 1:
                    cos += 0.5f;
                    x += (float)Math.Cos(cos);
                    y += (float)Math.Cos(5);
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
            switch(type)
            {
                case 1:
                    setSpriteCoords(364, frame(random.Next(6, 7)));
                    setSize(7,7);
                    break;
            }
        }
    }
}
