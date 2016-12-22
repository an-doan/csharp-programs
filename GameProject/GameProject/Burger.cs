using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameProject
{
    /// <summary>
    /// A burger
    /// </summary>
    public class Burger
    {
        #region Fields

        // graphic and drawing info
        Texture2D sprite;
        Rectangle drawRectangle;

        // burger stats
        int health = 100;

        // shooting support
        bool canShoot = true;
        int elapsedCooldownMilliseconds = 0;

        // sound effect
        SoundEffect shootSound;

        #endregion

        #region Constructors

        /// <summary>
        ///  Constructs a burger
        /// </summary>
        /// <param name="contentManager">the content manager for loading content</param>
        /// <param name="spriteName">the sprite name</param>
        /// <param name="x">the x location of the center of the burger</param>
        /// <param name="y">the y location of the center of the burger</param>
        /// <param name="shootSound">the sound the burger plays when shooting</param>
        public Burger(ContentManager contentManager, string spriteName, int x, int y,
            SoundEffect shootSound)
        {
            LoadContent(contentManager, spriteName, x, y);
            this.shootSound = shootSound;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the collision rectangle for the burger
        /// </summary>
        public Rectangle CollisionRectangle
        {
            get { return drawRectangle; }
        }

        /// <summary>
        /// Gets the health for the burger
        /// </summary>
        public int Health
        {
            get { return health; }

            set
            {
                if (value >= 0)
                {
                    health = value;
                }
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Updates the burger's location based on keyboard. Also fires 
        /// french fries as appropriate
        /// </summary>
        /// <param name="gameTime">game time</param>
        /// <param name="keyboard">the current state of the keyboard</param>
        public void Update(GameTime gameTime, KeyboardState keyboard)
        {
            // burger should only respond to input if it still has health
            if (health > 0)
            {
                // move burger using WASD keys or arrow keys (not required, but this is easier for me to control) on keyboard
                if (keyboard.IsKeyDown(Keys.W) || keyboard.IsKeyDown(Keys.Up))
                {
                    drawRectangle.Y -= GameConstants.BurgerMovementAmount;
                }
                if (keyboard.IsKeyDown(Keys.A) || keyboard.IsKeyDown(Keys.Left))
                {
                    drawRectangle.X -= GameConstants.BurgerMovementAmount;
                }
                if (keyboard.IsKeyDown(Keys.S) || keyboard.IsKeyDown(Keys.Down))
                {
                    drawRectangle.Y += GameConstants.BurgerMovementAmount;
                }
                if (keyboard.IsKeyDown(Keys.D) || keyboard.IsKeyDown(Keys.Right))
                { 
                    drawRectangle.X += GameConstants.BurgerMovementAmount;
                }

                // clamp burger in window
                if (drawRectangle.Left < 0)
                {
                    drawRectangle.X = 0;
                }
                if (drawRectangle.Right > GameConstants.WindowWidth)
                {
                    drawRectangle.X = GameConstants.WindowWidth - drawRectangle.Width;
                }
                if (drawRectangle.Top < 0)
                {
                    drawRectangle.Y = 0;
                }
                if (drawRectangle.Bottom > GameConstants.WindowHeight)
                {
                    drawRectangle.Y = GameConstants.WindowHeight - drawRectangle.Height;
                }


                // update shooting allowed
                //check if canShoot is false
                if(canShoot == false)
                {
                    //add elapsed game time to cooldown time
                    elapsedCooldownMilliseconds += gameTime.ElapsedGameTime.Milliseconds;

                    //if cooldown time is equal or greater total cooldown time, allow burger to shoot 
                    //if space button is released, allow burger to shoot
                    if (elapsedCooldownMilliseconds >= GameConstants.BurgerTotalCooldownMilliseconds || keyboard.IsKeyUp(Keys.Space))
                    {
                        canShoot = true;

                        //reset cooldown time 
                        elapsedCooldownMilliseconds = 0;
                    }
                }


                // timer concept (for animations) introduced in Chapter 7

                // shoot if appropriate
                //check if spacce was pressed and can shoot
                if (keyboard.IsKeyDown(Keys.Space) && canShoot)
                {
                    //create x and y values for projectile
                    int projectileX = drawRectangle.X + drawRectangle.Width / 2;

                    //minus the offset so that the french fries are offset above, since the french fries move up
                    int projectileY = drawRectangle.Y + drawRectangle.Height / 2 - GameConstants.FrenchFriesProjectileOffset;

                    //create new projectile 
                    //velocity is negative so that french fries go up

                    Projectile newProjectile = new Projectile(ProjectileType.FrenchFries,
                        Game1.GetProjectileSprite(ProjectileType.FrenchFries),
                        projectileX, projectileY, -(GameConstants.FrenchFriesProjectileSpeed));
                    Game1.AddProjectile(newProjectile);

                    //play shooting sound if not null
                    if (shootSound != null)
                    {
                        shootSound.Play();
                    }

                    //set canShoot to false so that burger cannot continuously shoot
                    canShoot = false;

                }
            }
            
        }

        /// <summary>
        /// Draws the burger
        /// </summary>
        /// <param name="spriteBatch">the sprite batch to use</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, drawRectangle, Color.White);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Loads the content for the burger
        /// </summary>
        /// <param name="contentManager">the content manager to use</param>
        /// <param name="spriteName">the name of the sprite for the burger</param>
        /// <param name="x">the x location of the center of the burger</param>
        /// <param name="y">the y location of the center of the burger</param>
        private void LoadContent(ContentManager contentManager, string spriteName,
            int x, int y)
        {
            // load content and set remainder of draw rectangle
            sprite = contentManager.Load<Texture2D>(spriteName);
            drawRectangle = new Rectangle(x - sprite.Width / 2,
                y - sprite.Height / 2, sprite.Width,
                sprite.Height);
        }

        #endregion
    }
}
