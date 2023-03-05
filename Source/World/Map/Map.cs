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
		public const int tileWidth = 16;
		public const int tileHeight = 16;
		public const int scaledTileWidth = tileWidth * scale;
		public const int scaledTileHeight = tileHeight * scale;
		public const int scale = 4;
		public static int tilesWidth;
		public static int tilesHeight;
		public static int currentMap;

		private List<XmlDataCache.Map> maps = new List<XmlDataCache.Map>();

		public Texture2D rectanglexdddd;
		
		public Map()
		{
			XmlDataCache.Map hm_map_0 = Globals.content.Load<XmlDataCache.Map>("Maps/hm_map_0");
			maps.Add(hm_map_0);

			rectanglexdddd = new Texture2D(Globals._graphics.GraphicsDevice, 1, 1);
			rectanglexdddd.SetData(new Color[] { Color.Red });
			//System.Diagnostics.Debug.WriteLine(TilesetManager.GetTilesetTextureByGid(18));
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

		public static Vector2 TileIdPosToXY(Vector2 position)
		{
			return new Vector2((int)position.X * scaledTileWidth, (int)position.Y * scaledTileHeight);
		}

		public static Vector2 GetPlayerTile()
		{
			return new Vector2(((int)Globals.player.hitBox.X + Globals.player.hitBox.Width / 2) / scaledTileWidth, ((int)Globals.player.hitBox.Y + Globals.player.hitBox.Height / 2) / scaledTileHeight);
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
								int gid = layer.tileData[y * layer.tilesWidth + x];
								if (gid == 0) continue;
								var tileset = TilesetManager.GetTilesetByGid(gid);
								//System.Diagnostics.Debug.WriteLine(tileset.tiles[gid - tileset.firstgid].id);
								var source = tileset.tiles[gid - tileset.firstgid].sourceData;
								//System.Diagnostics.Debug.WriteLine("gid: " + gid + " texture: " + tileset.texture + " source: " + source + " id: " + tileset.tiles[gid - 1].id + " layer: " + layer.layerName);
								Rectangle destination = new Rectangle(x * tileWidth * scale, y * tileHeight * scale, tileWidth * scale, tileHeight * scale);
								//tileset.tiles[gid].sourceData przy tym sie pierdoli
								//System.Diagnostics.Debug.WriteLine("gid: " + gid + " texture: " + tileset.texture + " source: " + tileset.tiles[gid].sourceData);
								//System.Diagnostics.Debug.WriteLine("drawing tile gid: " + gid + " source" + source + " tileset: " + tilesets[i].texture);
								Globals.spriteBatch.Draw(tileset.texture, destination, source, Color.White);
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
							Globals.spriteBatch.DrawString(FontManager.hm_f_outline, "" + x + "," + y, new Vector2(tileRect.X, tileRect.Y), Color.Yellow, 0f, Vector2.Zero, 1, SpriteEffects.None, 0);
						}
					}
				}
			}
			Globals.spriteBatch.Draw(rectanglexdddd, Globals.player.hitBox, Color.Red);
		}
	}
}
