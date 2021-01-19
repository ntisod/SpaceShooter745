using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceShooter
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Player player;
        List<Enemy> enemies;
        

        PrintText printText;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            
            //Spelaren
            player = new Player(Content.Load<Texture2D>("ship"),380, 400, 2.5f, 4.5f);

            //Skapa fiender
            enemies = new List<Enemy>();
            Random random = new Random();
            Texture2D tmpSprite = Content.Load<Texture2D>("mine");
            for (int i = 0; i<10; i++)
            {
                int rndX = random.Next(0, Window.ClientBounds.Width - tmpSprite.Width);
                int rndY = random.Next(0, Window.ClientBounds.Height / 2);

                Enemy temp = new Enemy(tmpSprite, rndX, rndY);
                enemies.Add(temp);
            }
            //För utskrift
            printText = new PrintText(Content.Load<SpriteFont>("myFont"));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            //spelaren
            player.Update(Window);
            
            //fiender
            foreach(Enemy e in enemies.ToList())
            {
                if (e.IsAlive) //Kontrollera om fienden lever
                {
                    e.Update(Window);
                }
                else //Ta bort fienden för den är död.
                    enemies.Remove(e);
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            player.Draw(_spriteBatch);

            foreach(Enemy e in enemies)
            {
                e.Draw(_spriteBatch);
            }
            printText.Print("Antal fiender: " + enemies.Count, _spriteBatch, 0, 0);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
