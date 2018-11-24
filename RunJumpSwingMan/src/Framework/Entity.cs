﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace RunJumpSwingMan.src.Framework {
	/// <summary>
	/// An object that exists in a World
	/// </summary>
	public abstract class Entity {

		/// <summary>
		/// The World that contains this Entity
		/// </summary>
		public World Parent { get; internal set; }

		public Vector3 Position { get; set; }
		public Vector3 Size { get; set; }
		public Vector3 Velocity{ get; set; }

		public float Volume { get { return Size.X * Size.Y * Size.Z; } }

		public BoundingBox Bounds { get { return GetBoundingBox(); } }

		//May not be using this, to save time
		//public Quaternion Rotation { get; set; }
		//public Quaternion RotationalVelocity { get; set; }

		/// <summary>
		/// When set to true, the entity will never move unless its position is manually set
		/// </summary>
		public bool Anchored { get; set; }

		/// <summary>
		/// Sets how much this entity is affected by the parent world's gravity
		/// Represented as a coefficient of gravitational acceleration (e.g. 1 for normal gravity, 0 for no gravity)
		/// </summary>
		public float GravityScale { get; set; }

		public Entity() {
			Parent = null;
			Position = new Vector3();
			Size = new Vector3();
			Velocity = new Vector3();
			//Rotation = new Quaternion();
			//RotationalVelocity = new Quaternion();

			Anchored = false;
			GravityScale = 1f;
		}

		/// <summary>
		/// Called by the Entity's Parent World to update the Entity
		/// </summary>
		/// <param name="gameTime">The time of the game at the moment Update was called</param>
		public abstract void Update(GameTime gameTime);

		/// <summary>
		/// Retusn the Entity's new position based on velocity and time elapsed BUT DOES NOT UPDATE ITS POSITION
		/// </summary>
		/// <param name="time">Amount of time elapsed in</param>
		public virtual Vector3 GetDisplacement(GameTime time) {
			return Velocity * (float)time.ElapsedGameTime.TotalSeconds;
		}

		/// <summary>
		/// Returns whether two Entities are intersecting
		/// </summary>
		/// <param name="other">The Entity to check intersection with</param>
		/// <returns>Whether two Entities are intersecting</returns>
		public bool Intersects(Entity other) {
			return GetBoundingBox().Intersects(other.GetBoundingBox());
		}

		/// <summary>
		/// Returns a BoundingBox that contains the Entity
		/// </summary>
		/// <returns>BoundingBox that encapsulates the Entity</returns>
		private BoundingBox GetBoundingBox() {
			Vector3 sizeHalf = Size / 2;
			return new BoundingBox(Position - sizeHalf, Position + sizeHalf);
		}

		/// <summary>
		/// Corrects the position and velocity of ent1, assuming ent1 and ent2 are colliding
		/// </summary>
		public static void CorrectCollision(Entity ent1, Entity ent2) {
			//skip this intersection if ghost1 is anchored (shouldn't move)
			if (ent1.Anchored || !ent1.Intersects(ent2)) return;

			//get the area that they intersect
			BoundingBox? intersectionArea = Geometry.GetIntersection(ent1.Bounds, ent2.Bounds);

			//time for the position correction
			if (intersectionArea.HasValue) {
				Vector3 area = intersectionArea.Value.Max - intersectionArea.Value.Min;
				Console.WriteLine(area);
				// position/velocity correction for the X axis
				if (area.X < area.Y && area.X < area.Z) {
					if (ent1.Position.X > ent2.Position.X) {
						ent1.Position = new Vector3(ent2.Position.X + (ent1.Size.X + ent2.Size.X) / 2, ent1.Position.Y, ent1.Position.Z);
						ent1.Velocity = new Vector3(Math.Max(ent1.Velocity.X, 0), ent1.Velocity.Y, ent1.Velocity.Z);
					}
					if (ent1.Position.X < ent2.Position.X) {
						ent1.Position = new Vector3(ent2.Position.X - (ent1.Size.X + ent2.Size.X) / 2, ent1.Position.Y, ent1.Position.Z);
						ent1.Velocity = new Vector3(Math.Min(ent1.Velocity.X, 0), ent1.Velocity.Y, ent1.Velocity.Z);
					}
				}

				// position/velocity correction for the Y axis
				if (area.Y < area.X && area.Y < area.Z) {
					if (ent1.Position.Y > ent2.Position.Y) {
						ent1.Position = new Vector3(ent1.Position.X, ent2.Position.Y + (ent1.Size.Y + ent2.Size.Y)/2, ent1.Position.Z);
						ent1.Velocity = new Vector3(ent1.Velocity.X, Math.Max(ent1.Velocity.Y, 0), ent1.Velocity.Z);
					}
					if (ent1.Position.Y < ent2.Position.Y) {
						ent1.Position = new Vector3(ent1.Position.X, ent2.Position.Y - (ent1.Size.Y + ent2.Size.Y) / 2, ent1.Position.Z);
						ent1.Velocity = new Vector3(ent1.Velocity.X, Math.Min(ent1.Velocity.Y, 0), ent1.Velocity.Z);
					}
				}

				// position/velocity correction for the Z axis
				if (area.Z < area.X && area.Z < area.Y) {
					if (ent1.Position.Z > ent2.Position.Z) {
						ent1.Position = new Vector3(ent1.Position.X, ent1.Position.Y, ent2.Position.Z + (ent1.Size.Z + ent2.Size.Z) / 2);
						ent1.Velocity = new Vector3(ent1.Velocity.X, ent1.Velocity.Y, Math.Max(ent1.Velocity.Z, 0));
					}
					if (ent1.Position.Z < ent2.Position.Z) {
						ent1.Position = new Vector3(ent1.Position.X, ent1.Position.Y, ent2.Position.Z - (ent1.Size.Z + ent2.Size.Z) / 2);
						ent1.Velocity = new Vector3(ent1.Velocity.X, ent1.Velocity.Y, Math.Min(ent1.Velocity.Z, 0));
					}
				}
			}

		}

		/// <summary>
		/// Fires off the event for colliding with another entity
		/// </summary>
		/// <param name="other"></param>
		internal void OnCollide(Entity other) {
			if (Collide != null) {
				Collide(other);
			}
		}

		/// <summary>
		/// Event handler delegate for collisions
		/// </summary>
		/// <param name="other">The Entity this one is colliding with</param>
		public delegate void CollisionHandler(Entity other);
		/// <summary>
		/// Event that is called whenever an Entity collides with another
		/// </summary>
		public event CollisionHandler Collide;
    }
}
