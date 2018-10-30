using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace RunJumpSwingMan.src.Framework {

	class World {

		public LinkedList<Entity> Entities { get; private set; }

		/// <summary>
		/// Acceleration dueto gravity. duh.
		/// </summary>
		public float GravityAcceleration { get; set; }


		public World() {
			Entities = new LinkedList<Entity>();

			GravityAcceleration = 9.8f;
		}

		/// <summary>
		/// Well, pretty self explanatory: The method that literally makes the world go 'round
		/// </summary>
		/// <param name="gameTime"></param>
		public void Update(GameTime gameTime) {

		}
	}
}
