using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RunJumpSwingMan.src.Framework;

namespace RunJumpSwingMan.src.Gameplay {

	public class PlayerController {

		private Player subject;
		private readonly Camera camera;

		public float MouseSensitivity {
			get;
			set;
		}

		public PlayerController( Player sub ) {
			subject = sub;
			MouseSensitivity = 10f;
		}

		public void Update( GameTime time ) {
			TurnPlayer( time );
			MovePlayer( time );
		}

		/// <summary>
		/// Moves the Player based on keyboard input
		/// </summary>
		/// <param name="time"></param>
		public void MovePlayer( GameTime time ) {
			KeyboardState kstate = Keyboard.GetState();

			//get keyboard movement input
			Vector2 wasdInput = Input.GetWASDVector( kstate );

			//determine the local direction to move in
			Vector2 lateralMovement = wasdInput * subject.MaxMoveSpeed;
			Vector3 move3D = new Vector3( lateralMovement.X, 0, -lateralMovement.Y );
			//set the movement vector to the local movement vector rotated by the horizontal lookAngle
			Vector3 moveTransformed = Vector3.Transform( move3D, Matrix.CreateRotationY( MathHelper.ToRadians( subject.LookAngle.X ) ) );
			subject.Movement = moveTransformed;
		}

		/// <summary>
		/// Turns the Player based on mouse input
		/// </summary>
		/// <param name="time"></param>
		public void TurnPlayer( GameTime time ) {
			//incrementing the look angle by mouse movement, with an inverted y
			Vector2 lookDisplace = Input.MouseDelta * new Vector2( -1, -1 ) * ( float )time.ElapsedGameTime.TotalSeconds * MouseSensitivity;
			Vector2 sum = subject.LookAngle + lookDisplace;
			subject.LookAngle = new Vector2( sum.X, MathHelper.Clamp( sum.Y, -90f, 90f ) );
		}

	}

}
