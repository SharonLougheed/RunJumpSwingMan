using System.IO;
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

		private VertexPositionTexture[] floorVertices;
		private BasicEffect floorEffect;

		private Texture2D checkerboardTexture;

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
			floorVertices = new VertexPositionTexture[ 6 ];
			floorVertices[ 0 ].Position = new Vector3( -20.0f, -20.0f, 0.0f );
			floorVertices[ 1 ].Position = new Vector3( -20.0f, 20.0f, 0.0f );
			floorVertices[ 2 ].Position = new Vector3( 20.0f, -20.0f, 0.0f );
			floorVertices[ 3 ].Position = floorVertices[ 1 ].Position;
			floorVertices[ 4 ].Position = new Vector3( 20.0f, 20.0f, 0.0f );
			floorVertices[ 5 ].Position = floorVertices[ 2 ].Position;

			floorVertices[ 0 ].TextureCoordinate = new Vector2( 0.0f, 0.0f );
			floorVertices[ 1 ].TextureCoordinate = new Vector2( 0.0f, 1.0f );
			floorVertices[ 2 ].TextureCoordinate = new Vector2( 1.0f, 0.0f );

			floorVertices[ 3 ].TextureCoordinate = floorVertices[ 1 ].TextureCoordinate;
			floorVertices[ 4 ].TextureCoordinate = new Vector2( 1.0f, 1.0f );
			floorVertices[ 5 ].TextureCoordinate = floorVertices[ 2 ].TextureCoordinate;

			floorEffect = new BasicEffect( graphics.GraphicsDevice );

			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent() {
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch( GraphicsDevice );

			checkerboardTexture = Content.Load<Texture2D>( "RunJumpSwingMan/out/DesktopGL/textures/checkerboard" );
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
		protected override void Draw( GameTime gameTime ) {
			GraphicsDevice.Clear( Color.CornflowerBlue );

			DrawGround();

			base.Draw( gameTime );
		}

		private void DrawGround() {
			Vector3 cameraPosition = new Vector3( 0.0f, 40.0f, 20.0f );
			Vector3 cameraLookAtVector = Vector3.Zero;
			Vector3 cameraUpVector = Vector3.UnitZ;

			floorEffect.View = Matrix.CreateLookAt( cameraPosition, cameraLookAtVector, cameraUpVector );

			float aspectRatio = graphics.PreferredBackBufferWidth / ( float )graphics.PreferredBackBufferHeight;
			float fov = Microsoft.Xna.Framework.MathHelper.PiOver4;
			float nearClipPlaneDistance = 1.0f;
			float farClipPlaneDistance = 200.0f;

			floorEffect.Projection = Matrix.CreatePerspectiveFieldOfView( fov, aspectRatio, nearClipPlaneDistance, farClipPlaneDistance );

			floorEffect.TextureEnabled = true;
			floorEffect.Texture = checkerboardTexture;

			foreach ( EffectPass pass in floorEffect.CurrentTechnique.Passes ) {
				pass.Apply();

				graphics.GraphicsDevice.DrawUserPrimitives<VertexPositionTexture>( PrimitiveType.TriangleList, floorVertices, 0, 2 );
			}
		}

	}

}
