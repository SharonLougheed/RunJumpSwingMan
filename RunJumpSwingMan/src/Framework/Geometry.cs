using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace RunJumpSwingMan.src.Framework {
	public static class Geometry {

		//returns the 3D position of the projection of a point along a plane
		public static Vector3 ProjectionOnPlane(Vector3 point, Plane plane) {
			Vector3 planeOrigin = plane.Normal * Math.Abs(plane.D);
			Vector3 dif = point - planeOrigin;

			//the projection of dif on the plane normal
			Vector3 projDifOnNorm = plane.Normal * plane.DotNormal(dif) / (float)Math.Pow(plane.Normal.LengthSquared(), 2);
			//the difference between dif and its projection along the plane normal
			//equal to the difference between the projection of point on the plane and the plane origin 
			Vector3 projDif = dif - projDifOnNorm;

			return planeOrigin + projDif;
		}


	}
}
