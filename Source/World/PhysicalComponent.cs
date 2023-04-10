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
	public class PhysicalComponent : GraphicsComponent
	{
		public Vector2 position, velocity, origin;
		public Rectangle hitBox;
		public float rotation;
		public int hitPoints;
		public int direction;

		public virtual void Draw()
		{
			inGameData = new Rectangle((int)position.X, (int)position.Y, sourceData.Width * Map.Map.scale, sourceData.Height * Map.Map.scale);
			Globals.spriteBatch.Draw(texture, inGameData, sourceData, color, rotation, origin, flipHorizontal ? SpriteEffects.FlipHorizontally : flipVertical ? SpriteEffects.FlipVertically : SpriteEffects.None, 0);
		}
		public PhysicalComponent() { }
	}
}
