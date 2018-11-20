using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RunJumpSwingMan {

	//References
	//https://gamedev.stackexchange.com/questions/93861/2d-camera-shaking-back-and-forth
	//https://github.com/prasadjay/Rotatable-Camera
	//http://rbwhitaker.wikidot.com/monogame-basic-effect
	class Camera {

		private GraphicsDeviceManager graphics;
		private Vector3[] lightDiffuseColor = new Vector3[3];
		private Vector3[] lightDirection = new Vector3[3];
		private Vector3[] lightSpecularColor = new Vector3[3];
		private bool[] directionalLightEnabled = { false, false, false };
		private Matrix firstLookAtMatrix = Matrix.Identity;
		private Matrix currentViewMatrix;
		private Matrix currentProjectionMatrix;
		private Viewport the2DViewport, the3DViewport;
		private SpriteBatch spriteBatch;

		public Camera( GraphicsDeviceManager gfx, SpriteBatch sb) {
			graphics = gfx;
			spriteBatch = sb;
			the3DViewport = graphics.GraphicsDevice.Viewport;
			the2DViewport = new Viewport();

			lightDiffuseColor[0] = new Vector3( 1.0f, 1.0f, 1.0f ); // a white light
			lightDirection[0] = new Vector3( 0.5f, 0.75f, -0.5f );  // some direction of light
			lightSpecularColor[0] = new Vector3( 1.0f, 1.0f, 1.0f ); // with white highlights
			directionalLightEnabled[0] = true;
		}

		/*
		====================
		Update( List<VertexPositionTexture[]> objects, Vector3 position, Vector3 target, Vector3 upVector )

		  Calculates currentViewMatrix and currentProjectionMatrix with the camera centered at the given position, with given lookAt and up vectors
		  To be called from the runner class.
		====================
		*/
		public void Update(Vector3 position, Vector3 target, Vector3 upVector, float lookAngleX, float lookAngleY) {
			//Get rotation matrices from mouse movement
			Matrix rotationY = Matrix.CreateRotationY(lookAngleX); //Moving along the 2D X axis rotates around the 3D Y axis
			Matrix rotationX = Matrix.CreateRotationX(lookAngleY); //Moving along the 2D Y axis rotates around the 3D X axis

			float aspectRatio = graphics.PreferredBackBufferWidth / (float)graphics.PreferredBackBufferHeight;
			float fov = Microsoft.Xna.Framework.MathHelper.PiOver4;
			float nearClipPlaneDistance = 1.0f;
			float farClipPlaneDistance = 200.0f;


			if (firstLookAtMatrix.Equals(Matrix.Identity)) {
				firstLookAtMatrix = Matrix.CreateLookAt(position, target, upVector);
			}
			//Attempt to correct camera flipping issue
			else {
				Console.Out.WriteLine(firstLookAtMatrix.Up + " " + Matrix.CreateLookAt(position, target, upVector).Up);
				if (firstLookAtMatrix.Up != Matrix.CreateLookAt(position, target, upVector).Up) {
					upVector = Matrix.Invert(firstLookAtMatrix).Up;
				}
			}
			//Uhh this isn't working
			//https://gamedev.stackexchange.com/questions/45280/making-a-camera-look-at-a-target-vector

			currentViewMatrix = Matrix.CreateLookAt(position, target, upVector) * rotationY * rotationX;
			currentProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(fov, aspectRatio, nearClipPlaneDistance, farClipPlaneDistance);
		}


		/*
		====================
		GetForwardVector()

		  Returns a vector in the direction the camera is pointed in.
		====================
		*/
		public Vector3 GetForwardVector() {
			return (currentViewMatrix * currentProjectionMatrix).Forward;
		}

		/*
		====================
		GetRightVector()

		  Returns a vector to the right of the camera, based on where it's directed.
		====================
		*/
		public Vector3 GetRightVector() {
			return (currentViewMatrix * currentProjectionMatrix).Right;
		}



		/*
		====================
		DrawModel( Model model )

		  Draws a 3D model to the screen using currentViewMatrix, currentViewMatrix, and lighting settings
		  So that all models are drawn correctly.
		  To be called from the runner class.
		====================
		*/
		public void DrawModel( Model model ) {
			//Draw model loaded from files
			foreach (ModelMesh mesh in model.Meshes) {
				//Effects for this model
				foreach (BasicEffect basicEffect in mesh.Effects) {
					basicEffect.View = currentViewMatrix;
					basicEffect.Projection = currentProjectionMatrix;
					
					basicEffect.LightingEnabled = true;
					basicEffect.DirectionalLight0.Enabled = directionalLightEnabled[0];
					basicEffect.DirectionalLight0.DiffuseColor = lightDiffuseColor[0];
					basicEffect.DirectionalLight0.Direction = lightDirection[0];
					basicEffect.DirectionalLight0.SpecularColor = lightSpecularColor[0];
					
					foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes) {
						pass.Apply();
					}
				}
				mesh.Draw();
			}	
		}


		/*
		====================
		DrawVertices( VertexPositionNormalTexture[] verticesObjects, BasicEffect[] basicEffects )

		  Draws a 3D model from vertices to the screen using currentViewMatrix, currentViewMatrix, and lighting settings
		  So that all models are drawn correctly.
		  To be called from the runner class.
		====================
		*/
		public void DrawVertices( VertexPositionNormalTexture[] verticesObjects, BasicEffect[] basicEffects ) {
			//Draw model made from vertices
			foreach (BasicEffect basicEffect in basicEffects) {
				basicEffect.View = currentViewMatrix;
				basicEffect.Projection = currentProjectionMatrix;

				//Couldn't figure out how to get lighting to work here
				/*
				basicEffect.LightingEnabled = true;
				basicEffect.DirectionalLight0.Enabled = directionalLightEnabled[0];
				basicEffect.DirectionalLight0.DiffuseColor = lightDiffuseColor[0];
				basicEffect.DirectionalLight0.Direction = lightDirection[0];
				basicEffect.DirectionalLight0.SpecularColor = lightSpecularColor[0];
				*/

				foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes) {
					pass.Apply();
				}
			}
			graphics.GraphicsDevice.DrawUserPrimitives<VertexPositionNormalTexture>(PrimitiveType.TriangleList, verticesObjects, 0, 2);
		}

		/*
		====================
		DrawImage(Texture2D image, Rectangle destinationRectangle)

		  Draws a 2D image at a given rectangle (which includes the location and size to fit it to) with given color
		  If color is white, the color doesn't change
		====================
		*/
		public void DrawImage(Texture2D image, Rectangle destinationRectangle, Color color) {
			spriteBatch.Begin();
			spriteBatch.Draw(image, destinationRectangle, color);
			spriteBatch.End();
		}
	}
}
