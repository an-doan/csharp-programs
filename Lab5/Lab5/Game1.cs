﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lab5
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

        Texture2D texture0;
        Texture2D texture1;
        Texture2D texture2;

        Rectangle rectangle0;
        Rectangle rectangle1;
        Rectangle rectangle2;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = WindowWidth;
            graphics.PreferredBackBufferHeight = WindowHeight;

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

            // use this.Content to load your game content here
            texture0 = Content.Load<Texture2D>(@"graphics\teddybear0");
            texture1 = Content.Load<Texture2D>(@"graphics\teddybear1");
            texture2 = Content.Load<Texture2D>(@"graphics\teddybear2");
            //create rectangles to draw textures
            rectangle0 = new Rectangle(100, 100, texture0.Width, texture0.Height);
            rectangle1 = new Rectangle(200, 100, texture1.Width, texture1.Height);
            rectangle2 = new Rectangle(300, 100, texture2.Width, texture2.Height);
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

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // use spriteBatch to draw textures
            spriteBatch.Begin();

            spriteBatch.Draw(texture0, rectangle0, Color.White);
            spriteBatch.Draw(texture1, rectangle1, Color.White);
            spriteBatch.Draw(texture2, rectangle2, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
