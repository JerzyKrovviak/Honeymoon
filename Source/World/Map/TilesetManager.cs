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
			tilesets.Add(new Tileset(Globals.content.Load<Texture2D>("Tilesets/hm_tileset_plain")));
			tilesets[0].firstgid = 1;
			//tilesets.Add(new Tileset(Globals.content.Load<Texture2D>("Tilesets/hm_tileset_animated")));
			XmlDataCache.TileData tileData = Globals.content.Load<XmlDataCache.TileData>("Data/TileData");

			for (int i = 0; i < tilesets.Count; i++)
			{
				tilesets[i].columns = tilesets[i].texture.Width / tilesets[i].tileWidth;
				tilesets[i].rows = tilesets[i].texture.Height / tilesets[i].tileHeight;
				tilesets[i].totalTiles = tilesets[i].rows * tilesets[i].columns;
				if (i > 0)
				{
					tilesets[i].firstgid = tilesets[i - 1].firstgid + tilesets[i - 1].totalTiles;
				}
				tilesets[i].tiles = new Tile[tilesets[i].totalTiles];
				for (int y = 0; y < tilesets[i].rows; y++)
				{
					for (int x = 0; x < tilesets[i].columns; x++)
					{
						int index = y * tilesets[i].columns + x;
						var source = new Rectangle(x * tilesets[i].tileWidth, y * tilesets[i].tileHeight, tilesets[i].tileWidth, tilesets[i].tileHeight);
						tilesets[i].tiles[index] = new Tile(index + tilesets[i].firstgid, source);
						for (int d = 0; d < tileData.tileData.Count; d++)
						{
							if (tilesets[i].tiles[index].id == tileData.tileData[d].gid)
							{
								tilesets[i].tiles[index].collision = tileData.tileData[d].collision;
								tilesets[i].tiles[index].isAnimated = tileData.tileData[d].isAnimated;
								tilesets[i].tiles[index].framesCount = tileData.tileData[d].framesCount;
								tilesets[i].tiles[index].frameSpeed = tileData.tileData[d].frameSpeed;
								tilesets[i].tiles[index].color.A = (byte)tileData.tileData[d].colorA;
							}
						}
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
