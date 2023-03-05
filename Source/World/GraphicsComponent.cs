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

namespace Honeymoon.Source.World
{
	public class GraphicsComponent
	{
		public Texture2D texture;
		public static Rectangle sourceData;
		public Color color;
		public bool flipHorizontal, flipVertical;
		public List<AnimatedComponent> animation = new List<AnimatedComponent>();
	}
}
