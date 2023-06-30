using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Honeymoon.Source.World.Map;
using System.Runtime.Serialization;

namespace Honeymoon.Source.World
{
	[DataContract(Namespace = "")]
	public class PhysicalComponent : GraphicsComponent
	{
		public Vector2 position, velocity, origin;
		public Rectangle hitBox;
		public float rotation, speed;
		public int hitPoints;
		public string direction;
		public Vector2 oldPosition;
		public virtual void Draw()
		{
			inGameData = new Rectangle((int)position.X, (int)position.Y, sourceData.Width * Map.Map.scale, sourceData.Height * Map.Map.scale);
			Globals.spriteBatch.Draw(texture, inGameData, sourceData, color, rotation, origin, flipHorizontal ? SpriteEffects.FlipHorizontally : flipVertical ? SpriteEffects.FlipVertically : SpriteEffects.None, 0);
		}
		public Vector2 GetEntityHitboxTile()
		{
			return new Vector2((hitBox.X + hitBox.Width) / Map.Map.scaledTileWidth, (hitBox.Y + hitBox.Height) / Map.Map.scaledTileHeight);
		}
		public static Vector2 TileIdPosToXY(Vector2 position)
		{
			return new Vector2(position.X * Map.Map.scaledTileWidth, position.Y * Map.Map.scaledTileWidth);
		}
		private static Rectangle GetTileBounds(int x, int y)
		{
			return new Rectangle(x * Map.Map.scaledTileWidth, y * Map.Map.scaledTileHeight, Map.Map.scaledTileWidth, Map.Map.scaledTileHeight);
		}
		public Rectangle GetPlayerBounds()
		{
			return new Rectangle(hitBox.X, hitBox.Y, hitBox.Width, hitBox.Height);
		}
		private bool IsTileCollision(int x, int y)
		{
			foreach (XmlDataCache.Map map in Map.Map.maps)
			{
				if (map.mapId == Map.Map.GetCurrentMapId())
				{
					foreach (var layer in map.layers)
					{
						int gid = layer.tileData[y * layer.tilesWidth + x];
						if (gid == 0) continue;
						if (TilesetManager.collisionTiles.Contains(gid))
						{
							return true;
						}
					}
				}
			}
			return false;
		}
		public string GetTileStepSound(int x, int y)
		{
			foreach (XmlDataCache.Map map in Map.Map.maps)
			{
				if (map.mapId == Map.Map.GetCurrentMapId())
				{
					foreach (var layer in map.layers)
					{
						int gid = layer.tileData[y * layer.tilesWidth + x];
						if (gid == 0) continue;
						if (TilesetManager.sandSoundTiles.Contains(gid))
						{
							string[] sounds = new string[] { "walkSand", "walkSand2" };
							return sounds[Globals.random.Next(0,sounds.Length)];
						}
						else if (TilesetManager.grassSoundTiles.Contains(gid))
						{
							string[] sounds = new string[] { "walkGrass", "walkGrass2" };
							return sounds[Globals.random.Next(0, sounds.Length)];
						}
					}
				}
			}
			return "walkSand";
		}
		protected bool IsTouchingLeft(Rectangle rect)
		{
			return this.hitBox.Right + this.velocity.X > rect.Left &&
			  this.hitBox.Left < rect.Left &&
			  this.hitBox.Bottom > rect.Top &&
			  this.hitBox.Top < rect.Bottom;
		}
		protected bool IsTouchingRight(Rectangle rect)
		{
			return this.hitBox.Left + this.velocity.X < rect.Right &&
			  this.hitBox.Right > rect.Right &&
			  this.hitBox.Bottom > rect.Top &&
			  this.hitBox.Top < rect.Bottom;
		}
		protected bool IsTouchingTop(Rectangle rect)
		{
			return this.hitBox.Bottom + this.velocity.Y > rect.Top &&
			  this.hitBox.Top < rect.Top &&
			  this.hitBox.Right > rect.Left &&
			  this.hitBox.Left < rect.Right;
		}
		protected bool IsTouchingBottom(Rectangle rect)
		{
			return this.hitBox.Top + this.velocity.Y < rect.Bottom &&
			  this.hitBox.Bottom > rect.Bottom &&
			  this.hitBox.Right > rect.Left &&
			  this.hitBox.Left < rect.Right;
		}
		public void HandleCollisions()
		{
			Rectangle playerBounds = GetPlayerBounds();
			int TileSize = 64;
			int leftTile = playerBounds.Left / TileSize;
			int topTile = playerBounds.Top / TileSize;
			int rightTile = (int)Math.Ceiling((float)playerBounds.Right / TileSize) - 1;
			int bottomTile = (int)Math.Ceiling(((float)playerBounds.Bottom / TileSize)) - 1;
			for (int y = topTile; y <= bottomTile; ++y)
			{
				for (int x = leftTile; x <= rightTile; ++x)
				{
					if (IsTileCollision(x, y))
					{
						if (this.velocity.X > 0 && this.IsTouchingLeft(GetTileBounds(x, y)))
						{
							this.velocity.X = 0;
						}
						else if (this.velocity.X < 0 & this.IsTouchingRight(GetTileBounds(x, y)))
						{
							this.velocity.X = 0;
						}

						if (this.velocity.Y > 0 && this.IsTouchingTop(GetTileBounds(x, y)))
						{
							this.velocity.Y = 0;
						}
						else if (this.velocity.Y < 0 & this.IsTouchingBottom(GetTileBounds(x, y)))
						{
							this.velocity.Y = 0;
						}
					}
				}
			}
			foreach (var obj in ObjectLayer.mapObjects)
			{
				if (!obj.isPassable)
				{
					if (this.velocity.X > 0 && this.IsTouchingLeft(obj.hitBox))
					{
						this.velocity.X = 0;
					}
					else if (this.velocity.X < 0 & this.IsTouchingRight(obj.hitBox))
					{
						this.velocity.X = 0;
					}

					if (this.velocity.Y > 0 && this.IsTouchingTop(obj.hitBox))
					{
						this.velocity.Y = 0;
					}
					else if (this.velocity.Y < 0 & this.IsTouchingBottom(obj.hitBox))
					{
						this.velocity.Y = 0;
					}
				}
			}
		}
		public void CheckForWindowBounds()
		{
			if (position.X < 0)
			{
				position.X = 0;
			}
			else if (position.X + hitBox.Width > Map.Map.GetCurrentMapScaledSizePixels().X)
			{
				position.X = Map.Map.GetCurrentMapScaledSizePixels().X - hitBox.Width;
			}

			if (position.Y < 0)
			{
				position.Y = 0;
			}
			else if (position.Y + hitBox.Height > Map.Map.GetCurrentMapScaledSizePixels().Y)
			{
				position.Y = Map.Map.GetCurrentMapScaledSizePixels().Y - hitBox.Height;
			}
		}
		public PhysicalComponent() { }
	}
}
