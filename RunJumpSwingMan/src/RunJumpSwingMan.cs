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
		private List<VertexPositionTexture[]> objects; //Objects to render
		private Vector3 cameraPosition = new Vector3( 0.0f, 40.0f, 20.0f );
		private Vector3 cameraTarget = Vector3.Zero;
		private Vector3 cameraUpVector = Vector3.UnitZ;

		//Camera settings
		private float lookAngleX = 0.0f, lookAngleY = 0.0f; //In radians
		private float rotationSpeed = 0.01f;
		private float maxXRotation = (float)Math.PI, maxYRotation = (float)Math.PI; //In radians (2pi for no limit)
		private int halfWidth, halfHeight;

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
			GraphicsDevice.Clear(Color.DarkSlateGray);
			UpdateCamera();
			base.Draw( gameTime );
		}

		/*
		====================
		UpdateCamera()

		  Updates camera position based on mouse movement and passes information to the camera.
		====================
		*/
		void UpdateCamera()
		{
			MouseState currentMouseState = Mouse.GetState();

			int moveX = currentMouseState.X - halfWidth;
			float newLookAngleX = lookAngleX + moveX * rotationSpeed;
			if (newLookAngleX >= -maxXRotation && newLookAngleX <= maxXRotation)
			{
				lookAngleX = newLookAngleX;
				lookAngleX = lookAngleX % (float)(2.0f * Math.PI);
			}

			int moveY = currentMouseState.Y - halfHeight;
			float newLookAngleY = lookAngleY + moveY * rotationSpeed;
			if (newLookAngleY >= -maxYRotation && newLookAngleY <= maxYRotation)
			{
				lookAngleY = newLookAngleY;
				lookAngleY = lookAngleY % (float)(2.0f * Math.PI);
			}
			
			Mouse.SetPosition(halfWidth, halfHeight);

			camera.Update(objects, cameraPosition, cameraTarget, cameraUpVector, lookAngleX, lookAngleY);
		}
	}
}
