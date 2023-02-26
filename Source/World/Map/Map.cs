using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Honeymoon.Source.World.Map
{
	public class Map
	{
		private List<XmlDataCache.Map> maps = new List<XmlDataCache.Map>();
		private const int tileWidth = 16;
		private const int tileHeight = 16;
		public const int scale = 4;

		public Map()
		{
			XmlDataCache.Map hm_map_0 = Globals.content.Load<XmlDataCache.Map>("Maps/hm_map_0");
			maps.Add(hm_map_0);
			XmlDataCache.Map hm_map_1 = Globals.content.Load<XmlDataCache.Map>("Maps/hm_map_1");
			maps.Add(hm_map_1);
			XmlDataCache.Map hm_map_2 = Globals.content.Load<XmlDataCache.Map>("Maps/hm_map_2");
			maps.Add(hm_map_2);
		}

		public virtual void Update()
		{
		}

		public virtual void Draw(int mapID)
		{
			foreach (XmlDataCache.Map map in maps)
			{
				if (map.mapId == mapID)
				{
					foreach (var layer in map.layers)
					{
						for (int y = 0; y < layer.tilesHeight; y++)
						{
							for (int x = 0; x < layer.tilesWidth; x++)
							{
								int index = layer.tileData[y * layer.tilesWidth + x] - 1;
								if (index == -1) continue;
								Rectangle destination = new Rectangle(x * tileWidth * scale, y * tileHeight * scale, tileWidth * scale, tileHeight * scale);
								Globals.spriteBatch.Draw(TilesetManager.tilesets[layer.tileset].texture, destination, TilesetManager.tilesets[layer.tileset].tiles[index], Color.White);
							}
						}
					}
				}
			}
		}
	}
}
