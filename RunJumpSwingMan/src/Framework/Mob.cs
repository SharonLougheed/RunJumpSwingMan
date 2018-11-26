using System;
using Microsoft.Xna.Framework;

namespace RunJumpSwingMan.src.Framework {

	/// <summary>
	/// An Entity with health and movement capabilities
	/// </summary>
	public abstract class Mob : Entity {

		//a distance to add onto the bottom of the Mob for checking Grounded state
		public const float GROUND_CHECK_DIST = 0f;

		public int MaxHealth {
			get; set;
		}

		public int Health {
			get; set;
		}

		public float MaxMoveSpeed {
			get; set;
		}

		/// <summary>
		/// The direction the Mob intends to move
		/// </summary>
		public Vector3 Movement {
			get; set;
		}

		public bool Grounded {
			get;
			protected set;
		}

		public Mob() : base() {
			MaxHealth = 100;
			Health = MaxHealth;
		}
		
		public override void Update(GameTime gameTime) {
			Grounded = false;
			Grounded = CheckGrounded();
			/*
			if (Grounded) {
				Console.WriteLine("Standing on something");
			}
			*/
		}

		/// <summary>
		/// Returns whether the Mob is Grounded or not
		/// </summary>
		/// <returns></returns>
		public bool CheckGrounded() {
			Ray downRay = new Ray(Position, Vector3.Down);
			Tuple<Entity, Vector3, float> castResult = Parent.Raycast(downRay, Size.Y/2f + GROUND_CHECK_DIST, this);
			return (castResult != null);
		}

		/// <summary>
		/// Overrides the Entity GetDisplacement method to also factor in the movement of the Mob
		/// </summary>
		/// <param name="time">The current time of the game</param>
		public override Vector3 GetDisplacement(GameTime time) => (Velocity + Movement) * (float)time.ElapsedGameTime.TotalSeconds;

		/// <summary>
		/// Reduces the health of the Mob
		/// </summary>
		/// <param name="damage">The amount to damage the Mob by</param>
		public void Damage(int damage) {
			Health = Math.Max(Health - damage, 0);
			if (Health <= 0) {
				Died();
			}
		}

		/// <summary>
		/// Increases the health of the mob
		/// </summary>
		/// <param name="heals">The amount to heal the mob by</param>
		public void Heal( int heals ) => Health = Math.Min( Health + heals, MaxHealth );

		
		/// <summary>
		/// Delegate for handling the death of a Mob
		/// </summary>
		public delegate void DeathHandler();

		/// <summary>
		/// The event that is triggered when the Mob dies
		/// </summary>
		public event DeathHandler Died;

	}

}
