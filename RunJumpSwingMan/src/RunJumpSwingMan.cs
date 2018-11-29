using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RunJumpSwingMan.src.Framework;
using RunJumpSwingMan.src.Gameplay;

namespace RunJumpSwingMan.src {

	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class RunJumpSwingMan : Game {

		private GraphicsDeviceManager graphics;
		private SpriteBatch spriteBatch;

		private Camera camera;
		private float aspectRatio;
		private readonly float rotationSpeed = 0.01f;
		private readonly float maxXRotation = 2.0f * ( float )Math.PI; // radians
		private readonly float maxYRotation = 2.0f * ( float )Math.PI; //In radians (2pi for no limit)

		private Texture2D crosshairTexture;
		private Model spikeModel;

		private World world;
		private Player player;

		private Vector3 lightDiffuseColor;
		private Vector3 lightDirection;
		private Vector3 lightSpecularColor;

		public RunJumpSwingMan() {
			graphics = new GraphicsDeviceManager( this );

			Content.RootDirectory = "Content/RunJumpSwingMan/out/DesktopGL";
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize() {
			camera = new Camera();
			aspectRatio = graphics.GraphicsDevice.Viewport.Width / graphics.GraphicsDevice.Viewport.Height;

			Input.MouseLocked = true;
			Input.Initialize( graphics );

			world = new World();

			player = new Player {
				Position = Vector3.Zero
			};
			world.AddEntity( player );

			//Block block = new Block {
			//	Position = new Vector3( 0.0f, -10.0f, 0.0f )
			//};
			//world.AddEntity( block );

			Block block2 = new Block {
				Position = new Vector3( 0.0f, -5.0f, 0.0f ),
				Size = new Vector3( 100.0f, 1.0f, 100.0f )
			};
			world.AddEntity( block2 );

			float testingDistance = 5.0f;

			Block cube1 = new Block() {
				Position = testingDistance * Vector3.UnitZ,
				Size = Vector3.One
			};

			Block cube2 = new Block() {
				Position = -testingDistance * Vector3.UnitZ,
				Size = Vector3.One
			};

			Block cube3 = new Block() {
				Position = testingDistance * Vector3.UnitX,
				Size = Vector3.One
			};

			Block cube4 = new Block() {
				Position = -testingDistance * Vector3.UnitX,
				Size = Vector3.One
			};

			Block cube5 = new Block() {
				Position = testingDistance * Vector3.UnitY,
				Size = Vector3.One
			};

			Block cube6 = new Block() {
				Position = -testingDistance * Vector3.UnitY,
				Size = Vector3.One
			};

			world.AddEntity( cube1 );
			world.AddEntity( cube2 );
			world.AddEntity( cube3 );
			world.AddEntity( cube4 );
			world.AddEntity( cube5 );
			world.AddEntity( cube6 );

			world.ProcessEntityQueues();

			IsMouseVisible = false;

			lightDiffuseColor = new Vector3( 0.1f, 1.0f, 0.1f ); // a white light
			lightDirection = new Vector3( 0.1f, 0.1f, -0.5f );  // some direction of light
			lightSpecularColor = new Vector3( 1.0f, 1.0f, 1.0f ); // with white highlights

			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent() {
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch( GraphicsDevice );

			crosshairTexture = Content.Load<Texture2D>( "textures/crosshair" );
			spikeModel = Content.Load<Model>( "models/spike" );

			foreach ( Entity entity in world.Entities ) {
				entity.VertexBuffer = Shapes.IndexedVertexBufferCube( graphics, Color.SkyBlue );
				entity.IndexBuffer = Shapes.IndexBufferCube( graphics );
				entity.PrimitiveCount = Shapes.PrimitiveCountCube();
				entity.BasicEffect = new BasicEffect( graphics.GraphicsDevice );
			}

			InitContentOfSpike();
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

			//UpdateCameraAngles();
			Input.Update();
			world.Update( gameTime );

			base.Update( gameTime );
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw( GameTime gameTime ) {
			GraphicsDevice.Clear( Color.LightCoral );

			camera.Update( player.Position, player.LookAngle.X, player.LookAngle.Y, 0.0f, aspectRatio );

			RasterizerState rasterizerState = new RasterizerState {
				CullMode = CullMode.CullCounterClockwiseFace
				//CullMode = CullMode.None
			};
			graphics.GraphicsDevice.RasterizerState = rasterizerState;

			foreach ( Entity entity in world.Entities ) {
				entity.Draw( gameTime, graphics, camera, lightDiffuseColor, lightDirection, lightSpecularColor );
			}

			//Draw stuff here
			//DrawVertices( floorVertices, new BasicEffect[] { floorEffect } );
			//DrawModel( spikeModel, new Vector3( -5.0f, 10.0f, 0.0f ), new Vector3( ( float )Math.PI / 2, ( float )Math.PI / 2, 0.0f ), new Vector3( 2.0f, 2.0f, 2.0f ) );
			//DrawModel( spikeModel, new Vector3( 5.0f, 0.0f, -10.0f ), new Vector3( ( float )Math.PI / 2, -( float )Math.PI / 2, 0.0f ), new Vector3( 2.0f, 2.0f, -2.0f ) );

			int halfWidth = graphics.PreferredBackBufferWidth / 2;
			int halfHeight = graphics.PreferredBackBufferHeight / 2;
			DrawImage( crosshairTexture, new Rectangle( halfWidth - 8, halfHeight - 8, 17, 17 ), Color.White );

			base.Draw( gameTime );
		}

		/*
		====================
		DrawModel( Model model )

		  Draws a 3D model to the screen using ViewMatrix, ViewMatrix, and lighting settings
		  So that all models are drawn correctly.
		  To be called from the runner class.
		====================
		*/
		public void DrawModel( Model model ) {
			//Draw model loaded from files
			foreach ( ModelMesh mesh in model.Meshes ) {
				//Effects for this model
				foreach ( BasicEffect basicEffect in mesh.Effects ) {
					basicEffect.View = camera.ViewMatrix;
					basicEffect.Projection = camera.ProjectionMatrix;

					basicEffect.LightingEnabled = true;
					basicEffect.DirectionalLight0.Enabled = true;
					basicEffect.DirectionalLight0.DiffuseColor = lightDiffuseColor;
					basicEffect.DirectionalLight0.Direction = lightDirection;
					basicEffect.DirectionalLight0.SpecularColor = lightSpecularColor;

					foreach ( EffectPass pass in basicEffect.CurrentTechnique.Passes ) {
						pass.Apply();
					}
				}
				mesh.Draw();
			}
		}

		/*
		====================
		DrawImage(Texture2D image, Rectangle destinationRectangle)

		  Draws a 2D image at a given rectangle (which includes the location and size to fit it to) with given color
		  If color is white, the color doesn't change
		====================
		*/
		public void DrawImage( Texture2D image, Rectangle destinationRectangle, Color color ) {
			spriteBatch.Begin();
			spriteBatch.Draw( image, destinationRectangle, color );
			spriteBatch.End();
		}

		/*
		====================
		InitializeSpike()

		  Setup effects for spike model.
		====================
		*/
		private void InitContentOfSpike() {
			foreach ( ModelMesh mesh in spikeModel.Meshes ) {
				foreach ( BasicEffect basicEffect in mesh.Effects ) {
					basicEffect.TextureEnabled = true;
					basicEffect.DiffuseColor = new Vector3( 1.0f, 0.0f, 0.0f );
					basicEffect.SpecularColor = new Vector3( 1.0f, 0.5f, 0.5f );
					basicEffect.AmbientLightColor = new Vector3( 0.1f, 0.1f, 0.1f );
				}
			}
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
			foreach ( ModelMesh mesh in model.Meshes ) {
				foreach ( BasicEffect basicEffect in mesh.Effects ) {
					basicEffect.World = Matrix.CreateTranslation( position );
					basicEffect.World = Matrix.CreateFromYawPitchRoll( rotationYawPitchRoll.X, rotationYawPitchRoll.Y, rotationYawPitchRoll.Z ) * basicEffect.World;
					basicEffect.World = Matrix.CreateScale( 2.0f ) * basicEffect.World;
				}
			}

			DrawModel( model );
		}

		///*
		//====================
		//UpdateCamera()

		//  Updates camera position based on mouse movement and passes information to the camera.
		//  This needs to be done BEFORE drawing anything to camera.
		//====================
		//*/
		//private void UpdateCameraAngles() {
		//	MouseState currentMouseState = Mouse.GetState();
		//	int halfWidth = graphics.PreferredBackBufferWidth / 2;
		//	int halfHeight = graphics.PreferredBackBufferHeight / 2;

		//	int moveX = currentMouseState.X - halfWidth;
		//	float newCameraYaw = camera.Yaw - moveX * rotationSpeed;
		//	if ( maxXRotation >= MathHelper.TwoPi || ( newCameraYaw >= -maxXRotation && newCameraYaw <= maxXRotation ) ) {
		//		camera.Yaw = newCameraYaw;
		//		camera.Yaw = camera.Yaw % MathHelper.TwoPi;
		//	}

		//	int moveY = currentMouseState.Y - halfHeight;
		//	float newCameraPitch = camera.Pitch - moveY * rotationSpeed;
		//	if ( maxYRotation >= MathHelper.TwoPi || ( newCameraPitch >= -maxYRotation && newCameraPitch <= maxYRotation ) ) {
		//		camera.Pitch = newCameraPitch;
		//		camera.Pitch = camera.Pitch % MathHelper.TwoPi;
		//	}
		//}

	}

}
