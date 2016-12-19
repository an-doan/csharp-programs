//created by An Doan

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using TeddyMineExplosion;

namespace ProgrammingAssignment5
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        const int WindowWidth = 800;
        const int WindowHeight = 600;

        // game sprites 
        Texture2D teddySprite;
        Texture2D mineSprite;
        Texture2D explosionSprite;

        //lists for game objects
        List<Mine> mines = new List<Mine>();
        List<TeddyBear> teddybears = new List<TeddyBear>();
        List<Explosion> explosions = new List<Explosion>();

        //teddy bear spawning
        int elapsedTeddyMilliseconds = 0;
        Random rand = new Random();

        // checking for clicks
        ButtonState previousLeftButtonState = ButtonState.Released;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //set window width and height
            graphics.PreferredBackBufferWidth = WindowWidth;
            graphics.PreferredBackBufferHeight = WindowHeight;

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

            //load graphics
            mineSprite = Content.Load<Texture2D>(@"graphics/mine");
            teddySprite = Content.Load<Texture2D>(@"graphics/teddybear");
            explosionSprite = Content.Load<Texture2D>(@"graphics/explosion");
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

            //use foreach loop to update teddys and explosions
            foreach (TeddyBear teddy in teddybears)
            {
                teddy.Update(gameTime);

                //check if teddys ran into each mine
                foreach (Mine mine in mines)
                {
                    if (teddy.CollisionRectangle.Intersects(mine.CollisionRectangle))
                    {
                        //make mine and teddy inactive
                        teddy.Active = false;
                        mine.Active = false;

                        //create intersection rectangle
                        Rectangle intersection = Rectangle.Intersect(teddy.CollisionRectangle, mine.CollisionRectangle);

                        //add to new explosion to explosions list
                        explosions.Add(new Explosion(explosionSprite, intersection.Center.X, intersection.Center.Y));
                    }
                }
            }
            foreach (Explosion explosion in explosions)
            {
                explosion.Update(gameTime);
            }

            //mine function
            // get current mouse state
            MouseState mouse = Mouse.GetState();
            
            //if left button mouse was CLICKED, not pressed
            if (mouse.LeftButton == ButtonState.Pressed &&
                previousLeftButtonState == ButtonState.Released)
            {
                //make mine with mouse location and add to mines list
                mines.Add(new Mine(mineSprite, mouse.X, mouse.Y));
            }

            //make sure teddys spawn every few seconds
            //if it's been over 1-3 seconds, spawn a new teddy
            //*1000 because it is in milliseconds, and the maximum range is exclusive, so the parameter is 4, not 3
            elapsedTeddyMilliseconds += gameTime.ElapsedGameTime.Milliseconds;
            if (elapsedTeddyMilliseconds >= rand.Next(1, 4)*1000)
            {
                //reset elapsed shot time
                elapsedTeddyMilliseconds = 0;
                
                //create random x and y velocities between -0.5 and 0.5, range is already 1, so no need to multiply next double
                Vector2 randomVelocity = new Vector2((float)(-0.5 + rand.NextDouble()), (float)(-0.5 + rand.NextDouble()));

                //create new teddy and add it
                teddybears.Add(new TeddyBear(teddySprite, randomVelocity, WindowWidth, WindowHeight));
            }

            //check for inactive teddies, explosions, and mines and remove them
            for (int i = teddybears.Count - 1; i >= 0; i--)
            {
                if(!teddybears[i].Active)
                {
                    teddybears.RemoveAt(i);
                }
            }

            for (int i = mines.Count - 1; i >= 0; i--)
            {
                if (!mines[i].Active)
                {
                    mines.RemoveAt(i);
                }
            }

            for (int i = explosions.Count - 1; i >= 0; i--)
            {
                if (!explosions[i].Playing)
                {
                    explosions.RemoveAt(i);
                }
            }

            base.Update(gameTime);

            previousLeftButtonState = mouse.LeftButton;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            //begin and end sprite batch
            spriteBatch.Begin();

            //use foreach loop to draw mines and teddys
            foreach (Mine mine in mines)
            {
                mine.Draw(spriteBatch);
            }

            foreach (TeddyBear teddy in teddybears)
            {
                teddy.Draw(spriteBatch);
            }

            foreach (Explosion explosion in explosions)
            {
                explosion.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}