﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Honeymoon.Managers;
using Honeymoon.Source.World.Creatures.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static Honeymoon.Source.World.Map.MapObject;

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

		public static List<XmlDataCache.Map> maps = new List<XmlDataCache.Map>();

		public Texture2D rectanglexdddd;
		
		public Map()
		{
			XmlDataCache.Map hm_map_0 = Globals.content.Load<XmlDataCache.Map>("Maps/hm_map_0");
			maps.Add(hm_map_0);
			XmlDataCache.Map hm_map_1 = Globals.content.Load<XmlDataCache.Map>("Maps/hm_map_1");
			maps.Add(hm_map_1);

			rectanglexdddd = new Texture2D(Globals._graphics.GraphicsDevice, 1, 1);
			rectanglexdddd.SetData(new Color[] { Color.Red });
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
		public Vector2 GetCameraRenderedMin()
		{
			return new Vector2(Math.Abs((int)Camera.cameraOffsetX / scaledTileWidth), Math.Abs((int)Camera.cameraOffsetY / scaledTileHeight));
		}
		public int GetCameraRenderedMaxX(int maxX)
		{
			if ((int)GetCameraRenderedMin().X + (Globals.windowSize.X / scaledTileWidth) <= maxX)
			{
				return (int)GetCameraRenderedMin().X + ((int)Globals.windowSize.X / scaledTileWidth);
			}
			return (int)GetCameraRenderedMin().X + ((int)Globals.windowSize.X / scaledTileWidth) - maxX;
		}
		public int GetCameraRenderedMaxY(int maxY)
		{
			if ((int)GetCameraRenderedMin().Y + 1 + (Globals.windowSize.Y / scaledTileHeight) <= maxY)
			{
				return (int)GetCameraRenderedMin().Y + 1 + ((int)Globals.windowSize.Y / scaledTileHeight);
			}
			return (int)GetCameraRenderedMin().Y + ((int)Globals.windowSize.Y / scaledTileHeight);
		}
		public virtual void Draw(int mapID)
		{
			//currentMap = mapID;
			//tilesWidth = maps[mapID].tilesWidth;
			//tilesHeight = maps[mapID].tilesHeight;
			//foreach (var layer in maps[mapID].layers)
			//{
			//	for (int y = 0; y < layer.tilesHeight; y++)
			//	{
			//		for (int x = 0; x < layer.tilesWidth; x++)
			//		{
			//			int gid = layer.tileData[y * layer.tilesWidth + x];
			//			if (gid == 0) continue;
			//			var tileset = TilesetManager.GetTilesetByGid(gid);
			//			var source = tileset.tiles[gid - tileset.firstgid].sourceData;
			//			Rectangle destination = new Rectangle(x * tileWidth * scale, y * tileHeight * scale, tileWidth * scale, tileHeight * scale);
			//			Globals.spriteBatch.Draw(tileset.texture, destination, source, tileset.tiles[gid - tileset.firstgid].color);
			//		}
			//	}
			//}
			currentMap = mapID;
			tilesWidth = maps[mapID].tilesWidth;
			tilesHeight = maps[mapID].tilesHeight;
			foreach (var layer in maps[mapID].layers)
			{
				for (int y = (int)GetCameraRenderedMin().Y; y <= GetCameraRenderedMaxY(tilesHeight); y++)
				{
					for (int x = (int)GetCameraRenderedMin().X; x <= GetCameraRenderedMaxX(tilesWidth); x++)
					{
						int gid = layer.tileData[y * layer.tilesWidth + x];
						if (gid == 0) continue;
						var tileset = TilesetManager.GetTilesetByGid(gid);
						var source = tileset.tiles[gid - tileset.firstgid].sourceData;
						Rectangle destination = new Rectangle(x * scaledTileWidth, y * scaledTileHeight, scaledTileWidth, scaledTileHeight);
						Globals.spriteBatch.Draw(tileset.texture, destination, source, tileset.tiles[gid - tileset.firstgid].color);
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
			//Rectangle destination = new Rectangle(TileIdPosToXY(GetEntityTile(Globals.player).X).X, GetEntityTile(Globals.player).Y);
			//Vector2 rectpos = TileIdPosToXY(new Vector2(Globals.player.GetEntityHitboxTile().X, Globals.player.GetEntityHitboxTile().Y));
			//Rectangle destination = new Rectangle((int)rectpos.X, (int)rectpos.Y, 64, 64);
			//Globals.spriteBatch.Draw(rectanglexdddd, destination, Color.Red);
			int leftTile = Globals.player.hitBox.Left / 64;
			int topTile = Globals.player.hitBox.Top / 64;
			int rightTile = (int)Math.Ceiling((float)Globals.player.hitBox.Right / 64) - 1;
			int bottomTile = (int)Math.Ceiling(((float)Globals.player.hitBox.Bottom / 64)) - 1;
			//int rightTile = (int)Math.Ceiling((float)Globals.player.hitBox.Right / 64) - 1;
			//int bottomTile = (int)Math.Ceiling(((float)Globals.player.hitBox.Bottom / 64)) - 1;
			for (int y = topTile; y <= bottomTile; ++y)
			{
				for (int x = leftTile; x <= rightTile; ++x)
				{
					Vector2 pos = TileIdPosToXY(new Vector2(x, y));
					Rectangle destination = new Rectangle((int)pos.X, (int)pos.Y, 64, 64);
					Globals.spriteBatch.Draw(rectanglexdddd, destination, new Color(Color.Blue, 120));
				}
			}
			Globals.spriteBatch.Draw(rectanglexdddd, Globals.player.hitBox, new Color(Color.Blue, 120));
		}
	}
}
