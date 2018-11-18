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
		private bool[] directionalLightEnabled = new bool[3];

		public Camera( GraphicsDeviceManager gfx ) {
			graphics = gfx;
			lightDiffuseColor[0] = new Vector3( 1.0f, 1.0f, 1.0f ); // a white light
			lightDirection[0] = new Vector3( 0f, 1.0f, 0f );  // 45 degrees
			lightSpecularColor[0] = new Vector3( 1.0f, 1.0f, 1.0f ); // with white highlights
			directionalLightEnabled[0] = true;
		}

		/*
		====================
		Update( List<VertexPositionTexture[]> objects, Vector3 position, Vector3 lookAtVector, Vector3 upVector)

		  Draws a list of 3D objects (each a list of vertex positions) to the screen,
		  with the camera centered at a given position, with given lookAt and up vectors
		  To be called from the runner class.
		====================
		*/
		public void Update(List<Model> models, List<VertexPositionTexture[]> verticesObjects, List<BasicEffect[]> basicEffectsforVerticesObjects,
			Vector3 position, Vector3 target, Vector3 upVector, float lookAngleX, float lookAngleY) {


			//X and Y are flipped
			Matrix rotationY = Matrix.CreateRotationY(lookAngleX);
			Matrix rotationX = Matrix.CreateRotationX(lookAngleY);

			float aspectRatio = graphics.PreferredBackBufferWidth / (float)graphics.PreferredBackBufferHeight;
			float fov = Microsoft.Xna.Framework.MathHelper.PiOver4;
			float nearClipPlaneDistance = 1.0f;
			float farClipPlaneDistance = 200.0f;

			for (int i = 0; i < verticesObjects.Count; i++) {
				VertexPositionTexture[] obj = verticesObjects[i];
				BasicEffect[] basicEffects = basicEffectsforVerticesObjects[i];
				
				foreach (BasicEffect basicEffect in basicEffects) {
					basicEffect.View = Matrix.CreateLookAt(position, target, upVector) * rotationY * rotationX;
					basicEffect.Projection = Matrix.CreatePerspectiveFieldOfView(fov, aspectRatio, nearClipPlaneDistance, farClipPlaneDistance);

					//basicEffect.LightingEnabled = true;
					//basicEffect.DirectionalLight0.Enabled = directionalLightEnabled[0];
					basicEffect.DirectionalLight0.DiffuseColor = lightDiffuseColor[0];
					basicEffect.DirectionalLight0.Direction = lightDirection[0];
					basicEffect.DirectionalLight0.SpecularColor = lightSpecularColor[0];

					foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes) {
						pass.Apply();
					}
				}

				graphics.GraphicsDevice.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleList, obj, 0, 2);
			}
			
			foreach (Model model in models) {
				foreach (ModelMesh mesh in model.Meshes) {

					foreach (BasicEffect basicEffect in mesh.Effects) {
						basicEffect.View = Matrix.CreateLookAt(position, target, upVector) * rotationY * rotationX;
						basicEffect.Projection = Matrix.CreatePerspectiveFieldOfView(fov, aspectRatio, nearClipPlaneDistance, farClipPlaneDistance);

						//basicEffect.LightingEnabled = true;
						//basicEffect.DirectionalLight0.Enabled = directionalLightEnabled[0];
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
			
		}
	}
}
