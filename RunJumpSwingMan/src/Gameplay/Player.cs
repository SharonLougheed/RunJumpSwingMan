using Microsoft.Xna.Framework;
using RunJumpSwingMan.src.Framework;

namespace RunJumpSwingMan.src.Gameplay {

	/// <summary>
	/// The player. I don't really know what else to say
	/// </summary>
	public class Player : Mob {

		private Vector3 _lookVector;
		private PlayerController _controller;
		//angle of looking around, in deg
		private Vector2 _lookAngle;

		public float JumpSpeed {
			get;
			set;
		}

		public PlayerController Controller {
			get => _controller;
			set {
				if ( _controller != null ) {
					_controller.Subject = null;
				}

				_controller = value;
				value.Subject = this;
			}
		}

		///The angle the vector is looking at
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
			Size = new Vector3( 0.5f, 1.0f, 0.5f );
			Controller = new PlayerController( this );
			MaxMoveSpeed = 10;
			JumpSpeed = 8;
			LookAngle = new Vector2();
		}

		public override void Update( GameTime time ) {
			base.Update( time );
			Controller.Update( time );
		}

	}

}
