using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace RunJumpSwingMan.src.Framework {
	/// <summary>
	/// An object that exists in a World
	/// </summary>
	abstract class Entity {

		/// <summary>
		/// The World that contains this Entity
		/// </summary>
		public World Parent { get; internal set; }

		public Vector3 Position { get; set; }
		public Vector3 Size { get; set; }
		public Vector3 Velocity{ get; set; }

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
		/// Updates the Entity's position based on velocity and time elapsed
		/// </summary>
		/// <param name="time">Amount of time elapsed in</param>
		public void ApplyVelocity(float time) {
			Position += Velocity * time;
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
		public BoundingBox GetBoundingBox() {
			Vector3 sizeHalf = Size / 2;
			return new BoundingBox(Position + sizeHalf, Position - sizeHalf);
		}

		/// <summary>
		/// Event handler delegate for collisions
		/// </summary>
		/// <param name="other">The Entity this one is colliding with</param>
		public delegate void CollisionHandler(Entity other);
		/// <summary>
		/// Event that is called whenever an Entity collides with another
		/// </summary>
		public event CollisionHandler Collides;
    }
}
