using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using RunJumpSwingMan.src.Framework;

namespace RunJumpSwingMan.src.Gameplay {
	class PlayerController {

		Player subject;

		Camera camera;

		public void Start(Player sub) {
			subject = sub;
		}

		public void Update(GameTime time) {

		}

	}
}
