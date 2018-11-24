using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace RunJumpSwingMan.src.Framework {


	/// <summary>
	/// A class for convenient input calculations
	/// 
	/// NOTE: To install into the game, call to initialize in the game init and call to update every update frame of the game
	/// </summary>
	public static class Input {

		private static GraphicsDeviceManager _graphicsMan;

		public static MouseState CurrentMouse { get; private set; }
		public static MouseState OldMouse { get; private set; }

		public static Vector2 MouseDelta { get; private set; }

		public static bool MouseLocked { get; set; }
		
		public static void Initialize(GraphicsDeviceManager devman) {
			_graphicsMan = devman;
			CurrentMouse = Mouse.GetState();
			OldMouse = Mouse.GetState();
		}

		public static void Update() {
			//updating old values
			OldMouse = CurrentMouse;
			CurrentMouse = Mouse.GetState();

			//calculating mouseDelta
			MouseDelta = CalculateMouseDelta();
			//if mouse is locked then reset position after calculating the delta
			if (MouseLocked) {
				Viewport port = _graphicsMan.GraphicsDevice.Viewport;
				Mouse.SetPosition(port.Width/2, port.Height/2);
			}

		}

		//calculates the amount of mouse movement since the last update call
		private static Vector2 CalculateMouseDelta() {
			Vector2 toReturn = new Vector2();

			//if it's mouselocked then just take the distance from the center of the screen
			if (MouseLocked) {
				Viewport port = _graphicsMan.GraphicsDevice.Viewport;
				Vector2 screenMid = new Vector2(port.Width, port.Height) / 2;
				toReturn = new Vector2(CurrentMouse.X, CurrentMouse.Y) - screenMid;
			} else {
				toReturn = new Vector2(CurrentMouse.X, CurrentMouse.Y) - new Vector2(OldMouse.X, OldMouse.Y);
			}
			return toReturn;
		}

		/// <summary>
		/// Returns the direction of a specified axis of input
		/// </summary>
		/// <param name="axisName"></param>
		/// <param name="kState"></param>
		/// <returns></returns>
		public static float GetAxis(string axisName, KeyboardState kState) {
			float toReturn = 0;

			switch (axisName) {
				case "Horizontal":
					if (kState.IsKeyDown(Keys.A)) toReturn += -1;
					if (kState.IsKeyDown(Keys.D)) toReturn += 1;
					break;
				case "Vertical":
					if (kState.IsKeyDown(Keys.S)) toReturn += -1;
					if (kState.IsKeyDown(Keys.W)) toReturn += 1;
					break;
				default:
					break;
			}
			return toReturn;
		}

		/// <summary>
		/// Returns a Vector2 representation of the direction the WASD keys are pressing
		/// Normalized
		/// </summary>
		/// <param name="kState"></param>
		/// <returns></returns>
		public static Vector2 GetWASDVector(KeyboardState kState) {
			Vector2 toReturn = new Vector2(GetAxis("Horizontal", kState), GetAxis("Vertical", kState));
			if (toReturn != Vector2.Zero) {
				toReturn.Normalize();
			}
			return toReturn;
		}


	}
}
