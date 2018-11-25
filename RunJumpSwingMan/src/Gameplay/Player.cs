using Microsoft.Xna.Framework;
using RunJumpSwingMan.src.Framework;

namespace RunJumpSwingMan.src.Gameplay {

	/// <summary>
	/// The player. I don't really know what else to say
	/// </summary>
	public class Player : Mob {

		private Vector3 _lookVector;
		//angle of looking around, in deg
		private Vector2 _lookAngle;

		public PlayerController Controller {
			get;
			set;
		}

		//The angle the vector is looking at
		public Vector2 LookAngle {
			get => _lookAngle;
			//automatically recalculate the lookVector
			set {
				_lookAngle = value;
				Matrix rotateHoriz = Matrix.CreateRotationY( MathHelper.ToRadians( _lookAngle.X ) );
				Matrix rotateVert = Matrix.CreateRotationX( MathHelper.ToRadians( _lookAngle.Y ) );
				_lookVector = Vector3.Transform( Vector3.Forward, rotateVert * rotateHoriz );
			}
		}

		public Vector3 LookVector => _lookVector;

		public Player() : base() {
			//GravityScale = 0;
			Size = new Vector3( 2, 4, 2 );
			Controller = new PlayerController( this );
			MaxMoveSpeed = 10;
			LookAngle = new Vector2();
		}

		public override void Update( GameTime time ) {
			//Console.WriteLine(_lookAngle + " " + _lookVector);
			Controller.Update( time );
		}

	}

}
