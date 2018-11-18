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
		private float maxXRotation = (float)Math.PI, maxYRotation = (float)Math.PI; //In radians (2pi for no limit)
		private int halfWidth, halfHeight;

		//For floor
		private Texture2D checkerboardTexture;

		//Objects to render
		private List<VertexPositionTexture[]> verticesObjects; //Objects to render, made from an array of vertices
		private List<BasicEffect[]> basicEffectsForVerticesObjects; //The basic effects that are paired with verticesObjects (by index) 
			//Implemented as an array in case you want to layer basic effects
		private List<Model> modelObjects; //Objects to render, loaded from model files
		private Model spikeModel;

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
			verticesObjects = new List<VertexPositionTexture[]>();
			basicEffectsForVerticesObjects = new List<BasicEffect[]>();
			modelObjects = new List<Model>();

			CreateFloor();

			CreateSpike( new Vector3 ( -20f, 20f, 0f ) , 2.0f );

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
		CreateFloor()

		  Create floor vertices, with positions and textures. Also set up the floor effect.
		  Add both to the lists verticesObjects and basicEffectsForVerticesObjects
		====================
		*/
		private void CreateFloor() {
			VertexPositionTexture[] floorVertices = new VertexPositionTexture[ 6 ];
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

			BasicEffect floorEffect = new BasicEffect(graphics.GraphicsDevice);
			floorEffect.TextureEnabled = true;
			floorEffect.Texture = checkerboardTexture;

			verticesObjects.Add( floorVertices );
			basicEffectsForVerticesObjects.Add( new BasicEffect[] { floorEffect } );
		}

		/*
		====================
		CreateSpike()

		  Setup spike location and effects and add to modelObjects.
		====================
		*/
		private void CreateSpike( Vector3 position, float scale ) {
			Matrix[] modelTransforms = new Matrix[spikeModel.Bones.Count];
			spikeModel.CopyAbsoluteBoneTransformsTo(modelTransforms);

			foreach (ModelMesh mesh in spikeModel.Meshes) {
				foreach (BasicEffect basicEffect in mesh.Effects) {
					basicEffect.TextureEnabled = true;
					basicEffect.DiffuseColor = new Vector3( 1.0f, 0.0f, 0.0f );
					basicEffect.SpecularColor = new Vector3( 1.0f, 0.5f, 0.5f );
					basicEffect.AmbientLightColor = new Vector3( 0.1f, 0.1f, 0.1f );

					//modelTransforms[mesh.ParentBone.Index].Translation = modelTransforms[mesh.ParentBone.Index].Translation + position;
					//basicEffect.World = modelTransforms[mesh.ParentBone.Index] * basicEffect.World;
					basicEffect.World = Matrix.CreateScale(scale) * basicEffect.World;
				}
			}

			modelObjects.Add(spikeModel);
		}

		/*
		====================
		UpdateCamera()

		  Updates camera position based on mouse movement and passes information to the camera.
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

			camera.Update(modelObjects, verticesObjects, basicEffectsForVerticesObjects, cameraPosition, cameraTarget, cameraUpVector, lookAngleX, lookAngleY);
		}
	}
}
