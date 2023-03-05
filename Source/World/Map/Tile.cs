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

		public int firstFrame, lastFrame;
		public float frameSpeed;
		public double timeSinceLastFrame;

		public Tile(int id, Rectangle sourceData)
		{
			this.id = id;
			this.sourceData = sourceData;
		}

		public void PlayAnimation()
		{
			timeSinceLastFrame += Globals.gameTime.ElapsedGameTime.TotalMilliseconds;
			if (timeSinceLastFrame > frameSpeed)
			{
				if (id < lastFrame - 1)
				{
					id += 1;
				}
				else
					id = firstFrame;
				timeSinceLastFrame = 0;
			}
		}
	}
}
