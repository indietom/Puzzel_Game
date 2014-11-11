using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Puzzel_game
{
    class effect:objects
    {
        byte maxAnimationCount;

        sbyte minFrame;
        sbyte maxFrame;

        sbyte repeatCount;
        sbyte maxRepeatCount;

        public effect(float x2, float y2, sbyte minFrame2, sbyte maxFrame2, int imx2, int size, byte maxAnimationCount2)
        {
            maxAnimationCount = maxAnimationCount2;
            setCoords(x2, y2);
            setSize(size);
            setSpriteCoords(imx2, frame(minFrame, size));
            minFrame = minFrame2;
            maxFrame = maxFrame2;
        }
        public effect(float x2, float y2, sbyte minFrame2, sbyte maxFrame2, sbyte maxRepeatCount2, int imx2, int size, byte maxAnimationCount2)
        {
            maxAnimationCount = maxAnimationCount2;
            setCoords(x2, y2);
            setSize(size);
            setSpriteCoords(imx2, frame(currentFrame, size));
            minFrame = minFrame2;
            maxFrame = maxFrame2;
        }
        public void update()
        {
            imy = frame(currentFrame, width);
            animationCount += 1;
            if(animationCount >= maxAnimationCount)
            {
                currentFrame += 1;
                animationCount = 0;
            }
            if(repeatCount >= maxRepeatCount && maxRepeatCount > 0)
            {
                destroy = true;
            }
            if(currentFrame >= maxFrame)
            {
                if (maxRepeatCount > 0)
                {
                    repeatCount += 1;
                    currentFrame = minFrame;
                }
                else
                {
                    destroy = true;
                }
            }
        }
    }
}
