using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RunJumpSwingMan {

	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class RunJumpSwingMan : Game {

		private GraphicsDeviceManager graphics;
		private SpriteBatch spriteBatch;

		private Camera camera;
		private List<VertexPositionTexture[]> objects;

		private Vector3 cameraPosition = new Vector3( 0.0f, 40.0f, 20.0f );
		private Vector3 cameraTarget = Vector3.Zero;
		private Vector3 cameraUpVector = Vector3.UnitZ;
		private float lookAngleX = 0.0f, lookAngleY = 0.0f;
		private float rotationSpeed = 0.05f;
		private int halfWidth, halfHeight;

		private MouseState previousMouseState;

		public RunJumpSwingMan() {
			graphics = new GraphicsDeviceManager( this );
			

			Content.RootDirectory = "Content";
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize() {
			//Initialize camera and objects list
			camera = new Camera(graphics);
			objects = new List<VertexPositionTexture[]>();

			//Create objects
			VertexPositionTexture[]  floorVertices = new VertexPositionTexture[ 6 ];
			floorVertices[ 0 ].Position = new Vector3( -20.0f, -20.0f, 0.0f );
			floorVertices[ 1 ].Position = new Vector3( -20.0f, 20.0f, 0.0f );
			floorVertices[ 2 ].Position = new Vector3( 20.0f, -20.0f, 0.0f );
			floorVertices[ 3 ].Position = floorVertices[ 1 ].Position;
			floorVertices[ 4 ].Position = new Vector3( 20.0f, 20.0f, 0.0f );
			floorVertices[ 5 ].Position = floorVertices[ 2 ].Position;

			//Add all objects to the list
			objects.Add(floorVertices);

			halfWidth = graphics.GraphicsDevice.Viewport.Width / 2;
			halfHeight = graphics.GraphicsDevice.Viewport.Height / 2;

			//Center mouse
			Mouse.SetPosition(halfWidth, halfHeight);
			Mouse.SetCursor(MouseCursor.Crosshair);
			this.IsMouseVisible = false;

			previousMouseState = Mouse.GetState();
			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent() {
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch( GraphicsDevice );
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// game-specific content.
		/// </summary>
		protected override void UnloadContent() {
			// TODO: Unload any non ContentManager content here
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update( GameTime gameTime ) {
			if ( Keyboard.GetState().IsKeyDown( Keys.Escape ) ) {
				Exit();
			}

			base.Update( gameTime );
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime) {

			MouseState currentMouseState = Mouse.GetState();
			int moveX = 0;
			int moveY = 0;
			if (currentMouseState.X < halfWidth && currentMouseState.X < previousMouseState.X)
				moveX = -1;
			else if (currentMouseState.X > halfWidth && currentMouseState.X > previousMouseState.X)
				moveX = 1;

			if (currentMouseState.Y < halfHeight && currentMouseState.Y < previousMouseState.Y)
				moveY = -1;
			else if (currentMouseState.Y > halfHeight && currentMouseState.Y > previousMouseState.Y)
				moveY = 1;

			float newLookAngleX = lookAngleX + moveX * rotationSpeed;
			if (newLookAngleX >= -Math.PI/4 && newLookAngleX <= Math.PI/4) {
				lookAngleX = newLookAngleX;
				lookAngleX = (float)(((double)lookAngleX) % (2 * Math.PI));
			}

			float newLookAngleY = lookAngleY + moveY * rotationSpeed;
			if (newLookAngleY >= -Math.PI/4 && newLookAngleY <= Math.PI/4)
			{
				lookAngleY = newLookAngleY;
				lookAngleY = (float)(((double)lookAngleY) % (2 * Math.PI));
			}


			/*
			cameraTarget = new Vector3(	cameraLookAtVector.X + moveX,
												cameraLookAtVector.Y + moveY,
												cameraLookAtVector.Z			);
			*/
			previousMouseState = Mouse.GetState();
			Mouse.SetPosition(halfWidth, halfHeight);

			GraphicsDevice.Clear( Color.DarkSlateGray );
			//Vector3 cameraTargetRotated = Vector3.Cross();
			camera.Update( objects, cameraPosition, cameraTarget, cameraUpVector, lookAngleX, lookAngleY);
			base.Draw( gameTime );
		}
	}
}
