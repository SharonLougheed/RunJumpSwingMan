using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RunJumpSwingMan.src {

	public class Shapes {

		public static VertexBuffer IndexedVertexBufferCube( GraphicsDeviceManager graphics, Color color ) {
			VertexPositionNormalTexture[] vertices = new VertexPositionNormalTexture[ 8 ];
			float unitAxisLength = ( float )Math.Sqrt( 1.0 / 3.0 );
			/*
			vertices[ 0 ] = new VertexPositionNormalTexture( new Vector3( -0.5f, -0.5f, 0.5f ), new Vector3( -unitAxisLength, -unitAxisLength, unitAxisLength ), new Vector2( 1.0f, 1.0f ) );
			vertices[ 1 ] = new VertexPositionNormalTexture( new Vector3( 0.5f, -0.5f, 0.5f ), new Vector3( unitAxisLength, -unitAxisLength, unitAxisLength ), new Vector2( 1.0f, 1.0f ) );
			vertices[ 2 ] = new VertexPositionNormalTexture( new Vector3( 0.5f, -0.5f, -0.5f ), new Vector3( unitAxisLength, -unitAxisLength, -unitAxisLength ), new Vector2( 1.0f, 1.0f ) );
			vertices[ 3 ] = new VertexPositionNormalTexture( new Vector3( -0.5f, -0.5f, -0.5f ), new Vector3( -unitAxisLength, -unitAxisLength, -unitAxisLength ), new Vector2( 1.0f, 1.0f ) );
			vertices[ 4 ] = new VertexPositionNormalTexture( new Vector3( -0.5f, 0.5f, 0.5f ), new Vector3( -unitAxisLength, unitAxisLength, unitAxisLength ), new Vector2( 1.0f, 1.0f ) );
			vertices[ 5 ] = new VertexPositionNormalTexture( new Vector3( 0.5f, 0.5f, 0.5f ), new Vector3( unitAxisLength, unitAxisLength, unitAxisLength ), new Vector2( 1.0f, 1.0f ) );
			vertices[ 6 ] = new VertexPositionNormalTexture( new Vector3( 0.5f, 0.5f, -0.5f ), new Vector3( unitAxisLength, unitAxisLength, -unitAxisLength ), new Vector2( 1.0f, 1.0f ) );
			vertices[ 7 ] = new VertexPositionNormalTexture( new Vector3( -0.5f, 0.5f, -0.5f ), new Vector3( -unitAxisLength, unitAxisLength, -unitAxisLength ), new Vector2( 1.0f, 1.0f ) );
			*/
			vertices[0] = new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, 0.5f), new Vector3(unitAxisLength, unitAxisLength, -unitAxisLength), new Vector2(1.0f, 1.0f));
			vertices[1] = new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, 0.5f), new Vector3(-unitAxisLength, unitAxisLength, -unitAxisLength), new Vector2(1.0f, 1.0f));
			vertices[2] = new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, -0.5f), new Vector3(-unitAxisLength, unitAxisLength, unitAxisLength), new Vector2(1.0f, 1.0f));
			vertices[3] = new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, -0.5f), new Vector3(unitAxisLength, unitAxisLength, unitAxisLength), new Vector2(1.0f, 1.0f));
			vertices[4] = new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, 0.5f), new Vector3(unitAxisLength, -unitAxisLength, -unitAxisLength), new Vector2(1.0f, 1.0f));
			vertices[5] = new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, 0.5f), new Vector3(-unitAxisLength, -unitAxisLength, -unitAxisLength), new Vector2(1.0f, 1.0f));
			vertices[6] = new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, -0.5f), new Vector3(-unitAxisLength, -unitAxisLength, unitAxisLength), new Vector2(1.0f, 1.0f));
			vertices[7] = new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, -0.5f), new Vector3(unitAxisLength, -unitAxisLength, unitAxisLength), new Vector2(1.0f, 1.0f));

			VertexBuffer vertexBuffer = new VertexBuffer( graphics.GraphicsDevice, typeof( VertexPositionNormalTexture ), vertices.Length, BufferUsage.WriteOnly );
			vertexBuffer.SetData<VertexPositionNormalTexture>( vertices );

			return vertexBuffer;
		}

		public static IndexBuffer IndexBufferCube( GraphicsDeviceManager graphics ) {
			short[] indices = new short[ 36 ];
			/*
			indices[ 0 ] = 0;
			indices[ 1 ] = 3;
			indices[ 2 ] = 2;
			indices[ 3 ] = 2;
			indices[ 4 ] = 1;
			indices[ 5 ] = 0;
			indices[ 6 ] = 4;
			indices[ 7 ] = 5;
			indices[ 8 ] = 6;
			indices[ 9 ] = 6;
			indices[ 10 ] = 7;
			indices[ 11 ] = 4;
			indices[ 12 ] = 0;
			indices[ 13 ] = 1;
			indices[ 14 ] = 5;
			indices[ 15 ] = 5;
			indices[ 16 ] = 4;
			indices[ 17 ] = 0;
			indices[ 18 ] = 1;
			indices[ 19 ] = 2;
			indices[ 20 ] = 6;
			indices[ 21 ] = 6;
			indices[ 22 ] = 5;
			indices[ 23 ] = 1;
			indices[ 24 ] = 2;
			indices[ 25 ] = 3;
			indices[ 26 ] = 7;
			indices[ 27 ] = 7;
			indices[ 28 ] = 6;
			indices[ 29 ] = 2;
			indices[ 30 ] = 3;
			indices[ 31 ] = 0;
			indices[ 32 ] = 4;
			indices[ 33 ] = 4;
			indices[ 34 ] = 7;
			indices[ 35 ] = 3;
			*/

			indices[0] = 2;
			indices[1] = 3;
			indices[2] = 0;
			indices[3] = 0;
			indices[4] = 1;
			indices[5] = 2;
			indices[6] = 6;
			indices[7] = 5;
			indices[8] = 4;
			indices[9] = 4;
			indices[10] = 7;
			indices[11] = 6;
			indices[12] = 5;
			indices[13] = 1;
			indices[14] = 0;
			indices[15] = 0;
			indices[16] = 4;
			indices[17] = 5;
			indices[18] = 6;
			indices[19] = 2;
			indices[20] = 1;
			indices[21] = 1;
			indices[22] = 5;
			indices[23] = 6;
			indices[24] = 7;
			indices[25] = 3;
			indices[26] = 2;
			indices[27] = 2;
			indices[28] = 6;
			indices[29] = 7;
			indices[30] = 4;
			indices[31] = 0;
			indices[32] = 3;
			indices[33] = 3;
			indices[34] = 7;
			indices[35] = 4;

			IndexBuffer indexBuffer = new IndexBuffer( graphics.GraphicsDevice, typeof( short ), indices.Length, BufferUsage.WriteOnly );
			indexBuffer.SetData<short>( indices );

			return indexBuffer;
		}

		public static int PrimitiveCountCube() {
			return 12;
		}

		public static VertexBuffer IndexedVertexBufferIcosahedron( GraphicsDeviceManager graphics ) {
			VertexPositionColor[] vertices = new VertexPositionColor[ 12 ];
			vertices[ 0 ] = new VertexPositionColor( new Vector3( -0.26286500f, 0.0f, 0.42532500f ), Color.Red );
			vertices[ 1 ] = new VertexPositionColor( new Vector3( 0.26286500f, 0.0f, 0.42532500f ), Color.Orange );
			vertices[ 2 ] = new VertexPositionColor( new Vector3( -0.26286500f, 0.0f, -0.42532500f ), Color.Yellow );
			vertices[ 3 ] = new VertexPositionColor( new Vector3( 0.26286500f, 0.0f, -0.42532500f ), Color.Green );
			vertices[ 4 ] = new VertexPositionColor( new Vector3( 0.0f, 0.42532500f, 0.26286500f ), Color.Blue );
			vertices[ 5 ] = new VertexPositionColor( new Vector3( 0.0f, 0.42532500f, -0.26286500f ), Color.Indigo );
			vertices[ 6 ] = new VertexPositionColor( new Vector3( 0.0f, -0.42532500f, 0.26286500f ), Color.Purple );
			vertices[ 7 ] = new VertexPositionColor( new Vector3( 0.0f, -0.42532500f, -0.26286500f ), Color.White );
			vertices[ 8 ] = new VertexPositionColor( new Vector3( 0.42532500f, 0.26286500f, 0.0f ), Color.Cyan );
			vertices[ 9 ] = new VertexPositionColor( new Vector3( -0.42532500f, 0.26286500f, 0.0f ), Color.Black );
			vertices[ 10 ] = new VertexPositionColor( new Vector3( 0.42532500f, -0.26286500f, 0.0f ), Color.DodgerBlue );
			vertices[ 11 ] = new VertexPositionColor( new Vector3( -0.42532500f, -0.26286500f, 0.0f ), Color.Crimson );

			VertexBuffer vertexBuffer = new VertexBuffer( graphics.GraphicsDevice, typeof( VertexPositionColor ), vertices.Length, BufferUsage.WriteOnly );
			vertexBuffer.SetData<VertexPositionColor>( vertices );

			return vertexBuffer;
		}

		public static IndexBuffer IndexBufferIcosahedron( GraphicsDeviceManager graphics ) {
			short[] indices = new short[ 60 ];
			indices[ 0 ] = 0;
			indices[ 1 ] = 6;
			indices[ 2 ] = 1;
			indices[ 3 ] = 0;
			indices[ 4 ] = 11;
			indices[ 5 ] = 6;
			indices[ 6 ] = 1;
			indices[ 7 ] = 4;
			indices[ 8 ] = 0;
			indices[ 9 ] = 1;
			indices[ 10 ] = 8;
			indices[ 11 ] = 4;
			indices[ 12 ] = 1;
			indices[ 13 ] = 10;
			indices[ 14 ] = 8;
			indices[ 15 ] = 2;
			indices[ 16 ] = 5;
			indices[ 17 ] = 3;
			indices[ 18 ] = 2;
			indices[ 19 ] = 9;
			indices[ 20 ] = 5;
			indices[ 21 ] = 2;
			indices[ 22 ] = 11;
			indices[ 23 ] = 9;
			indices[ 24 ] = 3;
			indices[ 25 ] = 7;
			indices[ 26 ] = 2;
			indices[ 27 ] = 3;
			indices[ 28 ] = 10;
			indices[ 29 ] = 7;
			indices[ 30 ] = 4;
			indices[ 31 ] = 8;
			indices[ 32 ] = 5;
			indices[ 33 ] = 4;
			indices[ 34 ] = 9;
			indices[ 35 ] = 0;
			indices[ 36 ] = 5;
			indices[ 37 ] = 8;
			indices[ 38 ] = 3;
			indices[ 39 ] = 5;
			indices[ 40 ] = 9;
			indices[ 41 ] = 4;
			indices[ 42 ] = 6;
			indices[ 43 ] = 10;
			indices[ 44 ] = 1;
			indices[ 45 ] = 6;
			indices[ 46 ] = 11;
			indices[ 47 ] = 7;
			indices[ 48 ] = 7;
			indices[ 49 ] = 10;
			indices[ 50 ] = 6;
			indices[ 51 ] = 7;
			indices[ 52 ] = 11;
			indices[ 53 ] = 2;
			indices[ 54 ] = 8;
			indices[ 55 ] = 10;
			indices[ 56 ] = 3;
			indices[ 57 ] = 9;
			indices[ 58 ] = 11;
			indices[ 59 ] = 0;

			IndexBuffer indexBuffer = new IndexBuffer( graphics.GraphicsDevice, typeof( short ), indices.Length, BufferUsage.WriteOnly );
			indexBuffer.SetData<short>( indices );

			return indexBuffer;
		}

		public static int PrimitiveCountIcosahedron() {
			return 20;
		}

		public static VertexBuffer IndexedVertexBufferPlane( GraphicsDeviceManager graphics, Color color ) {
			VertexPositionColor[] vertices = new VertexPositionColor[ 4 ];
			vertices[ 0 ] = new VertexPositionColor( new Vector3( -0.5f, 0.0f, 0.5f ), color );
			vertices[ 1 ] = new VertexPositionColor( new Vector3( 0.5f, 0.0f, 0.5f ), color );
			vertices[ 2 ] = new VertexPositionColor( new Vector3( 0.5f, 0.0f, -0.5f ), color );
			vertices[ 3 ] = new VertexPositionColor( new Vector3( -0.5f, 0.0f, -0.5f ), color );

			VertexBuffer vertexBuffer = new VertexBuffer( graphics.GraphicsDevice, typeof( VertexPositionColor ), vertices.Length, BufferUsage.WriteOnly );
			vertexBuffer.SetData<VertexPositionColor>( vertices );

			return vertexBuffer;
		}

		public static IndexBuffer IndexBufferPlane( GraphicsDeviceManager graphics ) {
			short[] indices = new short[ 6 ];
			indices[ 0 ] = 0;
			indices[ 1 ] = 1;
			indices[ 2 ] = 2;
			indices[ 3 ] = 2;
			indices[ 4 ] = 3;
			indices[ 5 ] = 0;

			IndexBuffer indexBuffer = new IndexBuffer( graphics.GraphicsDevice, typeof( short ), indices.Length, BufferUsage.WriteOnly );
			indexBuffer.SetData<short>( indices );

			return indexBuffer;
		}

		public static int PrimitiveCountPlane() {
			return 2;
		}

		public static VertexBuffer IndexedVertexBufferCylinder( GraphicsDeviceManager graphics, Color color, int sideFaces ) {
			if ( sideFaces < 1 ) {
				return null;
			} else if ( sideFaces == 1 ) {
				return IndexedVertexBufferLine( graphics );
			}

			VertexPositionColor[] vertices = new VertexPositionColor[ 2 * sideFaces + 2 ];

			vertices[ 0 ] = new VertexPositionColor( new Vector3( 0.0f, -0.5f, 0.0f ), color );
			vertices[ 1 ] = new VertexPositionColor( new Vector3( 0.0f, 0.5f, 0.0f ), color );

			float wedgeAngleDiff = 2.0f * MathHelper.Pi / sideFaces;
			float wedgeAngle = 0.0f;
			for ( int i = 0; i < sideFaces; i++ ) {
				float x = 0.5f * ( float )Math.Cos( wedgeAngle );
				float z = 0.5f * ( float )Math.Sin( wedgeAngle );
				vertices[ i + 2 ] = new VertexPositionColor( new Vector3( x, -0.5f, z ), color );
				vertices[ i + sideFaces + 2 ] = new VertexPositionColor( new Vector3( x, 0.5f, z ), color );

				wedgeAngle += wedgeAngleDiff;
			}

			VertexBuffer vertexBuffer = new VertexBuffer( graphics.GraphicsDevice, typeof( VertexPositionColor ), vertices.Length, BufferUsage.WriteOnly );
			vertexBuffer.SetData<VertexPositionColor>( vertices );

			return vertexBuffer;
		}

		public static IndexBuffer IndexBufferCylinder( GraphicsDeviceManager graphics, int sideFaces ) {
			if ( sideFaces < 1 ) {
				return null;
			} else if ( sideFaces == 1 ) {
				return IndexBufferLine( graphics );
			}

			short[] indices = new short[ 3 * 4 * sideFaces ];

			// Bottom face triangles
			int bottomLevelIndex = 2;
			for ( int i = 0; i < sideFaces - 1; i++ ) {
				indices[ 3 * i + 0 ] = 0;
				indices[ 3 * i + 1 ] = ( short )bottomLevelIndex;
				indices[ 3 * i + 2 ] = ( short )( bottomLevelIndex + 1 );

				bottomLevelIndex += 1;
			}

			indices[ 3 * ( sideFaces - 1 ) + 0 ] = 0;
			indices[ 3 * ( sideFaces - 1 ) + 1 ] = ( short )bottomLevelIndex;
			indices[ 3 * ( sideFaces - 1 ) + 2 ] = 2;

			// Top face triangles
			int topLevelIndex = sideFaces + 2;
			for ( int i = sideFaces; i < 2 * sideFaces - 1; i++ ) {
				indices[ 3 * i + 0 ] = 1;
				indices[ 3 * i + 1 ] = ( short )topLevelIndex;
				indices[ 3 * i + 2 ] = ( short )( topLevelIndex + 1 );

				topLevelIndex += 1;
			}

			indices[ 3 * ( 2 * sideFaces - 1 ) + 0 ] = 1;
			indices[ 3 * ( 2 * sideFaces - 1 ) + 1 ] = ( short )topLevelIndex;
			indices[ 3 * ( 2 * sideFaces - 1 ) + 2 ] = ( short )( sideFaces + 2 );

			// Side faces triangles
			bottomLevelIndex = 2;
			topLevelIndex = sideFaces + 2;
			for ( int i = 2 * sideFaces + 1; i < 4 * sideFaces - 1; i += 2 ) {
				indices[ 3 * ( i - 1 ) + 0 ] = ( short )bottomLevelIndex;
				indices[ 3 * ( i - 1 ) + 1 ] = ( short )( bottomLevelIndex + 1 );
				indices[ 3 * ( i - 1 ) + 2 ] = ( short )( topLevelIndex + 1 );
				indices[ 3 * i + 0 ] = ( short )( topLevelIndex + 1 );
				indices[ 3 * i + 1 ] = ( short )topLevelIndex;
				indices[ 3 * i + 2 ] = ( short )bottomLevelIndex;

				bottomLevelIndex++;
				topLevelIndex++;
			}

			indices[ 3 * ( 4 * sideFaces - 2 ) + 0 ] = ( short )bottomLevelIndex;
			indices[ 3 * ( 4 * sideFaces - 2 ) + 1 ] = 2;
			indices[ 3 * ( 4 * sideFaces - 2 ) + 2 ] = ( short )( sideFaces + 2 );
			indices[ 3 * ( 4 * sideFaces - 1 ) + 0 ] = ( short )( sideFaces + 2 );
			indices[ 3 * ( 4 * sideFaces - 1 ) + 1 ] = ( short )topLevelIndex;
			indices[ 3 * ( 4 * sideFaces - 1 ) + 2 ] = ( short )bottomLevelIndex;

			IndexBuffer indexBuffer = new IndexBuffer( graphics.GraphicsDevice, typeof( short ), indices.Length, BufferUsage.WriteOnly );
			indexBuffer.SetData<short>( indices );

			return indexBuffer;
		}

		public static int PrimitiveCountCylinder( int sideFaces ) {
			if ( sideFaces < 1 ) {
				return -1;
			} else if ( sideFaces == 1 ) {
				return PrimitiveCountLine();
			}

			return 4 * sideFaces;
		}

		public static VertexBuffer IndexedVertexBufferLine( GraphicsDeviceManager graphics ) {
			VertexPositionColor[] vertices = new VertexPositionColor[ 3 ];
			vertices[ 0 ] = new VertexPositionColor( new Vector3( 0.0f, -0.5f, 0.0f ), Color.White );
			vertices[ 1 ] = new VertexPositionColor( new Vector3( 0.0f, 0.0f, 0.0f ), Color.White );
			vertices[ 2 ] = new VertexPositionColor( new Vector3( 0.0f, 0.5f, 0.0f ), Color.White );

			VertexBuffer vertexBuffer = new VertexBuffer( graphics.GraphicsDevice, typeof( VertexPositionColor ), vertices.Length, BufferUsage.WriteOnly );
			vertexBuffer.SetData<VertexPositionColor>( vertices );

			return vertexBuffer;
		}

		public static IndexBuffer IndexBufferLine( GraphicsDeviceManager graphics ) {
			short[] indices = new short[ 3 ];
			indices[ 0 ] = 0;
			indices[ 1 ] = 1;
			indices[ 2 ] = 2;

			IndexBuffer indexBuffer = new IndexBuffer( graphics.GraphicsDevice, typeof( short ), indices.Length, BufferUsage.WriteOnly );
			indexBuffer.SetData<short>( indices );

			return indexBuffer;
		}

		public static int PrimitiveCountLine() {
			return 1;
		}

	}

}
