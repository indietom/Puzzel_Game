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

        static public Keys left;
        static public Keys right;
        static public Keys down;
        static public Keys up;

        static public sbyte environment = 0;
        Color envColor;

        public static bool playerBefore = false;
        public static bool playing = false;
        public static bool nukeDropped = false;
        public static bool activeBlocks = false;

        public static bool leftBlocksLeft;
        public static bool rightBlocksLeft;

        static public sbyte controlScheme;

        byte nukeWastecount;

        public static int grid(int cell)
        {
            return cell * 32;
        }

        static public char joyButtonHit(GamePadState gamepad, GamePadState prevGamepad)
        {
            if (gamepad.IsButtonDown(Buttons.A) && prevGamepad.IsButtonUp(Buttons.A))
                return 'a';
            if (gamepad.IsButtonDown(Buttons.B) && prevGamepad.IsButtonUp(Buttons.B))
                return 'b';
            if (gamepad.IsButtonDown(Buttons.Start) && prevGamepad.IsButtonUp(Buttons.Start))
                return 's';
            return ' ';
        }

        static public char joyHit(GamePadState gamepad, GamePadState prevGamepad)
        {
            if (gamepad.ThumbSticks.Left.X <= -0.2 && prevGamepad.ThumbSticks.Left.X == 0)
            {
                return 'l';
            }
            if (gamepad.ThumbSticks.Left.X >= 0.2 && prevGamepad.ThumbSticks.Left.X == 0)
            {
                return 'r';
            }
            if (gamepad.ThumbSticks.Left.Y >= 0.2 && prevGamepad.ThumbSticks.Left.Y == 0)
            {
                return 'u';
            }
            if (gamepad.ThumbSticks.Left.Y <= -0.2 && prevGamepad.ThumbSticks.Left.Y == 0)
            {
                return 'd';
            }
            return ' ';
        }

        public static void sort(ref List<int> ar)
        {
            for (int i = 1; i < ar.Count; i++)
            {
                for (int j = 0; j < ar.Count - i; j++)
                {
                    if (ar[j] > ar[j + 1])
                    {
                        int tmp = ar[j];
                        ar[j] = ar[j + 1];
                        ar[j + 1] = tmp;
                    }
                }
            }
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 640;
            Content.RootDirectory = "Content";
        }

        KeyboardState keyboard;
        KeyboardState prevKeyboard;

        GamePadState gamepad;
        GamePadState prevGamepad;

        ui ui = new ui();
        player player;
        level level = new level();
        store store = new store();
        menu menu = new menu();
        fileManager fileManager = new fileManager();
        highscoreScreen highscoreScreen = new highscoreScreen();
        spawner spawner = new spawner();
        options options = new options();

        List<block> blocks = new List<block>();
        List<window> windows = new List<window>();
        List<effect> effects = new List<effect>();
        List<particle> particles = new List<particle>();
        List<backgroundObject> backgroundObjects = new List<backgroundObject>();

        public static string gameState;

        protected override void Initialize()
        {
            player = new player();

            fileManager.loadGame(ref player);

            gameState = "menu";
            blocks.Clear();
            ui = new ui();

            left = Keys.Left;
            right = Keys.Right;
            down = Keys.Down;
            up = Keys.Up;

            for (int i = 0; i < 640 / 32; i++)
            {
                backgroundObjects.Add(new backgroundObject(i * 32, 480 - 64 * 2, 1, 364, 32, 32, -1, 1));
                backgroundObjects.Add(new backgroundObject(i * 32, 480 - 32 * 3, 1, 364, 32, 32, -1, 1));
            }

            //blocks.Add(new block(grid(11), grid(0), 1, 2));
            //blocks.Add(new block(grid(10), grid(0), 1, 2));

            //windows.Add(new window(20, 20, 20, 20));

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

        public void updateNuke()
        {
            Random random = new Random();
            if(nukeDropped)
            {
                for (int i = 0; i < 10000; i++)
                {
                    effects.Add(new effect(random.Next(640), random.Next(480), 0, 6, 298, 16, 4));
                }
                player.nukeCount -= 1;
                blocks.Clear();
                nukeWastecount = 1;
                nukeDropped = false;
            }
            if(nukeWastecount >= 1)
            {
                nukeWastecount += 1;
            }
            if(nukeWastecount >= 124)
            {
                for(int i = 0; i < 640; i++)
                {
                    particles.Add(new particle(i, random.Next(-700, -32), 2, 3, 0, Color.White, 90, random.Next(-3, 0)));
                }
                nukeWastecount = 0;
            }
        }

        protected override void Update(GameTime gameTime)
        {
            Random random = new Random();

            if(controlScheme == 0)
            {
                left = Keys.Left;
                right = Keys.Right;
                down = Keys.Down;
                up = Keys.Up;
            }
            if (controlScheme == 1)
            {
                left = Keys.A;
                right = Keys.D;
                down = Keys.S;
                up = Keys.W;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.F10) && keyboard.IsKeyDown(Keys.F11))
                this.Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.F1))
                Initialize();

            prevKeyboard = keyboard;
            keyboard = Keyboard.GetState();

            prevGamepad = gamepad;
            gamepad = GamePad.GetState(PlayerIndex.One);

            if(keyboard.IsKeyDown(Keys.F4) && prevKeyboard.IsKeyUp(Keys.F4))
            {
                nukeDropped = true;
            }
            if (keyboard.IsKeyDown(Keys.F2) && prevKeyboard.IsKeyUp(Keys.F2))
            {
                fileManager.saveGame(player);
            }
            if (keyboard.IsKeyDown(Keys.F3) && prevKeyboard.IsKeyUp(Keys.F3))
            {
                blocks.Add(new block(grid(5), 0, 4, 2));
                fileManager.saveHighScore(player);
            }

            switch (gameState)
            {
                case "highscore":
                    highscoreScreen.update();
                    break;
                case "options":
                    options.update();
                    if (keyboard.IsKeyDown(Keys.Escape) && prevKeyboard.IsKeyUp(Keys.Escape) || joyButtonHit(gamepad, prevGamepad) == 'b')
                    {
                        gameState = "menu";
                    }
                    break;
                case "menu":
                    highscoreScreen.delay = 0;
                    menu.update();
                    if (keyboard.IsKeyDown(Keys.Escape) && prevKeyboard.IsKeyUp(Keys.Escape) && playing || joyButtonHit(gamepad, prevGamepad) == 's' && playing)
                    {
                        gameState = "game";
                    }
                    break;
                case "store":
                    store.update(ref player, fileManager);
                    if (keyboard.IsKeyDown(Keys.Escape) && prevKeyboard.IsKeyUp(Keys.Escape) || joyButtonHit(gamepad, prevGamepad) == 'b')
                    {
                        fileManager.saveGame(player);
                        store.delay = 0;
                        gameState = "menu";
                    }
                    break;
                case "game":
                    menu.reset();
                    level.update();
                    spawner.update(blocks, backgroundObjects, ref player);
                    if (keyboard.IsKeyDown(Keys.Escape) && prevKeyboard.IsKeyUp(Keys.Escape) || joyButtonHit(gamepad, prevGamepad) == 's')
                    {
                        gameState = "menu";
                    }
                    if (keyboard.IsKeyDown(Keys.D4) && prevKeyboard.IsKeyUp(Keys.D4) && !spawner.lost && player.nukeCount >= 1)
                    {
                        nukeDropped = true;
                    }
                    updateNuke();
                    playing = true;
                    ui.update(player, level, blocks, fileManager);
                    foreach(particle p in particles)
                    {
                        p.update();
                    }
                    foreach(effect e in effects)
                    {
                        e.update();
                    }

                    foreach (block b in blocks)
                    {
                        b.update(blocks, effects, particles, ref player, level);
                        b.input();
                    }
                    foreach (backgroundObject bo in backgroundObjects)
                    {
                        bo.update();
                        bo.movment();
                    }

                    foreach (window w in windows)
                    {
                        w.update();
                    }
                    break;
            }
            for (int i = 0; i < windows.Count; i++)
            {
                if (windows[i].destroy)
                    windows.RemoveAt(i);
            }
            for (int i = 0; i < blocks.Count; i++)
            {
                if (blocks[i].destroy)
                    blocks.RemoveAt(i);
            }
            for (int i = 0; i < effects.Count; i++)
            {
                if (effects[i].destroy)
                    effects.RemoveAt(i);
            }
            for (int i = 0; i < particles.Count; i++)
            {
                if (particles[i].destroy)
                    particles.RemoveAt(i);
            }
            for (int i = 0; i < backgroundObjects.Count; i++)
            {
                if (backgroundObjects[i].destroy)
                    backgroundObjects.RemoveAt(i);
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
            envColor = (environment == 0) ? Color.CornflowerBlue : Color.Black;
            GraphicsDevice.Clear(envColor);
            spriteBatch.Begin();
            switch (gameState)
            {
                case "highscore":
                    highscoreScreen.draw(spriteBatch, spritesheet, font, fileManager);
                    break;
                case "options":
                    options.draw(spriteBatch, spritesheet, font);
                    break;
                case "menu":
                    menu.draw(spriteBatch, spritesheet, font);
                    break;
                case "store":
                    store.draw(spriteBatch, spritesheet, font);
                    break;
                case "game":
                    //foreach (block b in blocks) { b.drawSprite(spriteBatch, spritesheet, b.color); b.drawText(spriteBatch, font, 0, 1.0f, b.blockTouching.ToString(), b.x, b.y, Color.LightGreen);}
                    foreach (backgroundObject bo in backgroundObjects) { bo.drawSprite(spriteBatch, spritesheet); }
                    drawGrid();
                    foreach (block b in blocks) { b.drawSprite(spriteBatch, spritesheet, b.color); }
                    foreach (effect e in effects) { e.drawSprite(spriteBatch, spritesheet); }
                    foreach (particle p in particles) { if (!p.rotated) { p.drawSprite(spriteBatch, spritesheet, p.color); } else { p.drawSprite(spriteBatch, spritesheet, 1.0f, p.rotation, p.color); } }
                    ui.draw(spritesheet, spriteBatch, font);
                    foreach (window w in windows) { w.draw(spriteBatch, spritesheet, font); }
                    break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
