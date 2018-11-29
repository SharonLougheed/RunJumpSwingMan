using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RunJumpSwingMan.src.Framework {

	/// <summary>
	/// An object that exists in a World
	/// </summary>
	public abstract class Entity {

		public VertexBuffer VertexBuffer {
			get;
			set;
		}

		public IndexBuffer IndexBuffer {
			get;
			set;
		}

		public BasicEffect BasicEffect {
			get;
			set;
		}

		public int PrimitiveCount {
			get;
			set;
		}

		/// <summary>
		/// The World that contains this Entity
		/// </summary>
		public World Parent {
			get;
			internal set;
		}

		public Vector3 Position {
			get;
			set;
		}

		public Vector3 Size {
			get;
			set;
		}

		public Vector3 Velocity {
			get;
			set;
		}

		public float StaticFriction {
			get;
			set;
		}

		public float Volume => Size.X * Size.Y * Size.Z;

		public BoundingBox Bounds => GetBoundingBox();

		//May not be using this, to save time
		//public Quaternion Rotation { get; set; }
		//public Quaternion RotationalVelocity { get; set; }

		/// <summary>
		/// When set to true, the entity will never move unless its position is manually set
		/// </summary>
		public bool Anchored {
			get;
			set;
		}

		/// <summary>
		/// Sets how much this entity is affected by the parent world's gravity
		/// Represented as a coefficient of gravitational acceleration (e.g. 1 for normal gravity, 0 for no gravity)
		/// </summary>
		public float GravityScale {
			get;
			set;
		}

		public Entity() {
			VertexBuffer = null;
			IndexBuffer = null;
			BasicEffect = null;
			PrimitiveCount = 0;

			Parent = null;
			Position = Vector3.Zero;
			Size = Vector3.One;
			Velocity = Vector3.Zero;

			Anchored = false;
			GravityScale = 1.0f;
			StaticFriction = 0.6f;
		}

		/// <summary>
		/// Called by the Entity's Parent World to update the Entity
		/// </summary>
		/// <param name="gameTime">The time of the game at the moment Update was called</param>
		public abstract void Update(GameTime gameTime);

		/// <summary>
		/// Updates Position and Velocity as a function of time
		/// </summary>
		/// <param name="time"></param>
		public virtual void UpdateKinematics(GameTime time) {
			Position += GetDisplacement(time);
			UpdateVelocity(time);
		}

		/// <summary>
		/// Return the Entity's new position based on velocity and time elapsed BUT DOES NOT UPDATE ITS POSITION
		/// </summary>
		/// <param name="time">Time of the game</param>
		public virtual Vector3 GetDisplacement(GameTime time) {
			return Geometry.KinematicDistance(Velocity, Parent.GravityAcceleration * GravityScale, (float)time.ElapsedGameTime.TotalSeconds);
		}

		/// <summary>
		/// Updates the Entity's Velocity as a function of acceleration (at the moment, only due to gravity) and time
		/// </summary>
		/// <param name="time"></param>
		public virtual void UpdateVelocity(GameTime time) {
			Velocity = Geometry.KinematicSpeed(Velocity, Parent.GravityAcceleration * GravityScale, (float)time.ElapsedGameTime.TotalSeconds);
		}

		/// <summary>
		/// Returns whether two Entities are intersecting
		/// </summary>
		/// <param name="other">The Entity to check intersection with</param>
		/// <returns>Whether two Entities are intersecting</returns>
		public bool Intersects( Entity other ) => GetBoundingBox().Intersects( other.GetBoundingBox() );

		/// <summary>
		/// Returns a BoundingBox that contains the Entity
		/// </summary>
		/// <returns>BoundingBox that encapsulates the Entity</returns>
		private BoundingBox GetBoundingBox() {
			Vector3 sizeHalf = Size / 2;
			return new BoundingBox( Position - sizeHalf, Position + sizeHalf );
		}

		/// <summary>
		/// Corrects the position and velocity of ent1, assuming ent1 and ent2 are colliding
		/// </summary>
		public static void CorrectCollision( Entity ent1, Entity ent2 ) {
			//skip this intersection if ghost1 is anchored (shouldn't move)
			if ( ent1.Anchored || !ent1.Intersects( ent2 ) ) {
				return;
			}
			//get the area that they intersect
			BoundingBox? intersectionArea = Geometry.GetIntersection( ent1.Bounds, ent2.Bounds );

			//time for the position correction
			if ( intersectionArea.HasValue ) {
				Vector3 area = intersectionArea.Value.Max - intersectionArea.Value.Min;
				// position/velocity correction for the X axis
				if ( area.X < area.Y && area.X < area.Z ) {
					if ( ent1.Position.X > ent2.Position.X ) {
						ent1.Position = new Vector3( ent2.Position.X + ( ent1.Size.X + ent2.Size.X ) / 2, ent1.Position.Y, ent1.Position.Z );
						ent1.Velocity = new Vector3( Math.Max( ent1.Velocity.X, 0 ), ent1.Velocity.Y, ent1.Velocity.Z );
					}
					if ( ent1.Position.X < ent2.Position.X ) {
						ent1.Position = new Vector3( ent2.Position.X - ( ent1.Size.X + ent2.Size.X ) / 2, ent1.Position.Y, ent1.Position.Z );
						ent1.Velocity = new Vector3( Math.Min( ent1.Velocity.X, 0 ), ent1.Velocity.Y, ent1.Velocity.Z );
					}
				}

				// position/velocity correction for the Y axis
				if ( area.Y < area.X && area.Y < area.Z ) {
					if ( ent1.Position.Y > ent2.Position.Y ) {
						ent1.Position = new Vector3( ent1.Position.X, ent2.Position.Y + ( ent1.Size.Y + ent2.Size.Y ) / 2, ent1.Position.Z );
						ent1.Velocity = new Vector3( ent1.Velocity.X, Math.Max( ent1.Velocity.Y, 0 ), ent1.Velocity.Z );
					}
					if ( ent1.Position.Y < ent2.Position.Y ) {
						ent1.Position = new Vector3( ent1.Position.X, ent2.Position.Y - ( ent1.Size.Y + ent2.Size.Y ) / 2, ent1.Position.Z );
						ent1.Velocity = new Vector3( ent1.Velocity.X, Math.Min( ent1.Velocity.Y, 0 ), ent1.Velocity.Z );
					}
				}

				// position/velocity correction for the Z axis
				if ( area.Z < area.X && area.Z < area.Y ) {
					if ( ent1.Position.Z > ent2.Position.Z ) {
						ent1.Position = new Vector3( ent1.Position.X, ent1.Position.Y, ent2.Position.Z + ( ent1.Size.Z + ent2.Size.Z ) / 2 );
						ent1.Velocity = new Vector3( ent1.Velocity.X, ent1.Velocity.Y, Math.Max( ent1.Velocity.Z, 0 ) );
					}
					if ( ent1.Position.Z < ent2.Position.Z ) {
						ent1.Position = new Vector3( ent1.Position.X, ent1.Position.Y, ent2.Position.Z - ( ent1.Size.Z + ent2.Size.Z ) / 2 );
						ent1.Velocity = new Vector3( ent1.Velocity.X, ent1.Velocity.Y, Math.Min( ent1.Velocity.Z, 0 ) );
					}
				}

			}

		}

		/// <summary>
		/// Fires off the event for colliding with another entity
		/// </summary>
		/// <param name="other"></param>
		public void OnCollide( Entity other ) {
			if ( Collide != null ) {
				Collide( other );
			}
		}

		public void Draw( GameTime gameTime, GraphicsDeviceManager graphics, Camera camera, Vector3 diffuseColor, Vector3 lightDirection, Vector3 specularColor ) {
			BasicEffect.World = Matrix.CreateScale( Size.X, Size.Y, Size.Z ) * Matrix.CreateTranslation( Position );
			//BasicEffect.World = Matrix.CreateFromYawPitchRoll( Orientation.Y, Orientation.X, Orientation.Z ) * Matrix.CreateScale( Size.X, Size.Y, Size.Z ) * Matrix.CreateTranslation( Position );
			BasicEffect.View = camera.ViewMatrix;
			BasicEffect.Projection = camera.ProjectionMatrix;

			//BasicEffect.VertexColorEnabled = true;
			//BasicEffect.PreferPerPixelLighting = true;

			BasicEffect.LightingEnabled = true;
			BasicEffect.DirectionalLight0.DiffuseColor = diffuseColor;
			BasicEffect.DirectionalLight0.Direction = lightDirection;
			BasicEffect.DirectionalLight0.Enabled = true;
			BasicEffect.DirectionalLight0.SpecularColor = specularColor;

			graphics.GraphicsDevice.SetVertexBuffer( VertexBuffer );
			graphics.GraphicsDevice.Indices = IndexBuffer;

			foreach ( EffectPass effectPass in BasicEffect.CurrentTechnique.Passes ) {
				effectPass.Apply();
				graphics.GraphicsDevice.DrawIndexedPrimitives( PrimitiveType.TriangleList, 0, 0, PrimitiveCount );
			}
		}

		/// <summary>
		/// Event handler delegate for collisions
		/// </summary>
		/// <param name="other">The Entity this one is colliding with</param>
		public delegate void CollisionHandler( Entity other );

		/// <summary>
		/// Event that is called whenever an Entity collides with another
		/// </summary>
		public event CollisionHandler Collide;

	}

}
