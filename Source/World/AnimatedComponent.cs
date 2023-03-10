using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Honeymoon.Source.World
{
	public class AnimatedComponent : GraphicsComponent
	{
		public string name;
		public int framesCount;
		public int currentFrame;
		public int sourceY;
		public float frameSpeed;
		public double timeSinceLastFrame;

		public AnimatedComponent(string name, int framesCount, int sourceDataY, float frameSpeed)
		{
			this.name = name;
			this.framesCount = framesCount;
			this.frameSpeed = frameSpeed;
			this.sourceY = sourceDataY;
		}

		public void PlayAnimation()
		{
			timeSinceLastFrame += Globals.gameTime.ElapsedGameTime.TotalMilliseconds;
			if (timeSinceLastFrame > frameSpeed)
			{
				if (currentFrame < framesCount - 1)
				{
					currentFrame += 1;
				}
				else
					currentFrame = 0;
				timeSinceLastFrame = 0;
			}
			sourceData.Y = sourceY;
			sourceData.X = sourceData.Width * currentFrame;
		}

		public void StopAnimation()
		{
			currentFrame = 0;
			sourceData.X = sourceData.Width * currentFrame;
		}
	}
}
