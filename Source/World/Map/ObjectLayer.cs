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
		//private List<MapObject[]> mapObjects;
		private List<List<MapObject>> mapObjects;
		private Texture2D treeXD;
		public ObjectLayer(WorldSave save)
		{
			this.mapObjects = save.mapObjectsData;
			treeXD = Globals.content.Load<Texture2D>("Objects/tree");
			foreach (var data in mapObjects)
			{
				foreach (var mapObject in data)
				{
					if (mapObject.name == "tree")
					{
						mapObject.texture = Globals.content.Load<Texture2D>("Objects/tree");
						mapObject.sourceData = new Rectangle(0, 0, 48, 112);
						mapObject.color = Color.White;
						mapObject.rotation = 0f;
						mapObject.origin = Vector2.Zero;
						//mapObject.position.X -= 64;
						//mapObject.position.Y -= 384;
						//System.Diagnostics.Debug.WriteLine(mapObject.position + " " + mapObject.sourceData);
					}
				}
			}
		}
		public virtual void Update()
		{
		}
		public virtual void Draw()
		{
			foreach (var data in mapObjects)
			{
				foreach (var mapObject in data)
				{
					if (mapObject.mapid == Map.GetCurrentMapId())
					{
						if (mapObject.name == "tree")
						{
							//mapObject.Draw();
							Globals.spriteBatch.Draw(mapObject.texture, new Rectangle((int)mapObject.position.X, (int)mapObject.position.Y, mapObject.sourceData.Width * Map.scale, mapObject.sourceData.Height * Map.scale), mapObject.sourceData, mapObject.color, mapObject.rotation, mapObject.origin, SpriteEffects.None, 0);
							Globals.spriteBatch.DrawString(FontManager.hm_f_default, mapObject.mapid + "\n" + mapObject.name, mapObject.position, Color.OrangeRed);
						}
					}
				}
			}
		}
	}
}
