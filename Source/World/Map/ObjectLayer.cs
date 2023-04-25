using Honeymoon.Managers;
using Honeymoon.Source.SavedData;
using Honeymoon.Source.World.Creatures.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honeymoon.Source.World.Map
{
	public class ObjectLayer
	{
		public static List<MapObject> mapObjects;
		public WorldSave linkedSave;
		private Texture2D rectanglexdddd,treeTop;

		public ObjectLayer(WorldSave save)
		{
			//this.mapObjects = save.mapObjectsData;
			this.linkedSave = save;
			mapObjects = save.mapObjectsData;
			XmlDataCache.ObjectData objectData = Globals.content.Load<XmlDataCache.ObjectData>("Data/ObjectData");
			rectanglexdddd = new Texture2D(Globals._graphics.GraphicsDevice, 1, 1);
			rectanglexdddd.SetData(new Color[] { Color.Red });
			treeTop = Globals.content.Load<Texture2D>("Objects/treeTop");
			foreach (var mapObject in linkedSave.mapObjectsData)
			{
				if (mapObject.name == "treeTrunk")
				{
					mapObject.texture = Globals.content.Load<Texture2D>("Objects/treeTrunk");
					mapObject.sourceData = new Rectangle(0, 0, 16, 16);
					mapObject.color = Color.White;
					mapObject.drawAboveEntity = objectData.objectData[0].drawAboveEntity;
					mapObject.hitBox = new Rectangle((int)mapObject.position.X, (int)mapObject.position.Y, 64, 64);
					mapObject.isPassable = objectData.objectData[0].isPassable;
				}
				else if (mapObject.name == "mushrooms")
				{
					mapObject.texture = Globals.content.Load<Texture2D>("Objects/mushrooms");
					mapObject.sourceData = new Rectangle(0, 0, 16, 16);
					mapObject.color = Color.White;
					mapObject.hitBox = new Rectangle((int)mapObject.position.X, (int)mapObject.position.Y, 64, 64);
					mapObject.isPassable = objectData.objectData[1].isPassable;
				}
				else if (mapObject.name == "rock")
				{
					mapObject.texture = Globals.content.Load<Texture2D>("Objects/rock");
					int[] sources = new int[] { 0, 16, 32 };
					mapObject.sourceData = new Rectangle(sources[Globals.random.Next(0,sources.Length)], 0, 16, 16);
					mapObject.color = Color.White;
					mapObject.hitBox = new Rectangle((int)mapObject.position.X, (int)mapObject.position.Y, 64, 64);
					mapObject.isAnimated = objectData.objectData[2].isAnimated;
					mapObject.isPassable = objectData.objectData[2].isPassable;
					mapObject.framesCount = objectData.objectData[2].framesCount;
					mapObject.frameSpeed = objectData.objectData[2].frameSpeed;
				}
			}
		}
		public virtual void Update()
		{
			for (var i = 0; i < linkedSave.mapObjectsData.Count; i++)
			{
				if (linkedSave.mapObjectsData[i].IsObjectClicked())
				{
					linkedSave.mapObjectsData.RemoveAt(i);
				}
				if (linkedSave.mapObjectsData[i].isAnimated)
				{
					linkedSave.mapObjectsData[i].PlayAnimation();
				}
			}
		}
		public virtual void DrawUnder()
		{
			foreach (var mapObject in linkedSave.mapObjectsData)
			{
				Rectangle destination = new Rectangle((int)mapObject.position.X, (int)mapObject.position.Y, mapObject.sourceData.Width * Map.scale, mapObject.sourceData.Height * Map.scale);
				if (!mapObject.drawAboveEntity)
				{
					if (mapObject.mapid == Map.GetCurrentMapId())
					{
						Globals.spriteBatch.Draw(mapObject.texture, destination, mapObject.sourceData, mapObject.color, mapObject.rotation, mapObject.origin, SpriteEffects.None, 0);
					}
				}
				if (mapObject.IsObjectHovered())
				{
					Globals.spriteBatch.Draw(rectanglexdddd, mapObject.hitBox, Color.Red);
				}
			}
		}
		public virtual void DrawAbove()
		{
			foreach (var mapObject in linkedSave.mapObjectsData)
			{
				Rectangle destination = new Rectangle((int)mapObject.position.X, (int)mapObject.position.Y, mapObject.sourceData.Width * Map.scale, mapObject.sourceData.Height * Map.scale);
				if (mapObject.drawAboveEntity)
				{
					if (mapObject.mapid == Map.GetCurrentMapId())
					{
						Globals.spriteBatch.Draw(mapObject.texture, destination, mapObject.sourceData, mapObject.color, mapObject.rotation, mapObject.origin, SpriteEffects.None, 0);
					}
				}
				if (mapObject.IsObjectHovered())
				{
					Globals.spriteBatch.Draw(rectanglexdddd, mapObject.hitBox, Color.Red);
				}
				if (mapObject.name == "treeTrunk")
				{
					Globals.spriteBatch.Draw(treeTop, new Rectangle((int)mapObject.position.X - 64, (int)mapObject.position.Y - 320, 192, 320), new Rectangle(0, 0, 48, 80), Color.White);
				}
			}
		}
	}
}
