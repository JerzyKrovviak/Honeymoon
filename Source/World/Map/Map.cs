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

		private static List<XmlDataCache.Map> maps = new List<XmlDataCache.Map>();

		public Texture2D rectanglexdddd;
		
		public Map()
		{
			XmlDataCache.Map hm_map_0 = Globals.content.Load<XmlDataCache.Map>("Maps/hm_map_0");
			maps.Add(hm_map_0);

			rectanglexdddd = new Texture2D(Globals._graphics.GraphicsDevice, 1, 1);
			rectanglexdddd.SetData(new Color[] { Color.Red });
		}

		public static Vector2 GetEntityTile(PhysicalComponent entity)
		{
			return new Vector2((entity.hitBox.X + entity.hitBox.Width) / scaledTileWidth, (entity.hitBox.Y + entity.hitBox.Height) / scaledTileHeight);
		}

		public static bool CheckForCollision(PhysicalComponent entity)
		{
			foreach (XmlDataCache.Map map in maps)
			{
				if (map.mapId == GetCurrentMapId())
				{
					foreach (var layer in map.layers)
					{
						for (int y = (int)GetEntityTile(entity).Y - 1; y < (int)GetEntityTile(entity).Y + 1; y++)
						{
							for (int x = (int)GetEntityTile(entity).X - 1; x < (int)GetEntityTile(entity).X + 1; x++)
							{
								int gid = layer.tileData[y * layer.tilesWidth + x];
								if (gid == 0) continue;
								var tileset = TilesetManager.GetTilesetByGid(gid);
								Rectangle destination = new Rectangle(x * tileWidth * scale, y * tileHeight * scale, tileWidth * scale, tileHeight * scale);

								if (entity.hitBox.Intersects(destination) && tileset.tiles[gid - tileset.firstgid].collision)
								{
									return true;
								}
							}
						}
					}
				}
			}
			return false;
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
			return new Vector2(position.X * scaledTileWidth, position.Y * scaledTileHeight);
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
								var source = tileset.tiles[gid - tileset.firstgid].sourceData;
								Rectangle destination = new Rectangle(x * tileWidth * scale, y * tileHeight * scale, tileWidth * scale, tileHeight * scale);
								Globals.spriteBatch.Draw(tileset.texture, destination, source, tileset.tiles[gid - tileset.firstgid].color);
							}
						}
					}
				}
			}
		}
		public virtual void DrawWorldObjects(int saveFileId)
		{ 
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
			//Rectangle destination = new Rectangle(TileIdPosToXY(GetEntityTile(Globals.player).X).X, GetEntityTile(Globals.player).Y);
			Vector2 rectpos = TileIdPosToXY(new Vector2(GetEntityTile(Globals.player).X, GetEntityTile(Globals.player).Y));
			Rectangle destination = new Rectangle((int)rectpos.X, (int)rectpos.Y, 64, 64);
			Globals.spriteBatch.Draw(rectanglexdddd, destination, Color.Red);
		}
	}
}
