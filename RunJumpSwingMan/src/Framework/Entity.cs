using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace RunJumpSwingMan.src.Framework {

	abstract class Entity {

		/// <summary>
		/// The World that contains this object
		/// </summary>
		public World Parent { get; internal set; }

		public Vector3 Position { get; set; }
		public Vector3 Size { get; set; }
		public Vector3 Velocity{ get; set; }
		public Quaternion Rotation { get; set; }
		public Quaternion RotationalVelocity { get; set; }

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
			Rotation = new Quaternion();
			RotationalVelocity = new Quaternion();

			Anchored = false;
			GravityScale = 1f;
		}

		public void Update(GameTime gameTime) {

		}

		/// <summary>
		/// Event handler delegate for collisions
		/// </summary>
		/// <param name="other"></param>
		public delegate void CollisionHandler(Entity other);
		/// <summary>
		/// Event that is called whenever an object collides with another
		/// </summary>
		public event CollisionHandler Collides;
    }
}
