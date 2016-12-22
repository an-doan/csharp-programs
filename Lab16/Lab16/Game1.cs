using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lab16
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //teddy bear objects
        Texture2D teddyBear;
        Rectangle drawRectangle;

        //constant for moving bear speed
        const int BEAR_MOVE = 5;

        //off screen count 
        SpriteFont font;
        int offScreenCount = 0;
        Vector2 countPosition = new Vector2(20, 10);
        string countString;
        bool isOffscreen = false;

        int centerX;
        int centerY;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            //load teddy bear
            teddyBear = Content.Load<Texture2D>("teddybear");

            centerX = graphics.PreferredBackBufferWidth / 2 - teddyBear.Width / 2;
            centerY = graphics.PreferredBackBufferHeight / 2 - teddyBear.Height / 2;

            drawRectangle = new Rectangle(centerX, centerY, teddyBear.Width, teddyBear.Height);

            //load sprite font
            font = Content.Load<SpriteFont>("Arial20");

            //load string
            countString = GetCountString(offScreenCount);
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
            //add keyboard state
            KeyboardState keyboard = Keyboard.GetState();

            //check for keyboard pressing WASD keys to move
            if (keyboard.IsKeyDown(Keys.W))
            {
                drawRectangle.Y -= BEAR_MOVE;
            }
            if (keyboard.IsKeyDown(Keys.A))
            {
                drawRectangle.X -= BEAR_MOVE;
            }
            if (keyboard.IsKeyDown(Keys.S))
            {
                drawRectangle.Y += BEAR_MOVE;
            }
            if (keyboard.IsKeyDown(Keys.D))
            {
                drawRectangle.X += BEAR_MOVE;
            }
            if (keyboard.IsKeyDown(Keys.Space))
            {
                drawRectangle.X = centerX;
                drawRectangle.Y = centerY;
            }

            //set offset count
            
            if (drawRectangle.Right > graphics.PreferredBackBufferWidth || drawRectangle.Left < 0
                || drawRectangle.Top < 0 || drawRectangle.Bottom > graphics.PreferredBackBufferHeight)
            {
                if(!isOffscreen)
                {
                    isOffscreen = true;
                    offScreenCount += 1;
                    countString = GetCountString(offScreenCount);
                }
            }
            else if (isOffscreen)
            {
                isOffscreen = false;
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
            spriteBatch.Begin();

            spriteBatch.Draw(teddyBear, drawRectangle, Color.White);
            spriteBatch.DrawString(font, countString, countPosition, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// gets the offscreen count string 
        /// </summary>
        /// <param name="count">offscreen count</param>
        /// <returns></returns>
        private string GetCountString(int count)
        {
            return "Offscreen Count: " + offScreenCount;
        }
        
    }
}
