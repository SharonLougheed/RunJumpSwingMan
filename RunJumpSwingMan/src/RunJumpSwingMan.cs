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

		//For camera
		private Camera camera;
		private Vector3 cameraTarget = Vector3.Zero;
		private Vector3 cameraUpVector = Vector3.UnitZ;
		private Vector3 cameraPosition = new Vector3(0.0f, 40.0f, 20.0f);
		private float lookAngleX = 0.0f, lookAngleY = 0.0f; //In radians
		private float rotationSpeed = 0.01f;
		private float maxXRotation = 2.0f * (float)Math.PI, maxYRotation = 2.0f*(float)Math.PI; //In radians (2pi for no limit)
		private int halfWidth, halfHeight;
		private float playerForwardWalkSpeed = 1.0f;
		private float playerBackwardWalkSpeed = 0.75f;
		private float playerSidewaysWalkSpeed = 1.0f;
		private float playerRunMultiplier = 2.0f;

		//For floor
		private Texture2D checkerboardTexture;

		//Objects to render
		private Model spikeModel;
		private VertexPositionNormalTexture[] floorVertices;
		BasicEffect floorEffect;

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
			base.Initialize(); //This needs to be done first to load content

			//Initialize camera and objects list
			camera = new Camera(graphics);

			//Initialize ground and spike models
			InitializeGround();
			InitializeSpike();

			halfWidth = graphics.GraphicsDevice.Viewport.Width / 2;
			halfHeight = graphics.GraphicsDevice.Viewport.Height / 2;

			//Center mouse
			Mouse.SetPosition(halfWidth, halfHeight);
			Mouse.SetCursor(MouseCursor.Crosshair);
			this.IsMouseVisible = false;
		}


		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent() {
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch( GraphicsDevice );

			checkerboardTexture = Content.Load<Texture2D>( "RunJumpSwingMan/out/DesktopGL/textures/checkerboard" );

			spikeModel = Content.Load<Model>("spike");
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
			else if ( Keyboard.GetState().IsKeyDown( Keys.W ) ) {
				if ( Keyboard.GetState().IsKeyDown( Keys.LeftShift ) || Keyboard.GetState().IsKeyDown( Keys.RightShift ) ) {
					cameraPosition = new Vector3(cameraPosition.X, cameraPosition.Y - playerForwardWalkSpeed * playerRunMultiplier, cameraPosition.Z);
				}
				else
					cameraPosition = new Vector3(cameraPosition.X, cameraPosition.Y - playerForwardWalkSpeed, cameraPosition.Z);
			}
			else if ( Keyboard.GetState().IsKeyDown( Keys.S ) ) {
				cameraPosition = new Vector3(cameraPosition.X, cameraPosition.Y + playerBackwardWalkSpeed, cameraPosition.Z);
			}
			else if (Keyboard.GetState().IsKeyDown(Keys.A)) {
				cameraPosition = new Vector3(cameraPosition.X + playerSidewaysWalkSpeed, cameraPosition.Y, cameraPosition.Z);
			}
			else if (Keyboard.GetState().IsKeyDown(Keys.D)) {
				cameraPosition = new Vector3(cameraPosition.X - playerSidewaysWalkSpeed, cameraPosition.Y, cameraPosition.Z);
			}

			base.Update( gameTime );
		}


		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime) {
			GraphicsDevice.Clear(Color.DarkSlateGray);
			UpdateCamera(); //This needs to be done before drawing stuff

			//Draw stuff here
			DrawVertices( floorVertices, new BasicEffect[] { floorEffect } );
			DrawModel( spikeModel, new Vector3( -5.0f, 5.0f, 0.0f ), Vector3.Zero, new Vector3( 2.0f, 2.0f, 2.0f ));
			DrawModel( spikeModel, new Vector3( 5.0f, 0.0f, 10.0f ), new Vector3( 0.0f, (float)Math.PI, 0.0f), new Vector3( 2.0f, 2.0f, 2.0f ));
			//SHARON: I rotated the pitch by 180 degrees. Apparently the model is using y as the up vector, but the camera is using z as the up vector
			//(as per the tutorial we were following earlier I think)

			base.Draw( gameTime );
		}


		/*
		====================
		InitializeGround()

		  Create floor vertices, with positions and textures. Also set up the floor effect.
		====================
		*/
		private void InitializeGround() {
			floorVertices = new VertexPositionNormalTexture[ 6 ];
			floorVertices[ 0 ].Position = new Vector3( -20.0f, -20.0f, 0.0f );
			floorVertices[ 1 ].Position = new Vector3( -20.0f, 20.0f, 0.0f );
			floorVertices[ 2 ].Position = new Vector3( 20.0f, -20.0f, 0.0f );
			floorVertices[ 3 ].Position = floorVertices[ 1 ].Position;
			floorVertices[ 4 ].Position = new Vector3( 20.0f, 20.0f, 0.0f );
			floorVertices[ 5 ].Position = floorVertices[ 2 ].Position;

			floorVertices[ 0 ].Normal = new Vector3( 0.0f, 0.0f, 1.0f );
			floorVertices[ 1 ].Normal = floorVertices[0].Normal;
			floorVertices[ 2 ].Normal = floorVertices[0].Normal;
			floorVertices[ 3 ].Normal = floorVertices[0].Normal;
			floorVertices[ 4 ].Normal = floorVertices[0].Normal;
			floorVertices[ 5 ].Normal = floorVertices[0].Normal;

			floorVertices[ 0 ].TextureCoordinate = new Vector2( 0.0f, 0.0f );
			floorVertices[ 1 ].TextureCoordinate = new Vector2( 0.0f, 1.0f );
			floorVertices[ 2 ].TextureCoordinate = new Vector2( 1.0f, 0.0f );
			floorVertices[ 3 ].TextureCoordinate = floorVertices[ 1 ].TextureCoordinate;
			floorVertices[ 4 ].TextureCoordinate = new Vector2( 1.0f, 1.0f );
			floorVertices[ 5 ].TextureCoordinate = floorVertices[ 2 ].TextureCoordinate;

			floorEffect = new BasicEffect(graphics.GraphicsDevice);
			floorEffect.TextureEnabled = true;
			floorEffect.Texture = checkerboardTexture;
		}


		/*
		====================
		InitializeSpike()

		  Setup effects for spike model.
		====================
		*/
		private void InitializeSpike() {
			foreach (ModelMesh mesh in spikeModel.Meshes) {
				foreach (BasicEffect basicEffect in mesh.Effects) {
					basicEffect.TextureEnabled = true;
					basicEffect.DiffuseColor = new Vector3(1.0f, 0.0f, 0.0f);
					basicEffect.SpecularColor = new Vector3(1.0f, 0.5f, 0.5f);
					basicEffect.AmbientLightColor = new Vector3(0.1f, 0.1f, 0.1f);
				}
			}
		}


		/*
		====================
		DrawVertices(VertexPositionNormalTexture[] vertices, BasicEffect[] basicEffects, Vector3 position, Vector3 rotationYawPitchRoll, Vector3 scale ) {

		  Transform vertices and send to camera to draw.
		  position is coordinates
		  rotationYawPitchRoll is in radians, X=yaw, Y=pitch, Z=roll
		  scale is a factor, so 1.0 is unchanged
		====================
		*/
		private void DrawVertices(VertexPositionNormalTexture[] vertices, BasicEffect[] basicEffects, Vector3 position, Vector3 rotationYawPitchRoll, Vector3 scale ) {
			foreach (BasicEffect basicEffect in basicEffects) {
				basicEffect.World = Matrix.CreateTranslation(position);
				basicEffect.World = Matrix.CreateFromYawPitchRoll(rotationYawPitchRoll.X, rotationYawPitchRoll.Y, rotationYawPitchRoll.Z) * basicEffect.World;
				basicEffect.World = Matrix.CreateScale(scale) * basicEffect.World;
			}
			camera.DrawVertices(vertices, basicEffects);
		}


		/*
		====================
		DrawVertices( VertexPositionNormalTexture[] vertices, BasicEffect[] basicEffects )

		  Send to camera to draw
		  Just for consistency purposes; could call camera.DrawVertices( vertices, basicEffects ) directly
		====================
		*/
		private void DrawVertices( VertexPositionNormalTexture[] vertices, BasicEffect[] basicEffects ) {
			camera.DrawVertices(vertices, basicEffects);
		}



		/*
		====================
		DrawModel( Model model, Vector3 position, Vector3 rotationYawPitchRoll, Vector3 scale )

		  Transform model and send to camera to draw.
		  position is coordinates
		  rotationYawPitchRoll is in radians, X=yaw, Y=pitch, Z=roll
		  (depending on the model, what exactly corresponds with yaw/pitch/roll might require guessing and checking and may not be intuitive)
		  scale is a factor, so 1.0 is unchanged
		====================
		*/
		private void DrawModel( Model model, Vector3 position, Vector3 rotationYawPitchRoll, Vector3 scale ) {
			foreach (ModelMesh mesh in model.Meshes) {
				foreach (BasicEffect basicEffect in mesh.Effects) {
					basicEffect.World = Matrix.CreateTranslation( position );
					basicEffect.World = Matrix.CreateFromYawPitchRoll( rotationYawPitchRoll.X, rotationYawPitchRoll.Y, rotationYawPitchRoll.Z ) * basicEffect.World;
					basicEffect.World = Matrix.CreateScale( 2.0f ) * basicEffect.World;
				}
			}
			camera.DrawModel( model );
		}


		/*
		====================
		DrawModel( Model model )

		  Send to camera to draw
		  Just for consistency purposes; could call camera.DrawModel( model ) directly
		====================
		*/
		private void DrawModel(Model model ) {
			camera.DrawModel(model);
		}


		/*
		====================
		UpdateCamera()

		  Updates camera position based on mouse movement and passes information to the camera.
		  This needs to be done BEFORE drawing anything to camera.
		====================
		*/
		void UpdateCamera() {
			MouseState currentMouseState = Mouse.GetState();

			int moveX = currentMouseState.X - halfWidth;
			float newLookAngleX = lookAngleX + moveX * rotationSpeed;
			if (newLookAngleX >= -maxXRotation && newLookAngleX <= maxXRotation) {
				lookAngleX = newLookAngleX;
				lookAngleX = lookAngleX % (float)(2.0f * Math.PI);
			}

			int moveY = currentMouseState.Y - halfHeight;
			float newLookAngleY = lookAngleY + moveY * rotationSpeed;
			if (newLookAngleY >= -maxYRotation && newLookAngleY <= maxYRotation) {
				lookAngleY = newLookAngleY;
				lookAngleY = lookAngleY % (float)(2.0f * Math.PI);
			}

			Mouse.SetPosition(halfWidth, halfHeight);

			camera.Update(cameraPosition, cameraTarget, cameraUpVector, lookAngleX, lookAngleY);
		}
	}
}
