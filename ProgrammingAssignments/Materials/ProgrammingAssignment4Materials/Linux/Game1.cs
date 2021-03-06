﻿#region Using Statements
using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;

#endregion

namespace ProgrammingAssignment4
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

		// teddy support
		Texture2D teddySprite;
		TeddyBear teddy;

		// pickup support
		Texture2D pickupSprite;
		List<Pickup> pickups = new List<Pickup>();

		// click processing
		bool rightClickStarted = false;
		bool rightButtonReleased = true;

		public Game1 ()
		{
			graphics = new GraphicsDeviceManager (this);
			Content.RootDirectory = "Content";	            

			// STUDENTS: set resolution and make mouse visible

		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize ()
		{
			// TODO: Add your initialization logic here
			base.Initialize ();
				
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent ()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch (GraphicsDevice);

			// STUDENTS: load teddy and pickup sprites


			// STUDENTS: create teddy object centered in window

		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update (GameTime gameTime)
		{
			// For Mobile devices, this logic will close the Game when the Back button is pressed
			// Exit() is obsolete on iOS
			#if !__IOS__
			if (GamePad.GetState (PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
			    Keyboard.GetState ().IsKeyDown (Keys.Escape)) {
				Exit ();
			}
			#endif

			// STUDENTS: get current mouse state and update teddy


			// check for right click started
			if (mouse.RightButton == ButtonState.Pressed &&
				rightButtonReleased)
			{
				rightClickStarted = true;
				rightButtonReleased = false;
			}
			else if (mouse.RightButton == ButtonState.Released)
			{
				rightButtonReleased = true;

				// if right click finished, add new pickup to list
				if (rightClickStarted)
				{
					rightClickStarted = false;

					// STUDENTS: add a new pickup to the end of the list of pickups


					// STUDENTS: if this is the first pickup in the list, set teddy target

				}
			}

			// check for collision between collecting teddy and targeted pickup
			if (teddy.Collecting &&
				teddy.CollisionRectangle.Intersects(pickups[0].CollisionRectangle))
			{
				// STUDENTS: remove targeted pickup from list (it's always at location 0)


				// STUDENTS: if there's another pickup to collect, set teddy target
                // If not, clear teddy target and stop the teddy from collecting

			}

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw (GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear (Color.CornflowerBlue);

			// draw game objects
			spriteBatch.Begin();
			teddy.Draw(spriteBatch);
			foreach (Pickup pickup in pickups)
			{
				pickup.Draw(spriteBatch);
			}
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
