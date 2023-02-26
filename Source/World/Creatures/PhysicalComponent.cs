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

namespace Honeymoon.Source.World.Creatures
{
	public class PhysicalComponent
	{
		public Texture2D texture;
		public Vector2 position;
		public Vector2 velocity;
		public Rectangle sourceData;
		public Rectangle hitBox;
		public Color color;
		public float rotation;
		public Vector2 origin;
		public int hitPoints;
		public int direction;
		public int framesCount;
		public int currentFrame;
		public float frameSpeed;
		public double timeSinceLastFrame;

		public void PlayAnimation()
		{
			timeSinceLastFrame += Globals.gameTime.ElapsedGameTime.TotalMilliseconds;
			if (timeSinceLastFrame > frameSpeed)
			{
				currentFrame += 1;
				timeSinceLastFrame = 0;
				if (currentFrame >= framesCount)
				{
					currentFrame = 0;
				}
			}

			sourceData.X = sourceData.Width * currentFrame;
		}

		public void StopAnimation()
		{
		}

		public virtual void Draw() //this will not call unlesss all values are not null
		{
			Globals.spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, sourceData.Width * Map.Map.scale, sourceData.Height * Map.Map.scale), sourceData, color, rotation, origin, SpriteEffects.None, 0);
		}
	}
}
