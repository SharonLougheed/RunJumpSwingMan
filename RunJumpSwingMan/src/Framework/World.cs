using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace RunJumpSwingMan.src.Framework {

	public class World {

		private List<Entity> _entities;
		public ReadOnlyCollection<Entity> Entities { get { return new ReadOnlyCollection<Entity>(_entities); } }

		//lists of items to add and remove
		LinkedList<Entity> _addList;
		LinkedList<Entity> _removeList;

		/// <summary>
		/// Acceleration dueto gravity. duh.
		/// </summary>
		public float GravityAcceleration { get; set; }

		public World() {
			_entities = new List<Entity>();
			_addList = new LinkedList<Entity>();
			_removeList = new LinkedList<Entity>();

			GravityAcceleration = 9.8f;
		}

		/// <summary>
		/// Well, pretty self explanatory: The method that literally makes the world go 'round
		/// </summary>
		/// <param name="gameTime"></param>
		public void Update(GameTime gameTime) {

			foreach (Entity ent in _entities) {
				ent.Update(gameTime);
				ent.Position += ent.GetDisplacement(gameTime);
			}

			//list of entity collision relations
			//for a collision relation I'm gonna use an array of ghosts of length 2
			//i don't feel like making a class for this>
			LinkedList<Entity[]> intersections = new LinkedList<Entity[]>();

			//collision checking. Yes, I know it's O(n^2)
			foreach (Entity ent1 in _entities) {
				foreach(Entity ent2 in _entities) {
					if (ent1 == ent2) continue;
					if (ent1.Intersects(ent2)) {
						//add the relation if the ghosts' bounds intersect
						intersections.AddLast(new Entity[] { ent1, ent2 });
					}
				}
			}

			//iterate through the intersections
			foreach (Entity[] sectRel in intersections) {
				Entity ent1 = sectRel[0];
				Entity ent2 = sectRel[1];

				//skip this intersection if ghost1 is anchored (shouldn't move)
				if (ent1.Anchored) continue;

				//corrects the entity's position and velocity
				Entity.CorrectCollision(ent1, ent2);

				//have the entity fire off its collision event
				ent1.OnCollide(ent2);
			}

			//perform any add/remove entity maintenance
			ProcessEntityQueues();

		}

		/// <summary>
		/// Adds entities in the adding queue into the world and removes entities in the removal queue from the world
		/// </summary>
		private void ProcessEntityQueues() {
			//adding to the world
			foreach (Entity ent in _addList) {
				_entities.Add(ent);
				ent.Parent = this;
			}
			_addList.Clear();
			//removing from the world
			foreach (Entity ent in _removeList) {
				_entities.Remove(ent);
				ent.Parent = null;
			}
			_removeList.Clear();
		}

		/// <summary>
		/// Places an Entity in queue for insertion into the world 
		/// </summary>
		/// <param name="ent">The Entity to add</param>
		public void AddEntity(Entity ent) {
			if (!_addList.Contains(ent) && !_entities.Contains(ent))
				_addList.AddLast(ent);
		}

		/// <summary>
		/// Places an Entity in queue for deletion from the world
		/// </summary>
		/// <param name="ent">The Entity to add</param>
		public void RemoveEntity(Entity ent) {
			if (!_removeList.Contains(ent) && _entities.Contains(ent))
				_removeList.AddLast(ent);
		}

		

	}

}
