using Microsoft.Xna.Framework;
using RunJumpSwingMan.src.Framework;

namespace RunJumpSwingMan.src.Gameplay {

	public class Block : Entity {

		public Block() {
			Anchored = true;
			Size = new Vector3( 20, 20, 20 );
		}

		public override void Update( GameTime time ) {
		}
		
	}

}
