using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Puzzel_game
{
    class ui:objects
    {
        public void draw(Texture2D spritesheet, SpriteBatch spriteBatch)
        {
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    spriteBatch.Draw(spritesheet, new Vector2(j * 32, i * 32), new Rectangle(frame(15), 1, 32, 32), Color.White);
                    spriteBatch.Draw(spritesheet, new Vector2(j * 32 + 32*10+32*5, i * 32), new Rectangle(frame(15), 1, 32, 32), Color.White);
                }
            }
            for(int i = 0; i < 10; i++)
            {
                spriteBatch.Draw(spritesheet, new Vector2(i * 32 + 32 * 5, 13 * 32), new Rectangle(frame(15), 1, 32, 32), Color.White);
                spriteBatch.Draw(spritesheet, new Vector2(i * 32 + 32 * 5, 14 * 32), new Rectangle(frame(15), 1, 32, 32), Color.White);
            }
        }
    }
}
