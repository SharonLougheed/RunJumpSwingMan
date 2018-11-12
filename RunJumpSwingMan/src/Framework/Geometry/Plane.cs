using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace RunJumpSwingMan.src.Framework.Geometry {
	class Plane {

		public Vector3 origin;
		public Vector3 normal;

		public Plane(Vector3 ori, Vector3 norm) {
			origin = ori;
			normal = norm;
		}

		
	}
}
