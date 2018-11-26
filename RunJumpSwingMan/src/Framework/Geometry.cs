using System;
using Microsoft.Xna.Framework;

namespace RunJumpSwingMan.src.Framework {

	public static class Geometry {

		/// <summary>
		/// returns the 3D position of the projection of a point along a plane
		/// </summary>
		/// <param name="point"></param>
		/// <param name="plane"></param>
		/// <returns></returns>
		public static Vector3 ProjectionOnPlane( Vector3 point, Plane plane ) {
			Vector3 planeOrigin = plane.Normal * Math.Abs( plane.D );
			Vector3 dif = point - planeOrigin;

			//the projection of dif on the plane normal
			Vector3 projDifOnNorm = plane.Normal * plane.DotNormal( dif ) / ( float )Math.Pow( plane.Normal.LengthSquared(), 2 );
			//the difference between dif and its projection along the plane normal
			//equal to the difference between the projection of point on the plane and the plane origin 
			Vector3 projDif = dif - projDifOnNorm;

			return planeOrigin + projDif;
		}

		/// <summary>
		/// Returns the projection of Vector3 v on the plane perpendicular to normalized Vector3 norm
		/// </summary>
		/// <param name="v">The Vector3 to take the projection of</param>
		/// <param name="norm">The normalized direction of the normal of the plane</param>
		/// <returns></returns>
		public static Vector3 PerpendicularProjection(Vector3 v, Vector3 norm) {
			//the projection of v on norm
			Vector3 vPara = Vector3.Dot(v, norm) * norm;
			return v - vPara;
		}

		/// <summary>
		/// Returns the area of intersection between two BoundingBoxes. If they don't intersect then it returns null
		/// </summary>
		/// <param name="b1"></param>
		/// <param name="b2"></param>
		/// <returns></returns>
		public static BoundingBox? GetIntersection( BoundingBox b1, BoundingBox b2 ) {
			Vector3 bMax = new Vector3( Math.Min( b1.Max.X, b2.Max.X ), Math.Min( b1.Max.Y, b2.Max.Y ), Math.Min( b1.Max.Z, b2.Max.Z ) );
			Vector3 bMin = new Vector3( Math.Max( b1.Min.X, b2.Min.X ), Math.Max( b1.Min.Y, b2.Min.Y ), Math.Max( b1.Min.Z, b2.Min.Z ) );

			//difference between the two vectors. bMax should be greater than bMin in all dimensions or else they're not intersecting
			Vector3 dif = bMax - bMin;

			if ( dif.X >= 0 && dif.Y >= 0 && dif.Z >= 0 ) {
				return new BoundingBox( bMin, bMax );
			} else {
				return null;
			}
		}

		/// <summary>
		/// Applies the kinematic equation for distance as a function of speed, acceleration, and time. You know, Phys 1 stuff
		/// </summary>
		/// <param name="speed"></param>
		/// <param name="acceleration"></param>
		/// <param name="time"></param>
		/// <returns></returns>
		public static float KinematicDistance(float speed1, float acceleration, float time) {
			return (speed1 + .5f * acceleration * time) * time;
		}

		/// <summary>
		/// Applies the kinematic equation for final speed as a function of initial speed, acceleration, and time
		/// </summary>
		/// <returns></returns>
		public static float KinematicSpeed(float speed1, float acceleration, float time) {
			return speed1 + acceleration * time;
		}

		/// <summary>
		/// Applies the kinematic equation for distance as a function of velocity, acceleration, and time for Vector3's
		/// </summary>
		/// <param name="speed"></param>
		/// <param name="acceleration"></param>
		/// <param name="time"></param>
		/// <returns></returns>
		public static Vector3 KinematicDistance(Vector3 velocity1, Vector3 acceleration, float time) {
			return (velocity1 + .5f * acceleration * time) * time;
		}

		/// <summary>
		/// Applies the kinematic equation for final speed as a function of initial speed, acceleration, and time
		/// </summary>
		/// <returns></returns>
		public static Vector3 KinematicSpeed(Vector3 velocity1, Vector3 acceleration, float time) {
			return velocity1 + acceleration * time;
		}
	}

}
