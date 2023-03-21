using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Honeymoon.Source.World
{
	public class FrameSet
	{
		public Texture2D texture;
		public int columns, rows, totalFrames, frameWidth, frameHeight;
		public Vector2[] frameData;

		public FrameSet(Texture2D texture)
		{
			this.texture = texture;
		}
	}

	public class AnimatedComponent : GraphicsComponent
	{
		public string name;
		public int framesCount;
		public int currentFrame;
		public int frameWidth;
		public int frameHeight;
		public Vector2[] frameData;
		public double frameSpeed;
		public double timeSinceLastFrame;

		public AnimatedComponent(Texture2D loadedTexture, string name, int frameWidth, int frameHeight, Vector2[] frameData, double frameSpeed, Color color)
		{
			this.texture = loadedTexture;
			this.name = name;
			this.frameWidth = frameWidth;
			this.frameHeight = frameHeight;
			this.frameData = frameData;
			this.frameSpeed = frameSpeed;
			this.color = color;
			this.framesCount = frameData.Length;
			sourceData.Width = frameWidth;
			sourceData.Height = frameHeight;
		}
		public void PlayAnimation()
		{
			for (int f = 0; f < frameData.Length; f++)
			{
				timeSinceLastFrame += Globals.gameTime.ElapsedGameTime.TotalMilliseconds;
				if (timeSinceLastFrame > frameSpeed)
				{
					if (currentFrame < framesCount)
					{
						sourceData.X = (int)frameData[f].X;
						currentFrame += 1;
					}
					else
					{
						currentFrame = 0;
						timeSinceLastFrame = 0;
					}
				}
			}
		}

		public void StopAnimation()
		{
			currentFrame = 0;
			sourceData.X = (int)frameData[0].X;
			timeSinceLastFrame = 0;
		}
	}
}
