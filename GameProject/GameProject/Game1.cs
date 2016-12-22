//Modified by An Doan

using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameProject
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // game objects. Using inheritance would make this
        // easier, but inheritance isn't a GDD 1200 topic
        Burger burger;
        List<TeddyBear> bears = new List<TeddyBear>();
        static List<Projectile> projectiles = new List<Projectile>();
        List<Explosion> explosions = new List<Explosion>();

        // projectile and explosion sprites. Saved so they don't have to
        // be loaded every time projectiles or explosions are created
        static Texture2D frenchFriesSprite;
        static Texture2D teddyBearProjectileSprite;
        static Texture2D explosionSpriteStrip;

        // scoring support
        int score = 0;
        string scoreString = GameConstants.ScorePrefix + 0;

        // health support
        string healthString = GameConstants.HealthPrefix +
            GameConstants.BurgerInitialHealth;
        bool burgerDead = false;

        // text display support
        SpriteFont font;

        // sound effects
        SoundEffect burgerDamage;
        SoundEffect burgerDeath;
        SoundEffect burgerShot;
        SoundEffect explosion;
        SoundEffect teddyBounce;
        SoundEffect teddyShot;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // set resolution
            graphics.PreferredBackBufferWidth = GameConstants.WindowWidth;
            graphics.PreferredBackBufferHeight = GameConstants.WindowHeight;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            RandomNumberGenerator.Initialize();

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

            // load audio content
            burgerDamage = Content.Load<SoundEffect>(@"audio/BurgerDamage");
            burgerDeath = Content.Load<SoundEffect>(@"audio/BurgerDeath");
            burgerShot = Content.Load<SoundEffect>(@"audio/BurgerShot");
            explosion = Content.Load<SoundEffect>(@"audio/Explosion");
            teddyBounce = Content.Load<SoundEffect>(@"audio/TeddyBounce");
            teddyShot = Content.Load<SoundEffect>(@"audio/TeddyShot");

            // load sprite font
            font = Content.Load<SpriteFont>(@"fonts/Arial20");

            // load projectile and explosion sprites
            frenchFriesSprite = Content.Load<Texture2D>(@"graphics\frenchfries");
            teddyBearProjectileSprite = Content.Load<Texture2D>(@"graphics\teddybearprojectile");
            explosionSpriteStrip = Content.Load<Texture2D>(@"graphics\explosion");

            // add initial game objects

            //declare burger: Window height * (7/8) is 7/8 below the top of the screen, or 1/8 above the bottom of the screen
            burger = new Burger(Content, @"graphics\burger", GameConstants.WindowWidth / 2, (int)(GameConstants.WindowHeight * (7.0 / 8.0)), burgerShot);

            //spawn teddy bear
            for (int i = 0; i < GameConstants.MaxBears; i++)
            {
                SpawnBear();
            }


            // set initial health and score strings
            healthString = GameConstants.HealthPrefix + burger.Health;
            scoreString = GameConstants.ScorePrefix + score;
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

            // get current keyboard state and update burger
            KeyboardState keyboard = Keyboard.GetState();
            burger.Update(gameTime, keyboard);

            // update other game objects
            foreach (TeddyBear bear in bears)
            {
                bear.Update(gameTime);
            }
            foreach (Projectile projectile in projectiles)
            {
                projectile.Update(gameTime);
            }
            foreach (Explosion explosion in explosions)
            {
                explosion.Update(gameTime);
            }

            // check and resolve collisions between teddy bears
            //nested for loop to check each bear collision
            for (int i = 0; i < bears.Count; i++)
            {
                for (int j = i + 1; j < bears.Count; j++)
                {
                    //if both bears are active
                    if (bears[i].Active && bears[j].Active)
                    {
                        //create resolution info with check collision method
                        CollisionResolutionInfo bearCollisionInfo = CollisionUtils.CheckCollision(gameTime.ElapsedGameTime.Milliseconds,
                            GameConstants.WindowWidth, GameConstants.WindowHeight, bears[i].Velocity, bears[i].DrawRectangle,
                            bears[j].Velocity, bears[j].DrawRectangle);

                        //if the collision info isn't null, resolve collision
                        if (bearCollisionInfo != null)
                        {
                            // check if first bear is out of bounds
                            if(bearCollisionInfo.FirstOutOfBounds)
                            {
                                //make bear inactive
                                bears[i].Active = false;
                            }
                            else
                            {
                                //else, set velocity and draw rectangle 
                                bears[i].Velocity = bearCollisionInfo.FirstVelocity;
                                bears[i].DrawRectangle = bearCollisionInfo.FirstDrawRectangle;
                            }

                            //do the same with the other bear
                            if (bearCollisionInfo.SecondOutOfBounds)
                            {
                                //make bear inactive
                                bears[j].Active = false;
                            }
                            else
                            {
                                //else, set velocity and draw rectangle 
                                bears[j].Velocity = bearCollisionInfo.SecondVelocity;
                                bears[j].DrawRectangle = bearCollisionInfo.SecondDrawRectangle;
                            }

                            //play bounce sound 
                            teddyBounce.Play();
                        }                      
                    }
                }
            }


            // check and resolve collisions between burger and teddy bears
            foreach (TeddyBear bear in bears)
            {
                //check if they are colliding and bears are active
                if (burger.CollisionRectangle.Intersects(bear.CollisionRectangle) && bear.Active)
                {
                    //make bear inactive
                    bear.Active = false;
                    
                    //add to new explosion to explosions list and play sound
                    explosions.Add(new Explosion(explosionSpriteStrip, bear.Location.X, bear.Location.Y, explosion));

                    //subtract health from burger and play sound
                    burger.Health -= GameConstants.BearDamage;
                    burgerDamage.Play();

                    //check burger kill
                    CheckBurgerKill();

                    //update health string
                    healthString = GameConstants.HealthPrefix + burger.Health;
                }
            }

            // check and resolve collisions between burger and projectiles
            foreach (Projectile projectile in projectiles)
            {
                
                //check if they are colliding with burger and are active
                if (burger.CollisionRectangle.Intersects(projectile.CollisionRectangle) && projectile.Active)
                {
                    //check if projectiles are bear projectiles
                    if (projectile.Type == ProjectileType.TeddyBear)
                    {
                        //make projectile inactive
                        projectile.Active = false;
                        
                        //subtract health from burger and play sound
                        burger.Health -= GameConstants.BearDamage;
                        burgerDamage.Play();

                        //check burger kill
                        CheckBurgerKill();

                        //update health string
                        healthString = GameConstants.HealthPrefix + burger.Health;
                    }
                }
            }

            // check and resolve collisions between teddy bears and projectiles
            foreach (TeddyBear bear in bears)
            {
                //check for collisions with the projectiles
                foreach (Projectile projectile in projectiles)
                {
                    if (bear.CollisionRectangle.Intersects(projectile.CollisionRectangle) && bear.Active)
                    {
                        //check if projectiles are french fries
                        if (projectile.Type == ProjectileType.FrenchFries)
                        {
                            //update score and score string
                            score += GameConstants.BearPoints;
                            scoreString = GameConstants.ScorePrefix + score;

                            //make bear and projectile inactive
                            bear.Active = false;
                            projectile.Active = false;
                            
                            //add to new explosion to explosions list
                            explosions.Add(new Explosion(explosionSpriteStrip, bear.Location.X, bear.Location.Y, explosion));
                        }
                    }
                }
            }

            // clean out inactive teddy bears and add new ones as necessary
            for (int i = bears.Count - 1; i >= 0; i--)
            {
                //if bears aren't active, remove them
                if (!bears[i].Active)
                {
                    bears.RemoveAt(i);
                }
            }

            while (bears.Count < GameConstants.MaxBears)
            {
                SpawnBear();
            }

            // clean out inactive projectiles
            for (int i = projectiles.Count - 1; i >= 0; i--)
            {
                if (!projectiles[i].Active)
                {
                    projectiles.RemoveAt(i);
                }
            }
            
            // clean out finished explosions
            for (int i = explosions.Count - 1; i >= 0; i--)
            {
                if (explosions[i].Finished)
                {
                    explosions.RemoveAt(i);
                }
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

            spriteBatch.Begin();

            // draw game objects
            burger.Draw(spriteBatch);
            foreach (TeddyBear bear in bears)
            {
                bear.Draw(spriteBatch);
            }
            foreach (Projectile projectile in projectiles)
            {
                projectile.Draw(spriteBatch);
            }
            foreach (Explosion explosion in explosions)
            {
                explosion.Draw(spriteBatch);
            }

            // draw score and health
            spriteBatch.DrawString(font, healthString, GameConstants.HealthLocation, Color.White);
            spriteBatch.DrawString(font, scoreString, GameConstants.ScoreLocation, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        #region Public methods

        /// <summary>
        /// Gets the projectile sprite for the given projectile type
        /// </summary>
        /// <param name="type">the projectile type</param>
        /// <returns>the projectile sprite for the type</returns>
        public static Texture2D GetProjectileSprite(ProjectileType type)
        {
            // replace with code to return correct projectile sprite based on projectile type
            if (type == ProjectileType.FrenchFries)
            {
                return frenchFriesSprite;
            }
            else if (type == ProjectileType.TeddyBear)
            {
                return teddyBearProjectileSprite;
            }
            else
            {
                //return null if type is not either projectile types
                return null; 
            }
        }

        /// <summary>
        /// Adds the given projectile to the game
        /// </summary>
        /// <param name="projectile">the projectile to add</param>
        public static void AddProjectile(Projectile projectile)
        {
            projectiles.Add(projectile);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Spawns a new teddy bear at a random location
        /// </summary>
        private void SpawnBear()
        {
            // generate random location : SpawnBorderSize is subtracted twice, one for left/top side and one for right/bottom side

            int randomXLocation = GetRandomLocation(GameConstants.SpawnBorderSize, GameConstants.WindowWidth - (GameConstants.SpawnBorderSize * 2));
            int randomYLocation = GetRandomLocation(GameConstants.SpawnBorderSize, GameConstants.WindowHeight - (GameConstants.SpawnBorderSize * 2));

            // generate random velocity

            //create random speeds
            float randomSpeed = GameConstants.MinBearSpeed + RandomNumberGenerator.NextFloat(GameConstants.BearSpeedRange);

            //create random angle
            float randomAngle = 2 * (float)Math.PI * RandomNumberGenerator.NextFloat(1);

            //create Vector2
            Vector2 randomVelocity = new Vector2((float)Math.Cos(randomAngle) * randomSpeed, (float)Math.Sin(randomAngle) * randomSpeed);

            
            // create new bear
            TeddyBear newBear = new TeddyBear(Content, @"graphics\teddybear", randomXLocation, randomYLocation, randomVelocity, teddyBounce, teddyShot);

            // make sure we don't spawn into a collision
            while (CollisionUtils.IsCollisionFree(newBear.CollisionRectangle, GetCollisionRectangles()) == false)
            {
                //change x and y values
                newBear.X = GetRandomLocation(GameConstants.SpawnBorderSize, GameConstants.WindowWidth - (GameConstants.SpawnBorderSize * 2));
                newBear.Y = GetRandomLocation(GameConstants.SpawnBorderSize, GameConstants.WindowHeight - (GameConstants.SpawnBorderSize * 2));
            }

            
            // add new bear to list
            bears.Add(newBear);
        }

        /// <summary>
        /// Gets a random location using the given min and range
        /// </summary>
        /// <param name="min">the minimum</param>
        /// <param name="range">the range</param>
        /// <returns>the random location</returns>
        private int GetRandomLocation(int min, int range)
        {
            return min + RandomNumberGenerator.Next(range);
        }

        /// <summary>
        /// Gets a list of collision rectangles for all the objects in the game world
        /// </summary>
        /// <returns>the list of collision rectangles</returns>
        private List<Rectangle> GetCollisionRectangles()
        {
            List<Rectangle> collisionRectangles = new List<Rectangle>();
            collisionRectangles.Add(burger.CollisionRectangle);
            foreach (TeddyBear bear in bears)
            {
                collisionRectangles.Add(bear.CollisionRectangle);
            }
            foreach (Projectile projectile in projectiles)
            {
                collisionRectangles.Add(projectile.CollisionRectangle);
            }
            foreach (Explosion explosion in explosions)
            {
                collisionRectangles.Add(explosion.CollisionRectangle);
            }
            return collisionRectangles;
        }

        /// <summary>
        /// Checks to see if the burger has just been killed
        /// </summary>
        private void CheckBurgerKill()
        {
            if (burger.Health == 0)
            {
                burgerDead = true;
                burgerDeath.Play();
            }
        }

        #endregion
    }
}