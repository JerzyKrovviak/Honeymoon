using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Honeymoon.Source.World.Map
{
	[DataContract]
	public class MapObject : PhysicalComponent
	{
		[DataMember]
		public int mapid;
		[DataMember]
		public string name;
		[DataMember]
		public Vector2 position;

		public MapObject(int mapid, Texture2D texture, Rectangle sourcedata, string name, Vector2 position)
		{
			this.mapid = mapid;
			this.texture = texture;
			this.position = position;
			this.name = name;
		}
	}
}
