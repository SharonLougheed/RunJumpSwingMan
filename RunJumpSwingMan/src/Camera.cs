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
		private BasicEffect basicEffect;
		float speed = 0.5F;

		public Camera( GraphicsDeviceManager gfx ) {
			graphics = gfx;
			basicEffect = new BasicEffect( graphics.GraphicsDevice );
		}

		private void DrawModel(Model model, Matrix world, Matrix view, Matrix projection)
		{
			foreach (ModelMesh mesh in model.Meshes)
			{
				foreach (BasicEffect effect in mesh.Effects)
				{
					effect.World = world;
					effect.View = view;
					effect.Projection = projection;
				}
				mesh.Draw();
			}
		}

		/*
		====================
		Update( List<VertexPositionTexture[]> objects, Vector3 position, Vector3 lookAtVector, Vector3 upVector)

		  Draws a list of 3D objects (each a list of vertex positions) to the screen,
		  with the camera centered at a given position, with given lookAt and up vectors
		  To be called from the runner class.
		====================
		*/
		public void Update( List<VertexPositionTexture[]> objects, Vector3 position, Vector3 lookAtVector, Vector3 upVector) {
			
			basicEffect.View = Matrix.CreateLookAt( position, lookAtVector, upVector );

			float aspectRatio = graphics.PreferredBackBufferWidth / (float)graphics.PreferredBackBufferHeight;
			float fov = Microsoft.Xna.Framework.MathHelper.PiOver4;
			float nearClipPlaneDistance = 1.0f;
			float farClipPlaneDistance = 200.0f;

			basicEffect.Projection = Matrix.CreatePerspectiveFieldOfView( fov, aspectRatio, nearClipPlaneDistance, farClipPlaneDistance );

			foreach (VertexPositionTexture[] obj in objects ) {
				foreach( EffectPass pass in basicEffect.CurrentTechnique.Passes ) {
					pass.Apply();
					graphics.GraphicsDevice.DrawUserPrimitives<VertexPositionTexture>( PrimitiveType.TriangleList, obj, 0, 2 );
				}
			}
		}
	}
}
