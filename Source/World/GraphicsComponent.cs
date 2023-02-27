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
		public List<Animation2d> animation = new List<Animation2d>();

		public class Animation2d
		{
			public string name;
			public int framesCount;
			public int currentFrame;
			public int sourceY;
			public float frameSpeed;
			public double timeSinceLastFrame;

			public Animation2d(string name, int framesCount, int sourceY, float frameSpeed)
			{
				this.name = name;
				this.framesCount = framesCount;
				this.sourceY = sourceY;
				this.frameSpeed = frameSpeed;
			}

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
				sourceData.Y = sourceY;
				sourceData.X = sourceData.Width * currentFrame;
			}

			public void StopAnimation()
			{
				sourceData.X = sourceData.Width * currentFrame;
				currentFrame = 0;
			}
		}
	}
}
