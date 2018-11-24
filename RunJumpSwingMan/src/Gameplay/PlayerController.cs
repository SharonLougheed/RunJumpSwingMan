using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using RunJumpSwingMan.src.Framework;

namespace RunJumpSwingMan.src.Gameplay {
	class PlayerController {

		Player subject;

		Camera camera;

		public float MouseSensitivity { get; set; }

		public PlayerController(Player sub) {
			subject = sub;
			MouseSensitivity = 2;
		}

		public void Update(GameTime time) {
			TurnPlayer(time);
			MovePlayer(time);


		}

		/// <summary>
		/// Moves the Player based on keyboard input
		/// </summary>
		/// <param name="time"></param>
		public void MovePlayer(GameTime time) {
			KeyboardState kstate = Keyboard.GetState();

			//get keyboard movement input
			Vector2 wasdInput = Input.GetWASDVector(kstate);
			//determine the local direction to move in
			Vector2 lateralMovement = wasdInput * subject.MaxMoveSpeed * (float)time.ElapsedGameTime.TotalSeconds;
			Matrix rotation = Matrix.CreateRotationY(MathHelper.ToRadians(subject.LookAngle.X));

			//set the movement vector to the local movement vector rotated by the horizontal lookAngle
			subject.Movement = Vector3.Transform(new Vector3(lateralMovement.X, 0, -lateralMovement.Y), rotation);
		}

		/// <summary>
		/// Turns the Player based on mouse input
		/// </summary>
		/// <param name="time"></param>
		public void TurnPlayer(GameTime time) {
			subject.LookAngle += Input.MouseDelta * (float)time.ElapsedGameTime.TotalSeconds;

		}

	}
}
