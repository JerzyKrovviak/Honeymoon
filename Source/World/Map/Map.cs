using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Honeymoon.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Honeymoon.Source.World.Map
{
	public class Map
	{
		private List<XmlDataCache.Map> maps = new List<XmlDataCache.Map>();
		public const int tileWidth = 16;
		public const int tileHeight = 16;
		public const int scaledTileWidth = tileWidth * scale;
		public const int scaledTileHeight = tileHeight * scale;
		public const int scale = 4;
		public static int tilesWidth;
		public static int tilesHeight;
		public static int currentMap;

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

		public static int GetCurrentMapId()
		{
			return currentMap;
		}

		public static Vector2 GetCurrentMapTilesSize()
		{
			return new Vector2(tilesWidth, tilesHeight);
		}

		public static Vector2 GetCurrentMapSizePixels()
		{
			return new Vector2(tilesWidth * GetCurrentMapTilesSize().X, tilesHeight * GetCurrentMapTilesSize().Y);
		}

		public static Vector2 GetCurrentMapScaledSizePixels()
		{
			return new Vector2(scaledTileWidth * GetCurrentMapTilesSize().X, scaledTileHeight * GetCurrentMapTilesSize().Y);
		}

		public virtual void Draw(int mapID)
		{
			currentMap = mapID;
			foreach (XmlDataCache.Map map in maps)
			{
				if (map.mapId == mapID)
				{
					tilesWidth = map.tilesWidth;
					tilesHeight = map.tilesHeight;
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
		public virtual void DrawDebugMode()
		{
			foreach (XmlDataCache.Map map in maps)
			{
				foreach (var layer in map.layers)
				{
					for (int y = 0; y < layer.tilesHeight; y++)
					{
						for (int x = 0; x < layer.tilesWidth; x++)
						{
							Rectangle tileRect = new Rectangle(x * scaledTileWidth, y * scaledTileHeight, 64, 64);
							//Globals.spriteBatch.Draw(Globals.player.texture, tileRect, Color.White);
							Globals.spriteBatch.DrawString(FontManager.hm_f_outline, "" + x + "," + y, new Vector2(tileRect.X, tileRect.Y), Color.Yellow, 0f, Vector2.Zero, 1, SpriteEffects.None, 0);
						}
					}
				}
			}
		}
	}
}
