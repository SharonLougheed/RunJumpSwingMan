using Microsoft.Xna.Framework;

namespace RunJumpSwingMan.src {

	public class Camera {

		public Vector3 CameraOffset {
			get;
			set;
		}

		public Vector3 TargetOffset {
			get;
			set;
		}

		public Matrix ViewMatrix {
			get;
			set;
		}

		public Matrix ProjectionMatrix {
			get;
			set;
		}

		public float ViewAngle {
			get;
			set;
		}

		public float NearClipPlaneDistance {
			get;
			set;
		}

		public float FarClipPlaneDistance {
			get;
			set;
		}

		public Camera() {
			CameraOffset = new Vector3( 0.0f, 2.0f, 0.0f );
			TargetOffset = new Vector3( 0.0f, 2.0f, -1.0f );
			ViewMatrix = Matrix.Identity;
			ProjectionMatrix = Matrix.Identity;
			ViewAngle = MathHelper.PiOver4;
			NearClipPlaneDistance = 1.0f;
			FarClipPlaneDistance = 500.0f;
		}

		public void Update( Vector3 position, float yaw, float pitch, float roll, float aspectRatio ) {
			Matrix rotationMatrix = Matrix.CreateFromYawPitchRoll( yaw, pitch, roll );
			Vector3 transformedHeadOffset = Vector3.Transform( CameraOffset, rotationMatrix );
			Vector3 transformedReference = Vector3.Transform( TargetOffset, rotationMatrix );

			Vector3 cameraPosition = position + transformedHeadOffset;
			Vector3 cameraTarget = position + transformedReference;

			ViewMatrix = Matrix.CreateLookAt( cameraPosition, cameraTarget, Vector3.UnitY );
			ProjectionMatrix = Matrix.CreatePerspectiveFieldOfView( ViewAngle, aspectRatio, NearClipPlaneDistance, FarClipPlaneDistance );
		}

	}

}
