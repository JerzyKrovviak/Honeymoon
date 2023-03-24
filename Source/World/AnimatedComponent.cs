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
		public float frameSpeed;
		public float timeSinceLastFrame;

		public AnimatedComponent(Texture2D loadedTexture, string name, int frameWidth, int frameHeight, Vector2[] frameData, float frameSpeed, Color color)
		{
			this.texture = loadedTexture;
			this.name = name;
			this.frameWidth = frameWidth;
			this.frameHeight = frameHeight;
			this.frameData = frameData;
			this.frameSpeed = frameSpeed;
			this.color = color;
			this.framesCount = frameData.Length;
			timeSinceLastFrame = 0;
			sourceData.Width = frameWidth;
			sourceData.Height = frameHeight;
		}
		public void PlayAnimation()
		{
			timeSinceLastFrame += (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;

			if (timeSinceLastFrame > frameSpeed)
			{
				currentFrame += 1;
				timeSinceLastFrame = 0;
				if (currentFrame >= framesCount)
				{
					currentFrame = 0;
				}
			}
			sourceData.X = (int)frameData[currentFrame].X;
			sourceData.Y = (int)frameData[currentFrame].Y;
		}

		public void StopAnimation()
		{
			currentFrame = 0;
			sourceData.X = (int)frameData[0].X;
			timeSinceLastFrame = 0;
		}

		public virtual void Draw(Vector2 position)
		{
			var posiition = new Rectangle((int)position.X, (int)position.Y, sourceData.Width * Map.Map.scale, sourceData.Height * Map.Map.scale);
			Globals.spriteBatch.Draw(texture, posiition, sourceData, color, 0f, Vector2.Zero, flipHorizontal ? SpriteEffects.FlipHorizontally : flipVertical ? SpriteEffects.FlipVertically : SpriteEffects.None, 0);
		}

		public virtual void DrawSpecialScaled(Vector2 position, int scale)
		{
			var posiition = new Rectangle((int)position.X, (int)position.Y, sourceData.Width * scale, sourceData.Height * scale);
			Globals.spriteBatch.Draw(texture, posiition, sourceData, color, 0f, Vector2.Zero, flipHorizontal ? SpriteEffects.FlipHorizontally : flipVertical ? SpriteEffects.FlipVertically : SpriteEffects.None, 0);
		}
	}
}
