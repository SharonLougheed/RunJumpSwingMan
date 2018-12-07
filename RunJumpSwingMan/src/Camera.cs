using Microsoft.Xna.Framework;

namespace RunJumpSwingMan.src {

	public class Camera {

		private const float MAX_PITCH = MathHelper.PiOver2 - 0.00872665f;
		private const float MIN_PITCH = -MathHelper.PiOver2 + 0.00872665f;

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
			CameraOffset = new Vector3( 0.0f, 0.0f, 0.0f );
			TargetOffset = new Vector3( 0.0f, 0.0f, -1.0f );
			ViewMatrix = Matrix.Identity;
			ProjectionMatrix = Matrix.Identity;
			ViewAngle = MathHelper.PiOver4;
			NearClipPlaneDistance = 0.1f;
			FarClipPlaneDistance = 500.0f;
		}

		public void Update( Vector3 position, float yaw, float pitch, float roll, float aspectRatio ) {
			yaw = MathHelper.ToRadians( yaw );
			pitch = MathHelper.Clamp( MathHelper.ToRadians( pitch ), MIN_PITCH, MAX_PITCH );
			roll = MathHelper.ToRadians( roll );

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
