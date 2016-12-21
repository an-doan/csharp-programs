using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Lab15
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //create SoundEffect object for each sound
        SoundEffect upperLeft;
        SoundEffect upperRight;
        SoundEffect lowerLeft;
        SoundEffect lowerRight;

        bool mouseClickStarted = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //make mouse visible
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

            // TODO: use this.Content to load your game content here
            
            //load sound effects
            upperLeft = Content.Load<SoundEffect>(@"audio/upperLeft");
            upperRight = Content.Load<SoundEffect>(@"audio/upperRight");
            lowerLeft = Content.Load<SoundEffect>(@"audio/lowerLeft");
            lowerRight = Content.Load<SoundEffect>(@"audio/lowerRight");
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

            //get mouse state
            MouseState mouse = Mouse.GetState();

            //check mouse location to play sound effect
            //check if left button pressed and mouse click not started yet
            if (!mouseClickStarted && mouse.LeftButton == ButtonState.Pressed)
            {
                if (mouse.X <= graphics.PreferredBackBufferWidth / 2 && mouse.Y <= graphics.PreferredBackBufferHeight / 2)
                {
                    //play sound effect for upper left
                    upperLeft.Play();
                }
                if (mouse.X > graphics.PreferredBackBufferWidth / 2 && mouse.Y <= graphics.PreferredBackBufferHeight / 2)
                {
                    //play sound effect for upper right
                    upperRight.Play();
                }
                if (mouse.X <= graphics.PreferredBackBufferWidth / 2 && mouse.Y > graphics.PreferredBackBufferHeight / 2)
                {
                    //play sound effect for lower left
                    lowerLeft.Play();
                }
                if (mouse.X > graphics.PreferredBackBufferWidth / 2 && mouse.Y > graphics.PreferredBackBufferHeight / 2)
                {
                    //play sound effect for lower right
                    lowerRight.Play();
                }
                mouseClickStarted = true;
            }
            else if (mouseClickStarted && mouse.LeftButton == ButtonState.Released)
            {
                mouseClickStarted = false;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
