using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Honeymoon.Source.World.Map
{
	public class Tile 
	{
		public int id;
		public int tileWidth, tileHeight;
		public Rectangle sourceData;
		public bool collision, isAnimated;
		public Color color;

		public int framesCount;
		public int currentFrame;
		public float frameSpeed;
		public double timeSinceLastFrame;

		public Tile(int id, Rectangle sourceData)
		{
			this.id = id;
			this.sourceData = sourceData;
			this.color = Color.White;
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
			sourceData.X = sourceData.Width * currentFrame;
		}
	}
}
