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

		/// <summary>
		/// Returns the area of intersection between two BoundingBoxes. If they don't intersect then it returns null
		/// </summary>
		/// <param name="b1"></param>
		/// <param name="b2"></param>
		/// <returns></returns>
		public static BoundingBox? GetIntersection(BoundingBox b1, BoundingBox b2) {
			Vector3 bMax = new Vector3(Math.Min(b1.Max.X, b2.Max.X), Math.Min(b1.Max.Y, b2.Max.Y), Math.Min(b1.Max.Z, b2.Max.Z));
			Vector3 bMin = new Vector3(Math.Max(b1.Min.X, b2.Min.X), Math.Max(b1.Min.Y, b2.Min.Y), Math.Max(b1.Min.Z, b2.Min.Z));

			//difference between the two vectors. bMax should be greater than bMin in all dimensions or else they're not intersecting
			Vector3 dif = bMax - bMin;

			if (dif.X >= 0 && dif.Y >= 0 && dif.Z >= 0)
				return new BoundingBox(bMax, bMin);
			else return null;
		}
	}
}
