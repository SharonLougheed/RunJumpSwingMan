using Microsoft.Xna.Framework;

namespace RunJumpSwingMan.src {

	public class Camera {

		private const float MAX_PITCH = MathHelper.PiOver2 - 0.00872665f;
		private const float MIN_PITCH = -MathHelper.PiOver2 + 0.00872665f;

		private float yaw;
		private float pitch;
		private float roll;

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

		public float Yaw {
			get {
				return yaw;
			}
			set {
				yaw = value;
			}
		}

		public float Pitch {
			get => pitch;
			set {
				pitch = MathHelper.Max( MIN_PITCH, MathHelper.Min( MAX_PITCH, value ) );
			}
		}

		public float Roll {
			get {
				return roll;
			}
			set {
				roll = value;
			}
		}

		public Camera() {
			CameraOffset = new Vector3( 0.0f, 2.0f, 0.0f );
			TargetOffset = new Vector3( 0.0f, 2.0f, -1.0f );
			ViewMatrix = Matrix.Identity;
			ProjectionMatrix = Matrix.Identity;
			ViewAngle = MathHelper.PiOver4;
			NearClipPlaneDistance = 1.0f;
			FarClipPlaneDistance = 500.0f;
			Yaw = 0.0f;
			Pitch = 0.0f;
			Roll = 0.0f;
		}

		public void Update( Vector3 position, float aspectRatio ) {
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
