using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using RunJumpSwingMan.src.Framework;

namespace RunJumpSwingMan.src.Gameplay {
	class Player : Mob {

		public PlayerController Controller { get; set; }

		public Player() {
			Controller = new PlayerController();

		}

		public override void Update(GameTime time) {

			Controller.Update(time);
		}



	}
}
