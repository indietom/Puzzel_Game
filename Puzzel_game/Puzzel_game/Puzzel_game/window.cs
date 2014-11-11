using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Puzzel_game
{
    class window:objects
    {
        byte winWidth;
        byte winHeight;

        string mainBodyText;
        string titleText;
        string subText;

        public window(float x2, float y2, byte width2, byte height2)
        {
            setCoords(x2, y2);
            winWidth = width2;
            winHeight = height2;
        }
        public void update()
        {
            
        }
        public void draw(SpriteBatch spriteBatch, Texture2D spritesheet, SpriteFont font)
        {
            for(int i = 0; i < winWidth; i++)
            {
                for(int j = 0; j < winHeight; j++)
                {
                    spriteBatch.Draw(spritesheet, new Vector2(x + i * 8, y + j * 8), new Rectangle(463, 1, 8, 8), Color.White);

                    if(i == 0 && j == 0)
                    {
                        spriteBatch.Draw(spritesheet, new Vector2(x, y), new Rectangle(430, frame(0, 8), 8, 8), Color.White);
                    }
                    if(j == 0 && i != winWidth && i != 0)
                    {
                        spriteBatch.Draw(spritesheet, new Vector2(x + i * 8, y), new Rectangle(430, frame(7, 8), 8, 8), Color.White);
                    }
                    if (j == winHeight-1 && i != winWidth && i != 0)
                    {
                        spriteBatch.Draw(spritesheet, new Vector2(x + i * 8, y + winHeight * 8), new Rectangle(430, frame(6, 8), 8, 8), Color.White);
                    }
                    if (i == 0 && j != 0 && j != winHeight)
                    {
                        spriteBatch.Draw(spritesheet, new Vector2(x, y+j*8), new Rectangle(430, frame(4, 8), 8, 8), Color.White);
                    }
                    if (i == winWidth-1 && j != 0 && j != winHeight)
                    {
                        spriteBatch.Draw(spritesheet, new Vector2(x+winWidth*8, y + j * 8), new Rectangle(430, frame(5, 8), 8, 8), Color.White);
                    }
                    if (i == width && j == winHeight-1)
                    {
                        spriteBatch.Draw(spritesheet, new Vector2(x + winWidth * 8, y + winHeight * 8), new Rectangle(430, frame(3, 8), 8, 8), Color.White);
                    }
                    if (i == width && j == 0)
                    {
                        spriteBatch.Draw(spritesheet, new Vector2(x + winWidth * 8, y), new Rectangle(430, frame(2, 8), 8, 8), Color.White);
                    }
                    if (i == 0 && j == winHeight-1)
                    {
                        spriteBatch.Draw(spritesheet, new Vector2(x, y + winHeight * 8), new Rectangle(430, frame(1, 8), 8, 8), Color.White);
                    }
                }
            }
        }
    }
}
