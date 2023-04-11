using Honeymoon.Managers;
using Honeymoon.Source.SavedData;
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

		public ObjectLayer(WorldSave save)
		{
			this.mapObjects = save.mapObjectsData;
			XmlDataCache.ObjectData objectData = Globals.content.Load<XmlDataCache.ObjectData>("Data/ObjectData");
			foreach (var data in mapObjects)
			{
				foreach (var mapObject in data)
				{
					if (mapObject.name == "tree")
					{
						mapObject.texture = Globals.content.Load<Texture2D>("Objects/tree");
						mapObject.sourceData = new Rectangle(0, 0, 48, 112);
						mapObject.color = Color.White;
						mapObject.position.X -= 64;
						mapObject.position.Y -= 384;
					}
					else if (mapObject.name == "mushrooms")
					{
						mapObject.texture = Globals.content.Load<Texture2D>("Objects/mushrooms");
						mapObject.sourceData = new Rectangle(0, 0, 16, 16);
						mapObject.color = Color.White;
					}
				}
			}
		}
		public virtual void Update()
		{
			foreach (var data in mapObjects)
			{
				foreach (var mapObject in data)
				{
					mapObject.hitBox = new Rectangle((int)mapObject.position.X, (int)mapObject.position.Y, mapObject.sourceData.Width * Map.scale, mapObject.sourceData.Height * Map.scale);
					if (mapObject.name == "tree")
					{
						if (mapObject.hitBox.Contains(Globals.player.position))
						{
							if (mapObject.color.A > 90)
							{
								mapObject.color.A -= 10;
							}
						}
						else
						{
							//mapObject.color.A = 255;
							if (mapObject.color.A != 255)
							{
								mapObject.color.A += 5;
							}
						}
					}
				}
			}
		}
		public virtual void Draw()
		{
			foreach (var data in mapObjects)
			{
				foreach (var mapObject in data)
				{
					if (mapObject.mapid == Map.GetCurrentMapId())
					{
						Globals.spriteBatch.Draw(mapObject.texture, new Rectangle((int)mapObject.position.X, (int)mapObject.position.Y, mapObject.sourceData.Width * Map.scale, mapObject.sourceData.Height * Map.scale), mapObject.sourceData, mapObject.color, mapObject.rotation, mapObject.origin, SpriteEffects.None, 0);
					}
				}
			}
		}
	}
}
