using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace RunJumpSwingMan.src.Framework {
	/// <summary>
	/// An Entity with health and movement capabilities
	/// </summary>
	abstract class Mob : Entity {

		public int MaxHealth { get; set; }
		public int Health { get; set; }

		public float MaxMoveSpeed { get; set; }

		/// <summary>
		/// The direction the Mob intends to move
		/// </summary>
		public Vector3 Movement { get; set; }

		public Mob() : base() {
			MaxHealth = 0;
			Health = MaxHealth;
		}

		/// <summary>
		/// Reduces the health of the Mob
		/// </summary>
		/// <param name="damage">The amount to damage the Mob by</param>
		public void Damage(int damage) {
			Health = Math.Max(Health - damage, 0);
		}

		/// <summary>
		/// Increases the health of the mob
		/// </summary>
		/// <param name="heals">The amount to heal the mob by</param>
		public void Heal(int heals) {
			Health = Math.Min(Health + heals, MaxHealth);
		}

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
