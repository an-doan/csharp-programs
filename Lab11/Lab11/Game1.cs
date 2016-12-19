using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ExplodingTeddies;

namespace Lab11
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public const int WindowWidth = 800;
        public const int WindowHeight = 600;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        TeddyBear bear;
        Explosion explosion;

        Random rand = new Random();

        ButtonState previousRightButtonState = ButtonState.Released;
        ButtonState previousLeftButtonState = ButtonState.Released;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = WindowWidth;
            graphics.PreferredBackBufferHeight = WindowHeight;

            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            SpawnBear();
            
            explosion = new Explosion(Content, @"graphics\explosion");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            MouseState mouse = Mouse.GetState();

            //if(bear.DrawRectangle.Contains(mouse.Position) && mouse.LeftButton == ButtonState.Released &&
            //    previousLeftButtonState == ButtonState.Pressed)
            //{
            //    bear.Active = false;
            //    explosion.Play(bear.DrawRectangle.X, bear.DrawRectangle.Y);
            //}

            if (bear.DrawRectangle.Contains(mouse.Position) && mouse.RightButton == ButtonState.Released && previousRightButtonState == ButtonState.Pressed)
            {
                bear.Active = false;
                explosion.Play(bear.DrawRectangle.X, bear.DrawRectangle.Y);
                SpawnBear();
            }

            bear.Update(gameTime);
            explosion.Update(gameTime);

            base.Update(gameTime);
            previousLeftButtonState = mouse.LeftButton;
            previousRightButtonState = mouse.RightButton;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            bear.Draw(spriteBatch);
            explosion.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void SpawnBear()
        {
            // Create a teddy bear with a random velocity and location

            int x = 100 + rand.Next(WindowWidth - 199);
            int y = 100 + rand.Next(WindowHeight - 199);

            double speed = rand.NextDouble() / 10 + .1;
            double angle = 2 * Math.PI * rand.NextDouble();
            Vector2 velocity;
            velocity.X = (float)(Math.Cos(angle) * speed);
            velocity.Y = (float)(Math.Sin(angle) * speed);

            bear = new TeddyBear(Content, WindowWidth, WindowHeight, @"graphics\teddybear", x, y, velocity);

        }
    }

}
