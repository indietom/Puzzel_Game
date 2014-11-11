using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Puzzel_game
{
    class objects
    {
        public float x { get; set; }
        public float y { get; set; }

        public int imx { get; set; }
        public int imy { get; set; }
        public int width { get; set; }
        public int height { get; set; }

        public short hp { get; set; }

        public sbyte currentFrame { get; set; }
        public sbyte animationCount { get; set; }
        public sbyte direction { get; set; }

        public bool destroy { get; set; }
        public bool rotated { get; set; }
        public bool animationActive { get; set; }

        public float angle2 { get; set; }
        public float angle { get; set; }
        public float speed { get; set; }
        public float scale_x { get; set; }
        public float scale_y { get; set; }
        public float veclocity_x { get; set; }
        public float veclocity_y { get; set; }

        public float lerp(float x2, float y2, float t)
        {
            return x2 + t * (y2 - x2);
        }

        public float distanceTo(float x2, float y2)
        {
            return (float)Math.Sqrt((x - x2) * (x - x2) + (y - y2) * (y - y2));
        }

        public float distanceTo(float x2, float y2, bool center)
        {
            return (float)Math.Sqrt((x + width / 2 - x2) * (x + width / 2 - x2) + (y + height / 2 - y2) * (y + height / 2 - y2));
        }

        public void angleMath()
        {
            angle2 = (angle * (float)Math.PI / 180);
            scale_x = (float)Math.Cos(angle2);
            scale_y = (float)Math.Sin(angle2);
            veclocity_x = (speed * scale_x);
            veclocity_y = (speed * scale_y);
        }

        public void mathAim(float x2, float y2)
        {
            angle = (float)Math.Atan2(y2 - y, x2 - x);
            veclocity_x = (speed * (float)Math.Cos(angle));
            veclocity_y = (speed * (float)Math.Sin(angle));
        }

        public void radianMath()
        {
            veclocity_x = (speed * (float)Math.Cos(angle));
            veclocity_y = (speed * (float)Math.Sin(angle));
        }

        public float findTarget(float x2, float y2)
        {
            return (float)Math.Atan2(y2 - y, x2 - x);
        }

        public int frame(int frame2)
        {
            return frame2 * 32 + frame2 + 1;
        }

        public int frame(int Frame2, int size)
        {
            return Frame2 * size + Frame2 + 1;
        }

        public void checkOnScreen()
        {
            if (x >= 640 || x <= -width || y >= 480 || y <= -height)
                destroy = true;
        }

        public void setCoords(float x2, float y2)
        {
            x = x2;
            y = y2;
        }
        public void setSpriteCoords(int imx2, int imy2)
        {
            imx = imx2;
            imy = imy2;
        }

        public void setSize(int w2, int h2)
        {
            width = w2;
            height = h2;
        }
        public void setSize(int size)
        {
            width = size;
            height = size;
        }
        public void drawSprite(SpriteBatch spritebatch, Texture2D spritesheet)
        {
            spritebatch.Draw(spritesheet, new Vector2(x, y), new Rectangle(imx, imy, width, height), Color.White);
        }

        public void drawSprite(SpriteBatch spritebatch, Texture2D spritesheet, Color color)
        {
            spritebatch.Draw(spritesheet, new Vector2(x, y), new Rectangle(imx, imy, width, height), color);
        }
        public void drawSprite(SpriteBatch spritebatch, Texture2D spritesheet, float size)
        {
            spritebatch.Draw(spritesheet, new Vector2(x,y), new Rectangle(imx, imy, width, height), Color.White, angle, new Vector2(width / 2, height / 2), size, SpriteEffects.None, 0);
        }
        public void drawSpriteAngle(SpriteBatch spritebatch, Texture2D spritesheet, float size)
        {
            spritebatch.Draw(spritesheet, new Vector2(x, y), new Rectangle(imx, imy, width, height), Color.White, angle * (float)Math.PI / 180, new Vector2(width / 2, height / 2), size, SpriteEffects.None, 0);
        }
        public void drawSprite(SpriteBatch spritebatch, Texture2D spritesheet, float size, Color color)
        {
            spritebatch.Draw(spritesheet, new Vector2(x, y), new Rectangle(imx, imy, width, height), color, angle * (float)Math.PI / 180, new Vector2(width / 2, height / 2), size, SpriteEffects.None, 0);
        }
        public void drawSprite(SpriteBatch spritebatch, Texture2D spritesheet, float size, float rotation, Color color)
        {
            spritebatch.Draw(spritesheet, new Vector2(x, y), new Rectangle(imx, imy, width, height), color, rotation * (float)Math.PI / 180, new Vector2(width / 2, height / 2), size, SpriteEffects.None, 0);
        }
        public void drawText(SpriteBatch spriteBatch, SpriteFont font, float rotation, float size, string text, float x, float y, Color color2)
        {
            spriteBatch.DrawString(font, text.ToUpper(), new Vector2(x, y), color2, rotation, Vector2.Zero, size, SpriteEffects.None, 1);
        }
        public void drawText(SpriteBatch spriteBatch, SpriteFont font, float rotation, float size, string text, float x, float y, Color color2, Vector2 orgin)
        {
            spriteBatch.DrawString(font, text.ToUpper(), new Vector2(x, y), color2, rotation, orgin, size, SpriteEffects.None, 1);
        }
    }
}
