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

		private void DrawModel(Model model, Matrix world, Matrix view, Matrix projection) {
			foreach (ModelMesh mesh in model.Meshes) {
				foreach (BasicEffect effect in mesh.Effects) {
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
		public void Update( List<VertexPositionTexture[]> objects, Vector3 position, Vector3 target, Vector3 upVector, float lookAngleX, float lookAngleY) {

			//X and Y are flipped
			Matrix rotationY = Matrix.CreateRotationY(lookAngleX);
			Matrix rotationX = Matrix.CreateRotationX(lookAngleY);

			basicEffect.View = Matrix.CreateLookAt( position, target, upVector ) * rotationY * rotationX;

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

		
        //https://gamedev.stackexchange.com/questions/15070/orienting-a-model-to-face-a-target#15078
        public static Quaternion GetRotation(Vector3 source, Vector3 dest, Vector3 up)
        {
            float dot = Vector3.Dot(source, dest);

            if (Math.Abs(dot - (-1.0f)) < 0.000001f)
            {
                // vector a and b point exactly in the opposite direction, 
                // so it is a 180 degrees turn around the up-axis
                return new Quaternion(up, MathHelper.ToRadians(180.0f));
            }
            if (Math.Abs(dot - (1.0f)) < 0.000001f)
            {
                // vector a and b point exactly in the same direction
                // so we return the identity quaternion
                return Quaternion.Identity;
            }

            float rotAngle = (float)Math.Acos(dot);
            Vector3 rotAxis = Vector3.Cross(source, dest);
            rotAxis = Vector3.Normalize(rotAxis);
            return Quaternion.CreateFromAxisAngle(rotAxis, rotAngle);
        }

		/*
		 * 		/// <summary>
		/// Allows the game component to update itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public void Update(GameTime gameTime)
		{
			// TODO: Add your update code here


			// Move forward and backward

			if (Keyboard.GetState().IsKeyDown(Keys.W))
				cameraPosition += cameraDirection * speed;
			if (Keyboard.GetState().IsKeyDown(Keys.S))
				cameraPosition -= cameraDirection * speed;

			if (Keyboard.GetState().IsKeyDown(Keys.A))
				cameraPosition += Vector3.Cross(cameraUp, cameraDirection) * speed;
			if (Keyboard.GetState().IsKeyDown(Keys.D))
				cameraPosition -= Vector3.Cross(cameraUp, cameraDirection) * speed;



			// Rotation in the world
			cameraDirection = Vector3.Transform(cameraDirection, Matrix.CreateFromAxisAngle(cameraUp, (-MathHelper.PiOver4 / 150) * (Mouse.GetState().X - prevMouseState.X)));


			cameraDirection = Vector3.Transform(cameraDirection, Matrix.CreateFromAxisAngle(Vector3.Cross(cameraUp, cameraDirection), (MathHelper.PiOver4 / 100) * (Mouse.GetState().Y - prevMouseState.Y)));
			cameraUp = Vector3.Transform(cameraUp, Matrix.CreateFromAxisAngle(Vector3.Cross(cameraUp, cameraDirection), (MathHelper.PiOver4 / 100) * (Mouse.GetState().Y - prevMouseState.Y)));

			// Reset prevMouseState
			prevMouseState = Mouse.GetState();

			CreateLookAt();

			base.Update(gameTime);
		}

		private void CreateLookAt()
		{
			view = Matrix.CreateLookAt(cameraPosition, cameraPosition + cameraDirection, cameraUp);
		}
		*/
	}
}
