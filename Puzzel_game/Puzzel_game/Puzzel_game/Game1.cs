using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Puzzel_game
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static int grid(int cell)
        {
            return cell * 32;
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 640;
            Content.RootDirectory = "Content";
        }

        ui ui = new ui();
        List<block> blocks = new List<block>();

        protected override void Initialize()
        {
            blocks.Clear();
            ui = new ui();

            blocks.Add(new block(grid(12), 0, 1, 1));
            blocks.Add(new block(grid(12), grid(-3), 1, 3));
            blocks.Add(new block(grid(10), grid(-3), 1, 2));

            base.Initialize();
        }

        Texture2D spritesheet;
        SpriteFont font;
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spritesheet = Content.Load<Texture2D>("spritesheet");
            font = Content.Load<SpriteFont>("font");
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            foreach(block b in blocks)
            {
                b.update(blocks);
                b.input();
            }

            base.Update(gameTime);
        }

        void drawGrid()
        {
            sbyte gridW = 10;
            sbyte gridH = 13;
            for(int i = 0; i < gridH; i++)
            {
                for(int j = 0; j < gridW; j++)
                {
                    spriteBatch.Draw(spritesheet, new Vector2(j * 32+320/2, i * 32), new Rectangle(1, 34, 32, 32), Color.White);
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            drawGrid();
            ui.draw(spritesheet, spriteBatch);
            foreach (block b in blocks) { b.drawSprite(spriteBatch, spritesheet, b.color); }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}