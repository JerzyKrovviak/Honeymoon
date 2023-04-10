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
	[DataContract]
	public class GraphicsComponent
	{
		//[DataMember]
		public Texture2D texture;
		//[DataMember]
		public Rectangle sourceData, inGameData;
		//[DataMember]
		public Color color;
		//[DataMember]
		public bool flipHorizontal, flipVertical;

		public GraphicsComponent() { }
	}
}
