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
	public class TilesetManager
	{
		public static List<Tileset> tilesets = new List<Tileset>();

		public TilesetManager()
		{
			tilesets.Add(new Tileset(Globals.content.Load<Texture2D>("Tilesets/hm_tileset_common")));

			foreach (var tileset in tilesets)
			{
				int tilesetColumns = tileset.texture.Width / tileset.tileWidth;
				int tilesetRows = tileset.texture.Height / tileset.tileHeight;
				tileset.tiles = new Rectangle[tilesetColumns * tilesetRows];

				for (int y = 0; y < tilesetRows; y++)
				{
					for (int x = 0; x < tilesetColumns; x++)
					{
						int index = y * tilesetColumns + x;
						tileset.tiles[index] = new Rectangle(x * tileset.tileWidth, y * tileset.tileHeight, tileset.tileWidth, tileset.tileHeight);
					}
				}
			}
		}
	}
}
