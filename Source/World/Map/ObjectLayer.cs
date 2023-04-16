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
		private List<List<MapObject>> mapObjects;
		private Texture2D rectanglexdddd;

		public ObjectLayer(WorldSave save)
		{
			this.mapObjects = save.mapObjectsData;
			XmlDataCache.ObjectData objectData = Globals.content.Load<XmlDataCache.ObjectData>("Data/ObjectData");
			rectanglexdddd = new Texture2D(Globals._graphics.GraphicsDevice, 1, 1);
			rectanglexdddd.SetData(new Color[] { Color.Red });
			foreach (var data in mapObjects)
			{
				foreach (var mapObject in data)
				{
					if (mapObject.name == "tree")
					{
						mapObject.texture = Globals.content.Load<Texture2D>("Objects/tree");
						mapObject.sourceData = new Rectangle(0, 0, 48, 96);
						mapObject.color = Color.White;
						mapObject.position.X -= 64;
						mapObject.position.Y -= 320;
						mapObject.drawAboveEntity = true;
						mapObject.hitBox = new Rectangle((int)mapObject.position.X + 64, (int)mapObject.position.Y + 320, 64, 64);
					}
					else if (mapObject.name == "mushrooms")
					{
						mapObject.texture = Globals.content.Load<Texture2D>("Objects/mushrooms");
						mapObject.sourceData = new Rectangle(0, 0, 16, 16);
						mapObject.color = Color.White;
						mapObject.hitBox = new Rectangle((int)mapObject.position.X, (int)mapObject.position.Y, 64, 64);
					}
					else if (mapObject.name == "rock")
					{
						mapObject.texture = Globals.content.Load<Texture2D>("Objects/rock");
						mapObject.sourceData = new Rectangle(0, 0, 16, 16);
						mapObject.color = Color.White;
						mapObject.hitBox = new Rectangle((int)mapObject.position.X, (int)mapObject.position.Y, 64, 64);
						mapObject.isAnimated = objectData.objectData[2].isAnimated;
						mapObject.framesCount = objectData.objectData[2].framesCount;
						mapObject.frameSpeed = objectData.objectData[2].frameSpeed;
					}
					//mapObject.hitBox = new Rectangle((int)mapObject.position.X, (int)mapObject.position.Y, mapObject.sourceData.Width * Map.scale, mapObject.sourceData.Height * Map.scale);
				}
			}
		}
		public virtual void Update()
		{
			foreach (var objData in mapObjects)
			{
				for (var i = 0; i < objData.Count; i++)
				{
					if (objData[i].IsObjectClicked())
					{
						objData.RemoveAt(i);
					}
					if (objData[i].isAnimated)
					{
						objData[i].PlayAnimation();
					}
				}
			}
		}
		public virtual void DrawUnder()
		{
			foreach (var data in mapObjects)
			{
				foreach (var mapObject in data)
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
		}
		public virtual void DrawAbove()
		{
			foreach (var data in mapObjects)
			{
				foreach (var mapObject in data)
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
					//Globals.spriteBatch.Draw(rectanglexdddd, mapObject.hitBox, new Color(Color.Blue, 80));
					//Globals.spriteBatch.DrawString(FontManager.hm_f_default, mapObject.name, new Vector2(mapObject.hitBox.X, mapObject.hitBox.Y), Color.Red);
				}
			}
		}
	}
}
