using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using RunJumpSwingMan.src.Framework;

namespace RunJumpSwingMan.src.Gameplay {
	/// <summary>
	/// The player. I don't really know what else to say
	/// </summary>
	class Player : Mob {

		private Vector3 _lookVector;
		//angle of looking around, in deg
		private Vector2 _lookAngle;

		public PlayerController Controller { get; set; }

		//The angle the vector is looking at
		public Vector2 LookAngle {
			get { return _lookAngle; }
			//automatically recalculate the lookVector
			set { _lookAngle = value;
				Matrix rotateHoriz = Matrix.CreateRotationY(_lookAngle.X);
				Matrix rotateVert = Matrix.CreateRotationX(_lookAngle.Y);
				_lookVector = Vector3.Transform(Vector3.Forward, rotateHoriz * rotateVert);
			}
		}
		public Vector3 LookVector {
			get { return _lookVector; }
			set { _lookVector = value; }
		}

		public Player() {

			//GravityScale = 0;

			Controller = new PlayerController(this);
			MaxMoveSpeed = 10;
			LookAngle = new Vector2();
		}

		public override void Update(GameTime time) {

			Controller.Update(time);
		}


	}
}
