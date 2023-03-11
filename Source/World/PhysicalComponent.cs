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
	public class PhysicalComponent : GraphicsComponent
	{
		public Vector2 position;
		public Vector2 velocity;
		public Rectangle hitBox;
		public float rotation;
		public Vector2 origin;
		public int hitPoints;
		public int direction;

		public virtual void Draw() //this will not call properly unlesss all values are not null
		{
			Globals.spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, sourceData.Width * Map.Map.scale, sourceData.Height * Map.Map.scale), sourceData, color, rotation, origin, flipHorizontal ? SpriteEffects.FlipHorizontally : flipVertical ? SpriteEffects.FlipVertically : SpriteEffects.None, 0);
		}
	}
}
