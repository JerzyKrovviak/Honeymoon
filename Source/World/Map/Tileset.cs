using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Honeymoon.Source.World.Map
{
	public class Tileset
	{
		public Texture2D texture;
		public int tileWidth;
		public int tileHeight;
		public Rectangle[] tiles;
		public int[] collisionTiles;

		public Tileset(Texture2D loadedTexture)
		{
			texture = loadedTexture;
			tileWidth = 16;
			tileHeight = 16;
			tiles = Array.Empty<Rectangle>();
			collisionTiles = Array.Empty<int>();
		}
	}
}
