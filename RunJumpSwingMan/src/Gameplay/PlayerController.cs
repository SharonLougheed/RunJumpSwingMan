using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RunJumpSwingMan.src.Framework;

namespace RunJumpSwingMan.src.Gameplay {

	public class PlayerController {

		private float maxSwingLength;
		private Vector3? swingPoint;

		public Player Subject {
			get;
			internal set;
		}

		public float MouseSensitivity {
			get;
			set;
		}

		public PlayerController( Player sub ) {
			maxSwingLength = 50;
			swingPoint = null;
			MouseSensitivity = 10f;
			Subject = sub;
		}

		public void Update( GameTime time ) {
			TurnPlayer( time );
			MovePlayer( time );

			KeyboardState kb = Input.CurrentKeyboard;

			//checking for jump mechanics
			if (kb.IsKeyDown(Keys.Space) && Subject.Grounded) {
				Subject.Velocity = new Vector3(Subject.Velocity.X, Subject.JumpSpeed, Subject.Velocity.Z);
			}

			SwingControl();

			if (!swingPoint.HasValue && Subject.Grounded) {
				Subject.Velocity = new Vector3(0, Subject.Velocity.Y, 0);
			}
		}

		/// <summary>
		/// Does swingy stuff
		/// </summary>
		public void SwingControl() {
			if (Input.CurrentMouse.LeftButton == ButtonState.Pressed) {
				if (Input.OldMouse.LeftButton == ButtonState.Released) {
					//raycast at where the player is looking
					Ray hookRay = new Ray(Subject.Position, Subject.LookVector);
					Tuple<Entity, Vector3, float> castresult = Subject.Parent.Raycast(hookRay, maxSwingLength, Subject);
					
					if (castresult != null) {
						swingPoint = castresult.Item2;
						/*
						Block hitBlock = new Block();
						hitBlock.Size = new Vector3(1, 1, 1);
						hitBlock.Position = castresult.Item2;
						Subject.Parent.AddEntity(hitBlock);
						*/
					}
				}
				Console.WriteLine("SwingP: " + swingPoint + "\nPlayerP: " + Subject.Position);
			} else {
				swingPoint = null;
			}

			RecalculateSwinging();
		}

		/// <summary>
		/// Updates Subject Position and Velocity due to Swinging
		/// </summary>
		public void RecalculateSwinging() {
			if (swingPoint.HasValue) {
				Vector3 sPoint = swingPoint.Value;
				Vector3 dif = Subject.Position - sPoint;
				//if the distance between the player and the swingpoint is longer than the 
				if (dif.LengthSquared() > Math.Pow(maxSwingLength, 2)) {
					
					dif.Normalize();
					//reposition player
					Subject.Position = sPoint + dif * maxSwingLength;	
					Subject.Velocity = Geometry.PerpendicularProjection(Subject.Velocity, dif);
				}
			}
		}

		/// <summary>
		/// Moves the Player based on keyboard input
		/// </summary>
		/// <param name="time">The current time of the game</param>
		public void MovePlayer( GameTime time ) {
			//get keyboard movement input
			Vector2 wasdInput = Input.GetWASDVector( Input.CurrentKeyboard );

			//determine the local direction to move in
			Vector2 lateralMovement = wasdInput * Subject.MaxMoveSpeed;
			Vector3 move3D = new Vector3( lateralMovement.X, 0, -lateralMovement.Y );
			//set the movement vector to the local movement vector rotated by the horizontal lookAngle
			Vector3 moveTransformed = Vector3.Transform( move3D, Matrix.CreateRotationY( MathHelper.ToRadians( Subject.LookAngle.X ) ) );
			Subject.Movement = moveTransformed;
		}

		/// <summary>
		/// Turns the Player based on mouse input
		/// </summary>
		/// <param name="time"></param>
		public void TurnPlayer( GameTime time ) {
			//incrementing the look angle by mouse movement, with an inverted y
			Vector2 lookDisplace = Input.MouseDelta * new Vector2( -1, -1 ) * ( float )time.ElapsedGameTime.TotalSeconds * MouseSensitivity;
			Vector2 sum = Subject.LookAngle + lookDisplace;
			Subject.LookAngle = new Vector2( sum.X, MathHelper.Clamp( sum.Y, -90f, 90f ) );
		}

	}

}
