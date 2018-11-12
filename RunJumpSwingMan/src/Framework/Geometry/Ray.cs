using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace RunJumpSwingMan.src.Framework.Geometry {
	class Ray {

		public Vector3 origin;
		public Vector3 direction;

		public Ray(Vector3 ori, Vector3 dir) {
			origin = ori;
			direction = dir;
		}

		Vector3? intersection(Plane plane) {
			if (Vector3.Dot(direction, plane.normal) == 0) {
				return null;
			}

			float length = (Vector3.Dot(plane.normal, plane.origin) - Vector3.Dot(plane.normal, origin)) / (Vector3.Dot(plane.normal, direction));
			return origin + direction * length;
		}

	}
}
