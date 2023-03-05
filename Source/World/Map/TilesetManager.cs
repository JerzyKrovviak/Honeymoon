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
			tilesets[0].firstgid = 1;
			tilesets.Add(new Tileset(Globals.content.Load<Texture2D>("Tilesets/hm_tileset_animated")));

			for (int i = 0; i < tilesets.Count; i++)
			{
				tilesets[i].columns = tilesets[i].texture.Width / tilesets[i].tileWidth; //calculationg columns of tileset
				tilesets[i].rows = tilesets[i].texture.Height / tilesets[i].tileHeight; //calculatin rows of tileset
				tilesets[i].totalTiles = tilesets[i].rows * tilesets[i].columns; //calcuating total amount of tiles in tileset
				if (i > 0)
				{
					tilesets[i].firstgid = tilesets[i - 1].firstgid + tilesets[i - 1].totalTiles; // calculatin firstgid
				}
				tilesets[i].tiles = new Tile[tilesets[i].totalTiles];
				//System.Diagnostics.Debug.WriteLine(tilesets[i].tiles.Length);
				for (int y = 0; y < tilesets[i].rows; y++)
				{
					for (int x = 0; x < tilesets[i].columns; x++)
					{
						int index = y * tilesets[i].columns + x;
						System.Diagnostics.Debug.WriteLine(index);
						var source = new Rectangle(x * tilesets[i].tileWidth, y * tilesets[i].tileHeight, tilesets[i].tileWidth, tilesets[i].tileHeight);
						tilesets[i].tiles[index] = new Tile(index + tilesets[i].firstgid, source);
						System.Diagnostics.Debug.WriteLine("index: " + index + "   adding tile gid: " + tilesets[i].tiles[index].id + " source" + source + " tileset: " + tilesets[i].texture);
					}
				}
			}
		}
		public static Texture2D GetTilesetTextureByGid(int gid)
		{
			if (tilesets == null)
			{
				return null;
			}

			for (int i = 0; i < tilesets.Count; i++)
			{
				if (i < tilesets.Count - 1)
				{
					int firstgid = tilesets[i].firstgid;
					int firstgid2 = tilesets[i + 1].firstgid;
					if (gid >= firstgid && gid < firstgid2)
					{
						return tilesets[i].texture;
					}

					continue;
				}

				return tilesets[i].texture;
			}
			throw new Exception("Please select a value");
		}
		public static Tileset GetTilesetByGid(int gid)
		{
			if (tilesets == null)
			{
				return null;
			}

			for (int i = 0; i < tilesets.Count; i++)
			{
				if (i < tilesets.Count - 1)
				{
					int firstgid = tilesets[i].firstgid;
					int firstgid2 = tilesets[i + 1].firstgid;
					if (gid >= firstgid && gid < firstgid2)
					{
						return tilesets[i];
					}

					continue;
				}

				return tilesets[i];
			}

			return new Tileset(GetTilesetTextureByGid(gid));
		}

		public virtual void Update()
		{
			foreach (var tileset in tilesets)
			{
				foreach (var tile in tileset.tiles)
				{
					if (tile.isAnimated)
					{
						tile.PlayAnimation();
					}
				}
			}
		}
	}
}
